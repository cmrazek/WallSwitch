using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WallSwitch
{
	partial class FeedDialog : Form
	{
		private Location _feed = null;

		public FeedDialog(Location feed)
		{
			if (feed == null) throw new ArgumentNullException("feed");
			_feed = feed;

			InitializeComponent();
		}

		private void FeedDialog_Load(object sender, EventArgs e)
		{
			try
			{
				txtUrl.Text = _feed.Path;
				txtFreq.Text = _feed.UpdateFrequency.ToString();
				txtType.Text = _feed.Type.ToString();

				var str = _feed.UpdatePeriod.ToString();
				cmbPeriod.SelectedItem = (from i in cmbPeriod.Items.Cast<string>() where i == str select i).FirstOrDefault();

				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidateAndSaveForm())
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

		private bool ValidateAndSaveForm()
		{
			var path = txtUrl.Text;
			switch (_feed.Type)
			{
				case LocationType.File:
					if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
					{
						txtUrl.Focus();
						this.ShowError(Res.Error_InvalidFeedUrl);
						return false;
					}
					break;

				case LocationType.Directory:
					if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
					{
						txtUrl.Focus();
						this.ShowError(Res.Error_InvalidFeedUrl);
						return false;
					}
					break;

				case LocationType.Feed:
					if (string.IsNullOrWhiteSpace(path))
					{
						txtUrl.Focus();
						this.ShowError(Res.Error_InvalidFeedUrl);
						return false;
					}
					break;
			}

			int freq;
			if (!Int32.TryParse(txtFreq.Text, out freq) || freq <= 0)
			{
				txtFreq.Focus();
				this.ShowError(Res.Error_InvalidFeedUpdateFreq);
				return false;
			}

			Period period;
			if (!Enum.TryParse<Period>(cmbPeriod.SelectedItem as string, out period))
			{
				cmbPeriod.Focus();
				this.ShowError(Res.Error_InvalidFeedUpdatePeriod);
				return false;
			}

			_feed.Path = path;
			_feed.SetUpdateInterval(freq, period);
			return true;
		}

		public Location Feed
		{
			get { return _feed; }
			set { _feed = value; }
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				switch (_feed.Type)
				{
					case LocationType.File:
						{
							var dlg = new OpenFileDialog();
							dlg.Filter = ImageFormatDesc.ImageFileFilter;
							dlg.FileName = txtUrl.Text;
							if (dlg.ShowDialog(this) == DialogResult.OK)
							{
								txtUrl.Text = dlg.FileName;
								EnableControls();
							}
						}
						break;

					case LocationType.Directory:
						{
							var dlg = new FolderBrowserDialog();
							dlg.SelectedPath = txtUrl.Text;
							if (dlg.ShowDialog(this) == DialogResult.OK)
							{
								txtUrl.Text = dlg.SelectedPath;
								EnableControls();
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void EnableControls()
		{
			btnBrowse.Enabled = _feed.Type == LocationType.File || _feed.Type == LocationType.Directory;
		}

		private void txtUrl_TextChanged(object sender, EventArgs e)
		{
			try
			{
				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void txtFreq_TextChanged(object sender, EventArgs e)
		{
			try
			{
				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
	}
}
