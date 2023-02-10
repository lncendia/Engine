using Engine.Scenes.Models.LightedModel.LightShader;

namespace Engine.Scenes.Models.TexturedModel.TextureShader;

public class TextureShader : LightShader
{
    public int Texture { get; }
    public string SamplerName { get; }

    public TextureShader(string vertexSource, string fragmentSource, TextureShaderLocations shaderLocations) :
        base(vertexSource, fragmentSource, shaderLocations)
    {
        Texture = shaderLocations.Texture;
        SamplerName = shaderLocations.SamplerName;
    }
}