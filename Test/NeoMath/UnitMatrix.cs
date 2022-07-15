using Xunit;

using NeoMath;

namespace Test.NeoMath;

public class MatrixTest
{
    [Fact]
    public void SimpleMatrixMultiplication()
    {
        Matrix a = new Matrix(new double[,] {{2, 0},
                                             {1, -1}});

        Matrix b = new Matrix(new double[,] {{1, 0, -2},
                                             {0, -1, 1}});

        Matrix c = new Matrix(new double[,] {{2, 0, -4},
                                             {1, 1, -3}});

        Assert.True(a * b == c);
    }

    [Fact]
    public void EqualsMatrix()
    {
        Matrix a = new Matrix(new double[,] {{0, 1, 2, 3}, 
                                             {0, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a == a);
        Assert.False(a != a);

        Assert.True(a != 2 * a);
    }
}