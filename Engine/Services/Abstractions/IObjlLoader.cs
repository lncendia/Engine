using Engine.Services.Abstractions.Models;

namespace Engine.Services.Abstractions;

public interface IObjLoader
{
    List<ObjData> Get(string name);
}