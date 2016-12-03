WallSwitch
Switches desktop wallpapers from a variety of sources.
http://wallswitch.codeplex.com/
Current Version: 1.5.1

Requirements:
	.NET Framework 4.5.2:  https://www.microsoft.com/en-ca/download/details.aspx?id=42643

Contact:
	Chris Mrazek (cmrazek)
	Email: chrismrazek@gmail.com

ChangeLog:

Version 1.5.1 - 2016-12-03:
- Fix issue with wallpaper 'fill' not scaling properly, when the image is larger than the screen.

Version 1.5 - 2016-10-30:
- Ratings for images can now be stored in the database.
- Ratings can be set through the history tab.
- Added a Browse window to theme locations to view all images and ratings.
- Added a Filter tab to allow filtering images by rating or file name.
- Added ratings import/export buttons to the Tools -> Settings dialog.
- Improved icons in location list, which looked bad in recent versions of Windows.

Version 1.4.2 - 2016-10-23:
- Improved database performance.
- Showing a file in Explorer is now more reliable.

Version 1.4.1 - 2016-09-11:
- Fixed bug where unchecked locations were still being displayed.

Version 1.4 - 2016-09-04:
- Image lists are now saved to a database, rather than held in memory.
- Refresh interval is now visible on the images list box.
- You can now add a new image folder by pasting in the path, rather than forcing the user of the
  browse folder dialog.
- Images in the history view are now persisted when the app restarts.

Version 1.3.11 - 2016-01-16:
- Wallpaper images are now saved using unique filenames to try to avoid file locking from other processes.
- Fix problem with the settings view not scaling with the window.

Version 1.3.10 - 2015-09-27:
- Add extra error handling and retries to the image saving process.

Version 1.3.9 - 2015-09-19:
- Added option to clear between random groups when in collage mode.

Version 1.3.8 - 2015-08-23:
- Added support for groups of sequential images between random ones.

Version 1.3.7 - 2015-01-25:
- Added support for high DPI under Windows 8.1.
- Fixed transparency slider not scaling with window properly.
- Removed invalid widget 'IWidget' from the list of available widgets.
- Added keyboard shortcut for delete key in history list.

Version 1.3.6 - 2014-12-24:
- Added a slider to control window transparency.
- Added a theme setting to switch to that theme when exiting.
- Force .NET garbage collection after every wallpaper switch to free memory used during rendering.

Version 1.3.5 - 2014-12-14:
- Fixed issues with images not being displayed in the correct positions for multiple monitors under
  Windows 8 in some cases.

Version 1.3.4 - 2014-12-06:
- Fixed issues when used with multiple monitors of different sizes.

Version 1.3.3 - 2014-11-30:
- Now ignores hidden files and folders (configurable through Tools -> Settings)

Version 1.3.2 - 2014-11-29:
- Fixed collage/sequential images not being displayed.
- Re-enabled the 'Separate image for each monitor' checkbox in collage mode.

Version 1.3.1 - 2014-11-29:
- Fixed error with version 1.3 installer.
- Fixed problem with application not shutting down properly when killed by the installer.
  Note: Previous versions of WallSwitch may not shutdown correctly during installation.
  When upgrading to 1.3.1, I recommend exiting WallSwitch before installing.

Version 1.3 - 2014-11-29:
- Added 'widgets' which allow users to create their own custom objects that can be rendered over the
  wallpaper.
- Added 3 initial widgets:
    - Calendar - Displays the current month's calendar
	- Sys Info - Displays information about your computer
	- Flip Image - Inverts the image of a single monitor, horizontally or vertically.
- Added ability to span images across multiple monitors, if they have a suitable aspect ratio.
- Change installer from NSIS to WiX due to blue screens caused by NSIS.

Version 1.2.5 - 2014-08-16:
- Added support for sequential order in collage mode.
- Added option to display multiple images per switch in collage mode.
- Fixed bug where border width wasn't being loaded properly, and was reverting to default values.
- Fixed bug where sequential order was repeating images on multiple monitors.
- Decreased likelihood of random images being repeated.

Version 1.2.4 - 2014-04-26:
- Added an option to put a solid border around the image in collage mode.

Version 1.2.3 - 2013-07-06:
- Fixed issue with auto-update version number detection.

Version 1.2.2 - 2013-07-06:
- Added settings dialog.
- Added option to delay wallpaper switching after start up or resume from sleep mode.
- Added checkboxes to location list to allow disabling of locations without deleting them.
- Added option to delete an image directly from the history list.
- Fixed location list columns not resizing correctly.
- Fixed missing 'Add Feed' option.

Version 1.2.1 - 2012-11-12:
- Improved collage image distribution to overlap older images first.
- Set default collage background blur distance to 4 (provides a more gradual effect).
- Fixed issue where wallpaper not displayed on Windows Vista when Cross-Fade transitions enabled.
- Fixed issue with duplicated themes not updating history view correctly.

Version 1.2 - 2012-11-04:
- Added ability to duplicate a theme.
- Added drop shadows to collage mode.
- In collage mode, can use separate color effects for foreground and background.
- Added a blur option to collage background.
- Image positions in collage mode are now more evenly distributed.

Version 1.1 - 2012-10-27:
- Now automatically checks for updates (can be disabled if desired)
- Misc bug fixes.

Version 1.0.6 - 2012-09-30:
- Added hotkeys to perform a variety of operations (next/previous image, pause, clear history, etc.)
- Added color effects for grayscale, sepia and intense color.
- Various fixes.

Version 1.0.5 - 2012-09-01:
- Added cross-fade transitions (thanks to Freshie for assistance).
- Added tooltip text to controls.
- Added option to prevent small images from being scaled too large.
- Fixed incorrectly scaled images in collage mode.

Version 1.0.4 - 2012-06-30:
- Changes in previous version broke hotkey support; fixed this.
- When minimizing the window to tray, if there are unsaved changes, then prompt to save first.
- When changing the path on a folder location, reset the file list.
- Fixed app not shutting down properly when closed externally.
- Improved installer to shut down app instead of requiring reboot.
- Clear History action now clears *all* history, rather than just the current theme.

Version 1.0.3 - 2012-06-28:
- Added support for RSS feeds.
- Added a History tab to show recently displayed images.
- Directories are now only scanned periodically and updated at a configurable interval.
- Will no longer change wallpaper while the screensaver is active or the workstation is locked.
- Change settings to use Local AppData instead of Roaming on Vista/7.
- When starting for the first time, it will no longer complain about a missing settings file.

Version 1.0.2 - 2011-10-01:
- Bug fixes.

Version 1.0.1 - 2011-08-14:
- No longer keeps a lock on the wallpaper image file after it's no longer needed.
- No longer scans entire directory for each wallpaper switch event (happens only the first time).
- When deleting a location, select the next location in the list.
- Debug level logging has been eliminated.
