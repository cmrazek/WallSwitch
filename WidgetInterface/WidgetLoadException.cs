using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// Exception that occurs when a widget is unable to be loaded from the settings file.
	/// </summary>
	public class WidgetLoadException : Exception
	{
		/// <summary>
		/// Created the exception.
		/// </summary>
		/// <param name="message">A string describing the nature of the failure.</param>
		public WidgetLoadException(string message)
			: base(message)
		{ }
	}
}
