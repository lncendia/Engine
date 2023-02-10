using Engine.Scenes.Models.DefaultModel.DefaultShader;

namespace Engine.Scenes.Models.ColoredModel.ColorShader;

public class ColorShaderLocations : ShaderLocations
{
    public string Color { get; }

    public ColorShaderLocations(int position, string mMatrix, string vMatrix, string pMatrix, string color) : base(
        position, mMatrix, vMatrix, pMatrix)
    {
        Color = color;
    }
}