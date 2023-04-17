namespace Engine.Models.DefaultModel;

public class Dots
{
    public float[] Coordinates { get; }
    public uint[]? Indexes { get; }

    public Dots(float[] coordinates, uint[]? indexes)
    {
        Coordinates = coordinates;
        Indexes = indexes;
    }
}