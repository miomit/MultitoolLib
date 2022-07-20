namespace Algorithms;

public static class Std
{
    public static void Swap<T> (ref T a, ref T b) 
    {
        T c = a;
        a = b;
        b = c;
    }

    public static int GetIdByMinElem<T>(T[] arr)
    {
        //Todo Exception arr == null
        int id = 0;

        for (int i = 0; i < arr.Length; i++) if (Convert.ToDouble(arr[i]) < Convert.ToDouble(arr[id])) id = i;

        return id;
    }

    public static T GetMinElem<T>(T[] arr) => arr[GetIdByMinElem<T>(arr)];
}