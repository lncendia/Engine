using Engine.Scenes.Models.DefaultModel;

namespace Engine.Scenes.Models.LightedModel;

public class LightedDots : Dots
{
    public float[] Normals { get; }

    public LightedDots(float[] coordinates, float[] normals, uint[]? indexes) : base(coordinates, indexes)
    {
        Normals = normals;
    }
}