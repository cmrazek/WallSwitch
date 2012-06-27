using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WallSwitch
{
	class Theme
	{
		#region Constants
		private const int k_maxHistory = 10;

		private const int k_defaultFreq = 5;
		private const ThemePeriod k_defaultPeriod = ThemePeriod.Minutes;
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
		private ThemePeriod _period = k_defaultPeriod;
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
		private List<string[]> _history = new List<string[]>();
		private int _historyIndex = -1;
		private Random _rand = new Random();
		private int _feather = 15;
		#endregion

		#region Construction
		/// <summary>
		/// Constructs the Theme object.
		/// </summary>
		/// <param name="id"></param>
		public Theme(Guid id)
		{
			_id = id;
			CalcInterval();
		}
		#endregion

		#region Attributes
		/// <summary>
		/// Gets the GUID for this theme.
		/// </summary>
		public Guid ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the name of this theme.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the mode (sequential, random, ...) for this theme.
		/// </summary>
		public ThemeMode Mode
		{
			get { return _mode; }
			set { _mode = value; }
		}

		/// <summary>
		/// Gets or sets the image size, in percent.
		/// This is only used for collage mode.
		/// </summary>
		public int ImageSize
		{
			get { return _imageSize; }
			set
			{
				if (value < 1 || value > 100) throw new ArgumentOutOfRangeException(Res.Exception_Theme_ImageSize);
				_imageSize = value;
			}
		}

		/// <summary>
		/// Gets or sets a flag indicating if this theme is the one currently switching.
		/// </summary>
		public bool IsActive
		{
			get { return _active; }
			set
			{
				if (_active != value)
				{
					_active = value;
					if (value) OnActivate();
					else OnDeactivate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the flag indicating if a different background image should be displayed on each monitor.
		/// </summary>
		public bool SeparateMonitors
		{
			get { return _separateMonitors; }
			set { _separateMonitors = value; }
		}

		/// <summary>
		/// Gets or sets the hotkey information.
		/// </summary>
		public HotKey HotKey
		{
			get { return _hotKey; }
			set
			{
				if (value == null) throw new ArgumentNullException("Hotkey is null.");
				_hotKey.Copy(value);
			}
		}

		/// <summary>
		/// Gets or sets the image fit style.
		/// </summary>
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
		public void Save(XmlTextWriter xml)
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
				xml.WriteString(loc.Path);
				xml.WriteEndElement();	// Location
			}

			SaveHistory(xml);
		}

		public void Load(XmlElement xmlTheme)
		{
			_name = Util.ParseString(xmlTheme, "Name", _id.ToString());

			_active = Util.ParseBool(xmlTheme, "Active", false);
			_freq = Util.ParseInt(xmlTheme, "Frequency", k_defaultFreq);
			_period = Util.ParseEnum<ThemePeriod>(xmlTheme, "Period", k_defaultPeriod);
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
				string location = xmlLoc.InnerText;
				if (!string.IsNullOrWhiteSpace(location)) _locations.Add(new Location(location));
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

		public string[] Locations
		{
			get
			{
				// Create a string array of the paths.
				string[] ret = new string[_locations.Count];
				int i = 0;
				foreach (Location loc in _locations) ret[i++] = loc.Path;

				return ret;
			}
			set
			{
				// Create a new list retaining the existing location objects, where possible.
				List<Location> newList = new List<Location>(value.Length);
				foreach (string path in value)
				{
					bool found = false;
					foreach (Location loc in _locations)
					{
						if (loc.SamePath(path))
						{
							newList.Add(loc);
							found = true;
							break;
						}
					}
					if (!found)
					{
						Location newLoc = new Location(path);
						newList.Add(newLoc);
						//if (IsActive) newLoc.StartWatching();
					}
				}

				// Find locations that have been deleted, and destroy them.
				foreach (Location loc in _locations)
				{
					if (!newList.Contains(loc)) loc.Destroy();
				}

				// Start using the new list.
				_locations = newList;
			}
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

		public ThemePeriod Period
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
			switch(_period)
			{
				case ThemePeriod.Seconds:
					_interval = TimeSpan.FromSeconds(_freq);
					break;
				case ThemePeriod.Minutes:
					_interval = TimeSpan.FromMinutes(_freq);
					break;
				case ThemePeriod.Hours:
					_interval = TimeSpan.FromHours(_freq);
					break;
				case ThemePeriod.Days:
					_interval = TimeSpan.FromDays(_freq);
					break;
				default:
					_interval = TimeSpan.FromMinutes(_freq);
					break;
			}
		}

		public TimeSpan Interval
		{
			get { return _interval; }
		}
		#endregion

		#region Background Color
		/// <summary>
		/// Gets or sets the top background color.
		/// </summary>
		public Color BackColorTop
		{
			get { return _backColorTop; }
			set { _backColorTop = value; }
		}

		/// <summary>
		/// Gets or sets the bottom background color.
		/// </summary>
		public Color BackColorBottom
		{
			get { return _backColorBottom; }
			set { _backColorBottom = value; }
		}

		/// <summary>
		/// Gets or sets the opacity of the background (collage mode only).
		/// </summary>
		public int BackOpacity
		{
			get { return _backOpacity; }
			set
			{
				if (value < 0 || value > 100) throw new ArgumentOutOfRangeException(Res.Exception_Theme_BackOpacity);
				_backOpacity = value;
			}
		}

		/// <summary>
		/// Gets the opacity for a 0 -> 255 scale.
		/// </summary>
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
		public void AddHistory(string[] images)
		{
			_history.Add(images);
		}

		public bool CanGoPrev
		{
			get { return _historyIndex > 0; }
		}

		private void SaveHistory(XmlTextWriter xml)
		{
			int index = 0;
			foreach (string[] images in _history)
			{
				xml.WriteStartElement("History");
				if (index++ == _historyIndex) xml.WriteAttributeString("Current", Boolean.TrueString);
				foreach (string image in images) xml.WriteElementString("Image", image);
				xml.WriteEndElement();	// History
			}
		}

		private void LoadHistory(XmlElement xmlTheme)
		{
			foreach (XmlElement xmlHistory in xmlTheme.SelectNodes("History"))
			{
				bool current = false;
				if (xmlHistory.HasAttribute("Current")) Boolean.TryParse(xmlHistory.GetAttribute("Current"), out current);

				List<String> images = new List<string>();
				foreach (XmlElement xmlImage in xmlHistory.SelectNodes("Image"))
				{
					images.Add(xmlImage.InnerText.ToLower());
				}

				if (images.Count > 0)
				{
					if (current) _historyIndex = _history.Count;
					_history.Add(images.ToArray());
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
		#endregion

		#region ImageSelection
		/// <summary>
		/// This function selects the next images to be displayed as wallpaper.
		/// If the user previously hit the back button, then this function will return
		/// the next images in the cached list.
		/// </summary>
		/// <param name="numMonitors">The number of images to be selected (one image per monitor).</param>
		/// <returns>If no images could be located, then null; otherwise, an array of selected images.
		/// Note: The number of images returned may be less than the number of monitors, if not enough images were found.</returns>
		public string[] GetNextImages(int numMonitors)
		{
			string[] ret = null;

			Log.Write(LogLevel.Debug, "Finding next images...");

			// If we're not at the end of the history (previously hit the back button),
			// then go forward to the next images, and increment the counter.
			if (_historyIndex < _history.Count - 1)
			{
				Log.Write(LogLevel.Debug, "Previously selected images found.");
				return _history[++_historyIndex];
			}

			// Go through each location and find files.
			List<string> files = new List<string>();
			foreach (Location loc in _locations)
			{
				try
				{
					foreach (string locFile in loc.Files) files.Add(locFile);
				}
				catch (Exception)
				{ }
			}

			Log.Write(LogLevel.Debug, files.Count.ToString() + " files found.");

			if (files.Count > 0)
			{
				if (!_separateMonitors) numMonitors = 1;

				if (_mode == ThemeMode.Random || _mode == ThemeMode.Collage)
				{
					// Pick N number of files to display.
					List<String> pickedFiles = new List<string>(numMonitors);
					for (int i = 0; i < numMonitors; i++)
					{
						int index = _rand.Next(files.Count);
						string fileName = files[index];
						Log.Write(LogLevel.Debug, "Choosing file # " + index.ToString() + ": " + fileName);
						pickedFiles.Add(fileName);
					}

					ret = pickedFiles.ToArray();
				}
				else // Sequential
				{
					files.Sort();

					int fileIndex = 0;
					if (_history.Count > 0)
					{
						string[] currentImages = _history[_history.Count - 1];

						// Find the where the current images are in the list.
						int index = -1;
						foreach (string file in files)
						{
							foreach (string cur in currentImages)
							{
								if (file == cur) fileIndex = index + 1;
							}
							index++;
						}
					}

					// Pick N number of files to display.
					List<String> pickedFiles = new List<string>(numMonitors);
					for (int i = 0; i < numMonitors; i++)
					{
						if (fileIndex >= files.Count) fileIndex = 0;
						string fileName = files[fileIndex++];
						Log.Write(LogLevel.Debug, "Choosing file # " + fileIndex.ToString() + ": " + fileName);
						pickedFiles.Add(fileName);
					}

					ret = pickedFiles.ToArray();
				}
			}

			if (ret != null)
			{
				// Add history
				_history.Add(ret);
				_historyIndex = _history.Count - 1;

				// Trim off the beginning of the history if we've exceeded the max length.
				while (_history.Count > k_maxHistory)
				{
					_history.RemoveAt(0);
					_historyIndex--;
				}
			}

			return ret;
		}

		public static bool IsImageFile(string fileName)
		{
			string ext = Path.GetExtension(fileName).ToLower();
			switch (ext)
			{
				case ".jpg":
				case ".jpeg":
				case ".gif":
				case ".png":
				case ".bmp":
				case ".tiff":
					return true;
			}

			return false;
		}

		/// <summary>
		/// This function will return the previously displayed images.
		/// </summary>
		/// <returns>If there are previous images cached, an array of the image filenames; otherwise null.</returns>
		public string[] GetPrevImages()
		{
			Log.Write(LogLevel.Debug, "Going back to previous images.");
			if (_historyIndex > 0) return _history[--_historyIndex];
			return null;
		}
		#endregion

		#region Wallpaper
		public string WallpaperFileName
		{
			get { return String.Format("{0}\\{1}.bmp", Util.AppDataDir, _id); }
		}
		#endregion

		#region File System Watching
		/// <summary>
		/// Called when the theme is activating.
		/// </summary>
		private void OnActivate()
		{
			// Start watching directories.
			//foreach (Location loc in _locations) loc.StartWatching();
		}

		/// <summary>
		/// Called when the theme is no longer active.
		/// </summary>
		private void OnDeactivate()
		{
			// Stop watching directories.
			//foreach (Location loc in _locations) loc.StopWatching();
		}

		/// <summary>
		/// Called when the application loads, and this theme is the active theme.
		/// </summary>
		public void OnAppLoadActivate()
		{
			OnActivate();
		}
		#endregion
	}
}
