using System;

class PogadjanjeBroja
{
    private Random rnd;
    private int[] a;
    private bool[] vidljiv;
    private int iZamisljen;
    public PogadjanjeBroja(int n)
    {
        rnd = new Random();
        a = new int[n];
        vidljiv = new bool[n];
        iZamisljen = rnd.Next(n);
        for (int i = 0; i < n; i++)
            a[i] = rnd.Next(1000);

        Array.Sort(a);
    }
    public int N { get { return a.Length; } }
    public int Zadatak { get { return a[iZamisljen]; } }
    public string this[int i]
    {
        get
        {
            if (vidljiv[i]) return a[i].ToString();
            else return ".";
        }
    }
    public bool Otvori(int i)
    {
        vidljiv[i] = true;
        return (a[i] == a[iZamisljen]);
    }
}
class Program
{
    static void Prikazi(PogadjanjeBroja igra)
    {
        Console.Clear();
        for (int i = 0; i < igra.N; i++)
            Console.Write("{0,4}", igra[i]);
        Console.WriteLine();

        for (int i = 0; i < igra.N; i++)
            Console.Write("{0,4}", (char)('A' + i));
        Console.WriteLine();

        Console.WriteLine("Nadji broj {0}", igra.Zadatak);
    }
    static void Main(string[] args)
    {
        PogadjanjeBroja igra = new PogadjanjeBroja(25);

        bool resio = false;
        while (!resio)
        {
            Prikazi(igra);
            Console.Write("Otvori polje: ");
            char unos = Console.ReadLine().ToLower()[0];
            int i = unos - 'a';
            if (0 <= i && i < 25)
                resio = igra.Otvori(i);
        }
        Console.Clear();
        Prikazi(igra);
        Console.WriteLine("Bravo!");
    }
}
