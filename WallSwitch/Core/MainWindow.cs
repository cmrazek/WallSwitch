using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WallSwitch.ImageFilters;
using WallSwitch.Themes;
using WallSwitch.SettingsStore;

namespace WallSwitch
{
	internal partial class MainWindow : Form
	{
		#region Variables
		public static MainWindow Current;

		private List<Theme> _themes = new List<Theme>();
		private Theme _currentTheme;
		private bool _reallyClose;
		private bool _refreshing;
		private bool _dirty;
		//private ImageList _locationImages = new ImageList();		TODO: remove
		private bool _winStart;
		private HotKey _changeThemeHotKey = new HotKey();
		private Dictionary<Location, LocationBrowser> _locationBrowsers = new Dictionary<Location, LocationBrowser>();

		private delegate void VoidDelegate();
		private VoidDelegate _appActivateFunc;
		
		private EventHandler _balloonClickedHandler;
		#endregion

		#region Constants
		// Items in theme period combo
		private const int k_periodSeconds = 0;
		private const int k_periodMinutes = 1;
		private const int k_periodHours = 2;
		private const int k_periodDays = 3;

		// Items in theme mode combo
		private const int k_modeFullScreen = 0;
		private const int k_modeCollage = 1;

		// Items in theme order combo
		private const int k_orderSequential = 0;
		private const int k_orderRandom = 1;

		// Hotkeys
		private const int k_hotkeySwitchNext = 1;

		private const string k_nextUpdateFormat = "t";	// Uses "h:mm tt" format

		private const int k_minUpdateSeconds = 15;

		private const int k_groupBoxMargin = 8;
		private const double k_minTransparency = 0.1;

		// Location Image List
		private const int k_locimgFolder = 0;
		private const int k_locimgFeed = 1;
		private const int k_locimgImageFile = 2;
		#endregion

		#region Window Management
		public MainWindow(bool winStart)
		{
			MainWindow.Current = this;

			if (_winStart = winStart) HideToTray();
			InitializeComponent();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					LoadSettings(db);
					if (Settings.LoadHistoryImages) LoadHistoryFromDatabase(db);

					// Determine which theme is the currently active theme
					Theme activeTheme;
					if (_themes.Count == 0)
					{
						_currentTheme = new Theme();
						_themes.Add(_currentTheme);
						activeTheme = _currentTheme;
						_currentTheme.IsActive = true;
					}
					else
					{
						activeTheme = GetActiveTheme();
						if (!activeTheme.IsActive) activeTheme.IsActive = true;
						_currentTheme = activeTheme;
					}

					c_activateThemeHotKey.HotKey = _changeThemeHotKey;

					// Start the switch thread
					var switchThread = new SwitchThread();
					switchThread.SetStartUpDelay();
					switchThread.Start(db, activeTheme);
					switchThread.Switching += new SwitchThread.SwitchEventHandler(SwitchThread_Switching);
					switchThread.Switched += new SwitchThread.SwitchEventHandler(SwitchThread_Switched);
					Program.SwitchThread = switchThread;

					cmbColorEffectFore.InitForEnum<ColorEffect>(ColorEffect.None);
					cmbColorEffectBack.InitForEnum<ColorEffect>(ColorEffect.None);

					c_edgeMode.InitForEnum<EdgeMode>(EdgeMode.Feather);
				}

				// Update all the controls
				PopulateWidgetCombo();
				RefreshControls();
				Text = Res.AppName;
				trayIcon.Visible = true;
				EnableControls();
				Locations_Resize(this, null);

				UpdateTransparency();

				if (_winStart) HideToTray();

				RegisterHotKeys();

				if (Settings.CheckForUpdatesOnStartup) CheckForUpdates();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				Log.Write(LogLevel.Debug, "Main window closed.");

				using (var db = new Database())
				{
					SaveSettings(db);
					UnregisterHotKeys();

					// If the user has an 'exit theme' set, then switch to that theme.
					if (Program.SwitchThread != null)
					{
						var exitTheme = MainWindow.Current.Themes.FirstOrDefault(t => t.ActivateOnExit);
						if (exitTheme != null && exitTheme != Program.SwitchThread.Theme)
						{
							Log.Write(LogLevel.Info, "Switching to theme '{0}' on exit...", exitTheme.Name);
							int randomGroupCounter = Program.SwitchThread.RandomGroupCounter;
							var cancel = new CancellationTokenSource();
							Task.WaitAny(
								Task.Run(() =>
								{
									Program.SwitchThread.WallpaperSetter.Set(db, exitTheme, SwitchDir.Next, true, ref randomGroupCounter, cancel.Token);
								}),
								Task.Delay(3000));
							cancel.Cancel();
						}
						else
						{
							Log.Debug("No exit theme is set.");
						}
					}
					else
					{
						Log.Write(LogLevel.Warning, "On application exit, the switch thread or main window is null.");
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when form closed.");
			}
		}

		private void Shutdown(bool closeWindow)
		{
			_reallyClose = true;
			if (closeWindow) Close();
		}

