using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// Interface for a custom widget object.
	/// </summary>
	public interface IWidget
	{
		/// <summary>
		/// Called when the widget is being loaded from the settings file.
		/// </summary>
		/// <param name="config">A collection of configuration items loaded from the settings file</param>
		void Load(WidgetConfig config);

		/// <summary>
		/// Called just before the widget is to be saved to the settings file.
		/// </summary>
		/// <param name="config">A collection of configuration items that will be saved</param>
		void Save(WidgetConfig config);

		/// <summary>
		/// Gets the preferred rectangle for this widget.
		/// This will be the initial bounds of the widget when it is first created.
		/// </summary>
		/// <param name="screens">The monitors currently available on this computer</param>
		/// <returns>The preferred bounds of the widget</returns>
		Rectangle GetPreferredBounds(ScreenList screens);

		/// <summary>
		/// Called when the widget is required to render itself onto a new wallpaper.
		/// </summary>
		/// <param name="args">Arguments describing the current wallpaper</param>
		void Draw(WidgetDrawArgs args);

		/// <summary>
		/// Gets a flag indicating if this widget is resizeable.
		/// </summary>
		bool IsFixedSize { get; }

		/// <summary>
		/// Called when the bounds of a widget has changed.
		/// </summary>
		/// <param name="args">Arguments describing the new rectangle.</param>
		/// <remarks>The widget may alter the bounds, if needed. For example, if a widget wishes to maintain a specific aspect ratio, it can adjust the width/height accordingly.</remarks>
		void OnBoundsChanged(WidgetBoundsChangedArgs args);

		/// <summary>
		/// Gets an object which will be passed to the property grid allowing users to modify settings.
		/// </summary>
		object Properties { get; }
	}
}
