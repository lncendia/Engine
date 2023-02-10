using Engine.Scenes.Models.DefaultModel.DefaultShader;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models.DefaultModel;

public class Model : IDisposable
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
    protected Shader Shader { get; }

    private float _pitch;
    private float _yaw;
    private float _roll;

    public float Pitch
    {
        get => MathHelper.RadiansToDegrees(_pitch);
        set => _pitch = MathHelper.DegreesToRadians(value);
    }

    public float Yaw
    {
        get => MathHelper.RadiansToDegrees(_yaw);
        set => _yaw = MathHelper.DegreesToRadians(value);
    }

    public float Roll
    {
        get => MathHelper.RadiansToDegrees(_roll);
        set => _roll = MathHelper.DegreesToRadians(value);
    }

    protected Matrix4 ModelMatrix => Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(Position) *
                                     Matrix4.CreateRotationY(_yaw) * Matrix4.CreateRotationX(_pitch);


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


    public virtual void Draw(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        if (!IsBuffersSet) throw new Exception();
        Shader.SetMatrix4(Shader.MMatrix, ModelMatrix);
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