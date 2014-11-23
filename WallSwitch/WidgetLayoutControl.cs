﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WidgetInterface;

namespace WallSwitch
{
	internal partial class WidgetLayoutControl : UserControl
	{
		private float _displayScale;
		private PointF _displayOffset;
		private List<WidgetInstance> _widgets = new List<WidgetInstance>();
		private WidgetInstance _selectedWidget;
		private Point _lastScreenPt;
		private bool _dragging;
		private DragMethod _dragMethod;

		private const int k_designHandleDist = 2;

		public event EventHandler WidgetsChanged;

		public event EventHandler<SelectedWidgetChangedEventArgs> SelectedWidgetChanged;
		public class SelectedWidgetChangedEventArgs : EventArgs
		{
			public WidgetInstance Widget { get; private set; }

			public SelectedWidgetChangedEventArgs(WidgetInstance widget)
			{
				Widget = widget;
			}
		}

		private enum DragMethod
		{
			None,
			Move,
			ResizeTopLeft,
			ResizeTopRight,
			ResizeBottomLeft,
			ResizeBottomRight
		}

		public WidgetLayoutControl()
		{
			InitializeComponent();
		}

		private void WidgetLayoutControl_Load(object sender, EventArgs e)
		{
			try
			{
				UpdateLayout();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void UpdateLayout()
		{
			var clientRect = ClientRectangle;
			clientRect.Inflate(-2, -2);
			if (clientRect.Width <= 0 || clientRect.Height <= 0) return;

			var totalBounds = (RectangleF)GetTotalScreenArea();
			if (totalBounds.Width <= 0 || totalBounds.Height <= 0) return;
			var scaledBounds = totalBounds.ScaleRectWidth(clientRect.Width);
			if (scaledBounds.Height > clientRect.Height) scaledBounds = scaledBounds.ScaleRectHeight(clientRect.Height);
			scaledBounds = scaledBounds.CenterInside(clientRect);

			_displayScale = scaledBounds.Width / totalBounds.Width;
			_displayOffset = new PointF(scaledBounds.Left, scaledBounds.Top);

			foreach (var widget in _widgets)
			{
				widget.DesignBounds = ScreenToScaledClient(widget.Bounds);
			}
		}

		private void WidgetLayoutControl_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				var g = e.Graphics;
				g.FillRectangle(SystemBrushes.Window, e.ClipRectangle);

				var matrix = new System.Drawing.Drawing2D.Matrix();
				matrix.Translate(_displayOffset.X, _displayOffset.Y);
				matrix.Scale(_displayScale, _displayScale);
				g.Transform = matrix;

				foreach (var screen in System.Windows.Forms.Screen.AllScreens)
				{
					using (var brush = GraphicsUtil.CreateRadialGradientBrush(screen.Bounds, Color.Blue, Color.Navy))
					{
						g.FillRectangle(brush, screen.Bounds);
					}
				}

				foreach (var widget in _widgets)
				{
					var args = new WidgetDrawArgs(widget.Config, widget.Bounds, g, true);
					g.SetClip(widget.Bounds);
					widget.Draw(args);
					g.ResetClip();
				}

				foreach (var screen in System.Windows.Forms.Screen.AllScreens)
				{
					g.DrawRectangle(SystemPens.ControlDarkDark, screen.Bounds);
				}

				g.Transform = new System.Drawing.Drawing2D.Matrix();
				DrawSelectedWidgetBorder(g);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DrawSelectedWidgetBorder(Graphics g)
		{
			if (_selectedWidget == null) return;

			var bounds = _selectedWidget.DesignBounds;
			g.DrawRectangle(Pens.White, _selectedWidget.DesignBounds);

			if (!_selectedWidget.IsFixedSize)
			{
				g.FillRectangle(Brushes.LightGray, new Rectangle(bounds.TopLeft().Subtract(2, 2), new Size(4, 4)));
				g.FillRectangle(Brushes.LightGray, new Rectangle(bounds.TopRight().Subtract(2, 2), new Size(4, 4)));
				g.FillRectangle(Brushes.LightGray, new Rectangle(bounds.BottomLeft().Subtract(2, 2), new Size(4, 4)));
				g.FillRectangle(Brushes.LightGray, new Rectangle(bounds.BottomRight().Subtract(2, 2), new Size(4, 4)));
			}
		}

		private RectangleF GetTotalScreenArea()
		{
			float width = 0.0f, height = 0.0f;
			foreach (var screen in System.Windows.Forms.Screen.AllScreens)
			{
				if ((float)screen.Bounds.Right > width) width = (float)screen.Bounds.Right;
				if ((float)screen.Bounds.Bottom > height) height = (float)screen.Bounds.Bottom;
			}
			return new RectangleF(0.0f, 0.0f, width, height);
		}

		private void WidgetLayoutControl_Resize(object sender, EventArgs e)
		{
			try
			{
				UpdateLayout();
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		public void AddNewWidget(WidgetType wtype)
		{
			try
			{
				var widget = new WidgetInstance(wtype);
				widget.Bounds = widget.GetPreferredBounds();
				_widgets.Add(widget);
				Invalidate();
				OnWidgetsChanged(widget);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
				return;
			}
		}

		public void DeleteSelectedWidget()
		{
			if (_selectedWidget != null)
			{
				if (_widgets.Remove(_selectedWidget))
				{
					SelectWidget(null);
					OnWidgetsChanged(_selectedWidget);
				}
			}
		}

		public void SaveToTheme(Theme theme)
		{
			var str = SaveWidgetsToString(_widgets);
			var newWidgets = LoadWidgetsFromString(str);
			theme.ReplaceWidgets(newWidgets);
		}

		public void LoadFromTheme(Theme theme)
		{
			SelectWidget(null);

			var str = SaveWidgetsToString(theme.Widgets);
			var newWidgets = LoadWidgetsFromString(str);
			_widgets.Clear();
			_widgets.AddRange(newWidgets);
			Invalidate();
		}

		private string SaveWidgetsToString(IEnumerable<WidgetInstance> widgets)
		{
			var sb = new StringBuilder();
			using (var xmlWriter = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true }))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("Widgets");
				foreach (var widget in widgets) widget.Save(xmlWriter);
				xmlWriter.WriteEndElement();	// Widgets
				xmlWriter.WriteEndDocument();
			}
			return sb.ToString();
		}

		private IEnumerable<WidgetInstance> LoadWidgetsFromString(string str)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(str);

			foreach (var xmlWidget in xmlDoc.SelectNodes("/Widgets/" + WidgetInstance.XmlElementName).Cast<XmlElement>())
			{
				yield return WidgetInstance.Load(xmlWidget);
			}
		}

