using Engine.Scenes.Models;
using Engine.Scenes.Models.ColoredModel.ColorShader;
using Engine.Scenes.Models.DefaultModel;
using Engine.Scenes.Models.LightedModel;
using Engine.Scenes.Models.LightedModel.LightShader;
using Engine.Scenes.Models.TexturedModel.TextureShader;
using Engine.Scenes.ModelsExamples;
using Engine.Scenes.ModelsExamples.SkyBox;
using Engine.Scenes.Scenes;
using Engine.Shaders;
using OpenTK.Mathematics;
using SkyBoxShader = Engine.Scenes.ModelsExamples.SkyBox.SkyBoxShader;

namespace Engine.Scenes;

public partial class DefaultScene
{
    private Vector3 _lightDirection;
    

    private ColorShader _mainShader = null!;
    private LightShader _lightShader = null!;
    private TextureShader _textureShader = null!;
    private Engine.Shaders.SkyBoxShader _skyBoxShader = null!;

    private void InitializeComponents()
    {
        InitializeMainShader();
        InitializeLightShader();
        InitializeTextureShader();
        InitializeSkyBoxShader();
        
        IntializeSkyBox();
        InitializeBox();
        InitializePlate();
        IntializeTexturedBox();
        IntializeObjModel();
        
        _lightDirection = new Vector3(0.62735f, -0.3f, 0.73735f);
    }


    private void InitializeMainShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("colorShader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("colorShader.frag");
        var shaderLocations = new ColorShaderLocations(0, "u_MMatrix", "u_VMatrix", "u_PMatrix", "u_AmbientLightColor");
        _mainShader = new ColorShader(vsource, fsource, shaderLocations);
        Shaders.Add(_mainShader);
    }

    private void InitializeTextureShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("textureShader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("textureShader.frag");
        var shaderLocations = new TextureShaderLocations(0, 1, 2, "u_MMatrix", "u_VMatrix", "u_PMatrix", "u_NMatrix", "u_LightDirection",
            "u_CamPosition", "u_AmbientLightColor", "u_DiffuseLightColor", "u_SpecularLightColor", "u_M", "texture0");

        _textureShader = new TextureShader(vsource, fsource, shaderLocations);
        Shaders.Add(_textureShader);
    }
    
    private void InitializeLightShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("lightShader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("lightShader.frag");
        var shaderLocations = new LightShaderLocations(0, 1, "u_MMatrix", "u_VMatrix", "u_PMatrix", "u_NMatrix", "u_LightDirection",
            "u_CamPosition", "u_AmbientLightColor", "u_DiffuseLightColor", "u_SpecularLightColor", "u_M");
        _lightShader = new LightShader(vsource, fsource, shaderLocations);
        Shaders.Add(_lightShader);
    }
    
    private void InitializeSkyBoxShader()
    {
        var vsource = _shaderLoader.GetVertexShaderSource("skyboxShader.vert");
        var fsource = _shaderLoader.GetVertexShaderSource("skyboxShader.frag");
        _skyBoxShader = new Engine.Shaders.SkyBoxShader(vsource, fsource);
        Shaders.Add(_skyBoxShader);
    }
    
    
    private void InitializeBox()
    {
        var material = new Material(new Vector3(0.2775f, 0.2775f, 0.2775f), new Vector3(0.2775f, 0.2775f, 0.2775f),
            new Vector3(0.4739f, 0.4739f, 0.4739f), 50);
        var _boxModel = new BoxModel(material, _lightShader);
        _boxModel.Initialize();
        Models.Add(_boxModel);
    }

    private void InitializePlate()
    {
        var material = new Material(new Vector3(0.4313f, 0.1313f, 0.8313f), new Vector3(0.4775f, 0.4775f, 0.1775f),
            new Vector3(0.5739f, 0.3739f, 0.5739f), 50);
        var _plateModel = new PlateModel(material, _lightShader);
        _plateModel.Initialize();
        _plateModel.Position = new Vector3(0, -0.5f, 0);
        Models.Add(_plateModel);
    }

    private void IntializeSkyBox()
    {
        var back = _textureLoader.GetTexture("back.jpg");
        var bottom = _textureLoader.GetTexture("bottom.jpg");
        var top= _textureLoader.GetTexture("top.jpg");
        var front= _textureLoader.GetTexture("front.jpg");
        var left= _textureLoader.GetTexture("left.jpg");
        var right= _textureLoader.GetTexture("right.jpg");
        var _skyBoxModel = new SkyBoxModel(front, back, left, right, top, bottom, _skyBoxShader);
        _skyBoxModel.Initialize();
        Models.Add(_skyBoxModel);
    }
    
    private void IntializeTexturedBox()
    {
        var material = new Material(new Vector3(0.2775f, 0.2775f, 0.2775f), new Vector3(0.2775f, 0.2775f, 0.2775f),
            new Vector3(0.4739f, 0.4739f, 0.4739f), 50);
        var back = _textureLoader.GetTexture("wood.jpg");
        var _boxTexturedModel = new BoxTexturedModel(material, back, _textureShader);
        _boxTexturedModel.Initialize();
        _boxTexturedModel.Position = new Vector3(5, 0, 0);
        Models.Add(_boxTexturedModel);
    }

    private readonly List<Model> m = new List<Model>();
    private void IntializeObjModel()
    {
        var data = _objLoader.Get("aaa.obj");
        foreach (var objData in data)
        {
            var model = new LightedModel(new LightedDots(objData.Vertices, objData.Normals, null),
                new Material(objData.Color, objData.DifuseColor, objData.SpecularColor, objData.M), _lightShader);
            model.Initialize();
            model.Position = new Vector3(0, 0.5f, 0);
            //model.Scale = new Vector3(0.02f, 0.02f, 0.02f);
            Models.Add(model);
            m.Add(model);
        }
    }
}