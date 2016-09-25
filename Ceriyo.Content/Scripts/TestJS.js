function Main() {

    Logging.Print("firing logging");

    var desktop = Control.CreateNewDesktop();
    var window = Control.CreateWindow(200, 200, 20, 20, "the title", true);

    desktop.ShowCursor = true;

    desktop.Update();

    Logging.Print(desktop.ShowCursor);
    Control.AddWindowToDesktop(desktop, window);
    Control.ChangeDesktop(desktop);
}