		private void OnWidgetsChanged(WidgetInstance changedWidget)
		{
			if (changedWidget != null)
			{
				changedWidget.DesignBounds = ScreenToScaledClient(changedWidget.Bounds);
			}

			var ev = WidgetsChanged;
			if (ev != null) ev(this, EventArgs.Empty);
		}

		private WidgetInstance HitTest(Point mousePt, out DragMethod dragMethod)
		{
			foreach (var widget in _widgets)
			{
				var dm = HitTest(widget, mousePt);
				if (dm != DragMethod.None)
				{
					dragMethod = dm;
					return widget;
				}
			}

			dragMethod = DragMethod.None;
			return null;
		}

		private DragMethod HitTest(WidgetInstance widget, Point mousePt)
		{
			var bounds = widget.DesignBounds;

			if (!widget.IsFixedSize)
			{
				if (bounds.TopLeft().IsCloseTo(mousePt, k_designHandleDist))
				{
					return DragMethod.ResizeTopLeft;
				}

				if (bounds.TopRight().IsCloseTo(mousePt, k_designHandleDist))
				{
					return DragMethod.ResizeTopRight;
				}

				if (bounds.BottomLeft().IsCloseTo(mousePt, k_designHandleDist))
				{
					return DragMethod.ResizeBottomLeft;
				}

				if (bounds.BottomRight().IsCloseTo(mousePt, k_designHandleDist))
				{
					return DragMethod.ResizeBottomRight;
				}
			}

			if (bounds.Contains(mousePt))
			{
				return DragMethod.Move;
			}

			return DragMethod.None;
		}

		private Point ScaledClientToScreen(Point pt)
		{
			if (_displayScale == 0) return pt;
			return new Point((int)(((float)pt.X - _displayOffset.X) / _displayScale), (int)(((float)pt.Y - _displayOffset.Y) / _displayScale));
		}

