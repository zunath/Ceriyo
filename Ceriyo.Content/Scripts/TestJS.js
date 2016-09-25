function Main() {

    Logging.Print("firing logging");

    var desktop = Scene.CreateScene();
    var window = Control.CreateWindow(200, 200, 20, 20, "the title", true);

    desktop.ShowCursor = true;

    desktop.Update();

    Logging.Print(desktop.ShowCursor);
    Scene.AddControlToScene(desktop, window);
    Scene.ChangeScene(desktop);
}