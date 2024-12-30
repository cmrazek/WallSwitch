using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using WallSwitch.Rendering;
using WallSwitch.SettingsStore;
using WI = WallSwitch.WidgetInterface;

namespace WallSwitch
{
    class WallpaperRenderer : IDisposable
    {
        private Size _fullSize = new Size(0, 0);
        private List<Rectangle> _screenRects = new List<Rectangle>();
        private Theme _theme = null;
        private Random _rand = new Random();
        private IImageRenderer _renderer;
        private static Type _imageRendererType = null;

        private const int k_randomRectRetries = 10;

        public WallpaperRenderer()
        { }

        public void Dispose()
        {
            if (_renderer != null)
            {
                lock (_renderer)
                {
                    _renderer?.Dispose();
                    _renderer = null;
                }
            }
        }

        private static Type GetImageRendererType()
        {
            if (Settings.DisableHardwareAcceleration ||
                Program.OsVersion < OsVersion.Windows10)
            {
                return typeof(SoftwareRenderer);
            }

            if (_imageRendererType == null)
            {
                try
                {
                    Log.Info("Attempting to create hardware renderer.");
                    var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "WallSwitch.DX11.dll";
                    var asm = Assembly.LoadFrom(fileName);
                    _imageRendererType = asm.GetTypes().FirstOrDefault(x => typeof(IImageRenderer).IsAssignableFrom(x) && x.IsPublic);
                    if (_imageRendererType == null) throw new InvalidOperationException("Hardware accelerated renderer type not found in WallSwitch.DX11.dll");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to load hardware accelerated renderer assembly.");
                    _imageRendererType = typeof(SoftwareRenderer);
                }
            }

            return _imageRendererType;
        }

        public static void ResetHardwareAccelerationSettings()
        {
            _imageRendererType = null;
        }

        private static void DisableHardwareAcceleration()
        {
            Log.Info("Disabling hardware acceleration for now.");
            _imageRendererType = typeof(SoftwareRenderer);
        }

        private IImageRenderer GetImageRenderer()
        {
            try
            {
                return (IImageRenderer)Activator.CreateInstance(GetImageRendererType());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load hardware accelerated renderer.");
                DisableHardwareAcceleration();
                return new SoftwareRenderer();
            }
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
                Log.Debug("Not initializing frame because the dimensions are zero.");
                return false;
            }

            if (_renderer == null)
            {
                _renderer = GetImageRenderer();
            }

            var timer = new Stopwatch();
            timer.Start();

            try
            {
                if (lastImage != null && lastImage.Width == _fullSize.Width && lastImage.Height == _fullSize.Height)
                {
                    _renderer.InitializeFrame(_fullSize.Width, _fullSize.Height);
                }
                else
                {
                    _renderer.InitializeFrame(_fullSize.Width, _fullSize.Height);
                    _theme.ClearImageRectHistory();
                }
            }
            catch (HardwareAccelerationException ex)
            {
                Log.Error(ex);
                DisableHardwareAcceleration();
                _renderer = new SoftwareRenderer();
            }

            if (_theme.Mode == ThemeMode.Collage)
            {
                if (lastImage != null && lastImage.Size == _fullSize)
                {
                    _renderer.DrawImage(lastImage, new Rectangle(0, 0, lastImage.Width, lastImage.Height),
                        colorEffect: _theme.ColorEffectBack,
                        colorEffectRatio: _theme.ColorEffectBackRatio / 100.0f,
                        blurDistance: _theme.BackgroundBlur ? _theme.BackgroundBlurDist : 0);

                    // Tint the background
                    _renderer.DrawBackgroundTint(_theme.BackColorTopWithOpacity, _theme.BackColorBottomWithOpacity);
                }
                else if (_theme.BackColorTop == _theme.BackColorBottom)
                {
                    // Draw solid background
                    _renderer.Clear(_theme.BackColorTop);
                }
                else
                {
                    // Draw gradient background with solid opacity
                    _renderer.DrawBackgroundTint(Color.FromArgb(255, _theme.BackColorTop), Color.FromArgb(255, _theme.BackColorBottom));
                }
            }
            else // Fullscreen mode
            {
                if (_theme.BackColorTop == _theme.BackColorBottom)
                {
                    // Draw solid background
                    _renderer.Clear(_theme.BackColorTop);
                }
                else
                {
                    // Draw gradient background with solid opacity
                    _renderer.DrawBackgroundTint(Color.FromArgb(255, _theme.BackColorTop), Color.FromArgb(255, _theme.BackColorBottom));
                }
            }

            timer.Stop();
            Log.Debug("Background rendering elapsed time: {0}", timer.Elapsed);

            return true;
        }

        public Bitmap EndFrame()
        {
            return _renderer.EndFrame();
        }

        public void RenderCollageImageOnScreen(Database db, ImageRec file, WI.Screen screen)
        {
            try
            {
                if (file == null || !file.IsPresent) return;

                var img = file.Image;
                lock (img)
                {
                    // The maximum image size will be determined by the number of pixels (area), not by the max width/height.
                    // This will provide a more consistent image size when there are pictures with different aspect ratios.
                    float imgWidth = file.Image.Width;
                    float imgHeight = file.Image.Height;
                    float imgArea = imgWidth * imgHeight;
                    float imgSize = (float)_theme.ImageSize / 100.0f;
                    var screenRect = _theme.GetDrawableBounds(screen);
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

                    if (imgWidth <= 0 || imgHeight <= 0)
                    {
                        Log.Debug("Not rendering image because the scaled dimensions are zero.");
                        return;
                    }

                    var imgRect = GetRandomImageRect(db, screenRect, imgWidth, imgHeight).ToRectangle();

                    Log.Debug("Drawing image '{0}' to rect '{1}'.", file.Location, imgRect);

                    if (_theme.DropShadow && _theme.DropShadowDist > 0)
                    {
                        var dropShadowRect = imgRect;
                        dropShadowRect.Offset(_theme.DropShadowDist, _theme.DropShadowDist);
                        _renderer.DrawSolidRect(dropShadowRect, Color.FromArgb(_theme.DropShadowOpacity255, Color.Black),
                            featherDistance: _theme.DropShadowFeather ? _theme.DropShadowFeatherDist : 0);
                    }

                    if (_theme.EdgeMode == EdgeMode.SolidBorder)
                    {
                        _renderer.DrawSolidRect(imgRect, _theme.BorderColor);
                        imgRect.Inflate(-_theme.EdgeDist, -_theme.EdgeDist);
                    }

                    _renderer.DrawImage(img, imgRect,
                        colorEffect: _theme.ColorEffectFore,
                        featherDistance: _theme.EdgeMode == EdgeMode.Feather ? _theme.EdgeDist : 0);
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
        public void RenderFullScreenImageOnScreen(ImageRec file, Rectangle spanRect)
        {
            try
            {
                if (file == null || !file.IsPresent) return;

                Image img = file.Image;
                lock (img)
                {
                    RectangleF spanRectF = spanRect;
                    RectangleF imgRect = new RectangleF(0, 0, img.Width, img.Height);

                    imgRect = FitFullScreenImage(imgRect, spanRectF, img.Size, _theme);

                    _renderer.DrawImage(img, imgRect.ToRectangle(), colorEffect: _theme.ColorEffectFore);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, "Exception when drawing full screen image '{0}':", file);
            }
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
    }

    public class HardwareAccelerationException : Exception
    {
        public HardwareAccelerationException(string message) : base(message) { }
    }
}
