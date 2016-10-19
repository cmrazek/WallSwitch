using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WallSwitch
{
	class NativeMethods
	{
		public const int HWND_BROADCAST = 0xffff;
		public const int WM_HOTKEY = 0x0312;
		public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");

		[DllImport("user32")]
		public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32")]
		public static extern int RegisterWindowMessage(string message);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(IntPtr hWnd);
	}
}
