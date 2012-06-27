using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WallSwitch
{
	public static class FileUtils
	{
		/// <summary>
		/// Opens explorer with the specified file selected.
		/// </summary>
		/// <param name="fileName">The file to be selected.</param>
		public static void ExploreFile(string fileName)
		{
			Process.Start("explorer.exe", string.Format("/select,{0}", fileName));
		}

		/// <summary>
		/// Opens explorer at the specified directory.
		/// </summary>
		/// <param name="dirPath">The directory to be opened.</param>
		public static void ExploreDir(string dirPath)
		{
			Process.Start(dirPath);
		}
	}
}
