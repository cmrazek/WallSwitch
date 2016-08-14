namespace WallSwitch
{
	partial class FeedDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeedDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.txtFreq = new System.Windows.Forms.TextBox();
			this.cmbPeriod = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtType = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 43);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "Path/URL:";
			// 
			// txtUrl
			// 
			this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUrl.Location = new System.Drawing.Point(135, 39);
			this.txtUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(382, 22);
			this.txtUrl.TabIndex = 0;
			this.toolTip1.SetToolTip(this.txtUrl, "Path or address");
			this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
			// 
			// txtFreq
			// 
			this.txtFreq.Location = new System.Drawing.Point(135, 71);
			this.txtFreq.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtFreq.MaxLength = 5;
			this.txtFreq.Name = "txtFreq";
			this.txtFreq.Size = new System.Drawing.Size(79, 22);
			this.txtFreq.TabIndex = 2;
			this.toolTip1.SetToolTip(this.txtFreq, "Interval between directory/feed scans");
			this.txtFreq.TextChanged += new System.EventHandler(this.txtFreq_TextChanged);
			// 
			// cmbPeriod
			// 
			this.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPeriod.FormattingEnabled = true;
			this.cmbPeriod.Items.AddRange(new object[] {
            "Seconds",
            "Minutes",
            "Hours",
            "Days"});
			this.cmbPeriod.Location = new System.Drawing.Point(223, 71);
			this.cmbPeriod.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cmbPeriod.Name = "cmbPeriod";
			this.cmbPeriod.Size = new System.Drawing.Size(99, 24);
			this.cmbPeriod.TabIndex = 3;
			this.toolTip1.SetToolTip(this.cmbPeriod, "Interval between directory/feed scans");
			this.cmbPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbPeriod_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 75);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Update Interval:";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(370, 5);
			this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 28);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "&OK";
			this.toolTip1.SetToolTip(this.btnOk, "Save and exit");
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(478, 5);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 28);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.toolTip1.SetToolTip(this.btnCancel, "Exit without saving");
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 11);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Type:";
			// 
			// txtType
			// 
			this.txtType.Location = new System.Drawing.Point(135, 7);
			this.txtType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtType.Name = "txtType";
			this.txtType.ReadOnly = true;
			this.txtType.Size = new System.Drawing.Size(132, 22);
			this.txtType.TabIndex = 8;
			this.toolTip1.SetToolTip(this.txtType, "Type of location");
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(526, 37);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(40, 28);
			this.btnBrowse.TabIndex = 9;
			this.btnBrowse.Text = "...";
			this.toolTip1.SetToolTip(this.btnBrowse, "Browse for location");
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 108);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(582, 37);
			this.panel1.TabIndex = 10;
			// 
			// FeedDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(582, 145);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtType);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbPeriod);
			this.Controls.Add(this.txtFreq);
			this.Controls.Add(this.txtUrl);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximumSize = new System.Drawing.Size(43683, 192);
			this.MinimumSize = new System.Drawing.Size(463, 192);
			this.Name = "FeedDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Location Settings";
			this.Load += new System.EventHandler(this.FeedDialog_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUrl;
		private System.Windows.Forms.TextBox txtFreq;
		private System.Windows.Forms.ComboBox cmbPeriod;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Panel panel1;
	}
}