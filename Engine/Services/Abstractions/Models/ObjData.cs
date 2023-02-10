using OpenTK.Mathematics;

namespace Engine.Services.Abstractions.Models;

public class ObjData
{
    public ObjData(float[] vertices, float[] normals, Vector3 color, Vector3 difuseColor, Vector3 specularColor,
        float m)
    {
        Vertices = vertices;
        Normals = normals;
        Color = color;
        DifuseColor = difuseColor;
        SpecularColor = specularColor;
        M = m;
    }

    public float[] Vertices { get; }
    public float[] Normals { get; }
    public Vector3 Color { get; }
    public Vector3 DifuseColor { get; }
    public Vector3 SpecularColor { get; }
    public float M { get; }
}