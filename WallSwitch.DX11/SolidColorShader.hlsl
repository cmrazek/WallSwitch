cbuffer ConstantBuffer
{
	float4x4 transform;
	float2 feather1;
	float2 feather2;
	float featherDist;
};

struct VertexInput
{
	float2 pos : POSITION;
	float4 color : COLOR;
};

struct PixelInput
{
	float4 pos : SV_POSITION;
	float4 color : COLOR;
	float2 screenPos : SCREEN_POS;
};

PixelInput VShader(VertexInput input)
{
	PixelInput output;
	output.pos = mul(float4(input.pos, 0, 1), transform);
	output.screenPos = input.pos;
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

	return input.color * float4(1, 1, 1, alpha);
}
