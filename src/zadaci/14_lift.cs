using System;

public class Lift
{
    private uint nosivost;
    private uint masaULiftu;
    private uint sprat;
    private double rad;
    public uint Sprat { get { return sprat; } }
    public double Rad{ get { return rad; } }
    public Lift(uint n) { 
        nosivost = n; 
        masaULiftu = 0;
        sprat = 0;
        rad = 0;
    }
    public void Ulaz(uint m) 
    { 
        masaULiftu += m;
        if (masaULiftu > nosivost)
            throw new Exception("Lift je preopterecen.");
    }
    public void Izlaz(uint m) 
    {
        if (masaULiftu < m)
        {
            string poruka = string.Format("Nemoguc zahtev: izlaz({0}).", m);
            throw new Exception(poruka);
        }
        masaULiftu -= m;
    }
    public void Komanda(uint noviSprat) 
    {
        if (masaULiftu > nosivost)
            throw new Exception("Lift je preopterecen.");

        if (masaULiftu == 0)
            return; // ako je prazan, ignorise komandu

        if (noviSprat > sprat)
            rad += masaULiftu*(noviSprat - sprat);
        else
            rad += 0.5 * masaULiftu * (sprat - noviSprat);

        sprat = noviSprat;
    }
}
class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            Lift lift = new Lift(320);
            lift.Ulaz(200);
            lift.Komanda(3);
            lift.Izlaz(100);
            lift.Komanda(1);

            Console.WriteLine("Sprat: {0}, rad: {1}", lift.Sprat, lift.Rad);
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
