using System;
using System.Collections;
using System.Collections.Generic;

public class SparseArray
{
    private Dictionary<ulong, double> a = new Dictionary<ulong, double>();
    private double d; // default value
    public SparseArray() { d = 0; }
    private SparseArray(double c) { d = c; }
    static public SparseArray operator +(SparseArray a, SparseArray b) 
    {
        SparseArray ret = new SparseArray(a.d + b.d);

        foreach (KeyValuePair<ulong, double> kv in a.a)
            ret.a[kv.Key] = kv.Value + b.d;

        double y;
        foreach (KeyValuePair<ulong, double> kv in b.a)
        {
            if (ret.a.TryGetValue(kv.Key, out y))
                ret.a[kv.Key] = y + kv.Value - b.d;
            else
                ret.a[kv.Key] = kv.Value + a.d;
        }
        return ret;
    }
    public static implicit operator SparseArray(double v)
    {
        return new SparseArray(v);
    }
    public double this[ulong i]
    {
        get
        {
            if (a.ContainsKey(i))
                return a[i];
            else
                return d;
        }
        set { a[i] = value; }
    }
}
class Program
{
    static void Main(string[] args)
    {
        SparseArray x = new SparseArray(); x[2] = 3; x[3] = 7;
        SparseArray y = new SparseArray(); y[2] = 1; y[4] = 9;
        SparseArray z = 2 + x + y + 3;
        Console.WriteLine(z[1]);
        Console.WriteLine(z[2]);
        Console.WriteLine(z[3]);
        Console.WriteLine(z[4]);
    }
}
