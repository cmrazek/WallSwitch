using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WallSwitch
{
	class Settings
	{
		#region Constants
		private const bool k_defaultStartWithWindows = false;
		//private const bool k_defaultLogging = false;
		#endregion

		#region Variables
		private static bool _startWithWindows = k_defaultStartWithWindows;
		//private static bool _logging = false;
		#endregion

		/// <summary>
		/// Initializes all values when the program launches.
		/// </summary>
		public static void Initialize()
		{
			try
			{
				Log.Write(LogLevel.Debug, "Initializing settings.");
				GetStartWithWindowsFromRegistry();
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when initializing settings.");
			}
		}

		#region Save / Load
		public static void Save(XmlWriter xml)
		{
			//xml.WriteElementString("Logging", _logging.ToString());
		}

		public static void Load(XmlElement xmlSettings)
		{
			//XmlElement xmlLogging = (XmlElement)xmlSettings.SelectSingleNode("Logging");
			//if (xmlLogging != null) _logging = Util.ToBoolean(xmlLogging.InnerText, k_defaultLogging);
		}
		#endregion

		#region Start With Windows
		private static void GetStartWithWindowsFromRegistry()
		{
			RegistryKey key = null;
			try
			{
				_startWithWindows = false;

				string startValue = Application.ExecutablePath + " -winstart";

				key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
				string path = Convert.ToString(key.GetValue(Res.AppNameIdent));
				_startWithWindows = !string.IsNullOrEmpty(path) && path.ToLower() == WinStartCommand.ToLower();
			}
			catch(Exception ex)
			{
				Log.Write(ex, "Error when detecting 'start with windows' flag.");
			}
			finally
			{
				if (key != null) key.Close();
			}
		}

		/// <summary>
		/// Gets or sets the flag indicating if this application will start-up with Windows.
		/// </summary>
		public static bool StartWithWindows
		{
			get { return _startWithWindows; }
			set
			{
				RegistryKey key = null;
				try
				{
					if (_startWithWindows != value)
					{
						_startWithWindows = value;

						key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
						if (_startWithWindows)
						{
							Log.Write(LogLevel.Debug, "Enabling 'start with windows'.");
							key.SetValue(Res.AppNameIdent, WinStartCommand);
						}
						else
						{
							Log.Write(LogLevel.Debug, "Disabling 'start with windows'.");
							string[] values = key.GetValueNames();
							foreach (string val in values)
							{
								if (val == Res.AppNameIdent)
								{
									key.DeleteValue(Res.AppNameIdent);
									break;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when setting 'start with windows' flag.");
					throw ex;
				}
				finally
				{
					if (key != null) key.Close();
				}
			}
		}

		private static string WinStartCommand
		{
			get
			{
				return "\"" + Application.ExecutablePath + "\" -winstart";
			}
		}
		#endregion

	}
}
