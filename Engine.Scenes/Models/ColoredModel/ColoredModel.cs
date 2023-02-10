using Engine.Scenes.Models.DefaultModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models.ColoredModel;

public class ColoredModel : Model
{
    protected readonly Vector3 Color;

    private new ColorShader.ColorShader Shader => (ColorShader.ColorShader)base.Shader;

    protected ColoredModel(Dots coordinates, Vector3 color, ColorShader.ColorShader shader) : base(coordinates, shader)
    {
        Color = color;
    }

    public override void Draw(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        if (!IsBuffersSet) throw new Exception();
        var modelMatrix = ModelMatrix;
        Shader.SetMatrix4(Shader.VMatrix, viewMatrix);
        Shader.SetMatrix4(Shader.PMatrix, projectionMatrix);
        Shader.SetMatrix4(Shader.MMatrix, modelMatrix);
        Shader.SetVector3(Shader.Color, Color);
        GL.BindVertexArray(VertexArray);
        if (IndexesBuffer.HasValue)
        {
            GL.DrawElements(PrimitiveType.Triangles, CountElements, DrawElementsType.UnsignedInt, 0);
        }
        else
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, CountElements);
        }
    }
}