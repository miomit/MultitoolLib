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

    public static Matrix operator +(Matrix a, Matrix b)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = a.Value[row, col] + b.Value[row, col];
            }
        }
        return c;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = a.Value[row, col] - b.Value[row, col];
            }
        }
        return c;
    }

    public static Matrix operator *(Matrix a, double b)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = a.Value[row, col] * b;
            }
        }
        return c;
    }

    public static Matrix operator *(double b, Matrix a)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = a.Value[row, col] * b;
            }
        }
        return c;
    }

    public static Matrix operator %(Matrix a, double b)
    {
        Matrix c = new Matrix(a.Rows, a.Columns);
        for (int row = 0; row < c.Rows; row++)
        {
            for (int col = 0; col < c.Columns; col++)
            {
                c.Value[row, col] = a.Value[row, col] % b;
            }
        }
        return c;
    }
}