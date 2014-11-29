using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using WallSwitch.WidgetInterface;

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

		public bool OffsetBoundsSafe(Point offset, bool dragFinished)
		{
			var newBounds = _bounds;
			newBounds.Offset(offset);

			return ChangeBoundsSafe(newBounds, dragFinished);
		}

		public bool ChangeBoundsSafe(Rectangle newBounds, bool dragFinished)
		{
			var screens = new ScreenList();

			if (newBounds.Width <= 0 || newBounds.Height <= 0)
			{
				return false;
			}

			var envelope = screens.Select(s => s.Bounds).ToArray().GetEnvelope();
			if (!envelope.Contains(newBounds.TopLeft()) && !envelope.Contains(newBounds.TopRight()) &&
				!envelope.Contains(newBounds.BottomLeft()) && !envelope.Contains(newBounds.BottomRight()))
			{
				return false;
			}

			var args = new WidgetBoundsChangedArgs(_config, newBounds, _bounds, new ScreenList(), dragFinished);
			_widget.OnBoundsChanged(args);

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

		public object Properties
		{
			get { return _widget.Properties; }
		}

		public string DisplayName
		{
			get { return _wtype.Name; }
		}
	}
}
