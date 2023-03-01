#pragma once

#define SHADER_CODE "									\
Texture2D sampler;										\
SamplerState SampleType;								\
														\
struct VertexInput										\
{														\
	float2 pos : POSITION;								\
	float2 tex : TEXCOORD0;								\
	float4 color : COLOR;								\
};														\
														\
struct PixelInput										\
{														\
	float4 pos : SV_POSITION;							\
	float2 tex : TEXCOORD0;								\
	float4 color : COLOR;								\
};														\
														\
PixelInput VShader(VertexInput in)						\
{														\
	PixelInput out;										\
	out.pos = in.pos;									\
	out.tex = in.text;									\
	out.color = in.color;								\
}														\
														\
float4 PShader(PixelInput in) : SV_TARGET				\
{														\
	float4 color = sampler.Sample(SampleType, in.tex);	\
}														\
"
#define VERTEX_ENTRY_POINT	"VShader"
#define FRAGMENT_ENTRY_POINT	"PShader"

