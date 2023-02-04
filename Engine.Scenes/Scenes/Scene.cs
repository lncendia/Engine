using System.Drawing;
using Engine.Scenes.Models;
using Engine.Scenes.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Scenes;

public abstract class Scene : IDisposable
{
    protected readonly Camera Camera;
    protected readonly List<Model> Models = new();
    protected readonly List<Shader> Shaders = new();

    protected Scene(Camera camera)
    {
        Camera = camera;
        GL.ClearColor(Color.DarkCyan);
        GL.Enable(EnableCap.DepthTest);
    }

    private const float CameraSpeed = 2.5f;
    private const float Sensitivity = 0.1f;

    public virtual void MoveCamera(MoveDirect direct, float seconds)
    {
        Vector3 position;
        switch (direct)
        {
            case MoveDirect.Front:
                position = new Vector3(Camera.Front.X, 0, Camera.Front.Z);
                break;
            case MoveDirect.Back:
                position = -new Vector3(Camera.Front.X, 0, Camera.Front.Z);
                break;
            case MoveDirect.Left:
                position = -new Vector3(Camera.Right.X, 0, Camera.Right.Z);
                break;
            case MoveDirect.Right:
                position = new Vector3(Camera.Right.X, 0, Camera.Right.Z);
                break;
            case MoveDirect.Up:
                position = Vector3.UnitY;
                break;
            case MoveDirect.Down:
                position = -Vector3.UnitY;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direct), direct, null);
        }

        position.Normalize();
        Camera.Position += position * CameraSpeed * seconds;
    }

    private Vector2? _lastMousePosition;

    public virtual void RotateCamera(Vector2 mouse)
    {
        if (!_lastMousePosition.HasValue)
        {
            _lastMousePosition = mouse;
            return;
        }

        if (_lastMousePosition == mouse) return;
        var deltaX = mouse.X - _lastMousePosition.Value.X;
        var deltaY = mouse.Y - _lastMousePosition.Value.Y;
        _lastMousePosition = mouse;
        Camera.Yaw += deltaX * Sensitivity;
        Camera.Pitch -= deltaY * Sensitivity;
    }

    public virtual void ResizeScene(int width, int height)
    {
        GL.Viewport(0, 0, width, height);
        Camera.AspectRatio = width / (float)height;
    }


    public virtual void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        foreach (var shader in Shaders)
        {
            shader.SetMatrix4(shader.VMatrix, Camera.ViewMatrix);
            shader.SetMatrix4(shader.PMatrix, Camera.ProjectionMatrix);
        }
        Models.ForEach(x => x.Draw());
    }

    protected bool Disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed) return;
        Models.ForEach(x => x.Dispose());
        Shaders.ForEach(x => x.Dispose());
        Disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Scene() => Dispose(false);
}