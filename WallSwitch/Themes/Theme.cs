using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
		public const ColorEffect k_defaultColorEffectFore = ColorEffect.None;
		public const ColorEffect k_defaultColorEffectBack = ColorEffect.None;
		public const int k_defaultColorEffectBackRatio = 25;
		private const bool k_defaultDropShadow = false;
		private const int k_defaultDropShadowDist = 15;
		private const bool k_defaultDropShadowFeather = true;
		private const int k_defaultDropShadowFeatherDist = 20;
		private const int k_defaultDropShadowOpacity = 50;
		private const bool k_defaultBackgroundBlur = false;
		private const int k_defaultBackgroundBlurDist = 4;
		private const bool k_defaultAllowSpanning = true;
		private const int k_defaultMaxImageClip = 15;
		#endregion

		#region Member Variables
		private long _rowid;
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
		private List<RectangleF> _imageRectHistory = new List<RectangleF>();
		private string _historyGuid;
		private string _historyLatestGuid;
		private bool _historyCanGoPrev = false;
		private bool _fadeTransition = k_defaultFadeTransition;
		private string _lastWallpaperFile = string.Empty;
		private int _maxImageScale = k_defaultMaxImageScale;
		private int _numCollageImages = k_defaultNumCollageImages;
		private bool _allowSpanning = k_defaultAllowSpanning;
		private int _maxImageClip = k_defaultMaxImageClip;
		private string _lastImage = null;
		private bool _activateOnExit = false;
		private int _randomGroupCount = 1;
		private bool _clearBetweenRandomGroups = false;

		private ColorEffect _colorEffectFore = k_defaultColorEffectFore;
		private ColorEffect _colorEffectBack = k_defaultColorEffectBack;
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
		private ImageFilters.ImageFilter _filter;

		private static Random _rand = null;
		#endregion

		#region Construction
		public Theme()
		{
			if (_rand == null) _rand = new Random((int)(DateTime.Now.Ticks & 0x7fffffffL));

			CalcInterval();

			_hotKey.HotKeyPressed += new EventHandler(_hotKey_HotKeyPressed);
		}

		void _hotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				if (MainWindow.Current != null)
				{
					using (var db = new Database())
					{
						MainWindow.Current.OnActivateTheme(db, this);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when activating theme in response to hotkey.");
			}
		}

		public Theme Clone()
		{
			// Don't clone active, hotkey, history, historyIndex, rand, lastWallpaperFile

			return new Theme()
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
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public long RowId
		{
			get { return _rowid; }
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

		public int MaxImageClip
		{
			get { return _maxImageClip; }
			set
			{
				if (value < 0 || value > 100) throw new ArgumentOutOfRangeException();
				_maxImageClip = value;
			}
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

		public bool ActivateOnExit
		{
			get { return _activateOnExit; }
			set { _activateOnExit = value; }
		}

		public int RandomGroupCount
		{
			get { return _randomGroupCount; }
			set { _randomGroupCount = value; }
		}

		public bool ClearBetweenRandomGroups
		{
			get { return _clearBetweenRandomGroups; }
			set { _clearBetweenRandomGroups = value; }
		}

		public ImageFilters.ImageFilter Filter
		{
			get { return _filter; }
			set { _filter = value; }
		}
		#endregion

		#region Save/Load
		public void Save(Database db)
		{
			var cols = new object[]
			{
				"name", _name,
				"active", _active,
				"freq", _freq,
				"period", _period.ToString(),
				"mode", _mode.ToString(),
				"order_", _order.ToString(),
				"image_size", _imageSize,
				"back_color_top", ColorUtil.ColorToString(_backColorTop),
				"back_color_bottom", ColorUtil.ColorToString(_backColorBottom),
				"separate_monitors", _separateMonitors ? 1 : 0,
				"allow_spanning", _allowSpanning ? 1 : 0,
				"max_image_clip", _maxImageClip,
				"back_opacity", _backOpacity,
				"image_fit", _imageFit.ToString(),
				"edge_mode", _edgeMode.ToString(),
				"edge_dist", _edgeDist,
				"border_color", ColorUtil.ColorToString(_borderColor),
				"fade_transition", _fadeTransition ? 1 : 0,
				"last_wallpaper_file", _lastWallpaperFile,
				"max_image_scale", _maxImageScale,
				"num_collage_images", _numCollageImages,
				"color_effect_fore", _colorEffectFore.ToString(),
				"color_effect_back", _colorEffectBack.ToString(),
				"color_effect_back_ratio", _colorEffectBackRatio,
				"drop_shadow", _dropShadow ? 1 : 0,
				"drop_shadow_dist", _dropShadowDist,
				"drop_shadow_feather", _dropShadowFeather ? 1 : 0,
				"drop_shadow_feather_dist", _dropShadowFeatherDist,
				"drop_shadow_opacity", _dropShadowOpacity,
				"background_blur", _backgroundBlur ? 1 : 0,
				"background_blur_dist", _backgroundBlurDist,
				"last_image", _lastImage,
				"activate_on_exit", _activateOnExit ? 1 : 0,
				"random_group_count", _randomGroupCount,
				"clear_between_random_groups", _clearBetweenRandomGroups ? 1 : 0,
				"hot_key", _hotKey.ToSaveString(),
				"history_guid", _historyGuid,
				"latest_guid", _historyLatestGuid,
				"filter_xml", _filter?.ToSaveString()
			};

			var newRecord = false;
			if (_rowid == 0L)
			{
				_rowid = db.Insert("theme", cols.ToArray());
				newRecord = true;
			}
			else
			{
				db.Update("theme", "rowid = @rowid", cols, new object [] { "@rowid", _rowid });
			}

			// Save locations
			var locRowIds = new List<long>();
			foreach (var loc in _locations)
			{
				loc.Save(db, _rowid);
				locRowIds.Add(loc.RowId);
			}

			if (!newRecord)
			{
				// Purge removed locations
				foreach (var locId in db.SelectLongList("select rowid from location where theme_id = @theme_id", "@theme_id", _rowid))
				{
					if (!_locations.Any(l => l.RowId == locId))
					{
						db.ExecuteNonQuery("delete from location where rowid = @rowid", "@rowid", locId);
					}
				}
			}

			foreach (var widget in _widgets)
			{
				widget.Save(db, _rowid);
			}

			if (!newRecord)
			{
				// Purge removed widgets
				foreach (var id in db.SelectLongList("select rowid from widget where theme_id = @theme_id", "@theme_id", _rowid))
				{
					if (!_widgets.Any(w => w.RowId == id))
					{
						WidgetInstance.PurgeFromDatabase(db, id);
					}
				}
			}
		}

		public void Load(Database db, System.Data.DataRow row)
		{
			_rowid = row.GetLong("rowid");

			_name = row.GetString("name", Res.UnnamedTheme);
			_active = row.GetBoolean("active", false);
			_freq = row.GetInt("freq", k_defaultFreq);
			_period = row.GetEnum<Period>("period", k_defaultPeriod);
			CalcInterval();

			_mode = row.GetEnum<ThemeMode>("mode", k_defaultMode);
			_order = row.GetEnum<ThemeOrder>("order_", k_defaultOrder);

			_imageSize = row.GetInt("image_size", k_defaultImageSize);
			_backColorTop = ColorUtil.ParseColor(row.GetString("back_color_top"), k_defaultBackColor);
			_backColorBottom = ColorUtil.ParseColor(row.GetString("back_color_bottom"), k_defaultBackColor);
			_separateMonitors = row.GetBoolean("separate_monitors", k_defaultSeparateMonitors);
			_allowSpanning = row.GetBoolean("allow_spanning", k_defaultAllowSpanning);

			_maxImageClip = row.GetInt("max_image_clip", k_defaultMaxImageClip);
			if (_maxImageClip < 0) _maxImageClip = 0;
			else if (_maxImageClip > 100) _maxImageClip = 100;

			_backOpacity = row.GetInt("back_opacity", k_defaultBackOpacity);
			_imageFit = row.GetEnum("image_fit", k_defaultImageFit);
			_edgeMode = row.GetEnum("edge_mode", k_defaultEdgeMode);
			_edgeDist = row.GetInt("edge_dist", k_defaultEdgeDist);
			_borderColor = ColorUtil.ParseColor(row.GetString("border_color"), k_defaultBorderColor);
			_fadeTransition = row.GetBoolean("fade_transition", k_defaultFadeTransition);
			_lastWallpaperFile = row.GetString("last_wallpaper_file", string.Empty);
			_maxImageScale = row.GetInt("max_image_scale", k_defaultMaxImageScale);
			_numCollageImages = row.GetInt("num_collage_images", k_defaultNumCollageImages);

			_colorEffectFore = row.GetEnum("color_effect_fore", k_defaultColorEffectFore);
			_colorEffectBack = row.GetEnum("color_effect_back", k_defaultColorEffectBack);
			_colorEffectBackRatio = row.GetInt("color_effect_back_ratio", k_defaultColorEffectBackRatio);

			_dropShadow = row.GetBoolean("drop_shadow", k_defaultDropShadow);
			_dropShadowDist = row.GetInt("drop_shadow_dist", k_defaultDropShadowDist);
			_dropShadowFeather = row.GetBoolean("drop_shadow_feather", k_defaultDropShadowFeather);
			_dropShadowFeatherDist = row.GetInt("drop_shadow_feather_dist", k_defaultDropShadowFeatherDist);
			_dropShadowOpacity = row.GetInt("drop_shadow_opacity", k_defaultDropShadowOpacity);

			_backgroundBlur = row.GetBoolean("background_blur", k_defaultBackgroundBlur);
			_backgroundBlurDist = row.GetInt("background_blur_dist", k_defaultBackgroundBlurDist);

			_lastImage = row.GetString("last_image");
			_activateOnExit = row.GetBoolean("activate_on_exit", false);
			_randomGroupCount = row.GetInt("random_group_count", 1);
			_clearBetweenRandomGroups = row.GetBoolean("clear_between_random_groups", false);

			_hotKey.LoadFromSaveString(row.GetString("hot_key"));

			_historyGuid = row.GetString("history_guid");
			_historyLatestGuid = row.GetString("latest_guid");
			_filter = ImageFilters.ImageFilter.FromSaveString(row.GetString("filter_xml"));

			foreach (DataRow locRow in db.SelectDataTable("select rowid, * from location where theme_id = @theme_id", "@theme_id", _rowid).Rows)
			{
				try
				{
					var loc = new Location(locRow);
					_locations.Add(loc);
					AttachLocations(new Location[] { loc });
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when loading location from settings.");
				}
			}

			foreach (DataRow wRow in db.SelectDataTable("select rowid, * from widget where theme_id = @theme_id", "@theme_id", _rowid).Rows)
			{
				try
				{
					var widget = WidgetInstance.Load(db, wRow);
					_widgets.Add(widget);
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when loading widget from settings.");
				}
			}

			_historyCanGoPrev = db.SelectInt("select count(*) from history where theme_id = @theme_id", "@theme_id", _rowid) > 0;

			// Image rect history
			_imageRectHistory.Clear();
			foreach (DataRow rRow in db.SelectDataTable("select * from rhistory where theme_id = @id order by display_date", "@id", _rowid).Rows)
			{
				_imageRectHistory.Add(new RectangleF(rRow.GetInt("left"), rRow.GetInt("top"), rRow.GetInt("width"), rRow.GetInt("height")));
			}
		}

		public void Load(XmlElement xmlTheme)
		{
			_name = Util.ParseString(xmlTheme, "Name", Res.UnnamedTheme);

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

			_maxImageClip = Util.ParseInt(xmlTheme, "MaxImageClip", k_defaultMaxImageClip);
			if (_maxImageClip < 0) _maxImageClip = 0;
			else if (_maxImageClip > 100) _maxImageClip = 100;

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

			_activateOnExit = Util.ParseBool(xmlTheme, "ActivateOnExit", false);

			_randomGroupCount = Util.ParseInt(xmlTheme, "RandomGroupCount", 1);
			_clearBetweenRandomGroups = Util.ParseBool(xmlTheme, "ClearBetweenRandomGroups", false);

			foreach (var xmlHotKey in xmlTheme.SelectNodes(HotKey.XmlElementName).Cast<XmlElement>())
			{
				_hotKey.LoadXml(xmlHotKey);
			}

			foreach (var xmlLoc in xmlTheme.SelectNodes("Location").Cast<XmlElement>())
			{
				try
				{
					var loc = new Location(xmlLoc);
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

		public void DeleteFromDatabase(Database db)
		{
			if (_rowid == 0) return;
			db.ExecuteNonQuery("delete from theme where rowid = @id", "@id", _rowid);
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
			get { return _historyCanGoPrev; }
		}

		private void LoadHistory(XmlElement xmlTheme)
		{
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

		public void ClearHistory(Database db)
		{
            Log.Debug("Clearing history for theme '{0}'", _name);

			db.ExecuteNonQuery("delete from history where theme_id = @theme_id", "@theme_id", _rowid);
			_historyGuid = null;

            foreach (var fileName in Directory.GetFiles(Util.AppDataDir, string.Format("{0}_*", _rowid)))
            {
                try
                {
                    Log.Debug("Deleting file '{0}' for history clear.", fileName);

                    var attribs = File.GetAttributes(fileName);
                    if ((attribs & FileAttributes.ReadOnly) != 0)
                    {
                        attribs &= ~FileAttributes.ReadOnly;
                        File.SetAttributes(fileName, attribs);
                    }
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    Log.Warning("Failed to delete file '{1}' when clearing history: {0}", ex, fileName);
                }
            }

			_imageRectHistory.Clear();

			_lastImage = null;
		}

		public IEnumerable<RectangleF> ImageRectHistory
		{
			get { return _imageRectHistory; }
		}

		public void AddImageRectHistory(Database db, RectangleF rect, int numMonitors)
		{
			_imageRectHistory.Add(rect);

			db.Insert("rhistory", new object[]
				{
					"theme_id", _rowid,
					"display_date", DateTime.Now,
					"left", (int)rect.X,
					"top", (int)rect.Y,
					"width", (int)rect.Width,
					"height", (int)rect.Height
				});

			var maxHistory = k_maxImageRectHistory * numMonitors;
			if (_mode == ThemeMode.Collage) maxHistory *= _numCollageImages;

			while (_imageRectHistory.Count > maxHistory)
			{
				_imageRectHistory.RemoveAt(0);
			}

			var rowids = db.SelectLongList("select rowid from rhistory where theme_id = @theme_id order by display_date", "@theme_id", _rowid);
			while (rowids.Count > maxHistory)
			{
				db.ExecuteNonQuery("delete from rhistory where rowid = @rowid", "@rowid", rowids[0]);
				rowids.RemoveAt(0);
			}
		}

		public void ClearImageRectHistory()
		{
			_imageRectHistory.Clear();
		}
		#endregion

		#region ImageSelection
		public IEnumerable<ImageLayout> GetNextImages(Database db, Rectangle[] monitorRects,
			ref int randomGroupCounter, ref bool randomGroupClear, CancellationToken cancel)
		{
			IEnumerable<ImageLayout> ret = null;

			Log.Write(LogLevel.Debug, "Finding next images...");

			if (!string.IsNullOrEmpty(_historyGuid) && _historyGuid != _historyLatestGuid)
			{
				var lastHistoryTable = db.SelectDataTable("select * from history where theme_id = @theme_id and guid = @guid",
					"@theme_id", _rowid, "@guid", _historyGuid);
				if (lastHistoryTable.Rows.Count > 0)
				{
					var lastHistoryTime = lastHistoryTable.Rows[0].GetDateTime("display_date");
					var nextTable = db.SelectDataTable("select * from history where theme_id = @theme_id and display_date > @date order by display_date",
						"@theme_id", _rowid, "date", lastHistoryTime);
					if (nextTable.Rows.Count > 0)
					{
						// Select the images that share the same guid as the first found
						var nextGuid = nextTable.Rows[0].GetString("guid");
						var nextImages = (from i in nextTable.Rows.Cast<DataRow>()
										  where i.GetString("guid") == nextGuid
										  select ImageLayout.FromDataRow(i)).ToArray();
						_historyGuid = nextGuid;
						_historyCanGoPrev = true;
						return nextImages;
					}
				}

				// Reached the end of the history, so resume selecting new images
				_historyGuid = _historyLatestGuid;
			}

			foreach (var loc in _locations) loc.UpdateIfRequired(db, this, cancel);
			ret = PickImages(db, monitorRects, ref randomGroupCounter, ref randomGroupClear);
			AddHistoryImages(db, ret);
			return ret;
		}

		/// <summary>
		/// Creates a list of images to be displayed for all monitors.
		/// This function takes into account collage/fullscreen mode, and random/sequential order.
		/// </summary>
		/// <param name="allFiles">A list of all available image files</param>
		/// <param name="monitorRects">A list of all monitor rectangles</param>
		/// <returns>A list of images to be rendered</returns>
		private List<ImageLayout> PickImages(Database db, Rectangle[] monitorRects, ref int randomGroupCounter, ref bool randomGroupClear)
		{
			if (_mode == ThemeMode.Collage)
			{
				var pickedFiles = new List<ImageLayout>();
				var numMonitors = _separateMonitors ? monitorRects.Length : 1;
				var sequenceNumber = 0;
				var maxCount = -1;

				for (int monitorIndex = 0; monitorIndex < numMonitors; monitorIndex++)
				{
					for (int imageIndex = 0; imageIndex < _numCollageImages; imageIndex++)
					{
						var img = PickRandomOrSequentialImage(db, pickedFiles, sequenceNumber, ref randomGroupCounter, ref randomGroupClear, ref maxCount);
						if (img == null) break;
						img.Retrieve(db);

						if (_separateMonitors)
						{
							pickedFiles.Add(new ImageLayout(img, new int[] { monitorIndex }));
						}
						else
						{
							// This one image will be displayed on all the monitors.
							for (int layoutMonitor = 0; layoutMonitor < monitorRects.Length; layoutMonitor++)
							{
								pickedFiles.Add(new ImageLayout(img, new int[] { layoutMonitor }));
							}
						}

						sequenceNumber++;
					}
				}

				return pickedFiles;
			}
			else // fullscreen
			{
				var monitorSelections = (from m in monitorRects select new MonitorSelection { Rect = m, ImageRec = null }).ToArray();
				var retries = 0;
				var pickedFiles = new List<ImageLayout>();
				var sequenceNumber = 0;
				var maxCount = -1;

				while (monitorSelections.Any(m => m.ImageRec == null) && retries <= k_maxRetrievalRetries)
				{
					var img = PickRandomOrSequentialImage(db, pickedFiles, sequenceNumber, ref randomGroupCounter, ref randomGroupClear, ref maxCount);
					if (img == null) break;
					img.Retrieve(db);

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

					sequenceNumber++;
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

		private ImageRec PickRandomOrSequentialImage(Database db, List<ImageLayout> filesPickedThisTime, int sequenceNumber,
			ref int randomGroupCounter, ref bool randomGroupClear, ref int maxCount)
		{
			ImageRec img = null;
			if (_order == ThemeOrder.Random)
			{
				if (_randomGroupCount <= 1)
				{
					img = PickRandomImage(db, ref maxCount, filesPickedThisTime);
				}
				else
				{
					if (randomGroupCounter >= _randomGroupCount && sequenceNumber == 0) randomGroupCounter = 0;

					if (randomGroupCounter == 0)
					{
						Log.Debug("Picking random image because random group counter is {0}", randomGroupCounter);
						img = PickRandomImage(db, ref maxCount, filesPickedThisTime);
						randomGroupCounter++;
						if (_clearBetweenRandomGroups) randomGroupClear = true;
					}
					else if (randomGroupCounter < _randomGroupCount)
					{
						Log.Debug("Picking sequential image because random group counter is {0}", randomGroupCounter);
						img = PickSequentialImage(db, filesPickedThisTime);
						randomGroupCounter++;
					}
					else
					{
						Log.Debug("Picking no image because random group counter {0} has exceeded limit {1}", randomGroupCounter, _randomGroupCount);
					}
				}
			}
			else // sequential
			{
				img = PickSequentialImage(db, filesPickedThisTime);
			}

			if (img != null) _lastImage = img.Location;
			return img;
		}

		private ImageRec PickRandomImage(Database db, ref int maxCount, List<ImageLayout> filesPickedThisTime)
		{
			var repeatNowRetries = 0;
			var repeatHistoryRetries = 0;
			var loadRetries = 0;

			var filterClause = string.Empty;
			if (_filter != null)
			{
				var f = _filter.GenerateSqlWhere();
				if (!string.IsNullOrEmpty(f)) filterClause = string.Concat(" and ", f);
			}

			if (maxCount < 0)
			{
				var sql = new StringBuilder();
				sql.Append("select count(*) from img");
				sql.Append(" inner join location on location.rowid = img.location_id");
				sql.Append(" where img.theme_id = @id");
				sql.Append(" and location.disabled = 0");
				sql.Append(filterClause);

				maxCount = db.SelectInt(sql.ToString(), "@id", _rowid);
				Log.Debug("Number of images in database: {0}", maxCount);
			}
			if (maxCount == 0) return null;

			while (true)
			{
				var index = _rand.Next(maxCount);
				Log.Debug("Selecting image index {0} of {1}", index, maxCount);

				var sql = new StringBuilder();
				sql.Append("select img.rowid, img.* from img");
				sql.Append(" inner join location on location.rowid = img.location_id");
				sql.Append(" where img.theme_id = @id");
				sql.Append(" and location.disabled = 0");
				sql.Append(filterClause);
				sql.Append(" limit 1 offset @num");

				var table = db.SelectDataTable(sql.ToString(), "@id", _rowid, "@num", index);
				if (table.Rows.Count > 0)
				{
					var img = ImageRec.FromDataRow(table.Rows[0]);

					if (filesPickedThisTime.Any(i => i.ImageRec == img))
					{
						repeatNowRetries++;
						if (repeatNowRetries <= k_maxRepeatNowRetries) continue;
					}
					repeatNowRetries = 0;

					if (ImageRecIsInHistory(db, img))
					{
						repeatHistoryRetries++;
						if (repeatHistoryRetries <= k_maxRepeatHistoryRetries) continue;
					}
					repeatHistoryRetries = 0;

					if (img.Retrieve(db)) return img;
				}
				else return null;

				loadRetries++;
				if (loadRetries > k_maxRetrievalRetries) break;
			}

			return null;
		}

		private ImageRec PickSequentialImage(Database db, List<ImageLayout> filesPickedThisTime)
		{
			Log.Write(LogLevel.Debug, "Picking sequential image (last image = [{0}])", _lastImage);

			DataTable table;
			var sql = new StringBuilder();

			var filterClause = string.Empty;
			if (_filter != null)
			{
				var f = _filter.GenerateSqlWhere();
				if (!string.IsNullOrEmpty(f)) filterClause = string.Concat(" and ", f);
			}

			if (string.IsNullOrEmpty(_lastImage))
			{
				sql.Append("select img.rowid, img.* from img");
				sql.Append(" inner join location on location.rowid = img.location_id");
				sql.Append(" where img.theme_id = @id");
				sql.Append(" and location.disabled = 0");
				sql.Append(filterClause);
				sql.Append(" order by img.path limit 1");

				table = db.SelectDataTable(sql.ToString(), "@id", _rowid);
				if (table.Rows.Count > 0)
				{
					var img = ImageRec.FromDataRow(table.Rows[0]);
					Log.Debug("No 'last image' was saved, so returning the first image in the database: {0}", img.Location);
					return img;
				}
				else
				{
					Log.Debug("No 'last image' was saved, and there are no images in the database.");
					return null;
				}
			}

			// Find the first image that is greater than the last file.
			sql.Append("select img.rowid, img.* from img");
			sql.Append(" inner join location on location.rowid = img.location_id");
			sql.Append(" where img.theme_id = @id");
			sql.Append(" and img.path > @path");
			sql.Append(" and location.disabled = 0");
			sql.Append(filterClause);
			sql.Append(" order by img.path limit 1");

			table = db.SelectDataTable(sql.ToString(), "@id", _rowid, "@path", _lastImage);
			if (table.Rows.Count > 0)
			{
				var img = ImageRec.FromDataRow(table.Rows[0]);
				Log.Write(LogLevel.Debug, "Next sequential image is [{0}]", img.Location);
				return img;
			}
			else
			{
				// This is the last image for this theme. Select the first one in the database.
				sql.Clear();
				sql.Append("select img.rowid,img. * from img");
				sql.Append(" inner join location on location.rowid = img.location_id");
				sql.Append(" where img.theme_id = @id");
				sql.Append(" and location.disabled = 0");
				sql.Append(filterClause);
				sql.Append(" order by img.path limit 1");

				table = db.SelectDataTable(sql.ToString(), "@id", _rowid);
				if (table.Rows.Count > 0)
				{
					var img = ImageRec.FromDataRow(table.Rows[0]);
					Log.Debug("No next sequential image, so wrapping around to beginning: {0}", img.Location);
					return img;
				}
				else
				{
					Log.Debug("No next sequential image, and nothing left in the database.");
					return null;
				}
			}
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
					Log.Debug("Testing monitor selection: start [{0}] count [{1}]", startMonitor, numMonitors);

					if (!MonitorsAreAdjascent(monitorSelections, startMonitor, numMonitors))
					{
						Log.Debug("Discarding monitor selection because monitors are not adjascent.");
						continue;
					}

					var monitors = monitorSelections.Skip(startMonitor).Take(numMonitors).ToArray();
					var envelope = (from m in monitors select m.Rect).GetEnvelope();
					var aspect = envelope.Size.GetAspect();
					var aspectDiff = Math.Abs(aspect - imageAspect);

					if (monitors.Any(m => m.ImageRec != null))
					{
						if (bestUsedStart == -1 || aspectDiff < bestUsedAspectDiff)
						{
							if (CheckSpanImageClip(monitors, envelope, imgSize))
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
					}
					else
					{
						if (bestUnusedStart == -1 || aspectDiff < bestUnusedAspectDiff)
						{
							if (CheckSpanImageClip(monitors, envelope, imgSize))
							{
								bestUnusedStart = startMonitor;
								bestUnusedCount = numMonitors;
								bestUnusedAspectDiff = aspectDiff;
							}
						}
					}

					Log.Debug("Monitor selection: aspect [{0}] aspectDiff [{1}]", aspect, aspectDiff);
				}
			}

			var displacementAllowed = _order != ThemeOrder.Sequential;

			if (bestUnusedStart != -1)
			{
				if (displacementAllowed &&
					bestUsedStart != -1 &&
					bestUsedAspectDiff < bestUnusedAspectDiff &&
					Math.Abs(bestUsedAspectDiff - bestUnusedAspectDiff) > k_aspectThreshold &&
					bestUsedCount > 1)				// Only allow displacement when occupying more than 1 monitor.
				{
					// There's a better fit if we displace a previously selected image.
					Log.Debug("Using monitor range start [{0}] count [{1}] (displaced previously picked images)", bestUsedStart, bestUsedCount);
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
					Log.Debug("Using monitor range start [{0}] count [{1}] (unused range)", bestUnusedStart, bestUnusedCount);
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
				Log.Debug("Unable to find monitor range.");
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
		/// When spanning an image across multiple monitors, ensure that it doesn't clip too much of the image.
		/// </summary>
		private bool CheckSpanImageClip(MonitorSelection[] monitors, Rectangle envelope, Size imageSize)
		{
			if (monitors.Length == 1) return true;	// Don't check clipping for single-monitor display

			var envelopeArea = envelope.Area();

			var imageRectF = new RectangleF(0, 0, imageSize.Width, imageSize.Height);
			imageRectF = WallpaperRenderer.FitFullScreenImage(imageRectF, envelope, imageSize, this);
			var imageRect = imageRectF.ToRectangle();

			int clipArea = 0;
			foreach (var monitor in monitors)
			{
				clipArea += monitor.Rect.IntersectArea(imageRect);
			}

			var imageArea = imageRect.Area();

			float areaDiff = ((float)clipArea / (float)imageArea) * 100.0f;
			if (areaDiff >= 100.0f - _maxImageClip)
			{
				Log.Debug("Image clip check passed: areaDiff [{0}]% envelopeArea [{1}] clipArea [{2}] imageArea [{3}] imageRect [{4}] envelope [{5}]", areaDiff, envelopeArea, clipArea, imageArea, imageRect, envelope);
				return true;
			}
			else
			{
				Log.Debug("Image clip check failed: areaDiff [{0}]% envelopeArea [{1}] clipArea [{2}] imageArea [{3}] imageRect [{4}] envelope [{5}]", areaDiff, envelopeArea, clipArea, imageArea, imageRect, envelope);
				return false;
			}
		}

		private bool ImageRecIsInHistory(Database db, ImageRec img)
		{
			return db.SelectInt("select count(*) from history where theme_id = @theme_id and path = @path",
				"@theme_id", _rowid, "@path", img.Location) > 0;
		}

		public IEnumerable<ImageLayout> GetPrevImages(Database db)
		{
			Log.Write(LogLevel.Debug, "Going back to previous images.");
			if (!string.IsNullOrEmpty(_historyGuid))
			{
				var lastHistoryTable = db.SelectDataTable("select * from history where theme_id = @theme_id and guid = @guid",
					"@theme_id", _rowid, "@guid", _historyGuid);
				if (lastHistoryTable.Rows.Count > 0)
				{
					var lastHistoryTime = lastHistoryTable.Rows[0].GetDateTime("display_date");
					var nextTable = db.SelectDataTable("select * from history where theme_id = @theme_id and display_date < @date order by display_date desc",
						"@theme_id", _rowid, "date", lastHistoryTime);
					if (nextTable.Rows.Count > 0)
					{
						// Select the images that share the same guid as the first found
						var nextGuid = nextTable.Rows[0].GetString("guid");
						var nextImages = (from i in nextTable.Rows.Cast<DataRow>()
										  where i.GetString("guid") == nextGuid
										  select ImageLayout.FromDataRow(i)).ToArray();
						_historyGuid = nextGuid;
						_historyCanGoPrev = db.SelectInt("select count(*) from history where theme_id = @theme_id and display_date < @date",
							"@theme_id", _rowid, "@date", nextTable.Rows[0].GetDateTime("display_date")) > 0;
						return nextImages;
					}
				}

				_historyCanGoPrev = false;
				return null;
			}
			else
			{
				// Currently in 'new' mode, go to the last images displayed
				var lastGuid = db.SelectString("select guid from history where theme_id = @theme_id order by display_date desc limit 1", "@theme_id", _rowid);
				if (!string.IsNullOrEmpty(lastGuid))
				{
					var lastTable = db.SelectDataTable("select * from history where theme_id = @theme_id and guid = @guid", "@theme_id", _rowid, "@guid", lastGuid);
					_historyCanGoPrev = db.SelectStringList("select distinct guid from history where theme_id = @theme_id", "@theme_id", _rowid).Count > 1;
					_historyGuid = lastGuid;
					return (from i in lastTable.Rows.Cast<DataRow>() select ImageLayout.FromDataRow(i)).ToArray();
				}

				// No history in the database
				_historyCanGoPrev = false;
				return null;
			}
		}

		private void AddHistoryImages(Database db, IEnumerable<ImageLayout> images)
		{
			var guid = Guid.NewGuid().ToString();
			var now = DateTime.Now;

			using (var tran = db.BeginTransaction())
			{
				foreach (var imgLayout in images)
				{
					db.Insert("history", new object[]
						{
							"theme_id", _rowid,
							"display_date", now,
							"guid", guid,
							"monitors", imgLayout.GetMonitorsSaveString(),
							"type", imgLayout.ImageRec.Type.ToString(),
							"path", imgLayout.ImageRec.Location,
							"pub_date", imgLayout.ImageRec.PubDate.HasValue ? (object)imgLayout.ImageRec.PubDate.Value : null,
							"rating", imgLayout.ImageRec.Rating,
							"thumb", imgLayout.ImageRec.Thumbnail?.Data
						});
				}

				tran.Commit();
			}

			var allGuids = db.SelectStringList("select distinct guid from history where theme_id = @theme_id order by display_date",
				"@theme_id", _rowid);
			while (allGuids.Count > k_maxHistory)
			{
				db.ExecuteNonQuery("delete from history where theme_id = @theme_id and guid = @guid",
					"@theme_id", _rowid, "@guid", allGuids[0]);
				allGuids.RemoveAt(allGuids.Count - 1);
			}

			_historyCanGoPrev = true;
			_historyLatestGuid = guid;
			_historyGuid = guid;

			FireHistoryAdded(images);
		}
		#endregion

		#region Wallpaper
		private void OnActivate(bool active)
		{
		}

		public string GetBaseWallpaperFileName(ImageFormat format, DateTime timeStamp)
		{
            var fileName = string.Format("{0}_{2:yyyyMMddHHmmss}_base{1}", _rowid, ImageFormatDesc.ImageFormatToExtension(format), timeStamp);
			return Path.Combine(Util.AppDataDir, fileName);
		}

		public string GetDisplayWallpaperFileName(ImageFormat format, DateTime timeStamp)
		{
            var fileName = string.Format("{0}_{2:yyyyMMddHHmmss}{1}", _rowid, ImageFormatDesc.ImageFormatToExtension(format), timeStamp);
			return Path.Combine(Util.AppDataDir, fileName);
		}

        public string FindLastWallpaperFileName()
        {
            Log.Debug("Finding last wallpaper file name...");

            foreach (var fileName in Directory.GetFiles(Util.AppDataDir, string.Format("{0}_*", _rowid)).OrderByDescending(x => x.ToLower()))
            {
                if (ImageFormatDesc.FileNameToImageFormat(fileName) != null)
                {
                    Log.Debug("  File name appears to be supported: {0}", fileName);
                    return fileName;
                }
                else
                {
                    Log.Debug("  File name is not a supported image: {0}", fileName);
                }
            }

            return null;
        }

        public void PurgeOldWallpaperFiles(IEnumerable<string> currentFileNames)
        {
            foreach (var fileName in Directory.GetFiles(Util.AppDataDir, string.Format("{0}_*", _rowid)))
            {
                // Check that this is not the current image.
                var keep = false;
                foreach (var curFileName in currentFileNames)
                {
                    if (fileName.Length >= curFileName.Length &&
                        string.Equals(fileName.Substring(0, curFileName.Length), curFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        keep = true;
                        break;
                    }
                }

                if (!keep)
                {
                    try
                    {
                        Log.Debug("Purging old wallpaper file: {0}", fileName);

                        var attribs = File.GetAttributes(fileName);
                        if ((attribs & FileAttributes.ReadOnly) != 0)
                        {
                            attribs &= ~FileAttributes.ReadOnly;
                            File.SetAttributes(fileName, attribs);
                        }
                        File.Delete(fileName);
                    }
                    catch (Exception ex)
                    {
                        Log.Warning("Error when purging old wallpaper file '{0}': {1}", fileName, ex);
                    }
                }
            }
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
