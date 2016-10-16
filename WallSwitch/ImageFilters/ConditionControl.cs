using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WallSwitch.ImageFilters
{
	public partial class ConditionControl : UserControl
	{
		private static List<FilterConditionType> _globalCondTypes;

		private FilterCondition _cond;
		private int _index;
		private bool _suppressControls;

		public ConditionControl()
		{
			InitializeComponent();
		}

		private void ConditionControl_Load(object sender, EventArgs e)
		{
			try
			{
				c_operatorCombo.SelectedIndex = 0;

				foreach (var type in GlobalConditionTypes)
				{
					c_condTypeCombo.Items.Add(type);
				}

				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private IEnumerable<FilterConditionType> GlobalConditionTypes
		{
			get
			{
				if (_globalCondTypes == null)
				{
					_globalCondTypes = new List<FilterConditionType>();

					foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
					{
						if (!type.IsAbstract && type.IsSubclassOf(typeof(FilterCondition)))
						{
							_globalCondTypes.Add(new FilterConditionType(type));
						}
					}
				}

				return _globalCondTypes;
			}
		}

		private void CondTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (_suppressControls) return;

				var condType = c_condTypeCombo.SelectedItem as FilterConditionType;
				if (condType == null)
				{
					_cond = null;
					c_condCompareCombo.Items.Clear();
					c_condValuePanel.Controls.Clear();
				}
				else
				{
					_cond = (FilterCondition)Activator.CreateInstance(condType.Type);

					c_condCompareCombo.Items.Clear();
					var count = 0;
					foreach (var opt in _cond.ComparisonOptions)
					{
						c_condCompareCombo.Items.Add(opt);
						count++;
					}
					if (count > 0) c_condCompareCombo.SelectedIndex = 0;

					c_condValuePanel.Controls.Clear();
					var ctrl = _cond.CreateValueControl();
					ctrl.Dock = DockStyle.Fill;
					c_condValuePanel.Controls.Add(ctrl);
				}

				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_addButton_Click(object sender, EventArgs e)
		{
			try
			{
				var parent = Parent as FlowLayoutPanel;
				if (parent != null)
				{
					var ctrl = new ConditionControl();
					parent.Controls.InsertAfter(this, ctrl);
					MainWindow.Current.OnFiltersChanged();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_deleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				var parent = Parent as FlowLayoutPanel;
				if (parent != null)
				{
					if (CountConditions() > 1)
					{
						parent.Controls.Remove(this);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		public int Index
		{
			get { return _index; }
			set
			{
				if (_index != value)
				{
					_index = value;
					EnableControls();
				}
			}
		}

		public void EnableControls()
		{
			c_operatorCombo.Visible = _index > 0;
			c_condCompareCombo.Visible = c_condTypeCombo.SelectedIndex >= 0;
			c_condValuePanel.Visible = c_condCompareCombo.Visible;
			c_deleteButton.Visible = CountConditions() > 1;
		}

		private int CountConditions()
		{
			var parent = Parent as FlowLayoutPanel;
			if (parent != null)
			{
				return (from c in parent.Controls.Cast<Control>() where c is ConditionControl select c).Count();
			}
			return 0;
		}

		public bool SaveXml(XmlWriter xml, bool showErrors, TabPage tabPage)
		{
			if (_cond == null)
			{
				if (showErrors)
				{
					tabPage?.Select();
					c_condTypeCombo.Focus();
					this.ShowError("Attribute cannot be blank.");	// TODO: make resource
				}
				return false;
			}

			var compare = c_condCompareCombo.SelectedItem as string;
			if (compare == null)
			{
				if (showErrors)
				{
					tabPage?.Select();
					c_condCompareCombo.Focus();
					this.ShowError("Comparison cannot be blank.");	// TODO: make resource
				}
				return false;
			}

			var valueCtrl = c_condValuePanel.Controls.Cast<Control>().FirstOrDefault();
			var error = string.Empty;
			if (_cond.Validate(compare, valueCtrl, ref error))
			{
				if (showErrors)
				{
					tabPage?.Select();
					valueCtrl?.Focus();
					if (!string.IsNullOrEmpty(error)) this.ShowError(error);
					else this.ShowError("Invalid value.");	// TODO: make resource
				}
				return false;
			}

			xml.WriteAttributeString("Type", _cond.GetType().FullName);
			xml.WriteAttributeString("Compare", compare);
			_cond.SaveXml(xml, compare, valueCtrl);

			return true;
		}

		public void LoadXml(XmlElement element)
		{
			var typeFullName = element.GetAttribute("Type");
			if (string.IsNullOrEmpty(typeFullName)) return;

			var compare = element.GetAttribute("Compare");
			if (string.IsNullOrEmpty(compare)) return;

			var condType = (from g in GlobalConditionTypes where g.Type.FullName == typeFullName select g).FirstOrDefault();
			if (condType == null) return;

			_suppressControls = true;
			try
			{
				c_condTypeCombo.SelectedItem = condType;

				_cond = (FilterCondition)Activator.CreateInstance(condType.Type);

				var valueControl = _cond.LoadXml(element, compare);

				c_condCompareCombo.Items.Clear();
				string selItem = null;
				foreach (var opt in _cond.ComparisonOptions)
				{
					c_condCompareCombo.Items.Add(opt);
					if (opt == compare) selItem = opt;
				}
				c_condCompareCombo.SelectedItem = selItem;

				c_condValuePanel.Controls.Clear();
				if (valueControl != null)
				{
					valueControl.Dock = DockStyle.Fill;
					c_condValuePanel.Controls.Add(valueControl);
				}
			}
			finally
			{
				_suppressControls = false;
			}
		}

		// TODO: When fields have changed, mark the theme as dirty
	}
}
