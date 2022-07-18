namespace Algorithms;

public static class Std
{
    private static void Swap<T> (ref T a, ref T b) 
    {
        T c = a;
        a = b;
        b = c;
    }
}