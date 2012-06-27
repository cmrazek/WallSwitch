using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WallSwitch
{
	public partial class AboutDialog : Form
	{
		public AboutDialog()
		{
			InitializeComponent();
		}

		private void AboutDialog_Load(object sender, EventArgs e)
		{
			try
			{
				Assembly asm = Assembly.GetExecutingAssembly();
				lblAppVersion.Text = String.Format(Res.AppNameVersion, asm.GetName().Version.ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, String.Format(Res.Exception_GetAppVersion, ex.Message), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
