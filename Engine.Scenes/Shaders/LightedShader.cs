using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Shaders;

public abstract class LightedShader : Shader
{
    public int Normal { get; }
    public string NMatrix { get; }
    public string LightPosition { get; }
    public string CamPosition { get; }
    public string DiffuseLightColor { get; }
    public string SpecularLightColor { get; }
    public string M { get; }

    protected LightedShader(string vertexSource, string fragmentSource, LightedShaderLocations locations) : base(
        vertexSource, fragmentSource, locations)
    {
        Normal = locations.Normal;
        NMatrix = locations.NMatrix;
        LightPosition = locations.LightPosition;
        CamPosition = locations.CamPosition;
        DiffuseLightColor = locations.DiffuseLightColor;
        SpecularLightColor = locations.SpecularLightColor;
        M = locations.M;
    }
}