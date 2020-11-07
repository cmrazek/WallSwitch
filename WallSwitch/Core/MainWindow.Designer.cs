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
			this.c_browseLocationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.ciAddFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.miAddRssFeed = new System.Windows.Forms.ToolStripMenuItem();
			this.ciAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.ciLocationExplore = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.ciUpdateLocationNow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.ciDeleteLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.ciLocationProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.c_locationImages = new System.Windows.Forms.ImageList(this.components);
			this.cmbTheme = new System.Windows.Forms.ComboBox();
			this.grpTheme = new System.Windows.Forms.GroupBox();
			this.tblTheme = new System.Windows.Forms.TableLayoutPanel();
			this.btnActivate = new System.Windows.Forms.Button();
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
			this.btnSwitchNow = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnPrevious = new System.Windows.Forms.Button();
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
			this.tcTheme = new System.Windows.Forms.TabControl();
			this.c_locationsTab = new System.Windows.Forms.TabPage();
			this.c_settingsTab = new System.Windows.Forms.TabPage();
			this.tblFlowContent = new System.Windows.Forms.TableLayoutPanel();
			this.grpBackgroundColorEffects = new System.Windows.Forms.GroupBox();
			this.flowBackgroundImageEffects = new System.Windows.Forms.FlowLayoutPanel();
			this.flowColorEffect = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbColorEffectBack = new System.Windows.Forms.ComboBox();
			this.trkColorEffectCollageFadeRatio = new System.Windows.Forms.TrackBar();
			this.lblColorEffectCollageFadeRatioUnit = new System.Windows.Forms.Label();
			this.flowBackgroundBlur = new System.Windows.Forms.FlowLayoutPanel();
			this.chkBackgroundBlur = new System.Windows.Forms.CheckBox();
			this.trkBackgroundBlurDist = new System.Windows.Forms.TrackBar();
			this.lblBackgroundBlurDistValue = new System.Windows.Forms.Label();
			this.grpImageEffects = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbColorEffectFore = new System.Windows.Forms.ComboBox();
			this.grpCollageDisplay = new System.Windows.Forms.GroupBox();
			this.tblCollageDisplay = new System.Windows.Forms.TableLayoutPanel();
			this.lblImageSize = new System.Windows.Forms.Label();
			this.trkImageSize = new System.Windows.Forms.TrackBar();
			this.lblImageSizeDisplay = new System.Windows.Forms.Label();
			this.c_edgeMode = new System.Windows.Forms.ComboBox();
			this.c_edgeDist = new System.Windows.Forms.TrackBar();
			this.lblDropShadowFeatherDist = new System.Windows.Forms.Label();
			this.trkDropShadowFeatherDist = new System.Windows.Forms.TrackBar();
			this.c_edgeDistLabel = new System.Windows.Forms.Label();
			this.lblDropShadowOpacityValue = new System.Windows.Forms.Label();
			this.chkDropShadowFeather = new System.Windows.Forms.CheckBox();
			this.trkDropShadowOpacity = new System.Windows.Forms.TrackBar();
			this.chkDropShadow = new System.Windows.Forms.CheckBox();
			this.lblDropShadowOpacity = new System.Windows.Forms.Label();
			this.trkDropShadow = new System.Windows.Forms.TrackBar();
			this.lblDropShadowUnit = new System.Windows.Forms.Label();
			this.flowImagesPerSwitch = new System.Windows.Forms.FlowLayoutPanel();
			this.c_numCollageImages = new System.Windows.Forms.TextBox();
			this.c_numCollageImagesLabel = new System.Windows.Forms.Label();
			this.flowBorderColor = new System.Windows.Forms.FlowLayoutPanel();
			this.c_borderColorLabel = new System.Windows.Forms.Label();
			this.c_borderColor = new WallSwitch.ColorSample();
			this.grpBackgroundColor = new System.Windows.Forms.GroupBox();
			this.tblBackgroundColor = new System.Windows.Forms.TableLayoutPanel();
			this.tblBackgroundColorGradients = new System.Windows.Forms.TableLayoutPanel();
			this.lblBackTop = new System.Windows.Forms.Label();
			this.clrBackBottom = new WallSwitch.ColorSample();
			this.clrBackTop = new WallSwitch.ColorSample();
			this.lblBackBottom = new System.Windows.Forms.Label();
			this.flowBackgroundOpacity = new System.Windows.Forms.FlowLayoutPanel();
			this.lblOpacity = new System.Windows.Forms.Label();
			this.trkOpacity = new System.Windows.Forms.TrackBar();
			this.lblOpacityDisplay = new System.Windows.Forms.Label();
			this.grpFrequency = new System.Windows.Forms.GroupBox();
			this.tblChangeFrequency = new System.Windows.Forms.TableLayoutPanel();
			this.c_activateOnExitCheckBox = new System.Windows.Forms.CheckBox();
			this.tblChangeFrequencyGrid = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.txtThemeFreq = new System.Windows.Forms.TextBox();
			this.cmbThemePeriod = new System.Windows.Forms.ComboBox();
			this.c_activateThemeHotKey = new WallSwitch.HotKeyTextBox();
			this.c_activateThemeLabel = new System.Windows.Forms.Label();
			this.chkFadeTransition = new System.Windows.Forms.CheckBox();
			this.lblFrequency = new System.Windows.Forms.Label();
			this.grpDisplayMode = new System.Windows.Forms.GroupBox();
			this.tblDisplayModeRows = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.c_themeMode = new System.Windows.Forms.ComboBox();
			this.c_themeOrder = new System.Windows.Forms.ComboBox();
			this.c_imageFit = new System.Windows.Forms.ComboBox();
			this.c_separateMonitors = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.c_maxClipTrackBar = new System.Windows.Forms.TrackBar();
			this.c_allowSpanning = new System.Windows.Forms.CheckBox();
			this.c_maxClipLabel = new System.Windows.Forms.Label();
			this.c_maxClipPercent = new System.Windows.Forms.Label();
			this.flowDisplayModeScalingLimit = new System.Windows.Forms.FlowLayoutPanel();
			this.c_limitScale = new System.Windows.Forms.CheckBox();
			this.c_maxScale = new System.Windows.Forms.TextBox();
			this.c_maxScalePct = new System.Windows.Forms.Label();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.c_randomGroup = new System.Windows.Forms.CheckBox();
			this.c_randomGroupCount = new System.Windows.Forms.TextBox();
			this.c_randomGroupCountLabel = new System.Windows.Forms.Label();
			this.c_clearBetweenRandomGroups = new System.Windows.Forms.CheckBox();
			this.c_filterTab = new System.Windows.Forms.TabPage();
			this.c_filterFlow = new System.Windows.Forms.FlowLayoutPanel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.c_addFilterButton = new System.Windows.Forms.Button();
			this.c_widgetsTab = new System.Windows.Forms.TabPage();
			this.c_widgetPanelSplitter = new System.Windows.Forms.SplitContainer();
			this.c_widgetLayout = new WallSwitch.WidgetLayoutControl();
			this.c_widgetTopPanel = new System.Windows.Forms.Panel();
			this.tblWidgetSelector = new System.Windows.Forms.TableLayoutPanel();
			this.c_widgetTypesLabel = new System.Windows.Forms.Label();
			this.c_widgetTypes = new System.Windows.Forms.ComboBox();
			this.c_addWidgetButton = new System.Windows.Forms.Button();
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
			this.tblNavButtons = new System.Windows.Forms.TableLayoutPanel();
			this.grpTransparency = new System.Windows.Forms.GroupBox();
			this.flowTransparency = new System.Windows.Forms.Panel();
			this.c_transparencyTrackBar = new System.Windows.Forms.TrackBar();
			this.tblMain = new System.Windows.Forms.TableLayoutPanel();
			this.tblTopControls = new System.Windows.Forms.TableLayoutPanel();
			this.cmLocations.SuspendLayout();
			this.grpTheme.SuspendLayout();
			this.tblTheme.SuspendLayout();
			this.cmTheme.SuspendLayout();
			this.cmTray.SuspendLayout();
			this.mainMenu.SuspendLayout();
			this.tcTheme.SuspendLayout();
			this.c_locationsTab.SuspendLayout();
			this.c_settingsTab.SuspendLayout();
			this.tblFlowContent.SuspendLayout();
			this.grpBackgroundColorEffects.SuspendLayout();
			this.flowBackgroundImageEffects.SuspendLayout();
			this.flowColorEffect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkColorEffectCollageFadeRatio)).BeginInit();
			this.flowBackgroundBlur.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBackgroundBlurDist)).BeginInit();
			this.grpImageEffects.SuspendLayout();
			this.flowLayoutPanel6.SuspendLayout();
			this.grpCollageDisplay.SuspendLayout();
			this.tblCollageDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c_edgeDist)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowFeatherDist)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadow)).BeginInit();
			this.flowImagesPerSwitch.SuspendLayout();
			this.flowBorderColor.SuspendLayout();
			this.grpBackgroundColor.SuspendLayout();
			this.tblBackgroundColor.SuspendLayout();
			this.tblBackgroundColorGradients.SuspendLayout();
			this.flowBackgroundOpacity.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).BeginInit();
			this.grpFrequency.SuspendLayout();
			this.tblChangeFrequency.SuspendLayout();
			this.tblChangeFrequencyGrid.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.grpDisplayMode.SuspendLayout();
			this.tblDisplayModeRows.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_maxClipTrackBar)).BeginInit();
			this.flowDisplayModeScalingLimit.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.c_filterTab.SuspendLayout();
			this.panel3.SuspendLayout();
			this.c_widgetsTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelSplitter)).BeginInit();
			this.c_widgetPanelSplitter.Panel1.SuspendLayout();
			this.c_widgetPanelSplitter.Panel2.SuspendLayout();
			this.c_widgetPanelSplitter.SuspendLayout();
			this.c_widgetTopPanel.SuspendLayout();
			this.tblWidgetSelector.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelPropSplitter)).BeginInit();
			this.c_widgetPanelPropSplitter.Panel1.SuspendLayout();
			this.c_widgetPanelPropSplitter.Panel2.SuspendLayout();
			this.c_widgetPanelPropSplitter.SuspendLayout();
			this.c_widgetControlRightPanel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabHistory.SuspendLayout();
			this.cmHistory.SuspendLayout();
			this.grpNavButtons.SuspendLayout();
			this.tblNavButtons.SuspendLayout();
			this.grpTransparency.SuspendLayout();
			this.flowTransparency.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_transparencyTrackBar)).BeginInit();
			this.tblMain.SuspendLayout();
			this.tblTopControls.SuspendLayout();
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
			this.lstLocations.HideSelection = false;
			this.lstLocations.Location = new System.Drawing.Point(3, 3);
			this.lstLocations.Name = "lstLocations";
			this.lstLocations.Size = new System.Drawing.Size(770, 398);
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
			this.cmLocations.Size = new System.Drawing.Size(229, 220);
			this.cmLocations.Opening += new System.ComponentModel.CancelEventHandler(this.Locations_Opening);
			// 
			// c_browseLocationMenuItem
			// 
			this.c_browseLocationMenuItem.Name = "c_browseLocationMenuItem";
			this.c_browseLocationMenuItem.Size = new System.Drawing.Size(228, 24);
			this.c_browseLocationMenuItem.Text = "&Browse";
			this.c_browseLocationMenuItem.Click += new System.EventHandler(this.BrowseLocationMenuItem_Click);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(225, 6);
			// 
			// ciAddFolder
			// 
			this.ciAddFolder.Name = "ciAddFolder";
			this.ciAddFolder.Size = new System.Drawing.Size(228, 24);
			this.ciAddFolder.Text = "Add &Folder";
			this.ciAddFolder.Click += new System.EventHandler(this.ciAddFolder_Click);
			// 
			// miAddRssFeed
			// 
			this.miAddRssFeed.Name = "miAddRssFeed";
			this.miAddRssFeed.Size = new System.Drawing.Size(228, 24);
			this.miAddRssFeed.Text = "&Add Feed";
			this.miAddRssFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
			// 
			// ciAddImage
			// 
			this.ciAddImage.Name = "ciAddImage";
			this.ciAddImage.Size = new System.Drawing.Size(228, 24);
			this.ciAddImage.Text = "Add &Image";
			this.ciAddImage.Click += new System.EventHandler(this.ciAddImage_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(225, 6);
			// 
			// ciLocationExplore
			// 
			this.ciLocationExplore.Name = "ciLocationExplore";
			this.ciLocationExplore.Size = new System.Drawing.Size(228, 24);
			this.ciLocationExplore.Text = "&Explore";
			this.ciLocationExplore.Click += new System.EventHandler(this.ciLocationExplore_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(225, 6);
			// 
			// ciUpdateLocationNow
			// 
			this.ciUpdateLocationNow.Name = "ciUpdateLocationNow";
			this.ciUpdateLocationNow.Size = new System.Drawing.Size(228, 24);
			this.ciUpdateLocationNow.Text = "&Update At Next Switch";
			this.ciUpdateLocationNow.Click += new System.EventHandler(this.ciUpdateLocationNow_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(225, 6);
			// 
			// ciDeleteLocation
			// 
			this.ciDeleteLocation.Name = "ciDeleteLocation";
			this.ciDeleteLocation.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ciDeleteLocation.Size = new System.Drawing.Size(228, 24);
			this.ciDeleteLocation.Text = "&Delete";
			this.ciDeleteLocation.Click += new System.EventHandler(this.ciDeleteLocation_Click);
			// 
			// ciLocationProperties
			// 
			this.ciLocationProperties.Name = "ciLocationProperties";
			this.ciLocationProperties.Size = new System.Drawing.Size(228, 24);
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
			this.cmbTheme.Dock = System.Windows.Forms.DockStyle.Top;
			this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTheme.FormattingEnabled = true;
			this.cmbTheme.Location = new System.Drawing.Point(3, 3);
			this.cmbTheme.Name = "cmbTheme";
			this.cmbTheme.Size = new System.Drawing.Size(310, 25);
			this.cmbTheme.Sorted = true;
			this.cmbTheme.TabIndex = 0;
			this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
			// 
			// grpTheme
			// 
			this.grpTheme.AutoSize = true;
			this.grpTheme.Controls.Add(this.tblTheme);
			this.grpTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpTheme.Location = new System.Drawing.Point(3, 3);
			this.grpTheme.Name = "grpTheme";
			this.grpTheme.Size = new System.Drawing.Size(520, 84);
			this.grpTheme.TabIndex = 0;
			this.grpTheme.TabStop = false;
			this.grpTheme.Text = "Theme";
			// 
			// tblTheme
			// 
			this.tblTheme.AutoSize = true;
			this.tblTheme.ColumnCount = 4;
			this.tblTheme.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblTheme.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblTheme.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblTheme.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblTheme.Controls.Add(this.cmbTheme, 0, 0);
			this.tblTheme.Controls.Add(this.btnActivate, 3, 0);
			this.tblTheme.Controls.Add(this.btnSave, 2, 0);
			this.tblTheme.Controls.Add(this.btnTheme, 1, 0);
			this.tblTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblTheme.Location = new System.Drawing.Point(3, 19);
			this.tblTheme.Name = "tblTheme";
			this.tblTheme.RowCount = 1;
			this.tblTheme.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblTheme.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
			this.tblTheme.Size = new System.Drawing.Size(514, 62);
			this.tblTheme.TabIndex = 4;
			// 
			// btnActivate
			// 
			this.btnActivate.AutoSize = true;
			this.btnActivate.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnActivate.Location = new System.Drawing.Point(436, 3);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(75, 27);
			this.btnActivate.TabIndex = 3;
			this.btnActivate.Text = "&Activate";
			this.toolTip1.SetToolTip(this.btnActivate, "Activate this theme");
			this.btnActivate.UseVisualStyleBackColor = true;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// btnSave
			// 
			this.btnSave.AutoSize = true;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnSave.Location = new System.Drawing.Point(355, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 27);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// btnTheme
			// 
			this.btnTheme.AutoSize = true;
			this.btnTheme.ContextMenuStrip = this.cmTheme;
			this.btnTheme.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnTheme.Location = new System.Drawing.Point(319, 3);
			this.btnTheme.Name = "btnTheme";
			this.btnTheme.Size = new System.Drawing.Size(30, 27);
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
			// btnSwitchNow
			// 
			this.btnSwitchNow.AutoSize = true;
			this.btnSwitchNow.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnSwitchNow.Image = global::WallSwitch.Res.NextIcon;
			this.btnSwitchNow.Location = new System.Drawing.Point(75, 3);
			this.btnSwitchNow.Name = "btnSwitchNow";
			this.btnSwitchNow.Size = new System.Drawing.Size(30, 23);
			this.btnSwitchNow.TabIndex = 2;
			this.toolTip1.SetToolTip(this.btnSwitchNow, "Go to the next image");
			this.btnSwitchNow.UseVisualStyleBackColor = true;
			this.btnSwitchNow.Click += new System.EventHandler(this.btnSwitchNow_Click);
			// 
			// btnPause
			// 
			this.btnPause.AutoSize = true;
			this.btnPause.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnPause.Image = global::WallSwitch.Res.PauseIcon;
			this.btnPause.Location = new System.Drawing.Point(39, 3);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(30, 23);
			this.btnPause.TabIndex = 1;
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.AutoSize = true;
			this.btnPrevious.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnPrevious.Image = global::WallSwitch.Res.PrevIcon;
			this.btnPrevious.Location = new System.Drawing.Point(3, 3);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(30, 23);
			this.btnPrevious.TabIndex = 0;
			this.toolTip1.SetToolTip(this.btnPrevious, "Go back to the previous image");
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
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
			this.cmTray.Size = new System.Drawing.Size(206, 136);
			this.cmTray.Opening += new System.ComponentModel.CancelEventHandler(this.TrayMenu_Opening);
			// 
			// cmShowMainWindow
			// 
			this.cmShowMainWindow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmShowMainWindow.Name = "cmShowMainWindow";
			this.cmShowMainWindow.Size = new System.Drawing.Size(205, 24);
			this.cmShowMainWindow.Text = "&Show";
			this.cmShowMainWindow.Click += new System.EventHandler(this.cmShowMainWindow_Click);
			// 
			// ciTheme
			// 
			this.ciTheme.Name = "ciTheme";
			this.ciTheme.Size = new System.Drawing.Size(205, 24);
			this.ciTheme.Text = "&Theme";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
			// 
			// cmSwitchNow
			// 
			this.cmSwitchNow.Name = "cmSwitchNow";
			this.cmSwitchNow.Size = new System.Drawing.Size(205, 24);
			this.cmSwitchNow.Text = "&Next Wallpaper";
			this.cmSwitchNow.Click += new System.EventHandler(this.ciSwitchNow_Click);
			// 
			// ciSwitchPrev
			// 
			this.ciSwitchPrev.Name = "ciSwitchPrev";
			this.ciSwitchPrev.Size = new System.Drawing.Size(205, 24);
			this.ciSwitchPrev.Text = "&Previous Wallpaper";
			this.ciSwitchPrev.Click += new System.EventHandler(this.ciSwitchPrev_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
			// 
			// cmExit
			// 
			this.cmExit.Name = "cmExit";
			this.cmExit.Size = new System.Drawing.Size(205, 24);
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
			this.mainMenu.Size = new System.Drawing.Size(790, 28);
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
			this.menuFile.Size = new System.Drawing.Size(46, 26);
			this.menuFile.Text = "&File";
			// 
			// miFileNewTheme
			// 
			this.miFileNewTheme.Name = "miFileNewTheme";
			this.miFileNewTheme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.miFileNewTheme.Size = new System.Drawing.Size(246, 26);
			this.miFileNewTheme.Text = "&New Theme";
			this.miFileNewTheme.Click += new System.EventHandler(this.miFileNewTheme_Click);
			// 
			// miFileRenameTheme
			// 
			this.miFileRenameTheme.Name = "miFileRenameTheme";
			this.miFileRenameTheme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.miFileRenameTheme.Size = new System.Drawing.Size(246, 26);
			this.miFileRenameTheme.Text = "&Rename Theme";
			this.miFileRenameTheme.Click += new System.EventHandler(this.miFileRenameTheme_Click);
			// 
			// miDuplicateTheme
			// 
			this.miDuplicateTheme.Name = "miDuplicateTheme";
			this.miDuplicateTheme.Size = new System.Drawing.Size(246, 26);
			this.miDuplicateTheme.Text = "D&uplicate Theme";
			this.miDuplicateTheme.Click += new System.EventHandler(this.miDuplicateTheme_Click);
			// 
			// miFileDeleteTheme
			// 
			this.miFileDeleteTheme.Name = "miFileDeleteTheme";
			this.miFileDeleteTheme.Size = new System.Drawing.Size(246, 26);
			this.miFileDeleteTheme.Text = "&Delete Theme";
			this.miFileDeleteTheme.Click += new System.EventHandler(this.miFileDeleteTheme_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(243, 6);
			// 
			// miFileSave
			// 
			this.miFileSave.Name = "miFileSave";
			this.miFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.miFileSave.Size = new System.Drawing.Size(246, 26);
			this.miFileSave.Text = "&Save";
			this.miFileSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(243, 6);
			// 
			// miFileExit
			// 
			this.miFileExit.Name = "miFileExit";
			this.miFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.miFileExit.Size = new System.Drawing.Size(246, 26);
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
			this.menuTools.Size = new System.Drawing.Size(58, 26);
			this.menuTools.Text = "&Tools";
			// 
			// miClearHistory
			// 
			this.miClearHistory.Name = "miClearHistory";
			this.miClearHistory.Size = new System.Drawing.Size(177, 26);
			this.miClearHistory.Text = "&Clear History";
			this.miClearHistory.Click += new System.EventHandler(this.miClearHistory_Click);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(174, 6);
			// 
			// miHotKeys
			// 
			this.miHotKeys.Name = "miHotKeys";
			this.miHotKeys.Size = new System.Drawing.Size(177, 26);
			this.miHotKeys.Text = "&Hot Keys";
			this.miHotKeys.Click += new System.EventHandler(this.miHotKeys_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
			this.settingsToolStripMenuItem.Text = "&Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelpAbout});
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Size = new System.Drawing.Size(55, 26);
			this.menuHelp.Text = "&Help";
			// 
			// miHelpAbout
			// 
			this.miHelpAbout.Name = "miHelpAbout";
			this.miHelpAbout.Size = new System.Drawing.Size(133, 26);
			this.miHelpAbout.Text = "&About";
			this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
			// 
			// tcTheme
			// 
			this.tcTheme.Controls.Add(this.c_locationsTab);
			this.tcTheme.Controls.Add(this.c_settingsTab);
			this.tcTheme.Controls.Add(this.c_filterTab);
			this.tcTheme.Controls.Add(this.c_widgetsTab);
			this.tcTheme.Controls.Add(this.tabHistory);
			this.tcTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcTheme.Location = new System.Drawing.Point(3, 99);
			this.tcTheme.Name = "tcTheme";
			this.tcTheme.SelectedIndex = 0;
			this.tcTheme.Size = new System.Drawing.Size(784, 434);
			this.tcTheme.TabIndex = 10;
			// 
			// c_locationsTab
			// 
			this.c_locationsTab.Controls.Add(this.lstLocations);
			this.c_locationsTab.Location = new System.Drawing.Point(4, 26);
			this.c_locationsTab.Name = "c_locationsTab";
			this.c_locationsTab.Padding = new System.Windows.Forms.Padding(3);
			this.c_locationsTab.Size = new System.Drawing.Size(776, 404);
			this.c_locationsTab.TabIndex = 0;
			this.c_locationsTab.Text = "Images";
			this.c_locationsTab.UseVisualStyleBackColor = true;
			// 
			// c_settingsTab
			// 
			this.c_settingsTab.Controls.Add(this.tblFlowContent);
			this.c_settingsTab.Location = new System.Drawing.Point(4, 26);
			this.c_settingsTab.Name = "c_settingsTab";
			this.c_settingsTab.Padding = new System.Windows.Forms.Padding(3);
			this.c_settingsTab.Size = new System.Drawing.Size(776, 404);
			this.c_settingsTab.TabIndex = 1;
			this.c_settingsTab.Text = "Settings";
			this.c_settingsTab.UseVisualStyleBackColor = true;
			// 
			// tblFlowContent
			// 
			this.tblFlowContent.AutoScroll = true;
			this.tblFlowContent.AutoSize = true;
			this.tblFlowContent.ColumnCount = 1;
			this.tblFlowContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblFlowContent.Controls.Add(this.grpBackgroundColorEffects, 0, 5);
			this.tblFlowContent.Controls.Add(this.grpImageEffects, 0, 4);
			this.tblFlowContent.Controls.Add(this.grpCollageDisplay, 0, 3);
			this.tblFlowContent.Controls.Add(this.grpBackgroundColor, 0, 2);
			this.tblFlowContent.Controls.Add(this.grpFrequency, 0, 1);
			this.tblFlowContent.Controls.Add(this.grpDisplayMode, 0, 0);
			this.tblFlowContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblFlowContent.Location = new System.Drawing.Point(3, 3);
			this.tblFlowContent.Name = "tblFlowContent";
			this.tblFlowContent.RowCount = 6;
			this.tblFlowContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblFlowContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblFlowContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblFlowContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblFlowContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblFlowContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblFlowContent.Size = new System.Drawing.Size(770, 398);
			this.tblFlowContent.TabIndex = 15;
			// 
			// grpBackgroundColorEffects
			// 
			this.grpBackgroundColorEffects.AutoSize = true;
			this.grpBackgroundColorEffects.Controls.Add(this.flowBackgroundImageEffects);
			this.grpBackgroundColorEffects.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpBackgroundColorEffects.Location = new System.Drawing.Point(3, 851);
			this.grpBackgroundColorEffects.Name = "grpBackgroundColorEffects";
			this.grpBackgroundColorEffects.Size = new System.Drawing.Size(764, 158);
			this.grpBackgroundColorEffects.TabIndex = 6;
			this.grpBackgroundColorEffects.TabStop = false;
			this.grpBackgroundColorEffects.Text = "Background Image Effects";
			// 
			// flowBackgroundImageEffects
			// 
			this.flowBackgroundImageEffects.AutoSize = true;
			this.flowBackgroundImageEffects.Controls.Add(this.flowColorEffect);
			this.flowBackgroundImageEffects.Controls.Add(this.flowBackgroundBlur);
			this.flowBackgroundImageEffects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowBackgroundImageEffects.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowBackgroundImageEffects.Location = new System.Drawing.Point(3, 19);
			this.flowBackgroundImageEffects.Name = "flowBackgroundImageEffects";
			this.flowBackgroundImageEffects.Size = new System.Drawing.Size(758, 136);
			this.flowBackgroundImageEffects.TabIndex = 7;
			// 
			// flowColorEffect
			// 
			this.flowColorEffect.AutoSize = true;
			this.flowColorEffect.Controls.Add(this.label2);
			this.flowColorEffect.Controls.Add(this.cmbColorEffectBack);
			this.flowColorEffect.Controls.Add(this.trkColorEffectCollageFadeRatio);
			this.flowColorEffect.Controls.Add(this.lblColorEffectCollageFadeRatioUnit);
			this.flowColorEffect.Location = new System.Drawing.Point(3, 3);
			this.flowColorEffect.Name = "flowColorEffect";
			this.flowColorEffect.Size = new System.Drawing.Size(408, 62);
			this.flowColorEffect.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Color Effect:";
			this.toolTip1.SetToolTip(this.label2, "Color effect to be applied to background images.");
			// 
			// cmbColorEffectBack
			// 
			this.cmbColorEffectBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmbColorEffectBack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorEffectBack.FormattingEnabled = true;
			this.cmbColorEffectBack.Location = new System.Drawing.Point(94, 19);
			this.cmbColorEffectBack.Name = "cmbColorEffectBack";
			this.cmbColorEffectBack.Size = new System.Drawing.Size(139, 25);
			this.cmbColorEffectBack.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cmbColorEffectBack, "Color effect to be applied to background images.");
			this.cmbColorEffectBack.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// trkColorEffectCollageFadeRatio
			// 
			this.trkColorEffectCollageFadeRatio.BackColor = System.Drawing.SystemColors.Window;
			this.trkColorEffectCollageFadeRatio.LargeChange = 20;
			this.trkColorEffectCollageFadeRatio.Location = new System.Drawing.Point(239, 3);
			this.trkColorEffectCollageFadeRatio.Maximum = 100;
			this.trkColorEffectCollageFadeRatio.Name = "trkColorEffectCollageFadeRatio";
			this.trkColorEffectCollageFadeRatio.Size = new System.Drawing.Size(140, 56);
			this.trkColorEffectCollageFadeRatio.SmallChange = 10;
			this.trkColorEffectCollageFadeRatio.TabIndex = 1;
			this.trkColorEffectCollageFadeRatio.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkColorEffectCollageFadeRatio, "Amount of color effect to apply to background images");
			this.trkColorEffectCollageFadeRatio.Value = 25;
			this.trkColorEffectCollageFadeRatio.Scroll += new System.EventHandler(this.ColorEffectCollageFadeRatioTrackBar_Scroll);
			// 
			// lblColorEffectCollageFadeRatioUnit
			// 
			this.lblColorEffectCollageFadeRatioUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblColorEffectCollageFadeRatioUnit.AutoSize = true;
			this.lblColorEffectCollageFadeRatioUnit.Location = new System.Drawing.Point(385, 22);
			this.lblColorEffectCollageFadeRatioUnit.Name = "lblColorEffectCollageFadeRatioUnit";
			this.lblColorEffectCollageFadeRatioUnit.Size = new System.Drawing.Size(20, 17);
			this.lblColorEffectCollageFadeRatioUnit.TabIndex = 2;
			this.lblColorEffectCollageFadeRatioUnit.Text = "%";
			this.toolTip1.SetToolTip(this.lblColorEffectCollageFadeRatioUnit, "Amount of color effect to apply to background images");
			// 
			// flowBackgroundBlur
			// 
			this.flowBackgroundBlur.AutoSize = true;
			this.flowBackgroundBlur.Controls.Add(this.chkBackgroundBlur);
			this.flowBackgroundBlur.Controls.Add(this.trkBackgroundBlurDist);
			this.flowBackgroundBlur.Controls.Add(this.lblBackgroundBlurDistValue);
			this.flowBackgroundBlur.Location = new System.Drawing.Point(3, 71);
			this.flowBackgroundBlur.Name = "flowBackgroundBlur";
			this.flowBackgroundBlur.Size = new System.Drawing.Size(256, 62);
			this.flowBackgroundBlur.TabIndex = 1;
			// 
			// chkBackgroundBlur
			// 
			this.chkBackgroundBlur.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkBackgroundBlur.AutoSize = true;
			this.chkBackgroundBlur.Location = new System.Drawing.Point(3, 20);
			this.chkBackgroundBlur.Name = "chkBackgroundBlur";
			this.chkBackgroundBlur.Size = new System.Drawing.Size(55, 21);
			this.chkBackgroundBlur.TabIndex = 4;
			this.chkBackgroundBlur.Text = "Blur";
			this.toolTip1.SetToolTip(this.chkBackgroundBlur, "Blur the background?");
			this.chkBackgroundBlur.UseVisualStyleBackColor = true;
			this.chkBackgroundBlur.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// trkBackgroundBlurDist
			// 
			this.trkBackgroundBlurDist.BackColor = System.Drawing.SystemColors.Window;
			this.trkBackgroundBlurDist.LargeChange = 20;
			this.trkBackgroundBlurDist.Location = new System.Drawing.Point(64, 3);
			this.trkBackgroundBlurDist.Maximum = 20;
			this.trkBackgroundBlurDist.Name = "trkBackgroundBlurDist";
			this.trkBackgroundBlurDist.Size = new System.Drawing.Size(140, 56);
			this.trkBackgroundBlurDist.SmallChange = 10;
			this.trkBackgroundBlurDist.TabIndex = 5;
			this.trkBackgroundBlurDist.TickFrequency = 2;
			this.toolTip1.SetToolTip(this.trkBackgroundBlurDist, "Amount of blur to apply to background");
			this.trkBackgroundBlurDist.Value = 20;
			this.trkBackgroundBlurDist.Scroll += new System.EventHandler(this.BackgroundBlurDist_Scroll);
			// 
			// lblBackgroundBlurDistValue
			// 
			this.lblBackgroundBlurDistValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblBackgroundBlurDistValue.AutoSize = true;
			this.lblBackgroundBlurDistValue.Location = new System.Drawing.Point(210, 22);
			this.lblBackgroundBlurDistValue.Name = "lblBackgroundBlurDistValue";
			this.lblBackgroundBlurDistValue.Size = new System.Drawing.Size(43, 17);
			this.lblBackgroundBlurDistValue.TabIndex = 6;
			this.lblBackgroundBlurDistValue.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblBackgroundBlurDistValue, "Amount of blur to apply to background");
			// 
			// grpImageEffects
			// 
			this.grpImageEffects.AutoSize = true;
			this.grpImageEffects.Controls.Add(this.flowLayoutPanel6);
			this.grpImageEffects.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpImageEffects.Location = new System.Drawing.Point(3, 792);
			this.grpImageEffects.Name = "grpImageEffects";
			this.grpImageEffects.Size = new System.Drawing.Size(764, 53);
			this.grpImageEffects.TabIndex = 5;
			this.grpImageEffects.TabStop = false;
			this.grpImageEffects.Text = "Image Effects";
			// 
			// flowLayoutPanel6
			// 
			this.flowLayoutPanel6.AutoSize = true;
			this.flowLayoutPanel6.Controls.Add(this.label1);
			this.flowLayoutPanel6.Controls.Add(this.cmbColorEffectFore);
			this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 19);
			this.flowLayoutPanel6.Name = "flowLayoutPanel6";
			this.flowLayoutPanel6.Size = new System.Drawing.Size(758, 31);
			this.flowLayoutPanel6.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Color Effect:";
			this.toolTip1.SetToolTip(this.label1, "Color effect to be applied to image being displayed.");
			// 
			// cmbColorEffectFore
			// 
			this.cmbColorEffectFore.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmbColorEffectFore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorEffectFore.FormattingEnabled = true;
			this.cmbColorEffectFore.Location = new System.Drawing.Point(94, 3);
			this.cmbColorEffectFore.Name = "cmbColorEffectFore";
			this.cmbColorEffectFore.Size = new System.Drawing.Size(288, 25);
			this.cmbColorEffectFore.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cmbColorEffectFore, "Color effect to be applied to image being displayed.");
			this.cmbColorEffectFore.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// grpCollageDisplay
			// 
			this.grpCollageDisplay.AutoSize = true;
			this.grpCollageDisplay.Controls.Add(this.tblCollageDisplay);
			this.grpCollageDisplay.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpCollageDisplay.Location = new System.Drawing.Point(3, 454);
			this.grpCollageDisplay.Name = "grpCollageDisplay";
			this.grpCollageDisplay.Size = new System.Drawing.Size(764, 332);
			this.grpCollageDisplay.TabIndex = 4;
			this.grpCollageDisplay.TabStop = false;
			this.grpCollageDisplay.Text = "Collage Display";
			// 
			// tblCollageDisplay
			// 
			this.tblCollageDisplay.AutoSize = true;
			this.tblCollageDisplay.ColumnCount = 4;
			this.tblCollageDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblCollageDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblCollageDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblCollageDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblCollageDisplay.Controls.Add(this.lblImageSize, 0, 0);
			this.tblCollageDisplay.Controls.Add(this.trkImageSize, 1, 0);
			this.tblCollageDisplay.Controls.Add(this.lblImageSizeDisplay, 2, 0);
			this.tblCollageDisplay.Controls.Add(this.c_edgeMode, 0, 1);
			this.tblCollageDisplay.Controls.Add(this.c_edgeDist, 1, 1);
			this.tblCollageDisplay.Controls.Add(this.lblDropShadowFeatherDist, 2, 4);
			this.tblCollageDisplay.Controls.Add(this.trkDropShadowFeatherDist, 1, 4);
			this.tblCollageDisplay.Controls.Add(this.c_edgeDistLabel, 2, 1);
			this.tblCollageDisplay.Controls.Add(this.lblDropShadowOpacityValue, 2, 3);
			this.tblCollageDisplay.Controls.Add(this.chkDropShadowFeather, 0, 4);
			this.tblCollageDisplay.Controls.Add(this.trkDropShadowOpacity, 1, 3);
			this.tblCollageDisplay.Controls.Add(this.chkDropShadow, 0, 2);
			this.tblCollageDisplay.Controls.Add(this.lblDropShadowOpacity, 0, 3);
			this.tblCollageDisplay.Controls.Add(this.trkDropShadow, 1, 2);
			this.tblCollageDisplay.Controls.Add(this.lblDropShadowUnit, 2, 2);
			this.tblCollageDisplay.Controls.Add(this.flowImagesPerSwitch, 3, 0);
			this.tblCollageDisplay.Controls.Add(this.flowBorderColor, 3, 1);
			this.tblCollageDisplay.Dock = System.Windows.Forms.DockStyle.Top;
			this.tblCollageDisplay.Location = new System.Drawing.Point(3, 19);
			this.tblCollageDisplay.Name = "tblCollageDisplay";
			this.tblCollageDisplay.RowCount = 5;
			this.tblCollageDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblCollageDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblCollageDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblCollageDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblCollageDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblCollageDisplay.Size = new System.Drawing.Size(758, 310);
			this.tblCollageDisplay.TabIndex = 19;
			// 
			// lblImageSize
			// 
			this.lblImageSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblImageSize.AutoSize = true;
			this.lblImageSize.Location = new System.Drawing.Point(3, 22);
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
			this.trkImageSize.Location = new System.Drawing.Point(159, 3);
			this.trkImageSize.Maximum = 100;
			this.trkImageSize.Minimum = 1;
			this.trkImageSize.Name = "trkImageSize";
			this.trkImageSize.Size = new System.Drawing.Size(140, 56);
			this.trkImageSize.SmallChange = 10;
			this.trkImageSize.TabIndex = 1;
			this.trkImageSize.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkImageSize, "Image size in relation to the screen");
			this.trkImageSize.Value = 50;
			this.trkImageSize.Scroll += new System.EventHandler(this.ImageSize_Scroll);
			// 
			// lblImageSizeDisplay
			// 
			this.lblImageSizeDisplay.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblImageSizeDisplay.AutoSize = true;
			this.lblImageSizeDisplay.Location = new System.Drawing.Point(305, 22);
			this.lblImageSizeDisplay.Name = "lblImageSizeDisplay";
			this.lblImageSizeDisplay.Size = new System.Drawing.Size(20, 17);
			this.lblImageSizeDisplay.TabIndex = 2;
			this.lblImageSizeDisplay.Text = "%";
			this.toolTip1.SetToolTip(this.lblImageSizeDisplay, "Image size in relation to the screen");
			// 
			// c_edgeMode
			// 
			this.c_edgeMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_edgeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_edgeMode.FormattingEnabled = true;
			this.c_edgeMode.Location = new System.Drawing.Point(3, 80);
			this.c_edgeMode.Name = "c_edgeMode";
			this.c_edgeMode.Size = new System.Drawing.Size(150, 25);
			this.c_edgeMode.TabIndex = 5;
			this.toolTip1.SetToolTip(this.c_edgeMode, "Decoration on edge of image");
			this.c_edgeMode.SelectedIndexChanged += new System.EventHandler(this.EdgeMode_SelectedIndexChanged);
			// 
			// c_edgeDist
			// 
			this.c_edgeDist.BackColor = System.Drawing.SystemColors.Window;
			this.c_edgeDist.LargeChange = 20;
			this.c_edgeDist.Location = new System.Drawing.Point(159, 65);
			this.c_edgeDist.Maximum = 100;
			this.c_edgeDist.Name = "c_edgeDist";
			this.c_edgeDist.Size = new System.Drawing.Size(140, 56);
			this.c_edgeDist.SmallChange = 10;
			this.c_edgeDist.TabIndex = 6;
			this.c_edgeDist.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.c_edgeDist, "Width of feathering at the edges of images");
			this.c_edgeDist.Value = 50;
			this.c_edgeDist.Scroll += new System.EventHandler(this.Feather_Scroll);
			// 
			// lblDropShadowFeatherDist
			// 
			this.lblDropShadowFeatherDist.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblDropShadowFeatherDist.AutoSize = true;
			this.lblDropShadowFeatherDist.Location = new System.Drawing.Point(305, 270);
			this.lblDropShadowFeatherDist.Name = "lblDropShadowFeatherDist";
			this.lblDropShadowFeatherDist.Size = new System.Drawing.Size(43, 17);
			this.lblDropShadowFeatherDist.TabIndex = 18;
			this.lblDropShadowFeatherDist.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblDropShadowFeatherDist, "Width of feathering at the edges of shadows");
			// 
			// trkDropShadowFeatherDist
			// 
			this.trkDropShadowFeatherDist.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadowFeatherDist.LargeChange = 20;
			this.trkDropShadowFeatherDist.Location = new System.Drawing.Point(159, 251);
			this.trkDropShadowFeatherDist.Maximum = 100;
			this.trkDropShadowFeatherDist.Name = "trkDropShadowFeatherDist";
			this.trkDropShadowFeatherDist.Size = new System.Drawing.Size(140, 56);
			this.trkDropShadowFeatherDist.SmallChange = 10;
			this.trkDropShadowFeatherDist.TabIndex = 17;
			this.trkDropShadowFeatherDist.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadowFeatherDist, "Width of feathering at the edges of shadows");
			this.trkDropShadowFeatherDist.Value = 50;
			this.trkDropShadowFeatherDist.Scroll += new System.EventHandler(this.DropShadowFeatherDist_Scroll);
			// 
			// c_edgeDistLabel
			// 
			this.c_edgeDistLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_edgeDistLabel.AutoSize = true;
			this.c_edgeDistLabel.Location = new System.Drawing.Point(305, 84);
			this.c_edgeDistLabel.Name = "c_edgeDistLabel";
			this.c_edgeDistLabel.Size = new System.Drawing.Size(43, 17);
			this.c_edgeDistLabel.TabIndex = 7;
			this.c_edgeDistLabel.Text = "pixels";
			this.toolTip1.SetToolTip(this.c_edgeDistLabel, "Width of feathering at the edges of images");
			// 
			// lblDropShadowOpacityValue
			// 
			this.lblDropShadowOpacityValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblDropShadowOpacityValue.AutoSize = true;
			this.lblDropShadowOpacityValue.Location = new System.Drawing.Point(305, 208);
			this.lblDropShadowOpacityValue.Name = "lblDropShadowOpacityValue";
			this.lblDropShadowOpacityValue.Size = new System.Drawing.Size(20, 17);
			this.lblDropShadowOpacityValue.TabIndex = 15;
			this.lblDropShadowOpacityValue.Text = "%";
			this.toolTip1.SetToolTip(this.lblDropShadowOpacityValue, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			// 
			// chkDropShadowFeather
			// 
			this.chkDropShadowFeather.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkDropShadowFeather.AutoSize = true;
			this.chkDropShadowFeather.Location = new System.Drawing.Point(3, 268);
			this.chkDropShadowFeather.Name = "chkDropShadowFeather";
			this.chkDropShadowFeather.Size = new System.Drawing.Size(133, 21);
			this.chkDropShadowFeather.TabIndex = 16;
			this.chkDropShadowFeather.Text = "Feather Shadow";
			this.toolTip1.SetToolTip(this.chkDropShadowFeather, "Enable feathering on shadows?");
			this.chkDropShadowFeather.UseVisualStyleBackColor = true;
			this.chkDropShadowFeather.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// trkDropShadowOpacity
			// 
			this.trkDropShadowOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadowOpacity.LargeChange = 20;
			this.trkDropShadowOpacity.Location = new System.Drawing.Point(159, 189);
			this.trkDropShadowOpacity.Maximum = 100;
			this.trkDropShadowOpacity.Name = "trkDropShadowOpacity";
			this.trkDropShadowOpacity.Size = new System.Drawing.Size(140, 56);
			this.trkDropShadowOpacity.SmallChange = 10;
			this.trkDropShadowOpacity.TabIndex = 14;
			this.trkDropShadowOpacity.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadowOpacity, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			this.trkDropShadowOpacity.Value = 50;
			this.trkDropShadowOpacity.Scroll += new System.EventHandler(this.DropShadowOpacity_Scroll);
			// 
			// chkDropShadow
			// 
			this.chkDropShadow.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkDropShadow.AutoSize = true;
			this.chkDropShadow.Location = new System.Drawing.Point(3, 144);
			this.chkDropShadow.Name = "chkDropShadow";
			this.chkDropShadow.Size = new System.Drawing.Size(115, 21);
			this.chkDropShadow.TabIndex = 10;
			this.chkDropShadow.Text = "Drop Shadow";
			this.toolTip1.SetToolTip(this.chkDropShadow, "Enable drop shadows?");
			this.chkDropShadow.UseVisualStyleBackColor = true;
			this.chkDropShadow.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblDropShadowOpacity
			// 
			this.lblDropShadowOpacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblDropShadowOpacity.AutoSize = true;
			this.lblDropShadowOpacity.Location = new System.Drawing.Point(3, 208);
			this.lblDropShadowOpacity.Name = "lblDropShadowOpacity";
			this.lblDropShadowOpacity.Size = new System.Drawing.Size(114, 17);
			this.lblDropShadowOpacity.TabIndex = 13;
			this.lblDropShadowOpacity.Text = "Shadow Opacity:";
			this.toolTip1.SetToolTip(this.lblDropShadowOpacity, "Opacity of the shadow (0% = solid black, 100% = completely transparent)");
			// 
			// trkDropShadow
			// 
			this.trkDropShadow.BackColor = System.Drawing.SystemColors.Window;
			this.trkDropShadow.LargeChange = 20;
			this.trkDropShadow.Location = new System.Drawing.Point(159, 127);
			this.trkDropShadow.Maximum = 100;
			this.trkDropShadow.Name = "trkDropShadow";
			this.trkDropShadow.Size = new System.Drawing.Size(140, 56);
			this.trkDropShadow.SmallChange = 10;
			this.trkDropShadow.TabIndex = 11;
			this.trkDropShadow.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkDropShadow, "Offset of drop shadow");
			this.trkDropShadow.Value = 50;
			this.trkDropShadow.Scroll += new System.EventHandler(this.DropShadow_Scroll);
			// 
			// lblDropShadowUnit
			// 
			this.lblDropShadowUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblDropShadowUnit.AutoSize = true;
			this.lblDropShadowUnit.Location = new System.Drawing.Point(305, 146);
			this.lblDropShadowUnit.Name = "lblDropShadowUnit";
			this.lblDropShadowUnit.Size = new System.Drawing.Size(43, 17);
			this.lblDropShadowUnit.TabIndex = 12;
			this.lblDropShadowUnit.Text = "pixels";
			this.toolTip1.SetToolTip(this.lblDropShadowUnit, "Offset of drop shadow");
			// 
			// flowImagesPerSwitch
			// 
			this.flowImagesPerSwitch.AutoSize = true;
			this.flowImagesPerSwitch.Controls.Add(this.c_numCollageImages);
			this.flowImagesPerSwitch.Controls.Add(this.c_numCollageImagesLabel);
			this.flowImagesPerSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowImagesPerSwitch.Location = new System.Drawing.Point(354, 3);
			this.flowImagesPerSwitch.Name = "flowImagesPerSwitch";
			this.flowImagesPerSwitch.Size = new System.Drawing.Size(401, 56);
			this.flowImagesPerSwitch.TabIndex = 19;
			// 
			// c_numCollageImages
			// 
			this.c_numCollageImages.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_numCollageImages.Location = new System.Drawing.Point(3, 3);
			this.c_numCollageImages.Name = "c_numCollageImages";
			this.c_numCollageImages.Size = new System.Drawing.Size(40, 23);
			this.c_numCollageImages.TabIndex = 3;
			this.toolTip1.SetToolTip(this.c_numCollageImages, "Number of images to draw each time the wallpaper changes");
			this.c_numCollageImages.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_numCollageImagesLabel
			// 
			this.c_numCollageImagesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_numCollageImagesLabel.AutoSize = true;
			this.c_numCollageImagesLabel.Location = new System.Drawing.Point(49, 6);
			this.c_numCollageImagesLabel.Name = "c_numCollageImagesLabel";
			this.c_numCollageImagesLabel.Size = new System.Drawing.Size(130, 17);
			this.c_numCollageImagesLabel.TabIndex = 4;
			this.c_numCollageImagesLabel.Text = "image(s) per switch";
			this.toolTip1.SetToolTip(this.c_numCollageImagesLabel, "Number of images to draw each time the wallpaper changes");
			// 
			// flowBorderColor
			// 
			this.flowBorderColor.AutoSize = true;
			this.flowBorderColor.Controls.Add(this.c_borderColorLabel);
			this.flowBorderColor.Controls.Add(this.c_borderColor);
			this.flowBorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowBorderColor.Location = new System.Drawing.Point(354, 65);
			this.flowBorderColor.Name = "flowBorderColor";
			this.flowBorderColor.Size = new System.Drawing.Size(401, 56);
			this.flowBorderColor.TabIndex = 20;
			// 
			// c_borderColorLabel
			// 
			this.c_borderColorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_borderColorLabel.AutoSize = true;
			this.c_borderColorLabel.Location = new System.Drawing.Point(3, 4);
			this.c_borderColorLabel.Name = "c_borderColorLabel";
			this.c_borderColorLabel.Size = new System.Drawing.Size(45, 17);
			this.c_borderColorLabel.TabIndex = 8;
			this.c_borderColorLabel.Text = "Color:";
			this.toolTip1.SetToolTip(this.c_borderColorLabel, "Border color");
			// 
			// c_borderColor
			// 
			this.c_borderColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_borderColor.BackColor = System.Drawing.Color.Transparent;
			this.c_borderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.c_borderColor.Color = System.Drawing.Color.Transparent;
			this.c_borderColor.Location = new System.Drawing.Point(55, 4);
			this.c_borderColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_borderColor.Name = "c_borderColor";
			this.c_borderColor.Size = new System.Drawing.Size(30, 18);
			this.c_borderColor.TabIndex = 9;
			this.toolTip1.SetToolTip(this.c_borderColor, "Border color");
			this.c_borderColor.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.BorderColor_ColorChanged);
			// 
			// grpBackgroundColor
			// 
			this.grpBackgroundColor.AutoSize = true;
			this.grpBackgroundColor.Controls.Add(this.tblBackgroundColor);
			this.grpBackgroundColor.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpBackgroundColor.Location = new System.Drawing.Point(3, 358);
			this.grpBackgroundColor.Name = "grpBackgroundColor";
			this.grpBackgroundColor.Size = new System.Drawing.Size(764, 90);
			this.grpBackgroundColor.TabIndex = 3;
			this.grpBackgroundColor.TabStop = false;
			this.grpBackgroundColor.Text = "Background Color";
			// 
			// tblBackgroundColor
			// 
			this.tblBackgroundColor.AutoSize = true;
			this.tblBackgroundColor.ColumnCount = 2;
			this.tblBackgroundColor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblBackgroundColor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblBackgroundColor.Controls.Add(this.tblBackgroundColorGradients, 0, 0);
			this.tblBackgroundColor.Controls.Add(this.flowBackgroundOpacity, 1, 0);
			this.tblBackgroundColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblBackgroundColor.Location = new System.Drawing.Point(3, 19);
			this.tblBackgroundColor.Name = "tblBackgroundColor";
			this.tblBackgroundColor.RowCount = 1;
			this.tblBackgroundColor.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblBackgroundColor.Size = new System.Drawing.Size(758, 68);
			this.tblBackgroundColor.TabIndex = 6;
			// 
			// tblBackgroundColorGradients
			// 
			this.tblBackgroundColorGradients.AutoSize = true;
			this.tblBackgroundColorGradients.ColumnCount = 2;
			this.tblBackgroundColorGradients.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblBackgroundColorGradients.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblBackgroundColorGradients.Controls.Add(this.lblBackTop, 0, 0);
			this.tblBackgroundColorGradients.Controls.Add(this.clrBackBottom, 1, 1);
			this.tblBackgroundColorGradients.Controls.Add(this.clrBackTop, 1, 0);
			this.tblBackgroundColorGradients.Controls.Add(this.lblBackBottom, 0, 1);
			this.tblBackgroundColorGradients.Dock = System.Windows.Forms.DockStyle.Top;
			this.tblBackgroundColorGradients.Location = new System.Drawing.Point(3, 3);
			this.tblBackgroundColorGradients.Name = "tblBackgroundColorGradients";
			this.tblBackgroundColorGradients.RowCount = 2;
			this.tblBackgroundColorGradients.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblBackgroundColorGradients.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblBackgroundColorGradients.Size = new System.Drawing.Size(196, 52);
			this.tblBackgroundColorGradients.TabIndex = 0;
			// 
			// lblBackTop
			// 
			this.lblBackTop.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblBackTop.AutoSize = true;
			this.lblBackTop.Location = new System.Drawing.Point(3, 4);
			this.lblBackTop.Name = "lblBackTop";
			this.lblBackTop.Size = new System.Drawing.Size(133, 17);
			this.lblBackTop.TabIndex = 0;
			this.lblBackTop.Text = "Top Gradient Color:";
			this.toolTip1.SetToolTip(this.lblBackTop, "Top color of background gradient");
			// 
			// clrBackBottom
			// 
			this.clrBackBottom.BackColor = System.Drawing.Color.Black;
			this.clrBackBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackBottom.Color = System.Drawing.Color.Black;
			this.clrBackBottom.Location = new System.Drawing.Point(162, 30);
			this.clrBackBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.clrBackBottom.Name = "clrBackBottom";
			this.clrBackBottom.Size = new System.Drawing.Size(30, 18);
			this.clrBackBottom.TabIndex = 3;
			this.toolTip1.SetToolTip(this.clrBackBottom, "Bottom color of background gradient");
			this.clrBackBottom.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.BackBottom_ColorChanged);
			// 
			// clrBackTop
			// 
			this.clrBackTop.BackColor = System.Drawing.Color.Black;
			this.clrBackTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBackTop.Color = System.Drawing.Color.Black;
			this.clrBackTop.Location = new System.Drawing.Point(162, 4);
			this.clrBackTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.clrBackTop.Name = "clrBackTop";
			this.clrBackTop.Size = new System.Drawing.Size(30, 18);
			this.clrBackTop.TabIndex = 1;
			this.toolTip1.SetToolTip(this.clrBackTop, "Top color of background gradient");
			this.clrBackTop.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.BackTop_ColorChanged);
			// 
			// lblBackBottom
			// 
			this.lblBackBottom.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblBackBottom.AutoSize = true;
			this.lblBackBottom.Location = new System.Drawing.Point(3, 30);
			this.lblBackBottom.Name = "lblBackBottom";
			this.lblBackBottom.Size = new System.Drawing.Size(152, 17);
			this.lblBackBottom.TabIndex = 2;
			this.lblBackBottom.Text = "Bottom Gradient Color:";
			this.toolTip1.SetToolTip(this.lblBackBottom, "Bottom color of background gradient");
			// 
			// flowBackgroundOpacity
			// 
			this.flowBackgroundOpacity.AutoSize = true;
			this.flowBackgroundOpacity.Controls.Add(this.lblOpacity);
			this.flowBackgroundOpacity.Controls.Add(this.trkOpacity);
			this.flowBackgroundOpacity.Controls.Add(this.lblOpacityDisplay);
			this.flowBackgroundOpacity.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowBackgroundOpacity.Location = new System.Drawing.Point(205, 3);
			this.flowBackgroundOpacity.Name = "flowBackgroundOpacity";
			this.flowBackgroundOpacity.Size = new System.Drawing.Size(550, 62);
			this.flowBackgroundOpacity.TabIndex = 1;
			// 
			// lblOpacity
			// 
			this.lblOpacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblOpacity.AutoSize = true;
			this.lblOpacity.Location = new System.Drawing.Point(3, 22);
			this.lblOpacity.Name = "lblOpacity";
			this.lblOpacity.Size = new System.Drawing.Size(140, 17);
			this.lblOpacity.TabIndex = 3;
			this.lblOpacity.Text = "Background Opacity:";
			this.toolTip1.SetToolTip(this.lblOpacity, "Opacity of background used to fade out previous images");
			// 
			// trkOpacity
			// 
			this.trkOpacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.trkOpacity.BackColor = System.Drawing.SystemColors.Window;
			this.trkOpacity.LargeChange = 20;
			this.trkOpacity.Location = new System.Drawing.Point(149, 3);
			this.trkOpacity.Maximum = 100;
			this.trkOpacity.Name = "trkOpacity";
			this.trkOpacity.Size = new System.Drawing.Size(140, 56);
			this.trkOpacity.SmallChange = 10;
			this.trkOpacity.TabIndex = 4;
			this.trkOpacity.TickFrequency = 10;
			this.toolTip1.SetToolTip(this.trkOpacity, "Opacity of background used to fade out previous images");
			this.trkOpacity.Value = 50;
			this.trkOpacity.Scroll += new System.EventHandler(this.Opacity_Scroll);
			// 
			// lblOpacityDisplay
			// 
			this.lblOpacityDisplay.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblOpacityDisplay.AutoSize = true;
			this.lblOpacityDisplay.Location = new System.Drawing.Point(295, 22);
			this.lblOpacityDisplay.Name = "lblOpacityDisplay";
			this.lblOpacityDisplay.Size = new System.Drawing.Size(20, 17);
			this.lblOpacityDisplay.TabIndex = 5;
			this.lblOpacityDisplay.Text = "%";
			this.toolTip1.SetToolTip(this.lblOpacityDisplay, "Opacity of background used to fade out previous images");
			// 
			// grpFrequency
			// 
			this.grpFrequency.AutoSize = true;
			this.grpFrequency.Controls.Add(this.tblChangeFrequency);
			this.grpFrequency.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpFrequency.Location = new System.Drawing.Point(3, 231);
			this.grpFrequency.Name = "grpFrequency";
			this.grpFrequency.Size = new System.Drawing.Size(764, 121);
			this.grpFrequency.TabIndex = 2;
			this.grpFrequency.TabStop = false;
			this.grpFrequency.Text = "Change Frequency";
			// 
			// tblChangeFrequency
			// 
			this.tblChangeFrequency.AutoSize = true;
			this.tblChangeFrequency.ColumnCount = 1;
			this.tblChangeFrequency.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblChangeFrequency.Controls.Add(this.c_activateOnExitCheckBox, 0, 1);
			this.tblChangeFrequency.Controls.Add(this.tblChangeFrequencyGrid, 0, 0);
			this.tblChangeFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblChangeFrequency.Location = new System.Drawing.Point(3, 19);
			this.tblChangeFrequency.Name = "tblChangeFrequency";
			this.tblChangeFrequency.RowCount = 2;
			this.tblChangeFrequency.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblChangeFrequency.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblChangeFrequency.Size = new System.Drawing.Size(758, 99);
			this.tblChangeFrequency.TabIndex = 8;
			// 
			// c_activateOnExitCheckBox
			// 
			this.c_activateOnExitCheckBox.AutoSize = true;
			this.c_activateOnExitCheckBox.Location = new System.Drawing.Point(3, 75);
			this.c_activateOnExitCheckBox.Name = "c_activateOnExitCheckBox";
			this.c_activateOnExitCheckBox.Size = new System.Drawing.Size(272, 21);
			this.c_activateOnExitCheckBox.TabIndex = 6;
			this.c_activateOnExitCheckBox.Text = "Temporarily activate this theme on exit";
			this.toolTip1.SetToolTip(this.c_activateOnExitCheckBox, "When WallSwitch is closing, this theme will be displayed until the next time Wall" +
        "Switch starts up.");
			this.c_activateOnExitCheckBox.UseVisualStyleBackColor = true;
			this.c_activateOnExitCheckBox.CheckedChanged += new System.EventHandler(this.ActivateOnExitCheckBox_CheckedChanged);
			// 
			// tblChangeFrequencyGrid
			// 
			this.tblChangeFrequencyGrid.AutoSize = true;
			this.tblChangeFrequencyGrid.ColumnCount = 3;
			this.tblChangeFrequencyGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblChangeFrequencyGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblChangeFrequencyGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblChangeFrequencyGrid.Controls.Add(this.flowLayoutPanel3, 1, 0);
			this.tblChangeFrequencyGrid.Controls.Add(this.c_activateThemeHotKey, 1, 1);
			this.tblChangeFrequencyGrid.Controls.Add(this.c_activateThemeLabel, 0, 1);
			this.tblChangeFrequencyGrid.Controls.Add(this.chkFadeTransition, 2, 0);
			this.tblChangeFrequencyGrid.Controls.Add(this.lblFrequency, 0, 0);
			this.tblChangeFrequencyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblChangeFrequencyGrid.Location = new System.Drawing.Point(3, 3);
			this.tblChangeFrequencyGrid.Name = "tblChangeFrequencyGrid";
			this.tblChangeFrequencyGrid.RowCount = 2;
			this.tblChangeFrequencyGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblChangeFrequencyGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblChangeFrequencyGrid.Size = new System.Drawing.Size(752, 66);
			this.tblChangeFrequencyGrid.TabIndex = 7;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.AutoSize = true;
			this.flowLayoutPanel3.Controls.Add(this.txtThemeFreq);
			this.flowLayoutPanel3.Controls.Add(this.cmbThemePeriod);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(147, 3);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(298, 31);
			this.flowLayoutPanel3.TabIndex = 1;
			// 
			// txtThemeFreq
			// 
			this.txtThemeFreq.Location = new System.Drawing.Point(3, 3);
			this.txtThemeFreq.MaxLength = 5;
			this.txtThemeFreq.Name = "txtThemeFreq";
			this.txtThemeFreq.Size = new System.Drawing.Size(60, 23);
			this.txtThemeFreq.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtThemeFreq, "Wallpaper change interval");
			this.txtThemeFreq.TextChanged += new System.EventHandler(this.ControlChanged);
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
			this.cmbThemePeriod.Location = new System.Drawing.Point(69, 3);
			this.cmbThemePeriod.Name = "cmbThemePeriod";
			this.cmbThemePeriod.Size = new System.Drawing.Size(100, 25);
			this.cmbThemePeriod.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmbThemePeriod, "Wallpaper change interval");
			this.cmbThemePeriod.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeHotKey
			// 
			this.c_activateThemeHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_activateThemeHotKey.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_activateThemeHotKey.HotKey = null;
			this.c_activateThemeHotKey.Location = new System.Drawing.Point(147, 40);
			this.c_activateThemeHotKey.Name = "c_activateThemeHotKey";
			this.c_activateThemeHotKey.ReadOnly = true;
			this.c_activateThemeHotKey.Size = new System.Drawing.Size(298, 23);
			this.c_activateThemeHotKey.TabIndex = 5;
			this.toolTip1.SetToolTip(this.c_activateThemeHotKey, "Hot key to change to this theme");
			this.c_activateThemeHotKey.HotKeyChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_activateThemeLabel
			// 
			this.c_activateThemeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_activateThemeLabel.AutoSize = true;
			this.c_activateThemeLabel.Location = new System.Drawing.Point(3, 43);
			this.c_activateThemeLabel.Name = "c_activateThemeLabel";
			this.c_activateThemeLabel.Size = new System.Drawing.Size(62, 17);
			this.c_activateThemeLabel.TabIndex = 4;
			this.c_activateThemeLabel.Text = "Hot Key:";
			// 
			// chkFadeTransition
			// 
			this.chkFadeTransition.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkFadeTransition.AutoSize = true;
			this.chkFadeTransition.Location = new System.Drawing.Point(451, 8);
			this.chkFadeTransition.Name = "chkFadeTransition";
			this.chkFadeTransition.Size = new System.Drawing.Size(177, 21);
			this.chkFadeTransition.TabIndex = 3;
			this.chkFadeTransition.Text = "Cross-Fade Transitions";
			this.toolTip1.SetToolTip(this.chkFadeTransition, "Use smooth cross-fading between wallpapers (Windows 7 or higher)");
			this.chkFadeTransition.UseVisualStyleBackColor = true;
			this.chkFadeTransition.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblFrequency
			// 
			this.lblFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblFrequency.AutoSize = true;
			this.lblFrequency.Location = new System.Drawing.Point(3, 10);
			this.lblFrequency.Name = "lblFrequency";
			this.lblFrequency.Size = new System.Drawing.Size(138, 17);
			this.lblFrequency.TabIndex = 0;
			this.lblFrequency.Text = "Change image every";
			// 
			// grpDisplayMode
			// 
			this.grpDisplayMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpDisplayMode.AutoSize = true;
			this.grpDisplayMode.Controls.Add(this.tblDisplayModeRows);
			this.grpDisplayMode.Location = new System.Drawing.Point(3, 3);
			this.grpDisplayMode.Name = "grpDisplayMode";
			this.grpDisplayMode.Size = new System.Drawing.Size(764, 222);
			this.grpDisplayMode.TabIndex = 1;
			this.grpDisplayMode.TabStop = false;
			this.grpDisplayMode.Text = "Display Mode";
			// 
			// tblDisplayModeRows
			// 
			this.tblDisplayModeRows.AutoSize = true;
			this.tblDisplayModeRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tblDisplayModeRows.ColumnCount = 1;
			this.tblDisplayModeRows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblDisplayModeRows.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tblDisplayModeRows.Controls.Add(this.c_separateMonitors, 0, 1);
			this.tblDisplayModeRows.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tblDisplayModeRows.Controls.Add(this.flowDisplayModeScalingLimit, 0, 3);
			this.tblDisplayModeRows.Controls.Add(this.flowLayoutPanel2, 0, 4);
			this.tblDisplayModeRows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblDisplayModeRows.Location = new System.Drawing.Point(3, 19);
			this.tblDisplayModeRows.Name = "tblDisplayModeRows";
			this.tblDisplayModeRows.RowCount = 5;
			this.tblDisplayModeRows.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDisplayModeRows.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDisplayModeRows.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDisplayModeRows.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDisplayModeRows.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDisplayModeRows.Size = new System.Drawing.Size(758, 200);
			this.tblDisplayModeRows.TabIndex = 15;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.c_themeMode);
			this.flowLayoutPanel1.Controls.Add(this.c_themeOrder);
			this.flowLayoutPanel1.Controls.Add(this.c_imageFit);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(752, 31);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// c_themeMode
			// 
			this.c_themeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_themeMode.FormattingEnabled = true;
			this.c_themeMode.Items.AddRange(new object[] {
            "Full Screen",
            "Collage"});
			this.c_themeMode.Location = new System.Drawing.Point(3, 3);
			this.c_themeMode.Name = "c_themeMode";
			this.c_themeMode.Size = new System.Drawing.Size(141, 25);
			this.c_themeMode.TabIndex = 0;
			this.toolTip1.SetToolTip(this.c_themeMode, "Display mode");
			this.c_themeMode.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_themeOrder
			// 
			this.c_themeOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_themeOrder.FormattingEnabled = true;
			this.c_themeOrder.Items.AddRange(new object[] {
            "Sequential",
            "Random"});
			this.c_themeOrder.Location = new System.Drawing.Point(150, 3);
			this.c_themeOrder.Name = "c_themeOrder";
			this.c_themeOrder.Size = new System.Drawing.Size(141, 25);
			this.c_themeOrder.TabIndex = 1;
			this.toolTip1.SetToolTip(this.c_themeOrder, "Display order");
			this.c_themeOrder.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
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
			this.c_imageFit.Location = new System.Drawing.Point(297, 3);
			this.c_imageFit.Name = "c_imageFit";
			this.c_imageFit.Size = new System.Drawing.Size(141, 25);
			this.c_imageFit.TabIndex = 2;
			this.toolTip1.SetToolTip(this.c_imageFit, "Image sizing method");
			this.c_imageFit.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_separateMonitors
			// 
			this.c_separateMonitors.AutoSize = true;
			this.c_separateMonitors.Checked = true;
			this.c_separateMonitors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.c_separateMonitors.Location = new System.Drawing.Point(3, 40);
			this.c_separateMonitors.Name = "c_separateMonitors";
			this.c_separateMonitors.Size = new System.Drawing.Size(237, 21);
			this.c_separateMonitors.TabIndex = 3;
			this.c_separateMonitors.Text = "Separate image for each monitor";
			this.toolTip1.SetToolTip(this.c_separateMonitors, "Display a separate image on each monitor");
			this.c_separateMonitors.UseVisualStyleBackColor = true;
			this.c_separateMonitors.CheckedChanged += new System.EventHandler(this.chkSeparateMonitors_CheckedChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.c_maxClipTrackBar, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.c_allowSpanning, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.c_maxClipLabel, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.c_maxClipPercent, 3, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 67);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(752, 62);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// c_maxClipTrackBar
			// 
			this.c_maxClipTrackBar.BackColor = System.Drawing.SystemColors.Window;
			this.c_maxClipTrackBar.LargeChange = 20;
			this.c_maxClipTrackBar.Location = new System.Drawing.Point(583, 3);
			this.c_maxClipTrackBar.Maximum = 100;
			this.c_maxClipTrackBar.Name = "c_maxClipTrackBar";
			this.c_maxClipTrackBar.Size = new System.Drawing.Size(140, 56);
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
			this.c_allowSpanning.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_allowSpanning.AutoSize = true;
			this.c_allowSpanning.Checked = true;
			this.c_allowSpanning.CheckState = System.Windows.Forms.CheckState.Checked;
			this.c_allowSpanning.Location = new System.Drawing.Point(3, 20);
			this.c_allowSpanning.Name = "c_allowSpanning";
			this.c_allowSpanning.Size = new System.Drawing.Size(228, 21);
			this.c_allowSpanning.TabIndex = 4;
			this.c_allowSpanning.Text = "Allow spanning across monitors";
			this.toolTip1.SetToolTip(this.c_allowSpanning, "If an image is the correct aspect ratio, span it across multiple monitors.");
			this.c_allowSpanning.UseVisualStyleBackColor = true;
			this.c_allowSpanning.CheckedChanged += new System.EventHandler(this.AllowSpanning_CheckedChanged);
			// 
			// c_maxClipLabel
			// 
			this.c_maxClipLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_maxClipLabel.AutoSize = true;
			this.c_maxClipLabel.Location = new System.Drawing.Point(442, 22);
			this.c_maxClipLabel.Name = "c_maxClipLabel";
			this.c_maxClipLabel.Size = new System.Drawing.Size(135, 17);
			this.c_maxClipLabel.TabIndex = 5;
			this.c_maxClipLabel.Text = "Maximum Image Clip";
			this.toolTip1.SetToolTip(this.c_maxClipLabel, "When spanning across monitors that don\'t make a perfect rectangle, part of the im" +
        "age may be offscreen. This setting limits how much of the image WallSwitch will " +
        "allow to be clipped.");
			// 
			// c_maxClipPercent
			// 
			this.c_maxClipPercent.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_maxClipPercent.AutoSize = true;
			this.c_maxClipPercent.Location = new System.Drawing.Point(729, 22);
			this.c_maxClipPercent.Name = "c_maxClipPercent";
			this.c_maxClipPercent.Size = new System.Drawing.Size(20, 17);
			this.c_maxClipPercent.TabIndex = 7;
			this.c_maxClipPercent.Text = "%";
			this.toolTip1.SetToolTip(this.c_maxClipPercent, "When spanning across monitors that don\'t make a perfect rectangle, part of the im" +
        "age may be offscreen. This setting limits how much of the image WallSwitch will " +
        "allow to be clipped.");
			// 
			// flowDisplayModeScalingLimit
			// 
			this.flowDisplayModeScalingLimit.AutoSize = true;
			this.flowDisplayModeScalingLimit.Controls.Add(this.c_limitScale);
			this.flowDisplayModeScalingLimit.Controls.Add(this.c_maxScale);
			this.flowDisplayModeScalingLimit.Controls.Add(this.c_maxScalePct);
			this.flowDisplayModeScalingLimit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowDisplayModeScalingLimit.Location = new System.Drawing.Point(3, 135);
			this.flowDisplayModeScalingLimit.Name = "flowDisplayModeScalingLimit";
			this.flowDisplayModeScalingLimit.Size = new System.Drawing.Size(752, 29);
			this.flowDisplayModeScalingLimit.TabIndex = 5;
			// 
			// c_limitScale
			// 
			this.c_limitScale.AutoSize = true;
			this.c_limitScale.Location = new System.Drawing.Point(3, 3);
			this.c_limitScale.Name = "c_limitScale";
			this.c_limitScale.Size = new System.Drawing.Size(165, 21);
			this.c_limitScale.TabIndex = 8;
			this.c_limitScale.Text = "Limit image scaling to";
			this.toolTip1.SetToolTip(this.c_limitScale, "Limit the magnification of images?");
			this.c_limitScale.UseVisualStyleBackColor = true;
			this.c_limitScale.CheckedChanged += new System.EventHandler(this.chkLimitScale_CheckedChanged);
			// 
			// c_maxScale
			// 
			this.c_maxScale.Location = new System.Drawing.Point(174, 3);
			this.c_maxScale.Name = "c_maxScale";
			this.c_maxScale.Size = new System.Drawing.Size(50, 23);
			this.c_maxScale.TabIndex = 9;
			this.toolTip1.SetToolTip(this.c_maxScale, "Maximum amount of magnification (in percent)");
			this.c_maxScale.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_maxScalePct
			// 
			this.c_maxScalePct.AutoSize = true;
			this.c_maxScalePct.Location = new System.Drawing.Point(230, 0);
			this.c_maxScalePct.Name = "c_maxScalePct";
			this.c_maxScalePct.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.c_maxScalePct.Size = new System.Drawing.Size(20, 21);
			this.c_maxScalePct.TabIndex = 10;
			this.c_maxScalePct.Text = "%";
			this.toolTip1.SetToolTip(this.c_maxScalePct, "Maximum amount of magnification (in percent)");
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.Controls.Add(this.c_randomGroup);
			this.flowLayoutPanel2.Controls.Add(this.c_randomGroupCount);
			this.flowLayoutPanel2.Controls.Add(this.c_randomGroupCountLabel);
			this.flowLayoutPanel2.Controls.Add(this.c_clearBetweenRandomGroups);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 170);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(752, 27);
			this.flowLayoutPanel2.TabIndex = 6;
			// 
			// c_randomGroup
			// 
			this.c_randomGroup.AutoSize = true;
			this.c_randomGroup.Location = new System.Drawing.Point(2, 2);
			this.c_randomGroup.Margin = new System.Windows.Forms.Padding(2);
			this.c_randomGroup.Name = "c_randomGroup";
			this.c_randomGroup.Size = new System.Drawing.Size(156, 21);
			this.c_randomGroup.TabIndex = 11;
			this.c_randomGroup.Text = "Sequential Groups?";
			this.toolTip1.SetToolTip(this.c_randomGroup, "Enable groups of sequential images to be displayed before selecting the next rand" +
        "om image?");
			this.c_randomGroup.UseVisualStyleBackColor = true;
			this.c_randomGroup.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_randomGroupCount
			// 
			this.c_randomGroupCount.Location = new System.Drawing.Point(162, 2);
			this.c_randomGroupCount.Margin = new System.Windows.Forms.Padding(2);
			this.c_randomGroupCount.Name = "c_randomGroupCount";
			this.c_randomGroupCount.Size = new System.Drawing.Size(50, 23);
			this.c_randomGroupCount.TabIndex = 12;
			this.toolTip1.SetToolTip(this.c_randomGroupCount, "The number of sequential images chosen between random selections.");
			this.c_randomGroupCount.TextChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_randomGroupCountLabel
			// 
			this.c_randomGroupCountLabel.AutoSize = true;
			this.c_randomGroupCountLabel.Location = new System.Drawing.Point(216, 0);
			this.c_randomGroupCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.c_randomGroupCountLabel.Name = "c_randomGroupCountLabel";
			this.c_randomGroupCountLabel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.c_randomGroupCountLabel.Size = new System.Drawing.Size(119, 21);
			this.c_randomGroupCountLabel.TabIndex = 13;
			this.c_randomGroupCountLabel.Text = "images per group";
			this.toolTip1.SetToolTip(this.c_randomGroupCountLabel, "The number of sequential images chosen between random selections.");
			// 
			// c_clearBetweenRandomGroups
			// 
			this.c_clearBetweenRandomGroups.AutoSize = true;
			this.c_clearBetweenRandomGroups.Location = new System.Drawing.Point(339, 2);
			this.c_clearBetweenRandomGroups.Margin = new System.Windows.Forms.Padding(2);
			this.c_clearBetweenRandomGroups.Name = "c_clearBetweenRandomGroups";
			this.c_clearBetweenRandomGroups.Size = new System.Drawing.Size(176, 21);
			this.c_clearBetweenRandomGroups.TabIndex = 14;
			this.c_clearBetweenRandomGroups.Text = "Clear between groups?";
			this.c_clearBetweenRandomGroups.UseVisualStyleBackColor = true;
			this.c_clearBetweenRandomGroups.CheckedChanged += new System.EventHandler(this.ControlChanged);
			// 
			// c_filterTab
			// 
			this.c_filterTab.Controls.Add(this.c_filterFlow);
			this.c_filterTab.Controls.Add(this.panel3);
			this.c_filterTab.Location = new System.Drawing.Point(4, 26);
			this.c_filterTab.Margin = new System.Windows.Forms.Padding(2);
			this.c_filterTab.Name = "c_filterTab";
			this.c_filterTab.Padding = new System.Windows.Forms.Padding(2);
			this.c_filterTab.Size = new System.Drawing.Size(776, 402);
			this.c_filterTab.TabIndex = 5;
			this.c_filterTab.Text = "Filter";
			this.c_filterTab.UseVisualStyleBackColor = true;
			// 
			// c_filterFlow
			// 
			this.c_filterFlow.AutoScroll = true;
			this.c_filterFlow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_filterFlow.Location = new System.Drawing.Point(2, 33);
			this.c_filterFlow.Margin = new System.Windows.Forms.Padding(0);
			this.c_filterFlow.Name = "c_filterFlow";
			this.c_filterFlow.Size = new System.Drawing.Size(772, 367);
			this.c_filterFlow.TabIndex = 0;
			this.toolTip1.SetToolTip(this.c_filterFlow, "Conditions to narrow images selected");
			// 
			// panel3
			// 
			this.panel3.AutoSize = true;
			this.panel3.Controls.Add(this.c_addFilterButton);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(2, 2);
			this.panel3.Margin = new System.Windows.Forms.Padding(2);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(772, 31);
			this.panel3.TabIndex = 0;
			// 
			// c_addFilterButton
			// 
			this.c_addFilterButton.AutoSize = true;
			this.c_addFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.c_addFilterButton.Location = new System.Drawing.Point(2, 2);
			this.c_addFilterButton.Margin = new System.Windows.Forms.Padding(2);
			this.c_addFilterButton.Name = "c_addFilterButton";
			this.c_addFilterButton.Size = new System.Drawing.Size(141, 27);
			this.c_addFilterButton.TabIndex = 0;
			this.c_addFilterButton.Text = "Add Filter Condition";
			this.c_addFilterButton.UseVisualStyleBackColor = true;
			this.c_addFilterButton.Click += new System.EventHandler(this.c_addFilterButton_Click);
			// 
			// c_widgetsTab
			// 
			this.c_widgetsTab.Controls.Add(this.c_widgetPanelSplitter);
			this.c_widgetsTab.Location = new System.Drawing.Point(4, 26);
			this.c_widgetsTab.Name = "c_widgetsTab";
			this.c_widgetsTab.Padding = new System.Windows.Forms.Padding(3);
			this.c_widgetsTab.Size = new System.Drawing.Size(776, 402);
			this.c_widgetsTab.TabIndex = 4;
			this.c_widgetsTab.Text = "Widgets";
			this.c_widgetsTab.UseVisualStyleBackColor = true;
			// 
			// c_widgetPanelSplitter
			// 
			this.c_widgetPanelSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetPanelSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.c_widgetPanelSplitter.Location = new System.Drawing.Point(3, 3);
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
			this.c_widgetPanelSplitter.Size = new System.Drawing.Size(770, 396);
			this.c_widgetPanelSplitter.SplitterDistance = 534;
			this.c_widgetPanelSplitter.TabIndex = 2;
			// 
			// c_widgetLayout
			// 
			this.c_widgetLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetLayout.Location = new System.Drawing.Point(0, 33);
			this.c_widgetLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_widgetLayout.Name = "c_widgetLayout";
			this.c_widgetLayout.Size = new System.Drawing.Size(534, 363);
			this.c_widgetLayout.TabIndex = 0;
			this.c_widgetLayout.WidgetsChanged += new System.EventHandler(this.c_widgetLayout_WidgetsChanged);
			this.c_widgetLayout.SelectedWidgetChanged += new System.EventHandler<WallSwitch.WidgetLayoutControl.WidgetEventArgs>(this.c_widgetLayout_SelectedWidgetChanged);
			this.c_widgetLayout.WidgetAdded += new System.EventHandler<WallSwitch.WidgetLayoutControl.WidgetEventArgs>(this.c_widgetLayout_WidgetAdded);
			this.c_widgetLayout.WidgetDeleted += new System.EventHandler<WallSwitch.WidgetLayoutControl.WidgetEventArgs>(this.c_widgetLayout_WidgetDeleted);
			this.c_widgetLayout.WidgetOrderChanged += new System.EventHandler(this.c_widgetLayout_WidgetOrderChanged);
			// 
			// c_widgetTopPanel
			// 
			this.c_widgetTopPanel.AutoSize = true;
			this.c_widgetTopPanel.Controls.Add(this.tblWidgetSelector);
			this.c_widgetTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_widgetTopPanel.Location = new System.Drawing.Point(0, 0);
			this.c_widgetTopPanel.Name = "c_widgetTopPanel";
			this.c_widgetTopPanel.Size = new System.Drawing.Size(534, 33);
			this.c_widgetTopPanel.TabIndex = 1;
			// 
			// tblWidgetSelector
			// 
			this.tblWidgetSelector.AutoSize = true;
			this.tblWidgetSelector.ColumnCount = 3;
			this.tblWidgetSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblWidgetSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblWidgetSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblWidgetSelector.Controls.Add(this.c_widgetTypesLabel, 0, 0);
			this.tblWidgetSelector.Controls.Add(this.c_widgetTypes, 1, 0);
			this.tblWidgetSelector.Controls.Add(this.c_addWidgetButton, 2, 0);
			this.tblWidgetSelector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblWidgetSelector.Location = new System.Drawing.Point(0, 0);
			this.tblWidgetSelector.Name = "tblWidgetSelector";
			this.tblWidgetSelector.RowCount = 1;
			this.tblWidgetSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblWidgetSelector.Size = new System.Drawing.Size(534, 33);
			this.tblWidgetSelector.TabIndex = 3;
			// 
			// c_widgetTypesLabel
			// 
			this.c_widgetTypesLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.c_widgetTypesLabel.AutoSize = true;
			this.c_widgetTypesLabel.Location = new System.Drawing.Point(3, 8);
			this.c_widgetTypesLabel.Name = "c_widgetTypesLabel";
			this.c_widgetTypesLabel.Size = new System.Drawing.Size(56, 17);
			this.c_widgetTypesLabel.TabIndex = 0;
			this.c_widgetTypesLabel.Text = "Widget:";
			// 
			// c_widgetTypes
			// 
			this.c_widgetTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_widgetTypes.FormattingEnabled = true;
			this.c_widgetTypes.Location = new System.Drawing.Point(65, 3);
			this.c_widgetTypes.Name = "c_widgetTypes";
			this.c_widgetTypes.Size = new System.Drawing.Size(417, 25);
			this.c_widgetTypes.TabIndex = 1;
			this.c_widgetTypes.SelectedIndexChanged += new System.EventHandler(this.c_widgetTypes_SelectedIndexChanged);
			// 
			// c_addWidgetButton
			// 
			this.c_addWidgetButton.AutoSize = true;
			this.c_addWidgetButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_addWidgetButton.Location = new System.Drawing.Point(488, 3);
			this.c_addWidgetButton.Name = "c_addWidgetButton";
			this.c_addWidgetButton.Size = new System.Drawing.Size(43, 27);
			this.c_addWidgetButton.TabIndex = 2;
			this.c_addWidgetButton.Text = "Add";
			this.c_addWidgetButton.UseVisualStyleBackColor = true;
			this.c_addWidgetButton.Click += new System.EventHandler(this.c_addWidgetButton_Click);
			// 
			// c_widgetPanelPropSplitter
			// 
			this.c_widgetPanelPropSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetPanelPropSplitter.Location = new System.Drawing.Point(0, 0);
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
			this.c_widgetPanelPropSplitter.Size = new System.Drawing.Size(232, 396);
			this.c_widgetPanelPropSplitter.SplitterDistance = 131;
			this.c_widgetPanelPropSplitter.TabIndex = 0;
			// 
			// c_widgetList
			// 
			this.c_widgetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c_widgetColumn});
			this.c_widgetList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetList.HideSelection = false;
			this.c_widgetList.Location = new System.Drawing.Point(0, 0);
			this.c_widgetList.MultiSelect = false;
			this.c_widgetList.Name = "c_widgetList";
			this.c_widgetList.Size = new System.Drawing.Size(207, 131);
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
			this.c_widgetControlRightPanel.Location = new System.Drawing.Point(207, 0);
			this.c_widgetControlRightPanel.Name = "c_widgetControlRightPanel";
			this.c_widgetControlRightPanel.Size = new System.Drawing.Size(25, 131);
			this.c_widgetControlRightPanel.TabIndex = 4;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.c_widgetDeleteButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 108);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(25, 23);
			this.panel2.TabIndex = 4;
			// 
			// c_widgetDeleteButton
			// 
			this.c_widgetDeleteButton.Image = global::WallSwitch.Res.Delete;
			this.c_widgetDeleteButton.Location = new System.Drawing.Point(2, 0);
			this.c_widgetDeleteButton.Name = "c_widgetDeleteButton";
			this.c_widgetDeleteButton.Size = new System.Drawing.Size(23, 23);
			this.c_widgetDeleteButton.TabIndex = 3;
			this.c_widgetDeleteButton.UseVisualStyleBackColor = true;
			this.c_widgetDeleteButton.Click += new System.EventHandler(this.c_widgetDeleteButton_Click);
			// 
			// c_widgetMoveUpButton
			// 
			this.c_widgetMoveUpButton.Image = global::WallSwitch.Res.MoveUp;
			this.c_widgetMoveUpButton.Location = new System.Drawing.Point(2, 0);
			this.c_widgetMoveUpButton.Name = "c_widgetMoveUpButton";
			this.c_widgetMoveUpButton.Size = new System.Drawing.Size(23, 23);
			this.c_widgetMoveUpButton.TabIndex = 1;
			this.c_widgetMoveUpButton.UseVisualStyleBackColor = true;
			this.c_widgetMoveUpButton.Click += new System.EventHandler(this.c_widgetMoveUpButton_Click);
			// 
			// c_widgetMoveDownButton
			// 
			this.c_widgetMoveDownButton.Image = global::WallSwitch.Res.MoveDown;
			this.c_widgetMoveDownButton.Location = new System.Drawing.Point(2, 29);
			this.c_widgetMoveDownButton.Name = "c_widgetMoveDownButton";
			this.c_widgetMoveDownButton.Size = new System.Drawing.Size(23, 23);
			this.c_widgetMoveDownButton.TabIndex = 2;
			this.c_widgetMoveDownButton.UseVisualStyleBackColor = true;
			this.c_widgetMoveDownButton.Click += new System.EventHandler(this.c_widgetMoveDownButton_Click);
			// 
			// c_widgetPropertyGrid
			// 
			this.c_widgetPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.c_widgetPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_widgetPropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.c_widgetPropertyGrid.Name = "c_widgetPropertyGrid";
			this.c_widgetPropertyGrid.Size = new System.Drawing.Size(232, 261);
			this.c_widgetPropertyGrid.TabIndex = 0;
			this.c_widgetPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.c_widgetPropertyGrid_PropertyValueChanged);
			// 
			// tabHistory
			// 
			this.tabHistory.Controls.Add(this.c_historyTab);
			this.tabHistory.Location = new System.Drawing.Point(4, 26);
			this.tabHistory.Name = "tabHistory";
			this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabHistory.Size = new System.Drawing.Size(776, 402);
			this.tabHistory.TabIndex = 3;
			this.tabHistory.Text = "History";
			this.tabHistory.UseVisualStyleBackColor = true;
			// 
			// c_historyTab
			// 
			this.c_historyTab.ContextMenuStrip = this.cmHistory;
			this.c_historyTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_historyTab.ImageToolTip = this.toolTip1;
			this.c_historyTab.Location = new System.Drawing.Point(3, 3);
			this.c_historyTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_historyTab.Name = "c_historyTab";
			this.c_historyTab.Size = new System.Drawing.Size(770, 396);
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
			this.cmHistory.Size = new System.Drawing.Size(164, 106);
			// 
			// ciOpenHistoryFile
			// 
			this.ciOpenHistoryFile.Name = "ciOpenHistoryFile";
			this.ciOpenHistoryFile.Size = new System.Drawing.Size(163, 24);
			this.ciOpenHistoryFile.Text = "&Open File";
			this.ciOpenHistoryFile.Click += new System.EventHandler(this.ciOpenHistoryFile_Click);
			// 
			// ciExploreHistoryFile
			// 
			this.ciExploreHistoryFile.Name = "ciExploreHistoryFile";
			this.ciExploreHistoryFile.Size = new System.Drawing.Size(163, 24);
			this.ciExploreHistoryFile.Text = "&Explore File";
			this.ciExploreHistoryFile.Click += new System.EventHandler(this.ciExploreHistoryFile_Click);
			// 
			// ciDeleteHistoryFile
			// 
			this.ciDeleteHistoryFile.Name = "ciDeleteHistoryFile";
			this.ciDeleteHistoryFile.Size = new System.Drawing.Size(163, 24);
			this.ciDeleteHistoryFile.Text = "&Delete File";
			this.ciDeleteHistoryFile.Click += new System.EventHandler(this.ciDeleteHistoryFile_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(160, 6);
			// 
			// ciClearHistoryList
			// 
			this.ciClearHistoryList.Name = "ciClearHistoryList";
			this.ciClearHistoryList.Size = new System.Drawing.Size(163, 24);
			this.ciClearHistoryList.Text = "&Clear History";
			this.ciClearHistoryList.Click += new System.EventHandler(this.ciClearHistoryList_Click);
			// 
			// grpNavButtons
			// 
			this.grpNavButtons.AutoSize = true;
			this.grpNavButtons.Controls.Add(this.tblNavButtons);
			this.grpNavButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpNavButtons.Location = new System.Drawing.Point(529, 3);
			this.grpNavButtons.Name = "grpNavButtons";
			this.grpNavButtons.Size = new System.Drawing.Size(114, 84);
			this.grpNavButtons.TabIndex = 11;
			this.grpNavButtons.TabStop = false;
			// 
			// tblNavButtons
			// 
			this.tblNavButtons.AutoSize = true;
			this.tblNavButtons.ColumnCount = 3;
			this.tblNavButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tblNavButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tblNavButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tblNavButtons.Controls.Add(this.btnSwitchNow, 2, 0);
			this.tblNavButtons.Controls.Add(this.btnPause, 1, 0);
			this.tblNavButtons.Controls.Add(this.btnPrevious, 0, 0);
			this.tblNavButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblNavButtons.Location = new System.Drawing.Point(3, 19);
			this.tblNavButtons.Name = "tblNavButtons";
			this.tblNavButtons.RowCount = 1;
			this.tblNavButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblNavButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
			this.tblNavButtons.Size = new System.Drawing.Size(108, 62);
			this.tblNavButtons.TabIndex = 4;
			// 
			// grpTransparency
			// 
			this.grpTransparency.AutoSize = true;
			this.grpTransparency.Controls.Add(this.flowTransparency);
			this.grpTransparency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpTransparency.Location = new System.Drawing.Point(649, 3);
			this.grpTransparency.Name = "grpTransparency";
			this.grpTransparency.Size = new System.Drawing.Size(132, 84);
			this.grpTransparency.TabIndex = 12;
			this.grpTransparency.TabStop = false;
			this.grpTransparency.Text = "Transparency";
			// 
			// flowTransparency
			// 
			this.flowTransparency.AutoSize = true;
			this.flowTransparency.Controls.Add(this.c_transparencyTrackBar);
			this.flowTransparency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowTransparency.Location = new System.Drawing.Point(3, 19);
			this.flowTransparency.Name = "flowTransparency";
			this.flowTransparency.Size = new System.Drawing.Size(126, 62);
			this.flowTransparency.TabIndex = 1;
			// 
			// c_transparencyTrackBar
			// 
			this.c_transparencyTrackBar.Location = new System.Drawing.Point(3, 3);
			this.c_transparencyTrackBar.Minimum = 1;
			this.c_transparencyTrackBar.Name = "c_transparencyTrackBar";
			this.c_transparencyTrackBar.Size = new System.Drawing.Size(120, 56);
			this.c_transparencyTrackBar.TabIndex = 0;
			this.c_transparencyTrackBar.Value = 1;
			this.c_transparencyTrackBar.Scroll += new System.EventHandler(this.TransparencyTrackBar_Scroll);
			// 
			// tblMain
			// 
			this.tblMain.ColumnCount = 1;
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblMain.Controls.Add(this.tblTopControls, 0, 0);
			this.tblMain.Controls.Add(this.tcTheme, 0, 1);
			this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblMain.Location = new System.Drawing.Point(0, 28);
			this.tblMain.Name = "tblMain";
			this.tblMain.RowCount = 2;
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblMain.Size = new System.Drawing.Size(790, 536);
			this.tblMain.TabIndex = 13;
			// 
			// tblTopControls
			// 
			this.tblTopControls.AutoSize = true;
			this.tblTopControls.ColumnCount = 3;
			this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblTopControls.Controls.Add(this.grpTheme, 0, 0);
			this.tblTopControls.Controls.Add(this.grpNavButtons, 1, 0);
			this.tblTopControls.Controls.Add(this.grpTransparency, 2, 0);
			this.tblTopControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblTopControls.Location = new System.Drawing.Point(3, 3);
			this.tblTopControls.Name = "tblTopControls";
			this.tblTopControls.RowCount = 1;
			this.tblTopControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblTopControls.Size = new System.Drawing.Size(784, 90);
			this.tblTopControls.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(790, 564);
			this.Controls.Add(this.tblMain);
			this.Controls.Add(this.mainMenu);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.grpTheme.PerformLayout();
			this.tblTheme.ResumeLayout(false);
			this.tblTheme.PerformLayout();
			this.cmTheme.ResumeLayout(false);
			this.cmTray.ResumeLayout(false);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.tcTheme.ResumeLayout(false);
			this.c_locationsTab.ResumeLayout(false);
			this.c_settingsTab.ResumeLayout(false);
			this.c_settingsTab.PerformLayout();
			this.tblFlowContent.ResumeLayout(false);
			this.tblFlowContent.PerformLayout();
			this.grpBackgroundColorEffects.ResumeLayout(false);
			this.grpBackgroundColorEffects.PerformLayout();
			this.flowBackgroundImageEffects.ResumeLayout(false);
			this.flowBackgroundImageEffects.PerformLayout();
			this.flowColorEffect.ResumeLayout(false);
			this.flowColorEffect.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkColorEffectCollageFadeRatio)).EndInit();
			this.flowBackgroundBlur.ResumeLayout(false);
			this.flowBackgroundBlur.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBackgroundBlurDist)).EndInit();
			this.grpImageEffects.ResumeLayout(false);
			this.grpImageEffects.PerformLayout();
			this.flowLayoutPanel6.ResumeLayout(false);
			this.flowLayoutPanel6.PerformLayout();
			this.grpCollageDisplay.ResumeLayout(false);
			this.grpCollageDisplay.PerformLayout();
			this.tblCollageDisplay.ResumeLayout(false);
			this.tblCollageDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c_edgeDist)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowFeatherDist)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadowOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkDropShadow)).EndInit();
			this.flowImagesPerSwitch.ResumeLayout(false);
			this.flowImagesPerSwitch.PerformLayout();
			this.flowBorderColor.ResumeLayout(false);
			this.flowBorderColor.PerformLayout();
			this.grpBackgroundColor.ResumeLayout(false);
			this.grpBackgroundColor.PerformLayout();
			this.tblBackgroundColor.ResumeLayout(false);
			this.tblBackgroundColor.PerformLayout();
			this.tblBackgroundColorGradients.ResumeLayout(false);
			this.tblBackgroundColorGradients.PerformLayout();
			this.flowBackgroundOpacity.ResumeLayout(false);
			this.flowBackgroundOpacity.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).EndInit();
			this.grpFrequency.ResumeLayout(false);
			this.grpFrequency.PerformLayout();
			this.tblChangeFrequency.ResumeLayout(false);
			this.tblChangeFrequency.PerformLayout();
			this.tblChangeFrequencyGrid.ResumeLayout(false);
			this.tblChangeFrequencyGrid.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.grpDisplayMode.ResumeLayout(false);
			this.grpDisplayMode.PerformLayout();
			this.tblDisplayModeRows.ResumeLayout(false);
			this.tblDisplayModeRows.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_maxClipTrackBar)).EndInit();
			this.flowDisplayModeScalingLimit.ResumeLayout(false);
			this.flowDisplayModeScalingLimit.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.c_filterTab.ResumeLayout(false);
			this.c_filterTab.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.c_widgetsTab.ResumeLayout(false);
			this.c_widgetPanelSplitter.Panel1.ResumeLayout(false);
			this.c_widgetPanelSplitter.Panel1.PerformLayout();
			this.c_widgetPanelSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelSplitter)).EndInit();
			this.c_widgetPanelSplitter.ResumeLayout(false);
			this.c_widgetTopPanel.ResumeLayout(false);
			this.c_widgetTopPanel.PerformLayout();
			this.tblWidgetSelector.ResumeLayout(false);
			this.tblWidgetSelector.PerformLayout();
			this.c_widgetPanelPropSplitter.Panel1.ResumeLayout(false);
			this.c_widgetPanelPropSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_widgetPanelPropSplitter)).EndInit();
			this.c_widgetPanelPropSplitter.ResumeLayout(false);
			this.c_widgetControlRightPanel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tabHistory.ResumeLayout(false);
			this.cmHistory.ResumeLayout(false);
			this.grpNavButtons.ResumeLayout(false);
			this.grpNavButtons.PerformLayout();
			this.tblNavButtons.ResumeLayout(false);
			this.tblNavButtons.PerformLayout();
			this.grpTransparency.ResumeLayout(false);
			this.grpTransparency.PerformLayout();
			this.flowTransparency.ResumeLayout(false);
			this.flowTransparency.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_transparencyTrackBar)).EndInit();
			this.tblMain.ResumeLayout(false);
			this.tblMain.PerformLayout();
			this.tblTopControls.ResumeLayout(false);
			this.tblTopControls.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lstLocations;
		private System.Windows.Forms.ComboBox cmbTheme;
		private System.Windows.Forms.GroupBox grpTheme;
		private System.Windows.Forms.ColumnHeader colLocation;
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
		private System.Windows.Forms.ToolStripMenuItem miFileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem miClearHistory;
		private System.Windows.Forms.TabControl tcTheme;
		private System.Windows.Forms.TabPage c_locationsTab;
		private System.Windows.Forms.TabPage c_settingsTab;
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
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.ToolStripMenuItem miHotKeys;
		private System.Windows.Forms.Button btnTheme;
		private System.Windows.Forms.ContextMenuStrip cmTheme;
		private System.Windows.Forms.ToolStripMenuItem ciNewTheme;
		private System.Windows.Forms.ToolStripMenuItem ciRenameTheme;
		private System.Windows.Forms.ToolStripMenuItem ciDeleteTheme;
		private System.Windows.Forms.ToolStripMenuItem ciDuplicateTheme;
		private System.Windows.Forms.ToolStripMenuItem miDuplicateTheme;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem ciSaveTheme;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
		private System.Windows.Forms.GroupBox grpNavButtons;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ToolStripMenuItem miAddRssFeed;
		private System.Windows.Forms.ToolStripMenuItem ciDeleteHistoryFile;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
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
		private System.Windows.Forms.GroupBox grpTransparency;
		private System.Windows.Forms.TrackBar c_transparencyTrackBar;
		private System.Windows.Forms.Panel flowTransparency;
		private System.Windows.Forms.Panel c_widgetControlRightPanel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ColumnHeader colFrequency;
		private System.Windows.Forms.ToolStripMenuItem c_browseLocationMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.TabPage c_filterTab;
		private System.Windows.Forms.FlowLayoutPanel c_filterFlow;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button c_addFilterButton;
		private System.Windows.Forms.ImageList c_locationImages;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		private System.Windows.Forms.TableLayoutPanel tblWidgetSelector;
		private System.Windows.Forms.TableLayoutPanel tblMain;
		private System.Windows.Forms.TableLayoutPanel tblTopControls;
		private System.Windows.Forms.TableLayoutPanel tblTheme;
		private System.Windows.Forms.TableLayoutPanel tblNavButtons;
		private System.Windows.Forms.TableLayoutPanel tblFlowContent;
		private System.Windows.Forms.GroupBox grpBackgroundColorEffects;
		private System.Windows.Forms.FlowLayoutPanel flowBackgroundImageEffects;
		private System.Windows.Forms.FlowLayoutPanel flowColorEffect;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbColorEffectBack;
		private System.Windows.Forms.TrackBar trkColorEffectCollageFadeRatio;
		private System.Windows.Forms.Label lblColorEffectCollageFadeRatioUnit;
		private System.Windows.Forms.FlowLayoutPanel flowBackgroundBlur;
		private System.Windows.Forms.CheckBox chkBackgroundBlur;
		private System.Windows.Forms.TrackBar trkBackgroundBlurDist;
		private System.Windows.Forms.Label lblBackgroundBlurDistValue;
		private System.Windows.Forms.GroupBox grpImageEffects;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbColorEffectFore;
		private System.Windows.Forms.GroupBox grpCollageDisplay;
		private System.Windows.Forms.TableLayoutPanel tblCollageDisplay;
		private System.Windows.Forms.Label lblImageSize;
		private System.Windows.Forms.TrackBar trkImageSize;
		private System.Windows.Forms.Label lblImageSizeDisplay;
		private System.Windows.Forms.ComboBox c_edgeMode;
		private System.Windows.Forms.TrackBar c_edgeDist;
		private System.Windows.Forms.Label lblDropShadowFeatherDist;
		private System.Windows.Forms.TrackBar trkDropShadowFeatherDist;
		private System.Windows.Forms.Label c_edgeDistLabel;
		private System.Windows.Forms.Label lblDropShadowOpacityValue;
		private System.Windows.Forms.CheckBox chkDropShadowFeather;
		private System.Windows.Forms.TrackBar trkDropShadowOpacity;
		private System.Windows.Forms.CheckBox chkDropShadow;
		private System.Windows.Forms.Label lblDropShadowOpacity;
		private System.Windows.Forms.TrackBar trkDropShadow;
		private System.Windows.Forms.Label lblDropShadowUnit;
		private System.Windows.Forms.FlowLayoutPanel flowImagesPerSwitch;
		private System.Windows.Forms.TextBox c_numCollageImages;
		private System.Windows.Forms.Label c_numCollageImagesLabel;
		private System.Windows.Forms.FlowLayoutPanel flowBorderColor;
		private System.Windows.Forms.Label c_borderColorLabel;
		private ColorSample c_borderColor;
		private System.Windows.Forms.GroupBox grpBackgroundColor;
		private System.Windows.Forms.TableLayoutPanel tblBackgroundColor;
		private System.Windows.Forms.TableLayoutPanel tblBackgroundColorGradients;
		private System.Windows.Forms.Label lblBackTop;
		private ColorSample clrBackBottom;
		private ColorSample clrBackTop;
		private System.Windows.Forms.Label lblBackBottom;
		private System.Windows.Forms.FlowLayoutPanel flowBackgroundOpacity;
		private System.Windows.Forms.Label lblOpacity;
		private System.Windows.Forms.TrackBar trkOpacity;
		private System.Windows.Forms.Label lblOpacityDisplay;
		private System.Windows.Forms.GroupBox grpFrequency;
		private System.Windows.Forms.TableLayoutPanel tblChangeFrequency;
		private System.Windows.Forms.CheckBox c_activateOnExitCheckBox;
		private System.Windows.Forms.TableLayoutPanel tblChangeFrequencyGrid;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.TextBox txtThemeFreq;
		private System.Windows.Forms.ComboBox cmbThemePeriod;
		private HotKeyTextBox c_activateThemeHotKey;
		private System.Windows.Forms.Label c_activateThemeLabel;
		private System.Windows.Forms.CheckBox chkFadeTransition;
		private System.Windows.Forms.Label lblFrequency;
		private System.Windows.Forms.GroupBox grpDisplayMode;
		private System.Windows.Forms.TableLayoutPanel tblDisplayModeRows;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.ComboBox c_themeMode;
		private System.Windows.Forms.ComboBox c_themeOrder;
		private System.Windows.Forms.ComboBox c_imageFit;
		private System.Windows.Forms.CheckBox c_separateMonitors;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TrackBar c_maxClipTrackBar;
		private System.Windows.Forms.CheckBox c_allowSpanning;
		private System.Windows.Forms.Label c_maxClipLabel;
		private System.Windows.Forms.Label c_maxClipPercent;
		private System.Windows.Forms.FlowLayoutPanel flowDisplayModeScalingLimit;
		private System.Windows.Forms.CheckBox c_limitScale;
		private System.Windows.Forms.TextBox c_maxScale;
		private System.Windows.Forms.Label c_maxScalePct;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.CheckBox c_randomGroup;
		private System.Windows.Forms.TextBox c_randomGroupCount;
		private System.Windows.Forms.Label c_randomGroupCountLabel;
		private System.Windows.Forms.CheckBox c_clearBetweenRandomGroups;
	}
}

