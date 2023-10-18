using System;
using System.Collections.Generic;

public class LinFun
{
    private double k; // linearni koeficijent
    private double n; // slobodan koeficijent

    public LinFun(double k, double n)
    {
        this.k = k;
        this.n = n;
    }

    // izračunavanje vrednosti funkcije za dato x
    public double Value(double x) 
    {
        return k*x+n;
    }

    // operator sabiranja linearnih funkcija
    public static LinFun operator +(LinFun a, LinFun b)
    {
        return new LinFun(a.k + b.k, a.n + b.n);
    }

    // metod vraća funkciju koja predstavlja kompoziciju dve date funkcije
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
            LinFun f1 = new LinFun(2, 3); // f1(x) = 2x+3
            LinFun f2 = new LinFun(3, 5); // f1(x) = 3x+5
            LinFun f3 = f1 + f2; // sabiranje funkcija
            LinFun f4 = LinFun.Compose(f1, f2); // slaganje funkcija
            
            // preikaz vredxnosti f3(x) i f4(x) za razne x
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
