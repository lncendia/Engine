using Engine.Services.Abstractions.Models;
using Texture = Engine.Scenes.Models.Texture;

namespace Engine.Services.Abstractions;

public interface IObjLoader
{
    ObjData Get(string name);
}