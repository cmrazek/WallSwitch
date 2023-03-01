#pragma once

namespace WallSwitch::DX11
{
	class Renderer;

	struct DX11RendererPinnedData
	{
		ptr<Renderer> renderer;
	};

	public ref class DX11Renderer : public WallSwitch::Rendering::IImageRenderer
	{
	public:
		DX11Renderer();
		~DX11Renderer();

		virtual void InitializeFrame(int width, int height);
		virtual Bitmap^ EndFrame();
		virtual void Clear(Color color);
		virtual void DrawBackgroundTint(Color topColor, Color bottomColor);
		virtual void DrawImage(Image^ image, System::Drawing::Rectangle rect, ColorEffect colorEffect, float colorEffectRatio, int blurDistance, int featherDistance);
		virtual void DrawSolidRect(System::Drawing::Rectangle rect, Color color, int featherDistance);

	private:
		Bitmap^ _bitmap;
		DX11RendererPinnedData* _data;
	};
}
