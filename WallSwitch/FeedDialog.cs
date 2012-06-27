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

				var str = _feed.UpdatePeriod.ToString();
				cmbPeriod.SelectedItem = (from i in cmbPeriod.Items.Cast<string>() where i == str select i).FirstOrDefault();
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
			var url = txtUrl.Text;
			if (string.IsNullOrWhiteSpace(url))
			{
				txtUrl.Focus();
				this.ShowError(Res.Error_InvalidFeedUrl);
				return false;
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

			_feed.Path = url;
			_feed.SetUpdateInterval(freq, period);
			return true;
		}

		public Location Feed
		{
			get { return _feed; }
			set { _feed = value; }
		}
	}
}
