using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Engine;

public class Window : GameWindow
{
    private int _program;
    private int _vbo;
    private int _atribCoord;
    private int _uniformColor;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(
        gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnResize(ResizeEventArgs args)
    {
        GL.Viewport(0, 0, args.Width, args.Height);
    }

    protected override void OnLoad()
    {
        GL.ClearColor(Color.Blue);
        InitVbo();
        InitShader();
    }

    protected override void OnUnload()
    {
        FreeShader();
        FreeVbo();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {

    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit); 
        GL.UseProgram(_program); 
        GL.Uniform4(_uniformColor, 1.0f, 0.0f, 0.0f, 1.0f);
        GL.EnableVertexAttribArray(_atribCoord); 
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.VertexAttribPointer(_atribCoord, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3); 
        GL.DisableVertexAttribArray(_atribCoord); 
        GL.UseProgram(0); 
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0); 
        SwapBuffers();
    }

    private void InitShader()
    {
        const string vsSource = @"attribute vec2 coord; void main() { gl_Position = vec4(coord, 0.0, 1.0); };";
        const string fsSource = @"uniform vec4 color; void main() { gl_FragColor = color; };";
        var vShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vShader, vsSource);
        GL.CompileShader(vShader);
        var fShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fShader, fsSource);
        GL.CompileShader(fShader);
        _program = GL.CreateProgram();
        GL.AttachShader(_program, vShader);
        GL.AttachShader(_program, fShader);
        GL.LinkProgram(_program);
        const string attrName = "coord";
        _atribCoord = GL.GetAttribLocation(_program, attrName);
        if (_atribCoord == -1)
        {
            Console.WriteLine("could not bind attrib {0}", attrName);
            return;
        }
        
        const string unifName = "color";
        _uniformColor = GL.GetUniformLocation(_program, unifName);
        if (_uniformColor == -1)
        {
            Console.WriteLine("could not bind uniform {0}", unifName);
        }
    }

    private void InitVbo()
    {
        GL.GenBuffers(1, out _vbo);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        Vertex[] triangle =
        {
            new() { X = -1.0f, Y = -1.0f }, new() { X = 0.0f, Y = 1.0f }, new() { X = 1.0f, Y = -1.0f }
        }; //! Передаем вершины в буфер, 8 - количество байт в структуре 4 байта на 1 float, их у нас 2
        GL.BufferData(BufferTarget.ArrayBuffer, triangle.Length * 8, triangle, BufferUsageHint.StaticDraw);
    }

    private void FreeShader()
    {
        GL.UseProgram(0);
        GL.DeleteProgram(_program);
    }

    private void FreeVbo()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.DeleteBuffers(1, ref _vbo);
    }
}