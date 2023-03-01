Texture2D shaderTexture;
SamplerState SampleType;

cbuffer ConstantBuffer
{
	float4x4 transform;
	float4x4 colorMatrix;
	float2 feather1;
	float2 feather2;
	float2 texScale;
	float featherDist;
	float colorMatrixRatio;
	float blurDist;
};

static const float2 g_blurOffset[] = {
	float2(-3 / 3.0,-3 / 3.0), float2(-2 / 3.0,-3 / 3.0), float2(-1 / 3.0,-3 / 3.0), float2(0 / 3.0,-3 / 3.0), float2(1 / 3.0,-3 / 3.0), float2(2 / 3.0,-3 / 3.0), float2(3 / 3.0,-3 / 3.0),
	float2(-3 / 3.0,-2 / 3.0), float2(-2 / 3.0,-2 / 3.0), float2(-1 / 3.0,-2 / 3.0), float2(0 / 3.0,-2 / 3.0), float2(1 / 3.0,-2 / 3.0), float2(2 / 3.0,-2 / 3.0), float2(3 / 3.0,-2 / 3.0),
	float2(-3 / 3.0,-1 / 3.0), float2(-2 / 3.0,-1 / 3.0), float2(-1 / 3.0,-1 / 3.0), float2(0 / 3.0,-1 / 3.0), float2(1 / 3.0,-1 / 3.0), float2(2 / 3.0,-1 / 3.0), float2(3 / 3.0,-1 / 3.0),
	float2(-3 / 3.0, 0 / 3.0), float2(-2 / 3.0, 0 / 3.0), float2(-1 / 3.0, 0 / 3.0), float2(0 / 3.0, 0 / 3.0), float2(1 / 3.0, 0 / 3.0), float2(2 / 3.0, 0 / 3.0), float2(3 / 3.0, 0 / 3.0),
	float2(-3 / 3.0, 1 / 3.0), float2(-2 / 3.0, 1 / 3.0), float2(-1 / 3.0, 1 / 3.0), float2(0 / 3.0, 1 / 3.0), float2(1 / 3.0, 1 / 3.0), float2(2 / 3.0, 1 / 3.0), float2(3 / 3.0, 1 / 3.0),
	float2(-3 / 3.0, 2 / 3.0), float2(-2 / 3.0, 2 / 3.0), float2(-1 / 3.0, 2 / 3.0), float2(0 / 3.0, 2 / 3.0), float2(1 / 3.0, 2 / 3.0), float2(2 / 3.0, 2 / 3.0), float2(3 / 3.0, 2 / 3.0),
	float2(-3 / 3.0, 3 / 3.0), float2(-2 / 3.0, 3 / 3.0), float2(-1 / 3.0, 3 / 3.0), float2(0 / 3.0, 3 / 3.0), float2(1 / 3.0, 3 / 3.0), float2(2 / 3.0, 3 / 3.0), float2(3 / 3.0, 3 / 3.0)
};

static const float g_blurWeight[] = {
	0.00000067, 0.00002292, 0.00019117, 0.00038771, 0.00019117, 0.00002292, 0.00000067,
	0.00002292, 0.00078633, 0.00655965, 0.01330373, 0.00655965, 0.00078633, 0.00002292,
	0.00019117, 0.00655965, 0.05472157, 0.11098164, 0.05472157, 0.00655965, 0.00019117,
	0.00038771, 0.01330373, 0.11098164, 0.22508352, 0.11098164, 0.01330373, 0.00038771,
	0.00019117, 0.00655965, 0.05472157, 0.11098164, 0.05472157, 0.00655965, 0.00019117,
	0.00002292, 0.00078633, 0.00655965, 0.01330373, 0.00655965, 0.00078633, 0.00002292,
	0.00000067, 0.00002292, 0.00019117, 0.00038771, 0.00019117, 0.00002292, 0.00000067
};
static const int g_blurCount = 49;

struct VertexInput
{
	float2 pos : POSITION;
	float2 tex : TEXCOORD0;
	float4 color : COLOR;
};

struct PixelInput
{
	float4 pos : SV_POSITION;
	float2 tex : TEXCOORD0;
	float4 color : COLOR;
	float2 screenPos : SCREEN_POS;
};

PixelInput VShader(VertexInput input)
{
	PixelInput output;
	output.pos = mul(float4(input.pos, 0, 1), transform);
	output.screenPos = input.pos;
	output.tex = input.tex;
	output.color = input.color;
	return output;
}

float4 PShader(PixelInput input) : SV_TARGET
{
	float alpha = 1.0f;
	if (featherDist > 0.0f)
	{
		float fd = 0.0f;
		if (input.screenPos.x < feather1.x)
		{
			if (input.screenPos.y < feather1.y) fd = length(input.screenPos - feather1);
			else if (input.screenPos.y > feather2.y) fd = length(input.screenPos - float2(feather1.x, feather2.y));
			else fd = feather1.x - input.screenPos;
		}
		else if (input.screenPos.x > feather2.x)
		{
			if (input.screenPos.y < feather1.y) fd = length(input.screenPos - float2(feather2.x, feather1.y));
			else if (input.screenPos.y > feather2.y) fd = length(input.screenPos - feather2);
			else fd = input.screenPos - feather2.x;
		}
		else if (input.screenPos.y < feather1.y) fd = feather1.y - input.screenPos.y;
		else if (input.screenPos.y > feather2.y) fd = input.screenPos.y - feather2.y;

		if (fd > 0.0f) alpha = clamp((featherDist - fd) / featherDist, 0, 1);
	}

	float4 color;
	if (blurDist > 0.0f)
	{
		color = float4(0,0,0,0);
		for (int i = 0; i < g_blurCount; i++)
		{
			color += shaderTexture.Sample(SampleType, input.tex + g_blurOffset[i] * blurDist * texScale) * g_blurWeight[i];
		}
	}
	else color = shaderTexture.Sample(SampleType, input.tex);

	if (colorMatrixRatio > 0.0f)
	{
		float4 effect = mul(color, colorMatrix);
		color = color * (1.0f - colorMatrixRatio) + effect * colorMatrixRatio;
	}
	color *= input.color;
	color *= float4(1, 1, 1, alpha);
	return color;
}
