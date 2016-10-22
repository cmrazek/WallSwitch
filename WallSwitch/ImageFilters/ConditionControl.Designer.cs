namespace WallSwitch.ImageFilters
{
	partial class ConditionControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.c_condTypeCombo = new System.Windows.Forms.ComboBox();
			this.c_condCompareCombo = new System.Windows.Forms.ComboBox();
			this.c_condValuePanel = new System.Windows.Forms.Panel();
			this.c_addButton = new System.Windows.Forms.Button();
			this.c_deleteButton = new System.Windows.Forms.Button();
			this.c_operatorCombo = new System.Windows.Forms.ComboBox();
			this.c_bracketsPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// c_condTypeCombo
			// 
			this.c_condTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_condTypeCombo.FormattingEnabled = true;
			this.c_condTypeCombo.Location = new System.Drawing.Point(97, 3);
			this.c_condTypeCombo.Name = "c_condTypeCombo";
			this.c_condTypeCombo.Size = new System.Drawing.Size(200, 24);
			this.c_condTypeCombo.TabIndex = 0;
			this.c_condTypeCombo.SelectedIndexChanged += new System.EventHandler(this.CondTypeCombo_SelectedIndexChanged);
			// 
			// c_condCompareCombo
			// 
			this.c_condCompareCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_condCompareCombo.FormattingEnabled = true;
			this.c_condCompareCombo.Location = new System.Drawing.Point(303, 3);
			this.c_condCompareCombo.Name = "c_condCompareCombo";
			this.c_condCompareCombo.Size = new System.Drawing.Size(200, 24);
			this.c_condCompareCombo.TabIndex = 1;
			this.c_condCompareCombo.SelectedIndexChanged += new System.EventHandler(this.CondCompareCombo_SelectedIndexChanged);
			// 
			// c_condValuePanel
			// 
			this.c_condValuePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.c_condValuePanel.Location = new System.Drawing.Point(509, 3);
			this.c_condValuePanel.Name = "c_condValuePanel";
			this.c_condValuePanel.Size = new System.Drawing.Size(150, 24);
			this.c_condValuePanel.TabIndex = 2;
			// 
			// c_addButton
			// 
			this.c_addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_addButton.Location = new System.Drawing.Point(665, 3);
			this.c_addButton.Name = "c_addButton";
			this.c_addButton.Size = new System.Drawing.Size(23, 23);
			this.c_addButton.TabIndex = 3;
			this.c_addButton.Text = "+";
			this.c_addButton.UseVisualStyleBackColor = true;
			this.c_addButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// c_deleteButton
			// 
			this.c_deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_deleteButton.Location = new System.Drawing.Point(694, 3);
			this.c_deleteButton.Name = "c_deleteButton";
			this.c_deleteButton.Size = new System.Drawing.Size(23, 23);
			this.c_deleteButton.TabIndex = 4;
			this.c_deleteButton.Text = "-";
			this.c_deleteButton.UseVisualStyleBackColor = true;
			this.c_deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// c_operatorCombo
			// 
			this.c_operatorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_operatorCombo.FormattingEnabled = true;
			this.c_operatorCombo.Location = new System.Drawing.Point(31, 2);
			this.c_operatorCombo.Name = "c_operatorCombo";
			this.c_operatorCombo.Size = new System.Drawing.Size(60, 24);
			this.c_operatorCombo.TabIndex = 6;
			this.c_operatorCombo.SelectedIndexChanged += new System.EventHandler(this.OperatorCombo_SelectedIndexChanged);
			// 
			// c_bracketsPanel
			// 
			this.c_bracketsPanel.Location = new System.Drawing.Point(6, 0);
			this.c_bracketsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.c_bracketsPanel.Name = "c_bracketsPanel";
			this.c_bracketsPanel.Size = new System.Drawing.Size(16, 31);
			this.c_bracketsPanel.TabIndex = 7;
			this.c_bracketsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.c_bracketsPanel_Paint);
			// 
			// ConditionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.c_bracketsPanel);
			this.Controls.Add(this.c_operatorCombo);
			this.Controls.Add(this.c_deleteButton);
			this.Controls.Add(this.c_addButton);
			this.Controls.Add(this.c_condValuePanel);
			this.Controls.Add(this.c_condCompareCombo);
			this.Controls.Add(this.c_condTypeCombo);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ConditionControl";
			this.Size = new System.Drawing.Size(720, 31);
			this.Load += new System.EventHandler(this.ConditionControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox c_condTypeCombo;
		private System.Windows.Forms.ComboBox c_condCompareCombo;
		private System.Windows.Forms.Panel c_condValuePanel;
		private System.Windows.Forms.Button c_addButton;
		private System.Windows.Forms.Button c_deleteButton;
		private System.Windows.Forms.ComboBox c_operatorCombo;
		private System.Windows.Forms.Panel c_bracketsPanel;
	}
}
