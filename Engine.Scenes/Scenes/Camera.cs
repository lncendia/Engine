using OpenTK.Mathematics;

namespace Engine.Scenes.Scenes;

public class Camera
{
    private float _pitch;
    private float _yaw = -MathHelper.PiOver2;
    private float _fov = MathHelper.PiOver2;

    public Camera(Vector3 position, float aspectRatio)
    {
        Position = position;
        AspectRatio = aspectRatio;
    }

    public Vector3 Position { get; set; }
    public float AspectRatio { get; set; }

    public Vector3 Front { get; private set; } = -Vector3.UnitZ;

    public Vector3 Up { get; private set; } = Vector3.UnitY;

    public Vector3 Right { get; private set; } = Vector3.UnitX;

    public float Pitch
    {
        get => MathHelper.RadiansToDegrees(_pitch);
        set
        {
            var angle = MathHelper.Clamp(value, -89f, 89f);
            _pitch = MathHelper.DegreesToRadians(angle);
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

    public float Fov
    {
        get => MathHelper.RadiansToDegrees(_fov);
        set
        {
            var angle = MathHelper.Clamp(value, 1f, 90f);
            _fov = MathHelper.DegreesToRadians(angle);
        }
    }

    public virtual Matrix4 ViewMatrix => Matrix4.LookAt(Position, Position + Front, Up);

    public virtual Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);

    protected virtual void UpdateVectors()
    {
        var x = MathF.Cos(_pitch) * MathF.Cos(_yaw);
        var y = MathF.Sin(_pitch);
        var z = MathF.Cos(_pitch) * MathF.Sin(_yaw);
        Front = Vector3.Normalize(new Vector3(x, y, z));
        Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
        Up = Vector3.Normalize(Vector3.Cross(Right, Front));
    }
}