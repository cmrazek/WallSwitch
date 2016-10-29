using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WallSwitch.SettingsStore;

namespace WallSwitch
{
	internal enum LocationType
	{
		File,
		Directory,
		Feed
	}

	internal class Location
	{
		private const int k_defaultUpdateInterval = 1;
		private const Period k_defaultUpdatePeriod = Period.Hours;

		private long _rowid;
		private string _path;
		private LocationType _type;
		private DateTime _lastUpdate = DateTime.MinValue;
		private Period _updatePeriod = k_defaultUpdatePeriod;
		private int _updateFreq = k_defaultUpdateInterval;
		private TimeSpan _updateInterval;
		private bool _disabled = false;

		public event EventHandler ContentUpdated;

		public Location(LocationType type, string path)
		{
			_type = type;
			_path = path;
			_updateInterval = TimeSpanUtil.CalcInterval(_updateFreq, _updatePeriod);
		}

		public Location(XmlElement topElement)
		{
			Load(topElement);
		}

		public Location(System.Data.DataRow row)
		{
			Load(row);
		}

		public Location Clone()
		{
			return new Location(_type, _path)
			{
				_updateInterval = _updateInterval,
				_disabled = _disabled
			};
		}

		public override string ToString()
		{
			return _path;
		}

		public string Path
		{
			get { return _path; }
			set
			{
				if (_path != value)
				{
					_path = value;

					// Since the path has changed, the file list is now obsolete.
					_lastUpdate = DateTime.MinValue;
					FireUpdated();
				}
			}
		}

		public bool SamePath(string path)
		{
			return _path.Equals(path, StringComparison.OrdinalIgnoreCase);
		}

		private void SyncDatabaseToImageRecList(IEnumerable<ImageRec> filesOnDisk, Theme theme, Database db, CancellationToken cancel)
		{
			var table = db.SelectDataTable("select rowid, * from img where location_id = @id", "@id", _rowid);

			// Find files in the database that don't exist on disk
			var filesToRemove = new List<long>();
			foreach (DataRow row in table.Rows)
			{
				if (cancel.IsCancellationRequested) return;

				var path = (string)row["path"];
				var found = false;
				foreach (var file in filesOnDisk)
				{
					if (string.Equals(file.Location, path, StringComparison.OrdinalIgnoreCase))
					{
						found = true;
						break;
					}
				}
				if (!found) filesToRemove.Add((long)row["rowid"]);
			}

			var changesMade = false;

			using (var tran = db.BeginTransaction())
			{
				if (filesToRemove.Count > 0)
				{
					using (var cmd = db.CreateCommand("delete from img where rowid = @rowid"))
					{
						foreach (var rowid in filesToRemove)
						{
							if (cancel.IsCancellationRequested) return;

							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@rowid", rowid);

							cmd.ExecuteNonQuery();

							changesMade = true;
						}
					}
				}

				// Find files on disk that don't exist in the database
				var filesToAdd = new List<ImageRec>();
				foreach (var file in filesOnDisk)
				{
					if (cancel.IsCancellationRequested) return;

					DataRow foundRow = null;
					foreach (DataRow row in table.Rows)
					{
						if (cancel.IsCancellationRequested) return;

						if (string.Equals(file.Location, (string)row["path"], StringComparison.OrdinalIgnoreCase))
						{
							foundRow = row;
							break;
						}
					}
					if (foundRow == null)
					{
						filesToAdd.Add(file);
					}
					else
					{
						// Check if new cache path needs to be updated in the img table
						if (file.Type == ImageLocationType.Url && string.IsNullOrEmpty(file.LocationOnDisk))
						{
							string cachePathName;
							if (ImageCache.TryGetCachedImage(db, file.Location, out cachePathName))
							{
								db.ExecuteNonQuery("update img set cache_path = @cache_path where path = @path",
									"@cache_path", cachePathName,
									"@path", file.Location);

								db.ExecuteNonQuery("update history set cache_path = @cache_path where path = @path",
									"@cache_path", cachePathName,
									"@path", file.Location);

								changesMade = true;
							}
						}

						// Check if the size needs updating
						if (foundRow.GetLong("size", -1) == -1 && !string.IsNullOrEmpty(file.LocationOnDisk))
						{
							file.RefreshFileSize();
							var size = file.Size;
							if (size.HasValue)
							{
								db.ExecuteNonQuery("update img set size = @size where path = @path",
									"@size", size.Value,
									"@path", file.Location);

								// No need to update history since it doesn't care about the size.

								changesMade = true;
							}
						}
					}
				}

				if (filesToAdd.Count > 0)
				{
					using (var cmd = db.CreateCommand("insert into img (theme_id, location_id, type, path, pub_date, rating, thumb, size)"
						+ " values (@theme_id, @location_id, @type, @path, @pub_date, @rating, @thumb, @size)"))
					{
						foreach (var img in filesToAdd)
						{
							if (cancel.IsCancellationRequested) return;

#if DEBUG
							Log.Debug("Inserting: {0}", img.Location);
#endif

							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@theme_id", theme.RowId);
							cmd.Parameters.AddWithValue("@location_id", _rowid);
							cmd.Parameters.AddWithValue("@type", img.Type.ToString());
							cmd.Parameters.AddWithValue("@path", img.Location);
							cmd.Parameters.AddWithValue("@pub_date", img.PubDate.HasValue ? (object)img.PubDate.Value : null);
							cmd.Parameters.AddWithValue("@rating", img.Rating);
							cmd.Parameters.AddWithValue("@thumb", img.Thumbnail?.Data);
							if (img.Size.HasValue) cmd.Parameters.AddWithValue("@size", img.Size.Value);
							else cmd.Parameters.AddWithValue("@size", null);

							cmd.ExecuteNonQuery();

							changesMade = true;
						}
					}
				}

				tran.Commit();
			}

			if (changesMade)
			{
				ContentUpdated?.Invoke(this, EventArgs.Empty);
			}
		}

