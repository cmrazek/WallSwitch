#include "pch.h"
#include "Renderer.h"
#include "SolidColorShader.h"

namespace WallSwitch::DX11
{
	SolidColorShader::SolidColorShader(const ptr<Renderer>& renderer)
		: Shader(renderer)
	{
		Create("SolidColorShader.hlsl", "VShader", "PShader", SHADER_FLAG_VS_CONSTANT_BUFFER | SHADER_FLAG_PS_CONSTANT_BUFFER, sizeof(SolidColorConstantBuffer));
	}

	SolidColorShader::~SolidColorShader() { }

	void SolidColorShader::UpdateConstantBuffer(void* data)
	{
		auto r = _renderer.lock();
		auto cb = reinterpret_cast<SolidColorConstantBuffer*>(data);

		r->CreateTransformMatrix(cb->transform);

		cb->feather1 = _feather1;
		cb->feather2 = _feather2;
		cb->featherDist = _featherDist;
	}
}
