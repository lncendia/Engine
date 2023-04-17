using Engine.Start.Services.Abstractions;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Engine.Start.Window;

internal class GameWindow : OpenTK.Windowing.Desktop.GameWindow
{
    private readonly ISceneFactory _sceneFactory;
    private Scene? _scene;

    public GameWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings,
        ISceneFactory sceneFactory) : base(
        gameWindowSettings, nativeWindowSettings)
    {
        _sceneFactory = sceneFactory;
    }

    protected override void OnLoad()
    {
        _scene = _sceneFactory.Create(Size.X, Size.Y);
        CursorState = CursorState.Grabbed;
    }

    protected override void OnUnload()
    {
        _scene!.Dispose();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        if (!IsFocused) return;

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            _scene!.MoveCamera(MoveDirect.Front, (float)e.Time);
        }

        if (KeyboardState.IsKeyDown(Keys.S))
        {
            _scene!.MoveCamera(MoveDirect.Back, (float)e.Time);
        }

        if (KeyboardState.IsKeyDown(Keys.A))
        {
            _scene!.MoveCamera(MoveDirect.Left, (float)e.Time);
        }

        if (KeyboardState.IsKeyDown(Keys.D))
        {
            _scene!.MoveCamera(MoveDirect.Right, (float)e.Time);
        }

        if (KeyboardState.IsKeyDown(Keys.Space))
        {
            _scene!.MoveCamera(MoveDirect.Up, (float)e.Time);
        }

        if (KeyboardState.IsKeyDown(Keys.LeftShift))
        {
            _scene!.MoveCamera(MoveDirect.Down, (float)e.Time);
        }

        _scene!.RotateCamera(new Vector2(MouseState.X, MouseState.Y));
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        _scene!.Draw();
        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs args)
    {
        if (args.Width == 0 || args.Height == 0) return;
        _scene!.ResizeScene(args.Width, args.Height);
    }
}