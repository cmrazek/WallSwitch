#pragma once

#include "Shader.h"

namespace WallSwitch::DX11
{
	struct SolidColorVertex
	{
		vec2 pos;
		vec4 color;
	};

	struct SolidColorConstantBuffer
	{
		float transform[16];
		vec2 feather1;
		vec2 feather2;
		float featherDist;
		float padding[3];
	};

	class SolidColorShader : public Shader
	{
	public:
		SolidColorShader(const ptr<Renderer>& renderer);
		virtual ~SolidColorShader();

		void SetFeather(const vec2& feather1, const vec2& feather2, float featherDist) { _feather1 = feather1; _feather2 = feather2; _featherDist = featherDist; }

	protected:
		virtual void UpdateConstantBuffer(void* data) override;

	private:
		vec2 _feather1;
		vec2 _feather2;
		float _featherDist = 0.0f;
	};
}
