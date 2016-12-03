using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WallSwitch
{
	class WallpaperRenderer : IDisposable
	{
		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int BlurImage(IntPtr pImageBits, int width, int height, int iImageFormat, int stride, int blurDist);

		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int TransferChannel32(IntPtr pSrcImageBits, int srcChannel, int srcStride, IntPtr pDstImageBits, int dstChannel, int dstStride, int width, int height);

		private Size _fullSize = new Size(0, 0);
		private List<Rectangle> _screenRects = new List<Rectangle>();
		private Bitmap _bitmap = null;
		private Graphics _g = null;
		private Theme _theme = null;
		private Random _rand = new Random();

		private const int k_randomRectRetries = 10;

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

		public Bitmap WallpaperImage
		{
			get { return _bitmap; }
		}

		public bool InitFrame(Rectangle[] screenRects, Theme theme, Bitmap lastImage)
		{
			if (screenRects == null) throw new ArgumentNullException("screenRects");
			if (theme == null) throw new ArgumentNullException("theme");
			_theme = theme;

			_fullSize = new Size(0, 0);
			_screenRects.Clear();
			foreach (var rect in screenRects)
			{
				_screenRects.Add(rect);
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
			}
			else
			{
				_bitmap = new Bitmap(_fullSize.Width, _fullSize.Height);
				_g = Graphics.FromImage(_bitmap);
				_g.Clear(Color.Black);
				_theme.ClearImageRectHistory();
			}

			if (_theme.Mode == ThemeMode.Collage &&
				lastImage != null && lastImage.Width == _bitmap.Width && lastImage.Height == _bitmap.Height)
			{
				DrawLastBackgroundImage(lastImage);
			}

			if (_theme.Mode == ThemeMode.Collage)
			{
				if (_theme.BackOpacity255 > 0)
				{
					foreach (var screenRect in screenRects)
					{
						DrawBackgroundColor(screenRect, _theme.BackOpacity255);
					}
				}
			}
			else
			{
				if (_theme.BackColorTop != Color.Black || _theme.BackColorBottom != Color.Black)
				{
					foreach (var screenRect in screenRects)
					{
						DrawBackgroundColor(screenRect, 255);
					}
				}
			}

			return true;
		}

		private void DrawLastBackgroundImage(Bitmap lastImage)
		{
			var image = lastImage;

			if (_theme.BackgroundBlur && _theme.BackgroundBlurDist > 0)
			{
				BlurImage(image, _theme.BackgroundBlurDist);
			}

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

		public void RenderCollageImageOnScreen(Database db, ImageRec file, Rectangle screenRect)
		{
			try
			{
				if (file == null || !file.IsPresent) return;

				_g.SetClip(screenRect);

				var img = file.Image;
				lock (img)
				{
					// The maximum image size will be determined by the number of pixels (area), not by the max width/height.
					// This will provide a more consistent image size when there are pictures with different aspect ratios.
					float imgWidth = file.Image.Width;
					float imgHeight = file.Image.Height;
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

					var imgRect = GetRandomImageRect(db, screenRect, imgWidth, imgHeight);
					var imgToDraw = img;
					RectangleF srcRect = new RectangleF(0, 0, img.Width, img.Height);

					// Feather edges
					if (scale > 0.0f)
					{
						switch (_theme.EdgeMode)
						{
							case EdgeMode.Feather:
								if (_theme.EdgeDist > 0)
								{
									int featherWidth = (int)((float)_theme.EdgeDist / scale);
									if (featherWidth > 0) imgToDraw = FeatherImage(img, featherWidth);
									else imgToDraw = img;
								}
								break;

							case EdgeMode.SolidBorder:
								if (_theme.EdgeDist > 0)
								{
									int edgeWidth = (int)((float)_theme.EdgeDist / scale);
									if (edgeWidth > 0) imgToDraw = CreateSolidBorderImage(img, edgeWidth, _theme.BorderColor, ref srcRect);
									else imgToDraw = img;
								}
								break;

							case EdgeMode.None:
								imgToDraw = img;
								break;
						}
					}

					if (_theme.DropShadow) DrawDropShadow(imgRect);

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
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when drawing collage image '{0}':", file);
			}
		}

		/// <summary>
		/// Draws a full screen image to a single monitor.
		/// </summary>
		/// <param name="file">The image to be drawn</param>
		/// <param name="screenRect">The rectangle of the current screen</param>
		/// <param name="spanRect">The rectangle for all screens that this image is to be spanned across</param>
		public void RenderFullScreenImageOnScreen(ImageRec file, Rectangle screenRect, Rectangle spanRect)
		{
			try
			{
				if (file == null || !file.IsPresent) return;

				Image img = file.Image;
				Image imgToDraw = null;
				lock (img)
				{
					RectangleF spanRectF = spanRect;
					RectangleF imgRect = new RectangleF(0, 0, img.Width, img.Height);
					RectangleF srcRect = imgRect;

					_g.SetClip(screenRect);

					imgRect = FitFullScreenImage(imgRect, spanRectF, img.Size, _theme);
					imgToDraw = img;

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
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when drawing full screen image '{0}':", file);
			}
		}

		public void RenderBlankScreen(Rectangle screenRect)
		{
			if (_theme.Mode != ThemeMode.Collage)
			{
				DrawBackgroundColor(screenRect, 255);
			}
		}

		private void DrawBackgroundColor(Rectangle screenRect, int opacity)
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

		public static RectangleF FitFullScreenImage(RectangleF imgRect, RectangleF screenRect, SizeF imgSize, Theme theme)
		{
			SizeF screenSize = screenRect.Size;
			RectangleF origImgRect = imgRect;

			switch (theme.ImageFit)
			{
				case ImageFit.Original:
					imgRect = imgRect.CenterInside(screenRect);
					break;

				case ImageFit.Stretch:
					if (theme.MaxImageScale > 0)
					{
						var maxScale = (float)theme.MaxImageScale / 100.0f;
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
					CheckImageRectSize(theme, ref imgRect, imgSize);
					break;

				case ImageFit.Fill:
					if (imgRect.Width != screenRect.Width) imgRect = imgRect.ScaleRectWidth(screenRect.Width);
					if (imgRect.Height < screenRect.Height) imgRect = imgRect.ScaleRectHeight(screenRect.Height);
					imgRect = imgRect.CenterInside(screenRect);
					CheckImageRectSize(theme, ref imgRect, imgSize);
					break;

				default:
					throw new ArgumentException("Invalid image fit value.");
			}

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
			var srcData = srcBitmap.LockBits(new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			try
			{
				var dstData = dstBitmap.LockBits(new Rectangle(0, 0, dstBitmap.Width, dstBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				try
				{
					var ret = TransferChannel32(srcData.Scan0, (int)srcChannel, srcData.Stride,
						dstData.Scan0, (int)dstChannel, dstData.Stride,
						srcBitmap.Width, srcBitmap.Height);
					if (ret != 0)
					{
						Log.Write(LogLevel.Error, "Error when transferring bitmap channel: {0}", ret);
					}
				}
				finally
				{
					dstBitmap.UnlockBits(dstData);
				}
			}
			finally
			{
				srcBitmap.UnlockBits(srcData);
			}
		}

		private static void CheckImageRectSize(Theme theme, ref RectangleF rect, SizeF imgSize)
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

		private void BlurImage(Bitmap img, int blurDist)
		{
			var imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			try
			{
				BlurImage(imgData.Scan0, img.Width, img.Height, 0, imgData.Stride, blurDist);
			}
			finally
			{
				img.UnlockBits(imgData);
			}
		}

		private RectangleF GetRandomImageRect(Database db, Rectangle screenRect, float imgWidth, float imgHeight)
		{
			var retries = k_randomRectRetries;
			var bestRect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
			var bestOverlap = -1.0f;
			var screenRectF = screenRect;
			var oldRects = _theme.ImageRectHistory.ToArray();

			while (retries-- > 0)
			{
				var rect = new RectangleF(
					(float)(_rand.NextDouble() * (screenRect.Width - imgWidth)) + screenRect.X,
					(float)(_rand.NextDouble() * (screenRect.Height - imgHeight)) + screenRect.Y,
					imgWidth, imgHeight);

				var overlap = 0.0f;

				for (int i = 0; i < oldRects.Length; i++)
				{
					var ratio = (double)(i + 1) / (double)oldRects.Length;
					var importance = Math.Pow(ratio, Math.E);

					overlap += oldRects[i].IntersectArea(rect) * (float)importance;
				}

				if (overlap < bestOverlap || bestOverlap < 0)
				{
					bestRect = rect;
					bestOverlap = overlap;
				}
			}

			_theme.AddImageRectHistory(db, bestRect, _screenRects.Count);
			return bestRect;
		}

		private Image CreateSolidBorderImage(Image img, int edgeWidth, Color borderColor, ref RectangleF srcRect)
		{
			var dstWidth = img.Width + edgeWidth * 2;
			var dstHeight = img.Height + edgeWidth * 2;
			var dstBitmap = new Bitmap(dstWidth, dstHeight);

			using (var g = Graphics.FromImage(dstBitmap))
			{
				using (var brush = new SolidBrush(borderColor))
				{
					g.FillRectangle(brush, new Rectangle(0, 0, dstWidth, dstHeight));
				}
				g.DrawImage(img, new Rectangle(edgeWidth, edgeWidth, img.Width, img.Height));
			}

			srcRect = new RectangleF(0, 0, dstWidth, dstHeight);
			return dstBitmap;
		}
	}
}
