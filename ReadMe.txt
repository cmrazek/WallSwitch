WallSwitch
Switches desktop wallpapers from a variety of sources.
http://wallswitch.codeplex.com/
Current Version: 1.2.1

Requirements:
	.NET Framework 4:  http://www.microsoft.com/download/en/details.aspx?id=17718

Contact:
	Chris Mrazek (cmrazek)
	Email: chrismrazek@gmail.com

ChangeLog:

Version 1.2.2 - (pending):
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
