using Engine.Models.LightedModel;
using Engine.Models.LightedModel.LightShader;

namespace Engine.ModelsExamples;

public class PlateModel : LightedModel
{
    private new static float[] Coordinates => new[] { -2f, 0, 2f, -2f, 0, -2f, 2f, 0, 2f, 2f, 0, -2f };

    private new static uint[] Indexes => new uint[] { 0, 1, 2, 2, 3, 1, };

    private new static float[] Normals => new float[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 };

    public new LightShader Shader => base.Shader;
    public PlateModel(Material material, LightShader shader) : base(new LightedDots(Coordinates, Normals, Indexes), material, shader)
    {
    }
}