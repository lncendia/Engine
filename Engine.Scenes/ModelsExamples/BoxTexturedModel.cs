using Engine.Scenes.Models;
using Engine.Scenes.Shaders;
using OpenTK.Graphics.OpenGL;

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

    private new static TexturedDotes Coordinates => new(Vertex, Normals, Indexes, TextureCoordinates);


    protected override void InitializeTextures()
    {
        var texture = Textures.First();
        var textureId = GL.GenTexture();

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, textureId);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture.Width, texture.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, texture.Data);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture.Width, texture.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, texture.Data);
        TexturesIds.Add(textureId);
    }

    public override void Draw()
    {
        if (!IsBuffersSet) throw new Exception();
        Shader.Use();
        Shader.SetInt(Shader.Sampler2DName, 0);
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, TexturesIds.First());
        base.Draw();
    }

    public BoxTexturedModel(Material material, Texture texture,
        TexturedShader shader) : base(Coordinates, material, new[] { texture }, shader)
    {
    }
}