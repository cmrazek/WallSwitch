using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch.ImageFilters
{
	public partial class RatingControl : UserControl
	{
		private Rectangle[] _starRects = new Rectangle[5];
		private Rectangle _unratedRect;
		
		private int _rating;
		private int _mouseOverRating;
		private bool _refreshLayoutRequired;

		public event EventHandler RatingChanged;

		public RatingControl()
		{
			DoubleBuffered = true;

			InitializeComponent();
		}

		private void RatingControl_Load(object sender, EventArgs e)
		{
			try
			{
				_refreshLayoutRequired = true;
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RatingControl_Resize(object sender, EventArgs e)
		{
			try
			{
				_refreshLayoutRequired = true;
				Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RefreshLayout(Graphics g)
		{
			var starSize = new Size((int)Math.Round(Images.StarUnrated.Size.Width * g.DpiX / 96.0),
				(int)Math.Round(Images.StarUnrated.Size.Height * g.DpiY / 96.0));
			var totalWidth = starSize.Width * 5;

			var rect = new Rectangle((ClientSize.Width - totalWidth) / 2, (ClientSize.Height - starSize.Height) / 2,
				starSize.Width, starSize.Height);

			_unratedRect = rect.OffsetInline(-starSize.Width, 0);

			for (int i = 0; i < 5; i++)
			{
				_starRects[i] = rect;
				rect.X += starSize.Width;
			}
		}

		private void RatingControl_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				var g = e.Graphics;

				if (_refreshLayoutRequired)
				{
					_refreshLayoutRequired = false;
					RefreshLayout(g);
				}

				if (_mouseOverRating > 0 && _mouseOverRating != _rating)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (_mouseOverRating >= i) g.DrawImage(Images.StarMouseOver1, _starRects[i - 1]);
						else g.DrawImage(Images.StarMouseOver0, _starRects[i - 1]);
					}
				}
				else if (_rating > 0)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (_rating >= i) g.DrawImage(Images.StarRated1, _starRects[i - 1]);
						else g.DrawImage(Images.StarRated0, _starRects[i - 1]);
					}
				}
				else
				{
					for (int i = 1; i <= 5; i++)
					{
						g.DrawImage(Images.StarUnrated, _starRects[i - 1]);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RatingControl_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				var rating = HitTestRating(e.Location);
				if (rating != -1 && rating != _rating)
				{
					Rating = rating;
					Invalidate();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RatingControl_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				var rating = HitTestRating(e.Location);
				if (rating != -1)
				{
					if (_mouseOverRating != rating)
					{
						_mouseOverRating = rating;
						Invalidate();
					}
				}
				else
				{
					if (_mouseOverRating > 0)
					{
						_mouseOverRating = 0;
						Invalidate();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private void RatingControl_MouseLeave(object sender, EventArgs e)
		{
			try
			{
				if (_mouseOverRating > 0)
				{
					_mouseOverRating = 0;
					Invalidate();
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
		}

		private int HitTestRating(Point pt)
		{
			if (_unratedRect.Contains(pt)) return 0;

			for (int i = 0; i < 5; i++)
			{
				if (_starRects[i].Contains(pt)) return i + 1;
			}

			return -1;
		}

		public int Rating
		{
			get { return _rating; }
			set
			{
				if (value < 0 || value > 5) throw new ArgumentOutOfRangeException();
				if (_rating != value)
				{
					_rating = value;
					RatingChanged?.Invoke(this, EventArgs.Empty);
					Invalidate();
				}
			}
		}
	}
}
