using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WallSwitch
{
	class Location
	{
		private string _path;
		private bool _isFile;
		//private FileSystemWatcher _watcher = null;
		private List<string> _files = new List<string>();
		private bool _fullScanRequired = true;

		/// <summary>
		/// Constructs the Location object.
		/// </summary>
		/// <param name="path">The location path</param>
		public Location(string path)
		{
			_path = path;
			_isFile = File.Exists(path);
		}

		/// <summary>
		/// Gets the path to this location.
		/// </summary>
		public string Path
		{
			get { return _path; }
		}

		/// <summary>
		/// Returns true if the path matches the path in this object.
		/// </summary>
		/// <param name="path">The path to compare.</param>
		/// <returns>True if the paths match; false otherwise.</returns>
		public bool SamePath(string path)
		{
			return _path.Equals(path, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Destroys the object. Stops watching the directory, if applicable.
		/// </summary>
		public void Destroy()
		{
			//StopWatching();
		}

		/// <summary>
		/// Gets a flag indicating if this location points to a file.
		/// </summary>
		public bool IsFile
		{
			get { return _isFile; }
		}

		/// <summary>
		/// Gets a flag indicating if this location points to a directory.
		/// </summary>
		public bool IsDirectory
		{
			get { return !_isFile; }
		}

		/// <summary>
		/// Gets a flag indicating if the file or directory exists.
		/// </summary>
		public bool Exists
		{
			get
			{
				if (_isFile) return File.Exists(_path);
				else return Directory.Exists(_path);
			}
		}

		///// <summary>
		///// Starts watching for changes to this location.
		///// </summary>
		//public void StartWatching()
		//{
		//    try
		//    {
		//        if (!_isFile && _watcher == null)
		//        {
		//            Log.Write(LogLevel.Debug, "Starting to watch directory '{0}'.", _path);
		//            _watcher = new FileSystemWatcher(_path);
		//            _watcher.IncludeSubdirectories = true;
		//            _watcher.Created += new FileSystemEventHandler(_watcher_Created);
		//            _watcher.Deleted += new FileSystemEventHandler(_watcher_Deleted);
		//            _watcher.Error += new ErrorEventHandler(_watcher_Error);
		//            _watcher.Renamed += new RenamedEventHandler(_watcher_Renamed);
		//            _watcher.EnableRaisingEvents = true;
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Write(ex, "Exception when starting to watch directory '{0}'.", _path);
		//        _fullScanRequired = true;
		//    }
		//}

		///// <summary>
		///// Stops watching for changes to this location.
		///// </summary>
		//public void StopWatching()
		//{
		//    try
		//    {
		//        if (_watcher != null)
		//        {
		//            Log.Write(LogLevel.Debug, "No longer watching directory '{0}'.", _path);
		//            _watcher.Dispose();
		//            _watcher = null;
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Write(ex, "Exception when stopping to watch directory '{0}'.", _path);
		//    }
		//}

		//void _watcher_Created(object sender, FileSystemEventArgs e)
		//{
		//    try
		//    {
		//        Log.Write(LogLevel.Debug, "Received file created event: {0}", e.FullPath);
		//        if (File.Exists(e.FullPath) && Theme.IsImageFile(e.FullPath)) _files.Add(e.FullPath);
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Write(ex, "Exception when responding to file created event.");
		//        _fullScanRequired = true;
		//    }
		//}

		//void _watcher_Deleted(object sender, FileSystemEventArgs e)
		//{
		//    try
		//    {
		//        Log.Write(LogLevel.Debug, "Received file deleted event: {0}", e.FullPath);

		//        // Try to find and remove the path, case insensitive.
		//        string delFile = e.FullPath;
		//        foreach (string file in _files)
		//        {
		//            if (file.Equals(delFile, StringComparison.OrdinalIgnoreCase))
		//            {
		//                _files.Remove(file);
		//                break;
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Write(ex, "Exception when responding to file deleted event.");
		//        _fullScanRequired = true;
		//    }
		//}

		//void _watcher_Renamed(object sender, RenamedEventArgs e)
		//{
		//    try
		//    {
		//        Log.Write(LogLevel.Debug, "Received file renamed event: {0}", e.FullPath);

		//        // Try to find an remove the old path, case insensitive.
		//        string oldFile = e.OldFullPath;
		//        foreach (string file in _files)
		//        {
		//            if (file.Equals(oldFile, StringComparison.OrdinalIgnoreCase))
		//            {
		//                _files.Remove(file);
		//                break;
		//            }
		//        }

		//        if (File.Exists(e.FullPath) && Theme.IsImageFile(e.FullPath)) _files.Add(e.FullPath);
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Write(ex, "Exception when responding to file renamed event.");
		//        _fullScanRequired = true;
		//    }
		//}

		//void _watcher_Error(object sender, ErrorEventArgs e)
		//{
		//    Log.Write(e.GetException(), "Exception reported by FileSystemWatcher.");
		//    _fullScanRequired = true;
		//}

		/// <summary>
		/// Gets a list of all files represented by this location. Returns full paths.
		/// </summary>
		public string[] Files
		{
			get
			{
				try
				{
					if (_isFile) return new string[] { _path };

					//if (_fullScanRequired)
					//{
					//    Log.Write(LogLevel.Debug, "Performing a full rescan of the directory '{0}'.", _path);
						_files.Clear();
						SearchDir(_path);
						_fullScanRequired = false;
					//}
					//else
					//{
					//    Log.Write(LogLevel.Debug, "Using cached list of files.");
					//}

					return _files.ToArray();
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Exception when getting files list.");
					return new string[0];
				}
			}
		}

		/// <summary>
		/// Searches a directory for image files.
		/// Will recurse subdirectories.
		/// </summary>
		/// <param name="dir">The full path of the directory to be searched.</param>
		private void SearchDir(string dir)
		{
			try
			{
				// Search for image files in this directory.
				string[] imageFiles = Directory.GetFiles(dir);
				foreach (string file in imageFiles)
				{
					if (Theme.IsImageFile(file)) _files.Add(file.ToLower());
				}

				// Search sub-folders in this directory.
				string[] subDirs = Directory.GetDirectories(dir);
				foreach (string subDir in subDirs) SearchDir(subDir);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when searching directory '{0}'.", dir);
			}
		}
	}
}
