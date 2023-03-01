#include "pch.h"
#include "Renderer.h"
#include "Texture.h"
#include "Util.h"

namespace WallSwitch::DX11
{
	Texture::Texture(const ptr<Renderer>& renderer, const void* bits, int imageWidth, int imageHeight, int rowSize)
	{
		if (bits == nullptr) throw gcnew ArgumentNullException("bits");
		if (imageWidth <= 0) throw gcnew ArgumentOutOfRangeException("imageWidth");
		if (imageHeight <= 0) throw gcnew ArgumentOutOfRangeException("imageWidth");
		if (rowSize < imageWidth * 4) throw gcnew ArgumentOutOfRangeException("rowSize");

		if (!IsPowerOfTwo(imageWidth)) throw gcnew ArgumentException("Image width is not a power of 2.");
		if (!IsPowerOfTwo(imageHeight)) throw gcnew ArgumentException("Image height is not a power of 2.");

		D3D11_TEXTURE2D_DESC td = { };
		td.Width = imageWidth;
		td.Height = imageHeight;
		td.MipLevels = 0;
		td.ArraySize = 1;
		td.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
		td.SampleDesc.Count = 1;
		td.SampleDesc.Quality = 0;
		td.Usage = D3D11_USAGE_DEFAULT;
		td.BindFlags = D3D11_BIND_SHADER_RESOURCE | D3D11_BIND_RENDER_TARGET;
		td.CPUAccessFlags = 0;
		td.MiscFlags = 0;

		auto dev = renderer->GetDevice();
		auto context = renderer->GetContext();
		HRESULT hr;

		if (FAILED(hr = dev->CreateTexture2D(&td, nullptr, &_texture)))
			throw gcnew DirectXRunTimeException(hr, "Failed to create texture.");

		context->UpdateSubresource(_texture, 0, nullptr, bits, rowSize, 0);

		D3D11_SHADER_RESOURCE_VIEW_DESC srv = { };
		srv.Format = td.Format;
		srv.ViewDimension = D3D11_SRV_DIMENSION_TEXTURE2D;
		srv.Texture2D.MostDetailedMip = 0;
		srv.Texture2D.MipLevels = 1;

		if (FAILED(hr = dev->CreateShaderResourceView(_texture, &srv, &_srv)))
			throw gcnew DirectXRunTimeException(hr, "Failed to create shader resource view.");
	}

	Texture::~Texture()
	{
		if (_texture) _texture->Release();
		if (_srv) _srv->Release();
	}
}
