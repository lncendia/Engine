using Texture = Engine.Scenes.Models.Texture;

namespace Engine.Services.Abstractions;

public interface ITextureLoader
{
    Texture GetTexture(string name);
}