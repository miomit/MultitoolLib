namespace TUI;

public class SelectItem<T>
{
    public List<T> ItemList;

    public int SelectIndex { get; private set; }

    public T Value;

    public SelectItem(List<T> itemList)
    {
        SelectIndex = 0;
        ItemList = itemList;

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        while(keyInfo.Key != ConsoleKey.Enter)
        {
            if (keyInfo.Key == ConsoleKey.UpArrow) SelectUp();
            if (keyInfo.Key == ConsoleKey.DownArrow) SelectDown();

            Draw();

            keyInfo = Console.ReadKey();
        }

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
        Console.Clear();
        for (int i = 0; i < ItemList.Count; i++)
        {
            Console.Write($"[{(SelectIndex == i ? "*" : " ")}] ");
            Console.WriteLine(ItemList[i]);
        }
        
    }
}