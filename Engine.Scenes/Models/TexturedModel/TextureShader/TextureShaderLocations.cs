using Engine.Scenes.Models.LightedModel.LightShader;

namespace Engine.Scenes.Models.TexturedModel.TextureShader;

public class TextureShaderLocations : LightShaderLocations
{
    public int Texture { get; }
    public string SamplerName { get; }

    public TextureShaderLocations(int position, int normal, int texture, string mMatrix, string vMatrix,
        string pMatrix, string nMatrix, string LightDirection, string camPosition, string ambientLightColor,
        string diffuseLightColor, string specularLightColor, string m, string samplerName) : base(position, normal, mMatrix, vMatrix,
        pMatrix, nMatrix, LightDirection, camPosition, ambientLightColor, diffuseLightColor, specularLightColor, m)
    {
        Texture = texture;
        SamplerName = samplerName;
    }
}