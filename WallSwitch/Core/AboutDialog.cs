using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using WallSwitch.SettingsStore;

namespace WallSwitch
{
	public partial class AboutDialog : Form
	{
		private string _downloadUpdateUrl = null;

		public AboutDialog()
		{
			InitializeComponent();
		}

		private void AboutDialog_Load(object sender, EventArgs e)
		{
			try
			{
				lblAppVersion.Text = UpdateChecker.AssemblyVersion.ToAppFormat();

				lnkDownloadUpdate.Visible = false;

				var checker = new UpdateChecker();
				checker.UpdateAvailable += new EventHandler<UpdateCheckEventArgs>(checker_UpdateAvailable);
				checker.NoUpdateAvailable += new EventHandler<UpdateCheckEventArgs>(checker_NoUpdateAvailable);
				checker.UpdateCheckFailed += new EventHandler<UpdateCheckEventArgs>(checker_UpdateCheckFailed);
				checker.Check();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, String.Format(Res.Exception_GetAppVersion, ex.Message), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void checker_UpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				SetUpdateInfo(string.Format(Res.AboutUpdateAvailable, e.WebVersion.ToAppFormat()), e.UpdateUrl);
			}
			catch (Exception ex)
			{
				Log.Write(ex);
				SetUpdateInfo(null, null);
			}
		}

		void checker_NoUpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				SetUpdateInfo(Res.AboutUpdateNotAvailable, null);
			}
			catch (Exception ex)
			{
				Log.Write(ex);
				SetUpdateInfo(null, null);
			}
		}

		void checker_UpdateCheckFailed(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				SetUpdateInfo(null, null);
			}
			catch (Exception ex)
			{
				Log.Write(ex);
				SetUpdateInfo(null, null);
			}
		}

		private void lnkDownloadUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(_downloadUpdateUrl)) Process.Start(_downloadUpdateUrl);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SetUpdateInfo(string message, string url)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { SetUpdateInfo(message, url); }));
					return;
				}

				if (!string.IsNullOrEmpty(message))
				{
					lblUpdateInfo.Text = message;
					lblUpdateInfo.Visible = true;
				}
				else
				{
					lblUpdateInfo.Visible = false;
				}

				_downloadUpdateUrl = url;
				lnkDownloadUpdate.Visible = !string.IsNullOrEmpty(_downloadUpdateUrl);
			}
			catch (Exception ex)
			{
				Log.Write(ex);
			}
		}

		private void lnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start(Res.Website);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
	}
}
