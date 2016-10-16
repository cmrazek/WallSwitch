using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WallSwitch.ImageFilters
{
	[DisplayName("Rating")]
	class RatingCondition : FilterCondition
	{
		private static readonly string[] _compares = new string[]
		{
			"Greater Than or Equal To",
			"Greater Than",
			"Equals",
			"Less Than",
			"Less Than or Equal To"
		};

		public override IEnumerable<string> ComparisonOptions
		{
			get { return _compares; }
		}

		public override Control CreateValueControl()
		{
			return new RatingControl();
		}

		public override bool Validate(string comparison, Control valueControl, ref string error)
		{
			return true;
		}

		public override void SaveXml(XmlWriter xml, string compare, Control valueControl)
		{
			xml.WriteAttributeString("Rating", (valueControl as RatingControl).Rating.ToString());
		}

		public override Control LoadXml(XmlElement element, string compare)
		{
			return new RatingControl() { Rating = int.Parse(element.GetAttribute("Rating")) };
		}
	}
}
