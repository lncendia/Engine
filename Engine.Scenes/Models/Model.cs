using Engine.Scenes.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models;

public abstract class Model : IDisposable
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; } = Vector3.One;
    public Vector3 Front { get; private set; } = -Vector3.UnitZ;

    public Vector3 Up { get; private set; } = Vector3.UnitY;

    public Vector3 Right { get; private set; } = Vector3.UnitX;


    protected int VertexArray;
    protected float[]? Coordinates;
    protected uint[]? Indexes;
    protected readonly int CountElements;
    protected readonly Shader Shader;

    public Vector3 Color { get; }


    private float _pitch;
    private float _yaw = -MathHelper.PiOver2;

    public float Pitch
    {
        get => MathHelper.RadiansToDegrees(_pitch);
        set
        {
            _pitch = MathHelper.DegreesToRadians(value);
            UpdateVectors();
        }
    }

    public float Yaw
    {
        get => MathHelper.RadiansToDegrees(_yaw);
        set
        {
            _yaw = MathHelper.DegreesToRadians(value);
            UpdateVectors();
        }
    }

    private void UpdateVectors()
    {
        var x = MathF.Cos(_pitch) * MathF.Cos(_yaw);
        var y = MathF.Sin(_pitch);
        var z = MathF.Cos(_pitch) * MathF.Sin(_yaw);
        Front = Vector3.Normalize(new Vector3(x, y, z));
        Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
        Up = Vector3.Normalize(Vector3.Cross(Right, Front));
    }


    protected int DataBuffer;
    protected int? IndexesBuffer;

    protected Model(Dots coordinates, Vector3 color, Shader shader)
    {
        Color = color;
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


    public virtual void Draw()
    {
        if (!IsBuffersSet) throw new Exception();
        var modelMatrix = Matrix4.CreateTranslation(Position) * Matrix4.CreateScale(Scale);
        Shader.SetMatrix4(Shader.MMatrix, modelMatrix);
        Shader.SetVector3(Shader.Color, Color);
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