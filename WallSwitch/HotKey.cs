using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

namespace WallSwitch
{
	/// <summary>
	/// Holds information for a hot key.
	/// </summary>
	class HotKey
	{
		#region Variables
		private Keys _key = Keys.None;
		private bool _control = false;
		private bool _alt = false;
		private bool _shift = false;

		private Form _form = null;
		private int _id = 0;
		private static int _lastId = 0;
		#endregion

		#region PInvoke
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int UnregisterHotKey(IntPtr hwnd, int id);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern uint GetLastError();

		private const int MOD_ALT = 0x0001;
		private const int MOD_CONTROL = 0x0002;
		private const int MOD_NOREPEAT = 0x4000;
		private const int MOD_SHIFT = 0x0004;
		private const int MOD_WIN = 0x0008;
		#endregion

		/// <summary>
		/// Clears out the hot key.
		/// </summary>
		public void Clear()
		{
			_key = Keys.None;
			_control = _alt = _shift = false;
		}

		/// <summary>
		/// Copies the information from another hotkey object.
		/// </summary>
		/// <param name="k">The object to be copied.</param>
		public void Copy(HotKey k)
		{
			_key = k._key;
			_control = k._control;
			_alt = k._alt;
			_shift = k._shift;
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

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Returns a string containing the text representation of the hot key.
		/// </summary>
		public override string ToString()
		{
			if (_key == Keys.None) return "";

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
			sb.Append(_key.ToString());

			return sb.ToString();
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

			_key = e.KeyCode;
			_control = e.Control;
			_alt = e.Alt;
			_shift = e.Shift;

			return true;
		}

		/// <summary>
		/// Gets a flag indicating if a valid hot key combination was entered.
		/// </summary>
		public bool IsEnabled
		{
			get { return _key != Keys.None; }
		}

		/// <summary>
		/// Saves the hot key information to the theme element.
		/// </summary>
		/// <param name="xml"></param>
		public void SaveTheme(XmlWriter xml)
		{
			if (IsEnabled)
			{
				xml.WriteStartElement("HotKey");
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
		public void LoadTheme(XmlElement xmlTheme)
		{
			XmlElement xml = (XmlElement)xmlTheme.SelectSingleNode("HotKey");
			if (xml != null)
			{
				if (xml.HasAttribute("Control") && !Boolean.TryParse(xml.GetAttribute("Control"), out _control))
					throw new XmlException("Invalid hotkey Control value.");

				if (xml.HasAttribute("Alt") && !Boolean.TryParse(xml.GetAttribute("Alt"), out _alt))
					throw new XmlException("Invalid hotkey Alt value.");

				if (xml.HasAttribute("Shift") && !Boolean.TryParse(xml.GetAttribute("Shift"), out _shift))
					throw new XmlException("Invalid hotkey Shift value.");

				if (xml.HasAttribute("Key"))
				{
					if (!Enum.TryParse<Keys>(xml.GetAttribute("Key"), out _key)) throw new XmlException("Invalid hotkey Key value.");
				}
				else
				{
					throw new XmlException("Hotkey does not have a 'Key' attribute.");
				}
			}
		}

		/// <summary>
		/// Registers the hotkey with Windows.
		/// </summary>
		/// <param name="form">The form that will receive the WM_HOTKEY message.</param>
		public void Register(Form form)
		{
			try
			{
				if (!IsEnabled) return;

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
					_form = form;
					_id = id;
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
				if (_id == 0) return;

				Log.Write(LogLevel.Debug, "Unregistering hotkey [{0}] ID [{1}].", ToString(), _id);

				if (0 != UnregisterHotKey(_form.Handle, _id))
				{
					_form = null;
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
				if (_id != 0)
				{
					Log.Write(LogLevel.Debug, "Reregistering hotkey [{0}] ID [{1}].", ToString(), _id);

					int mod = 0;
					if (_control) mod |= MOD_CONTROL;
					if (_alt) mod |= MOD_ALT;
					if (_shift) mod |= MOD_SHIFT;

					if (0 != RegisterHotKey(_form.Handle, _id, mod, (int)_key))
					{
						Log.Write(LogLevel.Debug, "Successfully reregistered hotkey [{0}] with ID [{1}].", ToString(), _id);
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
	}
}
