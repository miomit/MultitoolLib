namespace Interrupts;

internal static class Vector
{
    public static void SizesDontMatch() => throw new Exception("The dimensions of the vector do not match.");
}