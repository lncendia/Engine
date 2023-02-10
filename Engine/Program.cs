using Engine.Services.Factories;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using GameWindow = Engine.Window.GameWindow;

using var g = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Title = "Блин", Size = new Vector2i(1500,800)},
    new DefaultSceneFactory());
g.Run();