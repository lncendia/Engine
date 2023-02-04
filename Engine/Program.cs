using Engine.Services.Factories;
using OpenTK.Windowing.Desktop;
using GameWindow = Engine.Window.GameWindow;

using var g = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Title = "Блин" },
    new DefaultSceneFactory());
g.Run();