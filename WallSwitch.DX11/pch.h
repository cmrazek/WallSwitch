#pragma once

#define WIN32_LEAN_AND_MEAN
#define NOMINMAX
#include <Windows.h>
#include <d3d11.h>
#include <d3dcompiler.h>
//#include <DirectXMath.h>
#include <string>
#include <memory>
#include <vector>

#pragma comment (lib, "d3d11.lib")
#pragma comment (lib, "d3dcompiler.lib")
//using namespace DirectX;

using namespace System;
using namespace System::Drawing;
using namespace System::Drawing::Imaging;
using namespace System::IO;

#include "VectorTypes.h"

namespace WallSwitch::DX11
{
	extern HRESULT g_hr;

#define CHECK(stmt) if (!(stmt)) throw gcnew DirectXRunTimeException(String::Format("Check Failed: {0}", gcnew String(#stmt)))
#define CHECK_HRESULT(stmt) if (FAILED(g_hr = (stmt))) throw gcnew DirectXRunTimeException(g_hr, String::Format("Check Failed: {0} (HRESULT: 0x{1:X8})", gcnew String(#stmt), g_hr))

	template<class T> using ptr = std::shared_ptr<T>;
	template<class T> using weakptr = std::weak_ptr<T>;

	ref class DirectXInitializationException : public HardwareAccelerationException
	{
	public:
		DirectXInitializationException(HRESULT hr, String^ message) : HardwareAccelerationException(String::Format(L"{0} (HRESULT: {1})", message, hr)) { }
	};

	ref class DirectXNotInitializedException : public HardwareAccelerationException
	{
	public:
		DirectXNotInitializedException() : HardwareAccelerationException(L"DirectX has not been correctly initialized.") { }
	};

	ref class DirectXRunTimeException : public HardwareAccelerationException
	{
	public:
		DirectXRunTimeException(HRESULT hr, String^ message) : HardwareAccelerationException(String::Format(L"{0} (HRESULT: {1})", message, hr)) { }
		DirectXRunTimeException(String^ message) : HardwareAccelerationException(message) { }
	};
}
