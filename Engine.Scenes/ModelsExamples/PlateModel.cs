using Engine.Scenes.Models;
using Engine.Scenes.Shaders;

namespace Engine.Scenes.ModelsExamples;

public class PlateModel : LightedModel
{
    private new static float[] Coordinates => new[] { -2f, 0, 2f, -2f, 0, -2f, 2f, 0, 2f, 2f, 0, -2f };

    private new static uint[] Indexes => new uint[] { 0, 1, 2, 2, 3, 1, };

    private new static float[] Normals => new float[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 };

    public new LightedShader Shader => (LightedShader)base.Shader;
    public PlateModel(Material material, LightedShader shader) : base(new LightedDots(Coordinates, Normals, Indexes), material, shader)
    {
    }
}