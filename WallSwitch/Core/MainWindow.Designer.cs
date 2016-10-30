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
			this.colFrequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmLocations = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciAddFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.miAddRssFeed = new System.Windows.Forms.ToolStripMenuItem();
			this.ciAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.c_browseLocationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationExplore = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.ciUpdateLocationNow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.ciDeleteLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.c_locationImages = new System.Windows.Forms.ImageList(this.components);
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
			this.c_imageFit = new System.Windows.Forms.ComboBox();
			this.lblOpacityDisplay = new System.Windows.Forms.Label();
			this.trkOpacity = new System.Windows.Forms.TrackBar();
			this.lblOpacity = new System.Windows.Forms.Label();
			this.c_separateMonitors = new System.Windows.Forms.CheckBox();
			this.lblImageSizeDisplay = new System.Windows.Forms.Label();
			this.lblImageSize = new System.Windows.Forms.Label();
			this.trkImageSize = new System.Windows.Forms.TrackBar();
			this.lblBackBottom = new System.Windows.Forms.Label();
			this.lblBackTop = new System.Windows.Forms.Label();
			this.btnAddImage = new System.Windows.Forms.Button();
			this.c_themeMode = new System.Windows.Forms.ComboBox();
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
			this.miClearHistory = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
			this.miHotKeys = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.c_themeTabControl = new System.Windows.Forms.TabControl();
			this.c_locationsTab = new System.Windows.Forms.TabPage();
			this.btnAddFeed = new System.Windows.Forms.Button();
			this.c_settingsTab = new System.Windows.Forms.TabPage();
			this.flowDisplay = new System.Windows.Forms.FlowLayoutPanel();
			this.grpDisplayMode = new System.Windows.Forms.GroupBox();
			this.c_clearBetweenRandomGroups = new System.Windows.Forms.CheckBox();
			this.c_randomGroupCountLabel = new System.Windows.Forms.Label();
			this.c_randomGroupCount = new System.Windows.Forms.TextBox();
			this.c_randomGroup = new System.Windows.Forms.CheckBox();
			this.c_maxClipLabel = new System.Windows.Forms.Label();
			this.c_maxScalePct = new System.Windows.Forms.Label();
			this.c_maxScale = new System.Windows.Forms.TextBox();
			this.c_maxClipPercent = new System.Windows.Forms.Label();
			this.c_maxClipTrackBar = new System.Windows.Forms.TrackBar();
			this.c_allowSpanning = new System.Windows.Forms.CheckBox();
			this.c_themeOrder = new System.Windows.Forms.ComboBox();
			this.c_limitScale = new System.Windows.Forms.CheckBox();
			this.grpFrequency = new System.Windows.Forms.GroupBox();
			this.c_activateOnExitCheckBox = new System.Windows.Forms.CheckBox();
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
			this.c_numCollageImagesLabel = new System.Windows.Forms.Label();
			this.c_numCollageImages = new System.Windows.Forms.TextBox();
			this.c_borderColor = new WallSwitch.ColorSample();
			this.c_borderColorLabel = new System.Windows.Forms.Label();
			this.c_edgeMode = new System.Windows.Forms.ComboBox();
			this.trkDropShadowFeatherDist = new System.Windows.Forms.TrackBar();
			this.trkDropShadowOpacity = new System.Windows.Forms.TrackBar();
			this.lblDropShadowOpacity = new System.Windows.Forms.Label();
			this.lblDropShadowOpacityValue = new System.Windows.Forms.Label();
			this.lblDropShadowFeatherDist = new System.Windows.Forms.Label();
			this.chkDropShadowFeather = new System.Windows.Forms.CheckBox();
			this.chkDropShadow = new System.Windows.Forms.CheckBox();
			this.lblDropShadowUnit = new System.Windows.Forms.Label();
			this.trkDropShadow = new System.Windows.Forms.TrackBar();
			this.c_edgeDistLabel = new System.Windows.Forms.Label();
			this.c_edgeDist = new System.Windows.Forms.TrackBar();
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
			this.c_filterTab = new System.Windows.Forms.TabPage();
			this.c_filterFlow = new System.Windows.Forms.FlowLayoutPanel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.c_addFilterButton = new System.Windows.Forms.Button();
			this.c_widgetsTab = new System.Windows.Forms.TabPage();
			this.c_widgetPanelSplitter = new System.Windows.Forms.SplitContainer();
			this.c_widgetLayout = new WallSwitch.WidgetLayoutControl();
			this.c_widgetTopPanel = new System.Windows.Forms.Panel();
			this.c_addWidgetButton = new System.Windows.Forms.Button();
			this.c_widgetTypes = new System.Windows.Forms.ComboBox();
			this.c_widgetTypesLabel = new System.Windows.Forms.Label();
			this.c_widgetPanelPropSplitter = new System.Windows.Forms.SplitContainer();
			this.c_widgetList = new System.Windows.Forms.ListView();
			this.c_widgetColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.c_widgetControlRightPanel = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.c_widgetDeleteButton = new System.Windows.Forms.Button();
			this.c_widgetMoveUpButton = new System.Windows.Forms.Button();
			this.c_widgetMoveDownButton = new System.Windows.Forms.Button();
			this.c_widgetPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.tabHistory = new System.Windows.Forms.TabPage();
			this.c_historyTab = new WallSwitch.HistoryList();
			this.cmHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciOpenHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ciExploreHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ciDeleteHistoryFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.ciClearHistoryList = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grpNavButtons = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.c_transparencyTrackBar = new System.Windows.Forms.TrackBar();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.cmLocations.SuspendLayout();
			this.grpTheme.SuspendLayout();
			this.cmTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).BeginInit();
			this.cmTray.SuspendLayout();
			this.mainMenu.SuspendLayout();
			this.c_themeTabControl.SuspendLayout();
			this.c_locationsTab.SuspendLayout();
			this.c_settingsTab.SuspendLayout();
			this.flowDisplay.SuspendLayout();
			this.grpDisplayMode.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_maxClipTrackBar)).BeginInit();
			this.grpFrequency.SuspendLayout();
			this.grpBackgroundColor.SuspendLayout();
			this.grpCollageDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowFeatherDist)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c_edgeDist)).BeginInit();
			this.grpImageEffects.SuspendLayout();
			this.grpBackgroundColorEffects.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBackgroundBlurDist)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkColorEffectCollageFadeRatio)).BeginInit();
			this.c_filterTab.SuspendLayout();
			this.panel3.SuspendLayout();
			this.c_widgetsTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelSplitter)).BeginInit();
			this.c_widgetPanelSplitter.Panel1.SuspendLayout();
			this.c_widgetPanelSplitter.Panel2.SuspendLayout();
			this.c_widgetPanelSplitter.SuspendLayout();
			this.c_widgetTopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelPropSplitter)).BeginInit();
			this.c_widgetPanelPropSplitter.Panel1.SuspendLayout();
			this.c_widgetPanelPropSplitter.Panel2.SuspendLayout();
			this.c_widgetPanelPropSplitter.SuspendLayout();
			this.c_widgetControlRightPanel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabHistory.SuspendLayout();
			this.cmHistory.SuspendLayout();
			this.grpNavButtons.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_transparencyTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// lstLocations
			// 
			this.lstLocations.AllowDrop = true;
			this.lstLocations.CheckBoxes = true;
			this.lstLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLocation,
            this.colNextUpdate,
            this.colFrequency});
			this.lstLocations.ContextMenuStrip = this.cmLocations;
			this.lstLocations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstLocations.Location = new System.Drawing.Point(4, 4);
			this.lstLocations.Margin = new System.Windows.Forms.Padding(4);
			this.lstLocations.Name = "lstLocations";
			this.lstLocations.Size = new System.Drawing.Size(931, 476);
			this.lstLocations.SmallImageList = this.c_locationImages;
			this.lstLocations.TabIndex = 0;
			this.toolTip1.SetToolTip(this.lstLocations, "Locations where images are retrieved");
			this.lstLocations.UseCompatibleStateImageBehavior = false;
			this.lstLocations.View = System.Windows.Forms.View.Details;
			this.lstLocations.ItemActivate += new System.EventHandler(this.lstLocations_ItemActivate);
			this.lstLocations.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstLocations_ItemChecked);
			this.lstLocations.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstLocations_DragDrop);
			this.lstLocations.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstLocations_DragEnter);
			this.lstLocations.Resize += new System.EventHandler(this.Locations_Resize);
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
			// colFrequency
			// 
			this.colFrequency.Text = "Refresh";
			this.colFrequency.Width = 100;
			// 
			// cmLocations
			// 
			this.cmLocations.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmLocations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_browseLocationMenuItem,
            this.toolStripMenuItem8,
            this.ciAddFolder,
            this.miAddRssFeed,
            this.ciAddImage,
            this.toolStripMenuItem7,
            this.ciLocationExplore,
            this.toolStripMenuItem6,
            this.ciUpdateLocationNow,
            this.toolStripMenuItem5,
            this.ciDeleteLocation,
            this.ciLocationProperties});
			this.cmLocations.Name = "locationsMenu";
			this.cmLocations.Size = new System.Drawing.Size(235, 236);
			this.cmLocations.Opening += new System.ComponentModel.CancelEventHandler(this.Locations_Opening);
			// 
			// ciAddFolder
			// 
			this.ciAddFolder.Name = "ciAddFolder";
			this.ciAddFolder.Size = new System.Drawing.Size(234, 26);
			this.ciAddFolder.Text = "Add &Folder";
			this.ciAddFolder.Click += new System.EventHandler(this.ciAddFolder_Click);
			// 
			// miAddRssFeed
			// 
			this.miAddRssFeed.Name = "miAddRssFeed";
			this.miAddRssFeed.Size = new System.Drawing.Size(234, 26);
			this.miAddRssFeed.Text = "&Add Feed";
			this.miAddRssFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
			// 
			// ciAddImage
			// 
			this.ciAddImage.Name = "ciAddImage";
			this.ciAddImage.Size = new System.Drawing.Size(234, 26);
			this.ciAddImage.Text = "Add &Image";
			this.ciAddImage.Click += new System.EventHandler(this.ciAddImage_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(231, 6);
			// 
			// c_browseLocationMenuItem
			// 
			this.c_browseLocationMenuItem.Name = "c_browseLocationMenuItem";
			this.c_browseLocationMenuItem.Size = new System.Drawing.Size(234, 26);
			this.c_browseLocationMenuItem.Text = "&Browse";
			this.c_browseLocationMenuItem.Click += new System.EventHandler(this.BrowseLocationMenuItem_Click);
			// 
			// ciLocationExplore
			// 
			this.ciLocationExplore.Name = "ciLocationExplore";
			this.ciLocationExplore.Size = new System.Drawing.Size(234, 26);
			this.ciLocationExplore.Text = "&Explore";
			this.ciLocationExplore.Click += new System.EventHandler(this.ciLocationExplore_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(231, 6);
			// 
			// ciUpdateLocationNow
			// 
			this.ciUpdateLocationNow.Name = "ciUpdateLocationNow";
			this.ciUpdateLocationNow.Size = new System.Drawing.Size(234, 26);
			this.ciUpdateLocationNow.Text = "&Update At Next Switch";
			this.ciUpdateLocationNow.Click += new System.EventHandler(this.ciUpdateLocationNow_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(231, 6);
			// 
			// ciDeleteLocation
			// 
			this.ciDeleteLocation.Name = "ciDeleteLocation";
			this.ciDeleteLocation.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ciDeleteLocation.Size = new System.Drawing.Size(234, 26);
			this.ciDeleteLocation.Text = "&Delete";
			this.ciDeleteLocation.Click += new System.EventHandler(this.ciDeleteLocation_Click);
			// 
			// ciLocationProperties
			// 
			this.ciLocationProperties.Name = "ciLocationProperties";
			this.ciLocationProperties.Size = new System.Drawing.Size(234, 26);
			this.ciLocationProperties.Text = "&Properties";
			this.ciLocationProperties.Click += new System.EventHandler(this.ciLocationProperties_Click);
			// 
			// c_locationImages
			// 
			this.c_locationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("c_locationImages.ImageStream")));
			this.c_locationImages.TransparentColor = System.Drawing.Color.Transparent;
			this.c_locationImages.Images.SetKeyName(0, "Folder.png");
			this.c_locationImages.Images.SetKeyName(1, "RSS.ico");
			this.c_locationImages.Images.SetKeyName(2, "ImageFile.png");
			// 
			// cmbTheme
			// 
			this.cmbTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTheme.FormattingEnabled = true;
			this.cmbTheme.Location = new System.Drawing.Point(8, 23);
			this.cmbTheme.Margin = new System.Windows.Forms.Padding(4);
			this.cmbTheme.Name = "cmbTheme";
			this.cmbTheme.Size = new System.Drawing.Size(327, 24);
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
			this.grpTheme.Location = new System.Drawing.Point(16, 33);
			this.grpTheme.Margin = new System.Windows.Forms.Padding(4);
			this.grpTheme.Name = "grpTheme";
			this.grpTheme.Padding = new System.Windows.Forms.Padding(4);
			this.grpTheme.Size = new System.Drawing.Size(600, 62);
			this.grpTheme.TabIndex = 0;
			this.grpTheme.TabStop = false;
			this.grpTheme.Text = "Theme";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(384, 21);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(100, 28);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// btnTheme
			// 
			this.btnTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTheme.ContextMenuStrip = this.cmTheme;
			this.btnTheme.Location = new System.Drawing.Point(344, 21);
			this.btnTheme.Margin = new System.Windows.Forms.Padding(4);
			this.btnTheme.Name = "btnTheme";
			this.btnTheme.Size = new System.Drawing.Size(32, 28);
			this.btnTheme.TabIndex = 1;
			this.btnTheme.Text = "...";
			this.btnTheme.UseVisualStyleBackColor = true;
			this.btnTheme.Click += new System.EventHandler(this.ThemeMenuButton_Click);
			// 
			// cmTheme
			// 
			this.cmTheme.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmTheme.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciNewTheme,
            this.ciRenameTheme,
            this.ciDuplicateTheme,
            this.toolStripMenuItem9,
            this.ciSaveTheme,
            this.toolStripMenuItem10,
            this.ciDeleteTheme});
			this.cmTheme.Name = "c_themeMenu";
			this.cmTheme.Size = new System.Drawing.Size(143, 136);
			// 
			// ciNewTheme
			// 
			this.ciNewTheme.Name = "ciNewTheme";
			this.ciNewTheme.Size = new System.Drawing.Size(142, 24);
			this.ciNewTheme.Text = "&New";
			this.ciNewTheme.Click += new System.EventHandler(this.btnNewTheme_Click);
			// 
			// ciRenameTheme
			// 
			this.ciRenameTheme.Name = "ciRenameTheme";
			this.ciRenameTheme.Size = new System.Drawing.Size(142, 24);
			this.ciRenameTheme.Text = "&Rename";
			this.ciRenameTheme.Click += new System.EventHandler(this.btnRenameTheme_Click);
			// 
			// ciDuplicateTheme
			// 
			this.ciDuplicateTheme.Name = "ciDuplicateTheme";
			this.ciDuplicateTheme.Size = new System.Drawing.Size(142, 24);
			this.ciDuplicateTheme.Text = "D&uplicate";
			this.ciDuplicateTheme.Click += new System.EventHandler(this.miDuplicateTheme_Click);
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(139, 6);
			// 
			// ciSaveTheme
			// 
			this.ciSaveTheme.Name = "ciSaveTheme";
			this.ciSaveTheme.Size = new System.Drawing.Size(142, 24);
			this.ciSaveTheme.Text = "&Save";
			this.ciSaveTheme.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(139, 6);
			// 
			// ciDeleteTheme
			// 
			this.ciDeleteTheme.Name = "ciDeleteTheme";
			this.ciDeleteTheme.Size = new System.Drawing.Size(142, 24);
			this.ciDeleteTheme.Text = "&Delete";
			this.ciDeleteTheme.Click += new System.EventHandler(this.btnDeleteTheme_Click);
			// 
			// btnActivate
			// 
			this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnActivate.Location = new System.Drawing.Point(492, 21);
			this.btnActivate.Margin = new System.Windows.Forms.Padding(4);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(100, 28);
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
			this.btnSwitchNow.Location = new System.Drawing.Point(111, 21);
			this.btnSwitchNow.Margin = new System.Windows.Forms.Padding(4);
			this.btnSwitchNow.Name = "btnSwitchNow";
			this.btnSwitchNow.Size = new System.Drawing.Size(40, 28);
			this.btnSwitchNow.TabIndex = 2;
			this.toolTip1.SetToolTip(this.btnSwitchNow, "Go to the next image");
			this.btnSwitchNow.UseVisualStyleBackColor = true;
			this.btnSwitchNow.Click += new System.EventHandler(this.btnSwitchNow_Click);
			// 
			// btnPause
			// 
			this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPause.Image = global::WallSwitch.Res.PauseIcon;
			this.btnPause.Location = new System.Drawing.Point(63, 21);
			this.btnPause.Margin = new System.Windows.Forms.Padding(4);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(40, 28);
			this.btnPause.TabIndex = 1;
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.Image = global::WallSwitch.Res.PrevIcon;
			this.btnPrevious.Location = new System.Drawing.Point(15, 21);
			this.btnPrevious.Margin = new System.Windows.Forms.Padding(4);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(40, 28);
			this.btnPrevious.TabIndex = 0;
			this.toolTip1.SetToolTip(this.btnPrevious, "Go back to the previous image");
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// c_imageFit
			// 
			this.c_imageFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_imageFit.FormattingEnabled = true;
			this.c_imageFit.Items.AddRange(new object[] {
            "Original Size",
            "Stretch",
            "Fit",
            "Fill"});
			this.c_imageFit.Location = new System.Drawing.Point(400, 23);
			this.c_imageFit.Margin = new System.Windows.Forms.Padding(4);
			this.c_imageFit.Name = "c_imageFit";
			this.c_imageFit.Size = new System.Drawing.Size(187, 24);
			this.c_imageFit.TabIndex = 2;
			this.toolTip1.SetToolTip(this.c_imageFit, "Image sizing method");
			this.c_imageFit.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblOpacityDisplay
			// 
			this.lblOpacityDisplay.AutoSize = true;
			this.lblOpacityDisplay.Location = new System.Drawing.Point(628, 23);
			this.lblOpacityDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOpacityDisplay.Name = "lblOpacityDisplay";
			this.lblOpacityDisplay.Size = new System.Drawing.Size(20, 17);
			this.lblOpacityDisplay.TabIndex = 5;
			this.lblOpacityDisplay.Text = "%";
			this.toolTip1.SetToolTip(this.lblOpacityDisplay, "Opacity of background used to fade out previous images");
			// 
			// trkOpacity
			// 
			this.trkOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkOpacity.LargeChange = 20;
			this.trkOpacity.Location = new System.Drawing.Point(433, 23);
			this.trkOpacity.Margin = new System.Windows.Forms.Padding(4);
			this.trkOpacity.Maximum = 100;
			this.trkOpacity.Name = "trkOpacity";
			this.trkOpacity.Size = new System.Drawing.Size(187, 56);
			this.trkOpacity.SmallChange = 10;
			this.trkOpacity.TabIndex = 4;
			this.trkOpacity.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkOpacity, "Opacity of background used to fade out previous images");
			this.trkOpacity.Value = 50;
			this.trkOpacity.Scroll += new System.EventHandler(this.Opacity_Scroll);
			// 
			// lblOpacity
			// 
			this.lblOpacity.AutoSize = true;
			this.lblOpacity.Location = new System.Drawing.Point(283, 23);
			this.lblOpacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOpacity.Name = "lblOpacity";
			this.lblOpacity.Size = new System.Drawing.Size(140, 17);
			this.lblOpacity.TabIndex = 3;
			this.lblOpacity.Text = "Background Opacity:";
			this.toolTip1.SetToolTip(this.lblOpacity, "Opacity of background used to fade out previous images");
			// 
			// c_separateMonitors
			// 
			this.c_separateMonitors.AutoSize = true;
			this.c_separateMonitors.Checked = true;
			this.c_separateMonitors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.c_separateMonitors.Location = new System.Drawing.Point(8, 62);
			this.c_separateMonitors.Margin = new System.Windows.Forms.Padding(4);
			this.c_separateMonitors.Name = "c_separateMonitors";
			this.c_separateMonitors.Size = new System.Drawing.Size(237, 21);
			this.c_separateMonitors.TabIndex = 3;
			this.c_separateMonitors.Text = "Separate image for each monitor";
			this.toolTip1.SetToolTip(this.c_separateMonitors, "Display a separate image on each monitor");
			this.c_separateMonitors.UseVisualStyleBackColor = true;
			this.c_separateMonitors.CheckedChanged += new System.EventHandler(this.chkSeparateMonitors_CheckedChanged);
			// 
			// lblImageSizeDisplay
			// 
			this.lblImageSizeDisplay.AutoSize = true;
			this.lblImageSizeDisplay.Location = new System.Drawing.Point(353, 23);
			this.lblImageSizeDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblImageSizeDisplay.Name = "lblImageSizeDisplay";
			this.lblImageSizeDisplay.Size = new System.Drawing.Size(20, 17);
			this.lblImageSizeDisplay.TabIndex = 2;
			this.lblImageSizeDisplay.Text = "%";
			this.toolTip1.SetToolTip(this.lblImageSizeDisplay, "Image size in relation to the screen");
			// 
			// lblImageSize
			// 
			this.lblImageSize.AutoSize = true;
			this.lblImageSize.Location = new System.Drawing.Point(8, 23);
			this.lblImageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblImageSize.Name = "lblImageSize";
			this.lblImageSize.Size = new System.Drawing.Size(81, 17);
			this.lblImageSize.TabIndex = 0;
			this.lblImageSize.Text = "Image Size:";
			this.toolTip1.SetToolTip(this.lblImageSize, "Image size in relation to the screen");
			// 
			// trkImageSize
			// 
			this.trkImageSize.BackColor = System.Drawing.SystemColors.Window;
			this.trkImageSize.LargeChange = 20;
			this.trkImageSize.Location = new System.Drawing.Point(159, 23);
			this.trkImageSize.Margin = new System.Windows.Forms.Padding(4);
			this.trkImageSize.Maximum = 100;
			this.trkImageSize.Minimum = 1;
			this.trkImageSize.Name = "trkImageSize";
			this.trkImageSize.Size = new System.Drawing.Size(187, 56);
			this.trkImageSize.SmallChange = 10;
			this.trkImageSize.TabIndex = 1;
			this.trkImageSize.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkImageSize, "Image size in relation to the screen");
			this.trkImageSize.Value = 50;
			this.trkImageSize.Scroll += new System.EventHandler(this.ImageSize_Scroll);
			// 
			// lblBackBottom
			// 
			this.lblBackBottom.AutoSize = true;
			this.lblBackBottom.Location = new System.Drawing.Point(8, 53);
			this.lblBackBottom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBackBottom.Name = "lblBackBottom";
			this.lblBackBottom.Size = new System.Drawing.Size(152, 17);
			this.lblBackBottom.TabIndex = 2;
			this.lblBackBottom.Text = "Bottom Gradient Color:";
			this.toolTip1.SetToolTip(this.lblBackBottom, "Bottom color of background gradient");
			// 
			// lblBackTop
			// 
			this.lblBackTop.AutoSize = true;
			this.lblBackTop.Location = new System.Drawing.Point(8, 23);
			this.lblBackTop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBackTop.Name = "lblBackTop";
			this.lblBackTop.Size = new System.Drawing.Size(133, 17);
			this.lblBackTop.TabIndex = 0;
			this.lblBackTop.Text = "Top Gradient Color:";
			this.toolTip1.SetToolTip(this.lblBackTop, "Top color of background gradient");
			// 
			// btnAddImage
			// 
			this.btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddImage.Location = new System.Drawing.Point(1107, 43);
			this.btnAddImage.Margin = new System.Windows.Forms.Padding(4);
			this.btnAddImage.Name = "btnAddImage";
			this.btnAddImage.Size = new System.Drawing.Size(160, 28);
			this.btnAddImage.TabIndex = 2;
			this.btnAddImage.Text = "Add &Image";
			this.toolTip1.SetToolTip(this.btnAddImage, "Add a single image file to the list");
			this.btnAddImage.UseVisualStyleBackColor = true;
			this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
			// 
			// c_themeMode
			// 
			this.c_themeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_themeMode.FormattingEnabled = true;
			this.c_themeMode.Items.AddRange(new object[] {
            "Full Screen",
            "Collage"});
			this.c_themeMode.Location = new System.Drawing.Point(8, 23);
			this.c_themeMode.Margin = new System.Windows.Forms.Padding(4);
			this.c_themeMode.Name = "c_themeMode";
			this.c_themeMode.Size = new System.Drawing.Size(187, 24);
			this.c_themeMode.TabIndex = 0;
			this.toolTip1.SetToolTip(this.c_themeMode, "Display mode");
			this.c_themeMode.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// btnAddFolder
			// 
			this.btnAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFolder.Location = new System.Drawing.Point(1107, 7);
			this.btnAddFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btnAddFolder.Name = "btnAddFolder";
			this.btnAddFolder.Size = new System.Drawing.Size(160, 28);
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
			this.trayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);
			// 
			// cmTray
			// 
			this.cmTray.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowMainWindow,
            this.ciTheme,
            this.toolStripSeparator1,
            this.cmSwitchNow,
            this.ciSwitchPrev,
            this.toolStripMenuItem1,
            this.cmExit});
			this.cmTray.Name = "trayMenu";
			this.cmTray.Size = new System.Drawing.Size(212, 146);
			this.cmTray.Opening += new System.ComponentModel.CancelEventHandler(this.TrayMenu_Opening);
			// 
			// cmShowMainWindow
			// 
			this.cmShowMainWindow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmShowMainWindow.Name = "cmShowMainWindow";
			this.cmShowMainWindow.Size = new System.Drawing.Size(211, 26);
			this.cmShowMainWindow.Text = "&Show";
			this.cmShowMainWindow.Click += new System.EventHandler(this.cmShowMainWindow_Click);
			// 
			// ciTheme
			// 
			this.ciTheme.Name = "ciTheme";
			this.ciTheme.Size = new System.Drawing.Size(211, 26);
			this.ciTheme.Text = "&Theme";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
			// 
			// cmSwitchNow
			// 
			this.cmSwitchNow.Name = "cmSwitchNow";
			this.cmSwitchNow.Size = new System.Drawing.Size(211, 26);
			this.cmSwitchNow.Text = "&Next Wallpaper";
			this.cmSwitchNow.Click += new System.EventHandler(this.ciSwitchNow_Click);
			// 
			// ciSwitchPrev
			// 
			this.ciSwitchPrev.Name = "ciSwitchPrev";
			this.ciSwitchPrev.Size = new System.Drawing.Size(211, 26);
			this.ciSwitchPrev.Text = "&Previous Wallpaper";
			this.ciSwitchPrev.Click += new System.EventHandler(this.ciSwitchPrev_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(208, 6);
			// 
			// cmExit
			// 
			this.cmExit.Name = "cmExit";
			this.cmExit.Size = new System.Drawing.Size(211, 26);
			this.cmExit.Text = "E&xit";
			this.cmExit.Click += new System.EventHandler(this.cmExit_Click);
			// 
			// mainMenu
			// 
			this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
            this.menuHelp});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.mainMenu.Size = new System.Drawing.Size(979, 28);
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
			this.menuFile.Size = new System.Drawing.Size(44, 24);
			this.menuFile.Text = "&File";
			// 
			// miFileNewTheme
			// 
			this.miFileNewTheme.Name = "miFileNewTheme";
			this.miFileNewTheme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.miFileNewTheme.Size = new System.Drawing.Size(238, 26);
			this.miFileNewTheme.Text = "&New Theme";
			this.miFileNewTheme.Click += new System.EventHandler(this.miFileNewTheme_Click);
			// 
			// miFileRenameTheme
			// 
			this.miFileRenameTheme.Name = "miFileRenameTheme";
			this.miFileRenameTheme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.miFileRenameTheme.Size = new System.Drawing.Size(238, 26);
			this.miFileRenameTheme.Text = "&Rename Theme";
			this.miFileRenameTheme.Click += new System.EventHandler(this.miFileRenameTheme_Click);
			// 
			// miDuplicateTheme
			// 
			this.miDuplicateTheme.Name = "miDuplicateTheme";
			this.miDuplicateTheme.Size = new System.Drawing.Size(238, 26);
			this.miDuplicateTheme.Text = "D&uplicate Theme";
			this.miDuplicateTheme.Click += new System.EventHandler(this.miDuplicateTheme_Click);
			// 
			// miFileDeleteTheme
			// 
			this.miFileDeleteTheme.Name = "miFileDeleteTheme";
			this.miFileDeleteTheme.Size = new System.Drawing.Size(238, 26);
			this.miFileDeleteTheme.Text = "&Delete Theme";
			this.miFileDeleteTheme.Click += new System.EventHandler(this.miFileDeleteTheme_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(235, 6);
			// 
			// miFileSave
			// 
			this.miFileSave.Name = "miFileSave";
			this.miFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.miFileSave.Size = new System.Drawing.Size(238, 26);
			this.miFileSave.Text = "&Save";
			this.miFileSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(235, 6);
			// 
			// miFileExit
			// 
			this.miFileExit.Name = "miFileExit";
			this.miFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.miFileExit.Size = new System.Drawing.Size(238, 26);
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
			this.menuTools.Size = new System.Drawing.Size(56, 24);
			this.menuTools.Text = "&Tools";
			// 
			// miClearHistory
			// 
			this.miClearHistory.Name = "miClearHistory";
			this.miClearHistory.Size = new System.Drawing.Size(169, 26);
			this.miClearHistory.Text = "&Clear History";
			this.miClearHistory.Click += new System.EventHandler(this.miClearHistory_Click);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(166, 6);
			// 
			// miHotKeys
			// 
			this.miHotKeys.Name = "miHotKeys";
			this.miHotKeys.Size = new System.Drawing.Size(169, 26);
			this.miHotKeys.Text = "&Hot Keys";
			this.miHotKeys.Click += new System.EventHandler(this.miHotKeys_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
			this.settingsToolStripMenuItem.Text = "&Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelpAbout});
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Size = new System.Drawing.Size(53, 24);
			this.menuHelp.Text = "&Help";
			// 
			// miHelpAbout
			// 
			this.miHelpAbout.Name = "miHelpAbout";
			this.miHelpAbout.Size = new System.Drawing.Size(125, 26);
			this.miHelpAbout.Text = "&About";
			this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
			// 
			// c_themeTabControl
			// 
			this.c_themeTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.c_themeTabControl.Controls.Add(this.c_locationsTab);
			this.c_themeTabControl.Controls.Add(this.c_settingsTab);
			this.c_themeTabControl.Controls.Add(this.c_filterTab);
			this.c_themeTabControl.Controls.Add(this.c_widgetsTab);
			this.c_themeTabControl.Controls.Add(this.tabHistory);
			this.c_themeTabControl.Location = new System.Drawing.Point(16, 102);
			this.c_themeTabControl.Margin = new System.Windows.Forms.Padding(4);
			this.c_themeTabControl.Name = "c_themeTabControl";
			this.c_themeTabControl.SelectedIndex = 0;
			this.c_themeTabControl.Size = new System.Drawing.Size(947, 513);
			this.c_themeTabControl.TabIndex = 10;
			// 
			// c_locationsTab
			// 
			this.c_locationsTab.Controls.Add(this.btnAddFeed);
			this.c_locationsTab.Controls.Add(this.lstLocations);
			this.c_locationsTab.Controls.Add(this.btnAddFolder);
			this.c_locationsTab.Controls.Add(this.btnAddImage);
			this.c_locationsTab.Location = new System.Drawing.Point(4, 25);
			this.c_locationsTab.Margin = new System.Windows.Forms.Padding(4);
			this.c_locationsTab.Name = "c_locationsTab";
			this.c_locationsTab.Padding = new System.Windows.Forms.Padding(4);
			this.c_locationsTab.Size = new System.Drawing.Size(939, 484);
			this.c_locationsTab.TabIndex = 0;
			this.c_locationsTab.Text = "Images";
			this.c_locationsTab.UseVisualStyleBackColor = true;
			// 
			// btnAddFeed
			// 
			this.btnAddFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFeed.Location = new System.Drawing.Point(1105, 79);
			this.btnAddFeed.Margin = new System.Windows.Forms.Padding(4);
			this.btnAddFeed.Name = "btnAddFeed";
			this.btnAddFeed.Size = new System.Drawing.Size(160, 28);
			this.btnAddFeed.TabIndex = 3;
			this.btnAddFeed.Text = "Add &Feed";
			this.toolTip1.SetToolTip(this.btnAddFeed, "Add a RSS/ATOM feed to the list");
			this.btnAddFeed.UseVisualStyleBackColor = true;
			this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
			// 
			// c_settingsTab
			// 
			this.c_settingsTab.Controls.Add(this.flowDisplay);
			this.c_settingsTab.Location = new System.Drawing.Point(4, 25);
			this.c_settingsTab.Margin = new System.Windows.Forms.Padding(4);
			this.c_settingsTab.Name = "c_settingsTab";
			this.c_settingsTab.Padding = new System.Windows.Forms.Padding(4);
			this.c_settingsTab.Size = new System.Drawing.Size(939, 484);
			this.c_settingsTab.TabIndex = 1;
			this.c_settingsTab.Text = "Settings";
			this.c_settingsTab.UseVisualStyleBackColor = true;
			// 
			// flowDisplay
			// 
			this.flowDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowDisplay.AutoScroll = true;
			this.flowDisplay.Controls.Add(this.grpDisplayMode);
			this.flowDisplay.Controls.Add(this.grpFrequency);
			this.flowDisplay.Controls.Add(this.grpBackgroundColor);
			this.flowDisplay.Controls.Add(this.grpCollageDisplay);
			this.flowDisplay.Controls.Add(this.grpImageEffects);
			this.flowDisplay.Controls.Add(this.grpBackgroundColorEffects);
			this.flowDisplay.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowDisplay.Location = new System.Drawing.Point(4, 4);
			this.flowDisplay.Margin = new System.Windows.Forms.Padding(4);
			this.flowDisplay.Name = "flowDisplay";
			this.flowDisplay.Size = new System.Drawing.Size(928, 474);
			this.flowDisplay.TabIndex = 14;
			this.flowDisplay.WrapContents = false;
			// 
			// grpDisplayMode
			// 
			this.grpDisplayMode.Controls.Add(this.c_clearBetweenRandomGroups);
			this.grpDisplayMode.Controls.Add(this.c_randomGroupCountLabel);
			this.grpDisplayMode.Controls.Add(this.c_randomGroupCount);
			this.grpDisplayMode.Controls.Add(this.c_randomGroup);
			this.grpDisplayMode.Controls.Add(this.c_maxClipLabel);
			this.grpDisplayMode.Controls.Add(this.c_maxScalePct);
			this.grpDisplayMode.Controls.Add(this.c_maxScale);
			this.grpDisplayMode.Controls.Add(this.c_maxClipPercent);
			this.grpDisplayMode.Controls.Add(this.c_maxClipTrackBar);
			this.grpDisplayMode.Controls.Add(this.c_allowSpanning);
			this.grpDisplayMode.Controls.Add(this.c_themeOrder);
			this.grpDisplayMode.Controls.Add(this.c_themeMode);
			this.grpDisplayMode.Controls.Add(this.c_imageFit);
			this.grpDisplayMode.Controls.Add(this.c_limitScale);
			this.grpDisplayMode.Controls.Add(this.c_separateMonitors);
			this.grpDisplayMode.Location = new System.Drawing.Point(4, 4);
			this.grpDisplayMode.Margin = new System.Windows.Forms.Padding(4);
			this.grpDisplayMode.Name = "grpDisplayMode";
			this.grpDisplayMode.Padding = new System.Windows.Forms.Padding(4);
			this.grpDisplayMode.Size = new System.Drawing.Size(892, 184);
			this.grpDisplayMode.TabIndex = 0;
			this.grpDisplayMode.TabStop = false;
			this.grpDisplayMode.Text = "Display Mode";
			// 
			// c_clearBetweenRandomGroups
			// 
			this.c_clearBetweenRandomGroups.AutoSize = true;
			this.c_clearBetweenRandomGroups.Location = new System.Drawing.Point(404, 149);
			this.c_clearBetweenRandomGroups.Name = "c_clearBetweenRandomGroups";
			this.c_clearBetweenRandomGroups.Size = new System.Drawing.Size(176, 21);
			this.c_clearBetweenRandomGroups.TabIndex = 14;
			this.c_clearBetweenRandomGroups.Text = "Clear between groups?";
			this.c_clearBetweenRandomGroups.UseVisualStyleBackColor = true;
			this.c_clearBetweenRandomGroups.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_randomGroupCountLabel
			// 
			this.c_randomGroupCountLabel.AutoSize = true;
			this.c_randomGroupCountLabel.Location = new System.Drawing.Point(279, 150);
			this.c_randomGroupCountLabel.Name = "c_randomGroupCountLabel";
			this.c_randomGroupCountLabel.Size = new System.Drawing.Size(119, 17);
			this.c_randomGroupCountLabel.TabIndex = 13;
			this.c_randomGroupCountLabel.Text = "images per group";
			this.toolTip1.SetToolTip(this.c_randomGroupCountLabel, "The number of sequential images chosen between random selections.");
			// 
			// c_randomGroupCount
			// 
			this.c_randomGroupCount.Location = new System.Drawing.Point(204, 147);
			this.c_randomGroupCount.Name = "c_randomGroupCount";
			this.c_randomGroupCount.Size = new System.Drawing.Size(65, 22);
			this.c_randomGroupCount.TabIndex = 12;
			this.toolTip1.SetToolTip(this.c_randomGroupCount, "The number of sequential images chosen between random selections.");
			this.c_randomGroupCount.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_randomGroup
			// 
			this.c_randomGroup.AutoSize = true;
			this.c_randomGroup.Location = new System.Drawing.Point(8, 149);
			this.c_randomGroup.Name = "c_randomGroup";
			this.c_randomGroup.Size = new System.Drawing.Size(156, 21);
			this.c_randomGroup.TabIndex = 11;
			this.c_randomGroup.Text = "Sequential Groups?";
			this.toolTip1.SetToolTip(this.c_randomGroup, "Enable groups of sequential images to be displayed before selecting the next rand" +
        "om image?");
			this.c_randomGroup.UseVisualStyleBackColor = true;
			this.c_randomGroup.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_maxClipLabel
			// 
			this.c_maxClipLabel.AutoSize = true;
			this.c_maxClipLabel.Location = new System.Drawing.Point(288, 91);
			this.c_maxClipLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_maxClipLabel.Name = "c_maxClipLabel";
			this.c_maxClipLabel.Size = new System.Drawing.Size(135, 17);
			this.c_maxClipLabel.TabIndex = 5;
			this.c_maxClipLabel.Text = "Maximum Image Clip";
			this.toolTip1.SetToolTip(this.c_maxClipLabel, "When spanning across monitors that don\'t make a perfect rectangle, part of the im" +
        "age may be offscreen. This setting limits how much of the image WallSwitch will " +
        "allow to be clipped.");
			// 
			// c_maxScalePct
			// 
			this.c_maxScalePct.AutoSize = true;
			this.c_maxScalePct.Location = new System.Drawing.Point(279, 122);
			this.c_maxScalePct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_maxScalePct.Name = "c_maxScalePct";
			this.c_maxScalePct.Size = new System.Drawing.Size(20, 17);
			this.c_maxScalePct.TabIndex = 10;
			this.c_maxScalePct.Text = "%";
			this.toolTip1.SetToolTip(this.c_maxScalePct, "Maximum amount of magnification (in percent)");
			// 
			// c_maxScale
			// 
			this.c_maxScale.Location = new System.Drawing.Point(204, 118);
			this.c_maxScale.Margin = new System.Windows.Forms.Padding(4);
			this.c_maxScale.Name = "c_maxScale";
			this.c_maxScale.Size = new System.Drawing.Size(65, 22);
			this.c_maxScale.TabIndex = 9;
			this.toolTip1.SetToolTip(this.c_maxScale, "Maximum amount of magnification (in percent)");
			this.c_maxScale.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_maxClipPercent
			// 
			this.c_maxClipPercent.AutoSize = true;
			this.c_maxClipPercent.Location = new System.Drawing.Point(613, 91);
			this.c_maxClipPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_maxClipPercent.Name = "c_maxClipPercent";
			this.c_maxClipPercent.Size = new System.Drawing.Size(20, 17);
			this.c_maxClipPercent.TabIndex = 7;
			this.c_maxClipPercent.Text = "%";
			this.toolTip1.SetToolTip(this.c_maxClipPercent, "When spanning across monitors that don\'t make a perfect rectangle, part of the im" +
        "age may be offscreen. This setting limits how much of the image WallSwitch will " +
        "allow to be clipped.");
			// 
			// c_maxClipTrackBar
			// 
			this.c_maxClipTrackBar.BackColor = System.Drawing.SystemColors.Window;
			this.c_maxClipTrackBar.LargeChange = 20;
			this.c_maxClipTrackBar.Location = new System.Drawing.Point(433, 82);
			this.c_maxClipTrackBar.Margin = new System.Windows.Forms.Padding(4);
			this.c_maxClipTrackBar.Maximum = 100;
			this.c_maxClipTrackBar.Name = "c_maxClipTrackBar";
			this.c_maxClipTrackBar.Size = new System.Drawing.Size(187, 56);
			this.c_maxClipTrackBar.SmallChange = 10;
			this.c_maxClipTrackBar.TabIndex = 6;
			this.c_maxClipTrackBar.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.c_maxClipTrackBar, "When spanning across monitors that don\'t make a perfect rectangle, part of the im" +
        "age may be offscreen. This setting limits how much of the image WallSwitch will " +
        "allow to be clipped.");
			this.c_maxClipTrackBar.Value = 15;
			this.c_maxClipTrackBar.Scroll += new System.EventHandler(this.MaxClipTrackBar_Scroll);
			// 
			// c_allowSpanning
			// 
			this.c_allowSpanning.AutoSize = true;
			this.c_allowSpanning.Checked = true;
			this.c_allowSpanning.CheckState = System.Windows.Forms.CheckState.Checked;
			this.c_allowSpanning.Location = new System.Drawing.Point(8, 90);
			this.c_allowSpanning.Margin = new System.Windows.Forms.Padding(4);
			this.c_allowSpanning.Name = "c_allowSpanning";
			this.c_allowSpanning.Size = new System.Drawing.Size(228, 21);
			this.c_allowSpanning.TabIndex = 4;
			this.c_allowSpanning.Text = "Allow spanning across monitors";
			this.toolTip1.SetToolTip(this.c_allowSpanning, "If an image is the correct aspect ratio, span it across multiple monitors.");
			this.c_allowSpanning.UseVisualStyleBackColor = true;
			this.c_allowSpanning.CheckedChanged += new System.EventHandler(this.AllowSpanning_CheckedChanged);
			// 
			// c_themeOrder
			// 
			this.c_themeOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_themeOrder.FormattingEnabled = true;
			this.c_themeOrder.Items.AddRange(new object[] {
            "Sequential",
            "Random"});
			this.c_themeOrder.Location = new System.Drawing.Point(204, 23);
			this.c_themeOrder.Margin = new System.Windows.Forms.Padding(4);
			this.c_themeOrder.Name = "c_themeOrder";
			this.c_themeOrder.Size = new System.Drawing.Size(187, 24);
			this.c_themeOrder.TabIndex = 1;
			this.toolTip1.SetToolTip(this.c_themeOrder, "Display order");
			this.c_themeOrder.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_limitScale
			// 
			this.c_limitScale.AutoSize = true;
			this.c_limitScale.Location = new System.Drawing.Point(8, 121);
			this.c_limitScale.Margin = new System.Windows.Forms.Padding(4);
			this.c_limitScale.Name = "c_limitScale";
			this.c_limitScale.Size = new System.Drawing.Size(165, 21);
			this.c_limitScale.TabIndex = 8;
			this.c_limitScale.Text = "Limit image scaling to";
			this.toolTip1.SetToolTip(this.c_limitScale, "Limit the magnification of images?");
			this.c_limitScale.UseVisualStyleBackColor = true;
			this.c_limitScale.CheckedChanged += new System.EventHandler(this.chkLimitScale_CheckedChanged);
			// 
			// grpFrequency
			// 
			this.grpFrequency.Controls.Add(this.c_activateOnExitCheckBox);
			this.grpFrequency.Controls.Add(this.chkFadeTransition);
			this.grpFrequency.Controls.Add(this.c_activateThemeLabel);
			this.grpFrequency.Controls.Add(this.cmbThemePeriod);
			this.grpFrequency.Controls.Add(this.lblFrequency);
			this.grpFrequency.Controls.Add(this.txtThemeFreq);
			this.grpFrequency.Controls.Add(this.c_activateThemeHotKey);
			this.grpFrequency.Location = new System.Drawing.Point(4, 196);
			this.grpFrequency.Margin = new System.Windows.Forms.Padding(4);
			this.grpFrequency.Name = "grpFrequency";
			this.grpFrequency.Padding = new System.Windows.Forms.Padding(4);
			this.grpFrequency.Size = new System.Drawing.Size(892, 119);
			this.grpFrequency.TabIndex = 1;
			this.grpFrequency.TabStop = false;
			this.grpFrequency.Text = "Change Frequency";
			// 
			// c_activateOnExitCheckBox
			// 
			this.c_activateOnExitCheckBox.AutoSize = true;
			this.c_activateOnExitCheckBox.Location = new System.Drawing.Point(8, 84);
			this.c_activateOnExitCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.c_activateOnExitCheckBox.Name = "c_activateOnExitCheckBox";
			this.c_activateOnExitCheckBox.Size = new System.Drawing.Size(272, 21);
			this.c_activateOnExitCheckBox.TabIndex = 6;
			this.c_activateOnExitCheckBox.Text = "Temporarily activate this theme on exit";
			this.toolTip1.SetToolTip(this.c_activateOnExitCheckBox, "When WallSwitch is closing, this theme will be displayed until the next time Wall" +
        "Switch starts up.");
			this.c_activateOnExitCheckBox.UseVisualStyleBackColor = true;
			this.c_activateOnExitCheckBox.CheckedChanged += new System.EventHandler(this.ActivateOnExitCheckBox_CheckedChanged);
			// 
			// chkFadeTransition
			// 
			this.chkFadeTransition.AutoSize = true;
			this.chkFadeTransition.Location = new System.Drawing.Point(433, 26);
			this.chkFadeTransition.Margin = new System.Windows.Forms.Padding(4);
			this.chkFadeTransition.Name = "chkFadeTransition";
			this.chkFadeTransition.Size = new System.Drawing.Size(177, 21);
			this.chkFadeTransition.TabIndex = 3;
			this.chkFadeTransition.Text = "Cross-Fade Transitions";
			this.toolTip1.SetToolTip(this.chkFadeTransition, "Use smooth cross-fading between wallpapers (Windows 7 or higher)");
			this.chkFadeTransition.UseVisualStyleBackColor = true;
			this.chkFadeTransition.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeLabel
			// 
			this.c_activateThemeLabel.AutoSize = true;
			this.c_activateThemeLabel.Location = new System.Drawing.Point(8, 59);
			this.c_activateThemeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_activateThemeLabel.Name = "c_activateThemeLabel";
			this.c_activateThemeLabel.Size = new System.Drawing.Size(62, 17);
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
			this.cmbThemePeriod.Location = new System.Drawing.Point(292, 23);
			this.cmbThemePeriod.Margin = new System.Windows.Forms.Padding(4);
			this.cmbThemePeriod.Name = "cmbThemePeriod";
			this.cmbThemePeriod.Size = new System.Drawing.Size(99, 24);
			this.cmbThemePeriod.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmbThemePeriod, "Wallpaper change interval");
			this.cmbThemePeriod.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblFrequency
			// 
			this.lblFrequency.AutoSize = true;
			this.lblFrequency.Location = new System.Drawing.Point(8, 27);
			this.lblFrequency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFrequency.Name = "lblFrequency";
			this.lblFrequency.Size = new System.Drawing.Size(138, 17);
			this.lblFrequency.TabIndex = 0;
			this.lblFrequency.Text = "Change image every";
			// 
			// txtThemeFreq
			// 
			this.txtThemeFreq.Location = new System.Drawing.Point(204, 23);
			this.txtThemeFreq.Margin = new System.Windows.Forms.Padding(4);
			this.txtThemeFreq.MaxLength = 5;
			this.txtThemeFreq.Name = "txtThemeFreq";
			this.txtThemeFreq.Size = new System.Drawing.Size(79, 22);
			this.txtThemeFreq.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtThemeFreq, "Wallpaper change interval");
			this.txtThemeFreq.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeHotKey
			// 
			this.c_activateThemeHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_activateThemeHotKey.HotKey = null;
			this.c_activateThemeHotKey.Location = new System.Drawing.Point(204, 55);
			this.c_activateThemeHotKey.Margin = new System.Windows.Forms.Padding(4);
			this.c_activateThemeHotKey.Name = "c_activateThemeHotKey";
			this.c_activateThemeHotKey.ReadOnly = true;
			this.c_activateThemeHotKey.Size = new System.Drawing.Size(187, 22);
			this.c_activateThemeHotKey.TabIndex = 5;
			this.toolTip1.SetToolTip(this.c_activateThemeHotKey, "Hot key to change to this theme");
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
			this.grpBackgroundColor.Location = new System.Drawing.Point(4, 323);
			this.grpBackgroundColor.Margin = new System.Windows.Forms.Padding(4);
			this.grpBackgroundColor.Name = "grpBackgroundColor";
			this.grpBackgroundColor.Padding = new System.Windows.Forms.Padding(4);
			this.grpBackgroundColor.Size = new System.Drawing.Size(892, 89);
			this.grpBackgroundColor.TabIndex = 2;
			this.grpBackgroundColor.TabStop = false;
			this.grpBackgroundColor.Text = "Background Color";
			// 
			// clrBackTop
			// 
			this.clrBackTop.BackColor = System.Drawing.Color.Black;
			this.clrBackTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackTop.Color = System.Drawing.Color.Black;
			this.clrBackTop.Location = new System.Drawing.Point(204, 23);
			this.clrBackTop.Margin = new System.Windows.Forms.Padding(5);
			this.clrBackTop.Name = "clrBackTop";
			this.clrBackTop.Size = new System.Drawing.Size(39, 22);
			this.clrBackTop.TabIndex = 1;
			this.toolTip1.SetToolTip(this.clrBackTop, "Top color of background gradient");
			this.clrBackTop.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.BackTop_ColorChanged);
			// 
			// clrBackBottom
			// 
			this.clrBackBottom.BackColor = System.Drawing.Color.Black;
			this.clrBackBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackBottom.Color = System.Drawing.Color.Black;
			this.clrBackBottom.Location = new System.Drawing.Point(204, 53);
			this.clrBackBottom.Margin = new System.Windows.Forms.Padding(5);
			this.clrBackBottom.Name = "clrBackBottom";
			this.clrBackBottom.Size = new System.Drawing.Size(39, 22);
			this.clrBackBottom.TabIndex = 3;
			this.toolTip1.SetToolTip(this.clrBackBottom, "Bottom color of background gradient");
			this.clrBackBottom.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.BackBottom_ColorChanged);
			// 
			// grpCollageDisplay
			// 
			this.grpCollageDisplay.Controls.Add(this.c_numCollageImagesLabel);
			this.grpCollageDisplay.Controls.Add(this.c_numCollageImages);
			this.grpCollageDisplay.Controls.Add(this.c_borderColor);
			this.grpCollageDisplay.Controls.Add(this.c_borderColorLabel);
			this.grpCollageDisplay.Controls.Add(this.c_edgeMode);
			this.grpCollageDisplay.Controls.Add(this.trkDropShadowFeatherDist);
			this.grpCollageDisplay.Controls.Add(this.trkDropShadowOpacity);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowOpacity);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowOpacityValue);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowFeatherDist);
			this.grpCollageDisplay.Controls.Add(this.chkDropShadowFeather);
			this.grpCollageDisplay.Controls.Add(this.chkDropShadow);
			this.grpCollageDisplay.Controls.Add(this.lblDropShadowUnit);
			this.grpCollageDisplay.Controls.Add(this.trkDropShadow);
			this.grpCollageDisplay.Controls.Add(this.c_edgeDistLabel);
			this.grpCollageDisplay.Controls.Add(this.c_edgeDist);
			this.grpCollageDisplay.Controls.Add(this.trkImageSize);
			this.grpCollageDisplay.Controls.Add(this.lblImageSize);
			this.grpCollageDisplay.Controls.Add(this.lblImageSizeDisplay);
			this.grpCollageDisplay.Location = new System.Drawing.Point(4, 420);
			this.grpCollageDisplay.Margin = new System.Windows.Forms.Padding(4);
			this.grpCollageDisplay.Name = "grpCollageDisplay";
			this.grpCollageDisplay.Padding = new System.Windows.Forms.Padding(4);
			this.grpCollageDisplay.Size = new System.Drawing.Size(892, 266);
			this.grpCollageDisplay.TabIndex = 3;
			this.grpCollageDisplay.TabStop = false;
			this.grpCollageDisplay.Text = "Collage Display";
			// 
			// c_numCollageImagesLabel
			// 
			this.c_numCollageImagesLabel.AutoSize = true;
			this.c_numCollageImagesLabel.Location = new System.Drawing.Point(497, 23);
			this.c_numCollageImagesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_numCollageImagesLabel.Name = "c_numCollageImagesLabel";
			this.c_numCollageImagesLabel.Size = new System.Drawing.Size(130, 17);
			this.c_numCollageImagesLabel.TabIndex = 4;
			this.c_numCollageImagesLabel.Text = "image(s) per switch";
			this.toolTip1.SetToolTip(this.c_numCollageImagesLabel, "Number of images to draw each time the wallpaper changes");
			// 
			// c_numCollageImages
			// 
			this.c_numCollageImages.Location = new System.Drawing.Point(436, 20);
			this.c_numCollageImages.Margin = new System.Windows.Forms.Padding(4);
			this.c_numCollageImages.Name = "c_numCollageImages";
			this.c_numCollageImages.Size = new System.Drawing.Size(52, 22);
			this.c_numCollageImages.TabIndex = 3;
			this.toolTip1.SetToolTip(this.c_numCollageImages, "Number of images to draw each time the wallpaper changes");
			this.c_numCollageImages.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_borderColor
			// 
			this.c_borderColor.BackColor = System.Drawing.Color.Transparent;
			this.c_borderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.c_borderColor.Color = System.Drawing.Color.Transparent;
			this.c_borderColor.Location = new System.Drawing.Point(485, 66);
			this.c_borderColor.Margin = new System.Windows.Forms.Padding(5);
			this.c_borderColor.Name = "c_borderColor";
			this.c_borderColor.Size = new System.Drawing.Size(39, 22);
			this.c_borderColor.TabIndex = 9;
			this.toolTip1.SetToolTip(this.c_borderColor, "Border color");
			this.c_borderColor.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.BorderColor_ColorChanged);
			// 
			// c_borderColorLabel
			// 
			this.c_borderColorLabel.AutoSize = true;
			this.c_borderColorLabel.Location = new System.Drawing.Point(432, 70);
			this.c_borderColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_borderColorLabel.Name = "c_borderColorLabel";
			this.c_borderColorLabel.Size = new System.Drawing.Size(45, 17);
			this.c_borderColorLabel.TabIndex = 8;
			this.c_borderColorLabel.Text = "Color:";
			this.toolTip1.SetToolTip(this.c_borderColorLabel, "Border color");
			// 
			// c_edgeMode
			// 
			this.c_edgeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_edgeMode.FormattingEnabled = true;
			this.c_edgeMode.Location = new System.Drawing.Point(8, 66);
			this.c_edgeMode.Margin = new System.Windows.Forms.Padding(4);
			this.c_edgeMode.Name = "c_edgeMode";
			this.c_edgeMode.Size = new System.Drawing.Size(149, 24);
			this.c_edgeMode.TabIndex = 5;
			this.toolTip1.SetToolTip(this.c_edgeMode, "Decoration on edge of image");
			this.c_edgeMode.SelectedIndexChanged += new System.EventHandler(this.EdgeMode_SelectedIndexChanged);
			// 
			// trkDropShadowFeatherDist
			// 
			this.trkDropShadowFeatherDist.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadowFeatherDist.LargeChange = 20;
			this.trkDropShadowFeatherDist.Location = new System.Drawing.Point(159, 206);
			this.trkDropShadowFeatherDist.Margin = new System.Windows.Forms.Padding(4);
			this.trkDropShadowFeatherDist.Maximum = 100;
			this.trkDropShadowFeatherDist.Name = "trkDropShadowFeatherDist";
			this.trkDropShadowFeatherDist.Size = new System.Drawing.Size(187, 56);
			this.trkDropShadowFeatherDist.SmallChange = 10;
			this.trkDropShadowFeatherDist.TabIndex = 17;
			this.trkDropShadowFeatherDist.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadowFeatherDist, "Width of feathering at the edges of shadows");
			this.trkDropShadowFeatherDist.Value = 50;
			this.trkDropShadowFeatherDist.Scroll += new System.EventHandler(this.DropShadowFeatherDist_Scroll);
			// 
			// trkDropShadowOpacity
			// 
			this.trkDropShadowOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadowOpacity.LargeChange = 20;
			this.trkDropShadowOpacity.Location = new System.Drawing.Point(159, 160);
			this.trkDropShadowOpacity.Margin = new System.Windows.Forms.Padding(4);
			this.trkDropShadowOpacity.Maximum = 100;
			this.trkDropShadowOpacity.Name = "trkDropShadowOpacity";
			this.trkDropShadowOpacity.Size = new System.Drawing.Size(187, 56);
			this.trkDropShadowOpacity.SmallChange = 10;
			this.trkDropShadowOpacity.TabIndex = 14;
			this.trkDropShadowOpacity.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadowOpacity, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			this.trkDropShadowOpacity.Value = 50;
			this.trkDropShadowOpacity.Scroll += new System.EventHandler(this.DropShadowOpacity_Scroll);
			// 
			// lblDropShadowOpacity
			// 
			this.lblDropShadowOpacity.AutoSize = true;
			this.lblDropShadowOpacity.Location = new System.Drawing.Point(8, 162);
			this.lblDropShadowOpacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDropShadowOpacity.Name = "lblDropShadowOpacity";
			this.lblDropShadowOpacity.Size = new System.Drawing.Size(114, 17);
			this.lblDropShadowOpacity.TabIndex = 13;
			this.lblDropShadowOpacity.Text = "Shadow Opacity:";
			this.toolTip1.SetToolTip(this.lblDropShadowOpacity, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			// 
			// lblDropShadowOpacityValue
			// 
			this.lblDropShadowOpacityValue.AutoSize = true;
			this.lblDropShadowOpacityValue.Location = new System.Drawing.Point(353, 162);
			this.lblDropShadowOpacityValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDropShadowOpacityValue.Name = "lblDropShadowOpacityValue";
			this.lblDropShadowOpacityValue.Size = new System.Drawing.Size(20, 17);
			this.lblDropShadowOpacityValue.TabIndex = 15;
			this.lblDropShadowOpacityValue.Text = "%";
			this.toolTip1.SetToolTip(this.lblDropShadowOpacityValue, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			// 
			// lblDropShadowFeatherDist
			// 
			this.lblDropShadowFeatherDist.AutoSize = true;
			this.lblDropShadowFeatherDist.Location = new System.Drawing.Point(353, 207);
			this.lblDropShadowFeatherDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDropShadowFeatherDist.Name = "lblDropShadowFeatherDist";
			this.lblDropShadowFeatherDist.Size = new System.Drawing.Size(43, 17);
			this.lblDropShadowFeatherDist.TabIndex = 18;
			this.lblDropShadowFeatherDist.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblDropShadowFeatherDist, "Width of feathering at the edges of shadows");
			// 
			// chkDropShadowFeather
			// 
			this.chkDropShadowFeather.AutoSize = true;
			this.chkDropShadowFeather.Location = new System.Drawing.Point(8, 206);
			this.chkDropShadowFeather.Margin = new System.Windows.Forms.Padding(4);
			this.chkDropShadowFeather.Name = "chkDropShadowFeather";
			this.chkDropShadowFeather.Size = new System.Drawing.Size(133, 21);
			this.chkDropShadowFeather.TabIndex = 16;
			this.chkDropShadowFeather.Text = "Feather Shadow";
			this.toolTip1.SetToolTip(this.chkDropShadowFeather, "Enable feathering on shadows?");
			this.chkDropShadowFeather.UseVisualStyleBackColor = true;
			this.chkDropShadowFeather.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// chkDropShadow
			// 
			this.chkDropShadow.AutoSize = true;
			this.chkDropShadow.Location = new System.Drawing.Point(8, 114);
			this.chkDropShadow.Margin = new System.Windows.Forms.Padding(4);
			this.chkDropShadow.Name = "chkDropShadow";
			this.chkDropShadow.Size = new System.Drawing.Size(115, 21);
			this.chkDropShadow.TabIndex = 10;
			this.chkDropShadow.Text = "Drop Shadow";
			this.toolTip1.SetToolTip(this.chkDropShadow, "Enable drop shadows?");
			this.chkDropShadow.UseVisualStyleBackColor = true;
			this.chkDropShadow.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblDropShadowUnit
			// 
			this.lblDropShadowUnit.AutoSize = true;
			this.lblDropShadowUnit.Location = new System.Drawing.Point(353, 116);
			this.lblDropShadowUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDropShadowUnit.Name = "lblDropShadowUnit";
			this.lblDropShadowUnit.Size = new System.Drawing.Size(43, 17);
			this.lblDropShadowUnit.TabIndex = 12;
			this.lblDropShadowUnit.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblDropShadowUnit, "Offset of drop shadow");
			// 
			// trkDropShadow
			// 
			this.trkDropShadow.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadow.LargeChange = 20;
			this.trkDropShadow.Location = new System.Drawing.Point(159, 114);
			this.trkDropShadow.Margin = new System.Windows.Forms.Padding(4);
			this.trkDropShadow.Maximum = 100;
			this.trkDropShadow.Name = "trkDropShadow";
			this.trkDropShadow.Size = new System.Drawing.Size(187, 56);
			this.trkDropShadow.SmallChange = 10;
			this.trkDropShadow.TabIndex = 11;
			this.trkDropShadow.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadow, "Offset of drop shadow");
			this.trkDropShadow.Value = 50;
			this.trkDropShadow.Scroll += new System.EventHandler(this.DropShadow_Scroll);
			// 
			// c_edgeDistLabel
			// 
			this.c_edgeDistLabel.AutoSize = true;
			this.c_edgeDistLabel.Location = new System.Drawing.Point(353, 70);
			this.c_edgeDistLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_edgeDistLabel.Name = "c_edgeDistLabel";
			this.c_edgeDistLabel.Size = new System.Drawing.Size(43, 17);
			this.c_edgeDistLabel.TabIndex = 7;
			this.c_edgeDistLabel.Text = "pixels";
			this.toolTip1.SetToolTip(this.c_edgeDistLabel, "Width of feathering at the edges of images");
			// 
			// c_edgeDist
			// 
			this.c_edgeDist.BackColor = System.Drawing.SystemColors.Window;
			this.c_edgeDist.LargeChange = 20;
			this.c_edgeDist.Location = new System.Drawing.Point(159, 69);
			this.c_edgeDist.Margin = new System.Windows.Forms.Padding(4);
			this.c_edgeDist.Maximum = 100;
			this.c_edgeDist.Name = "c_edgeDist";
			this.c_edgeDist.Size = new System.Drawing.Size(187, 56);
			this.c_edgeDist.SmallChange = 10;
			this.c_edgeDist.TabIndex = 6;
			this.c_edgeDist.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.c_edgeDist, "Width of feathering at the edges of images");
			this.c_edgeDist.Value = 50;
			this.c_edgeDist.Scroll += new System.EventHandler(this.Feather_Scroll);
			// 
			// grpImageEffects
			// 
			this.grpImageEffects.Controls.Add(this.label1);
			this.grpImageEffects.Controls.Add(this.cmbColorEffectFore);
			this.grpImageEffects.Location = new System.Drawing.Point(4, 694);
			this.grpImageEffects.Margin = new System.Windows.Forms.Padding(4);
			this.grpImageEffects.Name = "grpImageEffects";
			this.grpImageEffects.Padding = new System.Windows.Forms.Padding(4);
			this.grpImageEffects.Size = new System.Drawing.Size(892, 62);
			this.grpImageEffects.TabIndex = 4;
			this.grpImageEffects.TabStop = false;
			this.grpImageEffects.Text = "Image Effects";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 27);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Color Effect:";
			this.toolTip1.SetToolTip(this.label1, "Color effect to be applied to image being displayed.");
			// 
			// cmbColorEffectFore
			// 
			this.cmbColorEffectFore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbColorEffectFore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorEffectFore.FormattingEnabled = true;
			this.cmbColorEffectFore.Location = new System.Drawing.Point(159, 23);
			this.cmbColorEffectFore.Margin = new System.Windows.Forms.Padding(4);
			this.cmbColorEffectFore.Name = "cmbColorEffectFore";
			this.cmbColorEffectFore.Size = new System.Drawing.Size(383, 24);
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
			this.grpBackgroundColorEffects.Location = new System.Drawing.Point(4, 764);
			this.grpBackgroundColorEffects.Margin = new System.Windows.Forms.Padding(4);
			this.grpBackgroundColorEffects.Name = "grpBackgroundColorEffects";
			this.grpBackgroundColorEffects.Padding = new System.Windows.Forms.Padding(4);
			this.grpBackgroundColorEffects.Size = new System.Drawing.Size(892, 148);
			this.grpBackgroundColorEffects.TabIndex = 5;
			this.grpBackgroundColorEffects.TabStop = false;
			this.grpBackgroundColorEffects.Text = "Background Image Effects";
			// 
			// lblBackgroundBlurDistValue
			// 
			this.lblBackgroundBlurDistValue.AutoSize = true;
			this.lblBackgroundBlurDistValue.Location = new System.Drawing.Point(353, 69);
			this.lblBackgroundBlurDistValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBackgroundBlurDistValue.Name = "lblBackgroundBlurDistValue";
			this.lblBackgroundBlurDistValue.Size = new System.Drawing.Size(43, 17);
			this.lblBackgroundBlurDistValue.TabIndex = 6;
			this.lblBackgroundBlurDistValue.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblBackgroundBlurDistValue, "Amount of blur to apply to background");
			// 
			// trkBackgroundBlurDist
			// 
			this.trkBackgroundBlurDist.BackColor = System.Drawing.SystemColors.Window;
			this.trkBackgroundBlurDist.LargeChange = 20;
			this.trkBackgroundBlurDist.Location = new System.Drawing.Point(159, 64);
			this.trkBackgroundBlurDist.Margin = new System.Windows.Forms.Padding(4);
			this.trkBackgroundBlurDist.Maximum = 20;
			this.trkBackgroundBlurDist.Name = "trkBackgroundBlurDist";
			this.trkBackgroundBlurDist.Size = new System.Drawing.Size(187, 56);
			this.trkBackgroundBlurDist.SmallChange = 10;
			this.trkBackgroundBlurDist.TabIndex = 5;
			this.trkBackgroundBlurDist.TickFrequency = 2;
			this.toolTip1.SetToolTip(this.trkBackgroundBlurDist, "Amount of blur to apply to background");
			this.trkBackgroundBlurDist.Value = 20;
			this.trkBackgroundBlurDist.Scroll += new System.EventHandler(this.BackgroundBlurDist_Scroll);
			// 
			// chkBackgroundBlur
			// 
			this.chkBackgroundBlur.AutoSize = true;
			this.chkBackgroundBlur.Location = new System.Drawing.Point(8, 64);
			this.chkBackgroundBlur.Margin = new System.Windows.Forms.Padding(4);
			this.chkBackgroundBlur.Name = "chkBackgroundBlur";
			this.chkBackgroundBlur.Size = new System.Drawing.Size(55, 21);
			this.chkBackgroundBlur.TabIndex = 4;
			this.chkBackgroundBlur.Text = "Blur";
			this.toolTip1.SetToolTip(this.chkBackgroundBlur, "Blur the background?");
			this.chkBackgroundBlur.UseVisualStyleBackColor = true;
			this.chkBackgroundBlur.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 27);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Color Effect:";
			this.toolTip1.SetToolTip(this.label2, "Color effect to be applied to background images.");
			// 
			// cmbColorEffectBack
			// 
			this.cmbColorEffectBack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorEffectBack.FormattingEnabled = true;
			this.cmbColorEffectBack.Location = new System.Drawing.Point(159, 23);
			this.cmbColorEffectBack.Margin = new System.Windows.Forms.Padding(4);
			this.cmbColorEffectBack.Name = "cmbColorEffectBack";
			this.cmbColorEffectBack.Size = new System.Drawing.Size(184, 24);
			this.cmbColorEffectBack.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cmbColorEffectBack, "Color effect to be applied to background images.");
			this.cmbColorEffectBack.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblColorEffectCollageFadeRatioUnit
			// 
			this.lblColorEffectCollageFadeRatioUnit.AutoSize = true;
			this.lblColorEffectCollageFadeRatioUnit.Location = new System.Drawing.Point(547, 27);
			this.lblColorEffectCollageFadeRatioUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblColorEffectCollageFadeRatioUnit.Name = "lblColorEffectCollageFadeRatioUnit";
			this.lblColorEffectCollageFadeRatioUnit.Size = new System.Drawing.Size(20, 17);
			this.lblColorEffectCollageFadeRatioUnit.TabIndex = 2;
			this.lblColorEffectCollageFadeRatioUnit.Text = "%";
			this.toolTip1.SetToolTip(this.lblColorEffectCollageFadeRatioUnit, "Amount of color effect to apply to background images");
			// 
			// trkColorEffectCollageFadeRatio
			// 
			this.trkColorEffectCollageFadeRatio.BackColor = System.Drawing.SystemColors.Window;
			this.trkColorEffectCollageFadeRatio.LargeChange = 20;
			this.trkColorEffectCollageFadeRatio.Location = new System.Drawing.Point(352, 23);
			this.trkColorEffectCollageFadeRatio.Margin = new System.Windows.Forms.Padding(4);
			this.trkColorEffectCollageFadeRatio.Maximum = 100;
			this.trkColorEffectCollageFadeRatio.Name = "trkColorEffectCollageFadeRatio";
			this.trkColorEffectCollageFadeRatio.Size = new System.Drawing.Size(187, 56);
			this.trkColorEffectCollageFadeRatio.SmallChange = 10;
			this.trkColorEffectCollageFadeRatio.TabIndex = 1;
			this.trkColorEffectCollageFadeRatio.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkColorEffectCollageFadeRatio, "Amount of color effect to apply to background images");
			this.trkColorEffectCollageFadeRatio.Value = 25;
			this.trkColorEffectCollageFadeRatio.Scroll += new System.EventHandler(this.ColorEffectCollageFadeRatioTrackBar_Scroll);
			// 
			// c_filterTab
			// 
			this.c_filterTab.Controls.Add(this.c_filterFlow);
			this.c_filterTab.Controls.Add(this.panel3);
			this.c_filterTab.Location = new System.Drawing.Point(4, 25);
			this.c_filterTab.Name = "c_filterTab";
			this.c_filterTab.Padding = new System.Windows.Forms.Padding(3);
			this.c_filterTab.Size = new System.Drawing.Size(939, 484);
			this.c_filterTab.TabIndex = 5;
			this.c_filterTab.Text = "Filter";
			this.c_filterTab.UseVisualStyleBackColor = true;
			// 
			// c_filterFlow
			// 
			this.c_filterFlow.AutoScroll = true;
			this.c_filterFlow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_filterFlow.Location = new System.Drawing.Point(3, 31);
			this.c_filterFlow.Margin = new System.Windows.Forms.Padding(0);
			this.c_filterFlow.Name = "c_filterFlow";
			this.c_filterFlow.Size = new System.Drawing.Size(933, 450);
			this.c_filterFlow.TabIndex = 0;
			this.toolTip1.SetToolTip(this.c_filterFlow, "Conditions to narrow images selected");
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.c_addFilterButton);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(933, 28);
			this.panel3.TabIndex = 0;
			// 
			// c_addFilterButton
			// 
			this.c_addFilterButton.Location = new System.Drawing.Point(3, 3);
			this.c_addFilterButton.Name = "c_addFilterButton";
			this.c_addFilterButton.Size = new System.Drawing.Size(180, 23);
			this.c_addFilterButton.TabIndex = 0;
			this.c_addFilterButton.Text = "Add Filter Condition";
			this.c_addFilterButton.UseVisualStyleBackColor = true;
			this.c_addFilterButton.Click += new System.EventHandler(this.c_addFilterButton_Click);
			// 
			// c_widgetsTab
			// 
			this.c_widgetsTab.Controls.Add(this.c_widgetPanelSplitter);
			this.c_widgetsTab.Location = new System.Drawing.Point(4, 25);
			this.c_widgetsTab.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetsTab.Name = "c_widgetsTab";
			this.c_widgetsTab.Padding = new System.Windows.Forms.Padding(4);
			this.c_widgetsTab.Size = new System.Drawing.Size(939, 484);
			this.c_widgetsTab.TabIndex = 4;
			this.c_widgetsTab.Text = "Widgets";
			this.c_widgetsTab.UseVisualStyleBackColor = true;
			// 
			// c_widgetPanelSplitter
			// 
			this.c_widgetPanelSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetPanelSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.c_widgetPanelSplitter.Location = new System.Drawing.Point(4, 4);
			this.c_widgetPanelSplitter.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetPanelSplitter.Name = "c_widgetPanelSplitter";
			// 
			// c_widgetPanelSplitter.Panel1
			// 
			this.c_widgetPanelSplitter.Panel1.Controls.Add(this.c_widgetLayout);
			this.c_widgetPanelSplitter.Panel1.Controls.Add(this.c_widgetTopPanel);
			// 
			// c_widgetPanelSplitter.Panel2
			// 
			this.c_widgetPanelSplitter.Panel2.Controls.Add(this.c_widgetPanelPropSplitter);
			this.c_widgetPanelSplitter.Size = new System.Drawing.Size(931, 476);
			this.c_widgetPanelSplitter.SplitterDistance = 695;
			this.c_widgetPanelSplitter.SplitterWidth = 5;
			this.c_widgetPanelSplitter.TabIndex = 2;
			// 
			// c_widgetLayout
			// 
			this.c_widgetLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetLayout.Location = new System.Drawing.Point(0, 41);
			this.c_widgetLayout.Margin = new System.Windows.Forms.Padding(5);
			this.c_widgetLayout.Name = "c_widgetLayout";
			this.c_widgetLayout.Size = new System.Drawing.Size(695, 435);
			this.c_widgetLayout.TabIndex = 0;
			this.c_widgetLayout.WidgetsChanged += new System.EventHandler(this.c_widgetLayout_WidgetsChanged);
			this.c_widgetLayout.SelectedWidgetChanged += new System.EventHandler<WallSwitch.WidgetLayoutControl.WidgetEventArgs>(this.c_widgetLayout_SelectedWidgetChanged);
			this.c_widgetLayout.WidgetAdded += new System.EventHandler<WallSwitch.WidgetLayoutControl.WidgetEventArgs>(this.c_widgetLayout_WidgetAdded);
			this.c_widgetLayout.WidgetDeleted += new System.EventHandler<WallSwitch.WidgetLayoutControl.WidgetEventArgs>(this.c_widgetLayout_WidgetDeleted);
			this.c_widgetLayout.WidgetOrderChanged += new System.EventHandler(this.c_widgetLayout_WidgetOrderChanged);
			// 
			// c_widgetTopPanel
			// 
			this.c_widgetTopPanel.Controls.Add(this.c_addWidgetButton);
			this.c_widgetTopPanel.Controls.Add(this.c_widgetTypes);
			this.c_widgetTopPanel.Controls.Add(this.c_widgetTypesLabel);
			this.c_widgetTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_widgetTopPanel.Location = new System.Drawing.Point(0, 0);
			this.c_widgetTopPanel.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetTopPanel.Name = "c_widgetTopPanel";
			this.c_widgetTopPanel.Size = new System.Drawing.Size(695, 41);
			this.c_widgetTopPanel.TabIndex = 1;
			// 
			// c_addWidgetButton
			// 
			this.c_addWidgetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_addWidgetButton.Location = new System.Drawing.Point(611, 5);
			this.c_addWidgetButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_addWidgetButton.Name = "c_addWidgetButton";
			this.c_addWidgetButton.Size = new System.Drawing.Size(80, 28);
			this.c_addWidgetButton.TabIndex = 2;
			this.c_addWidgetButton.Text = "Add";
			this.c_addWidgetButton.UseVisualStyleBackColor = true;
			this.c_addWidgetButton.Click += new System.EventHandler(this.c_addWidgetButton_Click);
			// 
			// c_widgetTypes
			// 
			this.c_widgetTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.c_widgetTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_widgetTypes.FormattingEnabled = true;
			this.c_widgetTypes.Location = new System.Drawing.Point(71, 6);
			this.c_widgetTypes.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetTypes.Name = "c_widgetTypes";
			this.c_widgetTypes.Size = new System.Drawing.Size(531, 24);
			this.c_widgetTypes.TabIndex = 1;
			this.c_widgetTypes.SelectedIndexChanged += new System.EventHandler(this.c_widgetTypes_SelectedIndexChanged);
			// 
			// c_widgetTypesLabel
			// 
			this.c_widgetTypesLabel.AutoSize = true;
			this.c_widgetTypesLabel.Location = new System.Drawing.Point(4, 10);
			this.c_widgetTypesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_widgetTypesLabel.Name = "c_widgetTypesLabel";
			this.c_widgetTypesLabel.Size = new System.Drawing.Size(56, 17);
			this.c_widgetTypesLabel.TabIndex = 0;
			this.c_widgetTypesLabel.Text = "Widget:";
			// 
			// c_widgetPanelPropSplitter
			// 
			this.c_widgetPanelPropSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetPanelPropSplitter.Location = new System.Drawing.Point(0, 0);
			this.c_widgetPanelPropSplitter.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetPanelPropSplitter.Name = "c_widgetPanelPropSplitter";
			this.c_widgetPanelPropSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// c_widgetPanelPropSplitter.Panel1
			// 
			this.c_widgetPanelPropSplitter.Panel1.Controls.Add(this.c_widgetList);
			this.c_widgetPanelPropSplitter.Panel1.Controls.Add(this.c_widgetControlRightPanel);
			// 
			// c_widgetPanelPropSplitter.Panel2
			// 
			this.c_widgetPanelPropSplitter.Panel2.Controls.Add(this.c_widgetPropertyGrid);
			this.c_widgetPanelPropSplitter.Size = new System.Drawing.Size(231, 476);
			this.c_widgetPanelPropSplitter.SplitterDistance = 160;
			this.c_widgetPanelPropSplitter.SplitterWidth = 5;
			this.c_widgetPanelPropSplitter.TabIndex = 0;
			// 
			// c_widgetList
			// 
			this.c_widgetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c_widgetColumn});
			this.c_widgetList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetList.HideSelection = false;
			this.c_widgetList.Location = new System.Drawing.Point(0, 0);
			this.c_widgetList.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetList.MultiSelect = false;
			this.c_widgetList.Name = "c_widgetList";
			this.c_widgetList.Size = new System.Drawing.Size(198, 160);
			this.c_widgetList.TabIndex = 0;
			this.c_widgetList.UseCompatibleStateImageBehavior = false;
			this.c_widgetList.View = System.Windows.Forms.View.Details;
			this.c_widgetList.SelectedIndexChanged += new System.EventHandler(this.c_widgetList_SelectedIndexChanged);
			// 
			// c_widgetColumn
			// 
			this.c_widgetColumn.Text = "Widgets";
			this.c_widgetColumn.Width = 140;
			// 
			// c_widgetControlRightPanel
			// 
			this.c_widgetControlRightPanel.Controls.Add(this.panel2);
			this.c_widgetControlRightPanel.Controls.Add(this.c_widgetMoveUpButton);
			this.c_widgetControlRightPanel.Controls.Add(this.c_widgetMoveDownButton);
			this.c_widgetControlRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.c_widgetControlRightPanel.Location = new System.Drawing.Point(198, 0);
			this.c_widgetControlRightPanel.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetControlRightPanel.Name = "c_widgetControlRightPanel";
			this.c_widgetControlRightPanel.Size = new System.Drawing.Size(33, 160);
			this.c_widgetControlRightPanel.TabIndex = 4;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.c_widgetDeleteButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 132);
			this.panel2.Margin = new System.Windows.Forms.Padding(4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(33, 28);
			this.panel2.TabIndex = 4;
			// 
			// c_widgetDeleteButton
			// 
			this.c_widgetDeleteButton.Image = global::WallSwitch.Res.Delete;
			this.c_widgetDeleteButton.Location = new System.Drawing.Point(3, 0);
			this.c_widgetDeleteButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetDeleteButton.Name = "c_widgetDeleteButton";
			this.c_widgetDeleteButton.Size = new System.Drawing.Size(31, 28);
			this.c_widgetDeleteButton.TabIndex = 3;
			this.c_widgetDeleteButton.UseVisualStyleBackColor = true;
			this.c_widgetDeleteButton.Click += new System.EventHandler(this.c_widgetDeleteButton_Click);
			// 
			// c_widgetMoveUpButton
			// 
			this.c_widgetMoveUpButton.Image = global::WallSwitch.Res.MoveUp;
			this.c_widgetMoveUpButton.Location = new System.Drawing.Point(3, 0);
			this.c_widgetMoveUpButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetMoveUpButton.Name = "c_widgetMoveUpButton";
			this.c_widgetMoveUpButton.Size = new System.Drawing.Size(31, 28);
			this.c_widgetMoveUpButton.TabIndex = 1;
			this.c_widgetMoveUpButton.UseVisualStyleBackColor = true;
			this.c_widgetMoveUpButton.Click += new System.EventHandler(this.c_widgetMoveUpButton_Click);
			// 
			// c_widgetMoveDownButton
			// 
			this.c_widgetMoveDownButton.Image = global::WallSwitch.Res.MoveDown;
			this.c_widgetMoveDownButton.Location = new System.Drawing.Point(3, 36);
			this.c_widgetMoveDownButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetMoveDownButton.Name = "c_widgetMoveDownButton";
			this.c_widgetMoveDownButton.Size = new System.Drawing.Size(31, 28);
			this.c_widgetMoveDownButton.TabIndex = 2;
			this.c_widgetMoveDownButton.UseVisualStyleBackColor = true;
			this.c_widgetMoveDownButton.Click += new System.EventHandler(this.c_widgetMoveDownButton_Click);
			// 
			// c_widgetPropertyGrid
			// 
			this.c_widgetPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.c_widgetPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetPropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.c_widgetPropertyGrid.Margin = new System.Windows.Forms.Padding(4);
			this.c_widgetPropertyGrid.Name = "c_widgetPropertyGrid";
			this.c_widgetPropertyGrid.Size = new System.Drawing.Size(231, 311);
			this.c_widgetPropertyGrid.TabIndex = 0;
			this.c_widgetPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.c_widgetPropertyGrid_PropertyValueChanged);
			// 
			// tabHistory
			// 
			this.tabHistory.Controls.Add(this.c_historyTab);
			this.tabHistory.Location = new System.Drawing.Point(4, 25);
			this.tabHistory.Margin = new System.Windows.Forms.Padding(4);
			this.tabHistory.Name = "tabHistory";
			this.tabHistory.Padding = new System.Windows.Forms.Padding(4);
			this.tabHistory.Size = new System.Drawing.Size(939, 484);
			this.tabHistory.TabIndex = 3;
			this.tabHistory.Text = "History";
			this.tabHistory.UseVisualStyleBackColor = true;
			// 
			// c_historyTab
			// 
			this.c_historyTab.ContextMenuStrip = this.cmHistory;
			this.c_historyTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_historyTab.ImageToolTip = this.toolTip1;
			this.c_historyTab.Location = new System.Drawing.Point(4, 4);
			this.c_historyTab.Margin = new System.Windows.Forms.Padding(5);
			this.c_historyTab.Name = "c_historyTab";
			this.c_historyTab.Size = new System.Drawing.Size(931, 476);
			this.c_historyTab.TabIndex = 0;
			this.c_historyTab.SelectionChanged += new System.EventHandler(this.lstHistory_SelectionChanged);
			this.c_historyTab.ItemActivated += new System.EventHandler<WallSwitch.HistoryList.ItemActivatedEventArgs>(this.lstHistory_ItemActivated);
			this.c_historyTab.DeleteItemRequested += new System.EventHandler<WallSwitch.HistoryList.DeleteItemRequestedEventArgs>(this.HistoryTab_DeleteItemRequested);
			// 
			// cmHistory
			// 
			this.cmHistory.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciOpenHistoryFile,
            this.ciExploreHistoryFile,
            this.ciDeleteHistoryFile,
            this.toolStripMenuItem4,
            this.ciClearHistoryList});
			this.cmHistory.Name = "cmHistory";
			this.cmHistory.Size = new System.Drawing.Size(170, 114);
			// 
			// ciOpenHistoryFile
			// 
			this.ciOpenHistoryFile.Name = "ciOpenHistoryFile";
			this.ciOpenHistoryFile.Size = new System.Drawing.Size(169, 26);
			this.ciOpenHistoryFile.Text = "&Open File";
			this.ciOpenHistoryFile.Click += new System.EventHandler(this.ciOpenHistoryFile_Click);
			// 
			// ciExploreHistoryFile
			// 
			this.ciExploreHistoryFile.Name = "ciExploreHistoryFile";
			this.ciExploreHistoryFile.Size = new System.Drawing.Size(169, 26);
			this.ciExploreHistoryFile.Text = "&Explore File";
			this.ciExploreHistoryFile.Click += new System.EventHandler(this.ciExploreHistoryFile_Click);
			// 
			// ciDeleteHistoryFile
			// 
			this.ciDeleteHistoryFile.Name = "ciDeleteHistoryFile";
			this.ciDeleteHistoryFile.Size = new System.Drawing.Size(169, 26);
			this.ciDeleteHistoryFile.Text = "&Delete File";
			this.ciDeleteHistoryFile.Click += new System.EventHandler(this.ciDeleteHistoryFile_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(166, 6);
			// 
			// ciClearHistoryList
			// 
			this.ciClearHistoryList.Name = "ciClearHistoryList";
			this.ciClearHistoryList.Size = new System.Drawing.Size(169, 26);
			this.ciClearHistoryList.Text = "&Clear History";
			this.ciClearHistoryList.Click += new System.EventHandler(this.ciClearHistoryList_Click);
			// 
			// grpNavButtons
			// 
			this.grpNavButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpNavButtons.Controls.Add(this.btnSwitchNow);
			this.grpNavButtons.Controls.Add(this.btnPause);
			this.grpNavButtons.Controls.Add(this.btnPrevious);
			this.grpNavButtons.Location = new System.Drawing.Point(624, 33);
			this.grpNavButtons.Margin = new System.Windows.Forms.Padding(4);
			this.grpNavButtons.Name = "grpNavButtons";
			this.grpNavButtons.Padding = new System.Windows.Forms.Padding(4);
			this.grpNavButtons.Size = new System.Drawing.Size(164, 62);
			this.grpNavButtons.TabIndex = 11;
			this.grpNavButtons.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Location = new System.Drawing.Point(796, 33);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(167, 62);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Transparency";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.c_transparencyTrackBar);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(4, 19);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(159, 39);
			this.panel1.TabIndex = 1;
			// 
			// c_transparencyTrackBar
			// 
			this.c_transparencyTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_transparencyTrackBar.Location = new System.Drawing.Point(0, 0);
			this.c_transparencyTrackBar.Margin = new System.Windows.Forms.Padding(4);
			this.c_transparencyTrackBar.Minimum = 1;
			this.c_transparencyTrackBar.Name = "c_transparencyTrackBar";
			this.c_transparencyTrackBar.Size = new System.Drawing.Size(159, 39);
			this.c_transparencyTrackBar.TabIndex = 0;
			this.c_transparencyTrackBar.Value = 1;
			this.c_transparencyTrackBar.Scroll += new System.EventHandler(this.TransparencyTrackBar_Scroll);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(231, 6);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(979, 630);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.c_themeTabControl);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.grpTheme);
			this.Controls.Add(this.grpNavButtons);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(794, 568);
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
			this.c_themeTabControl.ResumeLayout(false);
			this.c_locationsTab.ResumeLayout(false);
			this.c_settingsTab.ResumeLayout(false);
			this.flowDisplay.ResumeLayout(false);
			this.grpDisplayMode.ResumeLayout(false);
			this.grpDisplayMode.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_maxClipTrackBar)).EndInit();
			this.grpFrequency.ResumeLayout(false);
			this.grpFrequency.PerformLayout();
			this.grpBackgroundColor.ResumeLayout(false);
			this.grpBackgroundColor.PerformLayout();
			this.grpCollageDisplay.ResumeLayout(false);
			this.grpCollageDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowFeatherDist)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c_edgeDist)).EndInit();
			this.grpImageEffects.ResumeLayout(false);
			this.grpImageEffects.PerformLayout();
			this.grpBackgroundColorEffects.ResumeLayout(false);
			this.grpBackgroundColorEffects.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBackgroundBlurDist)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkColorEffectCollageFadeRatio)).EndInit();
			this.c_filterTab.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.c_widgetsTab.ResumeLayout(false);
			this.c_widgetPanelSplitter.Panel1.ResumeLayout(false);
			this.c_widgetPanelSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelSplitter)).EndInit();
			this.c_widgetPanelSplitter.ResumeLayout(false);
			this.c_widgetTopPanel.ResumeLayout(false);
			this.c_widgetTopPanel.PerformLayout();
			this.c_widgetPanelPropSplitter.Panel1.ResumeLayout(false);
			this.c_widgetPanelPropSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelPropSplitter)).EndInit();
			this.c_widgetPanelPropSplitter.ResumeLayout(false);
			this.c_widgetControlRightPanel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tabHistory.ResumeLayout(false);
			this.cmHistory.ResumeLayout(false);
			this.grpNavButtons.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_transparencyTrackBar)).EndInit();
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
		private System.Windows.Forms.ComboBox c_themeMode;
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
		private System.Windows.Forms.CheckBox c_separateMonitors;
		private System.Windows.Forms.TrackBar trkOpacity;
		private System.Windows.Forms.Label lblOpacity;
		private System.Windows.Forms.Label lblOpacityDisplay;
		private System.Windows.Forms.ComboBox c_imageFit;
		private System.Windows.Forms.ToolStripMenuItem miFileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem miClearHistory;
		private System.Windows.Forms.TabControl c_themeTabControl;
		private System.Windows.Forms.TabPage c_locationsTab;
		private System.Windows.Forms.TabPage c_settingsTab;
		private System.Windows.Forms.GroupBox grpCollageDisplay;
		private System.Windows.Forms.TrackBar c_edgeDist;
		private System.Windows.Forms.Label c_edgeDistLabel;
		private System.Windows.Forms.Button btnAddFeed;
		private System.Windows.Forms.TabPage tabHistory;
		private HistoryList c_historyTab;
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
		private System.Windows.Forms.Label c_maxScalePct;
		private System.Windows.Forms.TextBox c_maxScale;
		private System.Windows.Forms.CheckBox c_limitScale;
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
		private System.Windows.Forms.ComboBox c_edgeMode;
		private ColorSample c_borderColor;
		private System.Windows.Forms.Label c_borderColorLabel;
		private System.Windows.Forms.ComboBox c_themeOrder;
		private System.Windows.Forms.Label c_numCollageImagesLabel;
		private System.Windows.Forms.TextBox c_numCollageImages;
		private System.Windows.Forms.TabPage c_widgetsTab;
		private WidgetLayoutControl c_widgetLayout;
		private System.Windows.Forms.Panel c_widgetTopPanel;
		private System.Windows.Forms.Button c_addWidgetButton;
		private System.Windows.Forms.ComboBox c_widgetTypes;
		private System.Windows.Forms.Label c_widgetTypesLabel;
		private System.Windows.Forms.SplitContainer c_widgetPanelSplitter;
		private System.Windows.Forms.SplitContainer c_widgetPanelPropSplitter;
		private System.Windows.Forms.PropertyGrid c_widgetPropertyGrid;
		private System.Windows.Forms.Button c_widgetDeleteButton;
		private System.Windows.Forms.Button c_widgetMoveDownButton;
		private System.Windows.Forms.Button c_widgetMoveUpButton;
		private System.Windows.Forms.ListView c_widgetList;
		private System.Windows.Forms.ColumnHeader c_widgetColumn;
		private System.Windows.Forms.CheckBox c_allowSpanning;
		private System.Windows.Forms.Label c_maxClipLabel;
		private System.Windows.Forms.Label c_maxClipPercent;
		private System.Windows.Forms.TrackBar c_maxClipTrackBar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TrackBar c_transparencyTrackBar;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox c_activateOnExitCheckBox;
		private System.Windows.Forms.Panel c_widgetControlRightPanel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label c_randomGroupCountLabel;
		private System.Windows.Forms.TextBox c_randomGroupCount;
		private System.Windows.Forms.CheckBox c_randomGroup;
		private System.Windows.Forms.CheckBox c_clearBetweenRandomGroups;
		private System.Windows.Forms.ColumnHeader colFrequency;
		private System.Windows.Forms.ToolStripMenuItem c_browseLocationMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.TabPage c_filterTab;
		private System.Windows.Forms.FlowLayoutPanel c_filterFlow;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button c_addFilterButton;
		private System.Windows.Forms.ImageList c_locationImages;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
	}
}