		private IEnumerable<ImageRec> SearchDir(string dir)
		{
			try
			{
				var files = new List<ImageRec>();

				// Search for image files in this directory.
				string[] imageFiles = Directory.GetFiles(dir);
				foreach (string file in imageFiles)
				{
					if (Settings.IgnoreHiddenFiles == false || (File.GetAttributes(file) & FileAttributes.Hidden) == 0)
					{
						if (ImageFormatDesc.FileNameToImageFormat(file) != null) files.Add(ImageRec.FromFile(file));
					}
				}

				// Search sub-folders in this directory.
				string[] subDirs = Directory.GetDirectories(dir);
				foreach (string subDir in subDirs)
				{
					if (Settings.IgnoreHiddenFiles == false || (File.GetAttributes(subDir) & FileAttributes.Hidden) == 0)
					{
						files.AddRange(SearchDir(subDir));
					}
				}

				return files;
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when searching directory '{0}'.", dir);
				return new ImageRec[0];
			}
		}

		public void Save(Database db, long themeId)
		{
			if (_rowid == 0L)
			{
				_rowid = db.Insert("location", new object[]
					{
						"theme_id", themeId,
						"path", _path,
						"type", _type.ToString(),
						"update_freq", _updateFreq,
						"update_period", _updatePeriod.ToString(),
						"disabled", _disabled ? 1 : 0,
						"last_update", _lastUpdate != DateTime.MinValue ? (object)_lastUpdate : null
					});
			}
			else
			{
				db.Update("location", "rowid = @rowid", new object[]
					{
						"theme_id", themeId,
						"path", _path,
						"type", _type.ToString(),
						"update_freq", _updateFreq,
						"update_period", _updatePeriod.ToString(),
						"disabled", _disabled ? 1 : 0,
						"last_update", _lastUpdate != DateTime.MinValue ? (object)_lastUpdate : null
					},
					new object[] { "@rowid", _rowid });
			}
		}

		private void Load(System.Data.DataRow row)
		{
			_rowid = row.GetLong("rowid");

			_path = row.GetString("path");
			if (string.IsNullOrWhiteSpace(_path)) throw new SettingsException("No location specified.");

			_type = row.GetEnum<LocationType>("type", LocationType.Directory);
			_updateFreq = row.GetInt("update_freq", k_defaultUpdateInterval);
			_updatePeriod = row.GetEnum<Period>("update_period", k_defaultUpdatePeriod);
			_updateInterval = TimeSpanUtil.CalcInterval(_updateFreq, _updatePeriod);

			_disabled = row.GetBoolean("disabled", false);

			_lastUpdate = row.GetDateTime("last_update", DateTime.MinValue);
		}

