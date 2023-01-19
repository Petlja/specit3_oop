/*
Napisati klasu Minolovac, tako da program moze da se izvrsi.
Potrebni delovi su:
- enum StanjeIgre 
- konstruktor sa celobrojnom matricom kao argumentom
- indekser koji vraca karakter
- celobrojna svojstva BrRedova, BrKolona, BrMina, BrZastavica 
- metodi 
    void Markiraj(int i, int j)
    void Razmarkiraj(int i, int j)
    StanjeIgre Otvori(int i, int j)

Napomena: nije potrebno da se u klasi rekurzivno otvaraju susedna polja
jer to radi metod 'OtvoriRekurzivno' klase Program.
-------------------
Tokom igre, komanda se zadaje kucanjem jednog od slova 'o', 'x', '.', pa razmak, pa polje.
Polje se zadaje slovom kolone i brojem reda, npr 'a3'
- komanda 'o' sluzi da se otvori polje
- komanda 'x' sluzi da se markira polje
- komanda '.' sluzi da se razmarkira polje

Moguce komande su 
o c4
x f1
itd.
*/
using System;

class Minolovac
{
}

class Program
{
    static void Prikazi(Minolovac igra)
    {
        Console.WriteLine("Broj zastavica: {0}/{1}", igra.BrZastavica, igra.BrMina);
        for (int red = 0; red < igra.BrRedova; red++)
        {
            Console.Write("{0} ", red + 1);
            for (int kol = 0; kol < igra.BrKolona; kol++)
                Console.Write(igra[red, kol]);

            Console.WriteLine();
        }
        Console.Write("  ");
        for (int kol = 0; kol < igra.BrKolona; kol++)
            Console.Write((char)('a' + kol));
        Console.WriteLine();
        Console.WriteLine();
    }
    static Minolovac.StanjeIgre OtvoriRekurzivno(Minolovac igra, int i, int j)
    {
        Minolovac.StanjeIgre stanje = igra.Otvori(i, j);
        if (igra[i, j] == '0')
        {
            int redPoc = Math.Max(0, i - 1);
            int redKraj = Math.Min(igra.BrRedova - 1, i + 1);
            int kolPoc = Math.Max(0, j - 1);
            int kolKraj = Math.Min(igra.BrKolona - 1, j + 1);
            for (int ii = redPoc; ii <= redKraj; ii++)
                for (int jj = kolPoc; jj <= kolKraj; jj++)
                    if (igra[ii, jj] == '.' || igra[ii, jj] == 'x')
                    {
                        if (OtvoriRekurzivno(igra, ii, jj) == Minolovac.StanjeIgre.Pobeda)
                            return Minolovac.StanjeIgre.Pobeda;
                    }
        }
        return stanje;
    }
    static void Main(string[] args)
    {
        int[,] a = {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0 },
        };
        Minolovac igra = new Minolovac(a);

        Minolovac.StanjeIgre stanje = Minolovac.StanjeIgre.UToku;
        while (stanje == Minolovac.StanjeIgre.UToku)
        {
            Console.Clear();
            Prikazi(igra);
            Console.Write("Komanda: ");
            string[] unos = Console.ReadLine().Split();
            string komanda = unos[0];
            int red = unos[1][1] - '1';
            int kol = unos[1][0] - 'a';
            if (komanda == "o")
            {
                stanje = OtvoriRekurzivno(igra, red, kol);
                Prikazi(igra);
            }
            else if (komanda == "x")
            {
                igra.Markiraj(red, kol);
                Prikazi(igra);
            }
            else if (komanda == ".")
            {
                igra.Razmarkiraj(red, kol);
                Prikazi(igra);
            }
        }
        Console.Clear();
        Prikazi(igra);
        if (stanje == Minolovac.StanjeIgre.Pobeda)
            Console.WriteLine("Bravo!");
        else
            Console.WriteLine("Bum!");
    }
}
