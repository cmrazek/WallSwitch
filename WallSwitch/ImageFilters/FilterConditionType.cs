using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WallSwitch.ImageFilters
{
	class FilterConditionType
	{
		private Type _type;
		private string _name;

		public FilterConditionType(Type type)
		{
			_type = type;
		}

		public override string ToString()
		{
			return Name;
		}

		public Type Type
		{
			get { return _type; }
		}

		public string Name
		{
			get
			{
				if (_name == null)
				{
					var displayNameAttrib = _type.GetCustomAttributes(false).Where(x => x is DisplayNameAttribute).Cast<DisplayNameAttribute>().FirstOrDefault();
					if (displayNameAttrib != null) _name = displayNameAttrib.DisplayName;
					else _name = _type.Name;
				}
				return _name;
			}
		}
	}
}
