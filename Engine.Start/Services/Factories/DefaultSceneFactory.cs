using Engine.Start.Scenes;
using Engine.Start.Services.Abstractions;
using Engine.Start.Services.Realizations;

namespace Engine.Start.Services.Factories;

public class DefaultSceneFactory : ISceneFactory
{
    public Scene Create(int width, int height)
    {
        return new DefaultScene(new FromFileShaderLoader(), new FromFileTextureLoader(), new FromFileObjLoader(),
            width / (float)height);
    }
}