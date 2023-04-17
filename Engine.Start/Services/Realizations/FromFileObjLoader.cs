using Engine.Start.Services.Abstractions.Models;
using ObjLoader.Loader.Loaders;
using OpenTK.Mathematics;

namespace Engine.Start.Services.Realizations;

public class FromFileObjLoader : Abstractions.IObjLoader
{
    public List<ObjData> Get(string name)
    {
        const string path = "Resources/Models";
        var material = new MaterialStreamProvider(path);
        var objLoaderFactory = new ObjLoaderFactory();
        var objLoader = objLoaderFactory.Create(material);
        var fileStream = new FileStream(Path.Combine(path, name), FileMode.Open);
        var result = objLoader.Load(fileStream);
        List<ObjData> list = new List<ObjData>();
        foreach (var group in result.Groups.Where(x => x.Faces.Count > 0))
        {
            var coords = new List<float>();
            var normals = new List<float>();

            foreach (var groupFace in group.Faces)
            {
                for (int i = 0; i < groupFace.Count; i++)
                {
                    var vertex = result.Vertices[groupFace[i].VertexIndex - 1];
                    coords.Add(vertex.X);
                    coords.Add(vertex.Y);
                    coords.Add(vertex.Z);
                    var normal = result.Normals[groupFace[i].NormalIndex - 1];
                    normals.Add(normal.X);
                    normals.Add(normal.Y);
                    normals.Add(normal.Z);
                }
            }

            var ambient = new Vector3(group.Material.AmbientColor.X, group.Material.AmbientColor.Y,
                group.Material.AmbientColor.Z);
            var diffuse = new Vector3(group.Material.DiffuseColor.X, group.Material.DiffuseColor.Y,
                group.Material.DiffuseColor.Z);
            var specular = new Vector3(group.Material.SpecularColor.X, group.Material.SpecularColor.Y,
                group.Material.SpecularColor.Z);
            var m = group.Material.SpecularCoefficient;
            list.Add(new ObjData(coords.ToArray(), normals.ToArray(), ambient, diffuse, specular, m));
        }

        return list;
    }
}