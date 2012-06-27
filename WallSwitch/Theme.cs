using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;

namespace WallSwitch
{
	class Theme
	{
		#region Constants
		private const int k_maxHistory = 10;
		private const int k_maxRetrievalRetries = 10;

		private const int k_defaultFreq = 5;
		private const Period k_defaultPeriod = Period.Minutes;
		private const ThemeMode k_defaultMode = ThemeMode.Random;
		private static Color k_defaultBackColor = Color.Black;
		private const bool k_defaultSeparateMonitors = true;
		private const int k_defaultImageSize = 50;
		private const int k_defaultBackOpacity = 15;
		private const ImageFit k_defaultImageFit = ImageFit.Fit;
		private const int k_defaultFeather = 15;
		#endregion

		#region Variables
		private Guid _id;
		private List<Location> _locations = new List<Location>();
		private string _name = "";
		private int _freq = k_defaultFreq;
		private Period _period = k_defaultPeriod;
		private TimeSpan _interval;
		private ThemeMode _mode = k_defaultMode;
		private int _imageSize = k_defaultImageSize;	// Used only for collage mode
		private bool _active = false;
		private bool _separateMonitors = k_defaultSeparateMonitors;
		private HotKey _hotKey = new HotKey();
		private Color _backColorTop = k_defaultBackColor;
		private Color _backColorBottom = k_defaultBackColor;
		private int _backOpacity = k_defaultBackOpacity;
		private ImageFit _imageFit = k_defaultImageFit;
		private List<IEnumerable<ImageRec>> _history = new List<IEnumerable<ImageRec>>();
		private int _historyIndex = -1;
		private Random _rand = new Random();
		private int _feather = 15;
		#endregion

		#region Construction
		public Theme(Guid id)
		{
			_id = id;
			CalcInterval();
		}
		#endregion