		private void cmExit_Click(object sender, EventArgs e)
		{
			try
			{
				Shutdown(true);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miFileExit_Click(object sender, EventArgs e)
		{
			try
			{
				Shutdown(true);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void cmShowMainWindow_Click(object sender, EventArgs e)
		{
			try
			{
				ShowFromTray();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void TrayIcon_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				ShowFromTray();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Log.Write(LogLevel.Debug, "Received form closing event: {0} ", e.CloseReason);
				Log.Flush();

				if (!_reallyClose && e.CloseReason == CloseReason.UserClosing)
				{
					var hideWindow = true;
					if (Dirty)
					{
						switch (MessageBox.Show(this, Res.Confirm_HideWindowDirtyTheme,
							Res.Confirm_HideWindowDirtyThemeCaption, MessageBoxButtons.YesNoCancel,
							MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
						{
							case DialogResult.Yes:
								if (!SaveControls(true)) hideWindow = false;
								break;
							case DialogResult.No:
								break;
							case DialogResult.Cancel:
								hideWindow = false;
								break;
						}
					}

					e.Cancel = true;
					if (hideWindow)
					{
						WindowState = FormWindowState.Minimized;
						//SaveSettings();
					}
				}
				else
				{
					Shutdown(false);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void MainWindow_Resize(object sender, EventArgs e)
		{
			try
			{
				if (WindowState == FormWindowState.Minimized) HideToTray();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private bool Dirty
		{
			get { return _dirty; }
			set
			{
				_dirty = value;
				Text = Res.AppName + (_dirty ? "*" : "");
				EnableControls();
			}
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == NativeMethods.WM_HOTKEY)
			{
				try
				{
					HotKey.OnWmHotKey(m.WParam.ToInt32());
				}
				catch (Exception ex)
				{
					this.ShowError(ex, Res.Exception_Generic);
				}
			}
			else if (m.Msg == NativeMethods.WM_SHOWME)
			{
				try
				{
					AppActivate();
				}
				catch (Exception ex)
				{
					this.ShowError(ex, Res.Exception_Generic);
				}
			}
		}
		#endregion

		#region Hide To Tray
		private void HideToTray()
		{
			WindowState = FormWindowState.Minimized;
			Visible = false;
		}

		private void ShowFromTray()
		{
			if (IsDisposed) return;

			Visible = true;
			WindowState = FormWindowState.Normal;

			NativeMethods.BringWindowToTop(this.Handle);

			var oldTopMost = TopMost;
			TopMost = true;
			TopMost = oldTopMost;

			// Select the active theme.
			Theme activeTheme = GetActiveTheme();
			if (activeTheme != null && !activeTheme.Equals(_currentTheme)) _currentTheme = activeTheme;

			RefreshControls();
		}

		public void AppActivate()
		{
			if (IsDisposed) return;

			Log.Debug("Received activate message.");

			if (InvokeRequired)
			{
				if (_appActivateFunc == null) _appActivateFunc = new VoidDelegate(AppActivate);
				BeginInvoke(_appActivateFunc);
				return;
			}

			ShowFromTray();
			NativeMethods.SetForegroundWindow(this.Handle);
		}

		public void ShowNotification(string message, EventHandler clickCallback = null)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => { ShowNotification(message, clickCallback); }));
				return;
			}

			if (!string.IsNullOrWhiteSpace(message))
			{
				trayIcon.BalloonTipText = message;
				trayIcon.BalloonTipTitle = Res.Notify_Title;
				trayIcon.BalloonTipIcon = ToolTipIcon.None;
				trayIcon.ShowBalloonTip(1);

				if (_balloonClickedHandler != null)
				{
					trayIcon.BalloonTipClicked -= _balloonClickedHandler;
					_balloonClickedHandler = null;
				}

				if (clickCallback != null)
				{
					_balloonClickedHandler = clickCallback;
					trayIcon.BalloonTipClicked += _balloonClickedHandler;
				}
			}
		}
		#endregion

		#region Controls
		private void RefreshControls()
		{
			_refreshing = true;

			// Theme list
			bool themeSelected = false;
			Theme activeTheme = null;
			int activeIndex = -1;

			cmbTheme.Items.Clear();
			foreach (Theme theme in _themes)
			{
				string name;
				if (theme.IsActive) name = String.Format(Res.ActiveTheme, theme.Name);
				else name = theme.Name;
				int index = cmbTheme.Items.Add(new TagString(name, theme));

				if (theme.Equals(_currentTheme))
				{
					cmbTheme.SelectedIndex = index;
					themeSelected = true;
				}

				if (theme.IsActive)
				{
					activeTheme = theme;
					activeIndex = index;
				}
			}

			if (!themeSelected)
			{
				if (activeTheme != null)
				{
					// Pick the active theme
					_currentTheme = activeTheme;
					cmbTheme.SelectedIndex = activeIndex;
				}
				else if (cmbTheme.Items.Count > 0)
				{
					// Pick the first theme in the list
					cmbTheme.SelectedIndex = 0;
					_currentTheme = (Theme)((TagString)cmbTheme.Items[0]).Tag;
				}
				else throw new InvalidOperationException(Res.Exception_SelectNewCurrentTheme);
			}

			// Theme frequency / period
			txtThemeFreq.Text = _currentTheme.Frequency.ToString();

			switch (_currentTheme.Period)
			{
				case Period.Seconds:
					cmbThemePeriod.SelectedIndex = k_periodSeconds;
					break;
				case Period.Minutes:
					cmbThemePeriod.SelectedIndex = k_periodMinutes;
					break;
				case Period.Hours:
					cmbThemePeriod.SelectedIndex = k_periodHours;
					break;
				case Period.Days:
					cmbThemePeriod.SelectedIndex = k_periodDays;
					break;
				default:
					cmbThemePeriod.SelectedIndex = k_periodMinutes;
					break;
			}

			// Theme mode

			switch(_currentTheme.Mode)
			{
				case ThemeMode.FullScreen:
					c_themeMode.SelectedIndex = k_modeFullScreen;
					break;
				case ThemeMode.Collage:
					c_themeMode.SelectedIndex = k_modeCollage;
					break;
				default:
					c_themeMode.SelectedIndex = k_modeFullScreen;
					break;
			}

			// Theme order

			switch (_currentTheme.Order)
			{
				case ThemeOrder.Sequential:
					c_themeOrder.SelectedIndex = k_orderSequential;
					break;
				case ThemeOrder.Random:
					c_themeOrder.SelectedIndex = k_orderRandom;
					break;
				default:
					c_themeOrder.SelectedIndex = k_orderRandom;
					break;
			}

			// Background Colors
			clrBackTop.Color = _currentTheme.BackColorTop;
			clrBackBottom.Color = _currentTheme.BackColorBottom;

			// Image Size
			trkImageSize.Value = _currentTheme.ImageSize.Clamp(trkImageSize.Minimum, trkImageSize.Maximum);
			UpdateImageSizeDisplay();

			c_separateMonitors.Checked = _currentTheme.SeparateMonitors;
			c_allowSpanning.Checked = _currentTheme.AllowSpanning;
			c_maxClipTrackBar.Value = _currentTheme.MaxImageClip;
			UpdateMaxClipPercent();
			_changeThemeHotKey.Copy(_currentTheme.HotKey);

			c_imageFit.SelectedIndex = (int)_currentTheme.ImageFit;

			trkOpacity.Value = _currentTheme.BackOpacity.Clamp(trkOpacity.Minimum, trkOpacity.Maximum);
			UpdateBackOpacityDisplay();

			c_edgeMode.SetEnumValue<EdgeMode>(_currentTheme.EdgeMode);
			c_edgeDist.Value = _currentTheme.EdgeDist.Clamp(c_edgeDist.Minimum, c_edgeDist.Maximum);
			c_borderColor.Color = _currentTheme.BorderColor;
			UpdateFeatherDisplay();

			chkFadeTransition.Checked = _currentTheme.FadeTransition;

			if (_currentTheme.MaxImageScale > 0)
			{
				c_limitScale.Checked = true;
				c_maxScale.Text = _currentTheme.MaxImageScale.ToString();
			}
			else
			{
				c_limitScale.Checked = false;
				c_maxScale.Text = string.Empty;
			}

			cmbColorEffectFore.SetEnumValue<ColorEffect>(_currentTheme.ColorEffectFore);
			cmbColorEffectBack.SetEnumValue<ColorEffect>(_currentTheme.ColorEffectBack);
			trkColorEffectCollageFadeRatio.Value = _currentTheme.ColorEffectBackRatio.Clamp(trkColorEffectCollageFadeRatio.Minimum, trkColorEffectCollageFadeRatio.Maximum);

			chkDropShadow.Checked = _currentTheme.DropShadow;
			trkDropShadow.Value = _currentTheme.DropShadowDist.Clamp(trkDropShadow.Minimum, trkDropShadow.Maximum);
			chkDropShadowFeather.Checked = _currentTheme.DropShadowFeather;
			lblDropShadowUnit.Text = string.Format(Res.DropShadowDist, trkDropShadow.Value);
			trkDropShadowFeatherDist.Value = _currentTheme.DropShadowFeatherDist.Clamp(trkDropShadowFeatherDist.Minimum, trkDropShadowFeatherDist.Maximum);
			lblDropShadowFeatherDist.Text = string.Format(Res.DropShadowFeatherDistValue, trkDropShadowFeatherDist.Value);
			trkDropShadowOpacity.Value = _currentTheme.DropShadowOpacity.Clamp(trkDropShadowOpacity.Minimum, trkDropShadowOpacity.Maximum);
			lblDropShadowOpacityValue.Text = string.Format(Res.DropShadowOpacityValue, trkDropShadowOpacity.Value);

			chkBackgroundBlur.Checked = _currentTheme.BackgroundBlur;
			trkBackgroundBlurDist.Value = _currentTheme.BackgroundBlurDist.Clamp(trkBackgroundBlurDist.Minimum, trkBackgroundBlurDist.Maximum);
			lblBackgroundBlurDistValue.Text = string.Format(Res.BackgroundBlurDist, trkBackgroundBlurDist.Value);

			c_numCollageImages.Text = _currentTheme.NumCollageImages.ToString();
			c_widgetLayout.LoadFromTheme(_currentTheme);
			c_activateOnExitCheckBox.Checked = _currentTheme.ActivateOnExit;

			if (_currentTheme.RandomGroupCount > 1)
			{
				c_randomGroup.Checked = true;
				c_randomGroupCount.Text = _currentTheme.RandomGroupCount.ToString();
				c_clearBetweenRandomGroups.Checked = _currentTheme.ClearBetweenRandomGroups;
			}
			else
			{
				c_randomGroup.Checked = false;
				c_randomGroupCount.Text = "1";
				c_clearBetweenRandomGroups.Checked = false;
			}

			RefreshTransparency();
			RefreshFilter();

			Dirty = false;

			RefreshLocations();
			RefreshWidgetList();
			EnableControls();

			_refreshing = false;
		}

		private bool SaveControls(bool showErrors)
		{
			if (_currentTheme == null) return false;

			int freq;
			if (!Int32.TryParse(txtThemeFreq.Text, out freq) || freq <= 0 || freq > 99999)
			{
				if (showErrors)
				{
					c_themeTabControl.SelectedTab = c_settingsTab;
					txtThemeFreq.Focus();
					this.ShowError(Res.Error_InvalidThemeFreq);
				}
				return false;
			}

			Period period = Period.Minutes;
			switch(cmbThemePeriod.SelectedIndex)
			{
				case k_periodSeconds:
					period = Period.Seconds;
					break;
				case k_periodMinutes:
					period = Period.Minutes;
					break;
				case k_periodHours:
					period = Period.Hours;
					break;
				case k_periodDays:
					period = Period.Days;
					break;
				default:
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_settingsTab;
						cmbThemePeriod.Focus();
						this.ShowError(Res.Error_InvalidThemePeriod);
					}
					return false;
			}

			if (period == Period.Seconds && freq < k_minUpdateSeconds)
			{
				if (showErrors)
				{
					c_themeTabControl.SelectedTab = c_settingsTab;
					txtThemeFreq.Focus();
					this.ShowError(Res.Error_ShortUpdateInterval);
				}
				return false;
			}

			ThemeMode mode = ThemeMode.FullScreen;
			switch(c_themeMode.SelectedIndex)
			{
				case k_modeFullScreen:
					mode = ThemeMode.FullScreen;
					break;
				case k_modeCollage:
					mode = ThemeMode.Collage;
					break;
				default:
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_settingsTab;
						c_themeMode.Focus();
						this.ShowError(Res.Error_InvalidThemeMode);
					}
					return false;
			}

			ThemeOrder order = ThemeOrder.Random;
			switch (c_themeOrder.SelectedIndex)
			{
				case k_orderSequential:
					order = ThemeOrder.Sequential;
					break;
				case k_orderRandom:
					order = ThemeOrder.Random;
					break;
				default:
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_settingsTab;
						c_themeOrder.Focus();
						this.ShowError(Res.Error_InvalidThemeOrder);
					}
					return false;
			}

			int maxImageScale = 0;
			if (c_limitScale.Checked)
			{
				if (!int.TryParse(c_maxScale.Text, out maxImageScale))
				{
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_settingsTab;
						c_maxScale.Focus();
						this.ShowError(Res.Error_InvalidMaxImageScale);
					}
					return false;
				}
				else if (maxImageScale <= 0)
				{
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_settingsTab;
						c_maxScale.Focus();
						this.ShowError(Res.Error_NegativeMaxImageScale);
					}
				}
			}

			int numCollageImages = Theme.k_defaultNumCollageImages;
			if (!int.TryParse(c_numCollageImages.Text, out numCollageImages))
			{
				if (showErrors)
				{
					c_themeTabControl.SelectedTab = c_settingsTab;
					c_numCollageImages.Focus();
					this.ShowError(Res.Error_InvalidNumCollageImages);
				}
				return false;
			}
			else if (numCollageImages <= 0 || numCollageImages > Theme.k_maxNumCollageImages)
			{
				if (showErrors)
				{
					c_themeTabControl.SelectedTab = c_settingsTab;
					c_numCollageImages.Focus();
					this.ShowError(Res.Error_OutOfRangeNumCollageImages);
				}
				return false;
			}

			int randomGroupCount;
			bool clearBetweenRandomGroups;
			if (c_randomGroup.Checked)
			{
				if (!int.TryParse(c_randomGroupCount.Text, out randomGroupCount) || randomGroupCount < 1)
				{
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_settingsTab;
						c_randomGroupCount.Focus();
						this.ShowError(Res.Error_InvalidRandomGroupCount);
					}
					return false;
				}

				clearBetweenRandomGroups = c_clearBetweenRandomGroups.Checked;
			}
			else
			{
				randomGroupCount = 1;
				clearBetweenRandomGroups = false;
			}

			ImageFilter filter;
			if (!SaveFilter(showErrors, out filter))
			{
				return false;
			}

			_currentTheme.Frequency = freq;
			_currentTheme.Period = period;
			_currentTheme.Mode = mode;
			_currentTheme.Order = order;
			_currentTheme.BackColorTop = clrBackTop.Color;
			_currentTheme.BackColorBottom = clrBackBottom.Color;
			_currentTheme.ImageSize = trkImageSize.Value;

			var locationList = new List<Location>();
			foreach (var lvi in lstLocations.Items.Cast<ListViewItem>())
			{
				var location = lvi.Tag as Location;
				location.Disabled = !lvi.Checked;
				locationList.Add(location);
			}
			_currentTheme.Locations = locationList;

			_currentTheme.SeparateMonitors = c_separateMonitors.Checked;
			_currentTheme.AllowSpanning = c_allowSpanning.Checked;
			_currentTheme.MaxImageClip = c_maxClipTrackBar.Value;
			if (!_currentTheme.HotKey.Equals(_changeThemeHotKey))
			{
				// Hot key is changing. Need to reregister.
				_currentTheme.HotKey = _changeThemeHotKey;
			}
			else
			{
				_currentTheme.HotKey = _changeThemeHotKey;
			}

			_currentTheme.ImageFit = (ImageFit)c_imageFit.SelectedIndex;
			_currentTheme.BackOpacity = trkOpacity.Value;
			_currentTheme.FadeTransition = chkFadeTransition.Checked;
			_currentTheme.MaxImageScale = maxImageScale;
			_currentTheme.NumCollageImages = numCollageImages;

			_currentTheme.ColorEffectFore = cmbColorEffectFore.GetEnumValue<ColorEffect>();
			_currentTheme.ColorEffectBack = cmbColorEffectBack.GetEnumValue<ColorEffect>();
			_currentTheme.ColorEffectBackRatio = trkColorEffectCollageFadeRatio.Value;

			_currentTheme.EdgeMode = c_edgeMode.GetEnumValue<EdgeMode>();
			_currentTheme.EdgeDist = c_edgeDist.Value;
			_currentTheme.BorderColor = c_borderColor.Color;

			_currentTheme.DropShadow = chkDropShadow.Checked;
			_currentTheme.DropShadowDist = trkDropShadow.Value;
			_currentTheme.DropShadowFeather = chkDropShadowFeather.Checked;
			_currentTheme.DropShadowFeatherDist = trkDropShadowFeatherDist.Value;
			_currentTheme.DropShadowOpacity = trkDropShadowOpacity.Value;

			_currentTheme.BackgroundBlur = chkBackgroundBlur.Checked;
			_currentTheme.BackgroundBlurDist = trkBackgroundBlurDist.Value;

			_currentTheme.RandomGroupCount = randomGroupCount;
			_currentTheme.ClearBetweenRandomGroups = clearBetweenRandomGroups;

			_currentTheme.Filter = filter;

			c_widgetLayout.SaveToTheme(_currentTheme);

			// Only 1 theme should be set as 'activate on exit' at a time.
			if (c_activateOnExitCheckBox.Checked)
			{
				foreach (var theme in _themes)
				{
					theme.ActivateOnExit = theme == _currentTheme;
				}
			}
			else
			{
				_currentTheme.ActivateOnExit = false;
			}

			Dirty = false;
			EnableControls();

			return true;
		}

