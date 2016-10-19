using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	public enum ColorEffect
	{
		[Description("(none)")]
		None,
		Grayscale,
		Sepia,
		[Description("Intense Color")]
		IntenseColor
	}

	public static class ColorEffectEx
	{
		public static float[][] GetColorMatrixElements(this ColorEffect effect, float alpha)
		{
			switch (effect)
			{
				case ColorEffect.Grayscale:
					return new float[][]
					{
						new float[] {.3f, .3f, .3f, 0, 0},
						new float[] {.59f, .59f, .59f, 0, 0},
						new float[] {.11f, .11f, .11f, 0, 0},
						new float[] {0, 0, 0, alpha, 0},
						new float[] {0, 0, 0, 0, 1}
					};

				case ColorEffect.Sepia:
					return new float[][]
					{
						new float[] {0.393f, 0.349f, 0.272f, 0, 0},
						new float[] {0.769f, 0.686f, 0.534f, 0, 0},
						new float[] {0.189f, 0.168f, 0.131f, 0, 0},
						new float[] {     0,      0,      0, alpha, 0},
						new float[] {     0,      0,      0, 0, 1}
					};

				case ColorEffect.IntenseColor:
					return new float[][]
					{
						new float[] { 2.0f, -.5f, -.5f, 0.0f, 0.0f },
						new float[] { -.5f, 2.0f, -.5f, 0.0f, 0.0f },
						new float[] { -.5f, -.5f, 2.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, alpha, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
					};

				default:
					return new float[][]
					{
						new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, alpha, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
					};
			}
		}

		public static ColorMatrix GetColorMatrix(this ColorEffect effect, float alpha = 1.0f)
		{
			return new ColorMatrix(effect.GetColorMatrixElements(alpha));
		}

		public static ColorMatrix GetColorMatrixBlend(this ColorEffect effect, ColorEffect blendEffect, float ratio)
		{
			var a = GetColorMatrixElements(effect, 1.0f);
			var b = GetColorMatrixElements(blendEffect, 1.0f);
			for (var x = 0; x < 5; x++)
			{
				for (var y = 0; y < 5; y++)
				{
					a[x][y] = a[x][y] * (1.0f - ratio) + b[x][y] * ratio;
				}
			}
			return new ColorMatrix(a);
		}
	}
}
