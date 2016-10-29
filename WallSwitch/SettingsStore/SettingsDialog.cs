using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch.SettingsStore
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
				c_startUpDelayTextBox.Text = Settings.StartUpDelay.ToString();
				c_startWithWindowsCheckBox.Checked = Settings.StartWithWindows;
				c_checkForUpdatesCheckBox.Checked = Settings.CheckForUpdatesOnStartup;
				c_ignoreHiddenFilesCheckbox.Checked = Settings.IgnoreHiddenFiles;
				c_logLevelCombo.InitForEnum<LogLevel>(Settings.LogLevel);
				c_loadHistoryImages.Checked = Settings.LoadHistoryImages;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private bool ValidateForm(bool showErrors, bool save)
		{
			int startUpDelay;
			if (!int.TryParse(c_startUpDelayTextBox.Text, out startUpDelay) || startUpDelay < 0)
			{
				if (showErrors)
				{
					c_tabControl.SelectedTab = c_startupTab;
					c_startUpDelayTextBox.Focus();
					this.ShowError(Res.Error_InvalidStartUpDelay);
					return false;
				}
			}

			if (save)
			{
				Settings.StartUpDelay = startUpDelay;
				Settings.StartWithWindows = c_startWithWindowsCheckBox.Checked;
				Settings.CheckForUpdatesOnStartup = c_checkForUpdatesCheckBox.Checked;
				Settings.IgnoreHiddenFiles = c_ignoreHiddenFilesCheckbox.Checked;
				Settings.LogLevel = c_logLevelCombo.GetEnumValue<LogLevel>();
				Settings.LoadHistoryImages = c_loadHistoryImages.Checked;
			}

			return true;
		}

		private void OkButton_Click(object sender, EventArgs e)
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

		private void CancelButton_Click(object sender, EventArgs e)
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

		private void CheckForUpdatesButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (_updateChecker != null) return;

				_updateChecker = new UpdateChecker();
				_updateChecker.UpdateAvailable += new EventHandler<UpdateCheckEventArgs>(UpdateChecker_UpdateAvailable);
				_updateChecker.NoUpdateAvailable += new EventHandler<UpdateCheckEventArgs>(UpdateChecker_NoUpdateAvailable);
				_updateChecker.UpdateCheckFailed += new EventHandler<UpdateCheckEventArgs>(UpdateChecker_UpdateCheckFailed);
				_updateChecker.Check();
				c_checkForUpdatesButton.Enabled = false;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		void UpdateChecker_UpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { UpdateChecker_UpdateAvailable(sender, e); }));
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

		void UpdateChecker_NoUpdateAvailable(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { UpdateChecker_NoUpdateAvailable(sender, e); }));
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

		void UpdateChecker_UpdateCheckFailed(object sender, UpdateCheckEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => { UpdateChecker_UpdateCheckFailed(sender, e); }));
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
			c_checkForUpdatesButton.Enabled = true;
		}

		private void ImportRatingsButton_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new OpenFileDialog();
				dlg.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
				dlg.FilterIndex = 1;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					ImportRatings(dlg.FileName);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ExportRatingsButton_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new SaveFileDialog();
				dlg.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
				dlg.FilterIndex = 1;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					ExportRatings(dlg.FileName);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ExportRatings(string fileName)
		{
			using (var csv = new CsvWriter(fileName))
			{
				csv.WriteLine("Path", "Rating");

				using (var db = new Database())
				{
					using (var cmd = db.CreateCommand("select path, rating from img where ifnull(rating, 0) > 0 order by path"))
					{
						using (var rdr = cmd.ExecuteReader())
						{
							var ordPath = rdr.GetOrdinal("path");
							var ordRating = rdr.GetOrdinal("rating");

							while (rdr.Read())
							{
								var path = rdr.GetString(ordPath);
								var rating = rdr.GetInt32(ordRating);
								csv.WriteLine(path, rating.ToString());
							}
						}
					}
				}
			}

			MessageBox.Show(this, Res.RatingsExport_Success, Res.RatingsExport_Success_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ImportRatings(string fileName)
		{
			using (var csv = new CsvReader(fileName))
			{
				if (!csv.ReadHeaders())
				{
					this.ShowError(Res.RatingsImport_NoHeaderRow);
					return;
				}

				var pathCol = csv.GetHeaderIndex("path");
				if (pathCol < 0)
				{
					this.ShowError(Res.RatingsImport_NoPathColumn);
					return;
				}

				var ratingCol = csv.GetHeaderIndex("rating");
				if (ratingCol < 0)
				{
					this.ShowError(Res.RatingsImport_NoRatingColumn);
					return;
				}

				var numImagesUpdated = 0;
				var numHistoryItemsUpdated = 0;

				using (var db = new Database())
				{
					using (var tran = db.BeginTransaction())
					{
						while (csv.ReadLine())
						{
							var path = csv[pathCol];
							if (string.IsNullOrWhiteSpace(path)) continue;

							int rating;
							if (!int.TryParse(csv[ratingCol], out rating) || rating < 0 || rating > 5) continue;

							using (var cmd = db.CreateCommand("update img set rating = @rating where path = @path"))
							{
								cmd.Parameters.AddWithValue("@path", path);
								cmd.Parameters.AddWithValue("@rating", rating);
								numImagesUpdated += cmd.ExecuteNonQuery();
							}

							using (var cmd = db.CreateCommand("update history set rating = @rating where path = @path"))
							{
								cmd.Parameters.AddWithValue("@path", path);
								cmd.Parameters.AddWithValue("@rating", rating);
								numHistoryItemsUpdated += cmd.ExecuteNonQuery();
							}

							Global.UpdateRating(path, rating);
						}

						tran.Commit();
					}
				}

				MessageBox.Show(this, string.Format(Res.RatingsImport_Success, numImagesUpdated, numHistoryItemsUpdated),
					Res.RatingsImport_Success_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
