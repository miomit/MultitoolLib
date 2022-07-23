using Xunit;

using Algorithms;
using System;

namespace Test.Algorithms;

public class StdTest
{
    [Fact]
    public void GetIdByMinElem()
    {
        double[] a = new double[] {1.23d, 1.00d, -2.66d, -0.123d, 10d};
        int resA = 2;

        Assert.True(Std.GetIdByMinElem<double>(a) == resA);

        double[] b = new double[] {1.23d, 10.00d, 0.12131d, 0d, 0d, 5d};
        int resB = 3;

        Assert.True(Std.GetIdByMinElem<double>(b) == resB);

        double[] c = new double[] {1.23d};
        int resC = 0;

        Assert.True(Std.GetIdByMinElem<double>(c) == resC);

        double[] d = new double[] {0d, 0d, 0d, 0d};

        Assert.True(Std.GetIdByMinElem<double>(d, true) is null);

        //TODO test exception
    }

    //TODO test GetMinElem

    [Fact]
    public void Nod()
    {
        int a1 = 84, b1 = 90, res1 = 6;
        Assert.True(Std.Nod(a1, b1) == res1);

        int a2 = 15, b2 = 28, res2 = 1;
        Assert.True(Std.Nod(a2, b2) == res2);

        int a3 = 27, b3 = 9,  res3 = 9;
        Assert.True(Std.Nod(a3, b3) == res3);
    }

    [Fact]
    public void Nok()
    {
        int a1 = 126, b1 = 70, res1 = 630;
        Assert.True(Std.Nok(a1, b1) == res1);

        int a2 = 68, b2 = 34, res2 = 68;
        Assert.True(Std.Nok(a2, b2) == res2);

        int a3 = 441, b3 = 700,  res3 = 44100;
        Assert.True(Std.Nok(a3, b3) == res3);
    }
}