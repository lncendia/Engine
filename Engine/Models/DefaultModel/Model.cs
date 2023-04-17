using Engine.Models.DefaultModel.DefaultShader;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Models.DefaultModel;

public class Model : IDisposable
{
    
    protected int VertexArray;
    protected float[]? Coordinates;
    protected uint[]? Indexes;
    protected readonly int CountElements;
    protected Shader Shader { get; }


    protected int DataBuffer;
    protected int? IndexesBuffer;

    protected Model(Dots coordinates, Shader shader)
    {
        Shader = shader;
        Coordinates = coordinates.Coordinates;
        Indexes = coordinates.Indexes;
        CountElements = Indexes?.Length ?? Coordinates.Length / 3;
    }

    protected bool IsBuffersSet;

    public virtual void Initialize()
    {
        if (IsBuffersSet) return;
        VertexArray = GL.GenVertexArray();
        GL.BindVertexArray(VertexArray);
        Shader.Use();
        DataBuffer = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, DataBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, Coordinates!.Length * sizeof(float), Coordinates,
            BufferUsageHint.StaticDraw);

        if (Indexes != null)
        {
            IndexesBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexesBuffer.Value);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CountElements * sizeof(uint), Indexes,
                BufferUsageHint.StaticDraw);
        }

        GL.VertexAttribPointer(Shader.Position, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(Shader.Position);

        Coordinates = null;
        Indexes = null;
        IsBuffersSet = true;
    }


    public virtual void Draw(Matrix4 modelMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        if (!IsBuffersSet) throw new Exception();
        Shader.SetMatrix4(Shader.MMatrix, modelMatrix);
        Shader.SetMatrix4(Shader.VMatrix, viewMatrix);
        Shader.SetMatrix4(Shader.PMatrix, projectionMatrix);
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


    protected bool Disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed) return;
        GL.DeleteBuffer(DataBuffer);
        if (IndexesBuffer.HasValue) GL.DeleteBuffer(IndexesBuffer.Value);
        GL.DeleteVertexArray(VertexArray);
        Disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Model() => Dispose(false);
}