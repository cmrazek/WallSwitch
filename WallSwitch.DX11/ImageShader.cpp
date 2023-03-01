#include "pch.h"
#include "ImageShader.h"
#include "Renderer.h"
#include "Texture.h"

namespace WallSwitch::DX11
{
	ImageShader::ImageShader(const ptr<Renderer>& renderer)
		: Shader(renderer)
	{
		Create("ImageShader.hlsl", "VShader", "PShader", SHADER_FLAG_TEXTURE | SHADER_FLAG_VS_CONSTANT_BUFFER | SHADER_FLAG_PS_CONSTANT_BUFFER, sizeof(ImageConstantBuffer));
	}

	ImageShader::~ImageShader()
	{
		if (_samplerState) _samplerState->Release();
	}

	void ImageShader::UseInner()
	{
		CHECK(_texture != nullptr);

		auto srv = _texture->GetShaderResourceView();
		auto r = _renderer.lock();
		auto ctx = r->GetContext();
		ctx->PSSetShaderResources(0, 1, &srv);

		if (_samplerState == nullptr)
		{
			D3D11_SAMPLER_DESC sd;
			sd.Filter = D3D11_FILTER_ANISOTROPIC;
			sd.AddressU = D3D11_TEXTURE_ADDRESS_CLAMP;
			sd.AddressV = D3D11_TEXTURE_ADDRESS_CLAMP;
			sd.AddressW = D3D11_TEXTURE_ADDRESS_CLAMP;
			sd.MipLODBias = 0.0f;
			sd.MaxAnisotropy = 4;
			sd.ComparisonFunc = D3D11_COMPARISON_ALWAYS;
			sd.BorderColor[0] = 0;
			sd.BorderColor[1] = 0;
			sd.BorderColor[2] = 0;
			sd.BorderColor[3] = 0;
			sd.MinLOD = 0;
			sd.MaxLOD = D3D11_FLOAT32_MAX;
			CHECK_HRESULT(r->GetDevice()->CreateSamplerState(&sd, &_samplerState));
		}
		ctx->PSSetSamplers(0, 1, &_samplerState);
	}

	void ImageShader::UpdateConstantBuffer(void* data)
	{
		auto r = _renderer.lock();
		auto cb = reinterpret_cast<ImageConstantBuffer*>(data);

		r->CreateTransformMatrix(cb->transform);

		cb->feather1 = _feather1;
		cb->feather2 = _feather2;
		cb->featherDist = _featherDist;

		memcpy(cb->colorMatrix, _colorMatrix, sizeof(float) * 16);
		cb->colorMatrixRatio = _colorMatrixRatio;

		cb->blurDist = _blurDist;
		cb->texScale = _texScale;
	}

	void ImageShader::SetState(const ptr<Texture>& texture,
		const vec2& feather1, const vec2& feather2, float featherDist,
		const float* colorMatrix, float colorMatrixRatio,
		float blurDist,
		const vec2& texScale)
	{
		_texture = texture;
		_feather1 = feather1;
		_feather2 = feather2;
		_featherDist = featherDist;
		memcpy(_colorMatrix, colorMatrix, sizeof(float) * 16);
		_colorMatrixRatio = colorMatrixRatio;
		_blurDist = blurDist;
		_texScale = texScale;
	}
}
