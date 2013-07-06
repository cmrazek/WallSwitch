#include "Common.h"
#include "BlurImage.h"
#include <math.h>

extern "C" __declspec(dllexport) int __cdecl BlurImage(void *pImageBits, int width, int height, int iImageFormat, int stride, int blurDist)
{
	if (iImageFormat == IMGFORMAT_32BPP_ARGB)
	{
		return BlurImage32(pImageBits, width, height, stride, blurDist);
	}
	return E_SUCCESS;
}

int BlurImage32(void *pImageBits, int width, int height, int stride, int blurDist)
{
	if (pImageBits == NULL) return E_IMAGE_NULL;
	if (width <= 0 || height <= 0) return E_INVALID_DIMENSIONS;
	if (stride < width * 4) return E_INVALID_STRIDE;
	if (blurDist <= 0) return E_INVALID_BLUR_DIST;

	BYTE*	pBuf = new BYTE[width < height ? height * 4 : width * 4];
	BYTE*	pBufPos;
	BYTE*	pImage = reinterpret_cast<BYTE*>(pImageBits);
	BYTE*	pLine;
	BYTE*	pLinePos;

	int		filterLen = blurDist * 2 + 1;
	
	DWORD*	pFilter = new DWORD[filterLen];
	DWORD	filterVal;

#define FILTER_SCALE	65536.0
#define FILTER_BITS		16

	double*	pFilterD = new double[filterLen];
	double filterScale = 0.0;
	for (int f = 0; f < filterLen; f++)
	{
		pFilterD[f] = floor(GaussElement(f, filterLen) * FILTER_SCALE);
		filterScale += pFilterD[f];
	}
	if (filterScale > 0.0)
	{
		filterScale = FILTER_SCALE / filterScale;
		for (int f = 0; f < filterLen; f++)
		{
			pFilterD[f] *= filterScale;
		}
	}
	for (int f = 0; f < filterLen; f++)
	{
		pFilter[f] = static_cast<int>(floor(pFilterD[f] + .5));
	}
	delete [] pFilterD;

	int		x, y, i, min, max, start, end, xOff;
	DWORD	r, g, b, a;

	// Horizontal Blur
	for (y = 0; y < height; y++)
	{
		pLine = pImage + stride * y;
		pLinePos = pLine;
		memcpy(pBuf, pLine, stride);

		for (x = 0; x < width; x++)
		{
			min = x - blurDist;
			max = x + blurDist;
			start = min < 0 ? 0 : min;
			end = max >= width ? width - 1 : max;

			r = g = b = a = 0;

			pBufPos = pBuf + start * 4;
			for (i = start; i <= end; i++)
			{
				filterVal = pFilter[i - min];
				b += (*(pBufPos++) * filterVal) >> FILTER_BITS;
				g += (*(pBufPos++) * filterVal) >> FILTER_BITS;
				r += (*(pBufPos++) * filterVal) >> FILTER_BITS;
				a += (*(pBufPos++) * filterVal) >> FILTER_BITS;
			}

			if (r > 255) r = 255;
			if (g > 255) g = 255;
			if (b > 255) b = 255;
			if (a > 255) a = 255;

			*(pLinePos++) = (BYTE)b;
			*(pLinePos++) = (BYTE)g;
			*(pLinePos++) = (BYTE)r;
			*(pLinePos++) = (BYTE)a;
		}
	}

	// Vertical Blur
	for (x = 0; x < width; x++)
	{
		xOff = x * 4;
		pLine = pImage + xOff;
		pBufPos = pBuf;
		y = height;
		while (y--)
		{
			*reinterpret_cast<DWORD*>(pBufPos) = *reinterpret_cast<DWORD*>(pLine);
			pLine += stride;
			pBufPos += 4;
		}

		for (y = 0; y < height; y++)
		{
			min = y - blurDist;
			max = y + blurDist;
			start = min < 0 ? 0 : min;
			end = max >= height ? height - 1 : max;

			r = g = b = a = 0;

			pBufPos = pBuf + start * 4;
			for (i = start; i <= end; i++)
			{
				filterVal = pFilter[i - min];
				b += (*(pBufPos++) * filterVal) >> FILTER_BITS;
				g += (*(pBufPos++) * filterVal) >> FILTER_BITS;
				r += (*(pBufPos++) * filterVal) >> FILTER_BITS;
				a += (*(pBufPos++) * filterVal) >> FILTER_BITS;
			}

			if (r > 255) r = 255;
			if (g > 255) g = 255;
			if (b > 255) b = 255;
			if (a > 255) a = 255;

			pLinePos = pImage + stride * y + xOff;
			*(pLinePos++) = (BYTE)b;
			*(pLinePos++) = (BYTE)g;
			*(pLinePos++) = (BYTE)r;
			*(pLinePos++) = (BYTE)a;
		}
	}

	delete [] pBuf;
	delete [] pFilter;

	return E_SUCCESS;
}

double Gauss(double x, double m, double d)
{
	return 1.0 / (d * sqrt(2.0 * PI)) * pow(E, ((x - m) * (x - m)) * -1.0 / 2.0 * d * d);
}

double GaussElement(int index, int arrayLength)
{
	int center = arrayLength / 2;
	double extent = arrayLength - center;
	double x = static_cast<double>(center - index) / extent * 3.0;
	return Gauss(x, 0.0, 1.0) / (extent / 3.0);
}
