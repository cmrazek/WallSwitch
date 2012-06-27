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

namespace WallSwitch
{
	public partial class MainWindow : Form
	{
		#region Variables
		private List<Theme> _themes = new List<Theme>();
		private Theme _currentTheme = null;
		private bool _reallyClose = false;
		private bool _refreshing = false;
		private bool _dirty = false;
		private SwitchThread _switchThread = null;
		private ImageList _locationImages = new ImageList();
		private bool _winStart = false;
		private HotKey _hotKey = new HotKey();
		private static MainWindow _mainWindow = null;

		private delegate void VoidDelegate();
		private VoidDelegate _appActivateFunc = null;
		#endregion

		#region PInvoke
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool BringWindowToTop(IntPtr hWnd);

		private static int WM_HOTKEY = 0x0312;

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
		private const int k_modeSequential = 0;
		private const int k_modeRandom = 1;
		private const int k_modeCollage = 2;

		// Hotkeys
		private const int k_hotkeySwitchNext = 1;
		#endregion

		#region Window Management
		public MainWindow(string[] args)
		{
			foreach (string arg in args)
			{
				if (arg.ToLower() == "-winstart")
				{
					_winStart = true;
					HideToTray();
				}
			}

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

				// Start the switch thread
				_switchThread = new SwitchThread();
				_switchThread.Start(activeTheme);
				_switchThread.Switching += new SwitchThread.SwitchEventHandler(_switchThread_Switching);
				_switchThread.Switched += new SwitchThread.SwitchEventHandler(_switchThread_Switched);

				// Tell the active theme that it's time to activate.
				activeTheme.OnAppLoadActivate();

				// Update all the controls
				RefreshControls();
				Text = Res.AppName;
				trayIcon.Visible = true;
				EnableControls();
				lstLocations_Resize(this, null);

				if (_winStart) HideToTray();

				RegisterHotKeys();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			UnregisterHotKeys();
		}

		private void Shutdown()
		{
			SaveSettings();

			_reallyClose = true;
			Close();

			if (_switchThread != null && _switchThread.IsAlive) _switchThread.Kill();
		}

