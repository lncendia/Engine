namespace Engine.Scenes.Shaders;

public class TexturedShaderLocations : LightedShaderLocations
{
    public int Texture { get; }
    public string Sampler2DName { get; }

    public TexturedShaderLocations(int position, int normal, int texture, string mMatrix, string vMatrix,
        string pMatrix, string nMatrix, string lightPosition, string camPosition, string ambientLightColor,
        string diffuseLightColor, string specularLightColor, string m, string sampler2DName) : base(position, normal, mMatrix, vMatrix,
        pMatrix, nMatrix, lightPosition, camPosition, ambientLightColor, diffuseLightColor, specularLightColor, m)
    {
        Texture = texture;
        Sampler2DName = sampler2DName;
    }
}