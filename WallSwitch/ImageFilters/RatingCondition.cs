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
		private int _rating;

		public RatingCondition()
		{
		}

		private const string k_ge = "Greater Than or Equal To";
		private const string k_gt = "Greater Than";
		private const string k_eq = "Equals";
		private const string k_lt = "Less Than";
		private const string k_le = "Less Than or Equal To";

		private static readonly string[] _compares = new string[]
		{
			k_ge,
			k_gt,
			k_eq,
			k_lt,
			k_le
		};

		public override IEnumerable<string> ComparisonOptions
		{
			get { return _compares; }
		}

		public override Control CreateValueControl()
		{
			var ctrl = new RatingControl();
			ctrl.Rating = _rating;
			ctrl.RatingChanged += ValueControl_RatingChanged;
			return ctrl;
		}

		private void ValueControl_RatingChanged(object sender, EventArgs e)
		{
			var ctrl = sender as RatingControl;
			if (ctrl != null && ctrl.Rating != _rating)
			{
				_rating = ctrl.Rating;
				FireValueChanged();
			}
		}

		public override bool Validate(ref string error)
		{
			return true;
		}

		public override void SaveXml(XmlWriter xml)
		{
			if (ValueControl != null)
			{
				xml.WriteAttributeString("rating", (ValueControl as RatingControl).Rating.ToString());
			}
		}

		public override bool LoadXml(XmlElement element)
		{
			return int.TryParse(element.GetAttribute("rating"), out _rating);
		}

		public override string GenerateSqlWhere()
		{
			switch (Compare)
			{
				case k_ge:
					return "ifnull(rating, 0) >= " + _rating.ToString();
				case k_gt:
					return "ifnull(rating, 0) > " + _rating.ToString();
				case k_eq:
					return "ifnull(rating, 0) = " + _rating.ToString();
				case k_lt:
					return "ifnull(rating, 0) < " + _rating.ToString();
				case k_le:
					return "ifnull(rating, 0) <= " + _rating.ToString();
				default:
					return null;
			}
		}
	}
}