		private void cmExit_Click(object sender, EventArgs e)
		{
			try
			{
				Shutdown();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miFileExit_Click(object sender, EventArgs e)
		{
			try
			{
				Shutdown();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				Close();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void trayIcon_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				ShowFromTray();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!_reallyClose && e.CloseReason == CloseReason.UserClosing)
				{
					e.Cancel = true;
					WindowState = FormWindowState.Minimized;
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
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
				if (m.Msg == WM_HOTKEY)
				{
					OnHotKey(m.WParam.ToInt32());
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miToolsStartWithWindows_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.StartWithWindows = !Settings.StartWithWindows;
				miToolsStartWithWindows.Checked = Settings.StartWithWindows;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
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
		}

		private void ShowFromTray()
		{
			Visible = true;
			WindowState = FormWindowState.Normal;
			BringWindowToTop(this.Handle);

			// Select the active theme.
			Theme activeTheme = GetActiveTheme();
			if (activeTheme != null && !activeTheme.Equals(_currentTheme)) _currentTheme = activeTheme;

			RefreshControls();
		}

		public void AppActivate()
		{
			if (InvokeRequired)
			{
				if (_appActivateFunc == null) _appActivateFunc = new VoidDelegate(AppActivate);
				Invoke(_appActivateFunc);
				return;
			}

			ShowFromTray();
			SetForegroundWindow(this.Handle);

			//WindowState = _windowState;
			//Bounds = _windowBounds;

			//SetForegroundWindow(this.Handle);
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
				case ThemePeriod.Seconds:
					cmbThemePeriod.SelectedIndex = k_periodSeconds;
					break;
				case ThemePeriod.Minutes:
					cmbThemePeriod.SelectedIndex = k_periodMinutes;
					break;
				case ThemePeriod.Hours:
					cmbThemePeriod.SelectedIndex = k_periodHours;
					break;
				case ThemePeriod.Days:
					cmbThemePeriod.SelectedIndex = k_periodDays;
					break;
				default:
					cmbThemePeriod.SelectedIndex = k_periodMinutes;
					break;
			}

			// Theme mode

			switch(_currentTheme.Mode)
			{
				case ThemeMode.Sequential:
					cmbThemeMode.SelectedIndex = k_modeSequential;
					break;
				case ThemeMode.Random:
					cmbThemeMode.SelectedIndex = k_modeRandom;
					break;
				case ThemeMode.Collage:
					cmbThemeMode.SelectedIndex = k_modeCollage;
					break;
				default:
					cmbThemeMode.SelectedIndex = k_modeRandom;
					break;
			}

			// Background Colors
			clrBackTop.Color = _currentTheme.BackColorTop;
			clrBackBottom.Color = _currentTheme.BackColorBottom;

			// Image Size
			trkImageSize.Value = _currentTheme.ImageSize;
			UpdateImageSizeDisplay();

			chkSeparateMonitors.Checked = _currentTheme.SeparateMonitors;
			_hotKey.Copy(_currentTheme.HotKey);
			SetHotKeyText();

			cmbImageFit.SelectedIndex = (int)_currentTheme.ImageFit;

			trkOpacity.Value = _currentTheme.BackOpacity;
			UpdateBackOpacityDisplay();

			trkFeather.Value = _currentTheme.Feather;
			UpdateFeatherDisplay();

			Dirty = false;

			RefreshImageList();
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
					txtThemeFreq.Focus();
					ShowError(Res.Error_InvalidThemeFreq);
				}
				return false;
			}

			ThemePeriod period = ThemePeriod.Minutes;
			switch(cmbThemePeriod.SelectedIndex)
			{
				case k_periodSeconds:
					period = ThemePeriod.Seconds;
					break;
				case k_periodMinutes:
					period = ThemePeriod.Minutes;
					break;
				case k_periodHours:
					period = ThemePeriod.Hours;
					break;
				case k_periodDays:
					period = ThemePeriod.Days;
					break;
				default:
					if (showErrors)
					{
						cmbThemePeriod.Focus();
						ShowError(Res.Error_InvalidThemePeriod);
					}
					return false;
			}

			ThemeMode mode = ThemeMode.Sequential;
			switch(cmbThemeMode.SelectedIndex)
			{
				case k_modeSequential:
					mode = ThemeMode.Sequential;
					break;
				case k_modeRandom:
					mode = ThemeMode.Random;
					break;
				case k_modeCollage:
					mode = ThemeMode.Collage;
					break;
				default:
					if (showErrors)
					{
						cmbThemeMode.Focus();
						ShowError(Res.Error_InvalidThemeOrder);
					}
					return false;
			}

			_currentTheme.Frequency = freq;
			_currentTheme.Period = period;
			_currentTheme.Mode = mode;
			_currentTheme.BackColorTop = clrBackTop.Color;
			_currentTheme.BackColorBottom = clrBackBottom.Color;
			_currentTheme.ImageSize = trkImageSize.Value;

			List<String> locations = new List<string>(lstLocations.Items.Count);
			foreach (ListViewItem lvi in lstLocations.Items) locations.Add(lvi.Text);
			_currentTheme.Locations = locations.ToArray();

			_currentTheme.SeparateMonitors = chkSeparateMonitors.Checked;
			if (!_currentTheme.HotKey.Equals(_hotKey))
			{
				// Hot key is changing. Need to reregister.
				_currentTheme.HotKey = _hotKey;
				_currentTheme.HotKey.Register(this);
			}
			else
			{
				_currentTheme.HotKey = _hotKey;
			}

			_currentTheme.ImageFit = (ImageFit)cmbImageFit.SelectedIndex;
			_currentTheme.BackOpacity = trkOpacity.Value;
			_currentTheme.Feather = trkFeather.Value;

			Dirty = false;
			EnableControls();
			SaveSettings();

			return true;
		}

		private void RefreshImageList()
		{
			lstLocations.Items.Clear();

			_locationImages.Images.Clear();
			lstLocations.SmallImageList = _locationImages;

			foreach (string loc in _currentTheme.Locations) AddImageItem(loc);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				if (SaveControls(true)) SaveSettings();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void miFileSave_Click(object sender, EventArgs e)
		{
			btnApply_Click(sender, e);
		}

		private void ControlChanged(object sender, EventArgs e)
		{
			if (!_refreshing) Dirty = true;
		}

		private void EnableControls()
		{
			btnApply.Enabled = Dirty;
			btnActivate.Enabled = _switchThread == null || _switchThread.Theme == null || !_switchThread.Theme.Equals(_currentTheme);
			btnDeleteTheme.Enabled = miFileDeleteTheme.Enabled = cmbTheme.Items.Count > 1;

			Theme activeTheme = GetActiveTheme();
			btnPrevious.Enabled = activeTheme != null && activeTheme.CanGoPrev && !_switchThread.IsSwitching;
			btnSwitchNow.Enabled = activeTheme != null && !_switchThread.IsSwitching;

			grpCollageDisplay.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;

			//lblImageSize.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;
			//trkImageSize.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;
			//lblImageSizeDisplay.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;

			//lblOpacity.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;
			//trkOpacity.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;
			//lblOpacityDisplay.Visible = cmbThemeMode.SelectedIndex == k_modeCollage;

			chkSeparateMonitors.Visible = Screen.AllScreens.Length > 1;
			cmbImageFit.Visible = cmbThemeMode.SelectedIndex != k_modeCollage;
		}

		private void locationsMenu_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				cmDeleteLocation.Visible = lstLocations.SelectedItems.Count > 0;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void clrBackTop_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void clrBackBottom_ColorChanged(object sender, ColorSample.ColorChangedEventArgs e)
		{
			try
			{
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void trayMenu_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				PopulateThemeContextMenu();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void lstLocations_Resize(object sender, EventArgs e)
		{
			try
			{
				colLocation.Width = lstLocations.ClientSize.Width - 20;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void trkImageSize_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateImageSizeDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void UpdateImageSizeDisplay()
		{
			lblImageSizeDisplay.Text = String.Format(Res.ImageSizePercent, trkImageSize.Value);
		}

		private void trkOpacity_Scroll(object sender, EventArgs e)
		{
			try
			{
				UpdateBackOpacityDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void UpdateBackOpacityDisplay()
		{
			lblOpacityDisplay.Text = String.Format(Res.BackOpacityPercent, trkOpacity.Value);
		}

		private void menuTools_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				miToolsStartWithWindows.Checked = Settings.StartWithWindows;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void trkFeather_Scroll(object sender, EventArgs e)
		{
			try
			{

				UpdateFeatherDisplay();
				Dirty = true;
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void UpdateFeatherDisplay()
		{
			lblFeatherDisplay.Text = String.Format(Res.FeatherWidth, trkFeather.Value);
		}
		#endregion

		#region Settings
		private void LoadSettings()
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(ConfigFileName);

				XmlElement xmlSettings = (XmlElement)xmlDoc.SelectSingleNode("Settings");
				if (xmlSettings != null)
				{
					Settings.Load(xmlSettings);

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
						}
						catch (Exception ex)
						{
							ShowError(ex, Res.Error_LoadTheme);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Error_LoadSettings);
			}
			
		}

		private void SaveSettings()
		{
			XmlTextWriter xml = null;
			
			try
			{
				xml = new XmlTextWriter(ConfigFileName, Encoding.UTF8);
				xml.Formatting = Formatting.Indented;

				xml.WriteStartDocument();
				xml.WriteStartElement("Settings");
				Settings.Save(xml);

				foreach (Theme theme in _themes)
				{
					xml.WriteStartElement("Theme");
					xml.WriteAttributeString("ID", theme.ID.ToString());
					theme.Save(xml);
					xml.WriteEndElement();	// Theme
				}

				xml.WriteEndElement();	// Settings
				xml.WriteEndDocument();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Error_SaveSettings);
			}
			finally
			{
				if (xml != null) xml.Close();
			}
		}

		private string ConfigFileName
		{
			get
			{
				string dir = String.Format(Res.SettingsDir, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
				return dir + Path.DirectorySeparatorChar + Res.SettingsFileName;
			}
		}
		#endregion

		#region Errors
		private void ShowError(string message)
		{
			Log.Write(LogLevel.Error, "Error: {0}", message);
			MessageBox.Show(String.Format(Res.Error_Content, message), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void ShowError(Exception ex, string message)
		{
			Log.Write(ex, "Error: {0}", message);
			MessageBox.Show(String.Format(Res.Error_ContentEx, message, ex.Message), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		#endregion

		#region Theme Selection
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

					_themes.Add(theme);
					_currentTheme = theme;
					Dirty = true;
					RefreshControls();
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Error_NewTheme);
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Error_RenameTheme);
			}
		}

		private void btnDeleteTheme_Click(object sender, EventArgs e)
		{
			try
			{
				if (_themes.Count == 1)
				{
					ShowError(Res.Error_CantDeleteLastTheme);
					return;
				}

				int index = cmbTheme.SelectedIndex;
				Theme deleteTheme = (Theme)((TagString)cmbTheme.Items[index]).Tag;

				if (MessageBox.Show(this, String.Format(Res.Confirm_DeleteTheme, deleteTheme.Name),
					Res.Confirm_DeleteTheme_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
					== DialogResult.Yes)
				{
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ActivateTheme(Theme setTheme)
		{
			_switchThread.Theme = setTheme;
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

			_switchThread.SwitchNow(SwitchDir.Next);

			SaveSettings();
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
			}
		}
		#endregion

		#region Location Selection
		private void AddImageItem(string location)
		{
			ListViewItem lvi = new ListViewItem(location);
			lstLocations.Items.Add(lvi);

			try
			{
				IconReader ir = new IconReader();
				Icon icon = ir.GetFileIcon(location);
				if (icon != null)
				{
					lvi.ImageIndex = _locationImages.Images.Count;
					_locationImages.Images.Add(icon);
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, String.Format(Res.Exception_GetIcon, location));
				Log.Write(LogLevel.Error, String.Format(Res.Exception_GetIcon, location) + ex.ToString());
				throw;
			}
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
							ShowError(Res.Error_DuplicateFolder);
							return;
						}
					}

					AddImageItem(path);
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_AddFolder);
			}
		}

		private void btnAddImage_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = Res.OpenImageDlgFilter;
				dlg.FilterIndex = 1;
				dlg.Multiselect = true;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					foreach (string fileName in dlg.FileNames) AddImageItem(fileName);
					Dirty = true;
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_AddFile);
			}
		}

		private void lstLocations_ItemActivate(object sender, EventArgs e)
		{
			try
			{
				foreach (ListViewItem lvi in lstLocations.SelectedItems)
				{
					string location = lvi.Text;
					if (Directory.Exists(location) || File.Exists(location))
					{
						Process.Start(lvi.Text);
					}
					else
					{
						ShowError(Res.Error_LocationMissing);
					}
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void cmDeleteLocation_Click(object sender, EventArgs e)
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_Generic);
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
							(File.Exists(file) && Theme.IsImageFile(file)))
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
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				foreach (string file in files)
				{
					if (Directory.Exists(file) ||
						(File.Exists(file) && Theme.IsImageFile(file)))
					{
						AddImageItem(file);
						Dirty = true;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception on drag-drop.");
			}
		}
		#endregion

		#region Thread Control
		private void ciSwitchNow_Click(object sender, EventArgs e)
		{
			try
			{
				_switchThread.SwitchNow(SwitchDir.Next);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_SwitchNowTray);
			}
		}

		private void ciSwitchPrev_Click(object sender, EventArgs e)
		{
			try
			{
				_switchThread.SwitchNow(SwitchDir.Prev);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_SwitchPrevTray);
				throw;
			}
		}

		private void btnSwitchNow_Click(object sender, EventArgs e)
		{
			try
			{
				_switchThread.SwitchNow(SwitchDir.Next);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_SwitchNow);
			}
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			try
			{
				_switchThread.SwitchNow(SwitchDir.Prev);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_SwitchPrev);
			}
		}

		private SwitchThread.SwitchEventHandler _switchThread_Switching_func = null;
		void _switchThread_Switching(object sender, EventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					// If not in the main thread, then invoke it.
					if (_switchThread_Switching_func == null) _switchThread_Switching_func = new SwitchThread.SwitchEventHandler(_switchThread_Switching);
					Invoke(_switchThread_Switching_func, new object[] { sender, e });
					return;
				}

				EnableControls();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private SwitchThread.SwitchEventHandler _switchThread_Switched_func = null;
		void _switchThread_Switched(object sender, EventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					// If not in the main thread, then invoke it.
					if (_switchThread_Switched_func == null) _switchThread_Switched_func = new SwitchThread.SwitchEventHandler(_switchThread_Switched);
					Invoke(_switchThread_Switched_func, new object[] { sender, e });
					return;
				}

				EnableControls();
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void ClearHistory()
		{
			if (_currentTheme != null) _currentTheme.ClearHistory();
		}

		private void miClearHistory_Click(object sender, EventArgs e)
		{
			try
			{
				ClearHistory();
				_switchThread.SwitchNow(SwitchDir.Clear);
				//MessageBox.Show(this, Res.HistoryCleared, Res.HistoryCleared_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
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
				ShowError(ex, Res.Exception_AboutDialog);
			}
		}
		#endregion

		#region HotKeys
		private void txtHotKey_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
					case Keys.Back:
					case Keys.Delete:
						if (_hotKey.IsEnabled)
						{
							_hotKey.Clear();
							SetHotKeyText();
							e.Handled = true;
							Dirty = true;
						}
						break;

					case Keys.Menu:
					case Keys.LMenu:
					case Keys.RMenu:
					case Keys.Shift:
					case Keys.ShiftKey:
					case Keys.LShiftKey:
					case Keys.RShiftKey:
					case Keys.Control:
					case Keys.ControlKey:
					case Keys.LControlKey:
					case Keys.RControlKey:
						break;

					default:
						if (_hotKey.Detect(e))
						{
							SetHotKeyText();
							e.Handled = true;
							Dirty = true;
						}
						break;
				}
			}
			catch (Exception ex)
			{
				ShowError(ex, Res.Exception_Generic);
			}
		}

		private void SetHotKeyText()
		{
			txtHotKey.Text = _hotKey.ToString();
		}

		private void RegisterHotKeys()
		{
			foreach (Theme theme in _themes) theme.HotKey.Register(this);
		}

		private void UnregisterHotKeys()
		{
			foreach (Theme theme in _themes) theme.HotKey.Unregister();
		}

		private void OnHotKey(int id)
		{
			Log.Write(LogLevel.Debug, String.Format("Received hot key event ID [{0}].", id));

			foreach (Theme theme in _themes)
			{
				if (id == theme.HotKey.ID)
				{
					if (!theme.IsActive) ActivateTheme(theme);
					else _switchThread.SwitchNow(SwitchDir.Next);
				}
			}
		}
		#endregion

		
	}
}
