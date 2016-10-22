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
		private string _compare;
		private Control _ctrl;
		private Operator _op;

		public abstract IEnumerable<string> ComparisonOptions { get; }

		public abstract Control CreateValueControl();

		public abstract bool Validate(ref string error);

		public abstract void SaveXml(XmlWriter xml);

		public abstract bool LoadXml(XmlElement element);

		public abstract string GenerateSqlWhere();

		public Operator Operator
		{
			get { return _op; }
			set { _op = value; }
		}

		public string Compare
		{
			get { return _compare; }
			set { _compare = value; }
		}

		public Control ValueControl
		{
			get { return _ctrl; }
			set { _ctrl = value; }
		}

		public event EventHandler ValueChanged;

		protected void FireValueChanged()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
