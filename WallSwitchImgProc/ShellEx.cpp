#include "Common.h"

#include <ShlObj.h>

extern "C" __declspec(dllexport) void __cdecl ShowFileInExplorer(const wchar_t* fileName)
{
	auto pidl = ILCreateFromPath(fileName);
	if (pidl)
	{
		SHOpenFolderAndSelectItems(pidl, 0, NULL, 0);
		ILFree(pidl);
	}
}
