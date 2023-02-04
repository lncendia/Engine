using Engine.Scenes.Models;
using Engine.Scenes.ModelsExamples;
using Engine.Scenes.Scenes;
using Engine.Shaders;
using OpenTK.Mathematics;

namespace Engine.Scenes;

public partial class DefaultScene
{
    private Vector3 _lightPosition;

    private BoxModel _boxModel = null!;
    private LightModel _lightModel = null!;
    private PlateModel _plateModel = null!;
    private BoxTexturedModel _boxTexturedModel = null!;
    private ObjModel _objModel = null!;

    private MainShader _mainShader = null!;
    private LightShader _lightShader = null!;
    private TextureShader _textureShader = null!;

    private void InitializeComponents()
    {
        InitializeMainShader();
        InitializeLightShader();
        InitializeTextureShader();
        
        InitializeBox();
        InitializePlate();
        IntializeTexturedBox();
        IntializeObjModel();
        //IntializeSkyBox();

        _lightModel = new LightModel(_mainShader);
        _lightModel.Initialize();
        _lightModel.Position = new Vector3(0, 5, 2);
        Models.Add(_lightModel);
        _lightPosition = new Vector3(0, 5, 2);
    }


    private void InitializeMainShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("shader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("shader.frag");
        _mainShader = new MainShader(vsource, fsource);
        Shaders.Add(_mainShader);
    }

    private void InitializeTextureShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("textureShader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("textureShader.frag");
        _textureShader = new TextureShader(vsource, fsource);
        Shaders.Add(_textureShader);
    }
    
    private void InitializeLightShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("lightShader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("lightShader.frag");
        _lightShader = new LightShader(vsource, fsource);
        Shaders.Add(_lightShader);
    }
    
    
    private void InitializeBox()
    {
        var material = new Material(new Vector3(0.2775f, 0.2775f, 0.2775f), new Vector3(0.2775f, 0.2775f, 0.2775f),
            new Vector3(0.4739f, 0.4739f, 0.4739f), 50);
        _boxModel = new BoxModel(material, _lightShader);
        _boxModel.Initialize();
        Models.Add(_boxModel);
    }

    private void InitializePlate()
    {
        var material = new Material(new Vector3(0.4313f, 0.1313f, 0.8313f), new Vector3(0.4775f, 0.4775f, 0.1775f),
            new Vector3(0.5739f, 0.3739f, 0.5739f), 50);
        _plateModel = new PlateModel(material, _lightShader);
        _plateModel.Initialize();
        _plateModel.Position = new Vector3(0, -0.5f, 0);
        Models.Add(_plateModel);
    }

    // private void IntializeSkyBox()
    // {
    //     var back = _textureLoader.GetTexture("back.png");
    //     var bottom = _textureLoader.GetTexture("bottom.png");
    //     var top= _textureLoader.GetTexture("top.png");
    //     var front= _textureLoader.GetTexture("front.png");
    //     var left= _textureLoader.GetTexture("left.png");
    //     var right= _textureLoader.GetTexture("right.png");
    //     _skyBoxModel = new SkyBoxModel(front, back, left, right, top, bottom, _skyboxShader);
    //     _skyBoxModel.Initialize();
    //     _skyBoxModel.Scale = new Vector3(500, 500, 500);
    //     Models.Add(_skyBoxModel);
    // }
    
    private void IntializeTexturedBox()
    {
        var material = new Material(new Vector3(0.2775f, 0.2775f, 0.2775f), new Vector3(0.2775f, 0.2775f, 0.2775f),
            new Vector3(0.4739f, 0.4739f, 0.4739f), 50);
        var back = _textureLoader.GetTexture("back.png");
        _boxTexturedModel = new BoxTexturedModel(material, back, _textureShader);
        _boxTexturedModel.Initialize();
        _boxTexturedModel.Position = new Vector3(5, 0, 0);
        Models.Add(_boxTexturedModel);
    }

    private void IntializeObjModel()
    {
        var material = new Material(new Vector3(0.2775f, 0.2775f, 0.2775f), new Vector3(0.2775f, 0.2775f, 0.2775f),
            new Vector3(0.4739f, 0.4739f, 0.4739f), 50);
        var data = _objLoader.Get("Rose.obj");
        _objModel = new ObjModel(new LightedDots(data.Vertices, data.Normals, data.Indexes), material, _lightShader);
        _objModel.Initialize();
        _objModel.Position = new Vector3(3, 5, 2);
        _objModel.Scale = new Vector3(0.2f, 0.2f, 0.2f);
        Models.Add(_objModel);
    }
}