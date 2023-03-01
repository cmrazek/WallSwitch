#pragma once

namespace WallSwitch::DX11
{
	class Renderer;

	class Texture
	{
	public:
		Texture(const ptr<Renderer>& renderer, const void* bits, int imageWidth, int imageHeight, int rowSize);
		~Texture();

		ID3D11ShaderResourceView* GetShaderResourceView() const noexcept { return _srv; }

	private:
		ID3D11Texture2D* _texture = nullptr;
		ID3D11ShaderResourceView* _srv = nullptr;
		vec2 _scale;
	};
}
