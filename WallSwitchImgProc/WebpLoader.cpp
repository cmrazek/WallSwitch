#include "Common.h"

#include "webp\decode.h"
#include "webp\types.h"

extern "C" __declspec(dllexport) DWORD webpGetSize(const byte* data, DWORD dataSize)
{
	// Gets the sizeof the webp image from the data buffer provided.
	// Returns 0 if the data is not a valid webp image.

	int width = 0;
	int height = 0;
	if (!WebPGetInfo(data, dataSize, &width, &height)) return 0;
	return (width << 16) | height;
}

extern "C" __declspec(dllexport) int webpLoadARGB(const byte* data, DWORD dataSize, byte* destImageBits, DWORD destSize, DWORD destStride)
{
	// Loads the webp from the buffer into the image bits.
	// Use webpGetSize() in order to know what the size of destImageBits buffer should be (width * height * 4)

	if (!WebPDecodeBGRAInto(data, dataSize, destImageBits, destSize, destStride)) return E_WEBP_ERROR;
	return E_SUCCESS;
}
