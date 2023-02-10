namespace Engine.Scenes.Models.TexturedModel;

public class Texture
{
    public Texture(int width, int height, byte[] data)
    {
        Width = width;
        Height = height;
        Data = data;
    }

    public int Width { get; }
    public int Height { get; }
    public byte[] Data { get; }
}