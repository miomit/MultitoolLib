namespace NeoMath;

public class Vector
{
    public double[] Value { get; set; }

    public int Count => Value.Length;

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
}