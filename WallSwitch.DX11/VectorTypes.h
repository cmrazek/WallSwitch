#pragma once

namespace WallSwitch::DX11
{
	class vec2
	{
	public:
		float x, y;

		vec2() : x(0), y(0) { }
		vec2(float x, float y) : x(x), y(y) { }
		vec2(int x, int y) : x(float(x)), y(float(y)) { }
		vec2(const vec2& v) : x(v.x), y(v.y) { }
	};

	class vec4
	{
	public:
		float x, y, z, w;

		vec4() : x(0), y(0), z(0), w(0) { }
		vec4(float x, float y, float z, float w) : x(x), y(y), z(z), w(w) { }
		vec4(const vec4& v) : x(v.x), y(v.y), z(v.z), w(v.w) { }
	};
}
