using System;

interface INizClanPoClan
{
    public long Sledeci();
}

class AritmetickiNiz : INizClanPoClan
{
    private long a0, d, sled;
    public AritmetickiNiz(long a0, long d)
    {
        this.a0 = a0;
        this.d = d;
        this.sled = a0;
    }
    public long Sledeci() 
    {
        long rez = sled;
        sled += d;
        return rez;
    }
}
class GeometrijskiNiz : INizClanPoClan
{
    private long a0, q, sled;
    public GeometrijskiNiz(long a0, long q)
    {
        this.a0 = a0;
        this.q = q;
        this.sled = a0;
    }
    public long Sledeci()
    {
        long rez = sled;
        sled *= q;
        return rez;
    }
}
class DveAritmProgresije : INizClanPoClan
{
    private long sledA, sledB, da, db;
    private bool naReduJeA;
    public DveAritmProgresije(long a0, long da, long b0, long db)
    {
        this.sledA = a0; this.da = da;
        this.sledB = b0; this.db = db;
        naReduJeA = true;
    }
    public long Sledeci()
    {
        long rez;
        if (naReduJeA) { rez = sledA; sledA += da; }
        else { rez = sledB; sledB += db; }
        naReduJeA = !naReduJeA;

        return rez;
    }
}
class FibonacijevNiz : INizClanPoClan
{
    private long sledeci1, sledeci2;
    public FibonacijevNiz(long a0, long a1)
    {
        this.sledeci1 = a0;
        this.sledeci2 = a1;
    }
    public long Sledeci()
    {
        long rez = sledeci1;
        sledeci1 = sledeci2;
        sledeci2 = sledeci1 + rez;
        return rez;
    }
}

class NaizmenicnoPlusPuta: INizClanPoClan
{
    private long sled, d, q;
    private bool naReduJePlus;
    public NaizmenicnoPlusPuta(long a0, long d, long q)
    {
        this.sled = a0; 
        this.d = d;
        this.q = q;
        naReduJePlus = true;
    }
    public long Sledeci()
    {
        long rez = sled;
        sled = naReduJePlus ? sled + d : sled * q;
        naReduJePlus = !naReduJePlus;
        return rez;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Dobijaces redom clanove nekog pravilnog niza,");
        Console.WriteLine("pokusaj da pogodis sledeci element");
        Console.WriteLine("\tPritisni 'Enter' za novi element istog niza");
        Console.WriteLine("\tPritisni '-' i 'Enter' za novi niz");
        Console.WriteLine("\tPritisni '--' i 'Enter' za izlazak iz programa");
        bool kraj = false;
        string unos = "";
        Random rnd = new Random();
        while (!kraj)
        {
            if (unos == "--")
                break;

            Console.WriteLine("Pocinje novi niz");
            bool pogodio = false;
            INizClanPoClan niz = null;
            int vrstaNiza = rnd.Next(5); // biramo jedan od 5 tipova niza
            switch (vrstaNiza)
            {
                case 0:
                    niz = new AritmetickiNiz(rnd.Next(1, 10), rnd.Next(3, 9));
                    break;
                case 1:
                    niz = new GeometrijskiNiz(rnd.Next(1, 5), rnd.Next(2, 5));
                    break;
                case 2:
                    long a1 = rnd.Next(1, 4);
                    long a2 = rnd.Next((int)a1, 6);
                    niz = new FibonacijevNiz(a1, a2);
                    break;
                case 3:
                    long db = rnd.Next(-3, 3);
                    if (db == 0) db++;
                    niz = new DveAritmProgresije(rnd.Next(3, 7),
                        rnd.Next(2, 5), rnd.Next(45, 51), db);
                    break;
                case 4:
                    niz = new NaizmenicnoPlusPuta(rnd.Next(1, 10),
                        rnd.Next(3, 7), rnd.Next(2, 5));
                    break;
            }
            long novi = niz.Sledeci();
            while (!pogodio)
            {
                Console.Write("Novi element je {0}, pogadjaj sledeci ", novi);
                novi = niz.Sledeci();

                unos = Console.ReadLine();
                if (unos == "") // Enter
                    continue;

                if (unos == "-" || unos == "--")
                    break;

                pogodio = (long.Parse(unos) == novi);
            }
            if (pogodio)
                Console.WriteLine("Bravo!");
            else if (unos == "-")
            {
                Console.Write("Steta, evo ti jos nekoliko elemenata: {0} ", novi);
                for (int i = 0; i < 5; i++)
                    Console.Write("{0,7}", niz.Sledeci());

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
