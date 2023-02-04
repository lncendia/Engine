using Engine.Scenes.Models;
using Engine.Scenes.Shaders;

namespace Engine.Scenes.ModelsExamples;

public class ObjModel : LightedModel
{
    public new LightedShader Shader => (LightedShader)base.Shader;

    public ObjModel(LightedDots coordinates, Material material, LightedShader shader) : base(coordinates, material,
        shader)
    {
    }
}