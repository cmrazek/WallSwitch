namespace WallSwitch
{
	partial class HotKeySettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotKeySettings));
			this.c_nextImageLabel = new System.Windows.Forms.Label();
			this.c_nextImageHotKey = new WallSwitch.HotKeyTextBox();
			this.c_prevImageHotKey = new WallSwitch.HotKeyTextBox();
			this.c_prevImageLabel = new System.Windows.Forms.Label();
			this.c_okButton = new System.Windows.Forms.Button();
			this.c_cancelButton = new System.Windows.Forms.Button();
			this.c_pauseHotKey = new WallSwitch.HotKeyTextBox();
			this.c_pauseLabel = new System.Windows.Forms.Label();
			this.c_clearHistoryHotKey = new WallSwitch.HotKeyTextBox();
			this.c_clearHistoryLabel = new System.Windows.Forms.Label();
			this.c_showWindowHotKey = new WallSwitch.HotKeyTextBox();
			this.c_showWindowLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// c_nextImageLabel
			// 
			this.c_nextImageLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_nextImageLabel.AutoSize = true;
			this.c_nextImageLabel.Location = new System.Drawing.Point(4, 36);
			this.c_nextImageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_nextImageLabel.Name = "c_nextImageLabel";
			this.c_nextImageLabel.Size = new System.Drawing.Size(82, 17);
			this.c_nextImageLabel.TabIndex = 0;
			this.c_nextImageLabel.Text = "Next Image:";
			// 
			// c_nextImageHotKey
			// 
			this.c_nextImageHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_nextImageHotKey.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_nextImageHotKey.HotKey = null;
			this.c_nextImageHotKey.Location = new System.Drawing.Point(129, 34);
			this.c_nextImageHotKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_nextImageHotKey.Name = "c_nextImageHotKey";
			this.c_nextImageHotKey.ReadOnly = true;
			this.c_nextImageHotKey.Size = new System.Drawing.Size(253, 22);
			this.c_nextImageHotKey.TabIndex = 1;
			// 
			// c_prevImageHotKey
			// 
			this.c_prevImageHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_prevImageHotKey.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_prevImageHotKey.HotKey = null;
			this.c_prevImageHotKey.Location = new System.Drawing.Point(129, 64);
			this.c_prevImageHotKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_prevImageHotKey.Name = "c_prevImageHotKey";
			this.c_prevImageHotKey.ReadOnly = true;
			this.c_prevImageHotKey.Size = new System.Drawing.Size(253, 22);
			this.c_prevImageHotKey.TabIndex = 2;
			// 
			// c_prevImageLabel
			// 
			this.c_prevImageLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_prevImageLabel.AutoSize = true;
			this.c_prevImageLabel.Location = new System.Drawing.Point(4, 66);
			this.c_prevImageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_prevImageLabel.Name = "c_prevImageLabel";
			this.c_prevImageLabel.Size = new System.Drawing.Size(109, 17);
			this.c_prevImageLabel.TabIndex = 2;
			this.c_prevImageLabel.Text = "Previous Image:";
			// 
			// c_okButton
			// 
			this.c_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.c_okButton.Location = new System.Drawing.Point(174, 4);
			this.c_okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_okButton.Name = "c_okButton";
			this.c_okButton.Size = new System.Drawing.Size(100, 28);
			this.c_okButton.TabIndex = 5;
			this.c_okButton.Text = "&OK";
			this.c_okButton.UseVisualStyleBackColor = true;
			this.c_okButton.Click += new System.EventHandler(this.c_okButton_Click);
			// 
			// c_cancelButton
			// 
			this.c_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.c_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.c_cancelButton.Location = new System.Drawing.Point(282, 4);
			this.c_cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_cancelButton.Name = "c_cancelButton";
			this.c_cancelButton.Size = new System.Drawing.Size(100, 28);
			this.c_cancelButton.TabIndex = 6;
			this.c_cancelButton.Text = "&Cancel";
			this.c_cancelButton.UseVisualStyleBackColor = true;
			this.c_cancelButton.Click += new System.EventHandler(this.c_cancelButton_Click);
			// 
			// c_pauseHotKey
			// 
			this.c_pauseHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_pauseHotKey.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_pauseHotKey.HotKey = null;
			this.c_pauseHotKey.Location = new System.Drawing.Point(129, 94);
			this.c_pauseHotKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_pauseHotKey.Name = "c_pauseHotKey";
			this.c_pauseHotKey.ReadOnly = true;
			this.c_pauseHotKey.Size = new System.Drawing.Size(253, 22);
			this.c_pauseHotKey.TabIndex = 3;
			// 
			// c_pauseLabel
			// 
			this.c_pauseLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_pauseLabel.AutoSize = true;
			this.c_pauseLabel.Location = new System.Drawing.Point(4, 96);
			this.c_pauseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_pauseLabel.Name = "c_pauseLabel";
			this.c_pauseLabel.Size = new System.Drawing.Size(52, 17);
			this.c_pauseLabel.TabIndex = 6;
			this.c_pauseLabel.Text = "Pause:";
			// 
			// c_clearHistoryHotKey
			// 
			this.c_clearHistoryHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_clearHistoryHotKey.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_clearHistoryHotKey.HotKey = null;
			this.c_clearHistoryHotKey.Location = new System.Drawing.Point(129, 124);
			this.c_clearHistoryHotKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_clearHistoryHotKey.Name = "c_clearHistoryHotKey";
			this.c_clearHistoryHotKey.ReadOnly = true;
			this.c_clearHistoryHotKey.Size = new System.Drawing.Size(253, 22);
			this.c_clearHistoryHotKey.TabIndex = 4;
			// 
			// c_clearHistoryLabel
			// 
			this.c_clearHistoryLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_clearHistoryLabel.AutoSize = true;
			this.c_clearHistoryLabel.Location = new System.Drawing.Point(4, 126);
			this.c_clearHistoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_clearHistoryLabel.Name = "c_clearHistoryLabel";
			this.c_clearHistoryLabel.Size = new System.Drawing.Size(93, 17);
			this.c_clearHistoryLabel.TabIndex = 8;
			this.c_clearHistoryLabel.Text = "Clear History:";
			// 
			// c_showWindowHotKey
			// 
			this.c_showWindowHotKey.BackColor = System.Drawing.SystemColors.Window;
			this.c_showWindowHotKey.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_showWindowHotKey.HotKey = null;
			this.c_showWindowHotKey.Location = new System.Drawing.Point(129, 4);
			this.c_showWindowHotKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.c_showWindowHotKey.Name = "c_showWindowHotKey";
			this.c_showWindowHotKey.ReadOnly = true;
			this.c_showWindowHotKey.Size = new System.Drawing.Size(253, 22);
			this.c_showWindowHotKey.TabIndex = 0;
			// 
			// c_showWindowLabel
			// 
			this.c_showWindowLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.c_showWindowLabel.AutoSize = true;
			this.c_showWindowLabel.Location = new System.Drawing.Point(4, 6);
			this.c_showWindowLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_showWindowLabel.Name = "c_showWindowLabel";
			this.c_showWindowLabel.Size = new System.Drawing.Size(117, 17);
			this.c_showWindowLabel.TabIndex = 9;
			this.c_showWindowLabel.Text = "Show WallSwitch:";
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.flowLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 222);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(386, 36);
			this.panel1.TabIndex = 10;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.c_showWindowLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.c_showWindowHotKey, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.c_clearHistoryHotKey, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.c_nextImageLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.c_clearHistoryLabel, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.c_nextImageHotKey, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.c_pauseHotKey, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.c_prevImageLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.c_pauseLabel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.c_prevImageHotKey, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(386, 222);
			this.tableLayoutPanel1.TabIndex = 11;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.c_cancelButton);
			this.flowLayoutPanel1.Controls.Add(this.c_okButton);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(386, 36);
			this.flowLayoutPanel1.TabIndex = 7;
			// 
			// HotKeySettings
			// 
			this.AcceptButton = this.c_okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.c_cancelButton;
			this.ClientSize = new System.Drawing.Size(386, 258);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "HotKeySettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Hot Keys";
			this.Load += new System.EventHandler(this.HotKeySettings_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label c_nextImageLabel;
		private HotKeyTextBox c_nextImageHotKey;
		private HotKeyTextBox c_prevImageHotKey;
		private System.Windows.Forms.Label c_prevImageLabel;
		private System.Windows.Forms.Button c_okButton;
		private System.Windows.Forms.Button c_cancelButton;
		private HotKeyTextBox c_pauseHotKey;
		private System.Windows.Forms.Label c_pauseLabel;
		private HotKeyTextBox c_clearHistoryHotKey;
		private System.Windows.Forms.Label c_clearHistoryLabel;
		private HotKeyTextBox c_showWindowHotKey;
		private System.Windows.Forms.Label c_showWindowLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}