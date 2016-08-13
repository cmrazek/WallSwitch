using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

namespace WallSwitch
{
	/// <summary>
	/// Holds information for a hot key.
	/// </summary>
	public class HotKey
	{
		#region Variables
		private Keys _key = Keys.None;
		private bool _control = false;
		private bool _alt = false;
		private bool _shift = false;

		private Form _form = null;
		private int _id = 0;
		private static int _lastId = 0;
		private bool _active = false;

		private static Dictionary<int, HotKey> _reg = new Dictionary<int, HotKey>();
		#endregion

		#region PInvoke
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int UnregisterHotKey(IntPtr hwnd, int id);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern uint GetLastError();

		[DllImport("user32.dll")]
		private static extern int MapVirtualKey(uint uCode, uint uMapType);

		private const int MOD_ALT = 0x0001;
		private const int MOD_CONTROL = 0x0002;
		private const int MOD_NOREPEAT = 0x4000;
		private const int MOD_SHIFT = 0x0004;
		private const int MOD_WIN = 0x0008;
		#endregion

		#region Events
		public event EventHandler KeyComboChanged;
		public event EventHandler HotKeyPressed;

		private void FireKeyComboChanged()
		{
			try
			{
				var ev = KeyComboChanged;
				if (ev != null) ev(this, new EventArgs());
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when firing KeyComboChanged event.");
			}
		}

		private void FireHotKeyPressed()
		{
			try
			{
				var ev = HotKeyPressed;
				if (ev != null) ev(this, new EventArgs());
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when firing HotKeyPressed event.");
			}
		}
		#endregion

		/// <summary>
		/// Clears out the hot key.
		/// </summary>
		public void Clear()
		{
			if (_key != Keys.None || _control || _alt || _shift)
			{
				_key = Keys.None;
				_control = _alt = _shift = false;

				OnKeyComboChanged();
			}
		}

		/// <summary>
		/// Copies the information from another hotkey object.
		/// </summary>
		/// <param name="k">The object to be copied.</param>
		public void Copy(HotKey k)
		{
			if (_key != k._key || _control != k._control || _alt != k._alt || _shift != k._shift)
			{
				_key = k._key;
				_control = k._control;
				_alt = k._alt;
				_shift = k._shift;

				OnKeyComboChanged();
			}
		}

		/// <summary>
		/// Determines if this hotkey has the same key configuration as another.
		/// </summary>
		/// <param name="obj">The other object to compare against</param>
		/// <returns>True if the same keys are present in both hotkeys; false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(HotKey)) return false;

			HotKey h = (HotKey)obj;
			return _key == h._key && _control == h._control && _alt == h._alt && _shift == h._shift;
		}

		/// <summary>
		/// Gets a hash code for this HotKey object.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public string GetText(bool friendly)
		{
			if (_key == Keys.None) return string.Empty;

			StringBuilder sb = new StringBuilder();

			if (_control)
			{
				if (sb.Length > 0) sb.Append(Res.HotKey_Delim);
				sb.Append(Res.HotKey_Control);
			}

			if (_shift)
			{
				if (sb.Length > 0) sb.Append(Res.HotKey_Delim);
				sb.Append(Res.HotKey_Shift);
			}

			if (_alt)
			{
				if (sb.Length > 0) sb.Append(Res.HotKey_Delim);
				sb.Append(Res.HotKey_Alt);
			}

			if (sb.Length > 0) sb.Append(Res.HotKey_Delim);
			sb.Append(GetFriendlyKeysDesc(_key));

			return sb.ToString();
		}

		/// <summary>
		/// Returns a string containing the text representation of the hot key.
		/// </summary>
		public override string ToString()
		{
			return GetText(false);
		}

		/// <summary>
		/// Gets a friendly representation of a virtual key code.
		/// </summary>
		/// <param name="key">The virtual key code.</param>
		/// <returns>A string that contains the friendly text.</returns>
		private string GetFriendlyKeysDesc(Keys key)
		{
			try
			{
				var charCode = MapVirtualKey((uint)_key, 2);
				var ch = Convert.ToChar(charCode);
				switch (ch)
				{
					case ' ':
						return Res.Char_Space;
					case '\t':
						return Res.Char_Tab;
					case '\r':
						return Res.Char_Cr;
					case '\n':
						return Res.Char_Lf;
					default:
						return ch.ToString();
				}
			}
			catch (Exception)
			{
				return key.ToString();
			}
		}

