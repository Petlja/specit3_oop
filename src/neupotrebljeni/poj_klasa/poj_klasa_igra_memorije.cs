using System;

class Memorije
{
    private Random rnd;
    private int[] a;
    private bool[] reseno;
    private bool[] otvoreno;
    private int prva;
    private int druga;
    private int brResenih;
    private int brPokusaja;
    public int BrojPokusaja { get { return brPokusaja; } }
    public int BrojPolja { get { return a.Length; } }
    public Memorije(int brPolja)
    {
        brPolja = brPolja / 2 * 2; // da bude paran
        rnd = new Random();
        a = new int[brPolja];
        reseno = new bool[brPolja];
        otvoreno = new bool[brPolja];
        for (int i = 0; i < brPolja; i += 2)
        {
            a[i] = i / 2;
            a[i + 1] = i / 2;
        }
        for (int i = brPolja - 1; i > 0; i--)
        {
            int k = rnd.Next(i + 1);
            int p = a[i]; a[i] = a[k]; a[k] = p;
        }
    }
    public string this[int i]
    {
        get
        {
            if (reseno[i] || otvoreno[i])
                return a[i].ToString();
            else
                return "-";
        }
    }
    public void PrvoPolje(int i)
    {
        otvoreno[prva] = false;
        otvoreno[druga] = false;
        otvoreno[i] = true;
        prva = i;
    }
    public bool DrugoPolje(int i)
    {
        brPokusaja++;
        if (i != prva && a[i] == a[prva])
        {
            reseno[prva] = true;
            reseno[i] = true;
            brResenih += 2;
            return (brResenih == reseno.Length);
        }
        else
        {
            otvoreno[i] = true;
            druga = i;
            return false;
        }
    }
}
class Program
{
    static bool resio = false;
    static void Prikazi(Memorije igra)
    {
        Console.Clear();
        if (!resio)
            Console.WriteLine("Pokusaj br. {0}", igra.BrojPokusaja + 1);

        for (int i = 0; i < igra.BrojPolja; i++)
            Console.Write("{0,3}", igra[i]);
        Console.WriteLine();
        for (int i = 0; i < igra.BrojPolja; i++)
            Console.Write("{0,3}", (char)('a' + i));
        Console.WriteLine();

        if (resio)
            Console.WriteLine("Reseno iz {0} pokusaja.", igra.BrojPokusaja);
    }
    static void Main(string[] args)
    {
        Console.Write("Sa koliko polja zelis da igras (unesi paran broj)? ");
        int brPolja = int.Parse(Console.ReadLine());
        Memorije igra = new Memorije(brPolja);
        while (!resio)
        {
            Prikazi(igra);
            Console.Write("Prvo polje: ");
            int i1 = Console.ReadLine().ToLower()[0] - 'a';
            igra.PrvoPolje(i1);
            Prikazi(igra);
            Console.Write("Drugo polje: ");
            int i2 = Console.ReadLine().ToLower()[0] - 'a';
            resio = igra.DrugoPolje(i2);
        }
        Prikazi(igra);
        Console.WriteLine("Bravo!");
    }
}
