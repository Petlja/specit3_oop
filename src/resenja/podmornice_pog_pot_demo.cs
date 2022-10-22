using System;
using System.Collections.Generic;

class Podmornice
{
    public enum Status { Neispravan, Promasaj, Pogodjen, Potopljen, Pobedjen };
    //...
}

class Program
{
    static void Prikaz(Podmornice igra)
    {
        Console.Clear();
        for (int r = igra.BrojRedova - 1; r >= 0; r--)
        {
            Console.Write("{0,2} ", r + 1);
            for (int k = 0; k < igra.BrojKolona; k++)
                Console.Write(igra[r, k]);
            Console.WriteLine();
        }
        Console.Write("   ");
        for (int k = 0; k < igra.BrojKolona; k++)
            Console.Write((char)('A' + k));
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        string[] a =
        {
            "0100000100",
            "0000000000",
            "0000000111",
            "0011000000",
            "0000001000",
            "0000001001",
            "0001000001",
            "1001000001",
            "0001000001",
            "1000001100",
        };

        Podmornice igra = new Podmornice(a);
        bool kraj = false;
        while (!kraj)
        {
            Prikaz(igra);
            Console.WriteLine();
            Console.Write("pogadjaj: ");
            try
            {
                string ulaz = Console.ReadLine().ToUpper();
                int r = int.Parse(ulaz.Substring(1)) - 1;
                int k = ulaz[0] - 'A';
                Podmornice.Status status = igra.Pokusaj(r, k);
                if (status == Podmornice.Status.Pobedjen)
                    kraj = true;
            }
            catch (Exception e) { }
        }
        Prikaz(igra);
        Console.WriteLine("Bravo!");
    }
}
