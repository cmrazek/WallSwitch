#include "pch.h"
#include "Renderer.h"

namespace WallSwitch::DX11
{
	Renderer::Renderer()
	{
	}

	Renderer::~Renderer()
	{
		Shutdown();
	}

	void Renderer::Setup(int width, int height)
	{
		Shutdown();

		_width = width;
		_height = height;

		HRESULT hr;
		ID3D11Device* device = nullptr;
		ID3D11DeviceContext* context = nullptr;

		UINT flags = D3D11_CREATE_DEVICE_SINGLETHREADED;
#ifdef _DEBUG
		flags |= D3D11_CREATE_DEVICE_DEBUG;
#endif
		if (FAILED(hr = D3D11CreateDevice(nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr, flags,
			nullptr, 0, D3D11_SDK_VERSION, &device, nullptr, &context)))
		{
			throw gcnew DirectXInitializationException(hr, L"Failed to create DirectX device");
		}

		_device = device;
		_context = context;

#define TEXTURE_FORMAT	DXGI_FORMAT_B8G8R8A8_UNORM

		D3D11_TEXTURE2D_DESC td = { };
		td.Width = _width;
		td.Height = _height;
		td.ArraySize = 1;
		td.SampleDesc.Count = 1;
		td.Format = TEXTURE_FORMAT;
		td.BindFlags = D3D11_BIND_RENDER_TARGET;
		if (FAILED(hr = _device->CreateTexture2D(&td, nullptr, &_targetTexture)))
		{
			throw gcnew DirectXInitializationException(hr, L"Failed to create target texture.");
		}

		td.BindFlags = 0;
		td.Usage = D3D11_USAGE_STAGING;
		td.CPUAccessFlags = D3D11_CPU_ACCESS_READ;
		if (FAILED(hr = _device->CreateTexture2D(&td, nullptr, &_image)))
		{
			throw gcnew DirectXInitializationException(hr, L"Failed to create image texture.");
		}

		D3D11_RENDER_TARGET_VIEW_DESC tv = { };
		tv.Format = TEXTURE_FORMAT;
		tv.ViewDimension = D3D11_RTV_DIMENSION_TEXTURE2D;
		tv.Texture2D.MipSlice = 0;
		if (FAILED(hr = _device->CreateRenderTargetView(_targetTexture, &tv, &_targetView)))
		{
			throw gcnew DirectXInitializationException(hr, L"Failed to create render target view.");
		}
		_context->OMSetRenderTargets(1, &_targetView, nullptr);

		D3D11_VIEWPORT viewport = { };
		viewport.TopLeftX = 0;
		viewport.TopLeftY = 0;
		viewport.Width = float(width);
		viewport.Height = float(height);
		_context->RSSetViewports(1, &viewport);

		D3D11_BLEND_DESC blend = { };
		blend.AlphaToCoverageEnable = false;
		blend.RenderTarget[0].BlendEnable = TRUE;
		blend.RenderTarget[0].SrcBlend = D3D11_BLEND_SRC_ALPHA;
		blend.RenderTarget[0].DestBlend = D3D11_BLEND_INV_SRC_ALPHA;
		blend.RenderTarget[0].BlendOp = D3D11_BLEND_OP_ADD;
		blend.RenderTarget[0].SrcBlendAlpha = D3D11_BLEND_ONE;
		blend.RenderTarget[0].DestBlendAlpha = D3D11_BLEND_ZERO;
		blend.RenderTarget[0].BlendOpAlpha = D3D11_BLEND_OP_ADD;
		blend.RenderTarget[0].RenderTargetWriteMask = D3D10_COLOR_WRITE_ENABLE_ALL;
		if (FAILED(hr = _device->CreateBlendState(&blend, &_blendState)))
		{
			throw gcnew DirectXInitializationException(hr, L"Failed to create blend state.");
		}

		_initialized = true;
		_version++;
	}

	void Renderer::Shutdown()
	{
		_initialized = false;

		if (_blendState) { _blendState->Release(); _blendState = nullptr; }
		if (_rasterState) { _rasterState->Release(); _rasterState = nullptr; }
		if (_targetView) { _targetView->Release(); _targetView = nullptr; }
		if (_targetTexture) { _targetTexture->Release(); _targetTexture = nullptr; }
		if (_image) { _image->Release(); _image = nullptr; }
		if (_device) { _device->Release(); _device = nullptr; }
		if (_context) { _context->Release(); _context = nullptr; }
	}

