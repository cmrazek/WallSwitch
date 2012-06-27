using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch
{
	partial class HistoryList : UserControl
	{
		#region Constants
		private const int k_imageWidth = 100;
		private const int k_imageHeight = 100;
		private const int k_spacer = 4;
		private const float k_mouseWheelScale = .4f;
		private const int k_maxHistory = 10;
		#endregion

		#region Member Variables
		private List<HistoryItem> _items = new List<HistoryItem>();
		private int _scroll = 0;
		private HistoryItem _selectedItem = null;
		private int _maxHistory = k_maxHistory;
		#endregion

		#region Internal Classes
		private class HistoryItem
		{
			public Bitmap image;
			public ImageRec imageRec;
			public Rectangle bounds;
			public RectangleF imageRect;
		}
		#endregion

		#region Construction
		public HistoryList()
		{
			InitializeComponent();

			this.MouseWheel += new MouseEventHandler(HistoryList_MouseWheel);
		}
		#endregion

		#region Item Manipulation
		public void AddHistory(ImageRec img)
		{
			var imageRect = new RectangleF(new PointF(0.0f, 0.0f), img.Image.Size);
			if (imageRect.Width > k_imageWidth) imageRect = Util.ScaleRectWidth(imageRect, k_imageWidth);
			if (imageRect.Height > k_imageHeight) imageRect = Util.ScaleRectHeight(imageRect, k_imageHeight);

			Bitmap bmp = null;
			if (img != null) bmp = new Bitmap(img.Image, (int)imageRect.Width, (int)imageRect.Height);

			_items.Insert(0, new HistoryItem
			{
				image = bmp,
				imageRec = img,
				imageRect = imageRect
			});

			while (_items.Count > _maxHistory)
			{
				_items.RemoveAt(_items.Count - 1);
			}

			UpdateLayout();
			Invalidate();
		}
		#endregion

		#region Layout / Drawing
		private void UpdateLayout()
		{
			Size clientSize = ClientSize;
			int currentX = k_spacer;
			int currentY = k_spacer;

			int widthRequired = k_imageWidth + k_spacer;
			int heightRequired = k_imageHeight + k_spacer;

			foreach (var item in _items)
			{
				if (currentX > k_spacer && currentX + widthRequired > clientSize.Width)
				{
					currentX = k_spacer;
					currentY += heightRequired;
				}

				item.bounds = new Rectangle(currentX, currentY, k_imageWidth, k_imageHeight);
				currentX += widthRequired;
			}
			currentY += heightRequired;

			int scrollMax = currentY - clientSize.Height;
			if (scrollMax < 0)
			{
				vScroll.Value = 0;
				vScroll.Maximum = 0;
				vScroll.Enabled = false;
			}
			else
			{
				if (vScroll.Value > scrollMax) vScroll.Value = scrollMax;
				vScroll.Maximum = scrollMax;
				vScroll.Enabled = true;
			}
		}

		private void HistoryList_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				foreach (var item in _items)
				{
					if (item.bounds.IntersectsWith(e.ClipRectangle))
					{
						DrawItem(item, e.Graphics);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DrawItem(HistoryItem item, Graphics g)
		{
			try
			{
				if (object.Equals(item, _selectedItem))
				{
					var selRect = item.bounds;
					selRect.Inflate(k_spacer / 2, k_spacer / 2);
					selRect.Offset(0, -_scroll);
					g.FillRectangle(SystemBrushes.Highlight, selRect);
				}
				else
				{
					var shadeRect = item.bounds;
					shadeRect.Offset(0, -_scroll);
					g.FillRectangle(SystemBrushes.ControlLight, shadeRect);
				}

				var image = item.image;
				if (image != null && image.Width > 0 && image.Height > 0)
				{
					var imageRect = new Rectangle(item.bounds.X, item.bounds.Y, (int)item.imageRect.Width, (int)item.imageRect.Height);
					imageRect.Offset((k_imageWidth - imageRect.Width) / 2, (k_imageHeight - imageRect.Height) / 2);
					imageRect.Offset(0, -_scroll);
					g.DrawImage(image, imageRect);
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when drawing history item.");
			}
		}

		private void vScroll_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				_scroll = vScroll.Value;
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void HistoryList_Resize(object sender, EventArgs e)
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

		void HistoryList_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				_scroll -= (int)(e.Delta * k_mouseWheelScale);
				if (_scroll < 0) _scroll = 0;
				else if (_scroll > vScroll.Maximum) _scroll = vScroll.Maximum;

				vScroll.Value = _scroll;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Selection
		private HistoryItem HitTest(Point pt)
		{
			pt = new Point(pt.X, pt.Y + _scroll);
			foreach (var item in _items)
			{
				if (item.bounds.Contains(pt)) return item;
			}
			return null;
		}

		private void HistoryList_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
				{
					SelectItemUnderMouse(e.Location);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SelectItemUnderMouse(Point pt)
		{
			var item = HitTest(pt);

			if (!object.Equals(item, _selectedItem))
			{
				_selectedItem = item;
				FireSelectionChanged();
				Invalidate();
			}
		}

		public ImageRec SelectedItem
		{
			get { return _selectedItem != null ? _selectedItem.imageRec : null; }
		}

		private void HistoryList_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
				{
					SelectItemUnderMouse(e.Location);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void HistoryList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
				{
					var item = HitTest(e.Location);
					if (item != null) FireItemActivated(item);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Events
		public event EventHandler SelectionChanged;

		private void FireSelectionChanged()
		{
			EventHandler ev = SelectionChanged;
			if (ev != null) ev(this, new EventArgs());
		}

		public event EventHandler<ItemActivatedEventArgs> ItemActivated;
		public class ItemActivatedEventArgs : EventArgs
		{
			public ImageRec ImageRec { get; set; }
		}

		private void FireItemActivated(HistoryItem item)
		{
			EventHandler<ItemActivatedEventArgs> ev = ItemActivated;
			if (ev != null)
			{
				ev(this, new ItemActivatedEventArgs
				{
					ImageRec = item.imageRec
				});
			}
		}
		#endregion
	}
}
