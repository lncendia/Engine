using OpenTK.Mathematics;

namespace Engine.Scenes.Models;

public class Material
{
    public Material(Vector3 ambientLightColor, Vector3 diffuseLightColor, Vector3 specularLightColor, float m)
    {
        AmbientLightColor = ambientLightColor;
        DiffuseLightColor = diffuseLightColor;
        SpecularLightColor = specularLightColor;
        M = m;
    }

    public Vector3 AmbientLightColor { get; }
    public Vector3 DiffuseLightColor { get; }
    public Vector3 SpecularLightColor { get; }
    public float M { get; }
}