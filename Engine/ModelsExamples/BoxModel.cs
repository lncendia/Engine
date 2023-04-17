using Engine.Models.DefaultModel.DefaultShader;
using Engine.Models.LightedModel;
using Engine.Models.LightedModel.LightShader;

namespace Engine.ModelsExamples;

public class BoxModel : LightedModel
{
    private static float[] Vertex => new[]
    {
        -0.5f, -0.5f, 0.5f, //1   0
        -0.5f, 0.5f, 0.5f, //2    1
        0.5f, 0.5f, 0.5f, //3     2
        0.5f, -0.5f, 0.5f, //4    3

        0.5f, 0.5f, 0.5f, //3     4
        0.5f, -0.5f, 0.5f, //4    5
        0.5f, -0.5f, -0.5f, //8   6
        0.5f, 0.5f, -0.5f, //7    7

        -0.5f, -0.5f, 0.5f, //1   8
        -0.5f, 0.5f, 0.5f, //2    9
        -0.5f, 0.5f, -0.5f, //5  10
        -0.5f, -0.5f, -0.5f, //6   11

        -0.5f, 0.5f, 0.5f, //2    12
        0.5f, 0.5f, 0.5f, //3     13
        0.5f, 0.5f, -0.5f, //6   14
        -0.5f, 0.5f, -0.5f, //7    15

        -0.5f, -0.5f, -0.5f, //5  16
        -0.5f, 0.5f, -0.5f, //6   17
        0.5f, 0.5f, -0.5f, //7    18
        0.5f, -0.5f, -0.5f, //8   19

        -0.5f, -0.5f, 0.5f, //1   20
        0.5f, -0.5f, 0.5f, //4    21
        0.5f, -0.5f, -0.5f, //5  22
        -0.5f, -0.5f, -0.5f, //8   23
    };

    private new static uint[] Indexes => new uint[]
    {
        0, 1, 2,
        2, 3, 0,

        4, 5, 6,
        6, 7, 4,

        8, 9, 10,
        10, 11, 8,

        12, 13, 14,
        14, 15, 12,

        16, 17, 18,
        18, 19, 16,

        20, 21, 22,
        22, 23, 20,
    };

    private new static float[] Normals => new float[]
    {
        0, 0, 1,
        0, 0, 1,
        0, 0, 1,
        0, 0, 1,

        1, 0, 0,
        1, 0, 0,
        1, 0, 0,
        1, 0, 0,

        -1, 0, 0,
        -1, 0, 0,
        -1, 0, 0,
        -1, 0, 0,

        0, 1, 0,
        0, 1, 0,
        0, 1, 0,
        0, 1, 0,

        0, 0, -1,
        0, 0, -1,
        0, 0, -1,
        0, 0, -1,

        0, -1, 0,
        0, -1, 0,
        0, -1, 0,
        0, -1, 0
    };

    private new static LightedDots Coordinates => new(Vertex, Normals, Indexes);

    public new Shader Shader => base.Shader;

    public BoxModel(Material material, LightShader shader) : base(Coordinates, material, shader)
    {
    }
}