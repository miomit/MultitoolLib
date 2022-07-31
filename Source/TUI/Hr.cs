namespace TUI;

public class Hr
{
    private int PositionLeft, PositionTop;

    private char Symbol = '-';
    private ConsoleColor Color;

    public Hr(bool isNewLine = true, bool isEndl = true, char symbol = '-'){

        if (isNewLine) Console.Write('\n');

        PositionLeft = 0;
        PositionTop = Console.CursorTop;

        Draw();

        if (isEndl) Console.Write('\n');
    }

    private void Draw()
    {
        int oldPositionLeft = Console.CursorLeft;
        int oldPositionTop = Console.CursorTop;

        Console.SetCursorPosition(PositionLeft, PositionTop);

        for (int i = 0; i < Console.LargestWindowWidth; i++) Console.Write(Symbol);

        Console.SetCursorPosition(oldPositionLeft, oldPositionTop);
        
    }
}