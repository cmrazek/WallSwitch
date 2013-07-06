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
			this.miAddRssFeed = new System.Windows.Forms.ToolStripMenuItem();
			this.ciAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationExplore = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.ciUpdateLocationNow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.ciDeleteLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.cmbTheme = new System.Windows.Forms.ComboBox();
			this.grpTheme = new System.Windows.Forms.GroupBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnTheme = new System.Windows.Forms.Button();
			this.cmTheme = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciNewTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.ciRenameTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.ciDuplicateTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
			this.ciSaveTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
			this.ciDeleteTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.btnActivate = new System.Windows.Forms.Button();
			this.btnSwitchNow = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnPrevious = new System.Windows.Forms.Button();
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
			this.btnAddImage = new System.Windows.Forms.Button();
			this.cmbThemeMode = new System.Windows.Forms.ComboBox();
			this.btnAddFolder = new System.Windows.Forms.Button();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmShowMainWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.ciTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmSwitchNow = new System.Windows.Forms.ToolStripMenuItem();
			this.ciSwitchPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileNewTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileRenameTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.miDuplicateTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileDeleteTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.miFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.miFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.miHotKeys = new System.Windows.Forms.ToolStripMenuItem();
			this.miClearHistory = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.tabThemeSettings = new System.Windows.Forms.TabControl();
			this.tabLocations = new System.Windows.Forms.TabPage();
			this.btnAddFeed = new System.Windows.Forms.Button();
			this.tabSettings = new System.Windows.Forms.TabPage();
			this.flowDisplay = new System.Windows.Forms.FlowLayoutPanel();
			this.grpDisplayMode = new System.Windows.Forms.GroupBox();
			this.lblScalePct = new System.Windows.Forms.Label();
			this.txtMaxScale = new System.Windows.Forms.TextBox();
			this.chkLimitScale = new System.Windows.Forms.CheckBox();
			this.grpFrequency = new System.Windows.Forms.GroupBox();
			this.chkFadeTransition = new System.Windows.Forms.CheckBox();
			this.c_activateThemeLabel = new System.Windows.Forms.Label();
			this.cmbThemePeriod = new System.Windows.Forms.ComboBox();
			this.lblFrequency = new System.Windows.Forms.Label();
			this.txtThemeFreq = new System.Windows.Forms.TextBox();
			this.c_activateThemeHotKey = new WallSwitch.HotKeyTextBox();
			this.grpBackgroundColor = new System.Windows.Forms.GroupBox();
			this.clrBackTop = new WallSwitch.ColorSample();
			this.clrBackBottom = new WallSwitch.ColorSample();
			this.grpCollageDisplay = new System.Windows.Forms.GroupBox();
			this.trkDropShadowFeatherDist = new System.Windows.Forms.TrackBar();
			this.trkDropShadowOpacity = new System.Windows.Forms.TrackBar();
			this.lblDropShadowOpacity = new System.Windows.Forms.Label();
			this.lblDropShadowOpacityValue = new System.Windows.Forms.Label();
			this.lblDropShadowFeatherDist = new System.Windows.Forms.Label();
			this.chkFeather = new System.Windows.Forms.CheckBox();
			this.chkDropShadowFeather = new System.Windows.Forms.CheckBox();
			this.chkDropShadow = new System.Windows.Forms.CheckBox();
			this.lblDropShadowUnit = new System.Windows.Forms.Label();
			this.trkDropShadow = new System.Windows.Forms.TrackBar();
			this.lblFeatherUnit = new System.Windows.Forms.Label();
			this.trkFeather = new System.Windows.Forms.TrackBar();
			this.grpImageEffects = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbColorEffectFore = new System.Windows.Forms.ComboBox();
			this.grpBackgroundColorEffects = new System.Windows.Forms.GroupBox();
			this.lblBackgroundBlurDistValue = new System.Windows.Forms.Label();
			this.trkBackgroundBlurDist = new System.Windows.Forms.TrackBar();
			this.chkBackgroundBlur = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbColorEffectBack = new System.Windows.Forms.ComboBox();
			this.lblColorEffectCollageFadeRatioUnit = new System.Windows.Forms.Label();
			this.trkColorEffectCollageFadeRatio = new System.Windows.Forms.TrackBar();
			this.tabHistory = new System.Windows.Forms.TabPage();
			this.lstHistory = new WallSwitch.HistoryList();
			this.cmHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciOpenHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ciExploreHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ciDeleteHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.ciClearHistoryList = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grpNavButtons = new System.Windows.Forms.GroupBox();
			this.cmLocations.SuspendLayout();
			this.grpTheme.SuspendLayout();
			this.cmTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).BeginInit();
			this.cmTray.SuspendLayout();
			this.mainMenu.SuspendLayout();
			this.tabThemeSettings.SuspendLayout();
			this.tabLocations.SuspendLayout();
			this.tabSettings.SuspendLayout();
			this.flowDisplay.SuspendLayout();
			this.grpDisplayMode.SuspendLayout();
			this.grpFrequency.SuspendLayout();
			this.grpBackgroundColor.SuspendLayout();
			this.grpCollageDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowFeatherDist)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkFeather)).BeginInit();
			this.grpImageEffects.SuspendLayout();
			this.grpBackgroundColorEffects.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBackgroundBlurDist)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkColorEffectCollageFadeRatio)).BeginInit();
			this.tabHistory.SuspendLayout();
			this.cmHistory.SuspendLayout();
			this.grpNavButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstLocations
			// 
			this.lstLocations.AllowDrop = true;
			this.lstLocations.CheckBoxes = true;
			this.lstLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLocation,
            this.colNextUpdate});
			this.lstLocations.ContextMenuStrip = this.cmLocations;
			this.lstLocations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstLocations.Location = new System.Drawing.Point(3, 3);
			this.lstLocations.Name = "lstLocations";
			this.lstLocations.Size = new System.Drawing.Size(546, 305);
			this.lstLocations.TabIndex = 0;
			this.toolTip1.SetToolTip(this.lstLocations, "Locations where images are retrieved");
			this.lstLocations.UseCompatibleStateImageBehavior = false;
			this.lstLocations.View = System.Windows.Forms.View.Details;
			this.lstLocations.ItemActivate += new System.EventHandler(this.lstLocations_ItemActivate);
			this.lstLocations.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstLocations_ItemChecked);
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
            this.miAddRssFeed,
            this.ciAddImage,
            this.ciLocationExplore,
            this.toolStripMenuItem6,
            this.ciUpdateLocationNow,
            this.toolStripMenuItem5,
            this.ciDeleteLocation,
            this.ciLocationProperties});
			this.cmLocations.Name = "locationsMenu";
			this.cmLocations.Size = new System.Drawing.Size(141, 170);
			this.cmLocations.Opening += new System.ComponentModel.CancelEventHandler(this.cmLocations_Opening);
			// 
			// ciAddFolder
			// 
			this.ciAddFolder.Name = "ciAddFolder";
			this.ciAddFolder.Size = new System.Drawing.Size(140, 22);
			this.ciAddFolder.Text = "Add &Folder";
			this.ciAddFolder.Click += new System.EventHandler(this.ciAddFolder_Click);
			// 
			// miAddRssFeed
			// 
			this.miAddRssFeed.Name = "miAddRssFeed";
			this.miAddRssFeed.Size = new System.Drawing.Size(140, 22);
			this.miAddRssFeed.Text = "&Add Feed";
			this.miAddRssFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
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
			this.cmbTheme.Size = new System.Drawing.Size(227, 21);
			this.cmbTheme.Sorted = true;
			this.cmbTheme.TabIndex = 0;
			this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
			// 
			// grpTheme
			// 
			this.grpTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpTheme.Controls.Add(this.btnSave);
			this.grpTheme.Controls.Add(this.btnTheme);
			this.grpTheme.Controls.Add(this.cmbTheme);
			this.grpTheme.Controls.Add(this.btnActivate);
			this.grpTheme.Location = new System.Drawing.Point(12, 27);
			this.grpTheme.Name = "grpTheme";
			this.grpTheme.Size = new System.Drawing.Size(431, 50);
			this.grpTheme.TabIndex = 0;
			this.grpTheme.TabStop = false;
			this.grpTheme.Text = "Theme";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(269, 17);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.miFileSave_Click);
			// 
			// btnTheme
			// 
			this.btnTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTheme.ContextMenuStrip = this.cmTheme;
			this.btnTheme.Location = new System.Drawing.Point(239, 17);
			this.btnTheme.Name = "btnTheme";
			this.btnTheme.Size = new System.Drawing.Size(24, 23);
			this.btnTheme.TabIndex = 1;
			this.btnTheme.Text = "...";
			this.btnTheme.UseVisualStyleBackColor = true;
			this.btnTheme.Click += new System.EventHandler(this.c_themeMenuButton_Click);
			// 
			// cmTheme
			// 
			this.cmTheme.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciNewTheme,
            this.ciRenameTheme,
            this.ciDuplicateTheme,
            this.toolStripMenuItem9,
            this.ciSaveTheme,
            this.toolStripMenuItem10,
            this.ciDeleteTheme});
			this.cmTheme.Name = "c_themeMenu";
			this.cmTheme.Size = new System.Drawing.Size(125, 126);
			// 
			// ciNewTheme
			// 
			this.ciNewTheme.Name = "ciNewTheme";
			this.ciNewTheme.Size = new System.Drawing.Size(124, 22);
			this.ciNewTheme.Text = "&New";
			this.ciNewTheme.Click += new System.EventHandler(this.btnNewTheme_Click);
			// 
			// ciRenameTheme
			// 
			this.ciRenameTheme.Name = "ciRenameTheme";
			this.ciRenameTheme.Size = new System.Drawing.Size(124, 22);
			this.ciRenameTheme.Text = "&Rename";
			this.ciRenameTheme.Click += new System.EventHandler(this.btnRenameTheme_Click);
			// 
			// ciDuplicateTheme
			// 
			this.ciDuplicateTheme.Name = "ciDuplicateTheme";
			this.ciDuplicateTheme.Size = new System.Drawing.Size(124, 22);
			this.ciDuplicateTheme.Text = "D&uplicate";
			this.ciDuplicateTheme.Click += new System.EventHandler(this.miDuplicateTheme_Click);
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(121, 6);
			// 
			// ciSaveTheme
			// 
			this.ciSaveTheme.Name = "ciSaveTheme";
			this.ciSaveTheme.Size = new System.Drawing.Size(124, 22);
			this.ciSaveTheme.Text = "&Save";
			this.ciSaveTheme.Click += new System.EventHandler(this.miFileSave_Click);
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(121, 6);
			// 
			// ciDeleteTheme
			// 
			this.ciDeleteTheme.Name = "ciDeleteTheme";
			this.ciDeleteTheme.Size = new System.Drawing.Size(124, 22);
			this.ciDeleteTheme.Text = "&Delete";
			this.ciDeleteTheme.Click += new System.EventHandler(this.btnDeleteTheme_Click);
			// 
			// btnActivate
			// 
			this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnActivate.Location = new System.Drawing.Point(350, 17);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(75, 23);
			this.btnActivate.TabIndex = 3;
			this.btnActivate.Text = "&Activate";
			this.toolTip1.SetToolTip(this.btnActivate, "Activate this theme");
			this.btnActivate.UseVisualStyleBackColor = true;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// btnSwitchNow
			// 
			this.btnSwitchNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSwitchNow.Image = global::WallSwitch.Res.NextIcon;
			this.btnSwitchNow.Location = new System.Drawing.Point(83, 17);
			this.btnSwitchNow.Name = "btnSwitchNow";
			this.btnSwitchNow.Size = new System.Drawing.Size(30, 23);
			this.btnSwitchNow.TabIndex = 2;
			this.toolTip1.SetToolTip(this.btnSwitchNow, "Go to the next image");
			this.btnSwitchNow.UseVisualStyleBackColor = true;
			this.btnSwitchNow.Click += new System.EventHandler(this.btnSwitchNow_Click);
			// 
			// btnPause
			// 
			this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPause.Image = global::WallSwitch.Res.PauseIcon;
			this.btnPause.Location = new System.Drawing.Point(47, 17);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(30, 23);
			this.btnPause.TabIndex = 1;
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.Image = global::WallSwitch.Res.PrevIcon;
			this.btnPrevious.Location = new System.Drawing.Point(11, 17);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(30, 23);
			this.btnPrevious.TabIndex = 0;
			this.toolTip1.SetToolTip(this.btnPrevious, "Go back to the previous image");
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
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
			this.cmbImageFit.Location = new System.Drawing.Point(153, 19);
			this.cmbImageFit.Name = "cmbImageFit";
			this.cmbImageFit.Size = new System.Drawing.Size(141, 21);
			this.cmbImageFit.TabIndex = 1;
			this.toolTip1.SetToolTip(this.cmbImageFit, "Wallpaper sizing mode");
			this.cmbImageFit.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblOpacityDisplay
			// 
			this.lblOpacityDisplay.AutoSize = true;
			this.lblOpacityDisplay.Location = new System.Drawing.Point(451, 19);
			this.lblOpacityDisplay.Name = "lblOpacityDisplay";
			this.lblOpacityDisplay.Size = new System.Drawing.Size(15, 13);
			this.lblOpacityDisplay.TabIndex = 5;
			this.lblOpacityDisplay.Text = "%";
			this.toolTip1.SetToolTip(this.lblOpacityDisplay, "Opacity of background used to fade out previous images");
			// 
			// trkOpacity
			// 
			this.trkOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkOpacity.LargeChange = 20;
			this.trkOpacity.Location = new System.Drawing.Point(325, 19);
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
			this.lblOpacity.Location = new System.Drawing.Point(212, 19);
			this.lblOpacity.Name = "lblOpacity";
			this.lblOpacity.Size = new System.Drawing.Size(107, 13);
			this.lblOpacity.TabIndex = 3;
			this.lblOpacity.Text = "Background Opacity:";
			this.toolTip1.SetToolTip(this.lblOpacity, "Opacity of background used to fade out previous images");
			// 
			// chkSeparateMonitors
			// 
			this.chkSeparateMonitors.AutoSize = true;
			this.chkSeparateMonitors.Checked = true;
			this.chkSeparateMonitors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSeparateMonitors.Location = new System.Drawing.Point(325, 21);
			this.chkSeparateMonitors.Name = "chkSeparateMonitors";
			this.chkSeparateMonitors.Size = new System.Drawing.Size(179, 17);
			this.chkSeparateMonitors.TabIndex = 2;
			this.chkSeparateMonitors.Text = "Separate image for each monitor";
			this.toolTip1.SetToolTip(this.chkSeparateMonitors, "Display a separate image on each monitor");
			this.chkSeparateMonitors.UseVisualStyleBackColor = true;
			this.chkSeparateMonitors.CheckedChanged += new System.EventHandler(this.chkSeparateMonitors_CheckedChanged);
			// 
			// lblImageSizeDisplay
			// 
			this.lblImageSizeDisplay.AutoSize = true;
			this.lblImageSizeDisplay.Location = new System.Drawing.Point(245, 19);
			this.lblImageSizeDisplay.Name = "lblImageSizeDisplay";
			this.lblImageSizeDisplay.Size = new System.Drawing.Size(15, 13);
			this.lblImageSizeDisplay.TabIndex = 2;
			this.lblImageSizeDisplay.Text = "%";
			this.toolTip1.SetToolTip(this.lblImageSizeDisplay, "Image size in relation to the screen");
			// 
			// lblImageSize
			// 
			this.lblImageSize.AutoSize = true;
			this.lblImageSize.Location = new System.Drawing.Point(6, 19);
			this.lblImageSize.Name = "lblImageSize";
			this.lblImageSize.Size = new System.Drawing.Size(62, 13);
			this.lblImageSize.TabIndex = 0;
			this.lblImageSize.Text = "Image Size:";
			this.toolTip1.SetToolTip(this.lblImageSize, "Image size in relation to the screen");
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
			this.lblBackBottom.Location = new System.Drawing.Point(6, 43);
			this.lblBackBottom.Name = "lblBackBottom";
			this.lblBackBottom.Size = new System.Drawing.Size(113, 13);
			this.lblBackBottom.TabIndex = 2;
			this.lblBackBottom.Text = "Bottom Gradient Color:";
			// 
			// lblBackTop
			// 
			this.lblBackTop.AutoSize = true;
			this.lblBackTop.Location = new System.Drawing.Point(6, 19);
			this.lblBackTop.Name = "lblBackTop";
			this.lblBackTop.Size = new System.Drawing.Size(99, 13);
			this.lblBackTop.TabIndex = 0;
			this.lblBackTop.Text = "Top Gradient Color:";
			// 
			// btnAddImage
			// 
			this.btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddImage.Location = new System.Drawing.Point(830, 35);
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
			this.cmbThemeMode.Location = new System.Drawing.Point(6, 19);
			this.cmbThemeMode.Name = "cmbThemeMode";
			this.cmbThemeMode.Size = new System.Drawing.Size(141, 21);
			this.cmbThemeMode.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cmbThemeMode, "Wallpaper display mode");
			this.cmbThemeMode.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// btnAddFolder
			// 
			this.btnAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFolder.Location = new System.Drawing.Point(830, 6);
			this.btnAddFolder.Name = "btnAddFolder";
			this.btnAddFolder.Size = new System.Drawing.Size(120, 23);
			this.btnAddFolder.TabIndex = 1;
			this.btnAddFolder.Text = "&Add Folder";
			this.toolTip1.SetToolTip(this.btnAddFolder, "Add a folder containing multiple images to the list");
			this.btnAddFolder.UseVisualStyleBackColor = true;
			this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
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
            this.miDuplicateTheme,
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
			// miDuplicateTheme
			// 
			this.miDuplicateTheme.Name = "miDuplicateTheme";
			this.miDuplicateTheme.Size = new System.Drawing.Size(198, 22);
			this.miDuplicateTheme.Text = "D&uplicate Theme";
			this.miDuplicateTheme.Click += new System.EventHandler(this.miDuplicateTheme_Click);
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
            this.miClearHistory,
            this.toolStripMenuItem11,
            this.miHotKeys,
            this.settingsToolStripMenuItem});
			this.menuTools.Name = "menuTools";
			this.menuTools.Size = new System.Drawing.Size(48, 20);
			this.menuTools.Text = "&Tools";
			// 
			// miHotKeys
			// 
			this.miHotKeys.Name = "miHotKeys";
			this.miHotKeys.Size = new System.Drawing.Size(152, 22);
			this.miHotKeys.Text = "&Hot Keys";
			this.miHotKeys.Click += new System.EventHandler(this.miHotKeys_Click);
			// 
			// miClearHistory
			// 
			this.miClearHistory.Name = "miClearHistory";
			this.miClearHistory.Size = new System.Drawing.Size(152, 22);
			this.miClearHistory.Text = "&Clear History";
			this.miClearHistory.Click += new System.EventHandler(this.miClearHistory_Click);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(149, 6);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.settingsToolStripMenuItem.Text = "&Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
			this.miHelpAbout.Size = new System.Drawing.Size(152, 22);
			this.miHelpAbout.Text = "&About";
			this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
			// 
			// tabThemeSettings
			// 
			this.tabThemeSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabThemeSettings.Controls.Add(this.tabLocations);
			this.tabThemeSettings.Controls.Add(this.tabSettings);
			this.tabThemeSettings.Controls.Add(this.tabHistory);
			this.tabThemeSettings.Location = new System.Drawing.Point(12, 83);
			this.tabThemeSettings.Name = "tabThemeSettings";
			this.tabThemeSettings.SelectedIndex = 0;
			this.tabThemeSettings.Size = new System.Drawing.Size(560, 337);
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
			this.tabLocations.Size = new System.Drawing.Size(552, 311);
			this.tabLocations.TabIndex = 0;
			this.tabLocations.Text = "Images";
			this.tabLocations.UseVisualStyleBackColor = true;
			// 
			// btnAddFeed
			// 
			this.btnAddFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFeed.Location = new System.Drawing.Point(829, 64);
			this.btnAddFeed.Name = "btnAddFeed";
			this.btnAddFeed.Size = new System.Drawing.Size(120, 23);
			this.btnAddFeed.TabIndex = 3;
			this.btnAddFeed.Text = "Add &Feed";
			this.toolTip1.SetToolTip(this.btnAddFeed, "Add a RSS/ATOM feed to the list");
			this.btnAddFeed.UseVisualStyleBackColor = true;
			this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
			// 
			// tabSettings
			// 
			this.tabSettings.Controls.Add(this.flowDisplay);
			this.tabSettings.Location = new System.Drawing.Point(4, 22);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(552, 311);
			this.tabSettings.TabIndex = 1;
			this.tabSettings.Text = "Settings";
			this.tabSettings.UseVisualStyleBackColor = true;
			// 
			// flowDisplay
			// 
			this.flowDisplay.AutoScroll = true;
			this.flowDisplay.Controls.Add(this.grpDisplayMode);
			this.flowDisplay.Controls.Add(this.grpFrequency);
			this.flowDisplay.Controls.Add(this.grpBackgroundColor);
			this.flowDisplay.Controls.Add(this.grpCollageDisplay);
			this.flowDisplay.Controls.Add(this.grpImageEffects);
			this.flowDisplay.Controls.Add(this.grpBackgroundColorEffects);
			this.flowDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowDisplay.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowDisplay.Location = new System.Drawing.Point(3, 3);
			this.flowDisplay.Name = "flowDisplay";
			this.flowDisplay.Size = new System.Drawing.Size(546, 305);
			this.flowDisplay.TabIndex = 14;
			this.flowDisplay.WrapContents = false;
			// 
			// grpDisplayMode
			// 
			this.grpDisplayMode.Controls.Add(this.cmbThemeMode);
			this.grpDisplayMode.Controls.Add(this.lblScalePct);
			this.grpDisplayMode.Controls.Add(this.cmbImageFit);
			this.grpDisplayMode.Controls.Add(this.txtMaxScale);
			this.grpDisplayMode.Controls.Add(this.chkLimitScale);
			this.grpDisplayMode.Controls.Add(this.chkSeparateMonitors);
			this.grpDisplayMode.Location = new System.Drawing.Point(3, 3);
			this.grpDisplayMode.Name = "grpDisplayMode";
			this.grpDisplayMode.Size = new System.Drawing.Size(520, 78);
			this.grpDisplayMode.TabIndex = 0;
			this.grpDisplayMode.TabStop = false;
			this.grpDisplayMode.Text = "Display Mode";
			// 
			// lblScalePct
			// 
			this.lblScalePct.AutoSize = true;
			this.lblScalePct.Location = new System.Drawing.Point(209, 49);
			this.lblScalePct.Name = "lblScalePct";
			this.lblScalePct.Size = new System.Drawing.Size(15, 13);
			this.lblScalePct.TabIndex = 5;
			this.lblScalePct.Text = "%";
			// 
			// txtMaxScale
			// 
			this.txtMaxScale.Location = new System.Drawing.Point(153, 46);
			this.txtMaxScale.Name = "txtMaxScale";
			this.txtMaxScale.Size = new System.Drawing.Size(50, 20);
			this.txtMaxScale.TabIndex = 4;
			this.txtMaxScale.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// chkLimitScale
			// 
			this.chkLimitScale.AutoSize = true;
			this.chkLimitScale.Location = new System.Drawing.Point(6, 48);
			this.chkLimitScale.Name = "chkLimitScale";
			this.chkLimitScale.Size = new System.Drawing.Size(126, 17);
			this.chkLimitScale.TabIndex = 3;
			this.chkLimitScale.Text = "Limit image scaling to";
			this.chkLimitScale.UseVisualStyleBackColor = true;
			this.chkLimitScale.CheckedChanged += new System.EventHandler(this.chkLimitScale_CheckedChanged);
			// 
			// grpFrequency
			// 
			this.grpFrequency.Controls.Add(this.chkFadeTransition);
			this.grpFrequency.Controls.Add(this.c_activateThemeLabel);
			this.grpFrequency.Controls.Add(this.cmbThemePeriod);
			this.grpFrequency.Controls.Add(this.lblFrequency);
			this.grpFrequency.Controls.Add(this.txtThemeFreq);
			this.grpFrequency.Controls.Add(this.c_activateThemeHotKey);
			this.grpFrequency.Location = new System.Drawing.Point(3, 87);
			this.grpFrequency.Name = "grpFrequency";
			this.grpFrequency.Size = new System.Drawing.Size(520, 76);
			this.grpFrequency.TabIndex = 1;
			this.grpFrequency.TabStop = false;
			this.grpFrequency.Text = "Change Frequency";
			// 
			// chkFadeTransition
			// 
			this.chkFadeTransition.AutoSize = true;
			this.chkFadeTransition.Location = new System.Drawing.Point(325, 21);
			this.chkFadeTransition.Name = "chkFadeTransition";
			this.chkFadeTransition.Size = new System.Drawing.Size(133, 17);
			this.chkFadeTransition.TabIndex = 3;
			this.chkFadeTransition.Text = "Cross-Fade Transitions";
			this.toolTip1.SetToolTip(this.chkFadeTransition, "Use smooth cross-fading between wallpapers (Windows 7 or higher)");
			this.chkFadeTransition.UseVisualStyleBackColor = true;
			this.chkFadeTransition.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeLabel
			// 
			this.c_activateThemeLabel.AutoSize = true;
			this.c_activateThemeLabel.Location = new System.Drawing.Point(6, 48);
			this.c_activateThemeLabel.Name = "c_activateThemeLabel";
			this.c_activateThemeLabel.Size = new System.Drawing.Size(48, 13);
			this.c_activateThemeLabel.TabIndex = 4;
			this.c_activateThemeLabel.Text = "Hot Key:";
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
			this.cmbThemePeriod.Location = new System.Drawing.Point(219, 19);
			this.cmbThemePeriod.Name = "cmbThemePeriod";
			this.cmbThemePeriod.Size = new System.Drawing.Size(75, 21);
			this.cmbThemePeriod.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmbThemePeriod, "Wallpaper change interval");
			this.cmbThemePeriod.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblFrequency
			// 
			this.lblFrequency.AutoSize = true;
			this.lblFrequency.Location = new System.Drawing.Point(6, 22);
			this.lblFrequency.Name = "lblFrequency";
			this.lblFrequency.Size = new System.Drawing.Size(104, 13);
			this.lblFrequency.TabIndex = 0;
			this.lblFrequency.Text = "Change image every";
			// 
			// txtThemeFreq
			// 
			this.txtThemeFreq.Location = new System.Drawing.Point(153, 19);
			this.txtThemeFreq.MaxLength = 5;
			this.txtThemeFreq.Name = "txtThemeFreq";
			this.txtThemeFreq.Size = new System.Drawing.Size(60, 20);
			this.txtThemeFreq.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtThemeFreq, "Wallpaper change interval");
			this.txtThemeFreq.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeHotKey
			// 
			this.c_activateThemeHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_activateThemeHotKey.HotKey = null;
			this.c_activateThemeHotKey.Location = new System.Drawing.Point(153, 45);
			this.c_activateThemeHotKey.Name = "c_activateThemeHotKey";
			this.c_activateThemeHotKey.ReadOnly = true;
			this.c_activateThemeHotKey.Size = new System.Drawing.Size(141, 20);
			this.c_activateThemeHotKey.TabIndex = 5;
			this.c_activateThemeHotKey.HotKeyChanged += new System.EventHandler(this.ControlChanged);
			// 
			// grpBackgroundColor
			// 
			this.grpBackgroundColor.Controls.Add(this.clrBackTop);
			this.grpBackgroundColor.Controls.Add(this.clrBackBottom);
			this.grpBackgroundColor.Controls.Add(this.lblBackBottom);
			this.grpBackgroundColor.Controls.Add(this.lblBackTop);
			this.grpBackgroundColor.Controls.Add(this.trkOpacity);
			this.grpBackgroundColor.Controls.Add(this.lblOpacity);
			this.grpBackgroundColor.Controls.Add(this.lblOpacityDisplay);
			this.grpBackgroundColor.Location = new System.Drawing.Point(3, 169);
			this.grpBackgroundColor.Name = "grpBackgroundColor";
			this.grpBackgroundColor.Size = new System.Drawing.Size(520, 72);
			this.grpBackgroundColor.TabIndex = 2;
			this.grpBackgroundColor.TabStop = false;
			this.grpBackgroundColor.Text = "Background Color";
			// 
			// clrBackTop
			// 
			this.clrBackTop.BackColor = System.Drawing.Color.Black;
			this.clrBackTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackTop.Color = System.Drawing.Color.Black;
			this.clrBackTop.Location = new System.Drawing.Point(153, 19);
			this.clrBackTop.Name = "clrBackTop";
			this.clrBackTop.Size = new System.Drawing.Size(30, 18);
			this.clrBackTop.TabIndex = 1;
			this.toolTip1.SetToolTip(this.clrBackTop, "Top color of background gradient");
			this.clrBackTop.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.clrBackTop_ColorChanged);
			// 
			// clrBackBottom
			// 
			this.clrBackBottom.BackColor = System.Drawing.Color.Black;
			this.clrBackBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackBottom.Color = System.Drawing.Color.Black;
			this.clrBackBottom.Location = new System.Drawing.Point(153, 43);
			this.clrBackBottom.Name = "clrBackBottom";
			this.clrBackBottom.Size = new System.Drawing.Size(30, 18);
			this.clrBackBottom.TabIndex = 3;
			this.toolTip1.SetToolTip(this.clrBackBottom, "Bottom color of background gradient");
			this.clrBackBottom.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.clrBackBottom_ColorChanged);
			// 
			// grpCollageDisplay
			// 
			this.grpCollageDisplay.Controls.Add(this.trkDropShadowFeatherDist);
			this.grpCollageDisplay.Controls.Add(this.trkDropShadowOpacity);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowOpacity);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowOpacityValue);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowFeatherDist);
			this.grpCollageDisplay.Controls.Add(this.chkFeather);
			this.grpCollageDisplay.Controls.Add(this.chkDropShadowFeather);
			this.grpCollageDisplay.Controls.Add(this.chkDropShadow);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowUnit);
			this.grpCollageDisplay.Controls.Add(this.trkDropShadow);
			this.grpCollageDisplay.Controls.Add(this.lblFeatherUnit);
			this.grpCollageDisplay.Controls.Add(this.trkFeather);
			this.grpCollageDisplay.Controls.Add(this.trkImageSize);
			this.grpCollageDisplay.Controls.Add(this.lblImageSize);
			this.grpCollageDisplay.Controls.Add(this.lblImageSizeDisplay);
			this.grpCollageDisplay.Location = new System.Drawing.Point(3, 247);
			this.grpCollageDisplay.Name = "grpCollageDisplay";
			this.grpCollageDisplay.Size = new System.Drawing.Size(520, 216);
			this.grpCollageDisplay.TabIndex = 3;
			this.grpCollageDisplay.TabStop = false;
			this.grpCollageDisplay.Text = "Collage Display";
			// 
			// trkDropShadowFeatherDist
			// 
			this.trkDropShadowFeatherDist.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadowFeatherDist.LargeChange = 20;
			this.trkDropShadowFeatherDist.Location = new System.Drawing.Point(119, 167);
			this.trkDropShadowFeatherDist.Maximum = 100;
			this.trkDropShadowFeatherDist.Name = "trkDropShadowFeatherDist";
			this.trkDropShadowFeatherDist.Size = new System.Drawing.Size(120, 45);
			this.trkDropShadowFeatherDist.SmallChange = 10;
			this.trkDropShadowFeatherDist.TabIndex = 13;
			this.trkDropShadowFeatherDist.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadowFeatherDist, "Width of feathering at the edges of shadows");
			this.trkDropShadowFeatherDist.Value = 50;
			this.trkDropShadowFeatherDist.Scroll += new System.EventHandler(this.trkDropShadowFeatherDist_Scroll);
			// 
			// trkDropShadowOpacity
			// 
			this.trkDropShadowOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadowOpacity.LargeChange = 20;
			this.trkDropShadowOpacity.Location = new System.Drawing.Point(119, 130);
			this.trkDropShadowOpacity.Maximum = 100;
			this.trkDropShadowOpacity.Name = "trkDropShadowOpacity";
			this.trkDropShadowOpacity.Size = new System.Drawing.Size(120, 45);
			this.trkDropShadowOpacity.SmallChange = 10;
			this.trkDropShadowOpacity.TabIndex = 16;
			this.trkDropShadowOpacity.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadowOpacity, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			this.trkDropShadowOpacity.Value = 50;
			this.trkDropShadowOpacity.Scroll += new System.EventHandler(this.trkDropShadowOpacity_Scroll);
			// 
			// lblDropShadowOpacity
			// 
			this.lblDropShadowOpacity.AutoSize = true;
			this.lblDropShadowOpacity.Location = new System.Drawing.Point(6, 132);
			this.lblDropShadowOpacity.Name = "lblDropShadowOpacity";
			this.lblDropShadowOpacity.Size = new System.Drawing.Size(88, 13);
			this.lblDropShadowOpacity.TabIndex = 12;
			this.lblDropShadowOpacity.Text = "Shadow Opacity:";
			this.toolTip1.SetToolTip(this.lblDropShadowOpacity, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			// 
			// lblDropShadowOpacityValue
			// 
			this.lblDropShadowOpacityValue.AutoSize = true;
			this.lblDropShadowOpacityValue.Location = new System.Drawing.Point(245, 132);
			this.lblDropShadowOpacityValue.Name = "lblDropShadowOpacityValue";
			this.lblDropShadowOpacityValue.Size = new System.Drawing.Size(15, 13);
			this.lblDropShadowOpacityValue.TabIndex = 14;
			this.lblDropShadowOpacityValue.Text = "%";
			this.toolTip1.SetToolTip(this.lblDropShadowOpacityValue, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			// 
			// lblDropShadowFeatherDist
			// 
			this.lblDropShadowFeatherDist.AutoSize = true;
			this.lblDropShadowFeatherDist.Location = new System.Drawing.Point(245, 168);
			this.lblDropShadowFeatherDist.Name = "lblDropShadowFeatherDist";
			this.lblDropShadowFeatherDist.Size = new System.Drawing.Size(33, 13);
			this.lblDropShadowFeatherDist.TabIndex = 17;
			this.lblDropShadowFeatherDist.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblDropShadowFeatherDist, "Width of feathering at the edges of shadows");
			// 
			// chkFeather
			// 
			this.chkFeather.AutoSize = true;
			this.chkFeather.Location = new System.Drawing.Point(6, 56);
			this.chkFeather.Name = "chkFeather";
			this.chkFeather.Size = new System.Drawing.Size(95, 17);
			this.chkFeather.TabIndex = 6;
			this.chkFeather.Text = "Feather Edges";
			this.toolTip1.SetToolTip(this.chkFeather, "Feather the edge of images?");
			this.chkFeather.UseVisualStyleBackColor = true;
			this.chkFeather.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// chkDropShadowFeather
			// 
			this.chkDropShadowFeather.AutoSize = true;
			this.chkDropShadowFeather.Location = new System.Drawing.Point(6, 167);
			this.chkDropShadowFeather.Name = "chkDropShadowFeather";
			this.chkDropShadowFeather.Size = new System.Drawing.Size(104, 17);
			this.chkDropShadowFeather.TabIndex = 15;
			this.chkDropShadowFeather.Text = "Feather Shadow";
			this.toolTip1.SetToolTip(this.chkDropShadowFeather, "Enable feathering on shadows?");
			this.chkDropShadowFeather.UseVisualStyleBackColor = true;
			this.chkDropShadowFeather.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// chkDropShadow
			// 
			this.chkDropShadow.AutoSize = true;
			this.chkDropShadow.Location = new System.Drawing.Point(6, 93);
			this.chkDropShadow.Name = "chkDropShadow";
			this.chkDropShadow.Size = new System.Drawing.Size(91, 17);
			this.chkDropShadow.TabIndex = 9;
			this.chkDropShadow.Text = "Drop Shadow";
			this.toolTip1.SetToolTip(this.chkDropShadow, "Enable drop shadows?");
			this.chkDropShadow.UseVisualStyleBackColor = true;
			this.chkDropShadow.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblDropShadowUnit
			// 
			this.lblDropShadowUnit.AutoSize = true;
			this.lblDropShadowUnit.Location = new System.Drawing.Point(245, 94);
			this.lblDropShadowUnit.Name = "lblDropShadowUnit";
			this.lblDropShadowUnit.Size = new System.Drawing.Size(33, 13);
			this.lblDropShadowUnit.TabIndex = 11;
			this.lblDropShadowUnit.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblDropShadowUnit, "Offset of drop shadow");
			// 
			// trkDropShadow
			// 
			this.trkDropShadow.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadow.LargeChange = 20;
			this.trkDropShadow.Location = new System.Drawing.Point(119, 93);
			this.trkDropShadow.Maximum = 100;
			this.trkDropShadow.Name = "trkDropShadow";
			this.trkDropShadow.Size = new System.Drawing.Size(120, 45);
			this.trkDropShadow.SmallChange = 10;
			this.trkDropShadow.TabIndex = 10;
			this.trkDropShadow.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadow, "Offset of drop shadow");
			this.trkDropShadow.Value = 50;
			this.trkDropShadow.Scroll += new System.EventHandler(this.trkDropShadow_Scroll);
			// 
			// lblFeatherUnit
			// 
			this.lblFeatherUnit.AutoSize = true;
			this.lblFeatherUnit.Location = new System.Drawing.Point(245, 57);
			this.lblFeatherUnit.Name = "lblFeatherUnit";
			this.lblFeatherUnit.Size = new System.Drawing.Size(33, 13);
			this.lblFeatherUnit.TabIndex = 8;
			this.lblFeatherUnit.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblFeatherUnit, "Width of feathering at the edges of images");
			// 
			// trkFeather
			// 
			this.trkFeather.BackColor = System.Drawing.SystemColors.Window;
			this.trkFeather.LargeChange = 20;
			this.trkFeather.Location = new System.Drawing.Point(119, 56);
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
			// grpImageEffects
			// 
			this.grpImageEffects.Controls.Add(this.label1);
			this.grpImageEffects.Controls.Add(this.cmbColorEffectFore);
			this.grpImageEffects.Location = new System.Drawing.Point(3, 469);
			this.grpImageEffects.Name = "grpImageEffects";
			this.grpImageEffects.Size = new System.Drawing.Size(520, 50);
			this.grpImageEffects.TabIndex = 4;
			this.grpImageEffects.TabStop = false;
			this.grpImageEffects.Text = "Image Effects";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Color Effect:";
			// 
			// cmbColorEffectFore
			// 
			this.cmbColorEffectFore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbColorEffectFore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorEffectFore.FormattingEnabled = true;
			this.cmbColorEffectFore.Location = new System.Drawing.Point(119, 19);
			this.cmbColorEffectFore.Name = "cmbColorEffectFore";
			this.cmbColorEffectFore.Size = new System.Drawing.Size(139, 21);
			this.cmbColorEffectFore.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cmbColorEffectFore, "Color effect to be applied to image being displayed.");
			this.cmbColorEffectFore.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// grpBackgroundColorEffects
			// 
			this.grpBackgroundColorEffects.Controls.Add(this.lblBackgroundBlurDistValue);
			this.grpBackgroundColorEffects.Controls.Add(this.trkBackgroundBlurDist);
			this.grpBackgroundColorEffects.Controls.Add(this.chkBackgroundBlur);
			this.grpBackgroundColorEffects.Controls.Add(this.label2);
			this.grpBackgroundColorEffects.Controls.Add(this.cmbColorEffectBack);
			this.grpBackgroundColorEffects.Controls.Add(this.lblColorEffectCollageFadeRatioUnit);
			this.grpBackgroundColorEffects.Controls.Add(this.trkColorEffectCollageFadeRatio);
			this.grpBackgroundColorEffects.Location = new System.Drawing.Point(3, 525);
			this.grpBackgroundColorEffects.Name = "grpBackgroundColorEffects";
			this.grpBackgroundColorEffects.Size = new System.Drawing.Size(520, 120);
			this.grpBackgroundColorEffects.TabIndex = 5;
			this.grpBackgroundColorEffects.TabStop = false;
			this.grpBackgroundColorEffects.Text = "Background Image Effects";
			// 
			// lblBackgroundBlurDistValue
			// 
			this.lblBackgroundBlurDistValue.AutoSize = true;
			this.lblBackgroundBlurDistValue.Location = new System.Drawing.Point(249, 53);
			this.lblBackgroundBlurDistValue.Name = "lblBackgroundBlurDistValue";
			this.lblBackgroundBlurDistValue.Size = new System.Drawing.Size(33, 13);
			this.lblBackgroundBlurDistValue.TabIndex = 6;
			this.lblBackgroundBlurDistValue.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblBackgroundBlurDistValue, "Amount of blur to apply to background");
			// 
			// trkBackgroundBlurDist
			// 
			this.trkBackgroundBlurDist.BackColor = System.Drawing.SystemColors.Window;
			this.trkBackgroundBlurDist.LargeChange = 20;
			this.trkBackgroundBlurDist.Location = new System.Drawing.Point(119, 52);
			this.trkBackgroundBlurDist.Maximum = 20;
			this.trkBackgroundBlurDist.Name = "trkBackgroundBlurDist";
			this.trkBackgroundBlurDist.Size = new System.Drawing.Size(124, 45);
			this.trkBackgroundBlurDist.SmallChange = 10;
			this.trkBackgroundBlurDist.TabIndex = 5;
			this.trkBackgroundBlurDist.TickFrequency = 2;
			this.toolTip1.SetToolTip(this.trkBackgroundBlurDist, "Amount of blur to apply to background");
			this.trkBackgroundBlurDist.Value = 20;
			this.trkBackgroundBlurDist.Scroll += new System.EventHandler(this.trkBackgroundBlurDist_Scroll);
			// 
			// chkBackgroundBlur
			// 
			this.chkBackgroundBlur.AutoSize = true;
			this.chkBackgroundBlur.Location = new System.Drawing.Point(6, 52);
			this.chkBackgroundBlur.Name = "chkBackgroundBlur";
			this.chkBackgroundBlur.Size = new System.Drawing.Size(44, 17);
			this.chkBackgroundBlur.TabIndex = 4;
			this.chkBackgroundBlur.Text = "Blur";
			this.toolTip1.SetToolTip(this.chkBackgroundBlur, "Blur the background?");
			this.chkBackgroundBlur.UseVisualStyleBackColor = true;
			this.chkBackgroundBlur.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Color Effect:";
			// 
			// cmbColorEffectBack
			// 
			this.cmbColorEffectBack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorEffectBack.FormattingEnabled = true;
			this.cmbColorEffectBack.Location = new System.Drawing.Point(119, 19);
			this.cmbColorEffectBack.Name = "cmbColorEffectBack";
			this.cmbColorEffectBack.Size = new System.Drawing.Size(139, 21);
			this.cmbColorEffectBack.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cmbColorEffectBack, "Color effect to be applied to background images.");
			this.cmbColorEffectBack.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblColorEffectCollageFadeRatioUnit
			// 
			this.lblColorEffectCollageFadeRatioUnit.AutoSize = true;
			this.lblColorEffectCollageFadeRatioUnit.Location = new System.Drawing.Point(394, 22);
			this.lblColorEffectCollageFadeRatioUnit.Name = "lblColorEffectCollageFadeRatioUnit";
			this.lblColorEffectCollageFadeRatioUnit.Size = new System.Drawing.Size(15, 13);
			this.lblColorEffectCollageFadeRatioUnit.TabIndex = 2;
			this.lblColorEffectCollageFadeRatioUnit.Text = "%";
			this.toolTip1.SetToolTip(this.lblColorEffectCollageFadeRatioUnit, "Amount of color effect to apply to background images");
			// 
			// trkColorEffectCollageFadeRatio
			// 
			this.trkColorEffectCollageFadeRatio.BackColor = System.Drawing.SystemColors.Window;
			this.trkColorEffectCollageFadeRatio.LargeChange = 20;
			this.trkColorEffectCollageFadeRatio.Location = new System.Drawing.Point(264, 19);
			this.trkColorEffectCollageFadeRatio.Maximum = 100;
			this.trkColorEffectCollageFadeRatio.Name = "trkColorEffectCollageFadeRatio";
			this.trkColorEffectCollageFadeRatio.Size = new System.Drawing.Size(124, 45);
			this.trkColorEffectCollageFadeRatio.SmallChange = 10;
			this.trkColorEffectCollageFadeRatio.TabIndex = 1;
			this.trkColorEffectCollageFadeRatio.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkColorEffectCollageFadeRatio, "Amount of color effect to apply to background images");
			this.trkColorEffectCollageFadeRatio.Value = 25;
			this.trkColorEffectCollageFadeRatio.Scroll += new System.EventHandler(this.c_colorEffectCollageFadeRatioTrackBar_Scroll);
			// 
			// tabHistory
			// 
			this.tabHistory.Controls.Add(this.lstHistory);
			this.tabHistory.Location = new System.Drawing.Point(4, 22);
			this.tabHistory.Name = "tabHistory";
			this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabHistory.Size = new System.Drawing.Size(552, 311);
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
			this.lstHistory.Size = new System.Drawing.Size(546, 305);
			this.lstHistory.TabIndex = 0;
			this.lstHistory.SelectionChanged += new System.EventHandler(this.lstHistory_SelectionChanged);
			this.lstHistory.ItemActivated += new System.EventHandler<WallSwitch.HistoryList.ItemActivatedEventArgs>(this.lstHistory_ItemActivated);
			// 
			// cmHistory
			// 
			this.cmHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciOpenHistoryFile,
            this.ciExploreHistoryFile,
            this.ciDeleteHistoryFile,
            this.toolStripMenuItem4,
            this.ciClearHistoryList});
			this.cmHistory.Name = "cmHistory";
			this.cmHistory.Size = new System.Drawing.Size(143, 98);
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
			// ciDeleteHistoryFile
			// 
			this.ciDeleteHistoryFile.Name = "ciDeleteHistoryFile";
			this.ciDeleteHistoryFile.Size = new System.Drawing.Size(142, 22);
			this.ciDeleteHistoryFile.Text = "&Delete File";
			this.ciDeleteHistoryFile.Click += new System.EventHandler(this.ciDeleteHistoryFile_Click);
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
			// grpNavButtons
			// 
			this.grpNavButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpNavButtons.Controls.Add(this.btnSwitchNow);
			this.grpNavButtons.Controls.Add(this.btnPause);
			this.grpNavButtons.Controls.Add(this.btnPrevious);
			this.grpNavButtons.Location = new System.Drawing.Point(449, 27);
			this.grpNavButtons.Name = "grpNavButtons";
			this.grpNavButtons.Size = new System.Drawing.Size(123, 50);
			this.grpNavButtons.TabIndex = 11;
			this.grpNavButtons.TabStop = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 432);
			this.Controls.Add(this.tabThemeSettings);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.grpTheme);
			this.Controls.Add(this.grpNavButtons);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(600, 470);
			this.Name = "MainWindow";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "WallSwitch";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			this.cmLocations.ResumeLayout(false);
			this.grpTheme.ResumeLayout(false);
			this.cmTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).EndInit();
			this.cmTray.ResumeLayout(false);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.tabThemeSettings.ResumeLayout(false);
			this.tabLocations.ResumeLayout(false);
			this.tabSettings.ResumeLayout(false);
			this.flowDisplay.ResumeLayout(false);
			this.grpDisplayMode.ResumeLayout(false);
			this.grpDisplayMode.PerformLayout();
			this.grpFrequency.ResumeLayout(false);
			this.grpFrequency.PerformLayout();
			this.grpBackgroundColor.ResumeLayout(false);
			this.grpBackgroundColor.PerformLayout();
			this.grpCollageDisplay.ResumeLayout(false);
			this.grpCollageDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowFeatherDist)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkFeather)).EndInit();
			this.grpImageEffects.ResumeLayout(false);
			this.grpImageEffects.PerformLayout();
			this.grpBackgroundColorEffects.ResumeLayout(false);
			this.grpBackgroundColorEffects.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBackgroundBlurDist)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkColorEffectCollageFadeRatio)).EndInit();
			this.tabHistory.ResumeLayout(false);
			this.cmHistory.ResumeLayout(false);
			this.grpNavButtons.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lstLocations;
		private System.Windows.Forms.ComboBox cmbTheme;
		private System.Windows.Forms.GroupBox grpTheme;
		private System.Windows.Forms.Button btnAddImage;
		private System.Windows.Forms.Button btnAddFolder;
		private System.Windows.Forms.ColumnHeader colLocation;
		private System.Windows.Forms.ComboBox cmbThemeMode;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenuStrip cmTray;
		private System.Windows.Forms.ToolStripMenuItem cmShowMainWindow;
		private System.Windows.Forms.ToolStripMenuItem cmExit;
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
		private System.Windows.Forms.ComboBox cmbImageFit;
		private System.Windows.Forms.ToolStripMenuItem miFileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem miClearHistory;
		private System.Windows.Forms.TabControl tabThemeSettings;
		private System.Windows.Forms.TabPage tabLocations;
		private System.Windows.Forms.TabPage tabSettings;
		private System.Windows.Forms.GroupBox grpCollageDisplay;
		private System.Windows.Forms.TrackBar trkFeather;
		private System.Windows.Forms.Label lblFeatherUnit;
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
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label lblScalePct;
		private System.Windows.Forms.TextBox txtMaxScale;
		private System.Windows.Forms.CheckBox chkLimitScale;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.ToolStripMenuItem miHotKeys;
		private System.Windows.Forms.GroupBox grpBackgroundColorEffects;
		private System.Windows.Forms.ComboBox cmbColorEffectFore;
		private System.Windows.Forms.Label lblColorEffectCollageFadeRatioUnit;
		private System.Windows.Forms.TrackBar trkColorEffectCollageFadeRatio;
		private System.Windows.Forms.ComboBox cmbColorEffectBack;
		private System.Windows.Forms.Button btnTheme;
		private System.Windows.Forms.ContextMenuStrip cmTheme;
		private System.Windows.Forms.ToolStripMenuItem ciNewTheme;
		private System.Windows.Forms.ToolStripMenuItem ciRenameTheme;
		private System.Windows.Forms.ToolStripMenuItem ciDeleteTheme;
		private System.Windows.Forms.ToolStripMenuItem ciDuplicateTheme;
		private System.Windows.Forms.ToolStripMenuItem miDuplicateTheme;
		private System.Windows.Forms.CheckBox chkDropShadowFeather;
		private System.Windows.Forms.CheckBox chkDropShadow;
		private System.Windows.Forms.Label lblDropShadowUnit;
		private System.Windows.Forms.TrackBar trkDropShadow;
		private System.Windows.Forms.CheckBox chkFeather;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem ciSaveTheme;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
		private System.Windows.Forms.GroupBox grpNavButtons;
		private System.Windows.Forms.FlowLayoutPanel flowDisplay;
		private System.Windows.Forms.GroupBox grpDisplayMode;
		private System.Windows.Forms.GroupBox grpBackgroundColor;
		private System.Windows.Forms.GroupBox grpImageEffects;
		private System.Windows.Forms.CheckBox chkFadeTransition;
		private System.Windows.Forms.GroupBox grpFrequency;
		private System.Windows.Forms.Label c_activateThemeLabel;
		private System.Windows.Forms.ComboBox cmbThemePeriod;
		private System.Windows.Forms.Label lblFrequency;
		private System.Windows.Forms.TextBox txtThemeFreq;
		private HotKeyTextBox c_activateThemeHotKey;
		private System.Windows.Forms.Label lblDropShadowFeatherDist;
		private System.Windows.Forms.TrackBar trkDropShadowFeatherDist;
		private System.Windows.Forms.Label lblDropShadowOpacity;
		private System.Windows.Forms.Label lblDropShadowOpacityValue;
		private System.Windows.Forms.TrackBar trkDropShadowOpacity;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblBackgroundBlurDistValue;
		private System.Windows.Forms.TrackBar trkBackgroundBlurDist;
		private System.Windows.Forms.CheckBox chkBackgroundBlur;
		private System.Windows.Forms.ToolStripMenuItem miAddRssFeed;
		private System.Windows.Forms.ToolStripMenuItem ciDeleteHistoryFile;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;

	}
}

