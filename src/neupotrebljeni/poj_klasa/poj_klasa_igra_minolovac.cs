using System;

class Minolovac
{
    public enum StanjeIgre { Pobeda, Poraz, UToku }
    private const int Zatvoreno = -3;
    private const int Zastavica = -2;
    private const int OtvMina = -1;
    private bool[,] mina;
    private int[,] tabla;
    private int brRedova;
    private int brKolona;
    private int brMina;
    private int brZastavica;
    private int brOtvPolja;

    public Minolovac(int[,] a)
    {
        brRedova = a.GetLength(0);
        brKolona = a.GetLength(1);
        brOtvPolja = 0;
        brMina = 0;
        mina = new bool[brRedova, brKolona];
        tabla = new int[brRedova, brKolona];
        for (int r = 0; r < brRedova; r++)
        {
            for (int k = 0; k < brKolona; k++)
            {
                mina[r, k] = (a[r, k] > 0);
                tabla[r, k] = Zatvoreno;
                if (mina[r, k])
                    brMina++;
            }
        }
    }
    public char this[int i, int j]
    {
        get
        {
            //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
            //    return ' ';
            if (tabla[i, j] == Zatvoreno)
                return '.';
            if (tabla[i, j] == Zastavica)
                return 'x';
            if (tabla[i, j] == OtvMina)
                return 'B';

            return (char)('0' + tabla[i, j]);
        }
    }
    public int BrRedova { get { return brRedova; } }
    public int BrKolona { get { return brKolona; } }
    public int BrMina { get { return brMina; } }
    public int BrZastavica { get { return brZastavica; } }
    public void Markiraj(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return;
        if (tabla[i, j] == Zatvoreno)
        {
            tabla[i, j] = Zastavica;
            brZastavica++;
        }
    }
    public void Razmarkiraj(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return;
        if (tabla[i, j] == Zastavica)
        {
            tabla[i, j] = Zatvoreno;
            brZastavica--;
        }
    }
    public StanjeIgre Otvori(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return StanjeIgre.UToku;

        if (tabla[i, j] != Zastavica && tabla[i, j] != Zatvoreno)
            return StanjeIgre.UToku;

        if (mina[i, j])
        {
            tabla[i, j] = OtvMina;
            return StanjeIgre.Poraz;
        }

        int brSusednihMina = 0;
        int redPoc = Math.Max(0, i - 1);
        int redKraj = Math.Min(brRedova - 1, i + 1);
        int kolPoc = Math.Max(0, j - 1);
        int kolKraj = Math.Min(brKolona - 1, j + 1);
        for (int r = redPoc; r <= redKraj; r++)
            for (int k = kolPoc; k <= kolKraj; k++)
                if (mina[r, k])
                    brSusednihMina++;

        tabla[i, j] = brSusednihMina;
        brOtvPolja++;
        if (brOtvPolja + brMina == brKolona * brRedova)
            return StanjeIgre.Pobeda;
        else
            return StanjeIgre.UToku;
    }
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
