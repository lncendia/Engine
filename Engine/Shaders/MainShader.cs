using Engine.Scenes.Shaders;

namespace Engine.Shaders;

public class MainShader : Shader
{
    private static ShaderLocations GetShaders()
    {
        return new ShaderLocations(0, "u_MMatrix", "u_VMatrix", "u_PMatrix", "u_AmbientLightColor");
    }

    public MainShader(string vertexSource, string fragmentSource) : base(vertexSource,
        fragmentSource, GetShaders())
    {
    }
}