namespace Engine.Scenes.Shaders;

public class LightedShaderLocations : ShaderLocations
{
    public int Normal { get; }
    public string NMatrix { get; }

    public string LightPosition { get; }
    public string CamPosition { get; }
    public string DiffuseLightColor { get; }
    public string SpecularLightColor { get; }
    public string M { get; }

    public LightedShaderLocations(int position, int normal, string mMatrix, string vMatrix, string pMatrix,
        string nMatrix, string lightPosition, string camPosition, string ambientLightColor, string diffuseLightColor,
        string specularLightColor, string m) : base(position, mMatrix, vMatrix, pMatrix, ambientLightColor)
    {
        Normal = normal;
        NMatrix = nMatrix;
        LightPosition = lightPosition;
        CamPosition = camPosition;
        DiffuseLightColor = diffuseLightColor;
        SpecularLightColor = specularLightColor;
        M = m;
    }
}