	void Renderer::InitializeFrame(int width, int height)
	{
		if (width <= 0 || height <= 0) return;
		if (_width != width || _height != height)
		{
			Setup(width, height);
		}
		InitializeFrame();
	}

	void Renderer::InitializeFrame()
	{
		HRESULT hr;

		if (_rasterState == nullptr)
		{
			D3D11_RASTERIZER_DESC rd = { };
			rd.FillMode = D3D11_FILL_SOLID;
			rd.CullMode = D3D11_CULL_NONE;
			if (FAILED(hr = _device->CreateRasterizerState(&rd, &_rasterState)))
				throw gcnew DirectXInitializationException(hr, "Failed to create rasterizer state.");
		}
		_context->RSSetState(_rasterState);

		float blendFactor[4] = { 0.0f, 0.0f, 0.0f, 0.0f };
		UINT sampleMask = 0xffffffff;
		_context->OMSetBlendState(_blendState, blendFactor, sampleMask);
	}

	void Renderer::EndFrame(void* dstBits, size_t rowSize)
	{
		if (!_initialized) throw gcnew DirectXNotInitializedException();

		_context->Flush();
		_context->CopyResource(_image, _targetTexture);

		HRESULT hr;
		D3D11_MAPPED_SUBRESOURCE resource = { };
		UINT resourceId = D3D11CalcSubresource(0, 0, 0);
		if (FAILED(hr = _context->Map(_image, resourceId, D3D11_MAP_READ, 0, &resource)))
		{
			throw gcnew DirectXRunTimeException(hr, L"Failed to map target texture.");
		}
		try
		{
			BYTE* src = static_cast<BYTE*>(resource.pData);
			BYTE* dst = static_cast<BYTE*>(dstBits);
			size_t rowSize = _width * 4;
			for (int y = 0; y < _height; y++)
			{
				memcpy(dst, src, rowSize);
				src += resource.RowPitch;
				dst += rowSize;
			}
		}
		catch (...)
		{
			_context->Unmap(_targetTexture, resourceId);
		}
	}

	void Renderer::Clear(float r, float g, float b)
	{
		if (!_initialized) throw gcnew DirectXNotInitializedException();

		FLOAT colorF[4] = { r, g, b, 1.0 };
		_context->ClearRenderTargetView(_targetView, colorF);
	}

	ptr<VertexBuffer> Renderer::CreateVertexBuffer(const void* data, int count, int stride)
	{
		return std::make_shared<VertexBuffer>(shared_from_this(), data, count, stride);
	}

	ptr<SolidColorShader> Renderer::GetSolidColorShader()
	{
		if (!_solidColorShader) _solidColorShader = std::make_shared<SolidColorShader>(shared_from_this());
		return _solidColorShader;
	}

	ptr<ImageShader> Renderer::GetImageShader()
	{
		if (!_imageShader) _imageShader = std::make_shared<ImageShader>(shared_from_this());
		return _imageShader;
	}

	void Renderer::DrawTriangles(const ptr<Shader>& shader, const ptr<VertexBuffer>& vb, int vertexCount, int vertexOffset)
	{
		shader->Use();

		auto buf = vb->GetBuffer();
		UINT bufStride = vb->GetStride();
		UINT bufOffset = 0;
		_context->IASetVertexBuffers(0, 1, &buf, &bufStride, &bufOffset);
		_context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
		_context->Draw(vertexCount, vertexOffset);
	}

	void Renderer::CreateTransformMatrix(float* mat)
	{
		// 0  1  2  3
		// 4  5  6  7
		// 8  9  10 11
		// 12 13 14 15

		for (int i = 0; i < 15; i++) mat[i] = 0.0f;
		mat[0] = 2.0f / float(_width);
		mat[3] = -1.0f;
		mat[5] = -2.0f / float(_height);
		mat[7] = 1.0f;
		mat[10] = 1.0f;
		mat[15] = 1.0f;
	}
}
