using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch
{
	public partial class SettingsDialog : Form
	{
		private UpdateChecker _updateChecker = null;

		public SettingsDialog()
		{
			InitializeComponent();
		}

		private void SettingsDialog_Load(object sender, EventArgs e)
		{
			try
			{
				txtStartUpDelay.Text = Settings.StartUpDelay.ToString();
				chkStartWithWindows.Checked = Settings.StartWithWindows;
				chkCheckForUpdates.Checked = Settings.CheckForUpdatesOnStartup;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private bool ValidateForm(bool showErrors, bool save)
		{
			int startUpDelay;
			if (!int.TryParse(txtStartUpDelay.Text, out startUpDelay) || startUpDelay < 0)
			{
				if (showErrors)
				{
					this.ShowError(Res.Error_InvalidStartUpDelay);
					return false;
				}
			}

			if (save)
			{
				Settings.StartUpDelay = startUpDelay;
				Settings.StartWithWindows = chkStartWithWindows.Checked;
				Settings.CheckForUpdatesOnStartup = chkCheckForUpdates.Checked;
			}

			return true;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidateForm(true, true))
				{
					DialogResult = DialogResult.OK;
					Close();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult = DialogResult.Cancel;
				Close();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void btnCheckForUpdates_Click(object sender, EventArgs e)
		{
			try
			{
				if (_updateChecker != null) return;

				_updateChecker = new UpdateChecker();
				_updateChecker.UpdateAvailable += new EventHandler<UpdateCheckEventArgs>(_updateChecker_UpdateAvailable);
				_updateChecker.NoUpdateAvailable += new EventHandler<UpdateCheckEventArgs>(_updateChecker_NoUpdateAvailable);
				_updateChecker.UpdateCheckFailed += new EventHandler<UpdateCheckEventArgs>(_updateChecker_UpdateCheckFailed);
				_updateChecker.Check();
				btnCheckForUpdates.Enabled = false;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		void _updateChecker_UpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { _updateChecker_UpdateAvailable(sender, e); }));
					return;
				}

				if (MessageBox.Show(this, string.Format(Res.SettingsUpdateAvailable, e.WebVersion.ToAppFormat()),
					Res.SettingsUpdateCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					== DialogResult.Yes)
				{
					e.OpenUpdateUrl();
				}

				UpdateCheckerFinished();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		void _updateChecker_NoUpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { _updateChecker_NoUpdateAvailable(sender, e); }));
					return;
				}

				MessageBox.Show(this, string.Format(Res.SettingsUpdateNotAvailable, e.ExeVersion.ToAppFormat()),
					Res.SettingsUpdateCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				UpdateCheckerFinished();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		void _updateChecker_UpdateCheckFailed(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { _updateChecker_UpdateCheckFailed(sender, e); }));
					return;
				}

				MessageBox.Show(this, Res.SettingsUpdateFailed, Res.SettingsUpdateCaption,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				UpdateCheckerFinished();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void UpdateCheckerFinished()
		{
			_updateChecker = null;
			btnCheckForUpdates.Enabled = true;
		}
	}
}
