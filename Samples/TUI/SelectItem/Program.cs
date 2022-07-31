TUI.Hr hr = new();
Console.WriteLine("What should I output?");
TUI.Hr hr2 = new(isNewLine: false);

TUI.SelectItem<string> sl = new (
    new List<string>(){
        "Hello",
        "Hello World",
        "How are you",
        "ohh :D"
    }
);

TUI.Hr hr3 = new();
Console.WriteLine(sl.Value);
TUI.Hr hr4 = new(isNewLine: false);