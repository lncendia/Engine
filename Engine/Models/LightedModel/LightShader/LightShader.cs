using Engine.Models.DefaultModel.DefaultShader;

namespace Engine.Models.LightedModel.LightShader;

public class LightShader : Shader
{
    public int Normal { get; }
    public string NMatrix { get; }
    public string LightDirection { get; }
    public string CamPosition { get; }
    public string AmbientLightColor { get; }
    public string DiffuseLightColor { get; }
    public string SpecularLightColor { get; }
    public string M { get; }

    public LightShader(string vertexSource, string fragmentSource, LightShaderLocations locations) : base(
        vertexSource, fragmentSource, locations)
    {
        Normal = locations.Normal;
        NMatrix = locations.NMatrix;
        LightDirection = locations.LightDirection;
        CamPosition = locations.CamPosition;
        AmbientLightColor = locations.AmbientLightColor;
        DiffuseLightColor = locations.DiffuseLightColor;
        SpecularLightColor = locations.SpecularLightColor;
        M = locations.M;
    }
}