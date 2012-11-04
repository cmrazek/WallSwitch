#pragma once

#define IMGFORMAT_32BPP_ARGB	0

int BlurImage32(void *pImageBits, int width, int height, int stride, int blurDist);
double Gauss(double x, double m, double d);
double GaussElement(int index, int arrayLength);
