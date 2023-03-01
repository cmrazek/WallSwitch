#pragma once

#include "ImageShader.h"
#include "SolidColorShader.h"
#include "VertexBuffer.h"

namespace WallSwitch::DX11
{
	class Renderer : public std::enable_shared_from_this<Renderer>
	{
	public:
		Renderer();
		~Renderer();

		void InitializeFrame(int width, int height);
		void EndFrame(void* dstBits, size_t rowSize);
		void Clear(float r, float g, float b);

		ID3D11Device* GetDevice() const noexcept { return _device; }
		ID3D11DeviceContext* GetContext() const noexcept { return _context; }

		int GetWidth() const noexcept { return _width; }
		int GetHeight() const noexcept { return _height; }
		int GetVersion() const noexcept { return _version; }

		ptr<SolidColorShader> GetSolidColorShader();
		ptr<ImageShader> GetImageShader();

		ptr<VertexBuffer> CreateVertexBuffer(const void* data, int count, int stride);
		template<typename T> ptr<VertexBuffer> CreateVertexBuffer(const T* data, int count) { return CreateVertexBuffer(data, count, sizeof(T)); }
		template<typename T> ptr<VertexBuffer> CreateVertexBuffer(const std::vector<T>& data) { return CreateVertexBuffer(data.data(), int(data.size()), sizeof(T)); }

		void DrawTriangles(const ptr<Shader>& shader, const ptr<VertexBuffer>& vb, int vertexCount, int vertexOffset);

		void CreateTransformMatrix(float* mat);
		static void CreateIdentityMatrix(float* mat);

	private:
		bool _initialized = false;
		ID3D11Device* _device = nullptr;
		ID3D11DeviceContext* _context = nullptr;
		ID3D11BlendState* _blendState = nullptr;
		ID3D11Texture2D* _targetTexture = nullptr;
		ID3D11RenderTargetView* _targetView = nullptr;
		ID3D11Texture2D* _image = nullptr;
		ID3D11RasterizerState* _rasterState = nullptr;

		int _width = 0;
		int _height = 0;
		int _version = 0;

		ptr<SolidColorShader> _solidColorShader;
		ptr<ImageShader> _imageShader;

		void Setup(int width, int height);
		void Shutdown();
		void InitializeFrame();
		void CreateShader();
	};
}
