using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WallSwitch.Themes
{
	partial class LocationBrowser : Form
	{
		private Location _loc;
		private LBItem _mouseOverItem;
		private List<LBItem> _rawItems = new List<LBItem>();
		private List<LBItem> _items = new List<LBItem>();
		private int _itemHeight = k_rawItemHeight;
		private int _totalHeight;
		private int _maxThumbWidth = k_rawThumbWidth;
		private int _maxThumbHeight = k_rawItemHeight;
		
		private bool _itemLayoutUpdateRequired = true;
		private int _spacer;
		private Rectangle _clientRect;

		private const int k_rawItemHeight = 100;
		private const int k_rawThumbWidth = 175;

		#region Construction
		public LocationBrowser(Location loc)
		{
			if (loc == null) throw new ArgumentNullException(nameof(loc));

			_loc = loc;
			_loc.ContentUpdated += Location_ContentUpdated;

			InitializeComponent();

			Text = _loc.Path;
			DoubleBuffered = true;

			MouseWheel += LocationBrowser_MouseWheel;
		}

		private void LocationBrowser_Load(object sender, EventArgs e)
		{
			try
			{
				LoadItems();
				Global.FileDeleted += Global_FileDeleted;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LoadItems()
		{
			foreach (var item in _rawItems) item.OnBrowserClosed();
			_rawItems.Clear();

			using (var db = new Database())
			{
				var table = db.SelectDataTable("select * from img where location_id = @location_id", "@location_id", _loc.RowId);
				foreach (DataRow row in table.Rows)
				{
					var item = new LBItem(this, ImageRec.FromDataRow(row), _loc, _items.Count);
					_rawItems.Add(item);
				}
			}

			lock (_thumbnailLock)
			{
				_items = _rawItems.ToList();
				ApplySort();

				_itemLayoutUpdateRequired = true;

				RenumberItems();
				RefreshStatusCounts();
				UpdateScroll();
				SetSelection(-1, -1);
				Invalidate();
			}

			Global.FileDeleted += Global_FileDeleted;
		}

		private void LocationBrowser_FormClosed(object sender, FormClosedEventArgs e)
		{
			Global.FileDeleted -= Global_FileDeleted;

			foreach (var item in _rawItems)
			{
				item.OnBrowserClosed();
			}
		}

		public Location LocationObject
		{
			get { return _loc; }
		}

		private void Location_ContentUpdated(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => { Location_ContentUpdated(sender, e); }));
				return;
			}

			try
			{
				LoadItems();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Image Management
		private int GetItemIndex(LBItem item)
		{
			var index = 0;
			foreach (var i in _items)
			{
				if (i == item) return index;
				index++;
			}
			return -1;
		}

		private void Global_FileDeleted(object sender, Global.DeleteFileEventArgs e)
		{
			try
			{
				var item = _items.FirstOrDefault(x => string.Equals(x.ImageRec.LocationOnDisk, e.LocationOnDisk, StringComparison.OrdinalIgnoreCase));
				if (item != null)
				{
					if (ItemIsVisible(item.Index)) Invalidate();
					RemoveItem(item.Index);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RemoveItem(int index)
		{
			lock (_thumbnailLock)
			{
				if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));

				_items.RemoveAt(index);
				RenumberItems();
				Invalidate();
			}
		}

		private void RenumberItems()
		{
			var index = 0;
			foreach (var item in _rawItems) item.Index = -1;
			foreach (var item in _items) item.Index = index++;
		}

		private void OpenItem(LBItem item)
		{
			var fileName = item.ImageRec.LocationOnDisk;
			if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
			{
				this.ShowError(Res.Error_ImageFileMissing, fileName);
				Global.OnFileDeleted(item.ImageRec.LocationOnDisk);
				return;
			}

			System.Diagnostics.Process.Start(fileName);
		}
		#endregion

		#region Context Menu
		private void OpenContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var item = SelectedItems.FirstOrDefault();
				if (item == null) return;

				OpenItem(item);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ExploreContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var item = SelectedItems.FirstOrDefault();
				if (item == null) return;

				var fileName = item.ImageRec.LocationOnDisk;
				if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				{
					this.ShowError(Res.Error_ImageFileMissing, fileName);
					Global.OnFileDeleted(item.ImageRec.LocationOnDisk);
					return;
				}

				FileUtil.ExploreFile(fileName);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void DeleteContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var items = SelectedItems.ToArray();
				if (items.Length == 0) return;

				if (items.Length == 1)
				{
					var item = items[0];
					var fileName = item.ImageRec.LocationOnDisk;
					if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
					{
						this.ShowError(Res.Error_ImageFileMissing, fileName);
						Global.OnFileDeleted(item.ImageRec.LocationOnDisk);
						return;
					}

					if (MessageBox.Show(this, Res.Confirm_DeleteHistoryFile, Res.Confirm_DeleteHistoryFile_Caption,
						MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						MainWindow.Current.DeleteImageFile(fileName);
					}
				}
				else
				{
					if (MessageBox.Show(this, Res.Confirm_DeleteFiles, Res.Confirm_DeleteFiles_Caption, MessageBoxButtons.YesNo,
						MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						foreach (var item in items)
						{
							var fileName = item.ImageRec.LocationOnDisk;
							if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
							{
								this.ShowError(Res.Error_ImageFileMissing, fileName);
								Global.OnFileDeleted(item.ImageRec.LocationOnDisk);
								continue;
							}

							MainWindow.Current.DeleteImageFile(fileName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void EnableContextMenuItems()
		{
			var selectCount = SelectionCount;

			c_openContextMenuItem.Enabled = selectCount == 1;
			c_exploreContextMenuItem.Enabled = selectCount == 1;
			c_deleteContextMenuItem.Enabled = selectCount >= 1;
		}
		#endregion

		#region Status Bar
		private void RefreshStatusCounts()
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => { RefreshStatusCounts(); }));
				return;
			}

			var count = _items.Count;
			if (count == 1) c_statusCounts.Text = string.Format(Res.Browser_ImageCount1, count);
			else c_statusCounts.Text = string.Format(Res.Browser_ImageCount, count);
		}

		private void SetStatusMessage(string message)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => { SetStatusMessage(message); }));
				return;
			}

			Log.Info("Status: {0}", message);
			c_statusMessage.Text = message;
		}
		#endregion

		#region Layout and Paint
		private Pen _borderPen;
		private Font _sizeFont;

		private void UpdateItemLayout(Graphics g)
		{
			_itemLayoutUpdateRequired = false;

			_itemHeight = (int)Math.Ceiling((float)k_rawItemHeight * g.DpiY / 96.0f);
			_totalHeight = _items.Count * _itemHeight;
			_spacer = (int)(4.0 * g.DpiX / 96.0f);

			_maxThumbWidth = (int)Math.Ceiling(k_rawThumbWidth * g.DpiX / 96.0f);
			_maxThumbHeight = _itemHeight - _spacer * 2;

			_clientRect = new Rectangle(0, c_filterPanel.Height,
				ClientSize.Width - c_vScroll.Width,
				ClientSize.Height - c_statusBar.Height - c_filterPanel.Height);

			_visibleRect = _clientRect;
			_visibleRect.Offset(0, _scroll);

			UpdateScroll();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			if (_itemLayoutUpdateRequired) UpdateItemLayout(g);

			g.FillRectangle(SystemBrushes.Window, _clientRect);

			g.TranslateTransform(0.0f, (float)(_clientRect.Top - _scroll));

			var topItem = _scroll / _itemHeight;
			var bottomItem = (_scroll + _clientRect.Height) / _itemHeight;
			var clipRectDoc = ClientToDoc(e.ClipRectangle);

			for (var itemIndex = topItem; itemIndex <= bottomItem && itemIndex < _items.Count; itemIndex++)
			{
				var item = _items[itemIndex];
				var itemBounds = GetItemDocBounds(itemIndex);
				if (itemBounds.IntersectsWith(clipRectDoc))
				{
					DrawItem(g, item, itemBounds);
				}
			}
		}

		private void DrawItem(Graphics g, LBItem item, Rectangle itemBounds)
		{
			// Highlight
			var selected = ItemIsSelected(item.Index);
			if (selected) g.FillRectangle(SystemBrushes.Highlight, itemBounds);

			// Border
			if (_borderPen == null) _borderPen = new Pen(SystemBrushes.Control);
			g.DrawRectangle(_borderPen, itemBounds);

			// Thumbnail background
			var maxThumbRect = new Rectangle(itemBounds.Left + _spacer, itemBounds.Top + _spacer, _maxThumbWidth, _maxThumbHeight);
			if (!selected)
			{
				var thumbBackgroundRect = maxThumbRect;
				thumbBackgroundRect.Inflate(1, 1);
				g.FillRectangle(SystemBrushes.Control, thumbBackgroundRect);
			}

			// Thumbnail
			var thumb = item.ImageRec.Thumbnail;
			if (thumb == null)
			{
				QueueThumbnailRetrieval(item);
			}
			else
			{
				var imgRect = new RectangleF(PointF.Empty, thumb.Size);
				imgRect = imgRect.ScaleRectWidth(_maxThumbWidth);
				if (imgRect.Height > _maxThumbHeight) imgRect = imgRect.ScaleRectHeight(_maxThumbHeight);
				imgRect = imgRect.CenterInside(maxThumbRect);

				g.DrawImage(thumb, imgRect);
			}

			var infoLeft = itemBounds.Left + _maxThumbWidth + _spacer * 2;
			var infoWidth = itemBounds.Right - infoLeft;
			if (infoWidth > 0)
			{
				var infoPt = new Point(infoLeft, itemBounds.Top + _spacer);

				// File Name
				var stringSize = g.MeasureString(item.RelativeLocation, Font, infoWidth);
				g.DrawString(item.RelativeLocation, Font, selected ? SystemBrushes.HighlightText : SystemBrushes.WindowText,
					new RectangleF(infoPt, stringSize));
				infoPt.Y += (int)Math.Ceiling(stringSize.Height) + _spacer;

				// Size
				var imgSize = item.ImageRec.Size;
				if (imgSize.HasValue)
				{
					if (_sizeFont == null) _sizeFont = new Font(Font.FontFamily, Font.Size * .9f);

					var str = imgSize.Value.ToSizeString();
					stringSize = g.MeasureString(str, _sizeFont);
					g.DrawString(str, _sizeFont, selected ? SystemBrushes.HighlightText : SystemBrushes.WindowText,
						new RectangleF(infoPt, stringSize));
					infoPt.Y += (int)Math.Ceiling(stringSize.Height);
				}

				// Rating
				infoPt.Y += _spacer;
				var pt = infoPt;
				var starSize = new Size((int)Math.Round(Images.StarUnrated.Width * g.DpiX / 96.0f),
					(int)Math.Round(Images.StarUnrated.Height * g.DpiY / 96.0f));

				for (int i = 0; i < 5; i++)
				{
					item.StarRects[i] = new Rectangle(pt, starSize);
					pt.X += starSize.Width;
				}

				var rating = item.ImageRec.Rating;
				if (item.MouseOverRating > 0 && item.MouseOverRating != rating)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (item.MouseOverRating >= i) g.DrawImage(Images.StarMouseOver1, item.StarRects[i - 1]);
						else g.DrawImage(Images.StarMouseOver0, item.StarRects[i - 1]);
					}
				}
				else if (rating > 0)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (rating >= i) g.DrawImage(Images.StarRated1, item.StarRects[i - 1]);
						else g.DrawImage(Images.StarRated0, item.StarRects[i - 1]);
					}
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						g.DrawImage(Images.StarUnrated, item.StarRects[i]);
					}
				}
			}
		}

		private void LocationBrowser_Resize(object sender, EventArgs e)
		{
			_itemLayoutUpdateRequired = true;
			Invalidate();
		}

		private Point ClientToDoc(Point pt)
		{
			return new Point(pt.X - _clientRect.X, pt.Y - _clientRect.Y + _scroll);
		}

		private Point DocToClient(Point pt)
		{
			return new Point(pt.X + _clientRect.X, pt.Y + _clientRect.Y - _scroll);
		}

		private Rectangle ClientToDoc(Rectangle r)
		{
			return new Rectangle(r.X - _clientRect.X, r.Y - _clientRect.Y + _scroll, r.Width, r.Height);
		}

		private Rectangle DocToClient(Rectangle r)
		{
			return new Rectangle(r.X + _clientRect.X, r.Y + _clientRect.Y - _scroll, r.Width, r.Height);
		}

		private Rectangle GetItemDocBounds(int index)
		{
			return new Rectangle(_clientRect.Left, index * _itemHeight, _clientRect.Width, _itemHeight);
		}

		private bool ItemIsVisible(int index)
		{
			var itemBounds = GetItemDocBounds(index);
			return itemBounds.IntersectsWith(_visibleRect);
		}

		public void InvalidateItem(int index)
		{
			if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));

			var itemBounds = DocToClient(GetItemDocBounds(index));
			if (itemBounds.IntersectsWith(_clientRect)) Invalidate(itemBounds);
		}
		#endregion

		#region Scrolling
		private int _scroll;
		private Rectangle _visibleRect;
		private int _maxScroll;

		private void UpdateScroll()
		{
			_maxScroll = _totalHeight - _clientRect.Height;
			if (_maxScroll < 0) _maxScroll = 0;

			if (_scroll > _maxScroll) c_vScroll.Value = _maxScroll;

			c_vScroll.Maximum = _totalHeight;
			c_vScroll.LargeChange = _clientRect.Height;
			c_vScroll.SmallChange = 24;
		}

		private void VScroll_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				_scroll = c_vScroll.Value;

				_visibleRect = _clientRect;
				_visibleRect.Offset(0, _scroll);

				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SetScroll(int value)
		{
			if (value < 0) value = 0;
			else if (value > _maxScroll) value = _maxScroll;

			if (value != c_vScroll.Value)
			{
				c_vScroll.Value = value;
			}
		}

		private void EnsureItemVisible(int index)
		{
			if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));

			var bounds = GetItemDocBounds(index);
			if (bounds.Top < _visibleRect.Top)
			{
				SetScroll(bounds.Top);
			}
			else if (bounds.Bottom > _visibleRect.Bottom)
			{
				SetScroll(bounds.Bottom - _visibleRect.Height);
			}
		}
		#endregion

		#region Mouse
		private void LocationBrowser_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Delta > 0)
				{
					// Scroll Up
					SetScroll(_scroll - c_vScroll.SmallChange * 3);
				}
				else if (e.Delta < 0)
				{
					// Scroll Down
					SetScroll(_scroll + c_vScroll.SmallChange * 3);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left)
				{
					var index = HitTest(e.Location);
					if (index != -1)
					{
						Capture = true;
						if (ModifierKeys.HasFlag(Keys.Shift))
						{
							SetSelection(_selStart, index);
						}
						else
						{
							SetSelection(index, index);
						}
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					var index = HitTest(e.Location);
					if (index != -1)
					{
						if (ModifierKeys.HasFlag(Keys.Shift))
						{
							SetSelection(_selStart, index);
						}
						else if (!ItemIsSelected(index))
						{
							SetSelection(index, index);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				Capture = false;
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button.HasFlag(MouseButtons.Left))
				{
					var index = HitTest(e.Location);
					if (index != -1)
					{
						SetSelection(_selStart, index);
					}
				}
				else if (e.Button == MouseButtons.None)
				{
					var index = HitTest(e.Location);
					if (index != -1)
					{
						if (_mouseOverItem != _items[index])
						{
							if (_mouseOverItem != null) _mouseOverItem.MouseOverRating = 0;
							_mouseOverItem = _items[index];

							var rating = HitTestRating(index, e.Location);
							if (rating != -1) _mouseOverItem.MouseOverRating = rating;
						}
						else // mouse is still over mouse-over item
						{
							var rating = HitTestRating(index, e.Location);
							if (rating != -1) _mouseOverItem.MouseOverRating = rating;
							else _mouseOverItem.MouseOverRating = 0;
						}
					}
					else // index == -1
					{
						if (_mouseOverItem != null)
						{
							_mouseOverItem.MouseOverRating = 0;
							_mouseOverItem = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left)
				{
					var index = HitTest(e.Location);
					if (index != -1)
					{
						var rating = HitTestRating(index, e.Location);
						if (rating != -1) SetItemRating(index, rating);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				var index = HitTest(e.Location);
				if (index != -1)
				{
					OpenItem(_items[index]);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private int HitTest(Point clientPt)
		{
			var docPt = ClientToDoc(clientPt);
			var index = docPt.Y / _itemHeight;
			if (index < 0 || index >= _items.Count) return -1;
			return index;
		}

		private int HitTestRating(int index, Point clientPt)
		{
			if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));

			var docPt = ClientToDoc(clientPt);
			var item = _items[index];

			for (int i = 0; i < 5; i++)
			{
				if (item.StarRects[i].Contains(docPt)) return i + 1;
			}

			return -1;
		}

		private void SetItemRating(int index, int rating)
		{
			if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));
			if (rating < 0 || rating > 5) throw new ArgumentOutOfRangeException(nameof(rating));

			var item = _items[index];
			Global.UpdateRating(item.ImageRec.Location, rating);

			using (var db = new Database())
			{
				using (var tran = db.BeginTransaction())
				{
					using (var cmd = db.CreateCommand("update history set rating = @rating where path = @path"))
					{
						cmd.Parameters.AddWithValue("@rating", rating);
						cmd.Parameters.AddWithValue("@path", item.ImageRec.Location);
						cmd.ExecuteNonQuery();
					}

					using (var cmd = db.CreateCommand("update img set rating = @rating where path = @path"))
					{
						cmd.Parameters.AddWithValue("@rating", rating);
						cmd.Parameters.AddWithValue("@path", item.ImageRec.Location);
						cmd.ExecuteNonQuery();
					}

					tran.Commit();
				}
			}
		}
		#endregion

		#region Keyboard
		private void LocationBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.PageDown)
				{
					if (IsAnySelected)
					{
						var index = BottomSelection + _visibleRect.Height / _itemHeight;
						if (index >= _items.Count) index = _items.Count - 1;

						SetSelection(index, ModifierKeys.HasFlag(Keys.Shift));
					}
					else if (_items.Count > 0)
					{
						SetSelection(0, false);
					}
				}
				else if (e.KeyCode == Keys.PageUp)
				{
					if (IsAnySelected)
					{
						var index = TopSelection - _visibleRect.Height / _itemHeight;
						if (index < 0) index = 0;

						SetSelection(index, ModifierKeys.HasFlag(Keys.Shift));
					}
					else if (_items.Count > 0)
					{
						SetSelection(0, false);
					}
				}
				else if (e.KeyCode == Keys.Down)
				{
					if (IsAnySelected)
					{
						var index = BottomSelection + 1;
						if (index < _items.Count) SetSelection(index, ModifierKeys.HasFlag(Keys.Shift));
					}
					else
					{
						if (_items.Count > 0) SetSelection(0, false);
					}
				}
				else if (e.KeyCode == Keys.Up)
				{
					if (IsAnySelected)
					{
						var index = TopSelection - 1;
						if (index > 0) SetSelection(index, ModifierKeys.HasFlag(Keys.Shift));
					}
					else
					{
						if (_items.Count > 0) SetSelection(0, false);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Thumbnail Retrieval
		private object _thumbnailLock = new object();
		private Queue<LBItem> _thumbnailQueue;
		private DateTime _lastThumbnailGenerate = DateTime.MinValue;

		private const int k_thumbnailWait = 200;

		private void QueueThumbnailRetrieval(LBItem item)
		{
			lock (_thumbnailLock)
			{
				if (_thumbnailQueue == null)
				{
					_thumbnailQueue = new Queue<LBItem>();
					_thumbnailQueue.Enqueue(item);
					System.Threading.ThreadPool.QueueUserWorkItem(ThumbnailRetrievalProc);
				}
				else if (!_thumbnailQueue.Contains(item))
				{
					_thumbnailQueue.Enqueue(item);
				}
			}
		}

		private void ThumbnailRetrievalProc(object state)
		{
			using (var db = new Database())
			{
				while (true)
				{
					LBItem item = null;
					lock (_thumbnailLock)
					{
						if (_thumbnailQueue.Count == 0)
						{
							_thumbnailQueue = null;
							return;
						}

						item = _thumbnailQueue.Peek();  // Don't remove it from the queue until the image is fully loaded

						if (!ItemIsVisible(item.Index))	// If item is no longer visible on the screen, then don't spend time getting the thumbnail.
						{
							_thumbnailQueue.Dequeue();
							item = null;
						}
					}

					if (item != null)
					{
						var now = DateTime.Now;
						if (now.Subtract(_lastThumbnailGenerate).TotalMilliseconds < k_thumbnailWait)
						{
							Thread.Sleep(k_thumbnailWait - (int)now.Subtract(_lastThumbnailGenerate).TotalMilliseconds);
						}

						SetStatusMessage(string.Format("Getting Thumbnail: {0}", item.ImageRec.Location));

						item.ImageRec.Retrieve(db);
						item.ImageRec.Release();
						var thumb = item.ImageRec.Thumbnail;
						if (thumb != null)
						{
							BeginInvoke(new Action(() => { OnThumbnailUpdated(item); }));
						}

						lock (_thumbnailLock)
						{
							_thumbnailQueue.Dequeue();
							if (_thumbnailQueue.Count == 0)
							{
								_thumbnailQueue = null;
								return;
							}
						}

						_lastThumbnailGenerate = DateTime.Now;
					}
				}
			}
		}

		private void OnThumbnailUpdated(LBItem item)
		{
			if (item != null && item.Index >= 0)
			{
				if (ItemIsVisible(item.Index)) Invalidate();
			}
		}
		#endregion

		#region Selection
		private int _selStart = -1;
		private int _selEnd = -1;

		private void SetSelection(int start, int end)
		{
			if (start != _selStart || end != _selEnd)
			{
				_selStart = start;
				_selEnd = end;

				if (_selEnd >= 0) EnsureItemVisible(_selEnd);
				Invalidate();
				EnableContextMenuItems();
			}
		}

		private void SetSelection(int index, bool extend)
		{
			if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));

			var end = index;
			var start = _selStart;
			if (!extend || start < 0) start = index;

			SetSelection(start, end);
		}

		private bool ItemIsSelected(int index)
		{
			if (_selStart <= _selEnd)
			{
				return _selStart <= index && _selEnd >= index;
			}
			else
			{
				return _selEnd <= index && _selStart >= index;
			}
		}

		private int SelectionCount
		{
			get
			{
				if (_selStart == -1) return 0;
				if (_selStart <= _selEnd) return _selEnd - _selStart + 1;
				return _selStart - _selEnd + 1;
			}
		}

		private IEnumerable<LBItem> SelectedItems
		{
			get
			{
				if (_selStart <= _selEnd)
				{
					for (int i = _selStart; i <= _selEnd; i++) yield return _items[i];
				}
				else
				{
					for (int i = _selStart; i >= _selEnd; i--) yield return _items[i];
				}
			}
		}

		private bool SingleItemSelected
		{
			get { return _selStart >= 0 && _selStart == _selEnd; }
		}

		private bool IsAnySelected
		{
			get { return _selStart >= 0; }
		}

		private int BottomSelection
		{
			get { return _selStart <= _selEnd ? _selEnd : _selStart; }
		}

		private int TopSelection
		{
			get { return _selStart <= _selEnd ? _selStart : _selEnd; }
		}
		#endregion

		#region Filter Text Box
		private void FilterTextBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				c_filterTimer.Stop();
				c_filterTimer.Start();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_filterTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Enter)
				{
					FilterTimer_Tick(sender, e);
				}
				else
				{
					switch (e.KeyCode)
					{
						case Keys.PageDown:
						case Keys.PageUp:
						case Keys.Down:
						case Keys.Up:
							LocationBrowser_PreviewKeyDown(sender, e);
							e.IsInputKey = false;
							break;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void c_filterTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Enter)
				{
					e.Handled = true;
					e.SuppressKeyPress = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void FilterTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				c_filterTimer.Stop();

				var text = c_filterTextBox.Text;
				SetStatusMessage(string.Format("Filtering for: {0}", text));

				ApplyFilter(text);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ClearFilterButton_Click(object sender, EventArgs e)
		{
			try
			{
				c_filterTextBox.Text = string.Empty;
				c_filterTextBox.Focus();
				FilterTimer_Tick(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void ApplyFilter(string text)
		{
			var filter = new TextFilter(text);
			var items = _rawItems.Where(x => filter.Match(x.RelativeLocation)).ToList();

			lock (_thumbnailLock)
			{
				_items = items;
				ApplySort();
				RenumberItems();
				RefreshStatusCounts();
				SetScroll(0);
				SetSelection(-1, -1);
				_itemLayoutUpdateRequired = true;
				Invalidate();
			}
		}
		#endregion

		#region Sorting
		private SortMode _sortMode = SortMode.Location;
		private bool _sortDesc;

		private enum SortMode
		{
			Location,
			Rating,
			Size
		}

		private void ApplySort()
		{
			switch (_sortMode)
			{
				case SortMode.Location:
					if (_sortDesc)
					{
						_items.Sort((a, b) => string.Compare(a.RelativeLocation, b.RelativeLocation, true) * -1);
					}
					else
					{
						_items.Sort((a, b) => string.Compare(a.RelativeLocation, b.RelativeLocation, true));
					}
					break;
				case SortMode.Rating:
					if (_sortDesc)
					{
						_items.Sort((a, b) => a.ImageRec.Rating.CompareTo(b.ImageRec.Rating) * -1);
					}
					else
					{
						_items.Sort((a, b) => a.ImageRec.Rating.CompareTo(b.ImageRec.Rating));
					}
					break;
				case SortMode.Size:
					if (_sortDesc)
					{
						_items.Sort((a, b) => a.ImageRec.Size.GetValueOrDefault().CompareTo(b.ImageRec.Size.GetValueOrDefault()) * -1);
					}
					else
					{
						_items.Sort((a, b) => a.ImageRec.Size.GetValueOrDefault().CompareTo(b.ImageRec.Size.GetValueOrDefault()));
					}
					break;
			}
		}

		private void SortPathContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (_sortMode != SortMode.Location)
				{
					_sortMode = SortMode.Location;
					_sortDesc = false;
				}
				else
				{
					_sortDesc = !_sortDesc;
				}

				ApplySort();
				RenumberItems();
				_itemLayoutUpdateRequired = true;
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SortRatingContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (_sortMode != SortMode.Rating)
				{
					_sortMode = SortMode.Rating;
					_sortDesc = true;
				}
				else
				{
					_sortDesc = !_sortDesc;
				}

				ApplySort();
				RenumberItems();
				_itemLayoutUpdateRequired = true;
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SortSizeContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (_sortMode != SortMode.Size)
				{
					_sortMode = SortMode.Size;
					_sortDesc = true;
				}
				else
				{
					_sortDesc = !_sortDesc;
				}

				ApplySort();
				RenumberItems();
				_itemLayoutUpdateRequired = true;
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion
	}
}
