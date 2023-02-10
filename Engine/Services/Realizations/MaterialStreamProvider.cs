using ObjLoader.Loader.Loaders;

namespace Engine.Services.Realizations;

public class MaterialStreamProvider : IMaterialStreamProvider
{
    private readonly string _path;

    public MaterialStreamProvider(string path) => _path = path;

    public Stream Open(string materialFilePath) =>
        File.Open(Path.Combine(_path, materialFilePath), FileMode.Open, FileAccess.Read);
}