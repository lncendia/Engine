using Engine.ModelsExamples.SkyBox;

namespace Engine.Start.Shaders;

public class SkyBoxShader : Engine.ModelsExamples.SkyBox.SkyBoxShader
{
    private static SkyBoxShaderLocations GetShaders()
    {
        return new SkyBoxShaderLocations(0, "u_MMatrix", "u_VMatrix", "u_PMatrix", "texture0");
    }

    public SkyBoxShader(string vertexSource, string fragmentSource) : base(vertexSource,
        fragmentSource, GetShaders())
    {
    }
}