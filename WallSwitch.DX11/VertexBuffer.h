#pragma once

namespace WallSwitch::DX11
{
	class Renderer;

	class VertexBuffer
	{
	public:
		VertexBuffer(const ptr<Renderer>& renderer, const void* data, int count, int stride);
		~VertexBuffer();

		ID3D11Buffer* GetBuffer() const noexcept { return _buf; }
		int GetStride() const noexcept { return _stride; }
		int GetCount() const noexcept { return _count; }

	private:
		ID3D11Buffer* _buf = nullptr;
		int _count;
		int _stride = 0;
	};
}
