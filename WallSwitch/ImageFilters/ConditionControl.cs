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
	partial class ConditionControl : UserControl
	{
		private static List<FilterConditionType> _globalCondTypes;

		private FilterCondition _cond;
		private int _index;
		private bool _suppressControls;

		public event EventHandler DataChanged;

		public ConditionControl()
		{
			InitializeComponent();
		}

		public ConditionControl(FilterCondition cond)
		{
			if (cond == null) throw new ArgumentNullException(nameof(cond));
			_cond = cond;
			_cond.ValueChanged += Condition_ValueChanged;

			InitializeComponent();
		}

		private void ConditionControl_Load(object sender, EventArgs e)
		{
			try
			{
				_suppressControls = true;

				c_operatorCombo.SelectedIndex = 0;

				// Refresh the 'type' combo
				var selIndex = -1;
				foreach (var type in GlobalConditionTypes)
				{
					if (_cond != null && _cond.GetType() == type.Type) selIndex = c_condTypeCombo.Items.Count;
					c_condTypeCombo.Items.Add(type);
				}
				if (selIndex >= 0) c_condTypeCombo.SelectedIndex = selIndex;

				RefreshCompareCombo();
				RefreshValueControl();

				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
			finally
			{
				_suppressControls = false;
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

		public FilterCondition Condition
		{
			get { return _cond; }
		}

		private void CondTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (_suppressControls) return;

				var condType = c_condTypeCombo.SelectedItem as FilterConditionType;
				if (condType == null)
				{
					if (_cond != null)
					{
						_cond.ValueChanged -= Condition_ValueChanged;
					}

					_cond = null;
					c_condCompareCombo.Items.Clear();
					c_condValuePanel.Controls.Clear();
				}
				else
				{
					if (_cond != null)
					{
						_cond.ValueChanged -= Condition_ValueChanged;
					}

					_cond = (FilterCondition)Activator.CreateInstance(condType.Type);
					_cond.ValueChanged += Condition_ValueChanged;

					RefreshCompareCombo();
					RefreshValueControl();
				}

				DataChanged?.Invoke(this, EventArgs.Empty);
				EnableControls();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RefreshCompareCombo()
		{
			c_condCompareCombo.Items.Clear();

			if (_cond != null)
			{
				var index = 0;
				var selIndex = -1;
				foreach (var opt in _cond.ComparisonOptions)
				{
					c_condCompareCombo.Items.Add(opt);
					if (opt == _cond.Compare) selIndex = index;
					index++;
				}

				if (selIndex >= 0) c_condCompareCombo.SelectedIndex = selIndex;
				else if (index > 0) c_condCompareCombo.SelectedIndex = 0;
			}
		}

		private void RefreshValueControl()
		{
			c_condValuePanel.Controls.Clear();

			if (_cond != null)
			{
				var ctrl = _cond.CreateValueControl();
				_cond.ValueControl = ctrl;
				ctrl.Dock = DockStyle.Fill;
				c_condValuePanel.Controls.Add(ctrl);
			}
		}

		private void RefreshOperatorCombo()
		{
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			try
			{
				var parent = Parent as FlowLayoutPanel;
				if (parent != null)
				{
					var ctrl = new ConditionControl();
					parent.Controls.InsertAfter(this, ctrl);
					MainWindow.Current.OnFilterControlAdded(ctrl);
					MainWindow.Current.OnFiltersChanged();
					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				var parent = Parent as FlowLayoutPanel;
				if (parent != null)
				{
					parent.Controls.Remove(this);
					DataChanged?.Invoke(this, EventArgs.Empty);
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

		private void CondCompareCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (_suppressControls) return;

				if (_cond != null)
				{
					_cond.Compare = c_condCompareCombo.SelectedItem as string;
				}

				DataChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void Condition_ValueChanged(object sender, EventArgs e)
		{
			if (_suppressControls) return;

			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => Condition_ValueChanged(sender, e)));
				return;
			}

			try
			{
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void OperatorCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (_suppressControls) return;

				if (_cond != null)
				{
					Operator op;
					if (Enum.TryParse((c_operatorCombo.SelectedItem as string), true, out op))
					{
						_cond.Operator = op;
						DataChanged?.Invoke(this, EventArgs.Empty);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
	}
}
