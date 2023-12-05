#include "pch.h"
#include "DX11Renderer.h"
#include "Renderer.h"
#include "Shaders.h"
#include "SolidColorShader.h"
#include "Texture.h"
#include "Util.h"

using namespace System::Drawing;

namespace WallSwitch::DX11
{
	DX11Renderer::DX11Renderer()
	{
		_data = new DX11RendererPinnedData();
		_data->renderer = std::make_shared<Renderer>();
	}

	DX11Renderer::~DX11Renderer()
	{
		delete _data;
	}

	void DX11Renderer::InitializeFrame(int width, int height)
	{
		Log::Debug("DirectX 11 renderer starting up...");

		_data->renderer->InitializeFrame(width, height);
	}

	Bitmap^ DX11Renderer::EndFrame()
	{
		int width = _data->renderer->GetWidth();
		int height = _data->renderer->GetHeight();
		if (_bitmap == nullptr || _bitmap->Width != width || _bitmap->Height != height)
		{
			_bitmap = gcnew Bitmap(width, height, PixelFormat::Format32bppRgb);
		}

		auto bd = _bitmap->LockBits(System::Drawing::Rectangle(0, 0, width, height), ImageLockMode::WriteOnly, PixelFormat::Format32bppRgb);
		try
		{
			_data->renderer->EndFrame(bd->Scan0.ToPointer(), bd->Stride);
			return _bitmap;
		}
		catch (System::Exception^ ex)
		{
			Log::Error(ex, L"Exception when copying image data.");
		}
		catch (...)
		{
			Log::Error(L"Unknown exception when copying image data.");
		}
		finally
		{
			_bitmap->UnlockBits(bd);
		}

		_bitmap->UnlockBits(bd);
		return _bitmap;
	}

