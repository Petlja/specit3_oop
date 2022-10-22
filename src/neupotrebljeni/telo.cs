using System;

public class Telo
{
    private double amax;
    private double x, y, vx, vy, ax, ay;
    public double X { get { return x; } }
    public double Y { get { return y; } }
    public double VX { get { return vx; } }
    public double VY { get { return vy; } }
    public Telo(double maxUbrzanje, double x0, double y0)
    {
        amax = maxUbrzanje;
        x = x0;
        y = y0;
    }
    public double AX
    {
        get { return ax; }
        set { ax = Math.Min(amax, Math.Max(-amax, value)); }
    }
    public double AY
    {
        get { return ay; }
        set { ay = Math.Min(amax, Math.Max(-amax, value)); }
    }
    public void Stani()
    {
        vx = vy = ax = ay = 0.0;
    }
    public void Vreme(double t)
    {
        double vx1 = vx + ax * t;
        double vy1 = vy + ay * t;
        double x1 = x + vx * t + 0.5 * ax * t * t;
        double y1 = y + vy * t + 0.5 * ay * t * t;
        x = x1; y = y1;
        vx = vx1; vy = vy1;
    }
    public override string ToString()
    {
        return string.Format("T({0}, {1}), V=({2}, {3})", X, Y, VX, VY);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Telo telo = new Telo(1, 0, 0);

        telo.AX = 1; telo.Vreme(1);
        Console.WriteLine(telo);
        telo.AX = 0; telo.Vreme(3);
        Console.WriteLine(telo);
        telo.AX = -1; telo.Vreme(1);
        Console.WriteLine(telo);
        telo.AX = 0;
    }
}