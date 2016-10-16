using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WallSwitch.ImageFilters
{
	[DisplayName("Path Name")]
	class PathNameCondition : FilterCondition
	{
		private static readonly string[] _compares = new string[]
		{
			"Contains",
			"Does Not Contain"
		};

		public override IEnumerable<string> ComparisonOptions
		{
			get { return _compares; }
		}

		public override Control CreateValueControl()
		{
			return new TextBox();
		}

		public override bool Validate(string comparison, Control valueControl, ref string error)
		{
			var ctrl = valueControl as TextBox;
			if (string.IsNullOrWhiteSpace(ctrl.Text))
			{
				error = "Field cannot be blank.";	// TODO: make resource
				return false;
			}

			return true;
		}

		public override void SaveXml(XmlWriter xml, string compare, Control valueControl)
		{
			xml.WriteAttributeString("Text", (valueControl as TextBox).Text);
		}

		public override Control LoadXml(XmlElement element, string compare)
		{
			return new TextBox() { Text = element.GetAttribute("Text") };
		}
	}
}
