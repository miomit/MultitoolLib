namespace NeoMath;

public class Matrix
{
    public double[,] Value { get; set; }
    public int Rows => Value.GetUpperBound(0) + 1;
    public int Columns => Value.Length / Rows;

    public Matrix(double[,] value)
    {
        Value = value;
    }

    public Matrix(int rows, int columns)
    {
        Value = new double[rows, columns];
    }

    public Matrix GetTransform()
    {
        Matrix res = new Matrix(Columns, Rows);
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                res.Value[col, row] = Value[row, col];
            }
        }
        return res;
    }

    public override string ToString()
    {
        string res = "";
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                res += Value[row, col].ToString() + "\t";
            }
            res += "\n";
        }
        return res;
    }

    public static bool operator ==(Matrix a, Matrix b) => BoolOprWithMat(a, b, (x, y) => x != y);

    public static bool operator !=(Matrix a, Matrix b) => !(a == b);

    public static bool operator <(Matrix a, Matrix b) => BoolOprWithMat(a, b, (x, y) => x >= y);

    public static bool operator >(Matrix a, Matrix b) => BoolOprWithMat(a, b, (x, y) => x <= y);

    public static bool operator <=(Matrix a, Matrix b) => BoolOprWithMat(a, b, (x, y) => x > y);

    public static bool operator >=(Matrix a, Matrix b) => BoolOprWithMat(a, b, (x, y) => x < y);

    public static Matrix operator +(Matrix a, Matrix b) => OprWithMat(a, b, (x, y) => x + y);

    public static Matrix operator -(Matrix a, Matrix b) => OprWithMat(a, b, (x, y) => x - y);

    public static Matrix operator %(Matrix a, double b) => OprWithNum(a, x => x % b);

    public static Matrix operator *(Matrix a, double b) => OprWithNum(a, x => x * b);

    public static Matrix operator *(double b, Matrix a) => OprWithNum(a, x => x * b);


    public static Matrix operator *(Matrix a, Matrix b)
    {
        Matrix c = new Matrix(a.Rows, b.Columns);

        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = 0;

                for (int i = 0; i < a.Columns; i++) c.Value[row, col] += a.Value[row, i] * b.Value[i, col];
            }
        }
        return c;
    }

    private static Matrix OprWithNum (Matrix a, Func<double, double> func)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = func(a.Value[row, col]);
            }
        }
        return c;
    }

    private static Matrix OprWithMat (Matrix a, Matrix b, Func<double, double, double> func)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = func(a.Value[row, col], b.Value[row, col]);
            }
        }
        return c;
    }

    private static bool BoolOprWithMat (Matrix a, Matrix b, Func<double, double, bool> func)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns) return false;

        for (int row = 0; row < a.Rows; row++)
        {
            for (int col = 0; col < a.Columns; col++)
            {
                if (func(a.Value[row, col], b.Value[row, col])) return false;
            }
        }

        return true;
    }
}