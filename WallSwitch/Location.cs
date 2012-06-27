using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;

namespace WallSwitch
{
	enum LocationType
	{
		File,
		Directory,
		Feed
	}

	class Location
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

		public Location(LocationType type, string path)
		{
			_type = type;
			_path = path;
			_updateInterval = TimeSpanEx.CalcInterval(_updateFreq, _updatePeriod);
		}

		public Location(XmlElement topElement)
		{
			Load(topElement);
		}

		public override string ToString()
		{
			return _path;
		}

		public string Path
		{
			get { return _path; }
			set { _path = value; }
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
							_files.Clear();
							SearchDir(_path);
							return _files;

						case LocationType.Feed:
							UpdateFeedIfRequired();
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
					if (ImageRec.FileNameToImageFormat(file) != null) _files.Add(ImageRec.FromFile(file));
				}

				// Search sub-folders in this directory.
				string[] subDirs = Directory.GetDirectories(dir);
				foreach (string subDir in subDirs) SearchDir(subDir);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when searching directory '{0}'.", dir);
			}
		}

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString("Type", _type.ToString());

			if (_type == LocationType.Feed)
			{
				xml.WriteAttributeString("UpdateFreq", _updateFreq.ToString());
				xml.WriteAttributeString("UpdatePeriod", _updatePeriod.ToString());
			}

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

			if (_type == LocationType.Feed)
			{
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

				_updateInterval = TimeSpanEx.CalcInterval(_updateFreq, _updatePeriod);
			}
		}

		public Icon GetIcon()
		{
			if (_type == LocationType.Feed) return Res.RSS;

			var ir = new IconReader();
			return ir.GetFileIcon(_path);
		}

		public void UpdateFeedIfRequired()
		{
			try
			{
				if (_lastUpdate.Add(_updateInterval) > DateTime.Now) return;
				
				var loader = new FeedLoader();
				if (loader.LoadUrl(_path))
				{
					_files.Clear();
					foreach (var image in loader.Images) _files.Add(image);
				}

				_lastUpdate = DateTime.Now;
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when updating feed '{0}'.", _path);
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
			_updateInterval = TimeSpanEx.CalcInterval(freq, period);
		}

	}
}
