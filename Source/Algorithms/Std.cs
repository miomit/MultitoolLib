namespace Algorithms;

public static class Std
{
    public static void Swap<T> (ref T a, ref T b) 
    {
        T c = a;
        a = b;
        b = c;
    }

    public static int? GetIdByMinElem<T>(T[] arr, bool skipZero = false, bool isAbs = false)
    {
        //Todo Exception arr == null
        int? id = null;

        double arrValue = 0;

        Func<int?, int, int?> skipZeroFunc = (x, i) => skipZero ? (arrValue != 0 ? i : x) : i;

        for (int i = 0; i < arr.Length; i++) 
        {
            arrValue = Convert.ToDouble(arr[i]);

            if (id is null) { id = skipZeroFunc(null, i); continue; }

            bool isMin = isAbs  ? Math.Abs(arrValue) < Math.Abs(Convert.ToDouble(arr[id ?? 0]))
                                : arrValue < Convert.ToDouble(arr[id ?? 0]);

            if (isMin) id = skipZeroFunc(id, i); //Todo Exception id is null :D
        }

        return id;
    }

    public static T GetMinElem<T>(T[] arr)
    {
        int? id = GetIdByMinElem<T>(arr);

        if (id is null) Interrupts.Std.EmptyArray();

        return arr[id ?? 0];
    }

    public static double Nod(double a, double b)
    {
        if (b < 0) b = -b;
        if (a < 0) a = -a;
        
        while (b > 0)
        {
            double temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
    public static double Nok(double a, double b) => Math.Abs(a * b)/Nod(a, b);
}