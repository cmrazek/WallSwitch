using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WallSwitch.Rendering
{
    internal class SoftwareRenderer : IImageRenderer
    {
        private Graphics _g;
        private Bitmap _bitmap = null;

        public void Dispose()
        {
            _g?.Dispose();
            _g = null;

            _bitmap?.Dispose();
            _bitmap = null;
        }

        public void InitializeFrame(Bitmap lastImage)
        {
            _bitmap = lastImage ?? throw new ArgumentNullException(nameof(lastImage));
            _g = Graphics.FromImage(_bitmap);
        }

        public void InitializeFrame(int width, int height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

            _bitmap = new Bitmap(width, height);
            _g = Graphics.FromImage(_bitmap);
            _g.SetClip(new Rectangle(0, 0, width, height));
        }

        public Bitmap Image => _bitmap;

        public void Clear(Color color)
        {
            if (_g == null) throw new NoGraphicsContextException();

            _g.Clear(color);
        }

        public void DrawImage(Image image, Rectangle rect, ColorEffect colorEffect = ColorEffect.None, float colorEffectRatio = 1.0f, int blurDistance = 0, int featherDistance = 0)
        {
            if (_g == null) throw new NoGraphicsContextException();

            if (image == null) throw new ArgumentNullException(nameof(image));

            if (featherDistance > 0)
            {
                image = FeatherImage(image, featherDistance);
            }

            if (colorEffect == ColorEffect.None || colorEffectRatio <= 0.0f)
            {
                _g.DrawImage(image, rect);
            }
            else
            {
                if (colorEffectRatio < 0.0f) colorEffectRatio = 0.0f;
                else if (colorEffectRatio > 1.0f) colorEffectRatio = 1.0f;

                var imgAttrib = new ImageAttributes();
                imgAttrib.SetColorMatrix(ColorEffect.None.GetColorMatrixBlend(colorEffect, colorEffectRatio));

                _g.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAttrib);
            }

            if (blurDistance > 0)
            {
                BlurImage(_bitmap, blurDistance);
            }
        }

        private void BlurImage(Bitmap img, int blurDist)
        {
            var imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            try
            {
                WallSwitchImgProc.BlurImage(imgData.Scan0, img.Width, img.Height, 0, imgData.Stride, blurDist);
            }
            finally
            {
                img.UnlockBits(imgData);
            }
        }

        public void DrawBackgroundTint(Color topColor, Color bottomColor)
        {
            if (_g == null) throw new NoGraphicsContextException();

            Brush brush = null;
            try
            {
                var screenRect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);

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
            }
            finally
            {
                brush?.Dispose();
            }
        }

        public void DrawFeatheredImage(Image image, Rectangle rect, int featherDistance)
        {
            var featheredImage = FeatherImage(image, featherDistance);
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
            DrawFeatherArc(g, new Rectangle(0, 0, feather2, feather2), new Point(feather, feather), 180.0f, 90.0f, Color.White, Color.Black);

            // Top-right corner
            DrawFeatherArc(g, new Rectangle(width - feather2, 0, feather2, feather2), new Point(width - feather, feather), 270.0f, 90.0f, Color.White, Color.Black);

            // Bottom-right corner
            DrawFeatherArc(g, new Rectangle(width - feather2, height - feather2, feather2, feather2), new Point(width - feather, height - feather), 0.0f, 90.0f, Color.White, Color.Black);

            // Bottom-left corner
            DrawFeatherArc(g, new Rectangle(0, height - feather2, feather2, feather2), new Point(feather, height - feather), 90.0f, 90.0f, Color.White, Color.Black);

            return bmp;
        }

        private void DrawFeatherEdge(Graphics g, Rectangle rect, LinearGradientMode gradMode, Color color1, Color color2)
        {
            var brush = new LinearGradientBrush(rect, color1, color2, gradMode);
            brush.WrapMode = WrapMode.TileFlipXY;
            g.FillRectangle(brush, rect);
        }

        private void DrawFeatherArc(Graphics g, Rectangle rect, Point center, float startAngle, float arcAngle, Color centerColor, Color outsideColor)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPie(rect, startAngle, arcAngle);

            PathGradientBrush brush = new PathGradientBrush(path);
            brush.CenterColor = centerColor;
            brush.SurroundColors = new Color[] { outsideColor };
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
                    var ret = WallSwitchImgProc.TransferChannel32(srcData.Scan0, (int)srcChannel, srcData.Stride,
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

        public void DrawSolidRect(Rectangle rect, Color color, int feather = 0)
        {
            var brush = new SolidBrush(color);

            if (feather == 0)
            {
                _g.FillRectangle(brush, rect);
            }
            else
            {
                if (feather > _bitmap.Width / 2) feather = _bitmap.Width / 2;
                if (feather > _bitmap.Height / 2) feather = _bitmap.Height / 2;
                if (feather <= 0)
                {
                    _g.FillRectangle(brush, rect);
                }
                var feather2 = feather * 2;
                var width = rect.Width;
                var height = rect.Height;

                // Center
                _g.FillRectangle(brush, new Rectangle(rect.Left + feather, rect.Top + feather, width - feather2, height - feather2));

                // Left edge
                DrawFeatherEdge(_g, new Rectangle(rect.Left, rect.Top + feather, feather, height - feather * 2), LinearGradientMode.Horizontal, Color.Transparent, color);

                // Top edge
                DrawFeatherEdge(_g, new Rectangle(rect.Left + feather, rect.Top, width - feather * 2, feather), LinearGradientMode.Vertical, Color.Transparent, color);

                // Right edge
                DrawFeatherEdge(_g, new Rectangle(rect.Left + width - feather, rect.Top + feather, feather, height - feather * 2), LinearGradientMode.Horizontal, color, Color.Transparent);

                // Bottom edge
                DrawFeatherEdge(_g, new Rectangle(rect.Left + feather, rect.Top + height - feather, width - feather * 2, feather), LinearGradientMode.Vertical, color, Color.Transparent);

                // Top-left corner
                DrawFeatherArc(_g, new Rectangle(rect.Left, rect.Top, feather2, feather2),
                    new Point(rect.Left + feather, rect.Top + feather), 180.0f, 90.0f, color, Color.Transparent);

                // Top-right corner
                DrawFeatherArc(_g, new Rectangle(rect.Left + width - feather2, rect.Top, feather2, feather2),
                    new Point(rect.Left + width - feather, rect.Top + feather), 270.0f, 90.0f, color, Color.Transparent);

                // Bottom-right corner
                DrawFeatherArc(_g, new Rectangle(rect.Left + width - feather2, rect.Top + height - feather2, feather2, feather2),
                    new Point(rect.Left + width - feather, rect.Top + height - feather), 0.0f, 90.0f, color, Color.Transparent);

                // Bottom-left corner
                DrawFeatherArc(_g, new Rectangle(rect.Left, rect.Top + height - feather2, feather2, feather2),
                    new Point(rect.Left + feather, rect.Top + height - feather), 90.0f, 90.0f, color, Color.Transparent);
            }
        }
    }

    class NoGraphicsContextException : InvalidOperationException
    {
        public NoGraphicsContextException() : base("Cannot draw when there is no current graphics context.") { }
    }
}
