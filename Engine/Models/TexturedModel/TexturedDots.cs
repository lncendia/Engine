using Engine.Models.LightedModel;

namespace Engine.Models.TexturedModel;

public class TexturedDotes : LightedDots
{
    public float[] TextureCoordinates { get; }

    public TexturedDotes(float[] coordinates, float[] normals, uint[]? indexes, float[] textureCoordinates) : base(
        coordinates, normals, indexes)
    {
        TextureCoordinates = textureCoordinates;
    }
}