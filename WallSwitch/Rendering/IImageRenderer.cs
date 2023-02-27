using System;
using System.Drawing;

namespace WallSwitch.Rendering
{
    public interface IImageRenderer : IDisposable
    {
        void InitializeFrame(Bitmap lastImage);
        void InitializeFrame(int width, int height);
        void Clear(Color color);
        void DrawBackgroundTint(Color topColor, Color bottomColor);
        void DrawImage(Image image, Rectangle rect, ColorEffect colorEffect = ColorEffect.None, float colorEffectRatio = 1.0f, int blurDistance = 0, int featherDistance = 0);
        void DrawSolidRect(Rectangle rect, Color color, int featherDistance = 0);

        Bitmap Image { get; }
    }
}
