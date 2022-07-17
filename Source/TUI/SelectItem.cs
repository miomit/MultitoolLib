namespace TUI;

public class SelectItem<T>
{
    private int PositionLeft, PositionTop;

    private List<T> ItemList;

    public int SelectIndex { get; private set; }

    public T Value {get; private set; }

    public SelectItem(List<T> itemList, bool isEndl = true)
    {
        SelectIndex = 0;
        ItemList = itemList;

        PositionLeft = 0;
        PositionTop = Console.CursorTop;

        Console.SetCursorPosition(0, PositionTop + itemList.Count - 1);

        if (isEndl) Console.Write('\n');

        ConsoleKeyInfo keyInfo;
        do
        {
            Draw();

            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow) SelectUp();
            if (keyInfo.Key == ConsoleKey.DownArrow) SelectDown();

        }while(keyInfo.Key != ConsoleKey.Enter);

        Value = ItemList[SelectIndex];
    }

    public void SelectUp()
    {
        SelectIndex--;
        if (SelectIndex == -1) SelectIndex = ItemList.Count - 1;
    }

    public void SelectDown()
    {
        SelectIndex++;
        if (SelectIndex == ItemList.Count) SelectIndex = 0;
    }

    private void Draw()
    {
        int oldPositionLeft = Console.CursorLeft;
        int oldPositionTop = Console.CursorTop;

        Console.SetCursorPosition(PositionLeft, PositionTop);

        for (int i = 0; i < ItemList.Count; i++)
        {
            Console.Write($"[{(SelectIndex == i ? "*" : " ")}] ");
            Console.WriteLine(ItemList[i]);
        }

         Console.SetCursorPosition(oldPositionLeft, oldPositionTop);
        
    }
}