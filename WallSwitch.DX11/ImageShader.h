#pragma once

#include "Shader.h"

namespace WallSwitch::DX11
{
	class Texture;

	struct ImageVertex
	{
		vec2 pos;
		vec2 tex;
		vec4 color;
	};

	struct ImageConstantBuffer
	{
		float transform[16];
		float colorMatrix[16];
		vec2 feather1;
		vec2 feather2;
		vec2 texScale;
		float featherDist;
		float colorMatrixRatio;
		float blurDist;
		float padding[3];
	};

	class ImageShader : public Shader
	{
	public:
		ImageShader(const ptr<Renderer>& renderer);
		virtual ~ImageShader();

		void SetState(const ptr<Texture>& texture,
			const vec2& feather1, const vec2& feather2, float featherDist,
			const float* colorMatrix, float colorMatrixRatio,
			float blurDist,
			const vec2& texScale);

	protected:
		virtual void UpdateConstantBuffer(void* data) override;
		virtual void UseInner() override;

	private:
		ptr<Texture> _texture;
		ID3D11SamplerState* _samplerState = nullptr;
		vec2 _feather1;
		vec2 _feather2;
		float _featherDist = 0.0f;
		float _colorMatrix[16] = { };
		float _colorMatrixRatio = 0.0f;
		float _blurDist = 0.0f;
		vec2 _texScale;
	};
}
