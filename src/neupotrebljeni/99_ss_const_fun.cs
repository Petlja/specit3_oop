using System;
using System.Collections.Generic;

// skoro svuda konstantna funkcija, izuzeci nabrojani recnikom
public class SSConstFun
{
    private Dictionary<double, double> f = new Dictionary<double, double>();
    private double defaultY;
    private int NumExceptions { get { return f.Count; } }

    public SSConstFun(int n, double defY, double[] x, double[] y)
    {
        defaultY = defY;
        for (int i = 0; i < n; i++)
        {
            f[x[i]] = y[i];
        }
    }
    public double Value(double x) 
    {
        double y;
        if (f.TryGetValue(x, out y))
            return y;
        else
            return defaultY;
    }

    public static SSConstFun operator +(SSConstFun a, SSConstFun b)
    {
        var fun = new SSConstFun(0, a.defaultY + b.defaultY, null, null);
        foreach (KeyValuePair<double, double> kv in a.f)
            fun.f[kv.Key] = kv.Value + b.defaultY;

        double y;
        foreach (KeyValuePair<double, double> kv in b.f)
        {
            if (fun.f.TryGetValue(kv.Key, out y))
                fun.f[kv.Key] = y + kv.Value - b.defaultY;
            else
                fun.f[kv.Key] = kv.Value + a.defaultY;
        }
        return fun;
    }

    public static SSConstFun Compose(SSConstFun a, SSConstFun b)
    {
        int n = b.NumExceptions;
        double[] x = new double[n];
        double[] y = new double[n];
        int i = 0;
        foreach (KeyValuePair<double, double> kv in b.f)
        {
            x[i] = kv.Key;
            y[i] = a.Value(kv.Value);
            i++;
        } 
        return new SSConstFun(n, a.Value(b.defaultY), x, y);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            SSConstFun f1 = new SSConstFun(3, 6, new double[] { 1, 2, 7 }, new double[] { 1, 4, 49 });
            SSConstFun f2 = new SSConstFun(2, 7, new double[] { 3, 4 }, new double[] { 1, 2 });
            SSConstFun f3 = f1 + f2;
            foreach (double x in new double[] { 0, 1, 2, 3, 4, 5, 6 })
                Console.WriteLine("f3({0}) = {1}", x, f3.Value(x));


            SSConstFun f4 = SSConstFun.Compose(f1, f2);
            foreach (double x in new double[] { 0, 1, 2, 3, 4, 5, 6 })
                Console.WriteLine("f4({0}) = {1}", x, f4.Value(x));
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
