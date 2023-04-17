using Engine.Models.TexturedModel;
using Engine.Start.Services.Abstractions;
using StbImageSharp;

namespace Engine.Start.Services.Realizations;

public class FromFileTextureLoader : ITextureLoader
{
    public Texture GetTexture(string name)
    {
        var image = ImageResult.FromStream(File.OpenRead($"Resources/Textures/{name}"), ColorComponents.RedGreenBlueAlpha);
        return new Texture(image.Width, image.Height, image.Data);
    }
}