using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

using WallSwitch.WidgetInterface;

namespace WallSwitch.Widgets
{
    [DisplayName("Screen Replicate")]
    public class ScreenReplicate : IWidget
    {
        private ScreenReplicateProperties _props = new ScreenReplicateProperties();

        public void Load(WidgetConfig config)
        {
            WidgetConfigItem item;

            if ((item = config.TryGetItem("SourceMonitor")) != null)
            {
                if (int.TryParse(item.Value, out var monitor) && monitor > 0) _props.SourceMonitor = monitor;
            }

            if ((item = config.TryGetItem("DestinationMonitor")) != null)
            {
                if (int.TryParse(item.Value, out var monitor) && monitor > 0) _props.DestinationMonitor = monitor;
            }

            if ((item = config.TryGetItem("SizingMode")) != null)
            {
                if (Enum.TryParse<SizingMode>(item.Value, out var sizingMode)) _props.SizingMode = sizingMode;
            }
        }

        public void Save(WidgetConfig config)
        {
            config["SourceMonitor"] = _props.SourceMonitor.ToString();
            config["DestinationMonitor"] = _props.DestinationMonitor.ToString();
            config["SizingMode"] = _props.SizingMode.ToString();
        }

        public Rectangle GetPreferredBounds(ScreenList screens)
        {
            var mon = _props.DestinationMonitor;
            if (mon > 0 && (mon - 1) < screens.Count)
            {
                return screens[mon - 1].Bounds;
            }

            return screens.Last().Bounds;
        }

        public void Draw(WidgetDrawArgs args)
        {
            if (args.Sample) return;
            if (_props.SourceMonitor <= 0) return;
            if (_props.DestinationMonitor <= 0) return;

            var screens = new ScreenList();
            if (_props.SourceMonitor > screens.Count) return;
            if (_props.DestinationMonitor > screens.Count) return;

            // Capture the source bitmap
            var srcScreen = screens[_props.SourceMonitor - 1];
            var dstScreen = screens[_props.DestinationMonitor - 1];
            var srcRect = srcScreen.Bounds;
            var dstRect = dstScreen.Bounds;
            if (!CalcRectSizes(_props.SizingMode, ref srcRect, ref dstRect)) return;

            using (var srcBmp = new Bitmap(srcRect.Width, srcRect.Height))
            using (var g = Graphics.FromImage(srcBmp))
            {
                // Graphics.CopyFromScreen requires a point relative to the primary monitor.
                var srcPos = new Point(srcRect.Left - screens.Primary.Bounds.Left, srcRect.Top - screens.Primary.Bounds.Top);
                g.CopyFromScreen(srcPos.X, srcPos.Y, 0, 0, srcRect.Size);
                args.Graphics.DrawImage(srcBmp, dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height);
            }
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        public void OnBoundsChanged(WidgetBoundsChangedArgs args)
        {
            if (!args.Final) return;

            var screens = new ScreenList();
            if (_props.DestinationMonitor > screens.Count) return;
            args.Bounds = screens[_props.DestinationMonitor - 1].Bounds;
        }

        public object Properties
        {
            get { return _props; }
        }

        public enum SizingMode
        {
            Fit,
            Fill,
            Stretch
        }

        private static bool CalcRectSizes(SizingMode sizingMode, ref Rectangle srcRect, ref Rectangle dstRect)
        {
            if (srcRect.Width <= 0 || srcRect.Height <= 0 || dstRect.Width <= 0 || dstRect.Height <= 0) return false;

            switch (sizingMode)
            {
                case SizingMode.Stretch:
                    // No changes to either rect required.
                    return true;

                case SizingMode.Fit:
                    {
                        float srcAspect = (float)srcRect.Width / (float)srcRect.Height;
                        float dstAspect = (float)dstRect.Width / (float)dstRect.Height;
                        if (Math.Abs(srcAspect - dstAspect) <= 0.01f) return true;  // Close enough to not be a noticable stretch

                        if (srcAspect > dstAspect)
                        {
                            // Source image is wider than the destination; scale the height down.
                            var scaledHeight = (int)Math.Round((float)dstRect.Width / srcAspect);
                            dstRect.Y += (dstRect.Height - scaledHeight) / 2;
                            dstRect.Height = scaledHeight;
                        }
                        else
                        {
                            // Source image is taller than the destination; scale the width down.
                            var scaledWidth = (int)Math.Round((float)dstRect.Height * srcAspect);
                            dstRect.X += (dstRect.Width - scaledWidth) / 2;
                            dstRect.Width = scaledWidth;
                        }
                    }
                    return true;

                case SizingMode.Fill:
                    {
                        float srcAspect = (float)srcRect.Width / (float)srcRect.Height;
                        float dstAspect = (float)dstRect.Width / (float)dstRect.Height;
                        if (Math.Abs(srcAspect - dstAspect) <= 0.01f) return true;  // Close enough to not be a noticable stretch

                        if (srcAspect > dstAspect)
                        {
                            // Source image is wider than the destination; cut off the left and right edges.
                            var scaledWidth = (int)Math.Round((float)srcRect.Height * dstAspect);
                            srcRect.X += (srcRect.Width - scaledWidth) / 2;
                            srcRect.Width = scaledWidth;
                        }
                        else
                        {
                            // Source image is taller than the destination; cut off the top and bottom edges.
                            var scaledHeight = (int)Math.Round((float)srcRect.Width / dstAspect);
                            srcRect.Y += (srcRect.Height - scaledHeight) / 2;
                            srcRect.Height = scaledHeight;
                        }
                    }
                    return true;

                default:
                    return false;
            }
        }

        public class ScreenReplicateProperties
        {
            public int SourceMonitor { get; set; } = 1;

            public int DestinationMonitor { get; set; } = 2;

            public SizingMode SizingMode { get; set; } = SizingMode.Fit;
        }
    }
}
