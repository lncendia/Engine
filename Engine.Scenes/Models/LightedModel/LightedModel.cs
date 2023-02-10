using Engine.Scenes.Models.DefaultModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models.LightedModel;

public class LightedModel : Model
{
    protected readonly Vector3 AmbientLightColor;
    protected readonly Vector3 DiffuseLightColor;
    protected readonly Vector3 SpecularLightColor;
    protected readonly float M;
    protected float[]? Normals;

    protected new LightShader.LightShader Shader => (LightShader.LightShader) base.Shader;

    public LightedModel(LightedDots coordinates, Material material, LightShader.LightShader shader) : base(coordinates, shader)
    {
        AmbientLightColor = material.AmbientLightColor;
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
        DataBuffer = GL.GenBuffer();
        var buffer = new List<float>();
        for (var i = 0; i < Coordinates!.Length; i += 3)
        {
            buffer.AddRange(Coordinates!.Skip(i).Take(3));
            buffer.AddRange(Normals!.Skip(i).Take(3));
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

    public override void Draw(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        if (!IsBuffersSet) throw new Exception();
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
}