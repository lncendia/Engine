using Engine.Scenes.Scenes;
using Engine.Scenes.Shaders;
using Engine.Services.Abstractions;
using OpenTK.Mathematics;

namespace Engine.Scenes;

public partial class DefaultScene : Scene
{
    public DefaultScene(IShaderLoader loader, ITextureLoader textureLoader, IObjLoader objLoader, float aspectRatio) : base(
        new Camera(new Vector3(0, 0, 2), aspectRatio))
    {
        _textureLoader = textureLoader;
        _objLoader = objLoader;
        _shaderLoader = loader;
        InitializeComponents();
    }

    private readonly ITextureLoader _textureLoader;
    private readonly IShaderLoader _shaderLoader;
    private readonly IObjLoader _objLoader;

    public override void Draw()
    {
        foreach (var shader in new LightedShader[] { _textureShader, _lightShader })
        {
            shader.SetVector3(shader.LightPosition, _lightPosition);
            shader.SetVector3(shader.CamPosition, Camera.Position);
        }
        
        base.Draw();
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}