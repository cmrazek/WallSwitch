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
			this.locationsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciAddFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.ciAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.cmDeleteLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.cmbTheme = new System.Windows.Forms.ComboBox();
			this.grpTheme = new System.Windows.Forms.GroupBox();
			this.btnRenameTheme = new System.Windows.Forms.Button();
			this.btnDeleteTheme = new System.Windows.Forms.Button();
			this.btnNewTheme = new System.Windows.Forms.Button();
			this.cmbImageFit = new System.Windows.Forms.ComboBox();
			this.lblOpacityDisplay = new System.Windows.Forms.Label();
			this.trkOpacity = new System.Windows.Forms.TrackBar();
			this.lblOpacity = new System.Windows.Forms.Label();
			this.txtHotKey = new System.Windows.Forms.TextBox();
			this.lblHotKey = new System.Windows.Forms.Label();
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
			this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
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
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.tabThemeSettings = new System.Windows.Forms.TabControl();
			this.tabLocations = new System.Windows.Forms.TabPage();
			this.tabDisplay = new System.Windows.Forms.TabPage();
			this.grpCollageDisplay = new System.Windows.Forms.GroupBox();
			this.lblFeatherDisplay = new System.Windows.Forms.Label();
			this.lblFeather = new System.Windows.Forms.Label();
			this.trkFeather = new System.Windows.Forms.TrackBar();
			this.clrBackTop = new WallSwitch.ColorSample();
			this.clrBackBottom = new WallSwitch.ColorSample();
			this.tabFrequency = new System.Windows.Forms.TabPage();
			this.locationsMenu.SuspendLayout();
			this.grpTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).BeginInit();
			this.trayMenu.SuspendLayout();
			this.mainMenu.SuspendLayout();
			this.tabThemeSettings.SuspendLayout();
			this.tabLocations.SuspendLayout();
			this.tabDisplay.SuspendLayout();
			this.grpCollageDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkFeather)).BeginInit();
			this.tabFrequency.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstLocations
			// 
			this.lstLocations.AllowDrop = true;
			this.lstLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLocation});
			this.lstLocations.ContextMenuStrip = this.locationsMenu;
			this.lstLocations.Location = new System.Drawing.Point(0, 0);
			this.lstLocations.MultiSelect = false;
			this.lstLocations.Name = "lstLocations";
			this.lstLocations.Size = new System.Drawing.Size(418, 232);
			this.lstLocations.TabIndex = 0;
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
			this.colLocation.Width = 394;
			// 
			// locationsMenu
			// 
			this.locationsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciAddFolder,
            this.ciAddImage,
            this.cmDeleteLocation});
			this.locationsMenu.Name = "locationsMenu";
			this.locationsMenu.Size = new System.Drawing.Size(133, 70);
			this.locationsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.locationsMenu_Opening);
			// 
			// ciAddFolder
			// 
			this.ciAddFolder.Name = "ciAddFolder";
			this.ciAddFolder.Size = new System.Drawing.Size(132, 22);
			this.ciAddFolder.Text = "Add &Folder";
			this.ciAddFolder.Click += new System.EventHandler(this.ciAddFolder_Click);
			// 
			// ciAddImage
			// 
			this.ciAddImage.Name = "ciAddImage";
			this.ciAddImage.Size = new System.Drawing.Size(132, 22);
			this.ciAddImage.Text = "Add &Image";
			this.ciAddImage.Click += new System.EventHandler(this.ciAddImage_Click);
			// 
			// cmDeleteLocation
			// 
			this.cmDeleteLocation.Name = "cmDeleteLocation";
			this.cmDeleteLocation.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.cmDeleteLocation.Size = new System.Drawing.Size(132, 22);
			this.cmDeleteLocation.Text = "&Delete";
			this.cmDeleteLocation.Click += new System.EventHandler(this.cmDeleteLocation_Click);
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
			this.cmbImageFit.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
			// 
			// lblOpacityDisplay
			// 
			this.lblOpacityDisplay.AutoSize = true;
			this.lblOpacityDisplay.Location = new System.Drawing.Point(245, 56);
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
			// txtHotKey
			// 
			this.txtHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.txtHotKey.Location = new System.Drawing.Point(145, 32);
			this.txtHotKey.MaxLength = 5;
			this.txtHotKey.Name = "txtHotKey";
			this.txtHotKey.ReadOnly = true;
			this.txtHotKey.Size = new System.Drawing.Size(141, 20);
			this.txtHotKey.TabIndex = 4;
			this.txtHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotKey_KeyDown);
			// 
			// lblHotKey
			// 
			this.lblHotKey.AutoSize = true;
			this.lblHotKey.Location = new System.Drawing.Point(6, 32);
			this.lblHotKey.Name = "lblHotKey";
			this.lblHotKey.Size = new System.Drawing.Size(48, 13);
			this.lblHotKey.TabIndex = 3;
			this.lblHotKey.Text = "Hot Key:";
			// 
			// chkSeparateMonitors
			// 
			this.chkSeparateMonitors.AutoSize = true;
			this.chkSeparateMonitors.Checked = true;
			this.chkSeparateMonitors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSeparateMonitors.Location = new System.Drawing.Point(9, 209);
			this.chkSeparateMonitors.Name = "chkSeparateMonitors";
			this.chkSeparateMonitors.Size = new System.Drawing.Size(179, 17);
			this.chkSeparateMonitors.TabIndex = 8;
			this.chkSeparateMonitors.Text = "Separate image for each monitor";
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
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenuStrip = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "WallSwitch";
			this.trayIcon.Visible = true;
			this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
			// 
			// trayMenu
			// 
			this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowMainWindow,
            this.ciTheme,
            this.toolStripSeparator1,
            this.cmSwitchNow,
            this.ciSwitchPrev,
            this.toolStripMenuItem1,
            this.cmExit});
			this.trayMenu.Name = "trayMenu";
			this.trayMenu.Size = new System.Drawing.Size(176, 126);
			this.trayMenu.Opening += new System.ComponentModel.CancelEventHandler(this.trayMenu_Opening);
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
			this.btnActivate.UseVisualStyleBackColor = true;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// btnSwitchNow
			// 
			this.btnSwitchNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSwitchNow.Location = new System.Drawing.Point(129, 347);
			this.btnSwitchNow.Name = "btnSwitchNow";
			this.btnSwitchNow.Size = new System.Drawing.Size(30, 23);
			this.btnSwitchNow.TabIndex = 7;
			this.btnSwitchNow.Text = "->";
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
            this.miClearHistory});
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
			this.tabThemeSettings.Location = new System.Drawing.Point(12, 83);
			this.tabThemeSettings.Name = "tabThemeSettings";
			this.tabThemeSettings.SelectedIndex = 0;
			this.tabThemeSettings.Size = new System.Drawing.Size(560, 258);
			this.tabThemeSettings.TabIndex = 10;
			// 
			// tabLocations
			// 
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
			// tabDisplay
			// 
			this.tabDisplay.Controls.Add(this.grpCollageDisplay);
			this.tabDisplay.Controls.Add(this.cmbImageFit);
			this.tabDisplay.Controls.Add(this.chkSeparateMonitors);
			this.tabDisplay.Controls.Add(this.cmbThemeMode);
			this.tabDisplay.Controls.Add(this.lblMode);
			this.tabDisplay.Controls.Add(this.clrBackTop);
			this.tabDisplay.Controls.Add(this.lblBackTop);
			this.tabDisplay.Controls.Add(this.clrBackBottom);
			this.tabDisplay.Controls.Add(this.lblBackBottom);
			this.tabDisplay.Location = new System.Drawing.Point(4, 22);
			this.tabDisplay.Name = "tabDisplay";
			this.tabDisplay.Padding = new System.Windows.Forms.Padding(3);
			this.tabDisplay.Size = new System.Drawing.Size(552, 232);
			this.tabDisplay.TabIndex = 1;
			this.tabDisplay.Text = "Display";
			this.tabDisplay.UseVisualStyleBackColor = true;
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
			this.grpCollageDisplay.Location = new System.Drawing.Point(9, 57);
			this.grpCollageDisplay.Name = "grpCollageDisplay";
			this.grpCollageDisplay.Size = new System.Drawing.Size(320, 146);
			this.grpCollageDisplay.TabIndex = 7;
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
			this.clrBackBottom.ColorChanged += new WallSwitch.ColorSample.ColorChangedEventHandler(this.clrBackBottom_ColorChanged);
			// 
			// tabFrequency
			// 
			this.tabFrequency.Controls.Add(this.cmbThemePeriod);
			this.tabFrequency.Controls.Add(this.lblFrequency);
			this.tabFrequency.Controls.Add(this.lblHotKey);
			this.tabFrequency.Controls.Add(this.txtHotKey);
			this.tabFrequency.Controls.Add(this.txtThemeFreq);
			this.tabFrequency.Location = new System.Drawing.Point(4, 22);
			this.tabFrequency.Name = "tabFrequency";
			this.tabFrequency.Padding = new System.Windows.Forms.Padding(3);
			this.tabFrequency.Size = new System.Drawing.Size(552, 232);
			this.tabFrequency.TabIndex = 2;
			this.tabFrequency.Text = "Frequency";
			this.tabFrequency.UseVisualStyleBackColor = true;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 382);
			this.Controls.Add(this.tabThemeSettings);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.btnActivate);
			this.Controls.Add(this.grpTheme);
			this.Controls.Add(this.btnPrevious);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSwitchNow);
			this.Controls.Add(this.btnApply);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "MainWindow";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "WallSwitch";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			this.locationsMenu.ResumeLayout(false);
			this.grpTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkImageSize)).EndInit();
			this.trayMenu.ResumeLayout(false);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.tabThemeSettings.ResumeLayout(false);
			this.tabLocations.ResumeLayout(false);
			this.tabDisplay.ResumeLayout(false);
			this.tabDisplay.PerformLayout();
			this.grpCollageDisplay.ResumeLayout(false);
			this.grpCollageDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkFeather)).EndInit();
			this.tabFrequency.ResumeLayout(false);
			this.tabFrequency.PerformLayout();
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
		private System.Windows.Forms.ContextMenuStrip trayMenu;
		private System.Windows.Forms.ToolStripMenuItem cmShowMainWindow;
		private System.Windows.Forms.ToolStripMenuItem cmExit;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnRenameTheme;
		private System.Windows.Forms.Button btnActivate;
		private System.Windows.Forms.ToolStripMenuItem cmSwitchNow;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.Button btnSwitchNow;
		private System.Windows.Forms.ContextMenuStrip locationsMenu;
		private System.Windows.Forms.ToolStripMenuItem cmDeleteLocation;
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
		private System.Windows.Forms.Label lblHotKey;
		private System.Windows.Forms.TextBox txtHotKey;
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

	}
}