		private void RefreshLocations()
		{
			lstLocations.Items.Clear();

			foreach (var loc in _currentTheme.Locations) AddLocationItem(loc);
		}

		private void Apply_Click(object sender, EventArgs e)
		{
			try
			{
				if (SaveControls(true))
				{
					using (var db = new Database())
					{
						SaveSettings(db);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{
			Apply_Click(sender, e);
		}

		private void ControlChanged(object sender, EventArgs e)
		{
			if (!_refreshing) Dirty = true;
			EnableControls();
		}

		private void EnableControls()
		{
			var switchThread = Program.SwitchThread;

			// Theme Group
			btnSave.Enabled = ciSaveTheme.Enabled = miFileSave.Enabled = Dirty;
			btnActivate.Enabled = switchThread == null || switchThread.Theme == null || !switchThread.Theme.Equals(_currentTheme);
			ciDeleteTheme.Enabled = miFileDeleteTheme.Enabled = cmbTheme.Items.Count > 1;

			// Prev/Pause/Next Group
			var isSwitching = switchThread != null && switchThread.IsSwitching;
			Theme activeTheme = GetActiveTheme();
			btnPrevious.Enabled = activeTheme != null && activeTheme.CanGoPrev && !isSwitching;
			btnSwitchNow.Enabled = activeTheme != null && !isSwitching;
			btnPause.Image = switchThread != null && switchThread.Paused ? Res.PlayIcon : Res.PauseIcon;

			// Display Mode Group
			var numScreens = Screen.AllScreens.Length;
			c_separateMonitors.Visible = numScreens > 1;
			c_allowSpanning.Visible = numScreens > 1 && c_themeMode.SelectedIndex != k_modeCollage;

			var maxClipTrackBarVisible = numScreens > 1 && c_themeMode.SelectedIndex != k_modeCollage && c_allowSpanning.Checked;
			c_maxClipTrackBar.Visible = maxClipTrackBarVisible;
			c_maxClipLabel.Visible = maxClipTrackBarVisible;
			c_maxClipPercent.Visible = maxClipTrackBarVisible;

			c_imageFit.Visible = c_themeMode.SelectedIndex != k_modeCollage;
			c_maxScale.Enabled = c_limitScale.Checked;

			// Change Frequency Group
			chkFadeTransition.Visible = Program.OsVersion >= OsVersion.Windows7;

			// Background Color Group

			// Collage Display Group
			grpCollageDisplay.Visible = c_themeMode.SelectedIndex == k_modeCollage;

			var edgeMode = c_edgeMode.GetEnumValue<EdgeMode>();
			c_edgeDist.Visible = edgeMode != EdgeMode.None;
			c_edgeDistLabel.Visible = edgeMode != EdgeMode.None;
			c_borderColorLabel.Visible = edgeMode == EdgeMode.SolidBorder;
			c_borderColor.Visible = edgeMode == EdgeMode.SolidBorder;

			var dropShadow = chkDropShadow.Checked;
			trkDropShadow.Visible = dropShadow;
			lblDropShadowUnit.Visible = dropShadow;
			chkDropShadowFeather.Visible = dropShadow;
			trkDropShadowFeatherDist.Visible = dropShadow && chkDropShadowFeather.Checked;
			lblDropShadowFeatherDist.Visible = dropShadow && chkDropShadowFeather.Checked;
			lblDropShadowOpacity.Visible = dropShadow;
			trkDropShadowOpacity.Visible = dropShadow;
			lblDropShadowOpacityValue.Visible = dropShadow;

			// Foreground Effects Group

			// Background Effects Group
			if (c_themeMode.SelectedIndex == k_modeCollage)
			{
				grpBackgroundColorEffects.Visible = true;
				trkColorEffectCollageFadeRatio.Visible = lblColorEffectCollageFadeRatioUnit.Visible = cmbColorEffectBack.GetEnumValue<ColorEffect>() != ColorEffect.None;
				trkBackgroundBlurDist.Visible = chkBackgroundBlur.Checked;
				lblBackgroundBlurDistValue.Visible = chkBackgroundBlur.Checked;
			}
			else
			{
				grpBackgroundColorEffects.Visible = false;
			}

			c_numCollageImages.Visible = c_numCollageImagesLabel.Visible = c_themeMode.SelectedIndex == k_modeCollage;

			if (c_themeOrder.SelectedIndex == k_orderRandom)
			{
				c_randomGroup.Visible = true;
				c_randomGroupCount.Visible = c_randomGroup.Checked;
				c_randomGroupCountLabel.Visible = c_randomGroup.Checked;

				int randomGroupCount;
				if (c_themeMode.SelectedIndex == k_modeCollage && c_randomGroup.Checked &&
					int.TryParse(c_randomGroupCount.Text, out randomGroupCount) && randomGroupCount > 1)
				{
					c_clearBetweenRandomGroups.Visible = true;
				}
				else
				{
					c_clearBetweenRandomGroups.Visible = false;
				}
			}
			else
			{
				c_randomGroup.Visible = false;
				c_randomGroupCount.Visible = false;
				c_randomGroupCountLabel.Visible = false;
				c_clearBetweenRandomGroups.Visible = false;
			}

			EnableLocationsContextMenu();
			EnableWidgetControls();
		}

		private void Locations_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				EnableLocationsContextMenu();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void Locations_Resize(object sender, EventArgs e)
		{
			try
			{
				lstLocations.DistributeColumns();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void BackTop_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void BackBottom_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void TrayMenu_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				PopulateThemeContextMenu();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ImageSize_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateImageSizeDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void UpdateImageSizeDisplay()
		{
			lblImageSizeDisplay.Text = String.Format(Res.ImageSizePercent, trkImageSize.Value);
		}

		private void Opacity_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateBackOpacityDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void UpdateBackOpacityDisplay()
		{
			lblOpacityDisplay.Text = String.Format(Res.BackOpacityPercent, trkOpacity.Value);
		}

		private void EdgeMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void Feather_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateFeatherDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void BorderColor_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void UpdateFeatherDisplay()
		{
			c_edgeDistLabel.Text = String.Format(Res.FeatherWidth, c_edgeDist.Value);
		}

		private void ColorEffectCollageFadeRatioTrackBar_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateCollageFadeRatioDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void UpdateCollageFadeRatioDisplay()
		{
			lblColorEffectCollageFadeRatioUnit.Text = string.Format(Res.CollageFadeRatioPercent, trkColorEffectCollageFadeRatio.Value);
		}

		private void DropShadow_Scroll(object sender, EventArgs e)
		{
			try
			{
				lblDropShadowUnit.Text = string.Format(Res.DropShadowDist, trkDropShadow.Value);
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DropShadowFeatherDist_Scroll(object sender, EventArgs e)
		{
			try
			{
				lblDropShadowFeatherDist.Text = string.Format(Res.DropShadowFeatherDistValue, trkDropShadowFeatherDist.Value);
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DropShadowOpacity_Scroll(object sender, EventArgs e)
		{
			try
			{
				lblDropShadowOpacityValue.Text = string.Format(Res.DropShadowOpacityValue, trkDropShadowOpacity.Value);
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void BackgroundBlurDist_Scroll(object sender, EventArgs e)
		{
			try
			{
				lblBackgroundBlurDistValue.Text = string.Format(Res.BackgroundBlurDist, trkBackgroundBlurDist.Value);
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void AllowSpanning_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void MaxClipTrackBar_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateMaxClipPercent();
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void UpdateMaxClipPercent()
		{
			c_maxClipPercent.Text = String.Format(Res.MaxClipPercent, c_maxClipTrackBar.Value);
		}

		private void ActivateOnExitCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Settings
		private void LoadSettings(Database db)
		{
			try
			{
				if (!Database.IsNew)
				{
					Settings.Load(db);
					LoadHotKeys(db);

					foreach (DataRow themeRow in db.SelectDataTable("select rowid, * from theme").Rows)
					{
						try
						{
							var theme = new Theme();
							theme.Load(db, themeRow);
							_themes.Add(theme);
							AttachTheme(theme);
						}
						catch (Exception ex)
						{
							this.ShowError(ex, Res.Error_LoadTheme);
						}
					}
				}
				else
				{
					var xmlFileName = ConfigFileName;
					if (File.Exists(xmlFileName))
					{
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.Load(xmlFileName);

						XmlElement xmlSettings = (XmlElement)xmlDoc.SelectSingleNode("Settings");
						if (xmlSettings != null)
						{
							Settings.Load(xmlSettings);

							foreach (XmlElement xmlHotKeys in xmlSettings.SelectNodes("HotKeys"))
							{
								LoadHotKeys(xmlHotKeys);
							}

							foreach (XmlElement xmlTheme in xmlSettings.SelectNodes("Theme"))
							{
								try
								{
									var theme = new Theme();
									theme.Load(xmlTheme);
									_themes.Add(theme);
									AttachTheme(theme);
								}
								catch (Exception ex)
								{
									this.ShowError(ex, Res.Error_LoadTheme);
								}
							}
						}

						SaveSettings(db);

						try
						{
							File.Delete(xmlFileName);
						}
						catch (Exception ex)
						{
							Log.Write(ex, "Failed to delete old settings file.");
						}
					}
					else
					{
						var theme = new Theme();
						theme.Name = Res.DefaultTheme;
						_themes.Add(theme);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Error_LoadSettings);
			}
			
		}

		private void SaveSettings(Database db)
		{
			try
			{
				using (var tran = db.BeginTransaction())
				{
					Settings.Save(db);
					SaveHotKeys(db);

					foreach (var theme in _themes)
					{
						theme.Save(db);
					}

					PurgeDatabaseOrphans(db);

					tran.Commit();
				}

				// As good time as any to flush the log.
				Log.Flush();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Error_SaveSettings);
			}
		}

		private string ConfigFileName
		{
			get
			{
				return Path.Combine(Util.AppDataDir, Res.SettingsFileName);
			}
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new SettingsDialog();
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					using (var db = new Database())
					{
						SaveSettings(db);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void PurgeDatabaseOrphans(Database db)
		{
			db.ExecuteNonQuery("delete from location where theme_id not in (select rowid from theme)");
			db.ExecuteNonQuery("delete from img where theme_id not in (select rowid from theme)");
			db.ExecuteNonQuery("delete from img where location_id not in (select rowid from location)");
			db.ExecuteNonQuery("delete from widget where theme_id not in (select rowid from theme)");
			db.ExecuteNonQuery("delete from widget_config where widget_id not in (select rowid from widget)");
			db.ExecuteNonQuery("delete from history where theme_id not in (select rowid from theme)");
			db.ExecuteNonQuery("delete from rhistory where theme_id not in (select rowid from theme)");
		}
		#endregion

		#region Theme Selection
		private void AttachTheme(Theme theme)
		{
			theme.HistoryAdded += theme_HistoryAdded;
			theme.LocationUpdated += theme_LocationUpdated;
		}

		private void DetachTheme(Theme theme)
		{
			theme.HistoryAdded -= theme_HistoryAdded;
			theme.LocationUpdated -= theme_LocationUpdated;
		}

		private void btnNewTheme_Click(object sender, EventArgs e)
		{
			try
			{
				PromptDialog dlg = new PromptDialog();
				dlg.Text = Res.NewThemeTitle;
				dlg.Prompt = Res.NewThemePrompt;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string name = dlg.String;

					Theme theme = new Theme();
					theme.Name = name;
					AttachTheme(theme);

					_themes.Add(theme);
					_currentTheme = theme;
					Dirty = true;
					RefreshControls();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Error_NewTheme);
			}
		}

		private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (_refreshing) return;

				if (cmbTheme.SelectedIndex >= 0)
				{
					Theme theme = (Theme)((TagString)cmbTheme.Items[cmbTheme.SelectedIndex]).Tag;
					bool cancelChange = false;
					var saveRequired = false;

					if (Dirty && !theme.Equals(_currentTheme) && _currentTheme != null)
					{
						switch (MessageBox.Show(this, Res.Confirm_ChangeDirtyTheme, Res.Confirm_ChangeDirtyTheme_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
						{
							case DialogResult.Yes:
								if (!SaveControls(true)) cancelChange = true;
								else saveRequired = true;
								break;
							case DialogResult.No:
								break;
							case DialogResult.Cancel:
								cancelChange = true;
								break;
						}
					}

					if (saveRequired)
					{
						using (var db = new Database())
						{
							SaveSettings(db);
						}
					}

					if (cancelChange)
					{
						// Find the index of the old theme.
						int cancelIndex = -1;
						int index = 0;
						foreach (TagString ts in cmbTheme.Items)
						{
							if (((Theme)ts.Tag).Equals(_currentTheme)) cancelIndex = index;
							index++;
						}

						if (cancelIndex >= 0)
						{
							_refreshing = true;
							cmbTheme.SelectedIndex = cancelIndex;
							_refreshing = false;
						}
						else
						{
							Log.Write(LogLevel.Error, "When attempting to cancel change, couldn't find the old theme in the combo box.");
						}

					}
					else
					{
						_currentTheme = theme;
						RefreshControls();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ThemeMenuButton_Click(object sender, EventArgs e)
		{
			try
			{
				cmTheme.Show(btnTheme, new Point(0, btnTheme.Height));
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void btnRenameTheme_Click(object sender, EventArgs e)
		{
			try
			{
				if (_currentTheme == null) return;

				PromptDialog dlg = new PromptDialog();
				dlg.Text = Res.RenameThemeTitle;
				dlg.Prompt = Res.RenameThemePrompt;
				dlg.String = _currentTheme.Name;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					_currentTheme.Name = dlg.String;
					RefreshControls();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Error_RenameTheme);
			}
		}

		private void btnDeleteTheme_Click(object sender, EventArgs e)
		{
			try
			{
				if (_themes.Count == 1)
				{
					this.ShowError(Res.Error_CantDeleteLastTheme);
					return;
				}

				int index = cmbTheme.SelectedIndex;
				Theme deleteTheme = (Theme)((TagString)cmbTheme.Items[index]).Tag;

				if (MessageBox.Show(this, String.Format(Res.Confirm_DeleteTheme, deleteTheme.Name),
					Res.Confirm_DeleteTheme_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
					== DialogResult.Yes)
				{
					using (var db = new Database())
					{
						DetachTheme(deleteTheme);
						_themes.Remove(deleteTheme);
						deleteTheme.DeleteFromDatabase(db);

						// Remove the theme out of the combo box, and use the combo-box's trigger to select the next theme.
						cmbTheme.Items.RemoveAt(index);
						if (index >= cmbTheme.Items.Count) index--;
						cmbTheme.SelectedIndex = index;

						if (deleteTheme.IsActive)
						{
							Theme newActiveTheme = (Theme)((TagString)cmbTheme.Items[cmbTheme.SelectedIndex]).Tag;
							ActivateTheme(db, newActiveTheme);
						}
					}
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miDuplicateTheme_Click(object sender, EventArgs e)
		{
			try
			{
				if (_currentTheme == null) return;

				var cancel = false;
				var saveRequired = false;
				if (Dirty)
				{
					switch (MessageBox.Show(this, Res.DuplicateThemeDirtyPrompt, Res.DuplicateThemeDirtyTitle,
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
					{
						case DialogResult.Yes:
							if (!SaveControls(true)) cancel = true;
							else saveRequired = true;
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							cancel = true;
							break;
					}
				}

				if (saveRequired)
				{
					using (var db = new Database())
					{
						SaveSettings(db);
					}
				}

				if (!cancel)
				{
					PromptDialog dlg = new PromptDialog();
					dlg.Text = Res.DuplicateThemeTitle;
					dlg.Prompt = Res.DuplicateThemePrompt;
					dlg.String = _currentTheme.Name;
					dlg.ValidateString = str => !string.IsNullOrWhiteSpace(str) && !(from t in _themes where t.Name == str select t).Any();
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						var newTheme = _currentTheme.Clone();
						newTheme.Name = dlg.String;
						AttachTheme(newTheme);

						_themes.Add(newTheme);
						_currentTheme = newTheme;
						Dirty = true;
						RefreshControls();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private Theme GetActiveTheme()
		{
			foreach (Theme theme in _themes)
			{
				if (theme.IsActive) return theme;
			}
			return _themes[0];
		}

		private void btnActivate_Click(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					ActivateTheme(db, _currentTheme);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		public void OnActivateTheme(Database db, Theme setTheme)
		{
			try
			{
				if (!setTheme.IsActive) ActivateTheme(db, setTheme);
				else
				{
					var switchThread = Program.SwitchThread;
					if (switchThread != null) switchThread.SwitchNow(db, SwitchDir.Next);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ActivateTheme(Database db, Theme setTheme)
		{
			_refreshing = true;
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread == null) return;
				switchThread.Theme = setTheme;
				setTheme.IsActive = true;

				for (int i = 0; i < cmbTheme.Items.Count; i++)
				{
					Theme theme = (Theme)((TagString)cmbTheme.Items[i]).Tag;
					string name;
					if (theme.Equals(setTheme))
					{
						theme.IsActive = true;
						name = String.Format(Res.ActiveTheme, theme.Name);
					}
					else
					{
						theme.IsActive = false;
						name = theme.Name;
					}
					cmbTheme.Items[i] = new TagString(name, theme);
				}

				switchThread.SwitchNow(db, SwitchDir.Next);

				SaveSettings(db);
			}
			finally
			{
				_refreshing = false;
			}
		}

		private void PopulateThemeContextMenu()
		{
			ciTheme.DropDownItems.Clear();

			if (_themes.Count == 0)
			{
				ciTheme.Visible = false;
			}
			else
			{
				ciTheme.Visible = true;

				foreach (Theme theme in _themes)
				{
					ToolStripMenuItem ciThemeItem = new ToolStripMenuItem(theme.Name);
					ciThemeItem.Checked = theme.IsActive;
					ciThemeItem.Tag = theme;
					ciThemeItem.Click += new EventHandler(ciThemeItem_Click);

					ciTheme.DropDownItems.Add(ciThemeItem);
				}
			}
		}

		void ciThemeItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType().IsSubclassOf(typeof(ToolStripItem)))
				{
					object tag = ((ToolStripItem)sender).Tag;
					if (tag != null && tag.GetType() == typeof(Theme))
					{
						using (var db = new Database())
						{
							ActivateTheme(db, (Theme)tag);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miFileNewTheme_Click(object sender, EventArgs e)
		{
			try
			{
				btnNewTheme_Click(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miFileRenameTheme_Click(object sender, EventArgs e)
		{
			try
			{
				btnRenameTheme_Click(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miFileDeleteTheme_Click(object sender, EventArgs e)
		{
			try
			{
				btnDeleteTheme_Click(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void chkSeparateMonitors_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		void theme_LocationUpdated(object sender, Theme.LocationUpdatedEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => theme_LocationUpdated(sender, e)));
				return;
			}

			try
			{
				var lvi = (from l in lstLocations.Items.Cast<ListViewItem>() where object.Equals(l.Tag, e.Location) select l).FirstOrDefault();
				if (lvi != null) UpdateLocationLvi(lvi);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread == null) return;
				switchThread.Paused = !switchThread.Paused;
				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		public IEnumerable<Theme> Themes
		{
			get { return _themes; }
		}
		#endregion

		#region Location List
		private void EnableLocationsContextMenu()
		{
			var multiSelectedItems = lstLocations.SelectedItems.Count > 0;
			var singleSelectedItem = lstLocations.SelectedItems.Count == 1;
			var multiFileOrDirSelected = (from l in lstLocations.SelectedItems.Cast<ListViewItem>()
										  where (l.Tag as Location).Type == LocationType.File || (l.Tag as Location).Type == LocationType.Directory
										  select l).Any();

			ciDeleteLocation.Enabled = multiSelectedItems;
			ciUpdateLocationNow.Enabled = multiSelectedItems;
			ciLocationExplore.Enabled = multiFileOrDirSelected;
			c_browseLocationMenuItem.Enabled = singleSelectedItem;
			ciLocationProperties.Enabled = singleSelectedItem;
		}

		private void AddLocationItem(Location location)
		{
			var lvi = new ListViewItem();
			lvi.Tag = location;
			lvi.Checked = !location.Disabled;
			UpdateLocationLvi(lvi);
			lstLocations.Items.Add(lvi);

			try
			{
				switch (location.Type)
				{
					case LocationType.Directory:
						lvi.ImageIndex = k_locimgFolder;
						break;
					case LocationType.Feed:
						lvi.ImageIndex = k_locimgFeed;
						break;
					case LocationType.File:
						lvi.ImageIndex = k_locimgImageFile;
						break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, String.Format(Res.Exception_GetIcon, location));
				Log.Write(LogLevel.Error, String.Format(Res.Exception_GetIcon, location) + ex.ToString());
				throw;
			}
		}

		private void UpdateLocationLvi(ListViewItem lvi)
		{
			var location = lvi.Tag as Location;

			// Path
			lvi.Text = location.Path;

			// Next update time
			var nextUpdate = location.NextUpdate;
			string nextUpdateString;
			if (nextUpdate <= DateTime.Now)
			{
				nextUpdateString = Res.NextUpdateNow;
			}
			else
			{
				nextUpdateString = nextUpdate.ToString(k_nextUpdateFormat);
			}
			if (lvi.SubItems.Count < 2) lvi.SubItems.Add(nextUpdateString);
			else lvi.SubItems[1].Text = nextUpdateString;

			var freqStr = TimeSpanUtil.IntervalDisplayString(location.UpdateFrequency, location.UpdatePeriod);
			if (lvi.SubItems.Count < 3) lvi.SubItems.Add(freqStr);
			else lvi.SubItems[2].Text = freqStr;
		}

		private void btnAddFolder_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new FeedDialog(new Location(LocationType.Directory, ""));
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					AddLocationItem(dlg.Feed);
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_AddFolder);
			}
		}

		private void btnAddImage_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new OpenFileDialog();
				dlg.Filter = ImageFormatDesc.ImageFileFilter;
				dlg.FilterIndex = 1;
				dlg.Multiselect = true;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					foreach (var fileName in dlg.FileNames)
					{
						AddLocationItem(new Location(LocationType.File, fileName));
					}
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_AddFile);
			}
		}

		private void btnAddFeed_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new FeedDialog(new Location(LocationType.Feed, ""));
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					AddLocationItem(dlg.Feed);
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void lstLocations_ItemActivate(object sender, EventArgs e)
		{
			try
			{
				EditSelectedLocation();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void EditSelectedLocation()
		{
			if (lstLocations.SelectedItems.Count != 1) return;

			var selectedItem = lstLocations.SelectedItems[0];
			var loc = selectedItem.Tag as Location;

			var dlg = new FeedDialog(loc);
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				UpdateLocationLvi(selectedItem);
				Dirty = true;
			}
		}

		private void ciDeleteLocation_Click(object sender, EventArgs e)
		{
			try
			{
				int selIndex = -1;
				foreach (ListViewItem lvi in lstLocations.SelectedItems)
				{
					selIndex = lvi.Index;
					lstLocations.Items.Remove(lvi);
					break;
				}

				if (selIndex >= 0)
				{
					if (selIndex >= lstLocations.Items.Count) selIndex = lstLocations.Items.Count - 1;
					if (selIndex >= 0) lstLocations.Items[selIndex].Selected = true;
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ciAddFolder_Click(object sender, EventArgs e)
		{
			try
			{
				btnAddFolder_Click(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ciAddImage_Click(object sender, EventArgs e)
		{
			try
			{
				btnAddImage_Click(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void lstLocations_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				bool accept = false;

				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

					foreach (string file in files)
					{
						if (Directory.Exists(file) ||
							(File.Exists(file) && ImageFormatDesc.FileNameToImageFormat(file) != null))
						{
							accept = true;
							break;
						}
					}
				}

				e.Effect = accept ? DragDropEffects.Copy : DragDropEffects.None;
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception on drag-enter.");
				e.Effect = DragDropEffects.None;
			}
		}

		private void lstLocations_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				foreach (string file in files)
				{
					if (Directory.Exists(file) ||
						(File.Exists(file) && ImageFormatDesc.FileNameToImageFormat(file) != null))
					{
						if (File.Exists(file)) AddLocationItem(new Location(LocationType.File, file));
						else AddLocationItem(new Location(LocationType.Directory, file));
						Dirty = true;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception on drag-drop.");
			}
		}

		private void ciUpdateLocationNow_Click(object sender, EventArgs e)
		{
			try
			{
				var dt = DateTime.Now;
				foreach (var loc in (from l in lstLocations.SelectedItems.Cast<ListViewItem>() select l.Tag as Location))
				{
					loc.SetNextUpdateNow();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ciLocationProperties_Click(object sender, EventArgs e)
		{
			try
			{
				EditSelectedLocation();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ciLocationExplore_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (var loc in (from l in lstLocations.SelectedItems.Cast<ListViewItem>()
									 select l.Tag as Location))
				{
					switch (loc.Type)
					{
						case LocationType.File:
							if (!File.Exists(loc.Path))
							{
								this.ShowError(Res.Error_ImageFileMissing, loc.Path);
							}
							else
							{
								FileUtil.ExploreFile(loc.Path);
							}
							break;
						case LocationType.Directory:
							if (!Directory.Exists(loc.Path))
							{
								this.ShowError(Res.Error_DirectoryMissing);
							}
							else
							{
								FileUtil.ExploreDir(loc.Path);
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void lstLocations_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void BrowseLocationMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (lstLocations.SelectedItems.Count != 1) return;

				var selectedItem = lstLocations.SelectedItems[0];
				var loc = selectedItem.Tag as Location;

				LocationBrowser window;
				if (_locationBrowsers.TryGetValue(loc, out window))
				{
					window.BringToFront();
					window.Activate();
				}
				else
				{
					window = new LocationBrowser(loc);
					window.Show();
					_locationBrowsers[loc] = window;
				}
				window.FormClosed += LocationBrowser_FormClosed;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_FormClosed(object sender, FormClosedEventArgs e)
		{
			var window = sender as LocationBrowser;
			if (window != null) _locationBrowsers.Remove(window.LocationObject);
		}
		#endregion

		#region Thread Control
		private void ciSwitchNow_Click(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null)
				{
					using (var db = new Database())
					{
						switchThread.SwitchNow(db, SwitchDir.Next);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_SwitchNowTray);
			}
		}

		private void ciSwitchPrev_Click(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null)
				{
					using (var db = new Database())
					{
						switchThread.SwitchNow(db, SwitchDir.Prev);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_SwitchPrevTray);
				throw;
			}
		}

		private void btnSwitchNow_Click(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null)
				{
					using (var db = new Database())
					{
						switchThread.SwitchNow(db, SwitchDir.Next);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_SwitchNow);
			}
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null)
				{
					using (var db = new Database())
					{
						switchThread.SwitchNow(db, SwitchDir.Prev);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_SwitchPrev);
			}
		}

		private SwitchThread.SwitchEventHandler _switchThread_Switching_func = null;
		void SwitchThread_Switching(object sender, EventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					// If not in the main thread, then invoke it.
					if (_switchThread_Switching_func == null) _switchThread_Switching_func = new SwitchThread.SwitchEventHandler(SwitchThread_Switching);
					BeginInvoke(_switchThread_Switching_func, new object[] { sender, e });
					return;
				}

				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private SwitchThread.SwitchEventHandler _switchThread_Switched_func = null;
		void SwitchThread_Switched(object sender, EventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					// If not in the main thread, then invoke it.
					if (_switchThread_Switched_func == null) _switchThread_Switched_func = new SwitchThread.SwitchEventHandler(SwitchThread_Switched);
					BeginInvoke(_switchThread_Switched_func, new object[] { sender, e });
					return;
				}

				EnableControls();
				using (var db = new Database())
				{
					ClearExpiredCache(db);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ClearHistory(Database db)
		{
            Log.Info("Clearing history...");
			foreach (var theme in _themes) theme.ClearHistory(db);
			c_historyTab.Clear();
			ClearExpiredCache(db);
		}

		private void miClearHistory_Click(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					ClearHistory(db);

					var switchThread = Program.SwitchThread;
					if (switchThread != null) switchThread.SwitchNow(db, SwitchDir.Clear);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
			
		}

		private void ClearExpiredCache(Database db)
		{
			// Create a list of all images currently in the history.
			// These are the files that we don't want to delete.
			var keepLocations = new List<string>();
			keepLocations.AddRange(db.SelectStringList("select history.path from history inner join theme on theme.rowid = history.theme_id"));
			keepLocations.AddRange(from h in c_historyTab.History select h.Location);
			keepLocations.AddRange(db.SelectStringList("select img.path from img inner join theme on theme.rowid = img.theme_id where cache_path != ''"));

			// Tell the image cache object to delete all others.
			ImageCache.ClearExpiredCache(db, keepLocations);
		}

		private void chkLimitScale_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (c_limitScale.Checked)
				{
					int scale = _currentTheme.MaxImageScale;
					if (scale <= 0) scale = Theme.k_defaultMaxImageScale;
					c_maxScale.Text = scale.ToString();
					c_maxScale.Enabled = true;
				}
				else
				{
					c_maxScale.Text = string.Empty;
					c_maxScale.Enabled = false;
				}
				EnableControls();
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region About Box
		private void miHelpAbout_Click(object sender, EventArgs e)
		{
			try
			{
				AboutDialog dlg = new AboutDialog();
				dlg.ShowDialog();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_AboutDialog);
			}
		}
		#endregion

		#region HotKeys
		private HotKey _nextImageHotKey = new HotKey();
		private HotKey _prevImageHotKey = new HotKey();
		private HotKey _pauseHotKey = new HotKey();
		private HotKey _clearHistoryHotKey = new HotKey();
		private HotKey _showWindowHotKey = new HotKey();

		private const string k_nextImageHotKey = "NextImageHotKey";
		private const string k_prevImageHotKey = "PrevImageHotKey";
		private const string k_pauseHotKey = "PauseHotKey";
		private const string k_clearHistoryHotKey = "ClearHistoryHotKey";
		private const string k_showWindowHotKey = "ShowWindowHotKey";

		private void RegisterHotKeys()
		{
			foreach (Theme theme in _themes) theme.HotKey.Register(this);

			_nextImageHotKey.Register(this);
			_nextImageHotKey.HotKeyPressed += new EventHandler(NextImageHotKey_HotKeyPressed);

			_prevImageHotKey.Register(this);
			_prevImageHotKey.HotKeyPressed += new EventHandler(PrevImageHotKey_HotKeyPressed);

			_pauseHotKey.Register(this);
			_pauseHotKey.HotKeyPressed += new EventHandler(PauseHotKey_HotKeyPressed);

			_clearHistoryHotKey.Register(this);
			_clearHistoryHotKey.HotKeyPressed += new EventHandler(ClearHistoryHotKey_HotKeyPressed);

			_showWindowHotKey.Register(this);
			_showWindowHotKey.HotKeyPressed += new EventHandler(ShowWindowHotKey_HotKeyPressed);
		}

		private void UnregisterHotKeys()
		{
			foreach (Theme theme in _themes) theme.HotKey.Unregister();

			_nextImageHotKey.Unregister();
			_prevImageHotKey.Unregister();
			_pauseHotKey.Unregister();
			_clearHistoryHotKey.Unregister();
			_showWindowHotKey.Unregister();
		}

		private void ReregisterHotKeys()
		{
			foreach (Theme theme in _themes) theme.HotKey.Reregister();

			_nextImageHotKey.Reregister();
			_prevImageHotKey.Reregister();
			_pauseHotKey.Reregister();
			_clearHistoryHotKey.Reregister();
			_showWindowHotKey.Reregister();
		}

		private void SaveHotKeys(Database db)
		{
			db.WriteSetting(k_nextImageHotKey, _nextImageHotKey.ToSaveString());
			db.WriteSetting(k_prevImageHotKey, _prevImageHotKey.ToSaveString());
			db.WriteSetting(k_pauseHotKey, _pauseHotKey.ToSaveString());
			db.WriteSetting(k_clearHistoryHotKey, _clearHistoryHotKey.ToSaveString());
			db.WriteSetting(k_showWindowHotKey, _showWindowHotKey.ToSaveString());
		}

		private void LoadHotKeys(Database db)
		{
			string str;
			if (!string.IsNullOrEmpty(str = db.LoadSetting(k_nextImageHotKey))) _nextImageHotKey.LoadFromSaveString(str);
			if (!string.IsNullOrEmpty(str = db.LoadSetting(k_prevImageHotKey))) _prevImageHotKey.LoadFromSaveString(str);
			if (!string.IsNullOrEmpty(str = db.LoadSetting(k_pauseHotKey))) _pauseHotKey.LoadFromSaveString(str);
			if (!string.IsNullOrEmpty(str = db.LoadSetting(k_clearHistoryHotKey))) _clearHistoryHotKey.LoadFromSaveString(str);
			if (!string.IsNullOrEmpty(str = db.LoadSetting(k_showWindowHotKey))) _showWindowHotKey.LoadFromSaveString(str);
		}

		private void LoadHotKeys(XmlElement xmlRoot)
		{
			foreach (XmlElement element in xmlRoot.SelectNodes(HotKey.XmlElementName))
			{
				switch (element.GetAttribute(HotKey.XmlNameAttribute))
				{
					case "NextImage":
						_nextImageHotKey.LoadXml(element);
						break;
					case "PrevImage":
						_prevImageHotKey.LoadXml(element);
						break;
					case "Pause":
						_pauseHotKey.LoadXml(element);
						break;
					case "ClearHistory":
						_clearHistoryHotKey.LoadXml(element);
						break;
					case "ShowWindow":
						_showWindowHotKey.LoadXml(element);
						break;
				}
			}
		}

		private void miHotKeys_Click(object sender, EventArgs e)
		{
			try
			{
				var form = new HotKeySettings();
				form.NextImageHotKey = _nextImageHotKey;
				form.PrevImageHotKey = _prevImageHotKey;
				form.PauseHotKey = _pauseHotKey;
				form.ClearHistoryHotKey = _clearHistoryHotKey;
				form.ShowWindowHotKey = _showWindowHotKey;

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					_nextImageHotKey.Copy(form.NextImageHotKey);
					_prevImageHotKey.Copy(form.PrevImageHotKey);
					_pauseHotKey.Copy(form.PauseHotKey);
					_clearHistoryHotKey.Copy(form.ClearHistoryHotKey);
					_showWindowHotKey.Copy(form.ShowWindowHotKey);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		void NextImageHotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null)
				{
					using (var db = new Database())
					{
						switchThread.SwitchNow(db, SwitchDir.Next);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception during next image hotkey.");
			}
		}

		void PrevImageHotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null)
				{
					using (var db = new Database())
					{
						switchThread.SwitchNow(db, SwitchDir.Prev);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception during previous image hotkey.");
			}
		}

		void PauseHotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread == null) return;
				switchThread.Paused = !switchThread.Paused;
				EnableControls();
				ShowNotification(switchThread.Paused ? Res.Notify_Paused : Res.Notify_Unpaused, null);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception during pause hotkey.");
			}
		}

		void ClearHistoryHotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					ClearHistory(db);
				}
				ShowNotification(Res.Notify_HistoryCleared, null);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception during clear history hotkey.");
			}
		}

		void ShowWindowHotKey_HotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				AppActivate();
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception during show window hotkey.");
			}
		}
		#endregion

		#region History
		void theme_HistoryAdded(object sender, Theme.HistoryAddedEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => theme_HistoryAdded(sender, e)));
				return;
			}

			try
			{
				using (var db = new Database())
				{
					foreach (var img in (from i in e.Images select i.ImageRec).Distinct())
					{
						c_historyTab.AddHistory(HistoryItem.FromImageRec(c_historyTab, db, img));
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void lstHistory_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				var item = c_historyTab.SelectedItem;
				ciOpenHistoryFile.Enabled = item != null;
				ciExploreHistoryFile.Enabled = item != null;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ciOpenHistoryFile_Click(object sender, EventArgs e)
		{
			try
			{
				var item = c_historyTab.SelectedItem;
				if (item != null)
				{
					var fileName = item.LocationOnDisk;
					if (string.IsNullOrWhiteSpace(fileName))
					{
						this.ShowError(Res.Error_ImageFileMissing, fileName);
					}
					else
					{
						Process.Start(fileName);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ciExploreHistoryFile_Click(object sender, EventArgs e)
		{
			try
			{
				var item = c_historyTab.SelectedItem;
				if (item != null)
				{
					var fileName = item.LocationOnDisk;
					if (string.IsNullOrWhiteSpace(fileName))
					{
						this.ShowError(Res.Error_ImageFileMissing, fileName);
					}
					else
					{
						FileUtil.ExploreFile(fileName);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void lstHistory_ItemActivated(object sender, HistoryList.ItemActivatedEventArgs e)
		{
			try
			{
				var fileName = e.Item.LocationOnDisk;
				if (string.IsNullOrWhiteSpace(fileName))
				{
					this.ShowError(Res.Error_ImageFileMissing, fileName);
				}
				else
				{
					Process.Start(fileName);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ciClearHistoryList_Click(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					ClearHistory(db);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ciDeleteHistoryFile_Click(object sender, EventArgs e)
		{
			try
			{
				var item = c_historyTab.SelectedItem;
				if (item != null) DeleteHistoryItem(item);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void HistoryTab_DeleteItemRequested(object sender, HistoryList.DeleteItemRequestedEventArgs e)
		{
			try
			{
				DeleteHistoryItem(e.Item);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DeleteHistoryItem(HistoryItem item)
		{
			var fileName = item.LocationOnDisk;
			if (string.IsNullOrWhiteSpace(fileName))
			{
				this.ShowError(Res.Error_ImageFileMissing, fileName);
			}
			else if (MessageBox.Show(this, Res.Confirm_DeleteHistoryFile, Res.Confirm_DeleteHistoryFile_Caption,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
				== DialogResult.Yes)
			{
				DeleteImageFile(fileName);
			}
		}

		private void LoadHistoryFromDatabase(Database db)
		{
			try
			{
				var maxHistory = c_historyTab.MaxHistory;

				var history = new List<HistoryItem>();
				foreach (DataRow row in db.SelectDataTable("select rowid, * from history order by display_date desc limit @max", "@max", maxHistory).Rows)
				{
					history.Add(HistoryItem.FromDataRow(c_historyTab, row));
				}

				history.Reverse();
				foreach (var item in history)
				{
					c_historyTab.AddHistory(item);
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when attempting to load history.");
			}
		}
		#endregion

		#region Auto-Update Check
		public void CheckForUpdates()
		{
			try
			{
				var checker = new UpdateChecker();
				checker.UpdateAvailable += new EventHandler<UpdateCheckEventArgs>(checker_UpdateAvailable);
				checker.Check();
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when checking for updates.");
			}
		}

		private void checker_UpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				ShowNotification(string.Format(Res.UpdateAvailableBalloonText, e.WebVersion.ToAppFormat()), (x, y) => { e.OpenUpdateUrl(); });
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when notifying that update available.");
			}
		}
		#endregion

		#region Widgets
		private void PopulateWidgetCombo()
		{
			c_widgetTypes.Items.Clear();
			foreach (var wt in WidgetManager.Types)
			{
				c_widgetTypes.Items.Add(new TagString(wt.Name, wt));
			}

			if (c_widgetTypes.Items.Count > 0) c_widgetTypes.SelectedIndex = 0;
		}

		private void EnableWidgetControls()
		{
			c_addWidgetButton.Enabled = c_widgetTypes.SelectedItem != null;
			c_widgetDeleteButton.Enabled = c_widgetLayout.SelectedWidget != null;
		}

		private void c_addWidgetButton_Click(object sender, EventArgs e)
		{
			try
			{
				var tag = c_widgetTypes.SelectedItem as TagString;
				if (tag != null) c_widgetLayout.AddNewWidget((WidgetType)tag.Tag);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_deleteWidgetButton_Click(object sender, EventArgs e)
		{
			try
			{
				c_widgetLayout.DeleteSelectedWidget();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetLayout_WidgetsChanged(object sender, EventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
				//RefreshWidgetList();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetLayout_SelectedWidgetChanged(object sender, WidgetLayoutControl.WidgetEventArgs e)
		{
			try
			{
				EnableWidgetControls();

				if (e.Widget != null)
				{
					c_widgetPropertyGrid.SelectedObject = e.Widget.Properties;
					SelectWidgetInList(e.Widget);
				}
				else
				{
					c_widgetPropertyGrid.SelectedObject = null;
					SelectWidgetInList(null);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				EnableWidgetControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			try
			{
				ControlChanged(c_widgetPropertyGrid, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetMoveUpButton_Click(object sender, EventArgs e)
		{
			try
			{
				var widget = c_widgetLayout.SelectedWidget;
				if (widget != null)
				{
					c_widgetLayout.MoveWidget(widget, WidgetLayoutControl.WidgetMoveDirection.Up);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetMoveDownButton_Click(object sender, EventArgs e)
		{
			try
			{
				var widget = c_widgetLayout.SelectedWidget;
				if (widget != null)
				{
					c_widgetLayout.MoveWidget(widget, WidgetLayoutControl.WidgetMoveDirection.Down);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetDeleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				c_widgetLayout.DeleteSelectedWidget();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RefreshWidgetList()
		{
			WidgetInstance selectedWidget = null;
			var selectedLvi = c_widgetList.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
			if (selectedLvi != null) selectedWidget = selectedLvi.Tag as WidgetInstance;

			c_widgetList.Items.Clear();

			foreach (var widget in c_widgetLayout.Widgets)
			{
				var lvi = new ListViewItem(widget.DisplayName);
				lvi.Tag = widget;
				c_widgetList.Items.Add(lvi);
			}

			if (selectedWidget != null)
			{
				SelectWidgetInList(selectedWidget);
			}
		}

		private void SelectWidgetInList(WidgetInstance widget)
		{
			ListViewItem widgetLvi = null;
			if (widget != null) widgetLvi = (from l in c_widgetList.Items.Cast<ListViewItem>() where l.Tag == widget select l).FirstOrDefault();

			if (widgetLvi != null)
			{
				if (!widgetLvi.Selected)
				{
					widgetLvi.Selected = true;
					c_widgetList.EnsureVisible(widgetLvi.Index);
				}
			}
			else
			{
				foreach (var lvi in (from l in c_widgetList.Items.Cast<ListViewItem>() where l.Selected select l))
				{
					lvi.Selected = false;
				}
			}
		}

		private void c_widgetList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				var selectedWidget = (from l in c_widgetList.SelectedItems.Cast<ListViewItem>() select l.Tag as WidgetInstance).FirstOrDefault();
				c_widgetLayout.SelectWidget(selectedWidget, false);
				EnableWidgetControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetLayout_WidgetAdded(object sender, WidgetLayoutControl.WidgetEventArgs e)
		{
			try
			{
				RefreshWidgetList();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetLayout_WidgetDeleted(object sender, WidgetLayoutControl.WidgetEventArgs e)
		{
			try
			{
				RefreshWidgetList();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_widgetLayout_WidgetOrderChanged(object sender, EventArgs e)
		{
			try
			{
				RefreshWidgetList();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Window Transparency
		private void TransparencyTrackBar_Scroll(object sender, EventArgs e)
		{
			try
			{
				Settings.Transparency = (double)c_transparencyTrackBar.Value / (double)c_transparencyTrackBar.Maximum;
				UpdateTransparency();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		/// <summary>
		/// Updates the Window's transparency
		/// </summary>
		private void UpdateTransparency()
		{
			if (Settings.Transparency < k_minTransparency) Settings.Transparency = k_minTransparency;
			else if (Settings.Transparency > 1.0) Settings.Transparency = 1.0;
			this.Opacity = Settings.Transparency;
		}

		/// <summary>
		/// Refreshes the transparency controls on the form.
		/// </summary>
		private void RefreshTransparency()
		{
			var value = (int)Math.Round(Settings.Transparency * ((double)c_transparencyTrackBar.Maximum - (double)c_transparencyTrackBar.Minimum) + (double)c_transparencyTrackBar.Minimum);
			if (value < c_transparencyTrackBar.Minimum) value = c_transparencyTrackBar.Minimum;
			if (value > c_transparencyTrackBar.Maximum) value = c_transparencyTrackBar.Maximum;
			c_transparencyTrackBar.Value = value;
		}
		#endregion

		#region Image Management
		public void DeleteImageFile(string fileName)
		{
			if (File.Exists(fileName)) FileUtil.RecycleFile(fileName);
			Global.OnFileDeleted(fileName);
			c_historyTab.RemoveItem(fileName);
		}
		#endregion

		#region Filter
		private void RefreshFilter()
		{
			c_filterFlow.Controls.Clear();

			var filter = _currentTheme.Filter;
			if (filter != null)
			{
				try
				{
					foreach (var cond in filter.Conditions)
					{
						var ctrl = new ConditionControl(cond);
						c_filterFlow.Controls.Add(ctrl);
						OnFilterControlAdded(ctrl);
					}

					UpdateFilterConditionLayout();
				}
				catch (Exception ex)
				{
					Log.Error(ex);
					c_filterFlow.Controls.Clear();
				}
			}
		}

		private void ConditionControl_DataChanged(object sender, EventArgs e)
		{
			ControlChanged(sender, e);
		}

		private bool SaveFilter(bool showErrors, out ImageFilter filterOut)
		{
			var count = (from c in c_filterFlow.Controls.Cast<Control>() where c is ConditionControl select c).Count();
			if (count == 0)
			{
				filterOut = null;
				return true;
			}

			var filter = new ImageFilter();
			foreach (ConditionControl ctrl in c_filterFlow.Controls.Cast<Control>().Where(x => x is ConditionControl))
			{
				var cond = ctrl.Condition;
				if (cond == null)
				{
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_filterTab;
						ctrl.TypeComboBox.Focus();
						this.ShowError(Res.Error_ImageFilterConditionTypeBlank);
					}

					filterOut = null;
					return false;
				}

				var compare = cond.Compare;
				if (string.IsNullOrEmpty(compare))
				{
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_filterTab;
						ctrl.CompareComboBox.Focus();
						this.ShowError(Res.Error_ImageFilterConditionCompareBlank);
					}

					filterOut = null;
					return false;
				}

				string error = null;
				if (!cond.Validate(ref error))
				{
					if (showErrors)
					{
						c_themeTabControl.SelectedTab = c_filterTab;
						var focusCtrl = cond.ValueControl;
						if (focusCtrl != null) focusCtrl.Focus();
						this.ShowError(error);
					}

					filterOut = null;
					return false;
				}

				filter.AddCondition(ctrl.Condition);
			}
			if (!filter.Conditions.Any())
			{
				filterOut = null;
				return true;
			}

			filterOut = filter;
			return true;
		}

		public void UpdateFilterConditionLayout()
		{
			var ctrls = GetFilterConditionControls();

			for (int i = 0; i < ctrls.Length; i++)
			{
				var ctrl = ctrls[i];
				ctrl.Index = i;

				if (ctrl.Operator == Operator.And || i == 0)
				{
					if (i + 1 < ctrls.Length && ctrls[i + 1].Operator == Operator.Or)
					{
						ctrl.GroupMode = ConditionGroupMode.Begin;
					}
					else
					{
						ctrl.GroupMode = ConditionGroupMode.Solo;
					}
				}
				else // Or
				{
					if (i + 1 < ctrls.Length && ctrls[i + 1].Operator == Operator.Or)
					{
						ctrl.GroupMode = ConditionGroupMode.Middle;
					}
					else
					{
						ctrl.GroupMode = ConditionGroupMode.End;
					}
				}
			}
		}

		private void c_addFilterButton_Click(object sender, EventArgs e)
		{
			try
			{
				var ctrl = new ConditionControl();
				c_filterFlow.Controls.Add(ctrl);
				OnFilterControlAdded(ctrl);

				UpdateFilterConditionLayout();
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		public void OnFilterControlAdded(ConditionControl ctrl)
		{
			ctrl.DataChanged += ConditionControl_DataChanged;
		}

		private ConditionControl[] GetFilterConditionControls()
		{
			return (from c in c_filterFlow.Controls.Cast<Control>()
					where c is ConditionControl
					select c as ConditionControl).ToArray();
		}
		#endregion
	}
}
