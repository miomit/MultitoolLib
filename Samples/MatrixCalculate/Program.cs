using NeoMath;
using TUI;

Matrix NewMatrix()
{
    int row, col;

    Console.Write("\nNumber of Rows: ");
    row = Convert.ToInt16(Console.ReadLine());

    Console.Write("Number of Columns: ");
    col = Convert.ToInt16(Console.ReadLine());

    Matrix res = new Matrix(row, col);

    Console.WriteLine("Enter the value of the matrix:");
    res.Input();

    return res;
}

Console.WriteLine("Simple Matrix Calculator");

Matrix a = NewMatrix(), b = NewMatrix();

Console.WriteLine("select an operation");

SelectItem<char> opr = new(
    new List<char>(){
        '+',
        '-',
        '*'
    }
);

Matrix c;

switch (opr.Value)
{
    case '+': c = a + b; break;
    case '-': c = a - b; break;
    default: c = a * b; break;
}

Console.WriteLine("Result:");
Console.Write(c);