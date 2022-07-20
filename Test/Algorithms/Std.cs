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
    }
}