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
    public void MatrixTransposition()
    {
        Matrix a = new Matrix(new double[,] {{12, 4},
                                             {-17, 29},
                                             {-30, -36}});

        Matrix b = new Matrix(new double[,] {{12, -17, -30},
                                             {4, 29, -36}});

        Assert.True(a.GetTransform() == b);
    }

    [Fact]
    public void MatrixTransposition2()
    {
        Matrix a = new Matrix(new double[,] {{5, 8, 9, 2},
                                             {6, 12, 11, 4},
                                             {1, 0, 3, 1}});

        Matrix b = new Matrix(new double[,] {{5, 6, 1},
                                             {8, 12, 0},
                                             {9, 11, 3},
                                             {2, 4, 1}});

        Assert.True(a.GetTransform() == b);
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

    [Fact]
    public void TestBoolOprMore()
    {
        Matrix a = new Matrix(new double[,] {{0, 1, 2, 3}, 
                                             {0, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a * 2 > a);
        Assert.False(a > a);
        Assert.False(a > a * 2);
    }

    [Fact]
    public void TestBoolOprLess()
    {
        Matrix a = new Matrix(new double[,] {{0, 1, 2, 3}, 
                                             {0, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a < a * 3);
        Assert.False(a < a);
        Assert.False(a * 10 < a);
    }

    [Fact]
    public void TestBoolOprMoreAndEqual()
    {
        Matrix a = new Matrix(new double[,] {{0, 1, 2, 3}, 
                                             {0, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a * 2 >= a);
        Assert.True(a >= a);
        Assert.False(a >= a * 2);
    }

    [Fact]
    public void TestBoolOprLessAndEqual()
    {
        Matrix a = new Matrix(new double[,] {{0, 1, 2, 3}, 
                                             {0, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a <= a * 3);
        Assert.True(a <= a);
        Assert.False(a * 10 <= a);
    }
}