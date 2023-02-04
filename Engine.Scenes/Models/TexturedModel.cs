using Engine.Scenes.Shaders;
using OpenTK.Graphics.OpenGL;

namespace Engine.Scenes.Models;

public abstract class TexturedModel : LightedModel
{
    protected readonly List<Texture> Textures = new();

    protected readonly List<int> TexturesIds = new();

    protected float[]? TextureCoordinates;

    protected new TexturedShader Shader => (TexturedShader)base.Shader;

    protected TexturedModel(TexturedDotes coordinates, Material material, IEnumerable<Texture> textures,
        TexturedShader shader) : base(coordinates, material, shader)
    {
        Textures.AddRange(textures);
        TextureCoordinates = coordinates.TextureCoordinates;
    }

    public override void Initialize()
    {
        if (IsBuffersSet) return;
        VertexArray = GL.GenVertexArray();
        GL.BindVertexArray(VertexArray);
        DataBuffer = GL.GenBuffer();
        var buffer = new List<float>();
        for (var i = 0; i < Coordinates!.Length / 3; i++)
        {
            buffer.AddRange(Coordinates!.Skip(i * 3).Take(3));
            buffer.AddRange(Normals!.Skip(i * 3).Take(3));
            buffer.AddRange(TextureCoordinates!.Skip(i * 2).Take(2));
        }

        GL.BindBuffer(BufferTarget.ArrayBuffer, DataBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, buffer.Count * sizeof(float), buffer.ToArray(),
            BufferUsageHint.StaticDraw);

        if (Indexes != null)
        {
            IndexesBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexesBuffer.Value);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CountElements * sizeof(uint), Indexes,
                BufferUsageHint.StaticDraw);
        }

        GL.VertexAttribPointer(Shader.Position, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
        GL.EnableVertexAttribArray(Shader.Position);

        GL.VertexAttribPointer(Shader.Normal, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float),
            3 * sizeof(float));
        GL.EnableVertexAttribArray(Shader.Normal);

        GL.VertexAttribPointer(Shader.Texture, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float),
            6 * sizeof(float));
        GL.EnableVertexAttribArray(Shader.Texture);

        InitializeTextures();
        Coordinates = null;
        Normals = null;
        Indexes = null;
        TextureCoordinates = null;
        IsBuffersSet = true;
    }

    protected abstract void InitializeTextures();

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        GL.DeleteTextures(TexturesIds.Count, TexturesIds.ToArray());
        base.Dispose(disposing);
        Disposed = true;
    }
}