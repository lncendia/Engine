using Engine.Models.DefaultModel;
using OpenTK.Mathematics;

namespace Engine.Start;

public class Object
{
    private readonly Model _model;

    public Object(Model model, string name)
    {
        _model = model;
        Name = name;
    }
    
    public string Name { get; }

    private float _pitch;
    private float _yaw;
    private float _roll;

    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; } = Vector3.One;

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

    private Matrix4 ModelMatrix => Matrix4.CreateScale(Scale) *
                                   Matrix4.CreateRotationY(_yaw) * Matrix4.CreateRotationX(_pitch) *
                                   Matrix4.CreateRotationZ(_roll) * Matrix4.CreateTranslation(Position);

    public void Draw(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        _model.Draw(ModelMatrix, viewMatrix, projectionMatrix);
    }
}