using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	class WallpaperRenderer : IDisposable
	{
		private Size _fullSize = new Size(0, 0);
		private List<Rectangle> _screenRects = new List<Rectangle>();
		private Bitmap _bitmap = null;
		private Graphics _g = null;
		private Theme _theme = null;
		private Random _rand = new Random();
		private bool _firstRender = true;

		public WallpaperRenderer()
		{ }

		public void Dispose()
		{
			if (_g != null)
			{
				_g.Dispose();
				_g = null;
			}

			if (_bitmap != null)
			{
				lock (_bitmap)
				{
					_bitmap.Dispose();
				}
				_bitmap = null;
			}
		}

		public Bitmap Bitmap
		{
			get { return _bitmap; }
		}

		public bool InitFrame(Rectangle[] screenRects, Theme theme, Bitmap lastImage)
		{
			if (screenRects == null) throw new ArgumentNullException("screenRects");
			if (theme == null) throw new ArgumentNullException("theme");
			_theme = theme;

			_fullSize = new Size(0, 0);
			foreach (var rect in screenRects)
			{
				if (rect.Left + rect.Width > _fullSize.Width) _fullSize.Width = rect.Left + rect.Width;
				if (rect.Top + rect.Height > _fullSize.Height) _fullSize.Height = rect.Top + rect.Height;
			}

			if (_fullSize.Width <= 0 || _fullSize.Height <= 0)
			{
				_bitmap = null;
				_g = null;
				return false;
			}

			if (lastImage != null && lastImage.Width == _fullSize.Width && lastImage.Height == _fullSize.Height)
			{
				_bitmap = lastImage;
				_g = Graphics.FromImage(_bitmap);
				_firstRender = false;
			}
			else
			{
				_bitmap = new Bitmap(_fullSize.Width, _fullSize.Height);
				_g = Graphics.FromImage(_bitmap);
				_g.Clear(Color.Black);
				_firstRender = true;
			}

			if (_theme.Mode == ThemeMode.Collage &&
				lastImage != null && lastImage.Width == _bitmap.Width && lastImage.Height == _bitmap.Height)
			{
				DrawBackground(lastImage);
			}

			return true;
		}

		private void DrawBackground(Bitmap lastImage)
		{
			var image = lastImage;

			//BlurImage(image, 10);
			//BlurImageTest(image, 5);

			if (_theme.ColorEffectBack == ColorEffect.None || _theme.ColorEffectBackRatio <= 0)
			{
				_g.DrawImage(image, new Point(0, 0));
			}
			else
			{
				var ratio = _theme.ColorEffectBackRatio / 100.0f;

				var imgAttrib = new ImageAttributes();
				imgAttrib.SetColorMatrix(ColorEffect.None.GetColorMatrixBlend(_theme.ColorEffectBack, ratio));
				_g.DrawImage(image, new Rectangle(0, 0, lastImage.Width, lastImage.Height), 0.0f, 0.0f, lastImage.Width, lastImage.Height,
					GraphicsUnit.Pixel, imgAttrib);
			}
		}

		public void DrawScreen(ImageRec file, Rectangle thisScreenRect)
		{
			try
			{
				if (file == null)
				{
					Log.Write(LogLevel.Warning, "No image file passed.");
					ClearBackground(thisScreenRect, 255);
				}
				else if (!file.IsPresent)
				{
					Log.Write(LogLevel.Warning, "No image object is loaded.");
					ClearBackground(thisScreenRect, 255);
				}
				else
				{
					Image img = file.Image;
					Image imgToDraw = null;
					lock (img)
					{
						bool clearRequired = false;
						int clearOpacity = 255;
						float imgWidth = img.Width;
						float imgHeight = img.Height;
						if (imgWidth <= 0.0f || imgHeight <= 0.0f) return;

						//Log.Write("Original image size: {0} x {1}", imgWidth, imgHeight);

						RectangleF screenRect = thisScreenRect;
						RectangleF imgRect = new RectangleF(0, 0, img.Width, img.Height);
						RectangleF srcRect = imgRect;

						_g.SetClip(screenRect);

						if (_theme.Mode == ThemeMode.Collage)
						{
							// The maximum image size will be determined by the number of pixels (area), not by the max width/height.
							// This will provide a more consistent image size when there are pictures with different aspect ratios.
							float imgArea = imgWidth * imgHeight;
							float imgSize = (float)_theme.ImageSize / 100.0f;
							float screenArea = screenRect.Width * imgSize * screenRect.Height * imgSize;
							float scale = (float)Math.Sqrt(screenArea / imgArea);

							if (_theme.MaxImageScale > 0)
							{
								var maxScale = (float)_theme.MaxImageScale / 100.0f;
								if (maxScale > 0.0f && scale > maxScale) scale = maxScale;
							}
							imgWidth *= scale;
							imgHeight *= scale;

							// If one of the dimensions is still wider/taller than the screen, then scale it down more.
							if (imgWidth > screenRect.Width)
							{
								float ratio = screenRect.Width / imgWidth;
								imgWidth = screenRect.Width;
								imgHeight *= ratio;
								scale *= ratio;
							}

							if (imgHeight > screenRect.Height)
							{
								float ratio = screenRect.Height / imgHeight;
								imgHeight = screenRect.Height;
								imgWidth *= ratio;
								scale *= ratio;
							}

							// Choose a random rect to display the image.
							imgRect.X = (float)(_rand.NextDouble() * (screenRect.Width - imgWidth)) + screenRect.X;
							imgRect.Y = (float)(_rand.NextDouble() * (screenRect.Height - imgHeight)) + screenRect.Y;
							imgRect.Width = imgWidth;
							imgRect.Height = imgHeight;

							if (_firstRender) clearOpacity = 255;
							else clearOpacity = _theme.BackOpacity255;
							clearRequired = true;

							// Feather edges
							if (scale > 0.0f && _theme.Feather && _theme.FeatherDist > 0)
							{
								int featherWidth = (int)((float)_theme.FeatherDist / scale);
								if (featherWidth > 0) imgToDraw = FeatherImage(img, featherWidth);
								else imgToDraw = img;
							}
							else
							{
								imgToDraw = img;
							}

							if (_theme.DropShadow) DrawDropShadow(imgRect);
						}
						else // Sequential or Random - one image per screen
						{
							imgRect = FitImage(imgRect, ref srcRect, screenRect, _theme.ImageFit, img.Size, ref clearRequired);
							imgToDraw = img;
						}

						if (clearRequired && clearOpacity > 0) ClearBackground(thisScreenRect, clearOpacity);

						if (_theme.ColorEffectFore == ColorEffect.None)
						{
							_g.DrawImage(imgToDraw, imgRect, srcRect, GraphicsUnit.Pixel);
						}
						else
						{
							_g.DrawImageWithColorMatrix(imgToDraw, imgRect, srcRect, _theme.ColorEffectFore.GetColorMatrix());
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when drawing image '{0}':", file);
			}
		}

		public void DrawBlank(Rectangle screenRect)
		{
			if (_theme.Mode != ThemeMode.Collage)
			{
				ClearBackground(screenRect, 255);
			}
		}

		private void ClearBackground(Rectangle screenRect, int opacity)
		{
			Color topColor = Color.FromArgb(opacity, _theme.BackColorTop.R, _theme.BackColorTop.G, _theme.BackColorTop.B);
			Color bottomColor = Color.FromArgb(opacity, _theme.BackColorBottom.R, _theme.BackColorBottom.G, _theme.BackColorBottom.B);

			Brush brush;
			if (topColor.ToArgb() == bottomColor.ToArgb())
			{
				// Solid color
				brush = new SolidBrush(topColor);
			}
			else
			{
				// Gradient
				brush = new LinearGradientBrush(screenRect, topColor, bottomColor, LinearGradientMode.Vertical);
			}

			_g.FillRectangle(brush, screenRect);
			brush.Dispose();
		}

		private RectangleF FitImage(RectangleF imgRect, ref RectangleF srcRect, RectangleF screenRect, ImageFit fit, SizeF imgSize, ref bool clearBackground)
		{
			SizeF screenSize = screenRect.Size;
			RectangleF origImgRect = imgRect;

			switch (fit)
			{
				case ImageFit.Original:
					imgRect = imgRect.CenterInside(screenRect);
					break;

				case ImageFit.Stretch:
					if (_theme.MaxImageScale > 0)
					{
						var maxScale = (float)_theme.MaxImageScale / 100.0f;
						imgRect = new RectangleF(0.0f, 0.0f, imgSize.Width * maxScale, imgSize.Height * maxScale);
						if (imgRect.Width > screenRect.Width) imgRect.Width = screenRect.Width;
						if (imgRect.Height > screenRect.Height) imgRect.Height = screenRect.Height;

						imgRect = imgRect.CenterInside(screenRect);
					}
					else
					{
						imgRect = screenRect;
					}
					break;

				case ImageFit.Fit:
					if (imgRect.Width != screenRect.Width) imgRect = imgRect.ScaleRectWidth(screenRect.Width);
					if (imgRect.Height > screenRect.Height) imgRect = imgRect.ScaleRectHeight(screenRect.Height);
					imgRect = imgRect.CenterInside(screenRect);
					CheckImageRectSize(_theme, ref imgRect, imgSize);
					break;

				case ImageFit.Fill:
					if (imgRect.Width < screenRect.Width) imgRect = imgRect.ScaleRectWidth(screenRect.Width);
					if (imgRect.Height < screenRect.Height) imgRect = imgRect.ScaleRectHeight(screenRect.Height);
					imgRect = imgRect.CenterInside(screenRect);
					CheckImageRectSize(_theme, ref imgRect, imgSize);
					break;

				default:
					throw new ArgumentException("Invalid image fit value.");
			}

			clearBackground = !screenRect.CompletelyInside(imgRect);
			return imgRect;
		}

		private Image FeatherImage(Image image, int feather)
		{
			// Create the destination bitmap
			Bitmap dstBitmap = new Bitmap(image);

			int imageWidth = image.Width;
			int imageHeight = image.Height;

			if (feather > imageWidth / 2) feather = imageWidth / 2;
			if (feather > imageHeight / 2) feather = imageHeight / 2;
			if (feather <= 0) return dstBitmap;

			Bitmap featherBitmap = CreateFeatherImage(imageWidth, imageHeight, feather);

			// Apply the feather bitmap to the alpha channel of the wallpaper image.
			TransferChannel(featherBitmap, ChannelARGB.Red, dstBitmap, ChannelARGB.Alpha);
			return dstBitmap;
		}

		private Bitmap CreateFeatherImage(int width, int height, int feather)
		{
			Bitmap bmp = new Bitmap(width, height);
			Rectangle imageRect = new Rectangle(0, 0, width, height);

			Graphics g = Graphics.FromImage(bmp);
			g.FillRectangle(Brushes.Black, imageRect);

			int feather2 = feather * 2;

			// Center
			g.FillRectangle(Brushes.White, new Rectangle(feather, feather, width - feather2, height - feather2));

			// Left edge
			DrawFeatherEdge(g, new Rectangle(0, feather, feather, height - feather * 2), LinearGradientMode.Horizontal, Color.Black, Color.White);

			// Top edge
			DrawFeatherEdge(g, new Rectangle(feather, 0, width - feather * 2, feather), LinearGradientMode.Vertical, Color.Black, Color.White);

			// Right edge
			DrawFeatherEdge(g, new Rectangle(width - feather, feather, feather, height - feather * 2), LinearGradientMode.Horizontal, Color.White, Color.Black);

			// Bottom edge
			DrawFeatherEdge(g, new Rectangle(feather, height - feather, width - feather * 2, feather), LinearGradientMode.Vertical, Color.White, Color.Black);

			// Top-left corner
			DrawFeatherArc(g, new Rectangle(0, 0, feather2, feather2), new Point(feather, feather), 180.0f, 90.0f);

			// Top-right corner
			DrawFeatherArc(g, new Rectangle(width - feather2, 0, feather2, feather2), new Point(width - feather, feather), 270.0f, 90.0f);

			// Bottom-right corner
			DrawFeatherArc(g, new Rectangle(width - feather2, height - feather2, feather2, feather2), new Point(width - feather, height - feather), 0.0f, 90.0f);

			// Bottom-left corner
			DrawFeatherArc(g, new Rectangle(0, height - feather2, feather2, feather2), new Point(feather, height - feather), 90.0f, 90.0f);

			return bmp;
		}

		private void DrawFeatherEdge(Graphics g, Rectangle rect, LinearGradientMode gradMode, Color color1, Color color2)
		{
			LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, gradMode);
			brush.WrapMode = WrapMode.TileFlipXY;
			g.FillRectangle(brush, rect);
		}

		private void DrawFeatherArc(Graphics g, Rectangle rect, Point center, float startAngle, float arcAngle)
		{
			GraphicsPath path = new GraphicsPath();
			path.AddPie(rect, startAngle, arcAngle);

			PathGradientBrush brush = new PathGradientBrush(path);
			brush.CenterColor = Color.White;
			brush.SurroundColors = new Color[] { Color.Black };
			brush.CenterPoint = center;

			Blend blend = new Blend();
			blend.Factors = new float[] { 0.0f, 1.0f };
			blend.Positions = new float[] { 0.0f, 1.0f };
			brush.Blend = blend;

			g.FillPath(brush, path);
		}

		private enum ChannelARGB : int
		{
			Blue = 0,
			Green = 1,
			Red = 2,
			Alpha = 3
		}

		private void TransferChannel(Bitmap srcBitmap, ChannelARGB srcChannel, Bitmap dstBitmap, ChannelARGB dstChannel)
		{
			if (srcBitmap.Size != dstBitmap.Size) throw new InvalidOperationException("Source and destination bitmaps must be the same size.");

			unsafe
			{
				Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);

				BitmapData srcData = srcBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				BitmapData dstData = dstBitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				try
				{
					byte* srcPtr = (byte*)srcData.Scan0.ToPointer();
					byte* dstPtr = (byte*)dstData.Scan0.ToPointer();

					dstPtr += (int)dstChannel;
					srcPtr += (int)srcChannel;

					int numPixels = srcBitmap.Width * srcBitmap.Height;
					for (int i = 0; i < numPixels; i++)
					{
						*dstPtr = *srcPtr;
						srcPtr += 4;
						dstPtr += 4;
					}
				}
				finally
				{
					dstBitmap.UnlockBits(dstData);
					srcBitmap.UnlockBits(srcData);
				}
			}
		}

		private void CheckImageRectSize(Theme theme, ref RectangleF rect, SizeF imgSize)
		{
			if (theme.MaxImageScale > 0)
			{
				var maxScale = (float)theme.MaxImageScale / 100.0f;
				var maxSize = new SizeF(imgSize.Width * maxScale, imgSize.Height * maxScale);
				if (rect.Width > maxSize.Width || rect.Height > maxSize.Height)
				{
					rect = rect.RestrictSize(maxSize).CenterInside(rect);
				}
			}
		}

		private void DrawDropShadow(RectangleF imgRect)
		{
			RectangleF shadowRect = imgRect;
			shadowRect.Offset(_theme.DropShadowDist, _theme.DropShadowDist);

			if (_theme.DropShadowFeather)
			{
				var alpha = (float)_theme.DropShadowOpacity / 100.0f;
				var matrixElements = new float[][]
					{
						new float[] {0, 0, 0, alpha, 0},
						new float[] {0, 0, 0, 0, 0},
						new float[] {0, 0, 0, 0, 0},
						new float[] {0, 0, 0, 0, 0},
						new float[] {0, 0, 0, 0, 1}
					};

				var featherBitmap = CreateFeatherImage((int)Math.Round(shadowRect.Width), (int)Math.Round(shadowRect.Height), _theme.DropShadowFeatherDist);
				var colorMatrix = new ColorMatrix(matrixElements);
				_g.DrawImageWithColorMatrix(featherBitmap, shadowRect, colorMatrix);
			}
			else
			{
				_g.FillRectangle(Brushes.Black, shadowRect);
			}
		}

		// Not enabled yet because it's a huge CPU hog.  Seeking alternatives.
		private unsafe void BlurImage(Bitmap img, int dist)
		{
			var startTime = DateTime.Now;

			var filterLen = dist * 2 + 1;
			var filter = new int[filterLen];
			for (var f = 0; f < filterLen; f++)
			{
				filter[f] = (int)(GaussElement(f, filterLen) * 256.0);
			}

			var imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			try
			{
				byte* imgBits = (byte*)imgData.Scan0;
				int stride = imgData.Stride;

				int width = img.Width;
				int height = img.Height;
				int x, y, off, i, lineOff;
				int r, g, b, a, min, max, filterVal;
				int start, end;

				// Horizontal blur
				int[] buf = new int[width * 4];

				for (y = 0; y < height; y++)
				{
					lineOff = stride * y;

					// Copy data into buffer
					for (off = 0, end = width * 4; off < end; off++)
					{
						buf[off] = *(byte*)(imgBits + lineOff + off);
					}

					for (x = 0; x < width; x++)
					{
						min = x - dist;
						max = x + dist;
						start = min < 0 ? 0 : min;
						end = max >= width ? width - 1 : max;

						r = g = b = a = 0;

						for (i = start; i <= end; i++)
						{
							filterVal = filter[i - min];
							off = i * 4;
							a += (buf[off + 3] * filterVal) >> 8;
							r += (buf[off + 2] * filterVal) >> 8;
							g += (buf[off + 1] * filterVal) >> 8;
							b += (buf[off + 0] * filterVal) >> 8;
						}

						*(int*)(imgBits + lineOff + x * 4) = (int)b | ((int)g << 8) | ((int)r << 16) | ((int)a << 24);
					}
				}

				// Vertical blur
				int xOff;

				buf = new int[height * 4];
				for (x = 0; x < width; x++)
				{
					xOff = x * 4;

					for (y = 0; y < height; y++)
					{
						off = stride * y + xOff;
						i = y * 4;
						buf[i + 0] = *(byte*)(imgBits + off + 0);
						buf[i + 1] = *(byte*)(imgBits + off + 1);
						buf[i + 2] = *(byte*)(imgBits + off + 2);
						buf[i + 3] = *(byte*)(imgBits + off + 3);
					}

					for (y = 0; y < height; y++)
					{
						min = y - dist;
						max = y + dist;
						start = min < 0 ? 0 : min;
						end = max >= height ? height - 1 : max;

						r = g = b = a = 0;

						for (i = start; i <= end; i++)
						{
							filterVal = filter[i - min];

							off = i * 4;
							a += (buf[off + 3] * filterVal) >> 8;
							r += (buf[off + 2] * filterVal) >> 8;
							g += (buf[off + 1] * filterVal) >> 8;
							b += (buf[off + 0] * filterVal) >> 8;
						}

						*(int*)(imgBits + stride * y + xOff) = (int)b | ((int)g << 8) | ((int)r << 16) | ((int)a << 24);
					}
				}
			}
			finally
			{
				img.UnlockBits(imgData);
			}

			Log.Write(LogLevel.Info, "Blur completed in {0} msec", DateTime.Now.Subtract(startTime).TotalMilliseconds);
		}

		double Gauss(double x, double m, double d)
		{
			return 1.0 / (d * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, ((x - m) * (x - m)) * -1 / 2 * d * d);
		}

		double GaussElement(int index, int arrayLength)
		{
			int center = arrayLength / 2;
			double extent = arrayLength - center;
			double x = (double)(center - index) / extent * 3.0;
			return Gauss(x, 0, 1) / (extent / 3.0);
		}

		private void BlurImageTest(Bitmap img, int dist)
		{
			var width = img.Width / dist;
			var height = img.Height / dist;
			if (width < 1) width = 1;
			if (height < 1) height = 1;

			var bm = new Bitmap(width, height);
			var g = Graphics.FromImage(bm);
			g.InterpolationMode = InterpolationMode.High;
			g.DrawImage(img, new Rectangle(0, 0, width, height));

			var g2 = Graphics.FromImage(img);
			g2.InterpolationMode = InterpolationMode.High;
			g2.DrawImage(bm, new Rectangle(0, 0, img.Width, img.Height));
		}
	}
}
