using Engine.Start.Services.Abstractions.Models;

namespace Engine.Start.Services.Abstractions;

public interface IObjLoader
{
    List<ObjData> Get(string name);
}