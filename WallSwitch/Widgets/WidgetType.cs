using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using WallSwitch.WidgetInterface;

namespace WallSwitch
{
	internal class WidgetType
	{
		private Type _type;
		private string _fullName;
		private string _name;

		public WidgetType(Type type)
		{
			if (!typeof(IWidget).IsAssignableFrom(type)) throw new ArgumentException("Type must inherit from IWidget.");
			_type = type;
			_fullName = _type.FullName;

			_name = null;
			foreach (var attrib in _type.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>())
			{
				_name = attrib.DisplayName;
				break;
			}

			if (string.IsNullOrWhiteSpace(_name)) _name = type.Name;
		}

		public string Name
		{
			get { return _name; }
		}

		public Type Type
		{
			get { return _type; }
		}

		public string FullName
		{
			get { return _fullName; }
		}
	}
}
