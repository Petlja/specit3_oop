using System;

class Sijalice
{
    private const int Blok = -2;
    private const int Sijalica = -1;
    private const int Tamno = 0;
    private int[,] tabla;
    private int brRedova;
    private int brKolona;
    private int brTamnihPolja;
    private int brOsvetljenihSijalica;

    public Sijalice(int[,] a)
    {
        brRedova = a.GetLength(0);
        brKolona = a.GetLength(1);
        tabla = new int[brRedova, brKolona];
        brTamnihPolja = 0;
        for (int r = 0; r < brRedova; r++)
        {
            for (int k = 0; k < brKolona; k++)
            {
                if (a[r, k] > 0)
                    tabla[r, k] = Blok;
                else
                {
                    tabla[r, k] = 0;
                    brTamnihPolja++;
                }
            }
        }
    }
    public char this[int i, int j]
    {
        get
        {
            //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
            //    return ' ';
            if (tabla[i, j] == Blok)
                return '█';
            if (tabla[i, j] == Sijalica)
                return 'X';
            if (tabla[i, j] == Tamno)
                return '.';
            else //if (tabla[i, j]  > 0)
                return 'o';
        }
    }
    public int BrRedova { get { return brRedova; } }
    public int BrKolona { get { return brKolona; } }
    public void Stavi(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return;
        if (tabla[i, j] != Blok)
        {
            brOsvetljenihSijalica += tabla[i, j];
            if (tabla[i, j] == Tamno) brTamnihPolja--;
            tabla[i, j] = Sijalica;
            for (int i1 = i + 1; i1 < brRedova; i1++)
            {
                if (tabla[i1, j] == Blok) break;
                if (tabla[i1, j] == Sijalica) break;
                if (tabla[i1, j] == 0) brTamnihPolja--;
                tabla[i1, j]++;
            }
            for (int i1 = i - 1; i1 >= 0; i1--)
            {
                if (tabla[i1, j] == Blok) break;
                if (tabla[i1, j] == Sijalica) break;
                if (tabla[i1, j] == 0) brTamnihPolja--;
                tabla[i1, j]++;
            }
            for (int j1 = j + 1; j1 < brKolona; j1++)
            {
                if (tabla[i, j1] == Blok) break;
                if (tabla[i, j1] == Sijalica) break;
                if (tabla[i, j1] == 0) brTamnihPolja--;
                tabla[i, j1]++;
            }
            for (int j1 = j - 1; j1 >= 0; j1--)
            {
                if (tabla[i, j1] == Blok) break;
                if (tabla[i, j1] == Sijalica) break;
                if (tabla[i, j1] == 0) brTamnihPolja--;
                tabla[i, j1]++;
            }
        }
    }
    public void Skloni(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return;
        if (tabla[i, j] == Sijalica)
        {
            tabla[i, j] = Tamno;
            for (int i1 = i + 1; i1 < brRedova; i1++)
            {
                if (tabla[i1, j] == Blok) break;
                if (tabla[i1, j] == Sijalica) { tabla[i, j]++; break; }
                tabla[i1, j]--;
                if (tabla[i1, j] == 0) brTamnihPolja++;
            }
            for (int i1 = i - 1; i1 >= 0; i1--)
            {
                if (tabla[i1, j] == Blok) break;
                if (tabla[i1, j] == Sijalica) { tabla[i, j]++; break; }
                tabla[i1, j]--;
                if (tabla[i1, j] == 0) brTamnihPolja++;
            }
            for (int j1 = j + 1; j1 < brKolona; j1++)
            {
                if (tabla[i, j1] == Blok) break;
                if (tabla[i, j1] == Sijalica) { tabla[i, j]++; break; }
                tabla[i, j1]--;
                if (tabla[i, j1] == 0) brTamnihPolja++;
            }
            for (int j1 = j - 1; j1 >= 0; j1--)
            {
                if (tabla[i, j1] == Blok) break;
                if (tabla[i, j1] == Sijalica) { tabla[i, j]++; break; }
                tabla[i, j1]--;
                if (tabla[i, j1] == 0) brTamnihPolja++;
            }
            if (tabla[i, j] == 0) brTamnihPolja++;
            brOsvetljenihSijalica -= tabla[i, j];
        }
    }
    public bool Resio
    {
        get { return brOsvetljenihSijalica == 0 && brTamnihPolja == 0; }
    }
}
class Program
{
    static void Prikazi(Sijalice igra)
    {
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
    static void Main(string[] args)
    {
        int[,] a = {
            { 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };
        Sijalice igra = new Sijalice(a);

        while (!igra.Resio)
        {
            Console.Clear();
            Prikazi(igra);
            Console.Write("Komanda: ");
            string[] unos = Console.ReadLine().Split();
            string komanda = unos[0];
            int red = unos[1][1] - '1';
            int kol = unos[1][0] - 'a';
            if (komanda == "x")
            {
                igra.Stavi(red, kol);
                Prikazi(igra);
            }
            else if (komanda == ".")
            {
                igra.Skloni(red, kol);
                Prikazi(igra);
            }
        }
        Console.Clear();
        Prikazi(igra);
        Console.WriteLine("Bravo!");
    }
}
