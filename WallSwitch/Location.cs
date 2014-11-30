using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;

namespace WallSwitch
{
	public enum LocationType
	{
		File,
		Directory,
		Feed
	}

	public class Location
	{
		private const int k_defaultUpdateInterval = 1;
		private const Period k_defaultUpdatePeriod = Period.Hours;

		private string _path;
		private List<ImageRec> _files = new List<ImageRec>();
		private LocationType _type;
		private DateTime _lastUpdate = DateTime.MinValue;
		private Period _updatePeriod = k_defaultUpdatePeriod;
		private int _updateFreq = k_defaultUpdateInterval;
		private TimeSpan _updateInterval;
		private bool _disabled = false;

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
					_files.Clear();
					_lastUpdate = DateTime.MinValue;
					FireUpdated();
				}
			}
		}

		public bool SamePath(string path)
		{
			return _path.Equals(path, StringComparison.OrdinalIgnoreCase);
		}

		public IEnumerable<ImageRec> Files
		{
			get
			{
				try
				{
					

					switch (_type)
					{
						case LocationType.File:
							return new ImageRec[] { ImageRec.FromFile(_path) };

						case LocationType.Directory:
							UpdateIfRequired();
							return _files;

						case LocationType.Feed:
							UpdateIfRequired();
							return _files;

						default:
							throw new InvalidOperationException("Invalid location type.");
					}
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Exception when getting files list.");
					return new ImageRec[0];
				}
			}
		}

		private void SearchDir(string dir)
		{
			try
			{
				// Search for image files in this directory.
				string[] imageFiles = Directory.GetFiles(dir);
				foreach (string file in imageFiles)
				{
					if (Settings.IgnoreHiddenFiles == false || (File.GetAttributes(file) & FileAttributes.Hidden) == 0)
					{
						if (ImageFormatDesc.FileNameToImageFormat(file) != null) _files.Add(ImageRec.FromFile(file));
					}
				}

				// Search sub-folders in this directory.
				string[] subDirs = Directory.GetDirectories(dir);
				foreach (string subDir in subDirs)
				{
					if (Settings.IgnoreHiddenFiles == false || (File.GetAttributes(subDir) & FileAttributes.Hidden) == 0)
					{
						SearchDir(subDir);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when searching directory '{0}'.", dir);
			}
		}

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString("Type", _type.ToString());
			xml.WriteAttributeString("UpdateFreq", _updateFreq.ToString());
			xml.WriteAttributeString("UpdatePeriod", _updatePeriod.ToString());
			if (_disabled) xml.WriteAttributeString("Disabled", _disabled.ToString());

			xml.WriteString(_path);
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

		public Icon GetIcon()
		{
			if (_type == LocationType.Feed) return Res.RSS;

			var ir = new IconReader();
			return ir.GetFileIcon(_path);
		}

		public void UpdateIfRequired()
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
						_files.Clear();
						SearchDir(_path);
						break;

					case LocationType.Feed:
						{
							var loader = new FeedLoader();
							if (loader.LoadUrl(_path))
							{
								_files.Clear();
								foreach (var image in loader.Images) _files.Add(image);
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
