namespace Engine.Start.Services.Abstractions;

public interface ISceneFactory
{
    Scene Create(int width, int height);
}