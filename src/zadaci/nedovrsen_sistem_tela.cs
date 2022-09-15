using System;
using System.Collections.Generic;

public class Telo
{
    private double m, x, y, z, vx, vy, vz;
    public Telo(double m, double x, double y, double z)
    {
        if (m <= 0)
            throw new Exception("Greska, negativna masa");

        this.m = m;
        this.x = x; this.y = y; this.z = z;
        this.vx = 0; this.vy = 0; this.vz = 0;
    }
    public double M { get { return m; } }
    public double X { get { return x; } }
    public double Y { get { return y; } }
    public double Z { get { return z; } }
    public double VX { get { return vx; } }
    public double VY { get { return vy; } }
    public double VZ { get { return vz; } }
}

public class SistemTela
{
    private const double G = 6.67430e-11;
    private List<Telo> tela = new List<Telo>();
    public void DodajTelo(Telo t)
    {
        tela.Add(t);
    }
    public void Reset()
    {
        tela.Clear();
    }
    public void Tik(double t)
    {
        var sile = new List<Tuple<double, double, double>>();
        for (int i = 0; i < tela.Count; i++)
        {
            double fx = 0, fy = 0, fz = 0;
            for (int j = 0; j < tela.Count; j++)
            {
                if (i != j)
                {
                    double dx = tela[j].X - tela[i].X;
                    double dy = tela[j].Y - tela[i].Y;
                    double dz = tela[j].Z - tela[i].Z;
                    double dd = dx * dx + dy * dy + dz * dz;
                    double F = G * tela[i].M * tela[j].M / dd;
                    double d = Math.Sqrt(dd);
                    fx += F * dx / d;
                    fy += F * dy / d;
                    fz += F * dz / d;
                }
            }
            sile.Add(new Tuple<double, double, double>(fx, fy, fz));
        }
        for (int i = 0; i < tela.Count; i++)
        {
            //... pomeri tela
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            SistemTela st = new SistemTela();
            st.DodajTelo(new Telo(1, 1, 1, 10));
            st.DodajTelo(new Telo(1, 2, 3, 10));

            Console.WriteLine("...");
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
