using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WallSwitch
{
	partial class LocationBrowser : Form
	{
		private Location _loc;
		private ItemInfo _mouseOverItem;
		private List<ItemInfo> _items = new List<ItemInfo>();
		private int _itemHeight = k_rawItemHeight;
		private int _totalHeight;
		private int _maxThumbWidth = k_rawThumbWidth;
		private int _maxThumbHeight = k_rawItemHeight;
		
		private bool _itemLayoutUpdateRequired = true;
		private int _spacer;
		private Rectangle _clientRect;

		private const int k_rawItemHeight = 100;
		private const int k_rawThumbWidth = 175;

		private class ItemInfo
		{
			public ImageRec img;
			public string relativeLocation;
			public Rectangle[] starRects;
			public int mouseOverRating;
			public int index;
		}

		#region Construction
		public LocationBrowser(Location loc)
		{
			if (loc == null) throw new ArgumentNullException(nameof(loc));

			_loc = loc;

			InitializeComponent();

			Text = _loc.Path;
			DoubleBuffered = true;

			MainWindow.Current.ImageFileDeleted += MainWindow_ImageFileDeleted;
			MouseWheel += LocationBrowser_MouseWheel;
		}

		private void LocationBrowser_Load(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					var table = db.SelectDataTable("select * from img where location_id = @location_id order by img.path", "@location_id", _loc.RowId);
					foreach (DataRow row in table.Rows)
					{
						var img = ImageRec.FromDataRow(row);
						var item = CreateItemInfo(img);
						item.index = _items.Count;
						_items.Add(item);
					}

					RefreshStatusCounts();
					UpdateScroll();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_FormClosed(object sender, FormClosedEventArgs e)
		{
			//_kill = true;		TODO: remove

			MainWindow.Current.ImageFileDeleted -= MainWindow_ImageFileDeleted;
		}

		public Location LocationObject
		{
			get { return _loc; }
		}

		private ItemInfo CreateItemInfo(ImageRec img)
		{
			var info = new ItemInfo();
			info.img = img;

			info.relativeLocation = img.Location;
			if (info.relativeLocation.StartsWith(_loc.Path, StringComparison.OrdinalIgnoreCase))
			{
				var remove = _loc.Path.Length;
				if (remove < info.relativeLocation.Length && info.relativeLocation[remove] == '\\') remove++;
				info.relativeLocation = info.relativeLocation.Substring(remove);
			}

			info.starRects = new Rectangle[5];

			return info;
		}
		#endregion

		#region Context Menu
		private void OpenContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				//var lvi = c_list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				//if (lvi == null) return;

				//var item = lvi.Tag as ItemInfo;
				//if (item == null) return;

				//var fileName = item.img.LocationOnDisk;
				//if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				//{
				//	this.ShowError(Res.Error_ImageFileMissing);
				//	return;
				//}

				//System.Diagnostics.Process.Start(fileName);
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
				//var lvi = c_list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				//if (lvi == null) return;

				//var item = lvi.Tag as ItemInfo;
				//if (item == null) return;

				//var fileName = item.img.LocationOnDisk;
				//if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				//{
				//	this.ShowError(Res.Error_ImageFileMissing);
				//	return;
				//}

				//FileUtil.ExploreFile(fileName);
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
				//var lvi = c_list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				//if (lvi == null) return;

				//var item = lvi.Tag as ItemInfo;
				//if (item == null) return;

				//var fileName = item.img.LocationOnDisk;
				//if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				//{
				//	this.ShowError(Res.Error_ImageFileMissing);
				//	return;
				//}

				//if (MessageBox.Show(this, Res.Confirm_DeleteHistoryFile, Res.Confirm_DeleteHistoryFile_Caption,
				//	MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				//{
				//	MainWindow.Current.DeleteImageFile(fileName);
				//}
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
			c_deleteContextMenuItem.Enabled = selectCount == 1;
		}

		private void MainWindow_ImageFileDeleted(object sender, MainWindow.ImageFileEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => { MainWindow_ImageFileDeleted(sender, e); }));
				return;
			}

			// TODO: re-implement this
			//var delFileName = e.FileName;

			//foreach (var lvi in c_list.Items.Cast<ListViewItem>())
			//{
			//	var item = lvi.Tag as ItemInfo;
			//	if (delFileName.Equals(item.img.LocationOnDisk, StringComparison.OrdinalIgnoreCase))
			//	{
			//		c_list.Items.Remove(lvi);
			//		break;
			//	}
			//}

			RefreshStatusCounts();
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

		private void UpdateItemLayout(Graphics g)
		{
			_itemLayoutUpdateRequired = false;

			_itemHeight = (int)Math.Ceiling((float)k_rawItemHeight * g.DpiY / 96.0f);
			_totalHeight = _items.Count * _itemHeight;
			_spacer = (int)(4.0 * g.DpiX / 96.0f);

			_maxThumbWidth = (int)Math.Ceiling(k_rawThumbWidth * g.DpiX / 96.0f);
			_maxThumbHeight = _itemHeight - _spacer * 2;

			_clientRect = new Rectangle(0, 0, ClientSize.Width - c_vScroll.Width, ClientSize.Height - c_statusBar.Height);

			_visibleRect = _clientRect;
			_visibleRect.Offset(0, _scroll);

			UpdateScroll();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			if (_itemLayoutUpdateRequired) UpdateItemLayout(g);

			g.FillRectangle(SystemBrushes.Window, _clientRect);

			g.TranslateTransform(0.0f, (float)-_scroll);

			var topItem = _scroll / _itemHeight;
			var bottomItem = (_scroll + _clientRect.Height) / _itemHeight;

			for (var itemIndex = topItem; itemIndex <= bottomItem && itemIndex < _items.Count; itemIndex++)
			{
				var item = _items[itemIndex];
				var itemBounds = GetItemDocBounds(itemIndex);
				DrawItem(g, item, itemBounds);
			}
		}

		private void DrawItem(Graphics g, ItemInfo item, Rectangle itemBounds)
		{
			// Highlight
			var selected = ItemIsSelected(item.index);
			if (selected) g.FillRectangle(SystemBrushes.Highlight, itemBounds);

			// Border
			if (_borderPen == null) _borderPen = new Pen(SystemBrushes.Control);
			g.DrawRectangle(_borderPen, itemBounds);

			// Thumbnail
			var thumb = item.img.Thumbnail;
			if (thumb == null)
			{
				QueueThumbnailRetrieval(item.img);
			}
			else
			{
				var maxThumbRect = new Rectangle(itemBounds.Left + _spacer, itemBounds.Top + _spacer, _maxThumbWidth, _maxThumbHeight);

				var imgRect = new RectangleF(PointF.Empty, thumb.Size);
				imgRect = imgRect.ScaleRectWidth(_maxThumbWidth);
				if (imgRect.Height > _maxThumbHeight) imgRect = imgRect.ScaleRectHeight(_maxThumbHeight);
				imgRect = imgRect.CenterInside(maxThumbRect);

				if (!selected)
				{
					maxThumbRect.Inflate(1, 1);
					g.FillRectangle(SystemBrushes.Control, maxThumbRect);
				}

				g.DrawImage(thumb, imgRect);
			}

			var infoLeft = itemBounds.Left + _maxThumbWidth + _spacer * 2;
			var infoWidth = itemBounds.Right - infoLeft;
			if (infoWidth > 0)
			{
				var infoPt = new Point(infoLeft, itemBounds.Top + _spacer);

				// File Name
				var stringSize = g.MeasureString(item.relativeLocation, Font, infoWidth);
				g.DrawString(item.relativeLocation, Font, selected ? SystemBrushes.HighlightText : SystemBrushes.WindowText,
					new RectangleF(infoPt, stringSize));
				infoPt.Y += (int)Math.Ceiling(stringSize.Height) + _spacer;

				// Size
				var imgSize = item.img.Size;
				if (imgSize.HasValue)
				{
					// TODO: this should be drawn using a slightly smaller font
					var str = imgSize.Value.ToSizeString();
					stringSize = g.MeasureString(str, Font);
					g.DrawString(str, Font, selected ? SystemBrushes.HighlightText : SystemBrushes.WindowText,
						new RectangleF(infoPt, stringSize));
					infoPt.Y += (int)Math.Ceiling(stringSize.Height);
				}

				// Rating
				infoPt.Y += _spacer;
				var pt = infoPt;
				var starSize = new Size((int)Math.Round(Res.StarUnrated.Width * g.DpiX / 96.0f),
					(int)Math.Round(Res.StarUnrated.Height * g.DpiY / 96.0f));

				for (int i = 0; i < 5; i++)
				{
					item.starRects[i] = new Rectangle(pt, starSize);
					pt.X += starSize.Width;
				}

				var rating = item.img.Rating;
				if (item.mouseOverRating > 0 && item.mouseOverRating != rating)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (item.mouseOverRating >= i) g.DrawImage(Res.StarMouseOver1, item.starRects[i - 1]);
						else g.DrawImage(Res.StarMouseOver0, item.starRects[i - 1]);
					}
				}
				else if (rating > 0)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (rating >= i) g.DrawImage(Res.StarRated1, item.starRects[i - 1]);
						else g.DrawImage(Res.StarRated0, item.starRects[i - 1]);
					}
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						g.DrawImage(Res.StarUnrated, item.starRects[i]);
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

		private Rectangle GetItemDocBounds(int index)
		{
			return new Rectangle(_clientRect.Left, index * _itemHeight, _clientRect.Width, _itemHeight);
		}

		private bool ItemIsVisible(ItemInfo item)
		{
			var index = GetItemIndex(item);
			if (index == -1) return false;

			var itemBounds = GetItemDocBounds(index);
			return itemBounds.IntersectsWith(_visibleRect);
		}

		private int GetItemIndex(ItemInfo item)
		{
			var index = 0;
			foreach (var i in _items)
			{
				if (i == item) return index;
				index++;
			}
			return -1;
		}
		#endregion

		#region Scrolling
		private int _scroll;
		private Rectangle _visibleRect;

		private void UpdateScroll()
		{
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
			else if (value > _totalHeight) value = _totalHeight;

			if (value != c_vScroll.Value)
			{
				c_vScroll.Value = value;
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
		#endregion

		#region Keyboard
		private void LocationBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.PageDown)
				{
					SetScroll(_scroll + c_vScroll.LargeChange);
				}
				else if (e.KeyCode == Keys.PageUp)
				{
					SetScroll(_scroll - c_vScroll.LargeChange);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
		#endregion

		#region Thumbnail Retrieval
		private List<ImageRec> _thumbnailQueue = new List<ImageRec>();

		private void QueueThumbnailRetrieval(ImageRec img)
		{
			if (_thumbnailQueue.Contains(img)) return;

			System.Threading.ThreadPool.QueueUserWorkItem((x) =>
			{
				SetStatusMessage(string.Format("Getting Thumbnail: {0}", img.Location));
				using (var db = new Database())
				{
					img.Retrieve(db);
					var thumb = img.Thumbnail;
					img.Release();
					BeginInvoke(new Action(() => {  OnThumbnailUpdated(img); }));
				}
			});
		}

		private void OnThumbnailUpdated(ImageRec img)
		{
			var item = (from i in _items where i.img == img select i).FirstOrDefault();
			if (item != null)
			{
				if (ItemIsVisible(item)) Invalidate();
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
				Invalidate();
				EnableContextMenuItems();
			}
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
		#endregion
	}
}