		public void Load(XmlElement topElement)
		{
			var path = topElement.InnerText;
			if (string.IsNullOrWhiteSpace(path)) throw new SettingsException("No location specified.");

			LocationType type;
			if (!Enum.TryParse<LocationType>(topElement.GetAttribute("Type"), out type))
			{
				type = File.Exists(path) ? LocationType.File : LocationType.Directory;
			}

			_type = type;
			_path = path;

			var str = topElement.GetAttribute("UpdateFreq");
			if (!string.IsNullOrWhiteSpace(str))
			{
				int freq;
				if (Int32.TryParse(str, out freq)) _updateFreq = freq;
				else _updateFreq = k_defaultUpdateInterval;
			}

			str = topElement.GetAttribute("UpdatePeriod");
			if (!string.IsNullOrWhiteSpace(str))
			{
				Period period;
				if (Enum.TryParse<Period>(str, out period)) _updatePeriod = period;
				else _updatePeriod = k_defaultUpdatePeriod;
			}

			str = topElement.GetAttribute("Disabled");
			if (!string.IsNullOrWhiteSpace(str))
			{
				bool disabled;
				if (bool.TryParse(str, out disabled)) _disabled = disabled;
				else _disabled = false;
			}

			_updateInterval = TimeSpanUtil.CalcInterval(_updateFreq, _updatePeriod);
		}

		public long RowId
		{
			get { return _rowid; }
		}

		public Icon GetIcon()
		{
			if (_type == LocationType.Feed) return Res.RSS;

			var ir = new IconReader();
			return ir.GetFileIcon(_path);
		}

		public void UpdateIfRequired(Database db, Theme theme, CancellationToken cancel)
		{
			try
			{
				if (NextUpdate > DateTime.Now) return;

				switch (_type)
				{
					case LocationType.File:
						// No updating required for files since it can't change.
						return;

					case LocationType.Directory:
						Log.Write(LogLevel.Debug, "Scanning directory: {0}", _path);
						var files = SearchDir(_path);
						if (files.Any())
						{
							SyncDatabaseToImageRecList(files, theme, db, cancel);
						}
						break;

					case LocationType.Feed:
						{
							var loader = new FeedLoader();
							if (loader.LoadUrl(_path))
							{
								SyncDatabaseToImageRecList(loader.Images, theme, db, cancel);
							}
						}
						break;
				}

				_lastUpdate = DateTime.Now;
				FireUpdated();
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when updating location '{0}'.", _path);
				_lastUpdate = DateTime.Now;
			}
		}

		public LocationType Type
		{
			get { return _type; }
		}

		public int UpdateFrequency
		{
			get { return _updateFreq; }
			set { _updateFreq = value; }
		}

		public Period UpdatePeriod
		{
			get { return _updatePeriod; }
			set { _updatePeriod = value; }
		}

		public void SetUpdateInterval(int freq, Period period)
		{
			_updateFreq = freq;
			_updatePeriod = period;
			_updateInterval = TimeSpanUtil.CalcInterval(freq, period);
		}

		public event EventHandler<LocationEventArgs> Updated;

		public class LocationEventArgs : EventArgs
		{
			public Location Location { get; set; }

			public LocationEventArgs(Location location)
			{
				Location = location;
			}
		}

		private void FireUpdated()
		{
			EventHandler<LocationEventArgs> ev = Updated;
			if (ev != null) ev(this, new LocationEventArgs(this));
		}

		public DateTime NextUpdate
		{
			get { return _lastUpdate.Add(_updateInterval); }
		}

		public void SetNextUpdateNow()
		{
			if (_lastUpdate != DateTime.MinValue)
			{
				_lastUpdate = DateTime.MinValue;
				FireUpdated();
			}
		}

		public event EventHandler<LocationEventArgs> DisabledChanged;

		public bool Disabled
		{
			get { return _disabled; }
			set
			{
				if (_disabled != value)
				{
					_disabled = value;

					var ev = DisabledChanged;
					if (ev != null) ev(this, new LocationEventArgs(this));
				}
			}
		}

	}
}
