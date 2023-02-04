namespace Engine.Services.Abstractions;

public interface IShaderLoader
{
    string GetVertexShaderSource(string name);
    string GetFragmentShaderSource(string name);
}