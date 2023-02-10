using Engine.Scenes.Models.LightedModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models.TexturedModel;

public class TexturedModel : LightedModel.LightedModel
{
    protected readonly Texture Texture;

    protected int TextureId;

    protected float[]? TextureCoordinates;

    protected new TextureShader.TextureShader Shader => (TextureShader.TextureShader)base.Shader;

    protected TexturedModel(TexturedDotes coordinates, Material material, Texture textures,
        TextureShader.TextureShader shader) : base(coordinates, material, shader)
    {
        Texture = textures;
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

    private void InitializeTextures()
    {
        var textureId = GL.GenTexture();
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, textureId);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Texture.Width, Texture.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, Texture.Data);
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        TextureId = textureId;
    }
    
    public override void Draw(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        if (!IsBuffersSet) throw new Exception();
        
        
        Shader.SetInt(Shader.SamplerName, 0);
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, TextureId);
        
        var modelMatrix = ModelMatrix;
        var normalMatrix = new Matrix3(Matrix4.Transpose(modelMatrix.Inverted()));
        Shader.SetMatrix4(Shader.VMatrix, viewMatrix);
        Shader.SetMatrix4(Shader.PMatrix, projectionMatrix);
        Shader.SetMatrix4(Shader.MMatrix, modelMatrix);
        Shader.SetMatrix3(Shader.NMatrix, normalMatrix);
        Shader.SetVector3(Shader.AmbientLightColor, AmbientLightColor);
        Shader.SetVector3(Shader.DiffuseLightColor, DiffuseLightColor);
        Shader.SetVector3(Shader.SpecularLightColor, SpecularLightColor);
        Shader.SetFloat(Shader.M, M);
        GL.BindVertexArray(VertexArray);
        if (IndexesBuffer.HasValue)
        {
            GL.DrawElements(PrimitiveType.Triangles, CountElements, DrawElementsType.UnsignedInt, 0);
        }
        else
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, CountElements);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        GL.DeleteTexture(TextureId);
        base.Dispose(disposing);
        Disposed = true;
    }
}