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
		public const int k_unscaledImageWidth = 120;
		public const int k_unscaledImageHeight = 120;
		private const int k_itemSpacer = 4;
		private const float k_mouseWheelScale = .4f;
		private const int k_maxHistory = 30;
		#endregion

		#region Member Variables
		private List<HistoryItem> _items = new List<HistoryItem>();
		private int _scroll;
		private int _maxScroll;
		private HistoryItem _selectedItem;
		private HistoryItem _mouseOverItem;
		private int _maxHistory = k_maxHistory;
		private ToolTip _imageToolTip = null;

		private static int _imageWidth = k_unscaledImageWidth;
		private static int _imageHeight = k_unscaledImageHeight;
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
		public HistoryItem AddHistory(HistoryItem item)
		{
			if (item == null) throw new ArgumentNullException("item");

			_items.Insert(0, item);

			while (_items.Count > _maxHistory)
			{
				var removeItem = _items[_items.Count - 1];
				if (removeItem == _selectedItem) _selectedItem = null;
				_items.RemoveAt(_items.Count - 1);
			}

			UpdateLayout();
			Invalidate();
			return item;
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

			if (item.Bounds.Top < _scroll)
			{
				_scroll = item.Bounds.Top;
				Invalidate();
				return true;
			}

			var clientSize = ClientSize;
			if (item.Bounds.Bottom > _scroll + clientSize.Height)
			{
				_scroll += item.Bounds.Bottom - (_scroll + clientSize.Height);
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

		public IEnumerable<HistoryItem> History
		{
			get { return _items; }
		}

		public bool RemoveItem(HistoryItem item)
		{
			if (_items.Remove(item))
			{
				if (_selectedItem.Equals(item)) _selectedItem = null;
				UpdateLayout();
				Invalidate();
				return true;
			}

			return false;
		}

		public bool RemoveItem(string fileName)
		{
			foreach (var item in _items)
			{
				if (item.LocationOnDisk == fileName)
				{
					return RemoveItem(item);
				}
			}

			return false;
		}
		#endregion

		#region Layout / Drawing
		private void UpdateLayout()
		{
			Size clientSize = ClientSize;
			int currentX = k_itemSpacer;
			int currentY = k_itemSpacer;

			float xScale, yScale;
			PointF dpi;
			using (var g = this.CreateGraphics())
			{
				dpi = g.DpiPoint();
				xScale = g.DpiX / 96.0f;
				yScale = g.DpiY / 96.0f;
			}
			var imageSize = new Size((int)Math.Round(k_unscaledImageWidth * xScale),
				(int)Math.Round(k_unscaledImageHeight * yScale));

			var itemSize = HistoryItem.GetRequiredSize(imageSize, dpi);

			foreach (var item in _items)
			{
				if (currentX > k_itemSpacer && currentX + itemSize.Width > clientSize.Width)
				{
					currentX = k_itemSpacer;
					currentY += itemSize.Height + k_itemSpacer;
				}

				item.SetBounds(new Rectangle(currentX , currentY, itemSize.Width, itemSize.Height), imageSize, dpi);
				currentX += itemSize.Width + k_itemSpacer;
			}
			currentY += itemSize.Height;

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
				vScroll.SmallChange = _imageHeight / 2;
				vScroll.LargeChange = clientSize.Height > 0 ? clientSize.Height : 0;
				vScroll.Enabled = true;
			}
		}

		private void HistoryList_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				var g = e.Graphics;
				g.TranslateTransform(0.0f, -_scroll);

				var visibleRect = e.ClipRectangle;
				visibleRect.Offset(0, _scroll);

				foreach (var item in _items)
				{
					if (item.Bounds.IntersectsWith(visibleRect))
					{
						item.Draw(g, item == _selectedItem);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
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

		public void InvalidateItem(HistoryItem item)
		{
			var bounds = item.Bounds;
			bounds.Offset(0, -_scroll);
			if (ClientRectangle.IntersectsWith(bounds)) Invalidate(bounds);
		}

		public Point ClientToDoc(Point pt)
		{
			return new Point(pt.X, pt.Y + _scroll);
		}

		public Rectangle ClientToDoc(Rectangle rect)
		{
			return new Rectangle(rect.Left, rect.Top + _scroll, rect.Width, rect.Height);
		}

		public Point DocToClient(Point pt)
		{
			return new Point(pt.X, pt.Y - _scroll);
		}

		public Rectangle DocToClient(Rectangle rect)
		{
			return new Rectangle(rect.Left, rect.Top - _scroll, rect.Width, rect.Height);
		}
		#endregion

		#region Selection
		private HistoryItem HitTest(Point pt)
		{
			pt = new Point(pt.X, pt.Y + _scroll);
			foreach (var item in _items)
			{
				if (item.Bounds.Contains(pt)) return item;
			}
			return null;
		}

		public HistoryItem SelectedItem
		{
			get { return _selectedItem; }
		}

		private void SelectItem(HistoryItem item)
		{
			var oldSel = _selectedItem;
			_selectedItem = item;

			if (!EnsureVisible(_selectedItem))
			{
				if (oldSel != null)
				{
					var rect = oldSel.Bounds;
					rect.Offset(0, -_scroll);
					Invalidate(rect);
				}

				if (_selectedItem != null)
				{
					var rect = _selectedItem.Bounds;
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
					ProcessItemClick(e.Location);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ProcessItemClick(Point pt)
		{
			var item = HitTest(pt);

			if (item != null)
			{
				var rating = item.RatingHitTest(ClientToDoc(pt));
				if (rating == -1)
				{
					if (_selectedItem != null) Invalidate(DocToClient(_selectedItem.Bounds));
					_selectedItem = item;
					FireSelectionChanged();
					Invalidate(DocToClient(_selectedItem.Bounds));
				}
				else
				{
					item.Rating = rating;
				}
			}
			else
			{
				if (_selectedItem != null)
				{
					_selectedItem = null;
					FireSelectionChanged();
					Invalidate(DocToClient(_selectedItem.Bounds));
				}
			}
		}

		private void HistoryList_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				Focus();
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

		public ToolTip ImageToolTip
		{
			get { return _imageToolTip; }
			set { _imageToolTip = value; }
		}

		private void HistoryList_MouseHover(object sender, EventArgs e)
		{
			try
			{
				var cursorPos = this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));

				if (_imageToolTip != null)
				{
					HistoryItem hi = HitTest(cursorPos);
					if (hi != null) _imageToolTip.SetToolTip(this, hi.Location);
					else _imageToolTip.SetToolTip(this, string.Empty);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void HistoryList_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				this.ResetMouseEventArgs();

				var item = HitTest(e.Location);
				if (item != null)
				{
					if (_mouseOverItem != item && _mouseOverItem != null)
					{
						_mouseOverItem = item;
						_mouseOverItem.OnMouseLeave();
					}

					var pt = e.Location;
					pt.Offset(0, _scroll);
					_mouseOverItem = item;
					_mouseOverItem.OnMouseOver(pt);
				}
				else
				{
					if (_mouseOverItem != null)
					{
						_mouseOverItem.OnMouseLeave();
						_mouseOverItem = null;
					}
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
							var pt = _selectedItem.Bounds.Center();
							pt.Y -= _selectedItem.Bounds.Height;

							var found = false;
							foreach (var item in _items)
							{
								if (item.Bounds.Contains(pt))
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
							var pt = _selectedItem.Bounds.Center();
							pt.Y += _selectedItem.Bounds.Height;

							var found = false;
							foreach (var item in _items)
							{
								if (item.Bounds.Contains(pt))
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
									if (item.Bounds.Top < pt.Y && item.Bounds.Bottom > pt.Y)
									{
										selItem = item;
									}
								}
								if (selItem != null) SelectItem(selItem);
							}
						}
						break;

					case Keys.Delete:
						if (_selectedItem != null)
						{
							FireDeleteItemRequested(_selectedItem);
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
			public HistoryItem Item { get; private set; }

			public ItemActivatedEventArgs(HistoryItem item)
			{
				Item = item;
			}
		}

		private void FireItemActivated(HistoryItem item)
		{
			EventHandler<ItemActivatedEventArgs> ev = ItemActivated;
			if (ev != null)
			{
				ev(this, new ItemActivatedEventArgs(item));
			}
		}

		public event EventHandler<DeleteItemRequestedEventArgs> DeleteItemRequested;

		public class DeleteItemRequestedEventArgs : EventArgs
		{
			public HistoryItem Item { get; private set; }

			public DeleteItemRequestedEventArgs(HistoryItem item)
			{
				Item = item;
			}
		}

		public void FireDeleteItemRequested(HistoryItem item)
		{
			var ev = DeleteItemRequested;
			if (ev != null) ev(this, new DeleteItemRequestedEventArgs(item));
		}
		#endregion

		public static int ThumbnailWidth
		{
			get { return _imageWidth; }
		}

		public static int ThumbnailHeight
		{
			get { return _imageHeight; }
		}

		public int MaxHistory
		{
			get { return _maxHistory; }
		}
	}
}
