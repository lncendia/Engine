using Texture = Engine.Models.TexturedModel.Texture;

namespace Engine.Start.Services.Abstractions;

public interface ITextureLoader
{
    Texture GetTexture(string name);
}