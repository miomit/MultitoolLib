namespace NeoMath;

internal static class MatrixException
{
    public static void LineDoesNotExist(bool isRow = true) => throw new Exception($"this {(isRow ? "row" : "column")} of the matrix does not exist.");

    public static void SizesDontMatch() => throw new Exception("The dimensions of the matrix do not match.");

    public static void CannotMul() => throw new Exception("Matrices cannot be multiplied.");

    public static void InputSize() => throw new Exception("The entered size does not correspond to reality.");
}