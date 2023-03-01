#include "pch.h"
#include "Renderer.h"
#include "VertexBuffer.h"

namespace WallSwitch::DX11
{
	VertexBuffer::VertexBuffer(const ptr<Renderer>& renderer, const void* data, int count, int stride)
	{
		HRESULT hr;
		D3D11_BUFFER_DESC bd = { };
		bd.Usage = D3D11_USAGE_DYNAMIC;
		bd.ByteWidth = count * stride;
		bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
		bd.CPUAccessFlags = D3D11_CPU_ACCESS_WRITE;

		if (FAILED(hr = renderer->GetDevice()->CreateBuffer(&bd, nullptr, &_buf)))
			throw gcnew DirectXRunTimeException(hr, "Failed to create vertex buffer.");

		_count = count;
		_stride = stride;

		auto dc = renderer->GetContext();

		D3D11_MAPPED_SUBRESOURCE ms = { };
		if (FAILED(hr = dc->Map(_buf, 0, D3D11_MAP_WRITE_DISCARD, 0, &ms)))
			throw gcnew DirectXRunTimeException(hr, "Failed to map vertex buffer data.");
		memcpy(ms.pData, data, count * stride);
		dc->Unmap(_buf, 0);
	}

	VertexBuffer::~VertexBuffer()
	{
		if (_buf) _buf->Release();
	}
}
