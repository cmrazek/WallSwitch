using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// An instance of a single setting name and value.
	/// </summary>
	public sealed class WidgetConfigItem
	{
		private string _name;
		private string _value;

		/// <summary>
		/// Creates a new widget configuration item.
		/// </summary>
		/// <param name="name">Item name</param>
		/// <param name="value">Item value</param>
		public WidgetConfigItem(string name, string value)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			_name = name;
			_value = value;
		}

		/// <summary>
		/// Gets the name of this configuration item.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets or sets the value for this configuration item.
		/// </summary>
		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}
	}
}
