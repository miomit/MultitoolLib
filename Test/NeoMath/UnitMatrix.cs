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
    public void TestGetRowsVector()
    {
        Matrix a = new Matrix(new double[,] {{12, 4},
                                             {-17, 29},
                                             {-30, -36}});

        double[] aRes = a.GetRowsVector(1);
        double[] aOtv = new double[] {-17, 29};

        for (int i = 0; i < aRes.Length; i++) Assert.True(aRes[i] == aOtv[i]);

        Matrix b = new Matrix(new double[,] {{12, -17, -30},
                                             {4, 29, -36}});

        double[] bRes = b.GetRowsVector(0);
        double[] bOtv = new double[] {12, -17, -30};

        for (int i = 0; i < bRes.Length; i++) Assert.True(bRes[i] == bOtv[i]);
    }

    [Fact]
    public void TestGetColumnsVector()
    {
        Matrix a = new Matrix(new double[,] {{12, 4},
                                             {-17, 29},
                                             {-30, -36}});

        double[] aRes = a.GetColumnsVector(1);
        double[] aOtv = new double[] {4, 29, -36};

        for (int i = 0; i < aRes.Length; i++) Assert.True(aRes[i] == aOtv[i]);

        Matrix b = new Matrix(new double[,] {{12, -17, -30},
                                             {4, 29, -36}});

        double[] bRes = b.GetColumnsVector(0);
        double[] bOtv = new double[] {12, 4};

        for (int i = 0; i < bRes.Length; i++) Assert.True(bRes[i] == bOtv[i]);
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
        Matrix a = new Matrix(new double[,] {{2, 1, 2, 3}, 
                                             {1, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a == a);
        Assert.False(a != a);

        Assert.True(a != 2 * a);
    }

    [Fact]
    public void TestBoolOprMore()
    {
        Matrix a = new Matrix(new double[,] {{1, 1, 2, 3}, 
                                             {2, 5, 3, 9}, 
                                             {3, 1, 1, 2}});

        Assert.True(a * 2 > a);
        Assert.False(a > a);
        Assert.False(a > a * 2);
    }

    [Fact]
    public void TestBoolOprLess()
    {
        Matrix a = new Matrix(new double[,] {{2, 1, 2, 3}, 
                                             {1, 5, 3, 9}, 
                                             {3, 1, 2, 1}});

        Assert.True(a < a * 3);
        Assert.False(a < a);
        Assert.False(a * 10 < a);
    }

    [Fact]
    public void TestBoolOprMoreAndEqual()
    {
        Matrix a = new Matrix(new double[,] {{2, 1, 2, 3}, 
                                             {1, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a * 2 >= a);
        Assert.True(a >= a);
        Assert.False(a >= a * 2);
    }

    [Fact]
    public void TestBoolOprLessAndEqual()
    {
        Matrix a = new Matrix(new double[,] {{1, 1, 2, 3}, 
                                             {2, 5, 3, 9}, 
                                             {3, 1, 0, 0}});

        Assert.True(a <= a * 3);
        Assert.True(a <= a);
        Assert.False(a * 10 <= a);
    }
}