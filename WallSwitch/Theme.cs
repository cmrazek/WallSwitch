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
		private const int k_maxImageRectHistory = 3;

		private const int k_defaultFreq = 5;
		private const Period k_defaultPeriod = Period.Minutes;
		private const ThemeMode k_defaultMode = ThemeMode.Random;
		private static readonly Color k_defaultBackColor = Color.Black;
		private const bool k_defaultSeparateMonitors = true;
		private const int k_defaultImageSize = 50;
		private const int k_defaultBackOpacity = 15;
		private const ImageFit k_defaultImageFit = ImageFit.Fit;
		private const bool k_defaultFeatherEnable = true;	// Deprecated
		private const EdgeMode k_defaultEdgeMode = EdgeMode.Feather;
		private const int k_defaultEdgeDist = 15;
		private static readonly Color k_defaultBorderColor = Color.White;
		private const bool k_defaultFadeTransition = true;
		public const int k_defaultMaxImageScale = 200;
		public const int k_defaultColorEffectBackRatio = 25;
		private const bool k_defaultDropShadow = false;
		private const int k_defaultDropShadowDist = 15;
		private const bool k_defaultDropShadowFeather = true;
		private const int k_defaultDropShadowFeatherDist = 20;
		private const int k_defaultDropShadowOpacity = 50;
		private const bool k_defaultBackgroundBlur = false;
		private const int k_defaultBackgroundBlurDist = 4;
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
		private List<RectangleF> _imageRectHistory = new List<RectangleF>();
		private int _historyIndex = -1;
		private Random _rand = null;
		private bool _fadeTransition = k_defaultFadeTransition;
		private string _lastWallpaperFile = string.Empty;
		private int _maxImageScale = k_defaultMaxImageScale;

		private ColorEffect _colorEffectFore = ColorEffect.None;
		private ColorEffect _colorEffectBack = ColorEffect.None;
		private int _colorEffectBackRatio = k_defaultColorEffectBackRatio;

		private EdgeMode _edgeMode = k_defaultEdgeMode;
		private int _edgeDist = k_defaultEdgeDist;
		private Color _borderColor = k_defaultBorderColor;

		private bool _dropShadow = k_defaultDropShadow;
		private int _dropShadowDist = k_defaultDropShadowDist;
		private bool _dropShadowFeather = k_defaultDropShadowFeather;
		private int _dropShadowFeatherDist = k_defaultDropShadowFeatherDist;
		private int _dropShadowOpacity = k_defaultDropShadowOpacity;

		private bool _backgroundBlur = k_defaultBackgroundBlur;
		private int _backgroundBlurDist = k_defaultBackgroundBlurDist;
		#endregion

		#region Construction
		public Theme(Guid id)
		{
			_id = id;
			_rand = RandomUtil.FromGuidAndTime(_id);
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

		public Theme Clone()
		{
			// Don't clone active, hotkey, history, historyIndex, rand, lastWallpaperFile

			return new Theme(Guid.NewGuid())
			{
				_locations = (from l in _locations select l.Clone()).ToList(),
				_name = _name,
				_freq = _freq,
				_period = _period,
				_interval = _interval,
				_mode = _mode,
				_imageSize = _imageSize,
				_separateMonitors = _separateMonitors,
				_backColorTop = _backColorTop,
				_backColorBottom = _backColorBottom,
				_backOpacity = _backOpacity,
				_imageFit = _imageFit,
				_edgeDist = _edgeDist,
				_fadeTransition = _fadeTransition,
				_maxImageScale = _maxImageScale,
				_colorEffectFore = _colorEffectFore,
				_colorEffectBack = _colorEffectBack,
				_colorEffectBackRatio = _colorEffectBackRatio,
				_dropShadow = _dropShadow,
				_dropShadowDist = _dropShadowDist,
				_dropShadowFeather = _dropShadowFeather,
				_dropShadowFeatherDist = _dropShadowFeatherDist,
				_dropShadowOpacity = _dropShadowOpacity,
				_backgroundBlur = _backgroundBlur,
				_backgroundBlurDist = _backgroundBlurDist
			};
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
		private const string k_nameXml = "Name";
		private const string k_activeXml = "Active";
		private const string k_freqXml = "Frequency";
		private const string k_periodXml = "Period";
		private const string k_modeXml = "Mode";
		private const string k_imageSizeXml = "ImageSize";
		private const string k_backColorTopXml = "BackColorTop";
		private const string k_backColorBottomXml = "BackColorBottom";
		private const string k_separateMonitorsXml = "SeparateMonitors";
		private const string k_backOpacityXml = "BackOpacity";
		private const string k_imageFitXml = "ImageFit";
		private const string k_fadeTransitionXml = "FadeTransition";
		private const string k_lastWallpaperFileXml = "LastWallpaperFile";
		private const string k_maxImageScaleXml = "MaxImageScale";
		private const string k_colorEffectXml = "ColorEffect";	// Deprecated
		private const string k_colorEffectForeXml = "ColorEffectFore";
		private const string k_colorEffectBackXml = "ColorEffectBack";
		private const string k_colorEffectCollageFadeXml = "ColorEffectCollageFade";	// Deprecated
		private const string k_colorEffectCollageFadeRatioXml = "ColorEffectCollageFadeRatio";
		private const string k_location = "Location";

		private const string k_featherEnableXml = "Feather";	// Deprecated
		private const string k_featherDistXml = "FeatherEdges";
		private const string k_edgeModeXml = "EdgeMode";
		private const string k_edgeDistXml = "EdgeDist";
		private const string k_borderColorXml = "BorderColor";

		private const string k_dropShadowXml = "DropShadow";
		private const string k_dropShadowDistXml = "DropShadowDist";
		private const string k_dropShadowFeatherXml = "DropShadowFeather";
		private const string k_dropShadowFeatherDistXml = "DropShadowFeatherDist";
		private const string k_dropShadowOpacityXml = "DropShadowOpacity";

		private const string k_backgroundBlurXml = "BackgroundBlur";
		private const string k_backgroundBlurDistXml = "BackgroundBlurDist";

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString(k_nameXml, _name);
			if (_active) xml.WriteAttributeString(k_activeXml, Boolean.TrueString);
			xml.WriteAttributeString(k_freqXml, _freq.ToString());
			xml.WriteAttributeString(k_periodXml, _period.ToString());
			xml.WriteAttributeString(k_modeXml, _mode.ToString());
			xml.WriteAttributeString(k_imageSizeXml, _imageSize.ToString());
			xml.WriteAttributeString(k_backColorTopXml, ColorUtil.ColorToString(_backColorTop));
			xml.WriteAttributeString(k_backColorBottomXml, ColorUtil.ColorToString(_backColorBottom));
			if (!_separateMonitors) xml.WriteAttributeString(k_separateMonitorsXml, _separateMonitors.ToString());

			if (_mode == ThemeMode.Collage)
			{
				xml.WriteAttributeString(k_backOpacityXml, _backOpacity.ToString());
			}
			else
			{
				xml.WriteAttributeString(k_imageFitXml, _imageFit.ToString());
			}

			if (_fadeTransition != k_defaultFadeTransition) xml.WriteAttributeString(k_fadeTransitionXml, _fadeTransition.ToString());
			if (!string.IsNullOrEmpty(_lastWallpaperFile)) xml.WriteAttributeString(k_lastWallpaperFileXml, _lastWallpaperFile);
			if (_maxImageScale != k_defaultMaxImageScale) xml.WriteAttributeString(k_maxImageScaleXml, _maxImageScale.ToString());

			if (_colorEffectFore != ColorEffect.None) xml.WriteAttributeString(k_colorEffectForeXml, _colorEffectFore.ToString());
			if (_colorEffectBack != ColorEffect.None) xml.WriteAttributeString(k_colorEffectBackXml, _colorEffectBack.ToString());
			if (_colorEffectBack != ColorEffect.None) xml.WriteAttributeString(k_colorEffectCollageFadeRatioXml, _colorEffectBackRatio.ToString());

			if (_edgeMode != k_defaultEdgeMode) xml.WriteAttributeString(k_edgeModeXml, _edgeMode.ToString());
			if (_edgeDist != k_defaultEdgeDist) xml.WriteAttributeString(k_edgeDistXml, _edgeDist.ToString());
			if (_borderColor != k_defaultBorderColor) xml.WriteAttributeString(k_borderColorXml, ColorUtil.ColorToString(_borderColor));

			if (_dropShadow != k_defaultDropShadow) xml.WriteAttributeString(k_dropShadowXml, _dropShadow.ToString());
			if (_dropShadowDist != k_defaultDropShadowDist) xml.WriteAttributeString(k_dropShadowDistXml, _dropShadowDist.ToString());
			if (_dropShadowFeather != k_defaultDropShadowFeather) xml.WriteAttributeString(k_dropShadowFeatherXml, _dropShadowFeather.ToString());
			if (_dropShadowFeatherDist != k_defaultDropShadowFeatherDist) xml.WriteAttributeString(k_dropShadowFeatherDistXml, _dropShadowFeatherDist.ToString());
			if (_dropShadowOpacity != k_defaultDropShadowOpacity) xml.WriteAttributeString(k_dropShadowOpacityXml, _dropShadowOpacity.ToString());

			if (_backgroundBlur != k_defaultBackgroundBlur) xml.WriteAttributeString(k_backgroundBlurXml, _backgroundBlur.ToString());
			if (_backgroundBlurDist != k_defaultBackgroundBlurDist) xml.WriteAttributeString(k_backgroundBlurDistXml, _backgroundBlurDist.ToString());

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
			_name = Util.ParseString(xmlTheme, k_nameXml, _id.ToString());

			_active = Util.ParseBool(xmlTheme, k_activeXml, false);
			_freq = Util.ParseInt(xmlTheme, k_freqXml, k_defaultFreq);
			_period = Util.ParseEnum<Period>(xmlTheme, k_periodXml, k_defaultPeriod);
			CalcInterval();
			_mode = Util.ParseEnum<ThemeMode>(xmlTheme, k_modeXml, k_defaultMode);
			_imageSize = Util.ParseInt(xmlTheme, k_imageSizeXml, k_defaultImageSize);
			_backColorTop = ColorUtil.ParseColor(xmlTheme, k_backColorTopXml, k_defaultBackColor);
			_backColorBottom = ColorUtil.ParseColor(xmlTheme, k_backColorBottomXml, k_defaultBackColor);
			_separateMonitors = Util.ParseBool(xmlTheme, k_separateMonitorsXml, k_defaultSeparateMonitors);
			_backOpacity = Util.ParseInt(xmlTheme, k_backOpacityXml, k_defaultBackOpacity);
			_imageFit = Util.ParseEnum<ImageFit>(xmlTheme, k_imageFitXml, k_defaultImageFit);
			if (xmlTheme.HasAttribute(k_featherEnableXml))
			{
				_edgeMode = Util.ParseBool(xmlTheme, k_featherEnableXml, k_defaultFeatherEnable) ? EdgeMode.Feather : WallSwitch.EdgeMode.None;
			}
			else
			{
				_edgeMode = Util.ParseEnum<EdgeMode>(xmlTheme, k_edgeModeXml, k_defaultEdgeMode);
			}
			_edgeDist = Util.ParseInt(xmlTheme, k_edgeDistXml, k_defaultEdgeDist);
			_borderColor = ColorUtil.ParseColor(xmlTheme, k_borderColorXml, k_defaultBorderColor);
			_fadeTransition = Util.ParseBool(xmlTheme, k_fadeTransitionXml, k_defaultFadeTransition);
			_lastWallpaperFile = Util.ParseString(xmlTheme, k_lastWallpaperFileXml, string.Empty);
			_maxImageScale = Util.ParseInt(xmlTheme, k_maxImageScaleXml, k_defaultMaxImageScale);

			// Old XML settings use a single attribute to store a fore/back color effect.
			// New XML uses a separate attribute for each.
			if (xmlTheme.HasAttribute(k_colorEffectXml))
			{
				if (Util.ParseBool(xmlTheme, k_colorEffectCollageFadeXml, false))
				{
					_colorEffectBack = Util.ParseEnum<ColorEffect>(xmlTheme, k_colorEffectXml, ColorEffect.None);
					_colorEffectFore = ColorEffect.None;
				}
				else
				{
					_colorEffectFore = Util.ParseEnum<ColorEffect>(xmlTheme, k_colorEffectXml, ColorEffect.None);
					_colorEffectBack = ColorEffect.None;
				}
			}
			_colorEffectFore = Util.ParseEnum<ColorEffect>(xmlTheme, k_colorEffectForeXml, _colorEffectFore);
			_colorEffectBack = Util.ParseEnum<ColorEffect>(xmlTheme, k_colorEffectBackXml, _colorEffectBack);
			_colorEffectBackRatio = Util.ParseInt(xmlTheme, k_colorEffectCollageFadeRatioXml, k_defaultColorEffectBackRatio);

			_dropShadow = Util.ParseBool(xmlTheme, k_dropShadowXml, k_defaultDropShadow);
			_dropShadowDist = Util.ParseInt(xmlTheme, k_dropShadowDistXml, k_defaultDropShadowDist);
			_dropShadowFeather = Util.ParseBool(xmlTheme, k_dropShadowFeatherXml, k_defaultDropShadowFeather);
			_dropShadowFeatherDist = Util.ParseInt(xmlTheme, k_dropShadowFeatherDistXml, k_defaultDropShadowFeatherDist);
			_dropShadowOpacity = Util.ParseInt(xmlTheme, k_dropShadowOpacityXml, k_defaultDropShadowOpacity);

			_backgroundBlur = Util.ParseBool(xmlTheme, k_backgroundBlurXml, k_defaultBackgroundBlur);
			_backgroundBlurDist = Util.ParseInt(xmlTheme, k_backgroundBlurDistXml, k_defaultBackgroundBlurDist);

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

			foreach (var rect in _imageRectHistory)
			{
				xml.WriteStartElement("ImageRectHistory");
				xml.WriteAttributeString("X", rect.X.ToString());
				xml.WriteAttributeString("Y", rect.Y.ToString());
				xml.WriteAttributeString("Width", rect.Width.ToString());
				xml.WriteAttributeString("Height", rect.Height.ToString());
				xml.WriteEndElement();	// ImageRectHistory
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

			_imageRectHistory.Clear();
			foreach (XmlElement xmlRect in xmlTheme.SelectNodes("ImageRectHistory"))
			{
				float x, y, width, height;
				if (xmlRect.HasAttribute("X") && xmlRect.HasAttribute("Y") &&
					xmlRect.HasAttribute("Width") && xmlRect.HasAttribute("Height") &&
					float.TryParse(xmlRect.GetAttribute("X"), out x) &&
					float.TryParse(xmlRect.GetAttribute("Y"), out y) &&
					float.TryParse(xmlRect.GetAttribute("Width"), out width) &&
					float.TryParse(xmlRect.GetAttribute("Height"), out height))
				{
					_imageRectHistory.Add(new RectangleF(x, y, width, height));
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

			_imageRectHistory.Clear();
		}

		public IEnumerable<IEnumerable<ImageRec>> History
		{
			get { return _history; }
		}

		public IEnumerable<RectangleF> ImageRectHistory
		{
			get { return _imageRectHistory; }
		}

		public void AddImageRectHistory(RectangleF rect, int numMonitors)
		{
			_imageRectHistory.Add(rect);
			while (_imageRectHistory.Count > k_maxImageRectHistory * numMonitors)
			{
				_imageRectHistory.RemoveAt(0);
			}
		}

		public void ClearImageRectHistory()
		{
			_imageRectHistory.Clear();
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
			foreach (Location loc in (from l in _locations
									  where !l.Disabled
									  select l)) files.AddRange(loc.Files);

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
		public ColorEffect ColorEffectFore
		{
			get { return _colorEffectFore; }
			set { _colorEffectFore = value; }
		}

		public ColorEffect ColorEffectBack
		{
			get { return _colorEffectBack; }
			set { _colorEffectBack = value; }
		}

		public int ColorEffectBackRatio
		{
			get { return _colorEffectBackRatio; }
			set
			{
				if (value < 0) _colorEffectBackRatio = 0;
				else if (value > 100) _colorEffectBackRatio = 100;
				else _colorEffectBackRatio = value;
			}
		}
		#endregion

		#region Edges
		public EdgeMode EdgeMode
		{
			get { return _edgeMode; }
			set { _edgeMode = value; }
		}

		public int EdgeDist
		{
			get { return _edgeDist; }
			set
			{
				if (value < 0) throw new ArgumentOutOfRangeException("Edge distance cannot be less than zero.");
				_edgeDist = value;
			}
		}

		public Color BorderColor
		{
			get { return _borderColor; }
			set { _borderColor = value; }
		}
		#endregion

		#region Drop Shadow
		public bool DropShadow
		{
			get { return _dropShadow; }
			set { _dropShadow = value; }
		}

		public int DropShadowDist
		{
			get { return _dropShadowDist; }
			set { _dropShadowDist = value; }
		}

		public bool DropShadowFeather
		{
			get { return _dropShadowFeather; }
			set { _dropShadowFeather = value; }
		}

		public int DropShadowFeatherDist
		{
			get { return _dropShadowFeatherDist; }
			set { _dropShadowFeatherDist = value; }
		}

		public int DropShadowOpacity
		{
			get { return _dropShadowOpacity; }
			set
			{
				_dropShadowOpacity = value;
				if (_dropShadowOpacity < 0) _dropShadowOpacity = 0;
				else if (_dropShadowOpacity > 100) _dropShadowOpacity = 100;
			}
		}
		#endregion

		#region Background Blur
		public bool BackgroundBlur
		{
			get { return _backgroundBlur; }
			set { _backgroundBlur = value; }
		}

		public int BackgroundBlurDist
		{
			get { return _backgroundBlurDist; }
			set
			{
				_backgroundBlurDist = value;
				if (_backgroundBlurDist < 0) _backgroundBlurDist = 0;
			}
		}
		#endregion
	}
}
