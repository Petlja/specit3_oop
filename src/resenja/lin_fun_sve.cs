using System;
using System.Collections.Generic;

public class LinFun
{
    private double k, n;
    public LinFun(double k, double n)
    {
        this.k = k;
        this.n = n;
    }
    public double Value(double x) 
    {
        return k*x+n;
    }

    public static LinFun operator +(LinFun a, LinFun b)
    {
        return new LinFun(a.k + b.k, a.n + b.n);
    }

    public static LinFun Compose(LinFun a, LinFun b)
    {
        // a(b(x)) = a.k * (b(x)) + a.n
        //         = a.k * (b.k*x+b.n) + a.n 
        //         = a.k*b.k*x + a.k*b.n + a.n 
        return new LinFun(a.k * b.k, a.k * b.n + a.n);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            LinFun f1 = new LinFun(2, 3);
            LinFun f2 = new LinFun(3, 5);
            LinFun f3 = f1 + f2;
            LinFun f4 = LinFun.Compose(f1, f2);
            foreach (double x in new double[] { 0, 1, 2, 3 })
            {
                Console.WriteLine("f3({0}) = {1}, f4({0}) = {2}", 
                    x, f3.Value(x), f4.Value(x));
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
