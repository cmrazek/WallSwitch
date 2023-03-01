#pragma once

namespace WallSwitch::DX11
{
	class Renderer;

#define SHADER_FLAG_TEXTURE				0x01
#define SHADER_FLAG_VS_CONSTANT_BUFFER	0x02
#define SHADER_FLAG_PS_CONSTANT_BUFFER	0x04

	class Shader
	{
	public:
		Shader(const ptr<Renderer>& renderer);
		virtual ~Shader();

		void Create(const char* src, const char* vertexEntryPoint, const char* pixelEntryPoint, int shaderFlags, int constantBufferSize);
		void Use();
		void Release();

	protected:
		virtual void UpdateConstantBuffer(void* data) = 0;
		virtual void UseInner() { }

		weakptr<Renderer> _renderer;

	private:
		std::string _src;
		std::string _vertexEntryPoint;
		std::string _pixelEntryPoint;
		int _flags = 0;
		int _constantBufferSize = 0;
		int _rendererVersion = 0;
		ID3DBlob* _vsCode = nullptr;
		ID3DBlob* _psCode = nullptr;
		ID3D11VertexShader* _vsShader = nullptr;
		ID3D11PixelShader* _psShader = nullptr;
		ID3D11InputLayout* _vertexLayout = nullptr;
		ID3D11Buffer* _constantBuffer = nullptr;

		void Compile();
	};
}
