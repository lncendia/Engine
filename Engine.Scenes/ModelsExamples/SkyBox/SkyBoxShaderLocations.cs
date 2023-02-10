using Engine.Scenes.Models.DefaultModel.DefaultShader;

namespace Engine.Scenes.ModelsExamples.SkyBox;

public class SkyBoxShaderLocations : ShaderLocations
{
    public string SamplerName { get; }

    public SkyBoxShaderLocations(int position, string mMatrix, string vMatrix, string pMatrix,
        string samplerName) : base(position, mMatrix, vMatrix, pMatrix)
    {
        SamplerName = samplerName;
    }
}