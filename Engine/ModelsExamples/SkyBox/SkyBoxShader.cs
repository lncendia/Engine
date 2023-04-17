using Engine.Models.DefaultModel.DefaultShader;

namespace Engine.ModelsExamples.SkyBox;

public class SkyBoxShader : Shader
{
    public string SamplerName { get; }

    public SkyBoxShader(string vertexSource, string fragmentSource, SkyBoxShaderLocations locations) : base(
        vertexSource, fragmentSource, locations)
    {
        SamplerName = locations.SamplerName;
    }
}