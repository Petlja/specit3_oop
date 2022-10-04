using System;
using System.Collections.Generic;

class Podmornice
{
    public enum Status { Neispravan, Promasaj, Pogodjen, Potopljen, Pobedjen };
    private struct Pravougaonik 
    { 
        public int r1, k1, r2, k2;
        public Pravougaonik(int i1, int j1, int i2, int j2) 
        {
            r1 = i1; k1 = j1; r2 = i2; k2 = j2;
        }
    }
    private int brRedova;
    private int brKolona;
    private bool[,] zadato;
    private bool[,] gadjano;
    private int brCiljeva;
    Dictionary<int, Pravougaonik> brod;
    Dictionary<Pravougaonik, int> nepogodjeni;

    public Podmornice(string[] a)
    {
        brRedova = a.Length;
        brKolona = a[0].Length;
        zadato = new bool[brRedova, brKolona];
        gadjano = new bool[brRedova, brKolona];
        brod = new Dictionary<int, Pravougaonik>();
        nepogodjeni = new Dictionary<Pravougaonik, int>();
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
        for (int i = 0; i < brRedova; i++)
        {
            for (int j = 0; j < brKolona; j++)
            {
                if (zadato[i, j]
                    && (i == 0 || !zadato[i - 1, j])
                    && (j == 0 || !zadato[i, j - 1])
                    ) 
                {
                    int r1 = i, k1 = j, r2 = i, k2 = j;
                    while (r2 < brRedova - 1 && zadato[r2 + 1, k1]) r2++;
                    while (k2 < brKolona - 1 && zadato[r1, k2 + 1]) k2++;
                    Pravougaonik p = new Pravougaonik(r1, k1, r2, k2);
                    for (int r = r1; r <= r2; r++)
                        for (int k = k1; k <= k2; k++)
                            brod[r * brKolona + k] = p;
                    nepogodjeni[p] = (r2 - r1 + 1) * (k2 - k1 + 1);
                }
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
        nepogodjeni[brod[r * brKolona + k]]--;
        if (brCiljeva == 0)
            return Status.Pobedjen;

        if (nepogodjeni[brod[r * brKolona + k]] == 0)
            return Status.Potopljen;
        else
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

            // polja koja su gadjana i zadata
            if (nepogodjeni[brod[r * brKolona + k]] == 0)
                return 'X'; // potopljen
            else
                return 'x'; // pogodjen
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
            Console.Write("{0,2} ", r+1);
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
        string [] a =
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
