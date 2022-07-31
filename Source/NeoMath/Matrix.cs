using Algorithms;

namespace NeoMath;

public class Matrix
{
    public double[,] Value { get; set; }
    public int Rows => Value.GetUpperBound(0) + 1;
    public int Columns => Value.Length / Rows;

    public double[] GetRowsVector(int row)
    {
        if (row >= Rows || row < 0) Interrupts.Matrix.LineDoesNotExist();

        double[] res = new double[Columns];
        for (int i = 0; i < Columns; i++) res[i] = Value[row, i];
        return res;
    }

    public double[] GetColumnsVector(int col)
    {
        if (col >= Columns || col < 0) Interrupts.Matrix.LineDoesNotExist(false);
        double[] res = new double[Rows];
        for (int i = 0; i < Rows; i++) res[i] = Value[i, col];
        return res;
    }

    public void SetRowsVector(int row, double[] rowsVector) 
    {
        if (row >= Rows || row < 0) Interrupts.Matrix.LineDoesNotExist();

        for (int i = 0; i < Columns; i++) Value[row, i] = rowsVector[i];
    }

    public void SetColumnsVector(int col, double[] columnsVector) 
    {
        if (col >= Columns || col < 0) Interrupts.Matrix.LineDoesNotExist(false);
        
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
            if (vec.Length != Columns) Interrupts.Matrix.InputSize();
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
        if (rowA < 0 || rowA >= Rows || rowB < 0 || rowB >= Rows) Interrupts.Matrix.LineDoesNotExist();
        for (int col = 0; col < Columns; col++) Algorithms.Std.Swap<double>(ref Value[rowA, col], ref Value[rowB, col]);
    }

    public void SwapColumns(int colA, int colB)
    {
        if (colA < 0 || colA >= Columns || colB < 0 || colB >= Columns) Interrupts.Matrix.LineDoesNotExist(false);
        for (int row = 0; row < Rows; row++) Algorithms.Std.Swap<double>(ref Value[row, colA], ref Value[row, colB]);
    }

    public Matrix Step(bool isUpperTriangular = true, Matrix united = null)
    {
        Step(isUpperTriangular: isUpperTriangular,
                        united: null,
                         isNok: true);

        return this;
    }

    private bool Step(bool isUpperTriangular, Matrix united, bool isAbs = false, bool isNok = false)
    {
        if (united is not null) 
            if (united.Rows != Rows && united.Columns != Columns) 
                Interrupts.Matrix.SizesDontMatch();

        int thisRowsColumns, thisColumnsRows;

        Func<int, double[]> getVector;
        Action<int, int> swap;

        Func<int, int, double> getValue, getUnitedValue;
        Action<int, int, double> setValue, setUnitedValue;

        bool isSub = false;

        if (isUpperTriangular)
        {
            thisColumnsRows = Columns;
            thisRowsColumns = Rows;

            getVector = GetColumnsVector;
            swap = SwapRows;

            getValue = (r, c) => Value[r, c];
            getUnitedValue = (r, c) => united.Value[r, c];

            setValue = (r, c, val) => Value[r, c] = val;
            setUnitedValue = (r, c, val) => united.Value[r, c] = val;
        }
        else
        {
            thisColumnsRows = Rows;
            thisRowsColumns = Columns;

            getVector = GetRowsVector;
            swap = SwapColumns;

            getValue = (c, r) => Value[r, c];
            getUnitedValue = (c, r) => united.Value[r, c];

            setValue = (c, r, val) => Value[r, c] = val;
            setUnitedValue = (c, r, val) => united.Value[r, c] = val;
        }

        int rowCol = 0;

        for (int colRow = 0; colRow < thisColumnsRows; colRow++)
        {
            if (rowCol == thisRowsColumns - 1) break;

            var vec = getVector(colRow)[rowCol..thisRowsColumns];

            var id = Std.GetIdByMinElem<double>(vec, true, isAbs);

            if (id is null) continue;

            if (rowCol != rowCol + id) { swap(rowCol, rowCol + id ?? 0); isSub = !isSub; }

            for (int rC = rowCol + 1; rC < thisRowsColumns; rC++)
            {
                if (getValue(rC, colRow) == 0d) continue;

                var k = getValue(rC, colRow) / getValue(rowCol, colRow);

                var nok = Std.Nok(getValue(rC, colRow), getValue(rowCol, colRow));
                var k1 = nok / getValue(rC, colRow);
                var k2 = nok / getValue(rowCol, colRow);

                for (int cR = colRow; cR < thisColumnsRows; cR++) 
                {
                    if (isNok)
                    {
                        setValue(rC, cR, 
                            getValue(rC, cR) * k1 - getValue(rowCol, cR) * k2
                        );

                        if (united is not null) setUnitedValue(rC, cR, 
                            getUnitedValue(rC, cR) * k1 - getUnitedValue(rowCol, cR) * k2
                        );
                    }
                    else
                    {
                        setValue(rC, cR, 
                            getValue(rC, cR) - getValue(rowCol, cR) * k
                        );

                        if (united is not null) setUnitedValue(rC, cR, 
                            getUnitedValue(rC, cR) - getUnitedValue(rowCol, cR) * k
                        );
                    }
                }
            }
            
            rowCol++;
        }

        return isSub;
    }

    public double Det()
    {
        if (Rows != Columns) Interrupts.Matrix.NotSquare();

        Matrix matrixStep = new(Value);

        double res = matrixStep.Step(isUpperTriangular: true,
                                                united: null,
                                                 isAbs: true) ? -1 : 1;

        for (int rowCol = 0; rowCol < Rows; rowCol++)
            res *= matrixStep.Value[rowCol, rowCol];

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
        if (a.Columns != b.Rows) Interrupts.Matrix.CannotMul();
        
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
        if (a.Rows != b.Rows || a.Columns != b.Columns) Interrupts.Matrix.SizesDontMatch();

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
        if (a.Rows != b.Rows || a.Columns != b.Columns) Interrupts.Matrix.SizesDontMatch();

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