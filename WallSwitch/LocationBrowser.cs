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
		private Queue<ImageRec> _thumbnailLoadQueue;
		private Thread _thumbnailLoadThread;
		private volatile bool _kill;
		private ItemInfo _mouseOverItem;
		private ItemSorter _itemSorter = new ItemSorter();

		private const int k_imageColumn = 0;
		private const int k_pathColumn = 1;
		private const int k_ratingColumn = 2;
		private const int k_sizeColumn = 3;

		private class ItemInfo
		{
			public ListViewItem lvi;
			public ImageRec img;
			public string relativeLocation;
			public Rectangle[] starRects;
			public int mouseOverRating;
		}

		public LocationBrowser(Location loc)
		{
			if (loc == null) throw new ArgumentNullException(nameof(loc));

			_loc = loc;

			InitializeComponent();

			Text = _loc.Path;

			// Hack to set the height of each item
			var imgList = new ImageList();
			imgList.ImageSize = new Size(1, 100);
			c_list.SmallImageList = imgList;

			var prop = c_list.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			if (prop != null) prop.SetValue(c_list, true, null);

			MainWindow.Current.ImageFileDeleted += MainWindow_ImageFileDeleted;
		}

		private void LocationBrowser_Load(object sender, EventArgs e)
		{
			try
			{
				using (var db = new Database())
				{
					var table = db.SelectDataTable("select * from img where location_id = @location_id", "@location_id", _loc.RowId);
					var items = new List<ItemInfo>();
					foreach (DataRow row in table.Rows)
					{
						var img = ImageRec.FromDataRow(row);
						var item = CreateItemInfo(img);
						items.Add(item);
					}

					c_list.ListViewItemSorter = _itemSorter;

					foreach (var item in items)
					{
						var lvi = CreateLVI(item);
						c_list.Items.Add(lvi);

						if (item.img.Thumbnail == null)
						{
							if (_thumbnailLoadQueue == null) _thumbnailLoadQueue = new Queue<ImageRec>();
							_thumbnailLoadQueue.Enqueue(item.img);
							item.img.ThumbnailUpdated += Img_ThumbnailUpdated;
						}
					}
				}

				if (_thumbnailLoadQueue != null)
				{
					_thumbnailLoadThread = new Thread(new ThreadStart(ThumbnailLoadThreadProc));
					_thumbnailLoadThread.Name = "Thumbnail Load";
					_thumbnailLoadThread.Start();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void LocationBrowser_FormClosed(object sender, FormClosedEventArgs e)
		{
			_kill = true;

			MainWindow.Current.ImageFileDeleted -= MainWindow_ImageFileDeleted;
		}

		public Location LocationObject
		{
			get { return _loc; }
		}

		private void Img_ThumbnailUpdated(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => { Img_ThumbnailUpdated(sender, e); }));
				return;
			}

			var img = sender as ImageRec;
			if (img == null) return;

			var index = 0;
			foreach (ListViewItem lvi in c_list.Items)
			{
				if (((ItemInfo)lvi.Tag).img == img)
				{
					if (img.Size.HasValue)
					{
						lvi.SubItems[k_sizeColumn].Text = img.Size.Value.ToSizeString();
					}

					var rect = c_list.GetItemRect(index);
					if (rect.IntersectsWith(ClientRectangle))
					{
						c_list.Invalidate(rect);
					}
					break;
				}
				index++;
			}
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

		private ListViewItem CreateLVI(ItemInfo info)
		{
			var lvi = new ListViewItem();
			info.lvi = lvi;
			lvi.Tag = info;

			while (lvi.SubItems.Count < c_list.Columns.Count)
			{
				var col = c_list.Columns[lvi.SubItems.Count];
				switch (col.Tag.ToString())
				{
					case "path":
						lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, info.relativeLocation));
						break;
					case "rating":
					case "size":
						lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi,
							info.img.Size.HasValue ? info.img.Size.Value.ToSizeString() : string.Empty));
						break;
					default:
						throw new InvalidOperationException(string.Format("Invalid column tag '{0}'.", col.Tag));
				}
			}

			return lvi;
		}

		private void List_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			try
			{
				var info = (ItemInfo)e.Item.Tag;
				var col = e.Header;
				switch (col.Tag.ToString())
				{
					case "thumb":
						e.DrawBackground();
						if ((e.ItemState & ListViewItemStates.Selected) != 0) e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
						if (info.img.Thumbnail != null)
						{
							var thumb = info.img.Thumbnail;

							var rect = new RectangleF(0, 0, thumb.Size.Width, thumb.Size.Height);
							if (rect.Width > e.Bounds.Width) rect = rect.ScaleRectWidth(e.Bounds.Width);
							if (rect.Height > e.Bounds.Height) rect = rect.ScaleRectHeight(e.Bounds.Height);
							rect = rect.CenterInside(e.Bounds);
							var imgRect = new Rectangle((int)Math.Round(rect.Left), (int)Math.Round(rect.Top),
								(int)Math.Round(rect.Width), (int)Math.Round(rect.Height));

							e.Graphics.DrawImage(info.img.Thumbnail, imgRect);
						}
						break;

					case "rating":
						e.DrawBackground();
						if ((e.ItemState & ListViewItemStates.Selected) != 0) e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
						DrawRating(info, e.Bounds, e.Graphics);
						break;

					default:
						e.DrawDefault = true;
						break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void List_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void List_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
		}

		private void ThumbnailLoadThreadProc()
		{
			try
			{
				Log.Info("Loading {0} thumbnail(s) in background thread.", _thumbnailLoadQueue.Count);

				using (var db = new Database())
				{
					while (_thumbnailLoadQueue.Count > 0 && !_kill)
					{
						var img = _thumbnailLoadQueue.Dequeue();
						try
						{
							Log.Info("Loading thumbnail for image: {0}", img.Location);
							img.Retrieve(db);
							img.Release();
						}
						catch (Exception ex)
						{
							Log.Error(ex);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}
		}

		private void DrawRating(ItemInfo info, Rectangle bounds, Graphics g)
		{
			g.SetClip(bounds);

			const int spacer = 1;
			var starWidth = Res.StarUnrated.Width;
			var starHeight = Res.StarUnrated.Height;

			var center = bounds.Center();
			var rect = new Rectangle(center.X - (starWidth * 5 + spacer * 4) / 2, center.Y - starHeight / 2, starWidth, starHeight);

			if (info.mouseOverRating > 0 && info.mouseOverRating != info.img.Rating)
			{
				for (int i = 1; i <= 5; i++)
				{
					if (info.mouseOverRating >= i) g.DrawImage(Res.StarMouseOver1, rect);
					else g.DrawImage(Res.StarMouseOver0, rect);

					info.starRects[i - 1] = rect;
					rect.X += starWidth + spacer;
				}
			}
			else if (info.img.Rating > 0)
			{
				var rating = info.img.Rating;

				for (int i = 1; i <= 5; i++)
				{
					if (rating >= i) g.DrawImage(Res.StarRated1, rect);
					else g.DrawImage(Res.StarRated0, rect);

					info.starRects[i - 1] = rect;
					rect.X += starWidth + spacer;
				}
			}
			else
			{
				for (int i = 1; i <= 5; i++)
				{
					g.DrawImage(Res.StarUnrated, rect);

					info.starRects[i - 1] = rect;
					rect.X += starWidth + spacer;
				}
			}
		}

		private void List_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				var lvi = c_list.GetItemAt(e.X, e.Y);
				if (lvi != null)
				{
					var item = lvi.Tag as ItemInfo;
					if (_mouseOverItem != item)
					{
						if (_mouseOverItem != null)
						{
							if (_mouseOverItem.mouseOverRating != 0)
							{
								_mouseOverItem.mouseOverRating = 0;
								c_list.InvalidateItem(_mouseOverItem.lvi);
							}
						}
						_mouseOverItem = item;
					}

					var rating = HitTestRating(item, e.Location);
					if (rating != -1)
					{
						if (item.mouseOverRating != rating)
						{
							item.mouseOverRating = rating;
							c_list.InvalidateItem(item.lvi);
						}
					}
					else
					{
						if (item.mouseOverRating != 0)
						{
							item.mouseOverRating = 0;
							c_list.InvalidateItem(item.lvi);
						}
					}
				}
				else
				{
					if (_mouseOverItem != null) _mouseOverItem.mouseOverRating = 0;
					_mouseOverItem = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void List_MouseLeave(object sender, EventArgs e)
		{
			try
			{
				if (_mouseOverItem != null)
				{
					if (_mouseOverItem.mouseOverRating > 0)
					{
						_mouseOverItem.mouseOverRating = 0;
						c_list.InvalidateItem(_mouseOverItem.lvi);
					}
					_mouseOverItem = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private int HitTestRating(ItemInfo item, Point pt)
		{
			for (int r = 1; r <= 5; r++)
			{
				if (item.starRects[r - 1].Contains(pt)) return r;
			}

			if (pt.Y >= item.starRects[0].Y && pt.Y < item.starRects[0].Bottom &&
				pt.X < item.starRects[0].X && item.starRects[0].X - pt.X < item.starRects[0].Width)
			{
				return 0;
			}

			return -1;
		}

		private void List_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				var lvi = c_list.GetItemAt(e.X, e.Y);
				if (lvi != null)
				{
					var item = lvi.Tag as ItemInfo;
					var rating = HitTestRating(item, e.Location);
					if (rating != -1)
					{
						SetItemRating(item, rating);
						c_list.InvalidateItem(item.lvi);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void SetItemRating(ItemInfo item, int rating)
		{
			item.img.Rating = rating;

			using (var db = new Database())
			{
				using (var cmd = db.CreateCommand("update history set rating = @rating where path = @path"))
				{
					cmd.Parameters.AddWithValue("@rating", rating);
					cmd.Parameters.AddWithValue("@path", item.img.Location);
					cmd.ExecuteNonQuery();
				}

				using (var cmd = db.CreateCommand("update img set rating = @rating where path = @path"))
				{
					cmd.Parameters.AddWithValue("@rating", rating);
					cmd.Parameters.AddWithValue("@path", item.img.Location);
					cmd.ExecuteNonQuery();
				}
			}
		}

		private void List_ItemActivate(object sender, EventArgs e)
		{
			try
			{
				OpenContextMenuItem_Click(sender, e);
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void OpenContextMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var lvi = c_list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				if (lvi == null) return;

				var item = lvi.Tag as ItemInfo;
				if (item == null) return;

				var fileName = item.img.LocationOnDisk;
				if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				{
					this.ShowError(Res.Error_ImageFileMissing);
					return;
				}

				System.Diagnostics.Process.Start(fileName);
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
				var lvi = c_list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				if (lvi == null) return;

				var item = lvi.Tag as ItemInfo;
				if (item == null) return;

				var fileName = item.img.LocationOnDisk;
				if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				{
					this.ShowError(Res.Error_ImageFileMissing);
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
				var lvi = c_list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				if (lvi == null) return;

				var item = lvi.Tag as ItemInfo;
				if (item == null) return;

				var fileName = item.img.LocationOnDisk;
				if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
				{
					this.ShowError(Res.Error_ImageFileMissing);
					return;
				}

				if (MessageBox.Show(this, Res.Confirm_DeleteHistoryFile, Res.Confirm_DeleteHistoryFile_Caption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					MainWindow.Current.DeleteImageFile(fileName);
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void List_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				EnableContextMenuItems();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void EnableContextMenuItems()
		{
			var selectCount = c_list.SelectedItems.Count;

			c_openContextMenuItem.Enabled = selectCount == 1;
			c_exploreContextMenuItem.Enabled = selectCount == 1;
			c_deleteContextMenuItem.Enabled = selectCount == 1;
		}

		private void MainWindow_ImageFileDeleted(object sender, MainWindow.ImageFileEventArgs e)
		{
			var delFileName = e.FileName;

			foreach (var lvi in c_list.Items.Cast<ListViewItem>())
			{
				var item = lvi.Tag as ItemInfo;
				if (delFileName.Equals(item.img.LocationOnDisk, StringComparison.OrdinalIgnoreCase))
				{
					c_list.Items.Remove(lvi);
					break;
				}
			}
		}

		public enum CompareColumn
		{
			Path,
			Rating,
			Size
		}

		private class ItemSorter : System.Collections.IComparer
		{
			private List<CompareColumn> _cols = new List<CompareColumn>();
			private int _asc = 1;

			public ItemSorter()
			{
				_cols.Add(CompareColumn.Path);
				_cols.Add(CompareColumn.Rating);
				_cols.Add(CompareColumn.Size);
			}

			public int Compare(object x, object y)
			{
				var leftLvi = x as ListViewItem;
				var rightLvi = y as ListViewItem;
				if (leftLvi == null || rightLvi == null) throw new ArgumentException("Both objects must be a list view item.");

				var left = leftLvi.Tag as ItemInfo;
				var right = rightLvi.Tag as ItemInfo;
				var result = 0;

				foreach (var col in _cols)
				{
					switch (col)
					{
						case CompareColumn.Path:
							result = string.Compare(left.relativeLocation, right.relativeLocation, true) * _asc;
							break;
						case CompareColumn.Rating:
							result = left.img.Rating.CompareTo(right.img.Rating) * _asc;
							break;
						case CompareColumn.Size:
							result = left.img.Size.GetValueOrDefault().CompareTo(right.img.Size.GetValueOrDefault()) * _asc;
							break;
					}

					if (result != 0) return result;
				}

				return 0;
			}

			public void OnColumnClick(CompareColumn col)
			{
				if (col == _cols[0])
				{
					_asc = -_asc;
				}
				else
				{
					_cols.Remove(col);
					_cols.Insert(0, col);
					_asc = 1;
				}
			}
		}

		private void c_list_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			try
			{
				var col = c_list.Columns[e.Column];
				switch (col.Tag.ToString())
				{
					case "path":
						_itemSorter.OnColumnClick(CompareColumn.Path);
						c_list.Sort();
						break;
					case "rating":
						_itemSorter.OnColumnClick(CompareColumn.Rating);
						c_list.Sort();
						break;
					case "size":
						_itemSorter.OnColumnClick(CompareColumn.Size);
						c_list.Sort();
						break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}
	}
}
