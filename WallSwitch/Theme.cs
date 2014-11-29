using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace WallSwitch
{
	internal class Theme
	{
		#region Constants
		private const int k_maxHistory = 10;
		private const int k_maxRetrievalRetries = 10;
		private const int k_maxRepeatHistoryRetries = 3;
		private const int k_maxRepeatNowRetries = 10;
		private const int k_maxImageRectHistory = 3;
		public const int k_maxNumCollageImages = 10;
		public const float k_aspectThreshold = 0.1f;	// The maximum distortion in aspect for spanning a monitor.

		private const int k_defaultFreq = 5;
		private const Period k_defaultPeriod = Period.Minutes;
		private const ThemeMode k_defaultMode = ThemeMode.FullScreen;
		private const ThemeOrder k_defaultOrder = ThemeOrder.Random;
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
		public const int k_defaultNumCollageImages = 1;
		public const int k_defaultColorEffectBackRatio = 25;
		private const bool k_defaultDropShadow = false;
		private const int k_defaultDropShadowDist = 15;
		private const bool k_defaultDropShadowFeather = true;
		private const int k_defaultDropShadowFeatherDist = 20;
		private const int k_defaultDropShadowOpacity = 50;
		private const bool k_defaultBackgroundBlur = false;
		private const int k_defaultBackgroundBlurDist = 4;
		private const bool k_defaultAllowSpanning = true;
		#endregion

		#region Variables
		private Guid _id;
		private List<Location> _locations = new List<Location>();
		private string _name = string.Empty;
		private int _freq = k_defaultFreq;
		private Period _period = k_defaultPeriod;
		private TimeSpan _interval;
		private ThemeMode _mode = k_defaultMode;
		private ThemeOrder _order = k_defaultOrder;
		private int _imageSize = k_defaultImageSize;	// Used only for collage mode
		private bool _active = false;
		private bool _separateMonitors = k_defaultSeparateMonitors;
		private HotKey _hotKey = new HotKey();
		private Color _backColorTop = k_defaultBackColor;
		private Color _backColorBottom = k_defaultBackColor;
		private int _backOpacity = k_defaultBackOpacity;
		private ImageFit _imageFit = k_defaultImageFit;
		private List<IEnumerable<ImageLayout>> _history = new List<IEnumerable<ImageLayout>>();
		private List<RectangleF> _imageRectHistory = new List<RectangleF>();
		private int _historyIndex = -1;
		private Random _rand = null;
		private bool _fadeTransition = k_defaultFadeTransition;
		private string _lastWallpaperFile = string.Empty;
		private int _maxImageScale = k_defaultMaxImageScale;
		private int _numCollageImages = k_defaultNumCollageImages;
		private bool _allowSpanning = k_defaultAllowSpanning;
		private string _lastImage = null;

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

		private List<WidgetInstance> _widgets = new List<WidgetInstance>();
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

		public ThemeOrder Order
		{
			get { return _order; }
			set { _order = value; }
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

		public bool AllowSpanning
		{
			get { return _allowSpanning; }
			set { _allowSpanning = value; }
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

		public int NumCollageImages
		{
			get { return _numCollageImages; }
			set { _numCollageImages = value; }
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
			xml.WriteAttributeString("Order", _order.ToString());
			xml.WriteAttributeString("ImageSize", _imageSize.ToString());
			xml.WriteAttributeString("BackColorTop", ColorUtil.ColorToString(_backColorTop));
			xml.WriteAttributeString("BackColorBottom", ColorUtil.ColorToString(_backColorBottom));
			if (_separateMonitors != k_defaultSeparateMonitors) xml.WriteAttributeString("SeparateMonitors", _separateMonitors.ToString());
			if (_allowSpanning != k_defaultAllowSpanning) xml.WriteAttributeString("AllowSpanning", _allowSpanning.ToString());

			if (_mode == ThemeMode.Collage)
			{
				xml.WriteAttributeString("BackOpacity", _backOpacity.ToString());
			}
			else
			{
				xml.WriteAttributeString("ImageFit", _imageFit.ToString());
			}

			if (_fadeTransition != k_defaultFadeTransition) xml.WriteAttributeString("FadeTransition", _fadeTransition.ToString());
			if (!string.IsNullOrEmpty(_lastWallpaperFile)) xml.WriteAttributeString("LastWallpaperFile", _lastWallpaperFile);
			if (_maxImageScale != k_defaultMaxImageScale) xml.WriteAttributeString("MaxImageScale", _maxImageScale.ToString());
			if (_numCollageImages != k_defaultNumCollageImages) xml.WriteAttributeString("NumCollageImages", _numCollageImages.ToString());

			if (_colorEffectFore != ColorEffect.None) xml.WriteAttributeString("ColorEffectFore", _colorEffectFore.ToString());
			if (_colorEffectBack != ColorEffect.None) xml.WriteAttributeString("ColorEffectBack", _colorEffectBack.ToString());
			if (_colorEffectBack != ColorEffect.None) xml.WriteAttributeString("ColorEffectCollageFadeRatio", _colorEffectBackRatio.ToString());

			if (_edgeMode != k_defaultEdgeMode) xml.WriteAttributeString("EdgeMode", _edgeMode.ToString());
			if (_edgeDist != k_defaultEdgeDist) xml.WriteAttributeString("EdgeDist", _edgeDist.ToString());
			if (_borderColor != k_defaultBorderColor) xml.WriteAttributeString("BorderColor", ColorUtil.ColorToString(_borderColor));

			if (_dropShadow != k_defaultDropShadow) xml.WriteAttributeString("DropShadow", _dropShadow.ToString());
			if (_dropShadowDist != k_defaultDropShadowDist) xml.WriteAttributeString("DropShadowDist", _dropShadowDist.ToString());
			if (_dropShadowFeather != k_defaultDropShadowFeather) xml.WriteAttributeString("DropShadowFeather", _dropShadowFeather.ToString());
			if (_dropShadowFeatherDist != k_defaultDropShadowFeatherDist) xml.WriteAttributeString("DropShadowFeatherDist", _dropShadowFeatherDist.ToString());
			if (_dropShadowOpacity != k_defaultDropShadowOpacity) xml.WriteAttributeString("DropShadowOpacity", _dropShadowOpacity.ToString());

			if (_backgroundBlur != k_defaultBackgroundBlur) xml.WriteAttributeString("BackgroundBlur", _backgroundBlur.ToString());
			if (_backgroundBlurDist != k_defaultBackgroundBlurDist) xml.WriteAttributeString("BackgroundBlurDist", _backgroundBlurDist.ToString());

			if (!string.IsNullOrEmpty(_lastImage)) xml.WriteAttributeString("LastImage", _lastImage);

			_hotKey.SaveXml(xml);

			foreach (var loc in _locations)
			{
				xml.WriteStartElement("Location");
				loc.Save(xml);
				xml.WriteEndElement();	// Location
			}

			foreach (var widget in _widgets)
			{
				widget.Save(xml);
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

			// Old XML has Mode "Sequential", "Random" and "Collage", but enum is now different
			var mode = Util.ParseString(xmlTheme, "Mode", ThemeMode.FullScreen.ToString());
			if (mode == "Sequential")
			{
				_mode = ThemeMode.FullScreen;
				_order = ThemeOrder.Sequential;
			}
			else if (mode == "Random")
			{
				_mode = ThemeMode.FullScreen;
				_order = ThemeOrder.Random;
			}
			else
			{
				_mode = Util.ParseEnum<ThemeMode>(xmlTheme, "Mode", k_defaultMode);
				_order = Util.ParseEnum<ThemeOrder>(xmlTheme, "Order", k_defaultOrder);
			}

			_imageSize = Util.ParseInt(xmlTheme, "ImageSize", k_defaultImageSize);
			_backColorTop = ColorUtil.ParseColor(xmlTheme, "BackColorTop", k_defaultBackColor);
			_backColorBottom = ColorUtil.ParseColor(xmlTheme, "BackColorBottom", k_defaultBackColor);
			_separateMonitors = Util.ParseBool(xmlTheme, "SeparateMonitors", k_defaultSeparateMonitors);
			_allowSpanning = Util.ParseBool(xmlTheme, "AllowSpanning", k_defaultAllowSpanning);
			_backOpacity = Util.ParseInt(xmlTheme, "BackOpacity", k_defaultBackOpacity);
			_imageFit = Util.ParseEnum<ImageFit>(xmlTheme, "ImageFit", k_defaultImageFit);
			if (xmlTheme.HasAttribute("Feather"))	// Deprecated
			{
				_edgeMode = Util.ParseBool(xmlTheme, "Feather", k_defaultFeatherEnable) ? EdgeMode.Feather : WallSwitch.EdgeMode.None;
			}
			else
			{
				_edgeMode = Util.ParseEnum<EdgeMode>(xmlTheme, "EdgeMode", k_defaultEdgeMode);
			}
			_edgeDist = Util.ParseInt(xmlTheme, "EdgeDist", k_defaultEdgeDist);
			_borderColor = ColorUtil.ParseColor(xmlTheme, "BorderColor", k_defaultBorderColor);
			_fadeTransition = Util.ParseBool(xmlTheme, "FadeTransition", k_defaultFadeTransition);
			_lastWallpaperFile = Util.ParseString(xmlTheme, "LastWallpaperFile", string.Empty);
			_maxImageScale = Util.ParseInt(xmlTheme, "MaxImageScale", k_defaultMaxImageScale);
			_numCollageImages = Util.ParseInt(xmlTheme, "NumCollageImages", k_defaultNumCollageImages);

			// Old XML settings use a single attribute to store a fore/back color effect.
			// New XML uses a separate attribute for each.
			if (xmlTheme.HasAttribute("ColorEffect"))	// Deprecated
			{
				if (Util.ParseBool(xmlTheme, "ColorEffectCollageFade", false))	// Deprecated
				{
					_colorEffectBack = Util.ParseEnum<ColorEffect>(xmlTheme, "ColorEffect", ColorEffect.None);
					_colorEffectFore = ColorEffect.None;
				}
				else
				{
					_colorEffectFore = Util.ParseEnum<ColorEffect>(xmlTheme, "ColorEffect", ColorEffect.None);
					_colorEffectBack = ColorEffect.None;
				}
			}
			_colorEffectFore = Util.ParseEnum<ColorEffect>(xmlTheme, "ColorEffectFore", _colorEffectFore);
			_colorEffectBack = Util.ParseEnum<ColorEffect>(xmlTheme, "ColorEffectBack", _colorEffectBack);
			_colorEffectBackRatio = Util.ParseInt(xmlTheme, "ColorEffectCollageFadeRatio", k_defaultColorEffectBackRatio);

			_dropShadow = Util.ParseBool(xmlTheme, "DropShadow", k_defaultDropShadow);
			_dropShadowDist = Util.ParseInt(xmlTheme, "DropShadowDist", k_defaultDropShadowDist);
			_dropShadowFeather = Util.ParseBool(xmlTheme, "DropShadowFeather", k_defaultDropShadowFeather);
			_dropShadowFeatherDist = Util.ParseInt(xmlTheme, "DropShadowFeatherDist", k_defaultDropShadowFeatherDist);
			_dropShadowOpacity = Util.ParseInt(xmlTheme, "DropShadowOpacity", k_defaultDropShadowOpacity);

			_backgroundBlur = Util.ParseBool(xmlTheme, "BackgroundBlur", k_defaultBackgroundBlur);
			_backgroundBlurDist = Util.ParseInt(xmlTheme, "BackgroundBlurDist", k_defaultBackgroundBlurDist);

			_lastImage = xmlTheme.GetAttribute("LastImage");

			foreach (var xmlHotKey in xmlTheme.SelectNodes(HotKey.XmlElementName).Cast<XmlElement>())
			{
				_hotKey.LoadXml(xmlHotKey);
			}

			foreach (var xmlLoc in xmlTheme.SelectNodes("Location").Cast<XmlElement>())
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

			foreach (var xmlWidget in xmlTheme.SelectNodes(WidgetInstance.XmlElementName).Cast<XmlElement>())
			{
				try
				{
					var widget = WidgetInstance.Load(xmlWidget);
					_widgets.Add(widget);
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when loading widget from settings file.");
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

				var images = new List<ImageLayout>();
				foreach (XmlElement xmlImage in xmlHistory.SelectNodes("Image"))
				{
					var image = ImageLayout.FromXml(xmlImage);
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

			var fileName = GetBaseWallpaperFileName(ImageFormat.Bmp);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetBaseWallpaperFileName(ImageFormat.Jpeg);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetBaseWallpaperFileName(ImageFormat.Png);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetDisplayWallpaperFileName(ImageFormat.Bmp);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetDisplayWallpaperFileName(ImageFormat.Jpeg);
			if (File.Exists(fileName)) File.Delete(fileName);

			fileName = GetDisplayWallpaperFileName(ImageFormat.Png);
			if (File.Exists(fileName)) File.Delete(fileName);

			_imageRectHistory.Clear();

			_lastImage = null;
		}

		public IEnumerable<IEnumerable<ImageLayout>> History
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

			var maxHistory = k_maxImageRectHistory * numMonitors;
			if (_mode == ThemeMode.Collage) maxHistory *= _numCollageImages;

			while (_imageRectHistory.Count > maxHistory)
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
		public IEnumerable<ImageLayout> GetNextImages(Rectangle[] monitorRects)
		{
			IEnumerable<ImageLayout> ret = null;

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
					if (!image.ImageRec.Retrieve())
					{
						allFound = false;
						break;
					}
				}
				if (allFound) return imageSet;
			}

			// Go through each location and find files.
			var allFiles = new List<ImageRec>();
			foreach (Location loc in (from l in _locations
									  where !l.Disabled
									  select l)) allFiles.AddRange(loc.Files);

			Log.Write(LogLevel.Debug, allFiles.Count.ToString() + " files found.");

			if (allFiles.Count > 0)
			{
				allFiles.Sort();
				ret = PickImages(allFiles, monitorRects);
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

		/// <summary>
		/// Creates a list of images to be displayed for all monitors.
		/// This function takes into account collage/fullscreen mode, and random/sequential order.
		/// </summary>
		/// <param name="allFiles">A list of all available image files</param>
		/// <param name="monitorRects">A list of all monitor rectangles</param>
		/// <returns>A list of images to be rendered</returns>
		private List<ImageLayout> PickImages(List<ImageRec> allFiles, Rectangle[] monitorRects)
		{
			if (_mode == ThemeMode.Collage)
			{
				var pickedFiles = new List<ImageLayout>();

				for (int monitorIndex = 0; monitorIndex < monitorRects.Length; monitorIndex++)
				{
					for (int imageIndex = 0; imageIndex < _numCollageImages; imageIndex++)
					{
						var img = PickRandomOrSequentialImage(allFiles, pickedFiles);
						if (img == null) break;
						pickedFiles.Add(new ImageLayout(img, new int[] { monitorIndex }));
					}
				}

				return pickedFiles;
			}
			else // fullscreen
			{
				var monitorSelections = (from m in monitorRects select new MonitorSelection { Rect = m, ImageRec = null }).ToArray();
				var retries = 0;
				var pickedFiles = new List<ImageLayout>();

				while (monitorSelections.Any(m => m.ImageRec == null) && retries <= k_maxRetrievalRetries)
				{
					var img = PickRandomOrSequentialImage(allFiles, pickedFiles);
					if (img == null) break;
					img.Retrieve();

					bool success = false;

					if (_separateMonitors)
					{
						if (_allowSpanning)
						{
							Log.Write(LogLevel.Debug, "Allowing spanning and separate images.");

							if (FindBestMonitorSpanningSelection(img, monitorSelections)) success = true;
							else Log.Write(LogLevel.Debug, "Unable to find best monitor for spanning.");
						}
						else
						{
							Log.Write(LogLevel.Debug, "Allowing separate images but not spanning.");

							// Put this image in the first vacant monitor
							var vacantMonitor = monitorSelections.First(m => m.ImageRec == null);
							vacantMonitor.ImageRec = img;
							vacantMonitor.Collection = Guid.NewGuid();
							success = true;
							Log.Write(LogLevel.Debug, "Put image in first vacant monitor [{0}]", vacantMonitor);
						}
					}
					else
					{
						if (_allowSpanning)
						{
							Log.Write(LogLevel.Debug, "Allowing spanning but no separate images.");

							// Span this image across as many monitors as possible, and keep spanning with the same image until all monitors are full.
							while (FindBestMonitorSpanningSelection(img, monitorSelections)) ;
							success = true;
						}
						else
						{
							Log.Write(LogLevel.Debug, "No spanning or separate images.");

							// Put this image in each monitor.
							foreach (var monitor in monitorSelections)
							{
								monitor.ImageRec = img;
								monitor.Collection = Guid.NewGuid();
							}
							success = true;
						}
					}

					if (success) retries = 0;
					else retries++;

					GenerateImageLayoutsFromMonitorSelections(monitorSelections, pickedFiles);
				}

				return pickedFiles;
			}
		}

		private void GenerateImageLayoutsFromMonitorSelections(MonitorSelection[] monitorSelections, List<ImageLayout> imageLayouts)
		{
			imageLayouts.Clear();

			var collections = new List<Guid>();

			for (int m = 0; m < monitorSelections.Length; m++)
			{
				var img = monitorSelections[m].ImageRec;
				if (img == null) continue;

				var collection = monitorSelections[m].Collection;
				if (collections.Contains(collection)) continue;

				var monitorList = new List<int>();
				for (int n = m; n < monitorSelections.Length; n++)
				{
					if (monitorSelections[n].Collection == collection)
					{
						monitorList.Add(n);
					}
				}
				collections.Add(monitorSelections[m].Collection);

				imageLayouts.Add(new ImageLayout(img, monitorList.ToArray()));
			}
		}

		private ImageRec PickRandomOrSequentialImage(List<ImageRec> allFiles, List<ImageLayout> filesPickedThisTime)
		{
			ImageRec img;
			if (_order == ThemeOrder.Random)
			{
				img = PickRandomImage(allFiles, filesPickedThisTime);
			}
			else // sequential
			{
				img = PickSequentialImage(allFiles, filesPickedThisTime);
			}

			_lastImage = img.Location;
			return img;
		}

		private ImageRec PickRandomImage(List<ImageRec> allFiles, List<ImageLayout> filesPickedThisTime)
		{
			var repeatNowRetries = 0;
			var repeatHistoryRetries = 0;
			var loadRetries = 0;

			while (true)
			{
				var index = _rand.Next(allFiles.Count);
				var img = allFiles[index];

				if (filesPickedThisTime.Any(i => i.ImageRec == img))
				{
					repeatNowRetries++;
					if (repeatNowRetries <= k_maxRepeatNowRetries) continue;
				}
				repeatNowRetries = 0;

				if (ImageRecIsInHistory(img))
				{
					repeatHistoryRetries++;
					if (repeatHistoryRetries <= k_maxRepeatHistoryRetries) continue;
				}
				repeatHistoryRetries = 0;

				if (img.Retrieve())
				{
					return img;
				}

				loadRetries++;
				if (loadRetries > k_maxRetrievalRetries) break;
			}

			return null;
		}

		private ImageRec PickSequentialImage(List<ImageRec> allFiles, List<ImageLayout> filesPickedThisTime)
		{
			Log.Write(LogLevel.Debug, "Picking sequential image (last image = [{0}])", _lastImage);

			if (allFiles.Count == 0)
			{
				Log.Write(LogLevel.Debug, "There are no images to choose from; returning null.");
				return null;
			}
			if (string.IsNullOrEmpty(_lastImage))
			{
				Log.Write(LogLevel.Debug, "No 'last image' was saved, so returning the first image in the list [{0}]", allFiles[0].Location);
				return allFiles[0];
			}

			// Find the first image that is greater than the last file.
			var img = allFiles.FirstOrDefault(i => string.Compare(i.Location, _lastImage, StringComparison.OrdinalIgnoreCase) > 0);
			if (img != null)
			{
				Log.Write(LogLevel.Debug, "Next sequential image is [{0}]", img.Location);
				return img;
			}

			Log.Write(LogLevel.Debug, "No images greater than last image; returning first image [{0}]", allFiles[0].Location);
			return allFiles[0];
		}

		private class MonitorSelection
		{
			public Rectangle Rect { get; set; }
			public ImageRec ImageRec { get; set; }
			public Guid Collection { get; set; }
		}

		private bool FindBestMonitorSpanningSelection(ImageRec img, MonitorSelection[] monitorSelections)
		{
			var imgSize = img.Image.Size;
			var imageAspect = imgSize.GetAspect();

			int bestUnusedStart = -1;
			int bestUnusedCount = -1;
			float bestUnusedAspectDiff = 0.0f;

			int bestUsedStart = -1;
			int bestUsedCount = -1;
			float bestUsedAspectDiff = 0.0f;
			List<ImageRec> bestUsedImages = new List<ImageRec>();

			for (var startMonitor = 0; startMonitor < monitorSelections.Length; startMonitor++)
			{
				for (var numMonitors = 1; numMonitors <= monitorSelections.Length - startMonitor; numMonitors++)
				{
					if (!MonitorsAreAdjascent(monitorSelections, startMonitor, numMonitors)) continue;

					var monitors = monitorSelections.Skip(startMonitor).Take(numMonitors).ToArray();
					var envelope = (from m in monitors select m.Rect).GetEnvelope();
					var aspect = envelope.Size.GetAspect();
					var aspectDiff = Math.Abs(aspect - imageAspect);

					if (monitors.Any(m => m.ImageRec != null))
					{
						if (bestUsedStart == -1 || aspectDiff < bestUsedAspectDiff)
						{
							bestUsedStart = startMonitor;
							bestUsedCount = numMonitors;
							bestUsedAspectDiff = aspectDiff;

							bestUsedImages.Clear();
							foreach (var monitor in monitors)
							{
								if (!bestUsedImages.Contains(monitor.ImageRec)) bestUsedImages.Remove(monitor.ImageRec);
							}
						}
					}
					else
					{
						if (bestUnusedStart == -1 || aspectDiff < bestUnusedAspectDiff)
						{
							bestUnusedStart = startMonitor;
							bestUnusedCount = numMonitors;
							bestUnusedAspectDiff = aspectDiff;
						}
					}
				}
			}

			var displacementAllowed = _order != ThemeOrder.Sequential;

			if (bestUnusedStart != -1)
			{
				if (displacementAllowed &&
					bestUsedStart != -1 &&
					bestUsedAspectDiff < bestUnusedAspectDiff &&
					Math.Abs(bestUsedAspectDiff - bestUnusedAspectDiff) > k_aspectThreshold)
				{
					// There's a better fit if we displace a previously selected image.
					Log.Write(LogLevel.Debug, "Using monitor range start [{0}] count [{1}] (displaced previously picked images)", bestUsedStart, bestUsedCount);
					var guid = Guid.NewGuid();
					for (int i = bestUsedStart; i < bestUsedStart + bestUsedCount; i++)
					{
						monitorSelections[i].ImageRec = img;
						monitorSelections[i].Collection = guid;
					}

					// Clear out any other places that image may have been selected (so we don't displace half a spanned image)
					foreach (var monitor in monitorSelections)
					{
						if (bestUsedImages.Contains(monitor.ImageRec))
						{
							monitor.ImageRec = null;
							monitor.Collection = Guid.Empty;
						}
					}

					return true;
				}
				else
				{
					// Use the unused range
					Log.Write(LogLevel.Debug, "Using monitor range start [{0}] count [{1}] (unused range)", bestUnusedStart, bestUnusedCount);
					var guid = Guid.NewGuid();
					for (int i = bestUnusedStart; i < bestUnusedStart + bestUnusedCount; i++)
					{
						monitorSelections[i].ImageRec = img;
						monitorSelections[i].Collection = guid;
					}

					return true;
				}
			}
			else
			{
				return false;
			}
		}

		private bool MonitorsAreAdjascent(MonitorSelection[] monitorSelections, int startMonitor, int numMonitors)
		{
			if (numMonitors == 1) return true;

			// Check horizontal alignment
			var aligned = true;
			for (int m = 1; m < numMonitors; m++)
			{
				if (monitorSelections[m - 1].Rect.Right != monitorSelections[m].Rect.Left)
				{
					aligned = false;
					break;
				}
			}
			if (aligned) return true;

			// Check vertical alignment
			aligned = false;
			for (int m = 1; m < numMonitors; m++)
			{
				if (monitorSelections[m - 1].Rect.Bottom != monitorSelections[m].Rect.Top)
				{
					aligned = false;
					break;
				}
			}
			if (aligned) return true;

			return false;
		}

		/// <summary>
		/// This function will attempt to fit an image to one or more monitors (depending if spanning is enabled).
		/// </summary>
		/// <param name="img">The image to fit</param>
		/// <param name="allMonitorRects">The bounds of monitors the image should attempt to fit to.</param>
		/// <returns>The number of monitors this image should be spanned across, or 0 if the image can't be loaded.</returns>
		private int FitImageToMonitorRects(ImageRec img, Rectangle[] allMonitorRects)
		{
			Log.Write(LogLevel.Debug, "Attempting to fit image [{0}] to {1} monitors", img.Location, allMonitorRects.Length);	// TODO

			if (!img.Retrieve())
			{
				Log.Write(LogLevel.Warning, "Failed to retrieve image [{0}]", img.Location);
				return 0;
			}

			if (allMonitorRects.Length == 1)
			{
				return 1;	// Don't need to check if the aspect matches for a single monitor.
			}

			var imgSize = img.Image.Size;
			var imageAspect = imgSize.GetAspect();

			// Find the best fit
			int bestNumMonitors = -1;
			float bestAspectDiff = 0.0f;

			for (var monitorCount = 1; monitorCount <= allMonitorRects.Length; monitorCount++)
			{
				// Get the aspect for this number of monitors, and check if it matches the aspect of the image.
				var monitorEnvelope = allMonitorRects.Take(monitorCount).GetEnvelope();
				var monitorAspect = monitorEnvelope.Size.GetAspect();
				var aspectDiff = Math.Abs(monitorAspect - imageAspect);

				if (bestNumMonitors == -1 || aspectDiff < bestAspectDiff)
				{
					bestAspectDiff = aspectDiff;
					bestNumMonitors = monitorCount;
				}
			}

			return bestNumMonitors == -1 ? 1 : bestNumMonitors;
		}

		private bool ImageRecIsInHistory(ImageRec img)
		{
			foreach (var h in _history)
			{
				if (h == null) continue;
				foreach (var i in h)
				{
					if (i.Equals(img)) return true;
				}
			}

			return false;
		}

		public IEnumerable<ImageLayout> GetPrevImages()
		{
			Log.Write(LogLevel.Debug, "Going back to previous images.");
			if (_historyIndex > 0)
			{
				var imageSet = _history[--_historyIndex];
				foreach (var image in imageSet) image.ImageRec.Retrieve();
				return imageSet;
			}
			return null;
		}
		#endregion

		#region Wallpaper
		private void OnActivate(bool active)
		{
		}

		public string GetBaseWallpaperFileName(ImageFormat format)
		{
			var fileName = string.Concat(_id.ToString(), ImageFormatDesc.ImageFormatToExtension(format));
			return Path.Combine(Util.AppDataDir, fileName);
		}

		public string GetDisplayWallpaperFileName(ImageFormat format)
		{
			var fileName = string.Concat(_id.ToString(), "_display", ImageFormatDesc.ImageFormatToExtension(format));
			return Path.Combine(Util.AppDataDir, fileName);
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
			public IEnumerable<ImageLayout> Images { get; set; }

			public HistoryAddedEventArgs(Theme theme, IEnumerable<ImageLayout> images)
				: base(theme)
			{
				Images = images;
			}
		}

		private void FireHistoryAdded(IEnumerable<ImageLayout> images)
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

		#region Widgets
		public IEnumerable<WidgetInstance> Widgets
		{
			get { return _widgets; }
		}

		public void ReplaceWidgets(IEnumerable<WidgetInstance> widgets)
		{
			_widgets.Clear();
			_widgets.AddRange(widgets);
		}
		#endregion
	}
}
