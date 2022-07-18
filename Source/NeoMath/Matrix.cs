namespace NeoMath;

public class Matrix
{
    public double[,] Value { get; set; }
    public int Rows => Value.GetUpperBound(0) + 1;
    public int Columns => Value.Length / Rows;

    public double[] GetRowsVector(int row)
    {
        if (row >= Rows || row < 0) MatrixException.LineDoesNotExist();

        double[] res = new double[Columns];
        for (int i = 0; i < Columns; i++) res[i] = Value[row, i];
        return res;
    }

    public double[] GetColumnsVector(int col)
    {
        if (col >= Columns || col < 0) MatrixException.LineDoesNotExist(false);
        double[] res = new double[Rows];
        for (int i = 0; i < Rows; i++) res[i] = Value[i, col];
        return res;
    }

    public void SetRowsVector(int row, double[] rowsVector) 
    {
        if (row >= Rows || row < 0) MatrixException.LineDoesNotExist();

        for (int i = 0; i < Columns; i++) Value[row, i] = rowsVector[i];
    }

    public void SetColumnsVector(int col, double[] columnsVector) 
    {
        if (col >= Columns || col < 0) MatrixException.LineDoesNotExist(false);
        
        for (int i = 0; i < Rows; i++) Value[i, col] = columnsVector[i];
    }

    public Matrix(double[,] value)
    {
        Value = value;
    }

    public Matrix(int size) : this(size, size) { }

    public Matrix(int rows, int columns)
    {
        Value = new double[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                Value[i, j] = 0;
    }

    public void Input()
    {
       for (int row = 0; row < Rows; row++)
       {
            string[] vec = new string[Columns];
            vec = Console.ReadLine().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            if (vec.Length != Columns) MatrixException.InputSize();
            for (int col = 0; col < Columns; col++)
            {
                Value[row, col] = Convert.ToDouble(vec[col]);
            }
       } 
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

    public void SwapRows(int rowA, int rowB)
    {
        for (int col = 0; col < Columns; col++) Algorithms.Std.Swap<double>(ref Value[rowA, col], ref Value[rowB, col]);
    }

    public void SwapColumns(int colA, int colB)
    {
        for (int row = 0; row < Rows; row++) Algorithms.Std.Swap<double>(ref Value[row, colA], ref Value[row, colB]);
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
        if (a.Columns != b.Rows) MatrixException.CannotMul();
        
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
        if (a.Rows != b.Rows || a.Columns != b.Columns) MatrixException.SizesDontMatch();

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
        if (a.Rows != b.Rows || a.Columns != b.Columns) MatrixException.SizesDontMatch();

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