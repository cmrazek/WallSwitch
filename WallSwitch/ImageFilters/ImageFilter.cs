using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WallSwitch.ImageFilters
{
	class ImageFilter
	{
		private List<FilterCondition> _conditions = new List<FilterCondition>();

		public IEnumerable<FilterCondition> Conditions
		{
			get { return _conditions; }
		}

		public void AddCondition(FilterCondition cond)
		{
			_conditions.Add(cond);
		}

		public string ToSaveString()
		{
			if (!_conditions.Any()) return null;

			var sb = new StringBuilder();
			using (var xml = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true }))
			{
				xml.WriteStartDocument();
				xml.WriteStartElement("filter");

				foreach (var cond in _conditions)
				{
					xml.WriteStartElement("cond");
					xml.WriteAttributeString("type", cond.GetType().FullName);
					xml.WriteAttributeString("op", cond.Operator.ToString());
					xml.WriteAttributeString("compare", cond.Compare);

					cond.SaveXml(xml);

					xml.WriteEndElement();
				}

				xml.WriteEndElement();
				xml.WriteEndDocument();
			}

			return sb.ToString();
		}

		public static ImageFilter FromSaveString(string saveStr)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(saveStr)) return null;

				var xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(saveStr);

				var filter = new ImageFilter();

				foreach (XmlElement xmlCond in xmlDoc.SelectNodes("/filter/cond"))
				{
					var typeFullName = xmlCond.GetAttribute("type");
					if (string.IsNullOrWhiteSpace(typeFullName)) continue;

					var compare = xmlCond.GetAttribute("compare");
					if (string.IsNullOrWhiteSpace(compare)) continue;

					var opString = xmlCond.GetAttribute("op");
					Operator op;
					if (!Enum.TryParse<Operator>(opString, true, out op)) continue;

					var type = Assembly.GetExecutingAssembly().GetType(typeFullName, false);
					if (type == null) continue;
					var cond = (FilterCondition)Activator.CreateInstance(type);
					cond.Compare = compare;
					cond.Operator = op;
					if (!cond.LoadXml(xmlCond)) continue;

					filter.AddCondition(cond);
				}

				if (!filter.Conditions.Any()) return null;
				return filter;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				return null;
			}
		}

		public string GenerateSqlWhere()
		{
			if (_conditions.Count == 0) return null;

			var index = 0;
			var sb = new StringBuilder();

			foreach (var cond in _conditions)
			{
				var str = cond.GenerateSqlWhere();
				if (string.IsNullOrWhiteSpace(str)) continue;

				if (index == 0)
				{
					sb.Append("((");
				}
				else
				{
					if (cond.Operator == Operator.And) sb.Append(") and (");
					else sb.Append(" or ");
				}

				sb.Append(str);

				index++;
			}

			if (sb.Length == 0) return null;
			sb.Append("))");
			return sb.ToString();
		}
	}
}
