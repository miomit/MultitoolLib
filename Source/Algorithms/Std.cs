namespace Algorithms;

public static class Std
{
    public static void Swap<T> (ref T a, ref T b) 
    {
        T c = a;
        a = b;
        b = c;
    }

    public static int? GetIdByMinElem<T>(T[] arr, bool skipZero = false)
    {
        //Todo Exception arr == null
        int? id = null;

        double arrValue = 0;

        Func<int?, int, int?> skipZeroFunc = (x, i) => skipZero ? (arrValue != 0 ? i : x) : i;

        for (int i = 0; i < arr.Length; i++) 
        {
            arrValue = Convert.ToDouble(arr[i]);

            if (id is null) { id = skipZeroFunc(null, i); continue; }

            if (arrValue < Convert.ToDouble(arr[id ?? 0])) id = skipZeroFunc(id, i); //Todo Exception id is null :D
        }

        return id;
    }

    public static T GetMinElem<T>(T[] arr) => arr[GetIdByMinElem<T>(arr) ?? 0];
}