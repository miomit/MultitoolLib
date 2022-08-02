namespace NeoMath;

public class Vector
{
    public double[] Value { get; set; }

    public int Count => Value.Length;

    public Vector(int count)
    {
        Value = new double[count];
    }

    public Vector(double[] value)
    {
        Value = value;
    }

    public override string ToString()
    {
        string res = "[";
        foreach (double val in Value) res += $"{val},";
        return res[0..(res.Length-1)] + "]";
    }

    public static bool operator ==(Vector a, Vector b) => BoolOprWithVec(a, b, (x, y) => x != y);

    public static bool operator !=(Vector a, Vector b) => !(a == b);

    public static bool operator <(Vector a, Vector b) => BoolOprWithVec(a, b, (x, y) => x >= y);

    public static bool operator >(Vector a, Vector b) => BoolOprWithVec(a, b, (x, y) => x <= y);

    public static bool operator <=(Vector a, Vector b) => BoolOprWithVec(a, b, (x, y) => x > y);

    public static bool operator >=(Vector a, Vector b) => BoolOprWithVec(a, b, (x, y) => x < y);

    public static Vector operator +(Vector a, Vector b) => OprWithVec(a, b, (x, y) => x + y);

    public static Vector operator -(Vector a, Vector b) => OprWithVec(a, b, (x, y) => x - y);

    public static Vector operator %(Vector a, double b) => OprWithNum(a, x => x % b);

    public static Vector operator *(Vector a, double b) => OprWithNum(a, x => x * b);

    public static Vector operator *(double b, Vector a) => OprWithNum(a, x => x * b);

    private static Vector OprWithNum (Vector a, Func<double, double> func)
    {
        Vector c = new(a.Count);
        for (int i = 0; i < c.Count; i++)
        {
            c.Value[i] = func(a.Value[i]);
        }
        return c;
    }

    private static Vector OprWithVec (Vector a, Vector b, Func<double, double, double> func)
    {
        if (a.Count != b.Count) Interrupts.Vector.SizesDontMatch();

        Vector c = new(a.Count);
        for (int i = 0; i < c.Count; i++)
        {
            c.Value[i] = func(a.Value[i], b.Value[i]);
        }
        return c;
    }

    private static bool BoolOprWithVec (Vector a, Vector b, Func<double, double, bool> func)
    {
        if (a.Count != b.Count) Interrupts.Vector.SizesDontMatch();

        for (int i = 0; i < a.Count; i++)
        {
            if (func(a.Value[i], b.Value[i])) return false;
        }

        return true;
    }
}