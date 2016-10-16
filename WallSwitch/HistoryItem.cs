using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	class HistoryItem
	{
		private HistoryList _list;
		private CompressedImage _thumbnail;
		private string _location;
		private string _locationOnDisk;
		private Rectangle _bounds;
		private Rectangle _thumbnailRect;
		private Rectangle[] _starRects = new Rectangle[5];
		private int _rating;
		private int _mouseOverRating;

		private static SolidBrush _selBrush = null;

		public const int Margin = 2;
		public const int RatingSpacer = 1;
		public const int RatingImageSpacer = 2;
		public const int SelectColorFade = 128;

		public static HistoryItem FromImageRec(HistoryList list, Database db, ImageRec rec)
		{
			if (rec == null) throw new ArgumentNullException(nameof(rec));

			var ret = new HistoryItem(list);

			ret._thumbnail = rec.Thumbnail;
			ret._location = rec.Location;
			ret._locationOnDisk = rec.GetLocationOnDisk(db);
			ret._rating = rec.Rating;

			return ret;
		}

		public static HistoryItem FromDataRow(HistoryList list, DataRow row)
		{
			if (row == null) throw new ArgumentNullException(nameof(row));

			var ret = new HistoryItem(list);

			ret._location = row.GetString("path");
			ret._locationOnDisk = row.GetString("path");
			ret._rating = row.GetInt("rating");

			var bytes = row.GetBytes("thumb");
			if (bytes != null)
			{
				ret._thumbnail = new CompressedImage(bytes);
			}

			return ret;
		}

		private HistoryItem(HistoryList list)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));

			_list = list;
		}

		public CompressedImage Thumbnail
		{
			get { return _thumbnail; }
		}

		public string Location
		{
			get { return _location; }
		}

		public string LocationOnDisk
		{
			get { return _locationOnDisk; }
		}

		public Rectangle Bounds
		{
			get { return _bounds; }
		}

		public int Rating
		{
			get { return _rating; }
			set
			{
				if (value < 0 || value > 5) throw new ArgumentOutOfRangeException();
				_rating = value;
			}
		}

		public static Size GetRequiredSize(Size thumbnailSize)
		{
			var ratingHeight = Res.StarUnrated.Height;
			var ratingWidth = (Res.StarUnrated.Width + RatingSpacer) * 5;

			var widthRequired = thumbnailSize.Width;
			if (widthRequired < ratingWidth) widthRequired = ratingWidth;
			widthRequired += Margin * 2;

			int heightRequired = thumbnailSize.Height + Margin * 2 + ratingHeight + RatingImageSpacer;

			return new Size(widthRequired, heightRequired);
		}

		public void SetBounds(Rectangle bounds, Size thumbnailSize)
		{
			_bounds = bounds;

			if (_thumbnail != null)
			{
				var thumbRect = new RectangleF(bounds.Left + Margin, bounds.Top + Margin, thumbnailSize.Width, thumbnailSize.Height);

				var imgRect = new RectangleF(PointF.Empty, _thumbnail.Size);
				imgRect = imgRect.ScaleRectWidth(thumbnailSize.Width);
				if (imgRect.Height > thumbnailSize.Height) imgRect = imgRect.ScaleRectHeight(thumbnailSize.Height);

				thumbRect = imgRect.CenterInside(thumbRect);
				_thumbnailRect = new Rectangle((int)Math.Round(thumbRect.Left), (int)Math.Round(thumbRect.Top),
					(int)Math.Round(thumbRect.Width), (int)Math.Round(thumbRect.Height));
			}
			else
			{
				_thumbnailRect = Rectangle.Empty;
			}

			var starWidth = Res.StarUnrated.Width;
			var starHeight = Res.StarUnrated.Height;
			var ratingTop = _bounds.Bottom - Margin - starHeight;
			var ratingLeft = bounds.Left + (bounds.Width - (starWidth + RatingSpacer) * 5) / 2;

			for (int s = 0; s < 5; s++)
			{
				_starRects[s] = new Rectangle(ratingLeft, ratingTop, starWidth, starHeight);
				ratingLeft += starWidth + RatingSpacer;
			}
		}

		public void Draw(Graphics g, bool selected)
		{
			try
			{
				if (_thumbnail != null)
				{
					lock (_thumbnail)
					{
						g.DrawImage(_thumbnail, _thumbnailRect);
					}
				}

				// Border
				g.DrawRectangle(SystemPens.ControlLight, _bounds);

				// Rating
				if (_mouseOverRating > 0 && _mouseOverRating != _rating)
				{
					for (int r = 1; r <= 5; r++)
					{
						g.DrawImage(_mouseOverRating >= r ? Res.StarMouseOver1 : Res.StarMouseOver0, _starRects[r - 1]);
					}
				}
				else if (_rating > 0)
				{
					for (int r = 1; r <= 5; r++)
					{
						g.DrawImage(_rating >= r ? Res.StarRated1 : Res.StarRated0, _starRects[r - 1]);
					}
				}
				else
				{
					for (int r = 1; r <= 5; r++)
					{
						g.DrawImage(Res.StarUnrated, _starRects[r - 1]);
					}
				}

				// Selection
				if (selected)
				{
					if (_selBrush == null)
					{
						Color color = SystemColors.Highlight;
						_selBrush = new SolidBrush(Color.FromArgb(SelectColorFade, color.R, color.G, color.B));
					}
					g.FillRectangle(_selBrush, _bounds);
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when drawing history item.");
			}
		}

		public int RatingHitTest(Point pt)
		{
			for (int r = 0; r < 5; r++)
			{
				if (_starRects[r].Contains(pt)) return r + 1;
			}

			// If mouse is just to the left of the first star, then consider this a 'zero' rating.
			if (pt.Y >= _starRects[0].Top && pt.Y < _starRects[0].Bottom &&
				pt.X < _starRects[0].Left && _starRects[0].Left - pt.X <= _starRects[0].Width)
			{
				return 0;
			}

			return -1;
		}

		public void OnMouseOver(Point pt)
		{
			var rating = RatingHitTest(pt);
			if (rating >= 0)
			{
				if (_mouseOverRating != rating)
				{
					_mouseOverRating = rating;
					_list.InvalidateItem(this);
				}
			}
			else
			{
				if (_mouseOverRating != 0)
				{
					_mouseOverRating = 0;
					_list.InvalidateItem(this);
				}
			}
		}

		public void OnMouseLeave()
		{
			if (_mouseOverRating != 0)
			{
				_mouseOverRating = 0;
				_list.InvalidateItem(this);
			}
		}

		public void SetRating(int rating, bool saveToDb)
		{
			_rating = rating;
			_list.InvalidateItem(this);

			if (saveToDb)
			{
				using (var db = new Database())
				{
					using (var cmd = db.CreateCommand("update history set rating = @rating where path = @path"))
					{
						cmd.Parameters.AddWithValue("@rating", rating);
						cmd.Parameters.AddWithValue("@path", _location);
						cmd.ExecuteNonQuery();
					}

					using (var cmd = db.CreateCommand("update img set rating = @rating where path = @path"))
					{
						cmd.Parameters.AddWithValue("@rating", rating);
						cmd.Parameters.AddWithValue("@path", _location);
						cmd.ExecuteNonQuery();
					}
				}
			}
		}
	}
}
