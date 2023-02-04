using Engine.Scenes.Shaders;

namespace Engine.Shaders;

public class TextureShader : TexturedShader
{
    private static TexturedShaderLocations GetShaders()
    {
        return new TexturedShaderLocations(0, 1, 2, "u_MMatrix", "u_VMatrix", "u_PMatrix", "u_NMatrix", "u_LightPosition",
            "u_CamPosition", "u_AmbientLightColor", "u_DiffuseLightColor", "u_SpecularLightColor", "u_M", "texture0");
    }

    public TextureShader(string vertexSource, string fragmentSource) : base(
        vertexSource, fragmentSource, GetShaders())
    {
    }
}