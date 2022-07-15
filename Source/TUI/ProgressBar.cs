namespace TUI;

public enum Theme
{
    ARCH,
    GOLD,
    NEO,
    DEFAULT
}

public class NumberTheme
{
    public ConsoleColor Color;

    public ConsoleColor BackgroundColor;

    public NumberTheme() : this(Console.ForegroundColor) { }

    public NumberTheme(ConsoleColor color) : this(color, Console.BackgroundColor) { }

    public NumberTheme(ConsoleColor color, ConsoleColor backgroundColor)
    {
        Color = color;
        BackgroundColor = backgroundColor;
    }
}

public class SymbolTheme
{
    public char Symbol;

    public ConsoleColor Color;

    public ConsoleColor BackgroundColor;

    public SymbolTheme(char symbol) : this(symbol, Console.ForegroundColor) { }

    public SymbolTheme(char symbol, ConsoleColor color) : this(symbol, color, Console.BackgroundColor) { }

    public SymbolTheme(char symbol, ConsoleColor color, ConsoleColor backgroundColor)
    {
        Symbol = symbol;
        Color = color;
        BackgroundColor = backgroundColor;
    }
}

public class ProgressBarTheme
{
    public SymbolTheme BorderLeft, BorderRight;// left - [ ] - right
    public SymbolTheme DeltaSpace, DeltaSuccess, DeltaTip; // - = >

    public SymbolTheme Percentages; // %

    public NumberTheme Number;

    public ProgressBarTheme(Theme theme)
    {
        switch (theme)
        {
            case Theme.ARCH:
                BorderLeft = new ('[');
                BorderRight = new (']');

                DeltaSpace = new('-');
                DeltaSuccess = new('#');
                DeltaTip = new('-');

                Percentages = new('%');

                Number = new();
            break;

            case Theme.GOLD:
                BorderLeft = new ('|', ConsoleColor.Yellow);
                BorderRight = new ('|', ConsoleColor.Yellow);

                DeltaSpace = new(' ');
                DeltaSuccess = new('-', ConsoleColor.Yellow);
                DeltaTip = new('>', ConsoleColor.Yellow);

                Percentages = new('%', ConsoleColor.Yellow);

                Number = new(ConsoleColor.Yellow);
            break;

            case Theme.NEO:
                BorderLeft = new ('[', ConsoleColor.White, ConsoleColor.DarkBlue);
                BorderRight = new (']', ConsoleColor.White, ConsoleColor.DarkBlue);

                DeltaSpace = new(' ', ConsoleColor.White, ConsoleColor.DarkBlue);
                DeltaSuccess = new('=', ConsoleColor.White, ConsoleColor.DarkBlue);
                DeltaTip = new(')', ConsoleColor.White, ConsoleColor.DarkBlue);

                Percentages = new('$', ConsoleColor.DarkBlue);

                Number = new(ConsoleColor.DarkBlue);
            break;

            default:
                BorderLeft = new ('[');
                BorderRight = new (']');

                DeltaSpace = new(' ');
                DeltaSuccess = new('=');
                DeltaTip = new('>');

                Percentages = new('%');

                Number = new();
            break;
        }
    }

    public ProgressBarTheme() : this(Theme.DEFAULT) { }
}

public class ProgressBar
{
    private int PositionLeft, PositionTop;

    private int _value = 0;

    private int Width;

    private bool IsPercentages;

    private ProgressBarTheme Theme;

    public int Value 
    {
        get => _value;
        set
        {
            _value = value;
            Draw();
        }
    }

    public int Max;

    public ProgressBar () : this(10, 100, true) { }

    public ProgressBar (bool isPercentages) : this(10, 100, isPercentages) { }

    public ProgressBar (int width, int max = 100) : this(width, max, true) { }

    public ProgressBar(int width, int max, bool isPercentages) : this(width, max, isPercentages, new()) { } 

    public ProgressBar(ProgressBarTheme theme) : this(10, theme) { }
    public ProgressBar(int width, ProgressBarTheme theme) : this(width, 100, true, theme) { }

    public ProgressBar(int width, int max, bool isPercentages, ProgressBarTheme theme)
    {
        Width = width;
        Max = max;
        IsPercentages = isPercentages;

        PositionLeft = Console.CursorLeft + 1;
        PositionTop = Console.CursorTop;
        Console.SetCursorPosition(PositionLeft + Width + (IsPercentages ? 5 : 0), PositionTop);

        Theme = theme;

        Draw();
    }

    private void Draw()
    {
        if (Value > Max) return;

        int oldPositionLeft = Console.CursorLeft;
        int oldPositionTop = Console.CursorTop;

        Console.SetCursorPosition(PositionLeft, PositionTop);
        PrintSymbolTheme(Theme.BorderLeft);

        for (int i = 0; i < Width; i++)
        {
            Console.SetCursorPosition(PositionLeft + i + 1, PositionTop);
            if (i < (int)((Value * Width) / Max ) - 1)
                PrintSymbolTheme(Theme.DeltaSuccess);
            else if (i < (int)((Value * Width) / Max ))
                PrintSymbolTheme(Theme.DeltaTip);
            else
                PrintSymbolTheme(Theme.DeltaSpace);
        }   

        PrintSymbolTheme(Theme.BorderRight);

        if (IsPercentages)
        {
            string percentages = ((int)((Value * 100) / Max)).ToString();
            Console.SetCursorPosition(PositionLeft + Width + 4 - percentages.Length, PositionTop);
            foreach (var s in percentages) PrintSymbol(s, Theme.Number.Color, Theme.Number.BackgroundColor, false);
            Console.SetCursorPosition(PositionLeft + Width + 5, PositionTop);
            PrintSymbolTheme(Theme.Percentages);
        }

        Console.SetCursorPosition(oldPositionLeft, oldPositionTop);
    }

    private static void PrintSymbolTheme(SymbolTheme theme) => PrintSymbol(theme.Symbol, theme.Color, theme.BackgroundColor);

    private static void PrintSymbol(char symbol, ConsoleColor color, ConsoleColor backgroundColor, bool isBackspace = true)
    {
        ConsoleColor oldColor = Console.ForegroundColor;
        ConsoleColor oldBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = color;
        Console.BackgroundColor = backgroundColor;

        Console.Write($"{(isBackspace? "\b" : "")}{symbol}");

        Console.ForegroundColor = oldColor;
        Console.BackgroundColor = oldBackgroundColor;
    }

}