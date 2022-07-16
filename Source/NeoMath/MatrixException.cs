namespace NeoMath;

internal static class MatrixException
{
    public static void LineDoesNotExist(bool isRow = true) => throw new Exception($"this {(isRow ? "row" : "column")} of the matrix does not exist");
}