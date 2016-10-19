using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using WallSwitch.WidgetInterface;

namespace WallSwitch
{
	internal class WidgetManager
	{
		private static List<WidgetType> _types = new List<WidgetType>();

		public static void SearchForWidgets()
		{
			SearchDirForWidgets(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Res.WidgetDirName));
			SearchDirForWidgets(Path.Combine(Util.AppDataDir, Res.WidgetDirName));
		}

		private static void SearchDirForWidgets(string path)
		{
			if (!Directory.Exists(path)) return;

			foreach (var dllFileName in Directory.GetFiles(path, "*.dll"))
			{
				Log.Write(LogLevel.Info, "Search DLL for widgets: {0}", dllFileName);

				try
				{
					var asm = Assembly.LoadFile(dllFileName);
					SearchAssemblyForWidgets(asm);
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Exception when attempting to load widget DLL.");
				}
			}
		}

		private static void SearchAssemblyForWidgets(Assembly asm)
		{
			foreach (var type in asm.GetExportedTypes())
			{
				if (!typeof(IWidget).IsAssignableFrom(type)) continue;
				if (type.IsAbstract) continue;

				Log.Write(LogLevel.Debug, "Found widget type: {0}", type.FullName);
				_types.Add(new WidgetType(type));
			}
		}

		public static IEnumerable<WidgetType> Types
		{
			get
			{
				return _types;
			}
		}

		public static WidgetType GetTypeFromFullName(string fullName)
		{
			foreach (var wtype in _types)
			{
				if (wtype.FullName == fullName) return wtype;
			}
			return null;
		}
	}
}
