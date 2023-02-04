namespace Engine.Services.Abstractions.Models;

public class ObjData
{
    public ObjData(float[] vertices, float[] normals, uint[] indexes)
    {
        Vertices = vertices;
        Normals = normals;
        Indexes = indexes;
    }

    public float[] Vertices { get; }
    public float[] Normals { get; }
    public uint[] Indexes { get; }
}