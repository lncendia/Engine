using Engine.Scenes.Models;
using Engine.Scenes.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.ModelsExamples;

public class SkyBoxModel : TexturedModel
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
        1f, -1f, 1f,
        1f, -1f, -1f,
        -1f, -1f, 1f,
        -1f, -1f, -1f
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
        0.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        1.0f, 0.0f,
        0.0f, 1.0f,
        0.0f, 0.0f,
        0.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f,
        0.0f, 0.0f,
        1.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f,
        0.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f
    };

    private static Material Material => new(new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), 1);

    private new static TexturedDotes Coordinates => new(Vertex, Normals, Indexes, TextureCoordinates);
    
    public new TexturedShader Shader => base.Shader;

    protected override void InitializeTextures()
    {
        var texture = Textures.First();
        var textureId = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, textureId);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture.Width, texture.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, texture.Data);
        TexturesIds.Add(textureId);
    }

    public SkyBoxModel(Texture front, Texture back, Texture left, Texture right, Texture top, Texture bottom,
        TexturedShader shader) : base(Coordinates, Material, new[] { front, back, left, right, top, bottom }, shader)
    {
    }
}