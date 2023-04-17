using Engine.Models.DefaultModel.DefaultShader;

namespace Engine.Models.ColoredModel.ColorShader;

public class ColorShader : Shader
{
    public string Color { get; }

    public ColorShader(string vertexSource, string fragmentSource, ColorShaderLocations locations) : base(
        vertexSource, fragmentSource, locations)
    {
        Color = locations.Color;
    }
}