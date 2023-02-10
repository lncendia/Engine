using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Engine.Scenes.Models.DefaultModel.DefaultShader;

public abstract class Shader : IDisposable
{
    protected readonly int Handle;
    protected readonly Dictionary<string, int> UniformLocations = new();
    public int Position { get; }
    public string MMatrix { get; }
    public string VMatrix { get; }
    public string PMatrix { get; }

    protected Shader(string vertexSource, string fragmentSource, ShaderLocations locations)
    {
        Position = locations.Position;
        MMatrix = locations.MMatrix;
        VMatrix = locations.VMatrix;
        PMatrix = locations.PMatrix;

        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSource);
        CompileShader(vertexShader);

        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSource);
        CompileShader(fragmentShader);

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);

        LinkProgram();
        LoadUniforms();
        Clean(vertexShader, fragmentShader);
    }

    private static void CompileShader(int shader)
    {
        GL.CompileShader(shader);
        GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
        if (code == (int)All.True) return;
        var infoLog = GL.GetShaderInfoLog(shader);
        throw new Exception($"Error occurred while compiling Shader({shader}).\n\n{infoLog}");
    }

    private void LinkProgram()
    {
        GL.LinkProgram(Handle);
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out var code);
        if (code == (int)All.True) return;
        throw new Exception($"Error occurred while linking Program({Handle})");
    }

    private void LoadUniforms()
    {
        GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
        for (var i = 0; i < numberOfUniforms; i++)
        {
            var key = GL.GetActiveUniform(Handle, i, out _, out _);
            var location = GL.GetUniformLocation(Handle, key);
            UniformLocations.Add(key, location);
        }
    }

    private void Clean(int vertex, int fragment)
    {
        GL.DetachShader(Handle, vertex);
        GL.DetachShader(Handle, fragment);
        GL.DeleteShader(fragment);
        GL.DeleteShader(vertex);
    }

    public void Use() => GL.UseProgram(Handle);


    public void SetInt(string name, int data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(UniformLocations[name], data);
    }

    public void SetFloat(string name, float data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(UniformLocations[name], data);
    }

    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(Handle);
        GL.UniformMatrix4(UniformLocations[name], true, ref data);
    }

    public void SetMatrix3(string name, Matrix3 data)
    {
        GL.UseProgram(Handle);
        GL.UniformMatrix3(UniformLocations[name], true, ref data);
    }

    public void SetVector3(string name, Vector3 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform3(UniformLocations[name], data);
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        GL.DeleteProgram(Handle);
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Shader() => Dispose(false);
}