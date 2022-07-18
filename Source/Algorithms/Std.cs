namespace Algorithms;

public static class Std
{
    public static void Swap<T> (ref T a, ref T b) 
    {
        T c = a;
        a = b;
        b = c;
    }
}