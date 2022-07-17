Console.WriteLine("What should I output?");

TUI.SelectItem<string> sl = new (
    new List<string>(){
        "Hello",
        "Hello World",
        "How are you",
        "ohh :D"
    }
);

Console.WriteLine(sl.Value);