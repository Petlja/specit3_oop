using System;
class Temperatura
{
    private double t;
    public double C
    {
        get { return t; }
        set { t = value; }
    }
    public double F
    {
        get { return t * 1.8 + 32.0; }
        set { t = (value - 32.0) / 1.8; }
    }
    public double K
    {
        get { return t + 273.15; }
        set { t = value - 273.15; }
    }

    public Temperatura(double temp)
    {
        t = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        Temperatura t1 = new Temperatura(23);
        Console.WriteLine("{0:0.00}C = {1:0.00}F", t1.C, t1.F);
        t1.F = 80;
        Console.WriteLine("{0:0.00}F = {1:0.00}C", t1.F, t1.C);
    }
}