		/// <summary>
		/// Detects the hot key from a key down event.
		/// </summary>
		/// <param name="e">The event arguments from the key down event</param>
		/// <returns>True if a valid hot key was detected; false otherwise.</returns>
		public bool Detect(KeyEventArgs e)
		{
			// Only accept hot key if one or more modifier keys are pressed as well.
			if (!e.Control && !e.Alt && !e.Shift) return false;

			if (_key != e.KeyCode || _control != e.Control || _alt != e.Alt || _shift != e.Shift)
			{
				_key = e.KeyCode;
				_control = e.Control;
				_alt = e.Alt;
				_shift = e.Shift;

				OnKeyComboChanged();
			}

			return true;
		}

		/// <summary>
		/// Gets a flag indicating if a valid hot key combination was entered.
		/// </summary>
		public bool IsValidKeyCombo
		{
			get { return _key != Keys.None; }
		}

		/// <summary>
		/// Saves the hot key information to the theme element.
		/// </summary>
		/// <param name="xml"></param>
		public void SaveXml(XmlWriter xml, string name = "")
		{
			if (IsValidKeyCombo)
			{
				xml.WriteStartElement("HotKey");
				if (!string.IsNullOrEmpty(name)) xml.WriteAttributeString("Name", name);
				if (_control) xml.WriteAttributeString("Control", Boolean.TrueString);
				if (_alt) xml.WriteAttributeString("Alt", Boolean.TrueString);
				if (_shift) xml.WriteAttributeString("Shift", Boolean.TrueString);
				xml.WriteAttributeString("Key", _key.ToString());
				xml.WriteEndElement();	// HotKey
			}
		}

		/// <summary>
		/// Loads the hot key information from the theme element.
		/// </summary>
		/// <param name="xmlTheme"></param>
		public void LoadXml(XmlElement xmlHotKey)
		{
			if (xmlHotKey.HasAttribute("Control") && !Boolean.TryParse(xmlHotKey.GetAttribute("Control"), out _control))
				throw new XmlException("Invalid hotkey Control value.");

			if (xmlHotKey.HasAttribute("Alt") && !Boolean.TryParse(xmlHotKey.GetAttribute("Alt"), out _alt))
				throw new XmlException("Invalid hotkey Alt value.");

			if (xmlHotKey.HasAttribute("Shift") && !Boolean.TryParse(xmlHotKey.GetAttribute("Shift"), out _shift))
				throw new XmlException("Invalid hotkey Shift value.");

			if (xmlHotKey.HasAttribute("Key"))
			{
				if (!Enum.TryParse<Keys>(xmlHotKey.GetAttribute("Key"), out _key)) throw new XmlException("Invalid hotkey Key value.");
			}
			else
			{
				throw new XmlException("Hotkey does not have a 'Key' attribute.");
			}
		}

		/// <summary>
		/// Gets the name of the XML element created by SaveXml().
		/// </summary>
		public static string XmlElementName
		{
			get { return "HotKey"; }
		}

		/// <summary>
		/// Gets the name of the Name attribute created by SaveXml().
		/// </summary>
		public static string XmlNameAttribute
		{
			get { return "Name"; }
		}

