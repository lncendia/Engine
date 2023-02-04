using Engine.Scenes.Models;
using Engine.Scenes.Shaders;
using OpenTK.Mathematics;

namespace Engine.Scenes.ModelsExamples;

public class LightModel : Model
{
    private static float[] Vertex => new[]
    {
        -0.1f, -0.1f, 0.1f,
        -0.1f, 0.1f, 0.1f,
        0.1f, 0.1f, 0.1f,
        0.1f, -0.1f, 0.1f,
        -0.1f, -0.1f, -0.1f,
        -0.1f, 0.1f, -0.1f,
        0.1f, 0.1f, -0.1f,
        0.1f, -0.1f, -0.1f,
    };

    private new static uint[] Indexes => new uint[]
    {
        0, 1, 2,
        2, 3, 0,

        0, 4, 5,
        5, 1, 0,

        4, 5, 6,
        6, 7, 4,

        2, 3, 7,
        7, 6, 2,

        1, 5, 6,
        6, 2, 1,

        0, 3, 7,
        7, 4, 0
    };

    private new static Dots Coordinates => new(Vertex, Indexes);

    public new Shader Shader => base.Shader;

    public LightModel(Shader shader) : base(Coordinates, Vector3.One, shader)
    {
    }
}