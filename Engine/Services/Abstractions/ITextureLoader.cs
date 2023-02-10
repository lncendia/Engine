using Texture = Engine.Scenes.Models.TexturedModel.Texture;

namespace Engine.Services.Abstractions;

public interface ITextureLoader
{
    Texture GetTexture(string name);
}