		/// <summary>
		/// Registers the hotkey with Windows.
		/// </summary>
		/// <param name="form">The form that will receive the WM_HOTKEY message.</param>
		public void Register(Form form)
		{
			try
			{
				_active = true;
				_form = form;
				if (!IsValidKeyCombo) return;

				if (_id != 0)
				{
					Log.Write(LogLevel.Debug, "Reregistering hotkey: {0}", ToString());
					if (0 == UnregisterHotKey(_form.Handle, _id))
					{
						Log.Write(LogLevel.Warning, "Failed to unregister hotkey [{0}] ID [{1}] (GetLastError = {2}).", ToString(), _id, GetLastError());
					}
				}
				else
				{
					Log.Write(LogLevel.Debug, "Registering hotkey: {0}", ToString());
				}

				int id = ++_lastId;

				int mod = 0;
				if (_control) mod |= MOD_CONTROL;
				if (_alt) mod |= MOD_ALT;
				if (_shift) mod |= MOD_SHIFT;

				if (0 != RegisterHotKey(form.Handle, id, mod, (int)_key))
				{
					Log.Write(LogLevel.Debug, "Registered hotkey [{0}] with ID [{1}].", ToString(), id);
					_id = id;
					_reg[id] = this;
				}
				else
				{
					Log.Write(LogLevel.Debug, "Failed to register hotkey [{0}] (GetLastError = {1})", ToString(), GetLastError());
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when registering hotkey: " + ToString());
			}
		}

		/// <summary>
		/// Unregisters the hotkey with Windows.
		/// </summary>
		public void Unregister()
		{
			try
			{
				_active = false;
				if (_id == 0 || _form == null) return;

				Log.Write(LogLevel.Debug, "Unregistering hotkey [{0}] ID [{1}].", ToString(), _id);

				if (0 != UnregisterHotKey(_form.Handle, _id))
				{
					_form = null;
					_reg.Remove(_id);
					_id = 0;
				}
				else
				{
					Log.Write(LogLevel.Warning, "Failed to unregister hotkey [{0}] ID [{1}] (GetLastError = {2}).", ToString(), _id, GetLastError());
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when unregistering hotkey [{0}] ID [{1}].", ToString(), _id);
			}
		}

		/// <summary>
		/// Reregisters the hotkey with windows using the same ID as before.
		/// </summary>
		public void Reregister()
		{
			try
			{
				_active = true;
				if (_id != 0 && _form != null)
				{
					Log.Write(LogLevel.Debug, "Reregistering hotkey [{0}] ID [{1}].", ToString(), _id);

					int mod = 0;
					if (_control) mod |= MOD_CONTROL;
					if (_alt) mod |= MOD_ALT;
					if (_shift) mod |= MOD_SHIFT;

					if (0 != RegisterHotKey(_form.Handle, _id, mod, (int)_key))
					{
						Log.Write(LogLevel.Debug, "Successfully reregistered hotkey [{0}] with ID [{1}].", ToString(), _id);
						_reg[_id] = this;
					}
					else
					{
						Log.Write(LogLevel.Warning, "Failed to reregister hotkey [{0}] with ID [{1}] (GetLastError = {2}).", ToString(), _id, GetLastError());
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when reregistering hotkey [{0}] with ID [{1}].", ToString(), _id);
			}
		}

		/// <summary>
		/// Gets the unique ID for the hotkey event.
		/// </summary>
		public int ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Called internally when the key combination has changed.
		/// </summary>
		private void OnKeyComboChanged()
		{
			if (_active && _form != null) Register(_form);
			FireKeyComboChanged();
		}

		/// <summary>
		/// Handles WM_HOTKEY events.
		/// </summary>
		/// <param name="id">The hotkey ID retrieved from WM_HOTKEY.</param>
		/// <remarks>This function should be called by form class that detects WM_HOTKEY event messages.</remarks>
		public static void OnWmHotKey(int id)
		{
			HotKey hk;
			if (_reg.TryGetValue(id, out hk)) hk.FireHotKeyPressed();
		}

		public string ToSaveString()
		{
			var sb = new StringBuilder();

			if (_control) sb.Append("Ctrl");
			if (_shift)
			{
				if (sb.Length > 0) sb.Append('+');
				sb.Append("Shift");
			}
			if (_alt)
			{
				if (sb.Length > 0) sb.Append('+');
				sb.Append("Alt");
			}
			if (sb.Length > 0) sb.Append('+');
			sb.Append(_key.ToString());

			return sb.ToString();
		}

		private static Regex _rxSaveString = new Regex(@"^(Ctrl)?\+?(Shift)?\+?(Alt)?\+?(\w+)?$");

		public void LoadFromSaveString(string str)
		{
			if (string.IsNullOrEmpty(str)) return;

			var match = _rxSaveString.Match(str);
			if (match.Success)
			{
				if (match.Groups[1].Value == "Ctrl") _control = true;
				if (match.Groups[2].Value == "Shift") _shift = true;
				if (match.Groups[3].Value == "Alt") _alt = true;
				if (!string.IsNullOrEmpty(match.Groups[4].Value))
				{
					Keys key;
					if (Enum.TryParse<Keys>(match.Groups[4].Value, true, out key)) _key = key;
				}
			}
		}
	}
}
