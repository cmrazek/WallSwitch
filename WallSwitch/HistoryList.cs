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
		private const int k_itemSpacer = 4;
		private const int k_itemMargin = 1;
		private const float k_mouseWheelScale = .4f;
		private const int k_maxHistory = 30;
		private const int k_selectColorFade = 128;
		#endregion

		#region Member Variables
		private List<HistoryItem> _items = new List<HistoryItem>();
		private int _scroll = 0;
		private int _maxScroll = 0;
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

			SetStyle(ControlStyles.Selectable, true);
			TabStop = true;

			this.MouseWheel += new MouseEventHandler(HistoryList_MouseWheel);
		}

		private void HistoryList_Load(object sender, EventArgs e)
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
		#endregion

		#region Item Manipulation
		public void AddHistory(ImageRec rec)
		{
			var imageRect = new RectangleF(new PointF(0.0f, 0.0f), rec.Image.Size);
			if (imageRect.Width > k_imageWidth) imageRect = imageRect.ScaleRectWidth(k_imageWidth);
			if (imageRect.Height > k_imageHeight) imageRect = imageRect.ScaleRectHeight(k_imageHeight);

			Bitmap bmp = null;
			if (rec != null) bmp = new Bitmap(rec.Image, (int)imageRect.Width, (int)imageRect.Height);

			_items.Insert(0, new HistoryItem
			{
				image = bmp,
				imageRec = rec,
				imageRect = imageRect
			});

			while (_items.Count > _maxHistory)
			{
				var removeItem = _items[_items.Count - 1];
				if (removeItem == _selectedItem) _selectedItem = null;
				_items.RemoveAt(_items.Count - 1);
			}

			UpdateLayout();
			Invalidate();
		}

		private int GetItemIndex(HistoryItem item)
		{
			if (item == null) return -1;

			int index = 0;
			foreach (var searchItem in _items)
			{
				if (searchItem == item) return index;
				index++;
			}
			return -1;
		}

		private HistoryItem GetItemByIndex(int index)
		{
			if (index < 0 || index >= _items.Count) return null;
			return _items[index];
		}

		private bool EnsureVisible(HistoryItem item)
		{
			if (item == null) return false;

			if (item.bounds.Top < _scroll)
			{
				_scroll = item.bounds.Top;
				Invalidate();
				return true;
			}

			var clientSize = ClientSize;
			if (item.bounds.Bottom > _scroll + clientSize.Height)
			{
				_scroll += item.bounds.Bottom - (_scroll + clientSize.Height);
				Invalidate();
				return true;
			}

			return false;
		}

		public void Clear()
		{
			_items.Clear();
			_selectedItem = null;
			UpdateLayout();
			Invalidate();
		}

		public IEnumerable<ImageRec> History
		{
			get
			{
				return (from i in _items select i.imageRec).ToArray();
			}
		}
		#endregion

		#region Layout / Drawing
		private void UpdateLayout()
		{
			Size clientSize = ClientSize;
			int currentX = k_itemSpacer;
			int currentY = k_itemSpacer;

			int widthRequired = k_imageWidth + k_itemSpacer + k_itemMargin * 2;
			int heightRequired = k_imageHeight + k_itemSpacer + k_itemMargin * 2;

			foreach (var item in _items)
			{
				if (currentX > k_itemSpacer && currentX + widthRequired > clientSize.Width)
				{
					currentX = k_itemSpacer;
					currentY += heightRequired;
				}

				item.bounds = new Rectangle(currentX - k_itemMargin, currentY - k_itemMargin,
					k_imageWidth + (k_itemMargin * 2), k_imageHeight + (k_itemMargin * 2));

				currentX += widthRequired;
			}
			currentY += heightRequired;

			_maxScroll = currentY - clientSize.Height;
			if (_maxScroll <= 0)
			{
				_maxScroll = 0;

				vScroll.Value = 0;
				vScroll.Maximum = 0;
				vScroll.SmallChange = 0;
				vScroll.LargeChange = 0;
				vScroll.Enabled = false;
			}
			else
			{
				if (vScroll.Value > _maxScroll) vScroll.Value = _maxScroll;
				vScroll.Maximum = _maxScroll + clientSize.Height;
				vScroll.SmallChange = k_imageHeight / 2;
				vScroll.LargeChange = clientSize.Height > 0 ? clientSize.Height : 0;
				vScroll.Enabled = true;
			}
		}

		private void HistoryList_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				foreach (var item in _items)
				{
					var itemBounds = item.bounds;
					itemBounds.Offset(0, -_scroll);

					if (itemBounds.IntersectsWith(e.ClipRectangle))
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

		private SolidBrush _selBrush = null;

		private void DrawItem(HistoryItem item, Graphics g)
		{
			try
			{
				var image = item.image;
				if (image != null && image.Width > 0 && image.Height > 0)
				{
					var imageRect = new Rectangle(item.bounds.X + k_itemMargin, item.bounds.Y + k_itemMargin,
						(int)item.imageRect.Width, (int)item.imageRect.Height);
					imageRect.Offset((k_imageWidth - imageRect.Width) / 2, (k_imageHeight - imageRect.Height) / 2);
					imageRect.Offset(0, -_scroll);

					g.DrawImage(image, imageRect);
				}

				var shadeRect = item.bounds;
				shadeRect.Offset(0, -_scroll);
				g.DrawRectangle(SystemPens.ControlLight, shadeRect);

				if (object.Equals(item, _selectedItem))
				{
					if (_selBrush == null)
					{
						Color color = SystemColors.Highlight;
						_selBrush = new SolidBrush(Color.FromArgb(k_selectColorFade, color.R, color.G, color.B));
					}
					var selRect = item.bounds;
					selRect.Offset(0, -_scroll);
					g.FillRectangle(_selBrush, selRect);
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
				var scroll = _scroll - (int)(e.Delta * k_mouseWheelScale);
				if (scroll < 0) scroll = 0;
				else if (scroll > _maxScroll) scroll = _maxScroll;

				SetScroll(scroll);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SetScroll(int scroll)
		{
			_scroll = scroll;
			if (_scroll < 0) _scroll = 0;
			else if (_scroll > vScroll.Maximum) _scroll = vScroll.Maximum;

			vScroll.Value = _scroll;
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

		public ImageRec SelectedItem
		{
			get { return _selectedItem != null ? _selectedItem.imageRec : null; }
		}

		private void SelectItem(HistoryItem item)
		{
			var oldSel = _selectedItem;
			_selectedItem = item;

			if (!EnsureVisible(_selectedItem))
			{
				if (oldSel != null)
				{
					var rect = oldSel.bounds;
					rect.Offset(0, -_scroll);
					Invalidate(rect);
				}

				if (_selectedItem != null)
				{
					var rect = _selectedItem.bounds;
					rect.Offset(0, -_scroll);
					Invalidate(rect);
				}
			}
		}
		#endregion

		#region Mouse Input
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

		private void HistoryList_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				Focus();

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

		#region Keyboard Input
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Left:
				case Keys.Right:
				case Keys.Up:
				case Keys.Down:
					return true;
			}
			return base.IsInputKey(keyData);
		}

		private void HistoryList_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
					case Keys.Left:
						{
							var index = GetItemIndex(_selectedItem);
							if (index > 0) SelectItem(GetItemByIndex(index - 1));
						}
						break;

					case Keys.Right:
						{
							var index = GetItemIndex(_selectedItem) + 1;
							if (index >= _items.Count) index = _items.Count - 1;
							SelectItem(GetItemByIndex(index));
						}
						break;

					case Keys.Up:
						if (_selectedItem == null)
						{
							SelectItem(GetItemByIndex(0));
						}
						else
						{
							var pt = _selectedItem.bounds.Center();
							pt.Y -= k_imageHeight + k_itemSpacer + k_itemMargin * 2;

							var found = false;
							foreach (var item in _items)
							{
								if (item.bounds.Contains(pt))
								{
									SelectItem(item);
									found = true;
									break;
								}
							}
							if (!found)
							{
								SelectItem(GetItemByIndex(0));
							}
						}
						break;

					case Keys.Down:
						if (_selectedItem == null)
						{
							SelectItem(GetItemByIndex(0));
						}
						else
						{
							var pt = _selectedItem.bounds.Center();
							pt.Y += k_imageHeight + k_itemSpacer + k_itemMargin * 2;

							var found = false;
							foreach (var item in _items)
							{
								if (item.bounds.Contains(pt))
								{
									SelectItem(item);
									found = true;
									break;
								}
							}

							if (!found)
							{
								HistoryItem selItem = null;
								foreach (var item in _items)
								{
									if (item.bounds.Top < pt.Y && item.bounds.Bottom > pt.Y)
									{
										selItem = item;
									}
								}
								if (selItem != null) SelectItem(selItem);
							}
						}
						break;
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
