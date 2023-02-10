using Engine.Scenes.Models.LightedModel;
using Engine.Scenes.Models.TexturedModel;
using Engine.Scenes.Models.TexturedModel.TextureShader;

namespace Engine.Scenes.ModelsExamples;

public class BoxTexturedModel : TexturedModel
{
    private static float[] Vertex => new[]
    {
        -1f, -1f, 1f,
        -1f, 1f, 1f,
        1f, 1f, 1f,
        1f, -1f, 1f,
        -1f, -1f, -1f,
        -1f, 1f, -1f,
        1f, 1f, -1f,
        1f, -1f, -1f,
        -1f, -1f, 1f,
        -1f, 1f, 1f,
        -1f, 1f, -1f,
        -1f, -1f, -1f,
        1f, -1f, 1f,
        1f, 1f, 1f,
        1f, 1f, -1f,
        1f, -1f, -1f,
        1f, 1f, 1f,
        1f, 1f, -1f,
        -1f, 1f, 1f,
        -1f, 1f, -1f,
        1f, -1f, 1f, //5 10
        1f, -1f, -1f, //6 00
        -1f, -1f, 1f, //7 11
        -1f, -1f, -1f //8 01
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
        18, 19, 17,
        20, 21, 22,
        22, 23, 21
    };

    private new static float[] Normals => new[]
    {
        0.0f, 0.0f, 1.0f,
        0.0f, 0.0f, 1.0f,
        0.0f, 0.0f, 1.0f,
        0.0f, 0.0f, 1.0f,
        0.0f, 0.0f, -1.0f,
        0.0f, 0.0f, -1.0f,
        0.0f, 0.0f, -1.0f,
        0.0f, 0.0f, -1.0f,
        -1.0f, 0.0f, 0.0f,
        -1.0f, 0.0f, 0.0f,
        -1.0f, 0.0f, 0.0f,
        -1.0f, 0.0f, 0.0f,
        1.0f, 0.0f, 0.0f,
        1.0f, 0.0f, 0.0f,
        1.0f, 0.0f, 0.0f,
        1.0f, 0.0f, 0.0f,
        0.0f, 1.0f, 0.0f,
        0.0f, 1.0f, 0.0f,
        0.0f, 1.0f, 0.0f,
        0.0f, 1.0f, 0.0f,
        0.0f, -1.0f, 0.0f,
        0.0f, -1.0f, 0.0f,
        0.0f, -1.0f, 0.0f,
        0.0f, -1.0f, 0.0f,
    };

    private new static float[] TextureCoordinates => new[]
    {
        0.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,
        0.0f, 0.0f,
        0.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,
        0.0f, 0.0f,

        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 0.0f,
        0.0f, 1.0f,

        1.0f, 1.0f,
        1.0f, 0.0f,
        0.0f, 1.0f,
        0.0f, 0.0f,
    };

    private new static TexturedDotes Coordinates => new(Vertex, Normals, Indexes, TextureCoordinates);

    public BoxTexturedModel(Material material, Texture texture,
        TextureShader shader) : base(Coordinates, material, texture, shader)
    {
    }
}