#include "pch.h"
#include "Renderer.h"
#include "Shader.h"

namespace WallSwitch::DX11
{
	Shader::Shader(const ptr<Renderer>& renderer)
		: _renderer(renderer)
	{
		_rendererVersion = renderer->GetVersion();
	}

	Shader::~Shader()
	{
		Release();
	}

	void Shader::Create(const char* srcFile, const char* vertexEntryPoint, const char* pixelEntryPoint, int shaderFlags, int constantBufferSize)
	{
		if (srcFile == nullptr) throw gcnew ArgumentNullException("src");
		if (vertexEntryPoint == nullptr) throw gcnew ArgumentNullException("vertexEntryPoint");
		if (pixelEntryPoint == nullptr) throw gcnew ArgumentNullException("pixelEntryPoint");

		auto fileName = String::Concat(Path::GetDirectoryName(System::Reflection::Assembly::GetExecutingAssembly()->Location), Path::DirectorySeparatorChar, gcnew String(srcFile));
		auto fileContent = File::ReadAllBytes(fileName);

		pin_ptr<Byte> filePtr = &fileContent[0];
		_src.resize(fileContent->Length);
		memcpy(_src.data(), filePtr, _src.size());

		_vertexEntryPoint = vertexEntryPoint;
		_pixelEntryPoint = pixelEntryPoint;
		_flags = shaderFlags;
		_constantBufferSize = constantBufferSize;

		Compile();
	}

	void Shader::Release()
	{
		if (_vsCode) { _vsCode->Release(); _vsCode = nullptr; }
		if (_psCode) { _psCode->Release(); _psCode = nullptr; }
		if (_vsShader) { _vsShader->Release(); _vsShader = nullptr; }
		if (_psShader) { _psShader->Release(); _psShader = nullptr; }
		if (_vertexLayout) { _vertexLayout->Release(); _vertexLayout = nullptr; }
		if (_constantBuffer) { _constantBuffer->Release(); _constantBuffer = nullptr; }
	}

