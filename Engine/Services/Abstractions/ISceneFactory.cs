using Engine.Scenes.Scenes;

namespace Engine.Services.Abstractions;

public interface ISceneFactory
{
    Scene Create(int width, int height);
}