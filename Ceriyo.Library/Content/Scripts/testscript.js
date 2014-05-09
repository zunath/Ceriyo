
function Main() {
    var name = app.GetName(self);

    var obj = new Object();
    obj.tag = "whateva";

    

    app.Print("This is a test message. " + name);

    return obj;
}