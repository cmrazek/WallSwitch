using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WallSwitch.ImageFilters
{
	abstract class FilterCondition
	{
		public abstract IEnumerable<string> ComparisonOptions { get; }

		public abstract Control CreateValueControl();

		public abstract bool Validate(string comparison, Control valueControl, ref string error);

		public abstract void SaveXml(XmlWriter xml, string compare, Control valueControl);

		public abstract Control LoadXml(XmlElement element, string compare);
	}
}
