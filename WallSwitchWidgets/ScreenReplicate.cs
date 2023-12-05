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
            int monitor;

            if ((item = config.TryGetItem("SourceMonitor")) != null)
            {
                if (int.TryParse(item.Value, out monitor) && monitor > 0) _props.SourceMonitor = monitor;
            }

            if ((item = config.TryGetItem("DestinationMonitor")) != null)
            {
                if (int.TryParse(item.Value, out monitor) && monitor > 0) _props.DestinationMonitor = monitor;
            }
        }

        public void Save(WidgetConfig config)
        {
            config["SourceMonitor"] = _props.SourceMonitor.ToString();
            config["DestinationMonitor"] = _props.DestinationMonitor.ToString();
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
            using (var srcBmp = new Bitmap(srcScreen.Bounds.Width, srcScreen.Bounds.Height))
            using (var g = Graphics.FromImage(srcBmp))
            {
                // Graphics.CopyFromScreen requires a point relative to the primary monitor.
                var srcPos = new Point(srcScreen.Bounds.Left - screens.Primary.Bounds.Left, srcScreen.Bounds.Top - screens.Primary.Bounds.Top);
                g.CopyFromScreen(srcPos.X, srcPos.Y, 0, 0, srcScreen.Bounds.Size);
                args.Graphics.DrawImage(srcBmp, dstScreen.Bounds.Left, dstScreen.Bounds.Top, dstScreen.Bounds.Width, dstScreen.Bounds.Height);
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

        public class ScreenReplicateProperties
        {
            public int SourceMonitor { get; set; } = 1;

            public int DestinationMonitor { get; set; } = 2;
        }
    }
}
