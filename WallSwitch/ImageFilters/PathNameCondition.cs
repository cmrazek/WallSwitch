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
		private string _text = string.Empty;

		private const string k_contains = "Contains";
		private const string k_doesNotContain = "Does Not Contain";

		private static readonly string[] _compares = new string[]
		{
			k_contains,
			k_doesNotContain
		};

		public override IEnumerable<string> ComparisonOptions
		{
			get { return _compares; }
		}

		public override Control CreateValueControl()
		{
			var ctrl = new TextBox();
			if (!string.IsNullOrEmpty(_text)) ctrl.Text = _text;
			ctrl.TextChanged += ValueControl_TextChanged;
			return ctrl;
		}

		private void ValueControl_TextChanged(object sender, EventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox != null)
			{
				var text = textBox.Text;
				if (text != _text)
				{
					_text = text;
					FireValueChanged();
				}
			}
		}

		public override bool Validate(ref string error)
		{
			if (string.IsNullOrWhiteSpace(_text))
			{
				error = Res.Error_BlankField;
				return false;
			}

			return true;
		}

		public override void SaveXml(XmlWriter xml)
		{
			xml.WriteAttributeString("text", _text);
		}

		public override bool LoadXml(XmlElement element)
		{
			_text = element.GetAttribute("text");
			return !string.IsNullOrEmpty(_text);
		}

		private string EscapeSqlLikeString(string str)
		{
			if (str.IndexOf('%') < 0 && str.IndexOf('_') < 0)
			{
				return string.Concat("'%", str.Replace("'", "''"), "%'");
			}

			var sb = new StringBuilder();
			sb.Append("'%");
			foreach (var ch in str)
			{
				switch (ch)
				{
					case '%':
						sb.Append(@"\%");
						break;
					case '_':
						sb.Append(@"\_");
						break;
					case '\\':
						sb.Append(@"\\");
						break;
					default:
						sb.Append(ch);
						break;
				}
			}
			sb.Append(@"%' escape '\'");
			return sb.ToString();
		}

		public override string GenerateSqlWhere()
		{
			if (string.IsNullOrEmpty(_text)) return null;

			switch (Compare)
			{
				case k_contains:
					return string.Concat("img.path like ", EscapeSqlLikeString(_text));
				case k_doesNotContain:
					return string.Concat("img.path not like ", EscapeSqlLikeString(_text));
				default:
					return null;
			}
		}
	}
}