	void DX11Renderer::Clear(System::Drawing::Color color)
	{
		_data->renderer->Clear(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
	}

	void DX11Renderer::DrawBackgroundTint(Color topColor, Color bottomColor)
	{
		float screenWidth = float(_data->renderer->GetWidth());
		float screenHeight = float(_data->renderer->GetHeight());

		vec4 vtopColor(topColor.R / 255.0f, topColor.G / 255.0f, topColor.B / 255.0f, topColor.A / 255.0f);
		vec4 vbottomColor(bottomColor.R / 255.0f, bottomColor.G / 255.0f, bottomColor.B / 255.0f, bottomColor.A / 255.0f);

		SolidColorVertex verts[] =
		{
			{ vec2(0.0f, screenHeight), vbottomColor },
			{ vec2(0, 0), vtopColor },
			{ vec2(screenWidth, 0.0f), vtopColor },

			{ vec2(0.0f, screenHeight), vbottomColor },
			{ vec2(screenWidth, 0.0f), vtopColor },
			{ vec2(screenWidth, screenHeight), vbottomColor }
		};

		auto shader = _data->renderer->GetSolidColorShader();
		shader->SetFeather(vec2(), vec2(), 0.0f);

		auto vb = _data->renderer->CreateVertexBuffer(verts, 6);

		_data->renderer->DrawTriangles(shader, vb, vb->GetCount(), 0);
	}

	void DX11Renderer::DrawImage(Image^ image, System::Drawing::Rectangle rect, ColorEffect colorEffect, float colorEffectRatio, int blurDistance, int featherDistance)
	{
		int texWidth = RoundUpToPowerOfTwo(std::min(image->Width, rect.Width));
		int texHeight = RoundUpToPowerOfTwo(std::min(image->Height, rect.Height));
		if (texWidth > D3D11_REQ_TEXTURE2D_U_OR_V_DIMENSION) texWidth = D3D11_REQ_TEXTURE2D_U_OR_V_DIMENSION;
		if (texHeight > D3D11_REQ_TEXTURE2D_U_OR_V_DIMENSION) texHeight = D3D11_REQ_TEXTURE2D_U_OR_V_DIMENSION;

		Bitmap^ bmp;
		if (image->GetType() == Bitmap::typeid && image->Width == texWidth && image->Height == texHeight)
		{
			bmp = (Bitmap^)image;
		}
		else
		{
			bmp = gcnew Bitmap(image, texWidth, texHeight);
		}

		ptr<Texture> texture = nullptr;
		std::vector<unsigned char> bmpData;
		int bmpStride;
		auto bd = bmp->LockBits(System::Drawing::Rectangle(0, 0, bmp->Width, bmp->Height), ImageLockMode::ReadOnly, PixelFormat::Format32bppRgb);
		try
		{
			bmpStride = bd->Stride;
			bmpData.resize(bd->Height * bd->Stride);
			memcpy(bmpData.data(), bd->Scan0.ToPointer(), bmpData.size());
		}
		catch (System::Exception^ ex)
		{
			Log::Error(ex, L"Exception when copying image data.");
			return;
		}
		catch (...)
		{
			Log::Error(L"Unknown exception when copying image data.");
			return;
		}
		bmp->UnlockBits(bd);

		texture = std::make_shared<Texture>(_data->renderer, bmpData.data(), texWidth, texHeight, bmpStride);

		ImageVertex verts[] =
		{
			{ vec2(rect.Left, rect.Bottom), vec2(0, 1), vec4(1, 1, 1, 1) },
			{ vec2(rect.Left, rect.Top), vec2(0, 0), vec4(1, 1, 1, 1) },
			{ vec2(rect.Right, rect.Top), vec2(1, 0), vec4(1, 1, 1, 1) },

			{ vec2(rect.Left, rect.Bottom), vec2(0, 1), vec4(1, 1, 1, 1) },
			{ vec2(rect.Right, rect.Top), vec2(1, 0), vec4(1, 1, 1, 1) },
			{ vec2(rect.Right, rect.Bottom), vec2(1, 1), vec4(1, 1, 1, 1) }
		};

		auto shader = _data->renderer->GetImageShader();

		if (featherDistance > rect.Width / 2) featherDistance = rect.Width / 2;
		if (featherDistance > rect.Height / 2) featherDistance = rect.Height / 2;

		auto colorMatrixElements = ColorEffectEx::GetColorMatrixElements(colorEffect, 1.0f);
		int i = 0;
		float colorMatrix[16];
		for (int y = 0; y < 4; y++)
		{
			for (int x = 0; x < 4; x++)
			{
				colorMatrix[i++] = colorMatrixElements[x][y];
			}
		}

		vec2 texScale(1.0f / float(rect.Width), 1.0f / float(rect.Height));

		shader->SetState(texture,
			vec2(rect.Left + featherDistance, rect.Top + featherDistance),
			vec2(rect.Right - featherDistance, rect.Bottom - featherDistance), float(featherDistance),
			colorMatrix, colorEffectRatio,
			float(blurDistance), texScale);

		auto vb = _data->renderer->CreateVertexBuffer(verts, 6);

		_data->renderer->DrawTriangles(shader, vb, vb->GetCount(), 0);
	}

	void DX11Renderer::DrawSolidRect(System::Drawing::Rectangle rect, Color color, int featherDistance)
	{
		vec4 vcolor(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);

		SolidColorVertex verts[] =
		{
			{ vec2(rect.Left, rect.Bottom), vcolor },
			{ vec2(rect.Left, rect.Top), vcolor },
			{ vec2(rect.Right, rect.Top), vcolor },

			{ vec2(rect.Left, rect.Bottom), vcolor },
			{ vec2(rect.Right, rect.Top), vcolor },
			{ vec2(rect.Right, rect.Bottom), vcolor }
		};

		auto shader = _data->renderer->GetSolidColorShader();

		if (featherDistance > rect.Width / 2) featherDistance = rect.Width / 2;
		if (featherDistance > rect.Height / 2) featherDistance = rect.Height / 2;
		shader->SetFeather(vec2(rect.Left + featherDistance, rect.Top + featherDistance),
			vec2(rect.Right - featherDistance, rect.Bottom - featherDistance), float(featherDistance));

		auto vb = _data->renderer->CreateVertexBuffer(verts, 6);

		_data->renderer->DrawTriangles(shader, vb, vb->GetCount(), 0);
	}
}
