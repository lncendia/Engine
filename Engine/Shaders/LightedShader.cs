using Engine.Scenes.Shaders;

namespace Engine.Shaders;

public class LightShader : LightedShader
{
    private static LightedShaderLocations GetShaders()
    {
        return new LightedShaderLocations(0, 1, "u_MMatrix", "u_VMatrix", "u_PMatrix", "u_NMatrix", "u_LightPosition",
            "u_CamPosition", "u_AmbientLightColor", "u_DiffuseLightColor", "u_SpecularLightColor", "u_M");
    }

    public LightShader(string vertexSource, string fragmentSource) : base(vertexSource,
        fragmentSource, GetShaders())
    {
    }
}