	void Shader::Compile()
	{
		HRESULT hr;
		ID3DBlob* errors = nullptr;
		ID3DBlob* code = nullptr;
		if (FAILED(hr = D3DCompile(_src.data(), _src.size(), nullptr, nullptr, nullptr, _vertexEntryPoint.c_str(), "vs_4_0", 0, 0, &code, &errors)))
		{
			if (errors != nullptr)
			{
				Log::Error(gcnew String(reinterpret_cast<const char*>(errors->GetBufferPointer())));
				errors->Release();
			}
			if (code) { code->Release(); code = nullptr; }
			throw gcnew DirectXRunTimeException(hr, L"Failed to compile vertex shader.");
		}
		_vsCode = code;

		if (FAILED(hr = D3DCompile(_src.data(), _src.size(), nullptr, nullptr, nullptr, _pixelEntryPoint.c_str(), "ps_4_0", 0, 0, &code, &errors)))
		{
			if (errors != nullptr)
			{
				Log::Error(gcnew String(reinterpret_cast<const char*>(errors->GetBufferPointer())));
				errors->Release();
			}
			if (code) { code->Release(); code = nullptr; }
			throw gcnew DirectXRunTimeException(hr, L"Failed to compile pixel shader.");
		}
		_psCode = code;

		auto r = _renderer.lock();
		if (FAILED(r->GetDevice()->CreateVertexShader(_vsCode->GetBufferPointer(), _vsCode->GetBufferSize(), nullptr, &_vsShader)))
		{
			throw gcnew DirectXRunTimeException(hr, L"Failed to create vertex shader.");
		}
		if (!_vsShader) throw gcnew DirectXRunTimeException("Compiled vertex shader is null.");

		if (FAILED(hr = r->GetDevice()->CreatePixelShader(_psCode->GetBufferPointer(), _psCode->GetBufferSize(), nullptr, &_psShader)))
		{
			throw gcnew DirectXRunTimeException(hr, L"Failed to create fragment shader.");
		}
		if (!_psShader) throw gcnew DirectXRunTimeException("Compiled pixel shader is null.");

		D3D11_INPUT_ELEMENT_DESC ied[3];
		int iedCount = 0;
		ied[iedCount].SemanticName = "POSITION";
		ied[iedCount].SemanticIndex = 0;
		ied[iedCount].Format = DXGI_FORMAT_R32G32_FLOAT;
		ied[iedCount].InputSlot = 0;
		ied[iedCount].AlignedByteOffset = 0;
		ied[iedCount].InputSlotClass = D3D11_INPUT_PER_VERTEX_DATA;
		ied[iedCount].InstanceDataStepRate = 0;
		iedCount++;

		if (_flags & SHADER_FLAG_TEXTURE)
		{
			ied[iedCount].SemanticName = "TEXCOORD";
			ied[iedCount].SemanticIndex = 0;
			ied[iedCount].Format = DXGI_FORMAT_R32G32_FLOAT;
			ied[iedCount].InputSlot = 0;
			ied[iedCount].AlignedByteOffset = D3D11_APPEND_ALIGNED_ELEMENT;
			ied[iedCount].InputSlotClass = D3D11_INPUT_PER_VERTEX_DATA;
			ied[iedCount].InstanceDataStepRate = 0;
			iedCount++;
		}

		ied[iedCount].SemanticName = "COLOR";
		ied[iedCount].SemanticIndex = 0;
		ied[iedCount].Format = DXGI_FORMAT_R32G32B32A32_FLOAT;
		ied[iedCount].InputSlot = 0;
		ied[iedCount].AlignedByteOffset = D3D11_APPEND_ALIGNED_ELEMENT;
		ied[iedCount].InputSlotClass = D3D11_INPUT_PER_VERTEX_DATA;
		ied[iedCount].InstanceDataStepRate = 0;
		iedCount++;

		if (FAILED(hr = r->GetDevice()->CreateInputLayout(ied, iedCount, _vsCode->GetBufferPointer(), _vsCode->GetBufferSize(), &_vertexLayout)))
		{
			throw gcnew DirectXRunTimeException(hr, L"Failed to create vertex layout.");
		}

		if (_constantBufferSize > 0)
		{
			D3D11_BUFFER_DESC cbd = { };
			cbd.ByteWidth = _constantBufferSize;
			cbd.Usage = D3D11_USAGE_DYNAMIC;
			cbd.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
			cbd.CPUAccessFlags = D3D11_CPU_ACCESS_WRITE;
			cbd.MiscFlags = 0;
			cbd.StructureByteStride = 0;

			if (FAILED(hr = r->GetDevice()->CreateBuffer(&cbd, nullptr, &_constantBuffer)))
				throw gcnew DirectXRunTimeException(hr, "Failed to create constant buffer.");
		}

		_rendererVersion = r->GetVersion();
	}

	void Shader::Use()
	{
		auto r = _renderer.lock();
		if (_rendererVersion != r->GetVersion() || _vsShader == nullptr || _psShader == nullptr)
		{
			Compile();
		}

		auto dc = r->GetContext();
		HRESULT hr;

		if (_constantBufferSize > 0)
		{
			D3D11_MAPPED_SUBRESOURCE constMap;
			if (FAILED(hr = dc->Map(_constantBuffer, 0, D3D11_MAP_WRITE_DISCARD, 0, &constMap)))
				throw gcnew DirectXRunTimeException(hr, "Failed to map constant buffer.");
			UpdateConstantBuffer(constMap.pData);
			dc->Unmap(_constantBuffer, 0);
			if (_flags & SHADER_FLAG_VS_CONSTANT_BUFFER) dc->VSSetConstantBuffers(0, 1, &_constantBuffer);
			if (_flags & SHADER_FLAG_PS_CONSTANT_BUFFER) dc->PSSetConstantBuffers(0, 1, &_constantBuffer);
		}

#ifdef _DEBUG
		if (_vsShader == nullptr) throw gcnew DirectXRunTimeException("Vertex shader is null.");
		if (_psShader == nullptr) throw gcnew DirectXRunTimeException("Pixel shader is null.");
#endif

		dc->VSSetShader(_vsShader, nullptr, 0);
		dc->PSSetShader(_psShader, nullptr, 0);
		dc->IASetInputLayout(_vertexLayout);

		UseInner();
	}
}
