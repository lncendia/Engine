using Engine.Scenes.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models;

public abstract class LightedModel : Model
{
    protected readonly Vector3 DiffuseLightColor;
    protected readonly Vector3 SpecularLightColor;
    protected readonly float M;
    protected float[]? Normals;

    private new LightedShader Shader => (LightedShader)base.Shader;

    protected LightedModel(LightedDots coordinates, Material material, LightedShader shader) : base(coordinates,
        material.AmbientLightColor, shader)
    {
        DiffuseLightColor = material.DiffuseLightColor;
        SpecularLightColor = material.SpecularLightColor;
        M = material.M;
        Normals = coordinates.Normals;
    }

    public override void Initialize()
    {
        if (IsBuffersSet) return;
        VertexArray = GL.GenVertexArray();
        GL.BindVertexArray(VertexArray);
        Shader.Use();
        DataBuffer = GL.GenBuffer();
        var buffer = new List<float>();
        for (var i = 0; i < CountElements; i += 3)
        {
            buffer.AddRange(Coordinates!.Skip(i).Take(3));
            buffer.AddRange(Normals!.Skip(i).Take(3));
        }

        GL.BindBuffer(BufferTarget.ArrayBuffer, DataBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, buffer.Count * sizeof(float), buffer.ToArray(),
            BufferUsageHint.StaticDraw);

        IndexesBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexesBuffer);
        GL.BufferData(BufferTarget.ElementArrayBuffer, CountElements * sizeof(uint), Indexes,
            BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(Shader.Position, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(Shader.Position);

        GL.VertexAttribPointer(Shader.Normal, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float),
            3 * sizeof(float));
        GL.EnableVertexAttribArray(Shader.Normal);

        Coordinates = null;
        Normals = null;
        Indexes = null;
        IsBuffersSet = true;
    }

    public override void Draw()
    {
        if (!IsBuffersSet) throw new Exception();
        var modelMatrix = Matrix4.CreateTranslation(Position) * Matrix4.CreateScale(Scale);
        var normalMatrix = new Matrix3(Matrix4.Transpose(modelMatrix.Inverted()));
        Shader.SetMatrix4(Shader.MMatrix, modelMatrix);
        Shader.SetMatrix3(Shader.NMatrix, normalMatrix);
        Shader.SetVector3(Shader.DiffuseLightColor, DiffuseLightColor);
        Shader.SetVector3(Shader.SpecularLightColor, SpecularLightColor);
        Shader.SetVector3(Shader.Color, Color);
        Shader.SetFloat(Shader.M, M);
        GL.BindVertexArray(VertexArray);
        GL.DrawElements(PrimitiveType.Triangles, CountElements, DrawElementsType.UnsignedInt, 0);
    }
}