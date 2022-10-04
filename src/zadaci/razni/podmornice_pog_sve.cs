using System;
using System.Collections.Generic;

class Podmornice
{
    public enum Status { Neispravan, Promasaj, Pogodjen, Pobedjen };
    private int brRedova;
    private int brKolona;
    private bool[,] zadato;
    private bool[,] gadjano;
    private int brCiljeva;

    public Podmornice(string[] a)
    {
        brRedova = a.Length;
        brKolona = a[0].Length;
        zadato = new bool[brRedova, brKolona];
        gadjano = new bool[brRedova, brKolona];
        brCiljeva = 0;
        for (int i = 0; i < brRedova; i++)
        {
            for (int j = 0; j < brKolona; j++)
            {
                zadato[i, j] = (a[i][j] == '1');
                if (zadato[i, j])
                    brCiljeva++;
            }
        }
    }
    public Status Pokusaj(int r, int k)
    {
        if (r < 0 || r >= brRedova || k < 0 || k >= brKolona)
            return Status.Neispravan;

        gadjano[r, k] = true;
        if (!zadato[r, k])
            return Status.Promasaj;

        brCiljeva--;
        if (brCiljeva == 0)
            return Status.Pobedjen;

        return Status.Pogodjen;
    }

    public char this[int r, int k]
    {
        get
        {
            if (!gadjano[r, k])
                return '.'; // nepoznato
            if (!zadato[r, k])
                return 'o'; // promasaj

            return 'X'; // pogodjen
        }
    }
    public int BrojRedova { get { return brRedova; } }
    public int BrojKolona { get { return brKolona; } }
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
