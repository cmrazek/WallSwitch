using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WidgetInterface
{
	public interface IWidget
	{
		void Load(WidgetConfig config);

		void Save(WidgetConfig config);

		Rectangle GetPreferredBounds(ScreenList screens);

		void Draw(WidgetDrawArgs args);

		bool IsFixedSize { get; }

		void OnSizeChanged(WidgetSizeChangedArgs args);
	}
}
