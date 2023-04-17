using Engine.Models.DefaultModel;

namespace Engine.Models.LightedModel;

public class LightedDots : Dots
{
    public float[] Normals { get; }

    public LightedDots(float[] coordinates, float[] normals, uint[]? indexes) : base(coordinates, indexes)
    {
        Normals = normals;
    }
}