using Engine.Services.Abstractions;

namespace Engine.Services.Realizations;

public class FromFileShaderLoader : IShaderLoader
{
    public string GetVertexShaderSource(string name)
    {
        return File.ReadAllText($"Resources/Shaders/{name}");
    }

    public string GetFragmentShaderSource(string name)
    {
        return File.ReadAllText($"Resources/Shaders/{name}");
    }
}