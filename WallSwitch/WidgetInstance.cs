using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using WidgetInterface;

namespace WallSwitch
{
	internal class WidgetInstance
	{
		private IWidget _widget;
		private WidgetType _wtype;
		private Rectangle _bounds;
		private WidgetConfig _config;
		private Rectangle _designBounds;

		public WidgetInstance(WidgetType wtype)
		{
			_wtype = wtype;
			_widget = (IWidget)Activator.CreateInstance(wtype.Type);
			_config = new WidgetConfig();
		}

		public WidgetInstance(WidgetType wtype, Rectangle bounds, WidgetConfig config)
		{
			_wtype = wtype;
			_bounds = bounds;
			_config = config;

			_widget = (IWidget)Activator.CreateInstance(wtype.Type);
			_widget.Load(config);
		}

		public static string XmlElementName
		{
			get { return "Widget"; }
		}

		public void Save(XmlWriter xml)
		{
			xml.WriteStartElement(XmlElementName);
			xml.WriteAttributeString("Name", _wtype.FullName);

			xml.WriteStartElement("Bounds");
			xml.WriteAttributeString("Left", _bounds.Left.ToString());
			xml.WriteAttributeString("Top", _bounds.Top.ToString());
			xml.WriteAttributeString("Width", _bounds.Width.ToString());
			xml.WriteAttributeString("Height", _bounds.Height.ToString());
			xml.WriteEndElement();	// Bounds

			var settings = new WidgetConfig();
			_widget.Save(settings);
			foreach (var setting in settings)
			{
				xml.WriteStartElement("Config");
				xml.WriteAttributeString("Name", setting.Name);
				if (setting.Value != null) xml.WriteAttributeString("Value", setting.Value);
				xml.WriteEndElement();	// Setting
			}

			xml.WriteEndElement();	// Widget
		}

		public static WidgetInstance Load(XmlElement element)
		{
			if (element.Name != XmlElementName) throw new WidgetLoadException(string.Format("Element name is not '{0}'.", XmlElementName));

			var fullName = element.GetAttribute("Name");
			if (fullName == null) throw new WidgetLoadException("'Name' attribute does not exist.");

			var type = WidgetManager.GetTypeFromFullName(fullName);
			if (type == null) throw new WidgetLoadException(string.Format("A widget with the name '{0}' cannot be found.", fullName));

			var boundsElement = element.SelectSingleNode("Bounds") as XmlElement;
			if (boundsElement == null) throw new WidgetLoadException("'Bounds' element does not exist.");

			var bounds = new Rectangle(int.Parse(boundsElement.GetAttribute("Left")), int.Parse(boundsElement.GetAttribute("Top")),
				int.Parse(boundsElement.GetAttribute("Width")), int.Parse(boundsElement.GetAttribute("Height")));

			var settings = new WidgetConfig();
			foreach (var xmlSetting in element.SelectNodes("Config").Cast<XmlElement>())
			{
				var name = xmlSetting.GetAttribute("Name");
				if (string.IsNullOrEmpty(name)) throw new WidgetLoadException("Widget config has no 'Name' attribute.");
				settings.Add(new WidgetConfigItem(name, xmlSetting.GetAttribute("Value")));
			}

			return new WidgetInstance(type, bounds, settings);
		}

		public Rectangle GetPreferredBounds()
		{
			return _widget.GetPreferredBounds(new ScreenList());
		}

		public Rectangle Bounds
		{
			get { return _bounds; }
			set { _bounds = value; }
		}

		public void Draw(WidgetDrawArgs args)
		{
			_widget.Draw(args);
		}

		public bool OffsetBoundsSafe(Point offset)
		{
			var newBounds = _bounds;
			newBounds.Offset(offset);

			return ChangeBoundsSafe(newBounds);
		}

		public bool ChangeBoundsSafe(Rectangle newBounds)
		{
			var screens = new ScreenList();

			if (newBounds.Width <= 0 || newBounds.Height <= 0) return false;

			if (!screens.Contains(newBounds.TopLeft()) || !screens.Contains(newBounds.TopRight()) ||
				!screens.Contains(newBounds.BottomLeft()) || !screens.Contains(newBounds.BottomRight()))
			{
				return false;
			}

			var args = new WidgetSizeChangedArgs(_config, newBounds, _bounds);
			_widget.OnSizeChanged(args);

			_bounds = args.Bounds;
			return true;
		}

		public WidgetConfig Config
		{
			get { return _config; }
		}

		public Rectangle DesignBounds
		{
			get { return _designBounds; }
			set { _designBounds = value; }
		}

		public bool IsFixedSize
		{
			get { return _widget.IsFixedSize; }
		}
	}
}
