using System;

public class Lift
{
    private uint nosivost; // ukupna masa koju lift može da prevozi
    private uint masaULiftu; // ukupna masa tereta (ljudi) u liftu
    private uint sprat; // sprat na kome se trenutno nalazi lift
    private double rad; // ukupan rad izvršen od kreiranja lifta

    public uint Sprat { get { return sprat; } }

    public double Rad{ get { return rad; } }

    // Konstruktor - lift je opisan svojom nosivošću
    public Lift(uint n) { 
        nosivost = n; 
        masaULiftu = 0;
        sprat = 0;
        rad = 0;
    }
    
    // Opterećivanje lifta sa novih m kilograma
    public void Ulaz(uint m)
    { 
        masaULiftu += m;
        if (masaULiftu > nosivost)
            throw new Exception("Lift je preopterecen.");
    }

    // Opterećenje lifta se smanjuje za m kilograma
    public void Izlaz(uint m) 
    {
        if (masaULiftu < m)
        {
            string poruka = string.Format("Nemoguc zahtev: izlaz({0}).", m);
            throw new Exception(poruka);
        }
        masaULiftu -= m;
    }

    // Komanda liftu da pređe na zadati sprat
    public void Komanda(uint noviSprat) 
    {
        if (masaULiftu > nosivost)
            throw new Exception("Lift je preopterecen.");

        if (masaULiftu == 0)
            return; // ako je prazan, ignorise komandu

        // ažuriranje izvršenog rada
        if (noviSprat > sprat)
            rad += masaULiftu*(noviSprat - sprat);
        else
            rad += 0.5 * masaULiftu * (sprat - noviSprat);

        // ažuriranje trenutnog sprata
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
            Lift lift = new Lift(320);  // novi lift nosivosti 320Kg
            lift.Ulaz(200);             // u kabinu ulaze ljudi mase 200Kg
            lift.Komanda(3);            // prelazak na treći sprat
            lift.Izlaz(100);            // iz kabine izlaze ljudi mase 100Kg
            lift.Komanda(1);            // prelazak na prvi sprat

            Console.WriteLine("Sprat: {0}, rad: {1}", lift.Sprat, lift.Rad);
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
