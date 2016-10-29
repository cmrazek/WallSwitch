using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallSwitch.SettingsStore
{
	/// <summary>
	/// An exception caused by an error in the settings file.
	/// </summary>
	class SettingsException : Exception
	{
		/// <summary>
		/// Creates the exception.
		/// </summary>
		/// <param name="message">A message explaining the reason for the exception.</param>
		public SettingsException(string message)
			: base(message)
		{
		}
	}
}
