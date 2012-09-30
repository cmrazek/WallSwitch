namespace WallSwitch
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.lstLocations = new System.Windows.Forms.ListView();
			this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colNextUpdate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmLocations = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciAddFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.ciAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationExplore = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.ciUpdateLocationNow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.ciDeleteLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.cmbTheme = new System.Windows.Forms.ComboBox();
			this.grpTheme = new System.Windows.Forms.GroupBox();
			this.btnRenameTheme = new System.Windows.Forms.Button();
			this.btnDeleteTheme = new System.Windows.Forms.Button();
			this.btnNewTheme = new System.Windows.Forms.Button();
			this.cmbImageFit = new System.Windows.Forms.ComboBox();
			this.lblOpacityDisplay = new System.Windows.Forms.Label();
			this.trkOpacity = new System.Windows.Forms.TrackBar();
			this.lblOpacity = new System.Windows.Forms.Label();
			this.chkSeparateMonitors = new System.Windows.Forms.CheckBox();
			this.lblImageSizeDisplay = new System.Windows.Forms.Label();
			this.lblImageSize = new System.Windows.Forms.Label();
			this.trkImageSize = new System.Windows.Forms.TrackBar();
			this.lblBackBottom = new System.Windows.Forms.Label();
			this.lblBackTop = new System.Windows.Forms.Label();
			this.lblFrequency = new System.Windows.Forms.Label();
			this.cmbThemePeriod = new System.Windows.Forms.ComboBox();
			this.lblMode = new System.Windows.Forms.Label();
			this.txtThemeFreq = new System.Windows.Forms.TextBox();
			this.btnAddImage = new System.Windows.Forms.Button();
			this.cmbThemeMode = new System.Windows.Forms.ComboBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnAddFolder = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmShowMainWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.ciTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmSwitchNow = new System.Windows.Forms.ToolStripMenuItem();
			this.ciSwitchPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmExit = new System.Windows.Forms.ToolStripMenuItem();
			this.btnActivate = new System.Windows.Forms.Button();
			this.btnSwitchNow = new System.Windows.Forms.Button();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileNewTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileRenameTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileDeleteTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.miFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.miFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.miToolsStartWithWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.miClearHistory = new System.Windows.Forms.ToolStripMenuItem();
			this.miHotKeys = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.tabThemeSettings = new System.Windows.Forms.TabControl();
			this.tabLocations = new System.Windows.Forms.TabPage();
			this.btnAddFeed = new System.Windows.Forms.Button();
			this.tabDisplay = new System.Windows.Forms.TabPage();
			this.c_colorEffectsGroupBox = new System.Windows.Forms.GroupBox();
			this.c_colorEffectCollageFadeRatioValue = new System.Windows.Forms.Label();
			this.c_colorEffectCollageFadeRatioTrackBar = new System.Windows.Forms.TrackBar();
			this.c_colorEffectCollageFade = new System.Windows.Forms.CheckBox();
			this.c_colorEffectCombo = new System.Windows.Forms.ComboBox();
			this.lblScalePct = new System.Windows.Forms.Label();
			this.txtMaxScale = new System.Windows.Forms.TextBox();
			this.chkLimitScale = new System.Windows.Forms.CheckBox();
			this.chkFadeTransition = new System.Windows.Forms.CheckBox();
			this.grpCollageDisplay = new System.Windows.Forms.GroupBox();
			this.lblFeatherDisplay = new System.Windows.Forms.Label();
			this.lblFeather = new System.Windows.Forms.Label();
			this.trkFeather = new System.Windows.Forms.TrackBar();
			this.clrBackTop = new WallSwitch.ColorSample();
			this.clrBackBottom = new WallSwitch.ColorSample();
			this.tabFrequency = new System.Windows.Forms.TabPage();
			this.c_activateThemeHotKey = new WallSwitch.HotKeyTextBox();
			this.c_activateThemeLabel = new System.Windows.Forms.Label();
			this.tabHistory = new System.Windows.Forms.TabPage();
			this.lstHistory = new WallSwitch.HistoryList();
			this.cmHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciOpenHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ciExploreHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.ciClearHistoryList = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnPause = new System.Windows.Forms.Button();
			this.cmLocations.SuspendLayout();
			this.grpTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).BeginInit();
			this.cmTray.SuspendLayout();
			this.mainMenu.SuspendLayout();
			this.tabThemeSettings.SuspendLayout();
			this.tabLocations.SuspendLayout();
			this.tabDisplay.SuspendLayout();
			this.c_colorEffectsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_colorEffectCollageFadeRatioTrackBar)).BeginInit();
			this.grpCollageDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkFeather)).BeginInit();
			this.tabFrequency.SuspendLayout();
			this.tabHistory.SuspendLayout();
			this.cmHistory.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstLocations
			// 
			this.lstLocations.AllowDrop = true;
			this.lstLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLocation,
            this.colNextUpdate});
			this.lstLocations.ContextMenuStrip = this.cmLocations;
			this.lstLocations.Location = new System.Drawing.Point(0, 0);
			this.lstLocations.Name = "lstLocations";
			this.lstLocations.Size = new System.Drawing.Size(418, 232);
			this.lstLocations.TabIndex = 0;
			this.toolTip1.SetToolTip(this.lstLocations, "Locations where images are retrieved");
			this.lstLocations.UseCompatibleStateImageBehavior = false;
			this.lstLocations.View = System.Windows.Forms.View.Details;
			this.lstLocations.ItemActivate += new System.EventHandler(this.lstLocations_ItemActivate);
			this.lstLocations.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstLocations_DragDrop);
			this.lstLocations.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstLocations_DragEnter);
			this.lstLocations.Resize += new System.EventHandler(this.lstLocations_Resize);
			// 
			// colLocation
			// 
			this.colLocation.Tag = "location";
			this.colLocation.Text = "Location";
			this.colLocation.Width = 290;
			// 
			// colNextUpdate
			// 
			this.colNextUpdate.Text = "Next Update";
			this.colNextUpdate.Width = 100;
			// 
			// cmLocations
			// 
			this.cmLocations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciAddFolder,
            this.ciAddImage,
            this.ciLocationExplore,
            this.toolStripMenuItem6,
            this.ciUpdateLocationNow,
            this.toolStripMenuItem5,
            this.ciDeleteLocation,
            this.ciLocationProperties});
			this.cmLocations.Name = "locationsMenu";
			this.cmLocations.Size = new System.Drawing.Size(141, 148);
			this.cmLocations.Opening += new System.ComponentModel.CancelEventHandler(this.cmLocations_Opening);
			// 
			// ciAddFolder
			// 
			this.ciAddFolder.Name = "ciAddFolder";
			this.ciAddFolder.Size = new System.Drawing.Size(140, 22);
			this.ciAddFolder.Text = "Add &Folder";
			this.ciAddFolder.Click += new System.EventHandler(this.ciAddFolder_Click);
			// 
			// ciAddImage
			// 
			this.ciAddImage.Name = "ciAddImage";
			this.ciAddImage.Size = new System.Drawing.Size(140, 22);
			this.ciAddImage.Text = "Add &Image";
			this.ciAddImage.Click += new System.EventHandler(this.ciAddImage_Click);
			// 
			// ciLocationExplore
			// 
			this.ciLocationExplore.Name = "ciLocationExplore";
			this.ciLocationExplore.Size = new System.Drawing.Size(140, 22);
			this.ciLocationExplore.Text = "&Explore";
			this.ciLocationExplore.Click += new System.EventHandler(this.ciLocationExplore_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(137, 6);
			// 
			// ciUpdateLocationNow
			// 
			this.ciUpdateLocationNow.Name = "ciUpdateLocationNow";
			this.ciUpdateLocationNow.Size = new System.Drawing.Size(140, 22);
			this.ciUpdateLocationNow.Text = "&Update Now";
			this.ciUpdateLocationNow.Click += new System.EventHandler(this.ciUpdateLocationNow_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(137, 6);
			// 
			// ciDeleteLocation
			// 
			this.ciDeleteLocation.Name = "ciDeleteLocation";
			this.ciDeleteLocation.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ciDeleteLocation.Size = new System.Drawing.Size(140, 22);
			this.ciDeleteLocation.Text = "&Delete";
			this.ciDeleteLocation.Click += new System.EventHandler(this.ciDeleteLocation_Click);
			// 
			// ciLocationProperties
			// 
			this.ciLocationProperties.Name = "ciLocationProperties";
			this.ciLocationProperties.Size = new System.Drawing.Size(140, 22);
			this.ciLocationProperties.Text = "&Properties";
			this.ciLocationProperties.Click += new System.EventHandler(this.ciLocationProperties_Click);
			// 
			// cmbTheme
			// 
			this.cmbTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTheme.FormattingEnabled = true;
			this.cmbTheme.Location = new System.Drawing.Point(6, 19);
			this.cmbTheme.Name = "cmbTheme";
			this.cmbTheme.Size = new System.Drawing.Size(305, 21);
			this.cmbTheme.Sorted = true;
			this.cmbTheme.TabIndex = 0;
			this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
			// 
			// grpTheme
			// 
			this.grpTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpTheme.Controls.Add(this.btnRenameTheme);
			this.grpTheme.Controls.Add(this.btnDeleteTheme);
			this.grpTheme.Controls.Add(this.btnNewTheme);
			this.grpTheme.Controls.Add(this.cmbTheme);
			this.grpTheme.Location = new System.Drawing.Point(12, 27);
			this.grpTheme.Name = "grpTheme";
			this.grpTheme.Size = new System.Drawing.Size(560, 50);
			this.grpTheme.TabIndex = 0;
			this.grpTheme.TabStop = false;
			this.grpTheme.Text = "Theme";
			// 
			// btnRenameTheme
			// 
			this.btnRenameTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRenameTheme.Location = new System.Drawing.Point(398, 17);
			this.btnRenameTheme.Name = "btnRenameTheme";
			this.btnRenameTheme.Size = new System.Drawing.Size(75, 23);
			this.btnRenameTheme.TabIndex = 3;
			this.btnRenameTheme.Text = "&Rename";
			this.toolTip1.SetToolTip(this.btnRenameTheme, "Rename this theme");
			this.btnRenameTheme.UseVisualStyleBackColor = true;
			this.btnRenameTheme.Click += new System.EventHandler(this.btnRenameTheme_Click);
			// 
			// btnDeleteTheme
			// 
			this.btnDeleteTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteTheme.Location = new System.Drawing.Point(479, 17);
			this.btnDeleteTheme.Name = "btnDeleteTheme";
			this.btnDeleteTheme.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteTheme.TabIndex = 2;
			this.btnDeleteTheme.Text = "Delete";
			this.toolTip1.SetToolTip(this.btnDeleteTheme, "Delete this theme");
			this.btnDeleteTheme.UseVisualStyleBackColor = true;
			this.btnDeleteTheme.Click += new System.EventHandler(this.btnDeleteTheme_Click);
			// 
			// btnNewTheme
			// 
			this.btnNewTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNewTheme.Location = new System.Drawing.Point(317, 17);
			this.btnNewTheme.Name = "btnNewTheme";
			this.btnNewTheme.Size = new System.Drawing.Size(75, 23);
			this.btnNewTheme.TabIndex = 1;
			this.btnNewTheme.Text = "New";
			this.toolTip1.SetToolTip(this.btnNewTheme, "Create a new theme");
			this.btnNewTheme.UseVisualStyleBackColor = true;
			this.btnNewTheme.Click += new System.EventHandler(this.btnNewTheme_Click);
			// 
			// cmbImageFit
			// 
			this.cmbImageFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbImageFit.FormattingEnabled = true;
			this.cmbImageFit.Items.AddRange(new object[] {
            "Original Size",
            "Stretch",
            "Fit",
            "Fill"});
			this.cmbImageFit.Location = new System.Drawing.Point(290, 6);
			this.cmbImageFit.Name = "cmbImageFit";
			this.cmbImageFit.Size = new System.Drawing.Size(141, 21);
			this.cmbImageFit.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmbImageFit, "Wallpaper sizing mode");
			this.cmbImageFit.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblOpacityDisplay
			// 
			this.lblOpacityDisplay.AutoSize = true;
			this.lblOpacityDisplay.Location = new System.Drawing.Point(245, 58);
			this.lblOpacityDisplay.Name = "lblOpacityDisplay";
			this.lblOpacityDisplay.Size = new System.Drawing.Size(15, 13);
			this.lblOpacityDisplay.TabIndex = 5;
			this.lblOpacityDisplay.Text = "%";
			// 
			// trkOpacity
			// 
			this.trkOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkOpacity.LargeChange = 20;
			this.trkOpacity.Location = new System.Drawing.Point(119, 56);
			this.trkOpacity.Maximum = 100;
			this.trkOpacity.Name = "trkOpacity";
			this.trkOpacity.Size = new System.Drawing.Size(120, 45);
			this.trkOpacity.SmallChange = 10;
			this.trkOpacity.TabIndex = 4;
			this.trkOpacity.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkOpacity, "Opacity of background used to fade out previous images");
			this.trkOpacity.Value = 50;
			this.trkOpacity.Scroll += new System.EventHandler(this.trkOpacity_Scroll);
			// 
			// lblOpacity
			// 
			this.lblOpacity.AutoSize = true;
			this.lblOpacity.Location = new System.Drawing.Point(6, 56);
			this.lblOpacity.Name = "lblOpacity";
			this.lblOpacity.Size = new System.Drawing.Size(107, 13);
			this.lblOpacity.TabIndex = 3;
			this.lblOpacity.Text = "Background Opacity:";
			// 
			// chkSeparateMonitors
			// 
			this.chkSeparateMonitors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSeparateMonitors.AutoSize = true;
			this.chkSeparateMonitors.Checked = true;
			this.chkSeparateMonitors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSeparateMonitors.Location = new System.Drawing.Point(9, 209);
			this.chkSeparateMonitors.Name = "chkSeparateMonitors";
			this.chkSeparateMonitors.Size = new System.Drawing.Size(179, 17);
			this.chkSeparateMonitors.TabIndex = 10;
			this.chkSeparateMonitors.Text = "Separate image for each monitor";
			this.toolTip1.SetToolTip(this.chkSeparateMonitors, "Display a separate image on each monitor");
			this.chkSeparateMonitors.UseVisualStyleBackColor = true;
			this.chkSeparateMonitors.CheckedChanged += new System.EventHandler(this.chkSeparateMonitors_CheckedChanged);
			// 
			// lblImageSizeDisplay
			// 
			this.lblImageSizeDisplay.AutoSize = true;
			this.lblImageSizeDisplay.Location = new System.Drawing.Point(245, 21);
			this.lblImageSizeDisplay.Name = "lblImageSizeDisplay";
			this.lblImageSizeDisplay.Size = new System.Drawing.Size(15, 13);
			this.lblImageSizeDisplay.TabIndex = 2;
			this.lblImageSizeDisplay.Text = "%";
			// 
			// lblImageSize
			// 
			this.lblImageSize.AutoSize = true;
			this.lblImageSize.Location = new System.Drawing.Point(6, 19);
			this.lblImageSize.Name = "lblImageSize";
			this.lblImageSize.Size = new System.Drawing.Size(62, 13);
			this.lblImageSize.TabIndex = 0;
			this.lblImageSize.Text = "Image Size:";
			// 
			// trkImageSize
			// 
			this.trkImageSize.BackColor = System.Drawing.SystemColors.Window;
			this.trkImageSize.LargeChange = 20;
			this.trkImageSize.Location = new System.Drawing.Point(119, 19);
			this.trkImageSize.Maximum = 100;
			this.trkImageSize.Minimum = 1;
			this.trkImageSize.Name = "trkImageSize";
			this.trkImageSize.Size = new System.Drawing.Size(120, 45);
			this.trkImageSize.SmallChange = 10;
			this.trkImageSize.TabIndex = 1;
			this.trkImageSize.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkImageSize, "Image size in relation to the screen");
			this.trkImageSize.Value = 50;
			this.trkImageSize.Scroll += new System.EventHandler(this.trkImageSize_Scroll);
			// 
			// lblBackBottom
			// 
			this.lblBackBottom.AutoSize = true;
			this.lblBackBottom.Location = new System.Drawing.Point(180, 36);
			this.lblBackBottom.Name = "lblBackBottom";
			this.lblBackBottom.Size = new System.Drawing.Size(43, 13);
			this.lblBackBottom.TabIndex = 5;
			this.lblBackBottom.Text = "Bottom:";
			// 
			// lblBackTop
			// 
			this.lblBackTop.AutoSize = true;
			this.lblBackTop.Location = new System.Drawing.Point(6, 36);
			this.lblBackTop.Name = "lblBackTop";
			this.lblBackTop.Size = new System.Drawing.Size(123, 13);
			this.lblBackTop.TabIndex = 3;
			this.lblBackTop.Text = "Background Color (Top):";
			// 
			// lblFrequency
			// 
			this.lblFrequency.AutoSize = true;
			this.lblFrequency.Location = new System.Drawing.Point(6, 9);
			this.lblFrequency.Name = "lblFrequency";
			this.lblFrequency.Size = new System.Drawing.Size(104, 13);
			this.lblFrequency.TabIndex = 0;
			this.lblFrequency.Text = "Change image every";
			// 
			// cmbThemePeriod
			// 
			this.cmbThemePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbThemePeriod.FormattingEnabled = true;
			this.cmbThemePeriod.Items.AddRange(new object[] {
            "Seconds",
            "Minutes",
            "Hours",
            "Days"});
			this.cmbThemePeriod.Location = new System.Drawing.Point(211, 6);
			this.cmbThemePeriod.Name = "cmbThemePeriod";
			this.cmbThemePeriod.Size = new System.Drawing.Size(75, 21);
			this.cmbThemePeriod.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmbThemePeriod, "Wallpaper change interval");
			this.cmbThemePeriod.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblMode
			// 
			this.lblMode.AutoSize = true;
			this.lblMode.Location = new System.Drawing.Point(6, 9);
			this.lblMode.Name = "lblMode";
			this.lblMode.Size = new System.Drawing.Size(37, 13);
			this.lblMode.TabIndex = 0;
			this.lblMode.Text = "Mode:";
			// 
			// txtThemeFreq
			// 
			this.txtThemeFreq.Location = new System.Drawing.Point(145, 6);
			this.txtThemeFreq.MaxLength = 5;
			this.txtThemeFreq.Name = "txtThemeFreq";
			this.txtThemeFreq.Size = new System.Drawing.Size(60, 20);
			this.txtThemeFreq.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtThemeFreq, "Wallpaper change interval");
			this.txtThemeFreq.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// btnAddImage
			// 
			this.btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddImage.Location = new System.Drawing.Point(425, 35);
			this.btnAddImage.Name = "btnAddImage";
			this.btnAddImage.Size = new System.Drawing.Size(120, 23);
			this.btnAddImage.TabIndex = 2;
			this.btnAddImage.Text = "Add &Image";
			this.toolTip1.SetToolTip(this.btnAddImage, "Add a single image file to the list");
			this.btnAddImage.UseVisualStyleBackColor = true;
			this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
			// 
			// cmbThemeMode
			// 
			this.cmbThemeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbThemeMode.FormattingEnabled = true;
			this.cmbThemeMode.Items.AddRange(new object[] {
            "Sequential",
            "Random",
            "Collage"});
			this.cmbThemeMode.Location = new System.Drawing.Point(143, 6);
			this.cmbThemeMode.Name = "cmbThemeMode";
			this.cmbThemeMode.Size = new System.Drawing.Size(141, 21);
			this.cmbThemeMode.TabIndex = 1;
			this.toolTip1.SetToolTip(this.cmbThemeMode, "Wallpaper display mode");
			this.cmbThemeMode.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(365, 347);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(120, 23);
			this.btnApply.TabIndex = 16;
			this.btnApply.Text = "&Save Theme";
			this.toolTip1.SetToolTip(this.btnApply, "Save these settings to the theme");
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnAddFolder
			// 
			this.btnAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFolder.Location = new System.Drawing.Point(425, 6);
			this.btnAddFolder.Name = "btnAddFolder";
			this.btnAddFolder.Size = new System.Drawing.Size(120, 23);
			this.btnAddFolder.TabIndex = 1;
			this.btnAddFolder.Text = "&Add Folder";
			this.toolTip1.SetToolTip(this.btnAddFolder, "Add a folder containing multiple images to the list");
			this.btnAddFolder.UseVisualStyleBackColor = true;
			this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(496, 347);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "&Close";
			this.toolTip1.SetToolTip(this.btnClose, "Minimize to task bar");
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenuStrip = this.cmTray;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "WallSwitch";
			this.trayIcon.Visible = true;
			this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
			// 
			// cmTray
			// 
			this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowMainWindow,
            this.ciTheme,
            this.toolStripSeparator1,
            this.cmSwitchNow,
            this.ciSwitchPrev,
            this.toolStripMenuItem1,
            this.cmExit});
			this.cmTray.Name = "trayMenu";
			this.cmTray.Size = new System.Drawing.Size(176, 126);
			this.cmTray.Opening += new System.ComponentModel.CancelEventHandler(this.trayMenu_Opening);
			// 
			// cmShowMainWindow
			// 
			this.cmShowMainWindow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmShowMainWindow.Name = "cmShowMainWindow";
			this.cmShowMainWindow.Size = new System.Drawing.Size(175, 22);
			this.cmShowMainWindow.Text = "&Show";
			this.cmShowMainWindow.Click += new System.EventHandler(this.cmShowMainWindow_Click);
			// 
			// ciTheme
			// 
			this.ciTheme.Name = "ciTheme";
			this.ciTheme.Size = new System.Drawing.Size(175, 22);
			this.ciTheme.Text = "&Theme";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
			// 
			// cmSwitchNow
			// 
			this.cmSwitchNow.Name = "cmSwitchNow";
			this.cmSwitchNow.Size = new System.Drawing.Size(175, 22);
			this.cmSwitchNow.Text = "&Next Wallpaper";
			this.cmSwitchNow.Click += new System.EventHandler(this.ciSwitchNow_Click);
			// 
			// ciSwitchPrev
			// 
			this.ciSwitchPrev.Name = "ciSwitchPrev";
			this.ciSwitchPrev.Size = new System.Drawing.Size(175, 22);
			this.ciSwitchPrev.Text = "&Previous Wallpaper";
			this.ciSwitchPrev.Click += new System.EventHandler(this.ciSwitchPrev_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 6);
			// 
			// cmExit
			// 
			this.cmExit.Name = "cmExit";
			this.cmExit.Size = new System.Drawing.Size(175, 22);
			this.cmExit.Text = "E&xit";
			this.cmExit.Click += new System.EventHandler(this.cmExit_Click);
			// 
			// btnActivate
			// 
			this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnActivate.Location = new System.Drawing.Point(48, 347);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(75, 23);
			this.btnActivate.TabIndex = 6;
			this.btnActivate.Text = "&Activate";
			this.toolTip1.SetToolTip(this.btnActivate, "Activate this theme");
			this.btnActivate.UseVisualStyleBackColor = true;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// btnSwitchNow
			// 
			this.btnSwitchNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSwitchNow.Location = new System.Drawing.Point(210, 347);
			this.btnSwitchNow.Name = "btnSwitchNow";
			this.btnSwitchNow.Size = new System.Drawing.Size(30, 23);
			this.btnSwitchNow.TabIndex = 7;
			this.btnSwitchNow.Text = "->";
			this.toolTip1.SetToolTip(this.btnSwitchNow, "Go to the next image");
			this.btnSwitchNow.UseVisualStyleBackColor = true;
			this.btnSwitchNow.Click += new System.EventHandler(this.btnSwitchNow_Click);
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
            this.menuHelp});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(584, 24);
			this.mainMenu.TabIndex = 8;
			this.mainMenu.Text = "menuStrip1";
			// 
			// menuFile
			// 
			this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileNewTheme,
            this.miFileRenameTheme,
            this.miFileDeleteTheme,
            this.toolStripMenuItem3,
            this.miFileSave,
            this.toolStripMenuItem2,
            this.miFileExit});
			this.menuFile.Name = "menuFile";
			this.menuFile.Size = new System.Drawing.Size(37, 20);
			this.menuFile.Text = "&File";
			// 
			// miFileNewTheme
			// 
			this.miFileNewTheme.Name = "miFileNewTheme";
			this.miFileNewTheme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.miFileNewTheme.Size = new System.Drawing.Size(198, 22);
			this.miFileNewTheme.Text = "&New Theme";
			this.miFileNewTheme.Click += new System.EventHandler(this.miFileNewTheme_Click);
			// 
			// miFileRenameTheme
			// 
			this.miFileRenameTheme.Name = "miFileRenameTheme";
			this.miFileRenameTheme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.miFileRenameTheme.Size = new System.Drawing.Size(198, 22);
			this.miFileRenameTheme.Text = "&Rename Theme";
			this.miFileRenameTheme.Click += new System.EventHandler(this.miFileRenameTheme_Click);
			// 
			// miFileDeleteTheme
			// 
			this.miFileDeleteTheme.Name = "miFileDeleteTheme";
			this.miFileDeleteTheme.Size = new System.Drawing.Size(198, 22);
			this.miFileDeleteTheme.Text = "&Delete Theme";
			this.miFileDeleteTheme.Click += new System.EventHandler(this.miFileDeleteTheme_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(195, 6);
			// 
			// miFileSave
			// 
			this.miFileSave.Name = "miFileSave";
			this.miFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.miFileSave.Size = new System.Drawing.Size(198, 22);
			this.miFileSave.Text = "&Save";
			this.miFileSave.Click += new System.EventHandler(this.miFileSave_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(195, 6);
			// 
			// miFileExit
			// 
			this.miFileExit.Name = "miFileExit";
			this.miFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.miFileExit.Size = new System.Drawing.Size(198, 22);
			this.miFileExit.Text = "E&xit";
			this.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);
			// 
			// menuTools
			// 
			this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miToolsStartWithWindows,
            this.miClearHistory,
            this.miHotKeys});
			this.menuTools.Name = "menuTools";
			this.menuTools.Size = new System.Drawing.Size(48, 20);
			this.menuTools.Text = "&Tools";
			this.menuTools.DropDownOpening += new System.EventHandler(this.menuTools_DropDownOpening);
			// 
			// miToolsStartWithWindows
			// 
			this.miToolsStartWithWindows.Name = "miToolsStartWithWindows";
			this.miToolsStartWithWindows.Size = new System.Drawing.Size(178, 22);
			this.miToolsStartWithWindows.Text = "&Start With Windows";
			this.miToolsStartWithWindows.Click += new System.EventHandler(this.miToolsStartWithWindows_Click);
			// 
			// miClearHistory
			// 
			this.miClearHistory.Name = "miClearHistory";
			this.miClearHistory.Size = new System.Drawing.Size(178, 22);
			this.miClearHistory.Text = "&Clear History";
			this.miClearHistory.Click += new System.EventHandler(this.miClearHistory_Click);
			// 
			// miHotKeys
			// 
			this.miHotKeys.Name = "miHotKeys";
			this.miHotKeys.Size = new System.Drawing.Size(178, 22);
			this.miHotKeys.Text = "&Hot Keys";
			this.miHotKeys.Click += new System.EventHandler(this.miHotKeys_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelpAbout});
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Size = new System.Drawing.Size(44, 20);
			this.menuHelp.Text = "&Help";
			// 
			// miHelpAbout
			// 
			this.miHelpAbout.Name = "miHelpAbout";
			this.miHelpAbout.Size = new System.Drawing.Size(107, 22);
			this.miHelpAbout.Text = "&About";
			this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrevious.Location = new System.Drawing.Point(12, 347);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(30, 23);
			this.btnPrevious.TabIndex = 9;
			this.btnPrevious.Text = "<-";
			this.toolTip1.SetToolTip(this.btnPrevious, "Go back to the previous image");
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// tabThemeSettings
			// 
			this.tabThemeSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabThemeSettings.Controls.Add(this.tabLocations);
			this.tabThemeSettings.Controls.Add(this.tabDisplay);
			this.tabThemeSettings.Controls.Add(this.tabFrequency);
			this.tabThemeSettings.Controls.Add(this.tabHistory);
			this.tabThemeSettings.Location = new System.Drawing.Point(12, 83);
			this.tabThemeSettings.Name = "tabThemeSettings";
			this.tabThemeSettings.SelectedIndex = 0;
			this.tabThemeSettings.Size = new System.Drawing.Size(560, 258);
			this.tabThemeSettings.TabIndex = 10;
			// 
			// tabLocations
			// 
			this.tabLocations.Controls.Add(this.btnAddFeed);
			this.tabLocations.Controls.Add(this.lstLocations);
			this.tabLocations.Controls.Add(this.btnAddFolder);
			this.tabLocations.Controls.Add(this.btnAddImage);
			this.tabLocations.Location = new System.Drawing.Point(4, 22);
			this.tabLocations.Name = "tabLocations";
			this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
			this.tabLocations.Size = new System.Drawing.Size(552, 232);
			this.tabLocations.TabIndex = 0;
			this.tabLocations.Text = "Images";
			this.tabLocations.UseVisualStyleBackColor = true;
			// 
			// btnAddFeed
			// 
			this.btnAddFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFeed.Location = new System.Drawing.Point(424, 64);
			this.btnAddFeed.Name = "btnAddFeed";
			this.btnAddFeed.Size = new System.Drawing.Size(120, 23);
			this.btnAddFeed.TabIndex = 3;
			this.btnAddFeed.Text = "Add &Feed";
			this.toolTip1.SetToolTip(this.btnAddFeed, "Add a RSS/ATOM feed to the list");
			this.btnAddFeed.UseVisualStyleBackColor = true;
			this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
			// 
			// tabDisplay
			// 
			this.tabDisplay.Controls.Add(this.c_colorEffectsGroupBox);
			this.tabDisplay.Controls.Add(this.lblScalePct);
			this.tabDisplay.Controls.Add(this.txtMaxScale);
			this.tabDisplay.Controls.Add(this.chkLimitScale);
			this.tabDisplay.Controls.Add(this.chkFadeTransition);
			this.tabDisplay.Controls.Add(this.grpCollageDisplay);
			this.tabDisplay.Controls.Add(this.cmbImageFit);
			this.tabDisplay.Controls.Add(this.chkSeparateMonitors);
			this.tabDisplay.Controls.Add(this.cmbThemeMode);
			this.tabDisplay.Controls.Add(this.lblMode);
			this.tabDisplay.Controls.Add(this.lblBackTop);
			this.tabDisplay.Controls.Add(this.lblBackBottom);
			this.tabDisplay.Controls.Add(this.clrBackTop);
			this.tabDisplay.Controls.Add(this.clrBackBottom);
			this.tabDisplay.Location = new System.Drawing.Point(4, 22);
			this.tabDisplay.Name = "tabDisplay";
			this.tabDisplay.Padding = new System.Windows.Forms.Padding(3);
			this.tabDisplay.Size = new System.Drawing.Size(552, 232);
			this.tabDisplay.TabIndex = 1;
			this.tabDisplay.Text = "Display";
			this.tabDisplay.UseVisualStyleBackColor = true;
			// 
			// c_colorEffectsGroupBox
			// 
			this.c_colorEffectsGroupBox.Controls.Add(this.c_colorEffectCollageFadeRatioValue);
			this.c_colorEffectsGroupBox.Controls.Add(this.c_colorEffectCollageFadeRatioTrackBar);
			this.c_colorEffectsGroupBox.Controls.Add(this.c_colorEffectCollageFade);
			this.c_colorEffectsGroupBox.Controls.Add(this.c_colorEffectCombo);
			this.c_colorEffectsGroupBox.Location = new System.Drawing.Point(9, 57);
			this.c_colorEffectsGroupBox.Name = "c_colorEffectsGroupBox";
			this.c_colorEffectsGroupBox.Size = new System.Drawing.Size(165, 144);
			this.c_colorEffectsGroupBox.TabIndex = 8;
			this.c_colorEffectsGroupBox.TabStop = false;
			this.c_colorEffectsGroupBox.Text = "Color Effects:";
			// 
			// c_colorEffectCollageFadeRatioValue
			// 
			this.c_colorEffectCollageFadeRatioValue.AutoSize = true;
			this.c_colorEffectCollageFadeRatioValue.Location = new System.Drawing.Point(131, 72);
			this.c_colorEffectCollageFadeRatioValue.Name = "c_colorEffectCollageFadeRatioValue";
			this.c_colorEffectCollageFadeRatioValue.Size = new System.Drawing.Size(15, 13);
			this.c_colorEffectCollageFadeRatioValue.TabIndex = 3;
			this.c_colorEffectCollageFadeRatioValue.Text = "%";
			// 
			// c_colorEffectCollageFadeRatioTrackBar
			// 
			this.c_colorEffectCollageFadeRatioTrackBar.BackColor = System.Drawing.SystemColors.Window;
			this.c_colorEffectCollageFadeRatioTrackBar.LargeChange = 20;
			this.c_colorEffectCollageFadeRatioTrackBar.Location = new System.Drawing.Point(6, 70);
			this.c_colorEffectCollageFadeRatioTrackBar.Maximum = 100;
			this.c_colorEffectCollageFadeRatioTrackBar.Name = "c_colorEffectCollageFadeRatioTrackBar";
			this.c_colorEffectCollageFadeRatioTrackBar.Size = new System.Drawing.Size(124, 45);
			this.c_colorEffectCollageFadeRatioTrackBar.SmallChange = 10;
			this.c_colorEffectCollageFadeRatioTrackBar.TabIndex = 2;
			this.c_colorEffectCollageFadeRatioTrackBar.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.c_colorEffectCollageFadeRatioTrackBar, "Amount of color effect to apply to background images");
			this.c_colorEffectCollageFadeRatioTrackBar.Value = 25;
			this.c_colorEffectCollageFadeRatioTrackBar.Scroll += new System.EventHandler(this.c_colorEffectCollageFadeRatioTrackBar_Scroll);
			// 
			// c_colorEffectCollageFade
			// 
			this.c_colorEffectCollageFade.AutoSize = true;
			this.c_colorEffectCollageFade.Location = new System.Drawing.Point(6, 47);
			this.c_colorEffectCollageFade.Name = "c_colorEffectCollageFade";
			this.c_colorEffectCollageFade.Size = new System.Drawing.Size(146, 17);
			this.c_colorEffectCollageFade.TabIndex = 1;
			this.c_colorEffectCollageFade.Text = "Fade background images";
			this.toolTip1.SetToolTip(this.c_colorEffectCollageFade, "If checked, older images will be faded out using this color effect, and the foreg" +
        "round image will be drawn normally.");
			this.c_colorEffectCollageFade.UseVisualStyleBackColor = true;
			this.c_colorEffectCollageFade.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_colorEffectCombo
			// 
			this.c_colorEffectCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.c_colorEffectCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_colorEffectCombo.FormattingEnabled = true;
			this.c_colorEffectCombo.Location = new System.Drawing.Point(6, 19);
			this.c_colorEffectCombo.Name = "c_colorEffectCombo";
			this.c_colorEffectCombo.Size = new System.Drawing.Size(153, 21);
			this.c_colorEffectCombo.TabIndex = 0;
			this.toolTip1.SetToolTip(this.c_colorEffectCombo, "Color effect to be applied to wallpaper.");
			this.c_colorEffectCombo.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblScalePct
			// 
			this.lblScalePct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblScalePct.AutoSize = true;
			this.lblScalePct.Location = new System.Drawing.Point(531, 210);
			this.lblScalePct.Name = "lblScalePct";
			this.lblScalePct.Size = new System.Drawing.Size(15, 13);
			this.lblScalePct.TabIndex = 13;
			this.lblScalePct.Text = "%";
			// 
			// txtMaxScale
			// 
			this.txtMaxScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMaxScale.Location = new System.Drawing.Point(475, 207);
			this.txtMaxScale.Name = "txtMaxScale";
			this.txtMaxScale.Size = new System.Drawing.Size(50, 20);
			this.txtMaxScale.TabIndex = 12;
			this.txtMaxScale.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// chkLimitScale
			// 
			this.chkLimitScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkLimitScale.AutoSize = true;
			this.chkLimitScale.Location = new System.Drawing.Point(343, 209);
			this.chkLimitScale.Name = "chkLimitScale";
			this.chkLimitScale.Size = new System.Drawing.Size(126, 17);
			this.chkLimitScale.TabIndex = 11;
			this.chkLimitScale.Text = "Limit image scaling to";
			this.chkLimitScale.UseVisualStyleBackColor = true;
			this.chkLimitScale.CheckedChanged += new System.EventHandler(this.chkLimitScale_CheckedChanged);
			// 
			// chkFadeTransition
			// 
			this.chkFadeTransition.AutoSize = true;
			this.chkFadeTransition.Location = new System.Drawing.Point(290, 35);
			this.chkFadeTransition.Name = "chkFadeTransition";
			this.chkFadeTransition.Size = new System.Drawing.Size(133, 17);
			this.chkFadeTransition.TabIndex = 7;
			this.chkFadeTransition.Text = "Cross-Fade Transitions";
			this.toolTip1.SetToolTip(this.chkFadeTransition, "Use smooth cross-fading between wallpapers (Windows 7 or higher)");
			this.chkFadeTransition.UseVisualStyleBackColor = true;
			this.chkFadeTransition.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// grpCollageDisplay
			// 
			this.grpCollageDisplay.Controls.Add(this.lblFeatherDisplay);
			this.grpCollageDisplay.Controls.Add(this.lblFeather);
			this.grpCollageDisplay.Controls.Add(this.trkFeather);
			this.grpCollageDisplay.Controls.Add(this.trkOpacity);
			this.grpCollageDisplay.Controls.Add(this.trkImageSize);
			this.grpCollageDisplay.Controls.Add(this.lblImageSize);
			this.grpCollageDisplay.Controls.Add(this.lblImageSizeDisplay);
			this.grpCollageDisplay.Controls.Add(this.lblOpacityDisplay);
			this.grpCollageDisplay.Controls.Add(this.lblOpacity);
			this.grpCollageDisplay.Location = new System.Drawing.Point(183, 57);
			this.grpCollageDisplay.Name = "grpCollageDisplay";
			this.grpCollageDisplay.Size = new System.Drawing.Size(363, 144);
			this.grpCollageDisplay.TabIndex = 9;
			this.grpCollageDisplay.TabStop = false;
			this.grpCollageDisplay.Text = "Collage Display";
			// 
			// lblFeatherDisplay
			// 
			this.lblFeatherDisplay.AutoSize = true;
			this.lblFeatherDisplay.Location = new System.Drawing.Point(245, 92);
			this.lblFeatherDisplay.Name = "lblFeatherDisplay";
			this.lblFeatherDisplay.Size = new System.Drawing.Size(33, 13);
			this.lblFeatherDisplay.TabIndex = 8;
			this.lblFeatherDisplay.Text = "pixels";
			// 
			// lblFeather
			// 
			this.lblFeather.AutoSize = true;
			this.lblFeather.Location = new System.Drawing.Point(6, 92);
			this.lblFeather.Name = "lblFeather";
			this.lblFeather.Size = new System.Drawing.Size(79, 13);
			this.lblFeather.TabIndex = 6;
			this.lblFeather.Text = "Feather Edges:";
			// 
			// trkFeather
			// 
			this.trkFeather.BackColor = System.Drawing.SystemColors.Window;
			this.trkFeather.LargeChange = 20;
			this.trkFeather.Location = new System.Drawing.Point(119, 92);
			this.trkFeather.Maximum = 100;
			this.trkFeather.Name = "trkFeather";
			this.trkFeather.Size = new System.Drawing.Size(120, 45);
			this.trkFeather.SmallChange = 10;
			this.trkFeather.TabIndex = 7;
			this.trkFeather.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkFeather, "Width of feathering at the edges of images");
			this.trkFeather.Value = 50;
			this.trkFeather.Scroll += new System.EventHandler(this.trkFeather_Scroll);
			// 
			// clrBackTop
			// 
			this.clrBackTop.BackColor = System.Drawing.Color.Black;
			this.clrBackTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackTop.Color = System.Drawing.Color.Black;
			this.clrBackTop.Location = new System.Drawing.Point(144, 33);
			this.clrBackTop.Name = "clrBackTop";
			this.clrBackTop.Size = new System.Drawing.Size(30, 18);
			this.clrBackTop.TabIndex = 4;
			this.toolTip1.SetToolTip(this.clrBackTop, "Top color of background gradient");
			this.clrBackTop.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.clrBackTop_ColorChanged);
			// 
			// clrBackBottom
			// 
			this.clrBackBottom.BackColor = System.Drawing.Color.Black;
			this.clrBackBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackBottom.Color = System.Drawing.Color.Black;
			this.clrBackBottom.Location = new System.Drawing.Point(229, 33);
			this.clrBackBottom.Name = "clrBackBottom";
			this.clrBackBottom.Size = new System.Drawing.Size(30, 18);
			this.clrBackBottom.TabIndex = 6;
			this.toolTip1.SetToolTip(this.clrBackBottom, "Bottom color of background gradient");
			this.clrBackBottom.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.clrBackBottom_ColorChanged);
			// 
			// tabFrequency
			// 
			this.tabFrequency.Controls.Add(this.c_activateThemeHotKey);
			this.tabFrequency.Controls.Add(this.c_activateThemeLabel);
			this.tabFrequency.Controls.Add(this.cmbThemePeriod);
			this.tabFrequency.Controls.Add(this.lblFrequency);
			this.tabFrequency.Controls.Add(this.txtThemeFreq);
			this.tabFrequency.Location = new System.Drawing.Point(4, 22);
			this.tabFrequency.Name = "tabFrequency";
			this.tabFrequency.Padding = new System.Windows.Forms.Padding(3);
			this.tabFrequency.Size = new System.Drawing.Size(552, 232);
			this.tabFrequency.TabIndex = 2;
			this.tabFrequency.Text = "Frequency";
			this.tabFrequency.UseVisualStyleBackColor = true;
			// 
			// c_activateThemeHotKey
			// 
			this.c_activateThemeHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_activateThemeHotKey.HotKey = null;
			this.c_activateThemeHotKey.Location = new System.Drawing.Point(145, 32);
			this.c_activateThemeHotKey.Name = "c_activateThemeHotKey";
			this.c_activateThemeHotKey.ReadOnly = true;
			this.c_activateThemeHotKey.Size = new System.Drawing.Size(141, 20);
			this.c_activateThemeHotKey.TabIndex = 4;
			this.c_activateThemeHotKey.HotKeyChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeLabel
			// 
			this.c_activateThemeLabel.AutoSize = true;
			this.c_activateThemeLabel.Location = new System.Drawing.Point(6, 35);
			this.c_activateThemeLabel.Name = "c_activateThemeLabel";
			this.c_activateThemeLabel.Size = new System.Drawing.Size(48, 13);
			this.c_activateThemeLabel.TabIndex = 3;
			this.c_activateThemeLabel.Text = "Hot Key:";
			// 
			// tabHistory
			// 
			this.tabHistory.Controls.Add(this.lstHistory);
			this.tabHistory.Location = new System.Drawing.Point(4, 22);
			this.tabHistory.Name = "tabHistory";
			this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabHistory.Size = new System.Drawing.Size(552, 232);
			this.tabHistory.TabIndex = 3;
			this.tabHistory.Text = "History";
			this.tabHistory.UseVisualStyleBackColor = true;
			// 
			// lstHistory
			// 
			this.lstHistory.ContextMenuStrip = this.cmHistory;
			this.lstHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstHistory.ImageToolTip = this.toolTip1;
			this.lstHistory.Location = new System.Drawing.Point(3, 3);
			this.lstHistory.Name = "lstHistory";
			this.lstHistory.Size = new System.Drawing.Size(546, 226);
			this.lstHistory.TabIndex = 0;
			this.lstHistory.SelectionChanged += new System.EventHandler(this.lstHistory_SelectionChanged);
			this.lstHistory.ItemActivated += new System.EventHandler<WallSwitch.HistoryList.ItemActivatedEventArgs>(this.lstHistory_ItemActivated);
			// 
			// cmHistory
			// 
			this.cmHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciOpenHistoryFile,
            this.ciExploreHistoryFile,
            this.toolStripMenuItem4,
            this.ciClearHistoryList});
			this.cmHistory.Name = "cmHistory";
			this.cmHistory.Size = new System.Drawing.Size(143, 76);
			// 
			// ciOpenHistoryFile
			// 
			this.ciOpenHistoryFile.Name = "ciOpenHistoryFile";
			this.ciOpenHistoryFile.Size = new System.Drawing.Size(142, 22);
			this.ciOpenHistoryFile.Text = "&Open File";
			this.ciOpenHistoryFile.Click += new System.EventHandler(this.ciOpenHistoryFile_Click);
			// 
			// ciExploreHistoryFile
			// 
			this.ciExploreHistoryFile.Name = "ciExploreHistoryFile";
			this.ciExploreHistoryFile.Size = new System.Drawing.Size(142, 22);
			this.ciExploreHistoryFile.Text = "&Explore File";
			this.ciExploreHistoryFile.Click += new System.EventHandler(this.ciExploreHistoryFile_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(139, 6);
			// 
			// ciClearHistoryList
			// 
			this.ciClearHistoryList.Name = "ciClearHistoryList";
			this.ciClearHistoryList.Size = new System.Drawing.Size(142, 22);
			this.ciClearHistoryList.Text = "&Clear History";
			this.ciClearHistoryList.Click += new System.EventHandler(this.ciClearHistoryList_Click);
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(129, 347);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(75, 23);
			this.btnPause.TabIndex = 17;
			this.btnPause.Text = "&Pause";
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 382);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.tabThemeSettings);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.btnActivate);
			this.Controls.Add(this.grpTheme);
			this.Controls.Add(this.btnPrevious);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnSwitchNow);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(600, 420);
			this.Name = "MainWindow";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "WallSwitch";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			this.cmLocations.ResumeLayout(false);
			this.grpTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).EndInit();
			this.cmTray.ResumeLayout(false);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.tabThemeSettings.ResumeLayout(false);
			this.tabLocations.ResumeLayout(false);
			this.tabDisplay.ResumeLayout(false);
			this.tabDisplay.PerformLayout();
			this.c_colorEffectsGroupBox.ResumeLayout(false);
			this.c_colorEffectsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_colorEffectCollageFadeRatioTrackBar)).EndInit();
			this.grpCollageDisplay.ResumeLayout(false);
			this.grpCollageDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkFeather)).EndInit();
			this.tabFrequency.ResumeLayout(false);
			this.tabFrequency.PerformLayout();
			this.tabHistory.ResumeLayout(false);
			this.cmHistory.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lstLocations;
		private System.Windows.Forms.ComboBox cmbTheme;
		private System.Windows.Forms.GroupBox grpTheme;
		private System.Windows.Forms.Button btnDeleteTheme;
		private System.Windows.Forms.Button btnNewTheme;
		private System.Windows.Forms.Button btnAddImage;
		private System.Windows.Forms.Button btnAddFolder;
		private System.Windows.Forms.ColumnHeader colLocation;
		private System.Windows.Forms.ComboBox cmbThemeMode;
		private System.Windows.Forms.ComboBox cmbThemePeriod;
		private System.Windows.Forms.Label lblFrequency;
		private System.Windows.Forms.TextBox txtThemeFreq;
		private System.Windows.Forms.Label lblMode;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenuStrip cmTray;
		private System.Windows.Forms.ToolStripMenuItem cmShowMainWindow;
		private System.Windows.Forms.ToolStripMenuItem cmExit;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnRenameTheme;
		private System.Windows.Forms.Button btnActivate;
		private System.Windows.Forms.ToolStripMenuItem cmSwitchNow;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.Button btnSwitchNow;
		private System.Windows.Forms.ContextMenuStrip cmLocations;
		private System.Windows.Forms.ToolStripMenuItem ciDeleteLocation;
		private ColorSample clrBackTop;
		private System.Windows.Forms.Label lblBackBottom;
		private ColorSample clrBackBottom;
		private System.Windows.Forms.Label lblBackTop;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem miFileExit;
		private System.Windows.Forms.ToolStripMenuItem menuHelp;
		private System.Windows.Forms.ToolStripMenuItem miHelpAbout;
		private System.Windows.Forms.ToolStripMenuItem menuTools;
		private System.Windows.Forms.ToolStripMenuItem ciTheme;
		private System.Windows.Forms.ToolStripMenuItem ciAddFolder;
		private System.Windows.Forms.ToolStripMenuItem ciAddImage;
		private System.Windows.Forms.ToolStripMenuItem miFileNewTheme;
		private System.Windows.Forms.ToolStripMenuItem miFileRenameTheme;
		private System.Windows.Forms.ToolStripMenuItem miFileDeleteTheme;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ciSwitchPrev;
		private System.Windows.Forms.Label lblImageSize;
		private System.Windows.Forms.TrackBar trkImageSize;
		private System.Windows.Forms.Label lblImageSizeDisplay;
		private System.Windows.Forms.CheckBox chkSeparateMonitors;
		private System.Windows.Forms.TrackBar trkOpacity;
		private System.Windows.Forms.Label lblOpacity;
		private System.Windows.Forms.Label lblOpacityDisplay;
		private System.Windows.Forms.ToolStripMenuItem miToolsStartWithWindows;
		private System.Windows.Forms.ComboBox cmbImageFit;
		private System.Windows.Forms.ToolStripMenuItem miFileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem miClearHistory;
		private System.Windows.Forms.TabControl tabThemeSettings;
		private System.Windows.Forms.TabPage tabLocations;
		private System.Windows.Forms.TabPage tabDisplay;
		private System.Windows.Forms.GroupBox grpCollageDisplay;
		private System.Windows.Forms.TrackBar trkFeather;
		private System.Windows.Forms.TabPage tabFrequency;
		private System.Windows.Forms.Label lblFeatherDisplay;
		private System.Windows.Forms.Label lblFeather;
		private System.Windows.Forms.Button btnAddFeed;
		private System.Windows.Forms.TabPage tabHistory;
		private HistoryList lstHistory;
		private System.Windows.Forms.ContextMenuStrip cmHistory;
		private System.Windows.Forms.ToolStripMenuItem ciOpenHistoryFile;
		private System.Windows.Forms.ToolStripMenuItem ciExploreHistoryFile;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem ciClearHistoryList;
		private System.Windows.Forms.ColumnHeader colNextUpdate;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem ciUpdateLocationNow;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem ciLocationProperties;
		private System.Windows.Forms.ToolStripMenuItem ciLocationExplore;
		private System.Windows.Forms.CheckBox chkFadeTransition;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label lblScalePct;
		private System.Windows.Forms.TextBox txtMaxScale;
		private System.Windows.Forms.CheckBox chkLimitScale;
		private System.Windows.Forms.Button btnPause;
		private HotKeyTextBox c_activateThemeHotKey;
		private System.Windows.Forms.Label c_activateThemeLabel;
		private System.Windows.Forms.ToolStripMenuItem miHotKeys;
		private System.Windows.Forms.GroupBox c_colorEffectsGroupBox;
		private System.Windows.Forms.CheckBox c_colorEffectCollageFade;
		private System.Windows.Forms.ComboBox c_colorEffectCombo;
		private System.Windows.Forms.Label c_colorEffectCollageFadeRatioValue;
		private System.Windows.Forms.TrackBar c_colorEffectCollageFadeRatioTrackBar;

	}
}

