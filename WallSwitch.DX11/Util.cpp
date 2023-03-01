#include "pch.h"
#include "Util.h"

namespace WallSwitch::DX11
{
	int RoundUpToPowerOfTwo(int value)
	{
		int pot = 1;
		while (pot < value) pot <<= 1;
		return pot;
	}

	bool IsPowerOfTwo(int value)
	{
		return RoundUpToPowerOfTwo(value) == value;
	}
}
