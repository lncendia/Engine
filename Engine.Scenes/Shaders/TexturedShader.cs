namespace Engine.Scenes.Shaders;

public abstract class TexturedShader : LightedShader
{
    public int Texture { get; }
    public string Sampler2DName { get; }

    protected TexturedShader(string vertexSource, string fragmentSource, TexturedShaderLocations shaderLocations) :
        base(vertexSource, fragmentSource, shaderLocations)
    {
        Texture = shaderLocations.Texture;
        Sampler2DName = shaderLocations.Sampler2DName;
    }
}