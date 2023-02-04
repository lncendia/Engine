using System.Collections.Immutable;
using Engine.Services.Abstractions.Models;
using ObjLoader.Loader.Loaders;
using IObjLoader = Engine.Services.Abstractions.IObjLoader;

namespace Engine.Services.Realizations;

public class FromFileObjLoader : IObjLoader
{
    public ObjData Get(string name)
    {
        var objLoaderFactory = new ObjLoaderFactory();
        var objLoader = objLoaderFactory.Create();
        var fileStream = new FileStream($"Resources/Models/{name}", FileMode.Open);
        var result = objLoader.Load(fileStream);
        var indexes = result.Groups.SelectMany(x => x.Faces);
        var data = new List<uint>();
        foreach (var face in indexes)
        {
            for (int i = 0; i < face.Count; i++)
            {
                data.Add(Convert.ToUInt32(face[i].VertexIndex));
            }
        }

        return new ObjData(result.Vertices.SelectMany(x => new[] { x.X, x.Y, x.Z }).ToArray(),
            result.Normals.SelectMany(x => new[] { x.X, x.Y, x.Z }).ToArray(), data.ToArray());
    }
}