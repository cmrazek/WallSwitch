using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch
{
	class HotKeyTextBox : TextBox
	{
		private HotKey _hotKey = null;

		public event EventHandler HotKeyChanged;

		public HotKeyTextBox()
		{
			this.ReadOnly = true;
			this.BackColor = SystemColors.Window;
		}

		public HotKey HotKey
		{
			get { return _hotKey; }
			set
			{
				if (_hotKey != value)
				{
					if (_hotKey != null) _hotKey.KeyComboChanged -= HotKey_KeyComboChanged;
					_hotKey = value;
					if (_hotKey != null) _hotKey.KeyComboChanged += HotKey_KeyComboChanged;

					SetHotKeyText();
					FireHotKeyChanged();
				}
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
					case Keys.Back:
					case Keys.Delete:
						if (_hotKey != null && _hotKey.IsValidKeyCombo)
						{
							_hotKey.Clear();
							SetHotKeyText();
							e.Handled = true;
							FireHotKeyChanged();
						}
						break;

					case Keys.Menu:
					case Keys.LMenu:
					case Keys.RMenu:
					case Keys.Shift:
					case Keys.ShiftKey:
					case Keys.LShiftKey:
					case Keys.RShiftKey:
					case Keys.Control:
					case Keys.ControlKey:
					case Keys.LControlKey:
					case Keys.RControlKey:
						break;

					default:
						if (_hotKey != null && _hotKey.Detect(e))
						{
							SetHotKeyText();
							e.Handled = true;
							FireHotKeyChanged();
						}
						break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex, Res.Exception_Generic);
			}
		}

		private void SetHotKeyText()
		{
			Text = _hotKey != null ? _hotKey.GetText(true) : string.Empty;
		}

		private void FireHotKeyChanged()
		{
			try
			{
				var ev = HotKeyChanged;
				if (ev != null) ev(this, new EventArgs());
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void HotKey_KeyComboChanged(object sender, EventArgs e)
		{
			SetHotKeyText();
			FireHotKeyChanged();
		}
	}
}
