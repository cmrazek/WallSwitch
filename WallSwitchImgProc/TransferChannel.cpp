#include "Common.h"

extern "C" __declspec(dllexport) int __cdecl TransferChannel32(void *pSrcImageBits, int srcChannel, int srcStride, void *pDstImageBits, int dstChannel, int dstStride, int width, int height)
{
	if (pSrcImageBits == NULL || pDstImageBits == NULL) return E_IMAGE_NULL;
	if (width <= 0 || height <= 0) return E_INVALID_DIMENSIONS;
	if (srcStride < width * 4 || dstStride < width * 4) return E_INVALID_STRIDE;
	if (srcChannel < 0 || srcChannel >= 4 || dstChannel < 0 || dstChannel >= 4) return E_INVALID_CHANNEL;
	if (srcChannel == dstChannel) return E_INVALID_CHANNEL;

	BYTE*	pSrc = reinterpret_cast<BYTE*>(pSrcImageBits);
	BYTE*	pDst = reinterpret_cast<BYTE*>(pDstImageBits);
	BYTE*	pSrcLine = pSrc + srcChannel;
	BYTE*	pDstLine = pDst + dstChannel;
	BYTE*	pEndLine = pSrcLine + height * srcStride;
	BYTE*	pSrcX;
	BYTE*	pDstX;
	BYTE*	pEndX;

	while (pSrcLine != pEndLine)
	{
		pSrcX = pSrcLine;
		pDstX = pDstLine;
		pEndX = pSrcX + width * 4;
		while (pSrcX != pEndX)
		{
			*pDstX = *pSrcX;
			pSrcX += 4;
			pDstX += 4;
		}
		pSrcLine += srcStride;
		pDstLine += dstStride;
	}

	return E_SUCCESS;
}
