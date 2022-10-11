using System;
using System.Collections;
using System.Collections.Generic;

public class SparseArray
{
    private Dictionary<ulong, double> a = new Dictionary<ulong, double>();
    public double this[ulong i]
    {
        get
        {
            if (a.ContainsKey(i))
                return a[i];
            else
                return 0;
        }
        set { a[i] = value; }
    }
}
class Program
{
    static void Main(string[] args)
    {
        SparseArray x = new SparseArray();
        ulong n = 4000000000000;
        x[n]++;
        x[n + 1] = 3;
        Console.WriteLine(x[n]);
        Console.WriteLine(x[n + 1]);
    }
}
