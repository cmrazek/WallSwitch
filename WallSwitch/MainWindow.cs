using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;

namespace WallSwitch
{
	internal partial class MainWindow : Form
	{
		#region Variables
		private List<Theme> _themes = new List<Theme>();
		private Theme _currentTheme = null;
		private bool _reallyClose = false;
		private bool _refreshing = false;
		private bool _dirty = false;
		private ImageList _locationImages = new ImageList();
		private bool _winStart = false;
		private HotKey _changeThemeHotKey = new HotKey();
		private static MainWindow _mainWindow = null;

		private delegate void VoidDelegate();
		private VoidDelegate _appActivateFunc = null;
		
		private EventHandler _balloonClickedHandler = null;
		#endregion

		#region PInvoke
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool BringWindowToTop(IntPtr hWnd);

		private const int WM_HOTKEY = 0x0312;

		[DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(IntPtr hWnd);
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
		#endregion

		#region Window Management
		public MainWindow(bool winStart)
		{
			if (_winStart = winStart) HideToTray();
			InitializeComponent();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			try
			{
				_mainWindow = this;

				LoadSettings();

				// Determine which theme is the currently active theme
				Theme activeTheme;
				if (_themes.Count == 0)
				{
					_currentTheme = new Theme(Guid.NewGuid());
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
				switchThread.Start(activeTheme);
				switchThread.Switching += new SwitchThread.SwitchEventHandler(SwitchThread_Switching);
				switchThread.Switched += new SwitchThread.SwitchEventHandler(SwitchThread_Switched);
				Program.SwitchThread = switchThread;

				cmbColorEffectFore.InitForEnum<ColorEffect>(ColorEffect.None);
				cmbColorEffectBack.InitForEnum<ColorEffect>(ColorEffect.None);

				c_edgeMode.InitForEnum<EdgeMode>(EdgeMode.Feather);

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

				var serviceMgr = new WallSwitchServiceManager();
				serviceMgr.CreateService();
				Program.ServiceManager = serviceMgr;

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

				SaveSettings();
				UnregisterHotKeys();

				// If the user has an 'exit theme' set, then switch to that theme.
				if (Program.SwitchThread != null)
				{
					var exitTheme = _mainWindow.Themes.FirstOrDefault(t => t.ActivateOnExit);
					if (exitTheme != null && exitTheme != _currentTheme)
					{
						Log.Write(LogLevel.Info, "Switching to theme '{0}' on exit...", exitTheme.Name);
						Program.SwitchThread.WallpaperSetter.Set(exitTheme, SwitchDir.Next, true);
					}
					else
					{
						Log.WriteDebug("No exit theme is set.");
					}
				}
				else
				{
					Log.Write(LogLevel.Warning, "On application exit, the switch thread or main window is null.");
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
						SaveSettings();
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

			try
			{
				switch (m.Msg)
				{
					case WM_HOTKEY:
						HotKey.OnWmHotKey(m.WParam.ToInt32());
						break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		public static MainWindow Window
		{
			get { return _mainWindow; }
		}
		#endregion

		#region Hide To Tray
		private void HideToTray()
		{
			WindowState = FormWindowState.Minimized;
			Visible = false;

			if (ShowInTaskbar != false)
			{
				ShowInTaskbar = false;

				// Switching ShowInTaskbar causes hotkeys to be unregistered; reregister them.
				ReregisterHotKeys();
			}
		}

		private void ShowFromTray()
		{
			if (IsDisposed) return;

			Visible = true;
			WindowState = FormWindowState.Normal;

			if (ShowInTaskbar != true)
			{
				ShowInTaskbar = true;

				// Switching ShowInTaskbar causes hotkeys to be unregistered; reregister them.
				ReregisterHotKeys();
			}

			BringWindowToTop(this.Handle);

			// Select the active theme.
			Theme activeTheme = GetActiveTheme();
			if (activeTheme != null && !activeTheme.Equals(_currentTheme)) _currentTheme = activeTheme;

			RefreshControls();
		}

		public void AppActivate()
		{
			if (IsDisposed) return;

			if (InvokeRequired)
			{
				if (_appActivateFunc == null) _appActivateFunc = new VoidDelegate(AppActivate);
				BeginInvoke(_appActivateFunc);
				return;
			}

			ShowFromTray();
			SetForegroundWindow(this.Handle);
		}

		public static void AppActivateExternal()
		{
			try
			{
				var client = WallSwitchServiceManager.GetClient();
				if (client != null)
				{
					client.Activate();
				}
			}
			catch (Exception)
			{ }
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

			RefreshTransparency();

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
			SaveSettings();

			return true;
		}

		private void RefreshLocations()
		{
			lstLocations.Items.Clear();

			_locationImages.Images.Clear();
			lstLocations.SmallImageList = _locationImages;

			foreach (var loc in _currentTheme.Locations) AddLocationItem(loc);
		}

		private void Apply_Click(object sender, EventArgs e)
		{
			try
			{
				if (SaveControls(true)) SaveSettings();
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
		private void LoadSettings()
		{
			try
			{
				var fileName = ConfigFileName;
				if (File.Exists(fileName))
				{
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load(fileName);

					XmlElement xmlSettings = (XmlElement)xmlDoc.SelectSingleNode("Settings");
					if (xmlSettings != null)
					{
						Settings.Load(xmlSettings);
						ImageCache.LoadSettings(xmlSettings);

						foreach (XmlElement xmlHotKeys in xmlSettings.SelectNodes("HotKeys"))
						{
							LoadHotKeys(xmlHotKeys);
						}

						foreach (XmlElement xmlTheme in xmlSettings.SelectNodes("Theme"))
						{
							try
							{
								Guid id = Guid.Empty;
								if (xmlTheme.HasAttribute("ID"))
								{
									id = Guid.Parse(xmlTheme.GetAttribute("ID"));
								}

								Theme theme = new Theme(id);
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
				}
				else
				{
					var theme = new Theme(Guid.NewGuid());
					theme.Name = Res.DefaultTheme;
					_themes.Add(theme);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Error_LoadSettings);
			}
			
		}

		private void SaveSettings()
		{
			try
			{
				// Write to a buffer in memory before overwriting the actual file.
				var memStream = new MemoryStream();
				var xmlSettings = new XmlWriterSettings();
				xmlSettings.Indent = true;
				xmlSettings.Encoding = Encoding.UTF8;
				using (var xml = XmlWriter.Create(memStream, xmlSettings))
				{
					xml.WriteStartDocument();
					xml.WriteStartElement("Settings");
					Settings.Save(xml);

					xml.WriteStartElement("HotKeys");
					SaveHotKeys(xml);
					xml.WriteEndElement();	// HotKeys

					foreach (Theme theme in _themes)
					{
						xml.WriteStartElement("Theme");
						xml.WriteAttributeString("ID", theme.ID.ToString());
						theme.Save(xml);
						xml.WriteEndElement();	// Theme
					}

					ImageCache.SaveSettings(xml);

					xml.WriteEndElement();	// Settings
					xml.WriteEndDocument();
				}

				// Save buffer to the file.
				var data = new byte[memStream.Length];
				memStream.Seek(0, SeekOrigin.Begin);
				memStream.Read(data, 0, (int)memStream.Length);
				File.WriteAllBytes(ConfigFileName, data);

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
					SaveSettings();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
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

					Theme theme = new Theme(Guid.NewGuid());
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

					if (Dirty && !theme.Equals(_currentTheme) && _currentTheme != null)
					{
						switch (MessageBox.Show(this, Res.Confirm_ChangeDirtyTheme, Res.Confirm_ChangeDirtyTheme_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
						{
							case DialogResult.Yes:
								if (!SaveControls(true)) cancelChange = true;
								break;
							case DialogResult.No:
								break;
							case DialogResult.Cancel:
								cancelChange = true;
								break;
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
					DetachTheme(deleteTheme);
					_themes.Remove(deleteTheme);

					// Remove the theme out of the combo box, and use the combo-box's trigger to select the next theme.
					cmbTheme.Items.RemoveAt(index);
					if (index >= cmbTheme.Items.Count) index--;
					cmbTheme.SelectedIndex = index;

					if (deleteTheme.IsActive)
					{
						Theme newActiveTheme = (Theme)((TagString)cmbTheme.Items[cmbTheme.SelectedIndex]).Tag;
						ActivateTheme(newActiveTheme);
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
				if (Dirty)
				{
					switch (MessageBox.Show(this, Res.DuplicateThemeDirtyPrompt, Res.DuplicateThemeDirtyTitle,
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
					{
						case DialogResult.Yes:
							if (!SaveControls(true)) cancel = true;
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							cancel = true;
							break;
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
				ActivateTheme(_currentTheme);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		public void OnActivateTheme(Theme setTheme)
		{
			try
			{
				if (!setTheme.IsActive) ActivateTheme(setTheme);
				else
				{
					var switchThread = Program.SwitchThread;
					if (switchThread != null) switchThread.SwitchNow(SwitchDir.Next);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ActivateTheme(Theme setTheme)
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

				switchThread.SwitchNow(SwitchDir.Next);

				SaveSettings();
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
						ActivateTheme((Theme)tag);
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
				var icon = location.GetIcon();
				if (icon != null)
				{
					lvi.ImageIndex = _locationImages.Images.Count;
					_locationImages.Images.Add(icon);
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
		}

		private void btnAddFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string path = dlg.SelectedPath;
					string pathLower = path.ToLower();

					foreach (ListViewItem existingLvi in lstLocations.Items)
					{
						if (existingLvi.Text.ToLower() == pathLower)
						{
							this.ShowError(Res.Error_DuplicateFolder);
							return;
						}
					}

					AddLocationItem(new Location(LocationType.Directory, path));
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
								this.ShowError(Res.Error_ImageFileMissing);
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
		#endregion

		#region Thread Control
		private void ciSwitchNow_Click(object sender, EventArgs e)
		{
			try
			{
				var switchThread = Program.SwitchThread;
				if (switchThread != null) switchThread.SwitchNow(SwitchDir.Next);
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
				if (switchThread != null) switchThread.SwitchNow(SwitchDir.Prev);
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
				if (switchThread != null) switchThread.SwitchNow(SwitchDir.Next);
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
				if (switchThread != null)  switchThread.SwitchNow(SwitchDir.Prev);
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
				ClearExpiredCache();
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ClearHistory()
		{
			foreach (var theme in _themes) theme.ClearHistory();
			c_historyTab.Clear();
			ClearExpiredCache();
		}

		private void miClearHistory_Click(object sender, EventArgs e)
		{
			try
			{
				ClearHistory();

				var switchThread = Program.SwitchThread;
				if (switchThread != null) switchThread.SwitchNow(SwitchDir.Clear);
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
			
		}

		private void ClearExpiredCache()
		{
			// Create a list of all images currently in the history.
			// These are the files that we don't want to delete.
			var keepLocations = new List<string>();
			foreach (var theme in _themes)
			{
				foreach (var historyItem in theme.History)
				{
					foreach (var loc in historyItem)
					{
						keepLocations.Add(loc.ImageRec.Location);
					}
				}
			}

			keepLocations.AddRange(from h in c_historyTab.History select h.Location);

			// Tell the image cache object to delete all others.
			ImageCache.ClearExpiredCache(keepLocations);
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

		private const string k_nextImageHotKey = "NextImage";
		private const string k_prevImageHotKey = "PrevImage";
		private const string k_pauseHotKey = "Pause";
		private const string k_clearHistoryHotKey = "ClearHistory";
		private const string k_showWindowHotKey = "ShowWindow";

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

		private void SaveHotKeys(XmlWriter xml)
		{
			_nextImageHotKey.SaveXml(xml, k_nextImageHotKey);
			_prevImageHotKey.SaveXml(xml, k_prevImageHotKey);
			_pauseHotKey.SaveXml(xml, k_pauseHotKey);
			_clearHistoryHotKey.SaveXml(xml, k_clearHistoryHotKey);
			_showWindowHotKey.SaveXml(xml, k_showWindowHotKey);
		}

		private void LoadHotKeys(XmlElement xmlRoot)
		{
			foreach (XmlElement element in xmlRoot.SelectNodes(HotKey.XmlElementName))
			{
				switch (element.GetAttribute(HotKey.XmlNameAttribute))
				{
					case k_nextImageHotKey:
						_nextImageHotKey.LoadXml(element);
						break;
					case k_prevImageHotKey:
						_prevImageHotKey.LoadXml(element);
						break;
					case k_pauseHotKey:
						_pauseHotKey.LoadXml(element);
						break;
					case k_clearHistoryHotKey:
						_clearHistoryHotKey.LoadXml(element);
						break;
					case k_showWindowHotKey:
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
				if (switchThread != null) switchThread.SwitchNow(SwitchDir.Next);
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
				if (switchThread != null) switchThread.SwitchNow(SwitchDir.Prev);
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
				ClearHistory();
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
				foreach (var img in (from i in e.Images select i.ImageRec).Distinct())
				{
					c_historyTab.AddHistory(new HistoryItem(img));
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
						this.ShowError(Res.Error_ImageFileMissing);
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
						this.ShowError(Res.Error_ImageFileMissing);
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
					this.ShowError(Res.Error_ImageFileMissing);
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
				ClearHistory();
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
				this.ShowError(Res.Error_ImageFileMissing);
			}
			else if (MessageBox.Show(this, Res.Confirm_DeleteHistoryFile, Res.Confirm_DeleteHistoryFile_Caption,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
				== DialogResult.Yes)
			{
				FileUtil.RecycleFile(fileName);
				c_historyTab.RemoveItem(item);
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

		private void clrBackTop_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{

		}

		private void clrBackBottom_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{

		}

		private void c_borderColor_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{

		}

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
	}
}
