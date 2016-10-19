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
	public partial class HotKeySettings : Form
	{
		private HotKey _nextImageHotKey = new HotKey();
		private HotKey _prevImageHotKey = new HotKey();
		private HotKey _pauseHotKey = new HotKey();
		private HotKey _clearHistoryHotKey = new HotKey();
		private HotKey _showWindowHotKey = new HotKey();

		public HotKeySettings()
		{
			InitializeComponent();
		}

		private void HotKeySettings_Load(object sender, EventArgs e)
		{
			c_nextImageHotKey.HotKey = _nextImageHotKey;
			c_prevImageHotKey.HotKey = _prevImageHotKey;
			c_pauseHotKey.HotKey = _pauseHotKey;
			c_clearHistoryHotKey.HotKey = _clearHistoryHotKey;
			c_showWindowHotKey.HotKey = _showWindowHotKey;
		}

		public HotKey NextImageHotKey
		{
			get { return _nextImageHotKey; }
			set { _nextImageHotKey.Copy(value); }
		}

		public HotKey PrevImageHotKey
		{
			get { return _prevImageHotKey; }
			set { _prevImageHotKey.Copy(value); }
		}

		public HotKey PauseHotKey
		{
			get { return _pauseHotKey; }
			set { _pauseHotKey.Copy(value); }
		}

		public HotKey ClearHistoryHotKey
		{
			get { return _clearHistoryHotKey; }
			set { _clearHistoryHotKey.Copy(value); }
		}

		public HotKey ShowWindowHotKey
		{
			get { return _showWindowHotKey; }
			set { _showWindowHotKey.Copy(value); }
		}

		private void c_okButton_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_cancelButton_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult = DialogResult.Cancel;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
	}
}
