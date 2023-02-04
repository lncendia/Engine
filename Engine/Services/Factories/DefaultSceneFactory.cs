using Engine.Scenes;
using Engine.Scenes.Scenes;
using Engine.Services.Abstractions;
using Engine.Services.Realizations;

namespace Engine.Services.Factories;

public class DefaultSceneFactory : ISceneFactory
{
    public Scene Create(int width, int height)
    {
        return new DefaultScene(new FromFileShaderLoader(), new FromFileTextureLoader(), width / (float)height);
    }
}