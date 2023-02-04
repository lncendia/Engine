namespace Engine.Scenes.Models;

public class LightedDots : Dots
{
    public float[] Normals { get; }

    public LightedDots(float[] coordinates, float[] normals, uint[]? indexes) : base(coordinates, indexes)
    {
        Normals = normals;
    }
}