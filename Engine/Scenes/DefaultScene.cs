using System.Drawing;
using Engine.Scenes.Scenes;
using Engine.Services.Abstractions;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes;

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
    private float _rotate;

    public override void Draw()
    {
        _rotate += 0.001f;
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        foreach (var shader in new[] {_textureShader, _lightShader})
        {
            shader.SetVector3(shader.LightDirection, _lightDirection);
            shader.SetVector3(shader.CamPosition, Camera.Position);
        }

        base.Draw();
        m.ForEach(x => x.Yaw += _rotate);
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}