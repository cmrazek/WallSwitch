using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch
{
	static class WallSwitchImgProc
	{
		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void ShowFileInExplorer(string fileName);

		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BlurImage(IntPtr pImageBits, int width, int height, int iImageFormat, int stride, int blurDist);

		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int TransferChannel32(IntPtr pSrcImageBits, int srcChannel, int srcStride, IntPtr pDstImageBits, int dstChannel, int dstStride, int width, int height);

		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint webpGetSize(IntPtr data, uint dataSize);

		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int webpLoadARGB(IntPtr data, uint dataSize, IntPtr destImageBits, uint destSize, uint destStride);
	}
}
