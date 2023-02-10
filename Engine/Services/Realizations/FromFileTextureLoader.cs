using Engine.Scenes.Models.TexturedModel;
using Engine.Services.Abstractions;
using StbImageSharp;

namespace Engine.Services.Realizations;

public class FromFileTextureLoader : ITextureLoader
{
    public Texture GetTexture(string name)
    {
        var image = ImageResult.FromStream(File.OpenRead($"Resources/Textures/{name}"), ColorComponents.RedGreenBlueAlpha);
        return new Texture(image.Width, image.Height, image.Data);
    }
}