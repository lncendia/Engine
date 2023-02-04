using Texture = Engine.Scenes.Models.Texture;

namespace Engine.Services.Abstractions;

public interface IMtlLoader
{
    Texture Get(string name);
}