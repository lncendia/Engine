using Engine.Scenes.Models.DefaultModel;
using Engine.Scenes.Models.TexturedModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.ModelsExamples.SkyBox;

public class SkyBoxModel : Model
{
    private static float[] Vertex => new[]
    {
        -1f, -1f, 1f,
        1f, -1f, 1f,
        1f, 1f, 1f,
        -1f, 1f, 1f,
        -1f, -1f, -1f,
        1f, -1f, -1f,
        1f, 1f, -1f,
        -1f, 1f, -1f
    };

    private new static uint[] Indexes => new uint[]
    {
        0, 1, 2,
        2, 3, 0,
        4, 5, 6,
        6, 7, 4,
        1, 5, 6,
        6, 2, 1,
        0, 4, 7,
        7, 3, 0,
        3, 2, 6,
        6, 7, 3,
        0, 1, 5,
        5, 4, 0
    };

    private new static Dots Coordinates => new(Vertex, Indexes);

    public new SkyBoxShader Shader => (SkyBoxShader) base.Shader;


    private int _textureId;
    private readonly List<Texture> _textures;

    public override void Initialize()
    {
        InitTextures();
        base.Initialize();
    }

    private void InitTextures()
    {
        var textureId = GL.GenTexture();
        GL.BindTexture(TextureTarget.TextureCubeMap, textureId);
        for (int i = 0; i < _textures.Count; i++)
        {
            GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba, _textures[i].Width,
                _textures[i].Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _textures[i].Data);
        }

        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter,
            (int) TextureMinFilter.LinearMipmapLinear);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter,
            (int) TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS,
            (int) TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT,
            (int) TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR,
            (int) TextureWrapMode.ClampToEdge);
        GL.GenerateMipmap(GenerateMipmapTarget.TextureCubeMap);
        _textureId = textureId;
    }

    public override void Draw(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        GL.DepthMask(false);
        if (!IsBuffersSet) throw new Exception();
        Shader.SetInt(Shader.SamplerName, 0);
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.TextureCubeMap, _textureId);
        base.Draw(new Matrix4(new Matrix3(viewMatrix)), projectionMatrix);
        GL.DepthMask(true);
    }

    public SkyBoxModel(Texture front, Texture back, Texture left, Texture right, Texture top, Texture bottom,
        SkyBoxShader shader) : base(Coordinates, shader)
    {
        _textures = new List<Texture> {right, left, top, bottom, front, back};
    }
}