		#region Attributes
		public Guid ID
		{
			get { return _id; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public ThemeMode Mode
		{
			get { return _mode; }
			set { _mode = value; }
		}

		public int ImageSize
		{
			get { return _imageSize; }
			set
			{
				if (value < 1 || value > 100) throw new ArgumentOutOfRangeException(Res.Exception_Theme_ImageSize);
				_imageSize = value;
			}
		}

		public bool IsActive
		{
			get { return _active; }
			set { _active = value; }
		}

		public bool SeparateMonitors
		{
			get { return _separateMonitors; }
			set { _separateMonitors = value; }
		}

		public HotKey HotKey
		{
			get { return _hotKey; }
			set
			{
				if (value == null) throw new ArgumentNullException("Hotkey is null.");
				_hotKey.Copy(value);
			}
		}

		public ImageFit ImageFit
		{
			get { return _imageFit; }
			set { _imageFit = value; }
		}

		public int Feather
		{
			get { return _feather; }
			set
			{
				if (value < 0) throw new ArgumentOutOfRangeException("Feather cannot be less than zero.");
				_feather = value;
			}
		}
		#endregion

		#region Save/Load
		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString("Name", _name);
			if (_active) xml.WriteAttributeString("Active", Boolean.TrueString);
			xml.WriteAttributeString("Frequency", _freq.ToString());
			xml.WriteAttributeString("Period", _period.ToString());
			xml.WriteAttributeString("Mode", _mode.ToString());
			xml.WriteAttributeString("BackColorTop", Util.ColorToString(_backColorTop));
			xml.WriteAttributeString("BackColorBottom", Util.ColorToString(_backColorBottom));
			if (!_separateMonitors) xml.WriteAttributeString("SeparateMonitors", _separateMonitors.ToString());

			if (_mode == ThemeMode.Collage)
			{
				xml.WriteAttributeString("BackOpacity", _backOpacity.ToString());
				if (_feather != k_defaultFeather) xml.WriteAttributeString("FeatherEdges", _feather.ToString());
			}
			else
			{
				xml.WriteAttributeString("ImageFit", _imageFit.ToString());
			}

			_hotKey.SaveTheme(xml);

			foreach (Location loc in _locations)
			{
				xml.WriteStartElement("Location");
				loc.Save(xml);
				xml.WriteEndElement();	// Location
			}

			SaveHistory(xml);
		}

		public void Load(XmlElement xmlTheme)
		{
			_name = Util.ParseString(xmlTheme, "Name", _id.ToString());

			_active = Util.ParseBool(xmlTheme, "Active", false);
			_freq = Util.ParseInt(xmlTheme, "Frequency", k_defaultFreq);
			_period = Util.ParseEnum<Period>(xmlTheme, "Period", k_defaultPeriod);
			CalcInterval();
			_mode = Util.ParseEnum<ThemeMode>(xmlTheme, "Mode", k_defaultMode);
			_backColorTop = Util.ParseColor(xmlTheme, "BackColorTop", k_defaultBackColor);
			_backColorBottom = Util.ParseColor(xmlTheme, "BackColorBottom", k_defaultBackColor);
			_separateMonitors = Util.ParseBool(xmlTheme, "SeparateMonitors", k_defaultSeparateMonitors);
			_backOpacity = Util.ParseInt(xmlTheme, "BackOpacity", k_defaultBackOpacity);
			_imageFit = Util.ParseEnum<ImageFit>(xmlTheme, "ImageFit", k_defaultImageFit);
			_feather = Util.ParseInt(xmlTheme, "FeatherEdges", k_defaultFeather);

			_hotKey.LoadTheme(xmlTheme);

			foreach (XmlElement xmlLoc in xmlTheme.SelectNodes("Location"))
			{
				try
				{
					var loc = new Location(xmlLoc);
					loc.Load(xmlLoc);
					_locations.Add(loc);
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when loading location from settings file.");
				}
			}

			LoadHistory(xmlTheme);
		}

		#endregion

		#region Items
		public bool ContainsLocation(string location)
		{
			foreach (Location loc in _locations)
			{
				if (loc.SamePath(location)) return true;
			}
			return false;
		}

		public IEnumerable<Location> Locations
		{
			get { return _locations; }
			set { _locations = value.ToList(); }
		}
		#endregion

		#region Interval
		public int Frequency
		{
			get { return _freq; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException(Res.Exception_ThemeFreqOutOfRange);
				_freq = value;
				CalcInterval();
			}
		}

		public Period Period
		{
			get { return _period; }
			set
			{
				_period = value;
				CalcInterval();
			}
		}

		private void CalcInterval()
		{
			_interval = TimeSpanEx.CalcInterval(_freq, _period);
		}

		public TimeSpan Interval
		{
			get { return _interval; }
		}
		#endregion

		#region Background Color
		public Color BackColorTop
		{
			get { return _backColorTop; }
			set { _backColorTop = value; }
		}

		public Color BackColorBottom
		{
			get { return _backColorBottom; }
			set { _backColorBottom = value; }
		}

		public int BackOpacity
		{
			get { return _backOpacity; }
			set
			{
				if (value < 0 || value > 100) throw new ArgumentOutOfRangeException(Res.Exception_Theme_BackOpacity);
				_backOpacity = value;
			}
		}

		public int BackOpacity255
		{
			get
			{
				if (_backOpacity <= 0) return 0;
				if (_backOpacity >= 100) return 255;
				return (int)((float)_backOpacity * 255.0f / 100.0f);
			}
		}
		#endregion

		#region History
		public bool CanGoPrev
		{
			get { return _historyIndex > 0; }
		}

		private void SaveHistory(XmlWriter xml)
		{
			int index = 0;
			foreach (var images in _history)
			{
				xml.WriteStartElement("History");
				if (index++ == _historyIndex) xml.WriteAttributeString("Current", Boolean.TrueString);
				foreach (var image in images)
				{
					xml.WriteStartElement("Image");
					image.Save(xml);
					xml.WriteEndElement();	// Image
				}
				xml.WriteEndElement();	// History
			}
		}

		private void LoadHistory(XmlElement xmlTheme)
		{
			foreach (XmlElement xmlHistory in xmlTheme.SelectNodes("History"))
			{
				bool current = false;
				if (xmlHistory.HasAttribute("Current")) Boolean.TryParse(xmlHistory.GetAttribute("Current"), out current);

				var images = new List<ImageRec>();
				foreach (XmlElement xmlImage in xmlHistory.SelectNodes("Image"))
				{
					var image = ImageRec.FromXml(xmlImage);
					if (image != null) images.Add(image);
				}

				if (images.Count > 0)
				{
					if (current) _historyIndex = _history.Count;
					_history.Add(images);
				}
			}
		}

		public void ClearHistory()
		{
			_history.Clear();
			_historyIndex = -1;

			string fileName = WallpaperFileName;
			if (File.Exists(fileName)) File.Delete(fileName);
		}

		public IEnumerable<IEnumerable<ImageRec>> History
		{
			get { return _history; }
		}
		#endregion

		#region ImageSelection
		public IEnumerable<ImageRec> GetNextImages(int numMonitors)
		{
			IEnumerable<ImageRec> ret = null;

			Log.Write(LogLevel.Debug, "Finding next images...");

			// If we're not at the end of the history (previously hit the back button),
			// then go forward to the next images, and increment the counter.
			if (_historyIndex < _history.Count - 1)
			{
				Log.Write(LogLevel.Debug, "Previously selected images found.");
				var imageSet = _history[++_historyIndex];
				var allFound = true;

				foreach (var image in imageSet)
				{
					if (!image.Retrieve())
					{
						allFound = false;
						break;
					}
				}
				if (allFound) return imageSet;
			}

			// Go through each location and find files.
			var files = new List<ImageRec>();
			foreach (Location loc in _locations) files.AddRange(loc.Files);

			Log.Write(LogLevel.Debug, files.Count.ToString() + " files found.");

			if (files.Count > 0)
			{
				if (!_separateMonitors) numMonitors = 1;

				if (_mode == ThemeMode.Random || _mode == ThemeMode.Collage)
				{
					// Pick N number of files to display.
					var pickedFiles = new List<ImageRec>(numMonitors);
					var retries = 0;
					while (pickedFiles.Count < numMonitors && retries < k_maxRetrievalRetries)
					{
						var index = _rand.Next(files.Count);
						var loc = files[index];
						if (loc.Retrieve())
						{
							Log.Write(LogLevel.Debug, "Choosing file # {0}: {1}", index, loc);
							pickedFiles.Add(loc);
							retries = 0;
						}
						else
						{
							retries++;
						}
					}

					ret = pickedFiles;
				}
				else // Sequential
				{
					files.Sort();

					int fileIndex = 0;
					if (_history.Count > 0)
					{
						var currentImages = _history[_history.Count - 1];

						// Find the where the current images are in the list.
						int index = -1;
						foreach (var file in files)
						{
							foreach (var cur in currentImages)
							{
								if (file.Equals(cur)) fileIndex = index + 1;
							}
							index++;
						}
					}

					// Pick N number of files to display.
					var pickedFiles = new List<ImageRec>(numMonitors);
					var retries = 0;
					while (pickedFiles.Count < numMonitors && fileIndex < files.Count && retries < k_maxRetrievalRetries)
					{
						var loc = files[fileIndex++];
						if (loc.Retrieve())
						{
							Log.Write(LogLevel.Debug, "Choosing file # {0}: {1}", fileIndex, loc);
							pickedFiles.Add(loc);
							retries = 0;
						}
						else
						{
							retries++;
						}
					}

					ret = pickedFiles;
				}
			}

			if (ret != null)
			{
				// Add history
				_history.Add(ret);
				_historyIndex = _history.Count - 1;

				FireHistoryAdded(ret);

				// Trim off the beginning of the history if we've exceeded the max length.
				while (_history.Count > k_maxHistory)
				{
					_history.RemoveAt(0);
					_historyIndex--;
				}
			}

			return ret;
		}

		public IEnumerable<ImageRec> GetPrevImages()
		{
			Log.Write(LogLevel.Debug, "Going back to previous images.");
			if (_historyIndex > 0)
			{
				var imageSet = _history[--_historyIndex];
				foreach (var image in imageSet) image.Retrieve();
				return imageSet;
			}
			return null;
		}
		#endregion

		#region Wallpaper
		public string WallpaperFileName
		{
			get { return String.Format("{0}\\{1}.bmp", Util.AppDataDir, _id); }
		}
		#endregion

		#region Events
		public event EventHandler<HistoryAddedEventArgs> HistoryAdded;
		public class HistoryAddedEventArgs : EventArgs
		{
			public IEnumerable<ImageRec> Images { get; set; }
		}

		private void FireHistoryAdded(IEnumerable<ImageRec> images)
		{
			EventHandler<HistoryAddedEventArgs> ev = HistoryAdded;
			if (ev != null)
			{
				ev(this, new HistoryAddedEventArgs
				{
					Images = images
				});
			}
		}
		#endregion
	}
}
