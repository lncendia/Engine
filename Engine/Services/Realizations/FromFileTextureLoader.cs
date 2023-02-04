using Engine.Scenes.Models;
using Engine.Services.Abstractions;
using StbImageSharp;

namespace Engine.Services.Realizations;

public class FromFileTextureLoader : ITextureLoader
{
    public Texture GetTexture(string name)
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
        var image = ImageResult.FromStream(File.OpenRead($"Resources/Textures/{name}"), ColorComponents.RedGreenBlueAlpha);
        return new Texture(image.Width, image.Height, image.Data);
    }
}