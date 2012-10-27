using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace WallSwitch
{
	public class Theme
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
		private const bool k_defaultFadeTransition = true;
		public const int k_defaultMaxImageScale = 200;
		public const int k_defaultColorEffectCollageFadeRatio = 25;
		#endregion

		#region Variables
		private Guid _id;
		private List<Location> _locations = new List<Location>();
		private string _name = string.Empty;
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
		private bool _fadeTransition = k_defaultFadeTransition;
		private string _lastWallpaperFile = string.Empty;
		private int _maxImageScale = k_defaultMaxImageScale;
		private ColorEffect _colorEffect = ColorEffect.None;
		private bool _colorEffectCollageFade = false;
		private int _colorEffectCollageFadeRatio = k_defaultColorEffectCollageFadeRatio;
		#endregion

		#region Construction
		public Theme(Guid id)
		{
			_id = id;
			CalcInterval();

			_hotKey.HotKeyPressed += new EventHandler(_hotKey_HotKeyPressed);
		}

		void _hotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				if (MainWindow.Window != null) MainWindow.Window.OnActivateTheme(this);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when activating theme in response to hotkey.");
			}
		}
		#endregion

		#region Properties
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
			set
			{
				if (_active != value)
				{
					_active = value;
					OnActivate(_active);
				}
			}
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

		public bool FadeTransition
		{
			get { return _fadeTransition; }
			set { _fadeTransition = value; }
		}

		public string LastWallpaperFile
		{
			get { return _lastWallpaperFile; }
			set { _lastWallpaperFile = value; }
		}

		public int MaxImageScale
		{
			get { return _maxImageScale; }
			set { _maxImageScale = value; }
		}
		#endregion

		#region Save/Load
		private const string k_name = "Name";
		private const string k_active = "Active";
		private const string k_freq = "Frequency";
		private const string k_period = "Period";
		private const string k_mode = "Mode";
		private const string k_imageSize = "ImageSize";
		private const string k_backColorTop = "BackColorTop";
		private const string k_backColorBottom = "BackColorBottom";
		private const string k_separateMonitors = "SeparateMonitors";
		private const string k_backOpacity = "BackOpacity";
		private const string k_featherEdges = "FeatherEdges";
		private const string k_imageFit = "ImageFit";
		private const string k_fadeTransition = "FadeTransition";
		private const string k_lastWallpaperFile = "LastWallpaperFile";
		private const string k_maxImageScale = "MaxImageScale";
		private const string k_colorEffectXml = "ColorEffect";
		private const string k_colorEffectCollageFadeXml = "ColorEffectCollageFade";
		private const string k_colorEffectCollageFadeRatio = "ColorEffectCollageFadeRatio";
		private const string k_location = "Location";

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString(k_name, _name);
			if (_active) xml.WriteAttributeString(k_active, Boolean.TrueString);
			xml.WriteAttributeString(k_freq, _freq.ToString());
			xml.WriteAttributeString(k_period, _period.ToString());
			xml.WriteAttributeString(k_mode, _mode.ToString());
			xml.WriteAttributeString(k_imageSize, _imageSize.ToString());
			xml.WriteAttributeString(k_backColorTop, ColorUtil.ColorToString(_backColorTop));
			xml.WriteAttributeString(k_backColorBottom, ColorUtil.ColorToString(_backColorBottom));
			if (!_separateMonitors) xml.WriteAttributeString(k_separateMonitors, _separateMonitors.ToString());

			if (_mode == ThemeMode.Collage)
			{
				xml.WriteAttributeString(k_backOpacity, _backOpacity.ToString());
				if (_feather != k_defaultFeather) xml.WriteAttributeString(k_featherEdges, _feather.ToString());
			}
			else
			{
				xml.WriteAttributeString(k_imageFit, _imageFit.ToString());
			}

			if (_fadeTransition != k_defaultFadeTransition) xml.WriteAttributeString(k_fadeTransition, _fadeTransition.ToString());
			if (!string.IsNullOrEmpty(_lastWallpaperFile)) xml.WriteAttributeString(k_lastWallpaperFile, _lastWallpaperFile);
			if (_maxImageScale != k_defaultMaxImageScale) xml.WriteAttributeString(k_maxImageScale, _maxImageScale.ToString());

			if (_colorEffect != ColorEffect.None) xml.WriteAttributeString(k_colorEffectXml, _colorEffect.ToString());
			if (_colorEffectCollageFade != false) xml.WriteAttributeString(k_colorEffectCollageFadeXml, _colorEffectCollageFade.ToString());
			if (_colorEffectCollageFade != false) xml.WriteAttributeString(k_colorEffectCollageFadeRatio, _colorEffectCollageFadeRatio.ToString());

			_hotKey.SaveXml(xml);

			foreach (Location loc in _locations)
			{
				xml.WriteStartElement(k_location);
				loc.Save(xml);
				xml.WriteEndElement();	// Location
			}

			SaveHistory(xml);
		}

		public void Load(XmlElement xmlTheme)
		{
			_name = Util.ParseString(xmlTheme, k_name, _id.ToString());

			_active = Util.ParseBool(xmlTheme, k_active, false);
			_freq = Util.ParseInt(xmlTheme, k_freq, k_defaultFreq);
			_period = Util.ParseEnum<Period>(xmlTheme, k_period, k_defaultPeriod);
			CalcInterval();
			_mode = Util.ParseEnum<ThemeMode>(xmlTheme, k_mode, k_defaultMode);
			_imageSize = Util.ParseInt(xmlTheme, k_imageSize, k_defaultImageSize);
			_backColorTop = ColorUtil.ParseColor(xmlTheme, k_backColorTop, k_defaultBackColor);
			_backColorBottom = ColorUtil.ParseColor(xmlTheme, k_backColorBottom, k_defaultBackColor);
			_separateMonitors = Util.ParseBool(xmlTheme, k_separateMonitors, k_defaultSeparateMonitors);
			_backOpacity = Util.ParseInt(xmlTheme, k_backOpacity, k_defaultBackOpacity);
			_imageFit = Util.ParseEnum<ImageFit>(xmlTheme, k_imageFit, k_defaultImageFit);
			_feather = Util.ParseInt(xmlTheme, k_featherEdges, k_defaultFeather);
			_fadeTransition = Util.ParseBool(xmlTheme, k_fadeTransition, k_defaultFadeTransition);
			_lastWallpaperFile = Util.ParseString(xmlTheme, k_lastWallpaperFile, string.Empty);
			_maxImageScale = Util.ParseInt(xmlTheme, k_maxImageScale, k_defaultMaxImageScale);
			_colorEffect = Util.ParseEnum<ColorEffect>(xmlTheme, k_colorEffectXml, ColorEffect.None);
			_colorEffectCollageFade = Util.ParseBool(xmlTheme, k_colorEffectCollageFadeXml, false);
			_colorEffectCollageFadeRatio = Util.ParseInt(xmlTheme, k_colorEffectCollageFadeRatio, k_defaultColorEffectCollageFadeRatio);

			foreach (XmlElement xmlHotKey in xmlTheme.SelectNodes(HotKey.XmlElementName))
			{
				_hotKey.LoadXml(xmlHotKey);
			}

			foreach (XmlElement xmlLoc in xmlTheme.SelectNodes(k_location))
			{
				try
				{
					var loc = new Location(xmlLoc);
					loc.Load(xmlLoc);
					_locations.Add(loc);
					AttachLocations(new Location[] { loc });
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when loading location from settings file.");
				}
			}

			LoadHistory(xmlTheme);
		}

		#endregion

		#region Locations
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
			set
			{
				var removedLocations = (from l in _locations where !value.Contains(l) select l).ToArray();
				var addedLocations = (from v in value where !_locations.Contains(v) select v).ToArray();

				_locations = value.ToList();

				DetachLocations(removedLocations);
				AttachLocations(addedLocations);
			}
		}

		private void AttachLocations(IEnumerable<Location> locations)
		{
			foreach (var loc in locations)
			{
				loc.Updated += loc_Updated;
			}
		}

		private void DetachLocations(IEnumerable<Location> locations)
		{
			foreach (var loc in locations)
			{
				loc.Updated -= loc_Updated;
			}
		}

		void loc_Updated(object sender, Location.LocationEventArgs e)
		{
			FireLocationUpdated(e.Location);
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
			_interval = TimeSpanUtil.CalcInterval(_freq, _period);
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

			var fileName = GetWallpaperFileName(ImageFormat.Bmp);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetWallpaperFileName(ImageFormat.Jpeg);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetWallpaperFileName(ImageFormat.Png);
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
		private void OnActivate(bool active)
		{
		}

		public string GetWallpaperFileName(ImageFormat format)
		{
			return String.Format("{0}\\{1}{2}", Util.AppDataDir, _id, ImageFormatDesc.ImageFormatToExtension(format));
		}
		#endregion

		#region Events
		public class ThemeEventArgs : EventArgs
		{
			public Theme Theme { get; set; }

			public ThemeEventArgs(Theme theme)
			{
				Theme = theme;
			}
		}

		public event EventHandler<HistoryAddedEventArgs> HistoryAdded;
		public class HistoryAddedEventArgs : ThemeEventArgs
		{
			public IEnumerable<ImageRec> Images { get; set; }

			public HistoryAddedEventArgs(Theme theme, IEnumerable<ImageRec> images)
				: base(theme)
			{
				Images = images;
			}
		}

		private void FireHistoryAdded(IEnumerable<ImageRec> images)
		{
			EventHandler<HistoryAddedEventArgs> ev = HistoryAdded;
			if (ev != null) ev(this, new HistoryAddedEventArgs(this, images));
		}

		public event EventHandler<LocationUpdatedEventArgs> LocationUpdated;

		public class LocationUpdatedEventArgs : ThemeEventArgs
		{
			public Location Location { get; set; }

			public LocationUpdatedEventArgs(Theme theme, Location location)
				: base(theme)
			{
				Location = location;
			}
		}

		private void FireLocationUpdated(Location location)
		{
			EventHandler<LocationUpdatedEventArgs> ev = LocationUpdated;
			if (ev != null) ev(this, new LocationUpdatedEventArgs(this, location));
		}
		#endregion

		#region Color Effects
		public ColorEffect ColorEffect
		{
			get { return _colorEffect; }
			set { _colorEffect = value; }
		}

		public bool ColorEffectCollageFade
		{
			get { return _colorEffectCollageFade; }
			set { _colorEffectCollageFade = value; }
		}

		public int ColorEffectCollageFadeRatio
		{
			get { return _colorEffectCollageFadeRatio; }
			set
			{
				if (value < 0) _colorEffectCollageFadeRatio = 0;
				else if (value > 100) _colorEffectCollageFadeRatio = 100;
				else _colorEffectCollageFadeRatio = value;
			}
		}
		#endregion
	}
}
