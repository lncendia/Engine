using System.Drawing;
using Engine.Start.Services.Abstractions;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Start.Scenes;

public partial class DefaultScene : Scene
{
    public DefaultScene(IShaderLoader loader, ITextureLoader textureLoader, IObjLoader objLoader,
        float aspectRatio) : base(
        new Camera(new Vector3(0, 0, 2), aspectRatio))
    {
        _textureLoader = textureLoader;
        _objLoader = objLoader;
        _shaderLoader = loader;
        InitializeComponents();
        GL.ClearColor(Color.DarkCyan);
        GL.Enable(EnableCap.DepthTest);
    }

    private readonly ITextureLoader _textureLoader;
    private readonly IShaderLoader _shaderLoader;
    private readonly IObjLoader _objLoader;
    private const float Rotate = 0.1f;

    public override void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        base.Draw();
        foreach (var x in Objects)
        {
            if (x.Name == "SkyBox") continue;
            x.Yaw += Rotate;
        }
    }

    public override void PrepareShaders()
    {
        foreach (var shader in new[] { _textureShader, _lightShader })
        {
            shader.SetVector3(shader.LightDirection, _lightDirection);
            shader.SetVector3(shader.CamPosition, Camera.Position);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        Models.ForEach(x => x.Dispose());
        Shaders.ForEach(x => x.Dispose());
        base.Dispose(disposing);
    }
}