		private Rectangle ScreenToScaledClient(Rectangle rect)
		{
			return new Rectangle((int)(rect.Left * _displayScale + _displayOffset.X),
				(int)(rect.Top * _displayScale + _displayOffset.Y),
				(int)(rect.Width * _displayScale),
				(int)(rect.Height * _displayScale));
		}

		private void WidgetLayoutControl_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				DragMethod dragMethod;
				var widget = HitTest(e.Location, out dragMethod);
				if (widget != null)
				{
					SelectWidget(widget);
					_lastScreenPt = ScaledClientToScreen(e.Location);
					_dragging = false;
					Capture = true;
				}
				else
				{
					SelectWidget(null);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void WidgetLayoutControl_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if (_selectedWidget != null)
				{
					if (_dragging)
					{
						// User has finished dragging

						_dragging = false;
						Invalidate();
					}
					else
					{
						// User clicked to select
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void WidgetLayoutControl_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (_selectedWidget != null)
				{
					
					if ((e.Button & System.Windows.Forms.MouseButtons.Left) != 0)
					{
						var screenPt = ScaledClientToScreen(e.Location);
						var dist = screenPt.Subtract(_lastScreenPt);

						if (_dragMethod != DragMethod.None) _dragging = true;

						switch (_dragMethod)
						{
							case DragMethod.Move:
								if (_selectedWidget.OffsetBoundsSafe(dist))
								{
									Invalidate();
									OnWidgetsChanged(_selectedWidget);
								}
								break;
							case DragMethod.ResizeTopLeft:
								{
									var bounds = _selectedWidget.Bounds;
									if (_selectedWidget.ChangeBoundsSafe(new Rectangle(bounds.Left + dist.X, bounds.Top + dist.Y, bounds.Width - dist.X, bounds.Height - dist.Y)))
									{
										Invalidate();
										OnWidgetsChanged(_selectedWidget);
									}
								}
								break;
							case DragMethod.ResizeTopRight:
								{
									var bounds = _selectedWidget.Bounds;
									if (_selectedWidget.ChangeBoundsSafe(new Rectangle(bounds.Left, bounds.Top + dist.Y, bounds.Width + dist.X, bounds.Height - dist.Y)))
									{
										Invalidate();
										OnWidgetsChanged(_selectedWidget);
									}
								}
								break;
							case DragMethod.ResizeBottomLeft:
								{
									var bounds = _selectedWidget.Bounds;
									if (_selectedWidget.ChangeBoundsSafe(new Rectangle(bounds.Left + dist.X, bounds.Top, bounds.Width - dist.X, bounds.Height + dist.Y)))
									{
										Invalidate();
										OnWidgetsChanged(_selectedWidget);
									}
								}
								break;
							case DragMethod.ResizeBottomRight:
								{
									var bounds = _selectedWidget.Bounds;
									if (_selectedWidget.ChangeBoundsSafe(new Rectangle(bounds.Left, bounds.Top, bounds.Width + dist.X, bounds.Height + dist.Y)))
									{
										Invalidate();
										OnWidgetsChanged(_selectedWidget);
									}
								}
								break;
						}

						_lastScreenPt = screenPt;
					}
					else if (e.Button == System.Windows.Forms.MouseButtons.None)
					{
						_dragMethod = HitTest(_selectedWidget, e.Location);

						switch (_dragMethod)
						{
							case DragMethod.Move:
								this.Cursor = Cursors.SizeAll;
								break;
							case DragMethod.ResizeTopLeft:
							case DragMethod.ResizeBottomRight:
								this.Cursor = Cursors.SizeNWSE;
								break;
							case DragMethod.ResizeBottomLeft:
							case DragMethod.ResizeTopRight:
								this.Cursor = Cursors.SizeNESW;
								break;
							default:
								this.Cursor = Cursors.Default;
								break;
						}
					}
					else
					{
						this.Cursor = Cursors.Default;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SelectWidget(WidgetInstance widget)
		{
			if (_selectedWidget != widget)
			{
				_selectedWidget = widget;

				Invalidate();

				var ev = SelectedWidgetChanged;
				if (ev != null) ev(this, new SelectedWidgetChangedEventArgs(_selectedWidget));
			}
		}

		public WidgetInstance SelectedWidget
		{
			get { return _selectedWidget; }
		}
	}
}
