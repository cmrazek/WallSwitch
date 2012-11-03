using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch
{
	public partial class PromptDialog : Form
	{
		public delegate bool ValidateCallBack(string text);

		public ValidateCallBack ValidateString { get; set; }

		public PromptDialog()
		{
			InitializeComponent();
		}

		private void PromptDialog_Load(object sender, EventArgs e)
		{
			btnOK.Enabled = ValidateString != null ? ValidateString(txtString.Text) : !string.IsNullOrWhiteSpace(txtString.Text);
			txtString.SelectAll();
		}

		public string String
		{
			get { return txtString.Text; }
			set { txtString.Text = value; }
		}

		public string Prompt
		{
			get { return txtPrompt.Text; }
			set { txtPrompt.Text = value; }
		}

		private void txtString_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = ValidateString != null ? ValidateString(txtString.Text) : !string.IsNullOrWhiteSpace(txtString.Text);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtString.Text))
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
