using Engine.Models.DefaultModel.DefaultShader;

namespace Engine.Models.LightedModel.LightShader;

public class LightShaderLocations : ShaderLocations
{
    public int Normal { get; }
    public string NMatrix { get; }
    public string LightDirection { get; }
    public string CamPosition { get; }
    public string AmbientLightColor { get; }
    public string DiffuseLightColor { get; }
    public string SpecularLightColor { get; }
    public string M { get; }

    public LightShaderLocations(int position, int normal, string mMatrix, string vMatrix, string pMatrix,
        string nMatrix, string lightDirection, string camPosition, string ambientLightColor, string diffuseLightColor,
        string specularLightColor, string m) : base(position, mMatrix, vMatrix, pMatrix)
    {
        Normal = normal;
        NMatrix = nMatrix;
        LightDirection = lightDirection;
        CamPosition = camPosition;
        AmbientLightColor = ambientLightColor;
        DiffuseLightColor = diffuseLightColor;
        SpecularLightColor = specularLightColor;
        M = m;
    }
}