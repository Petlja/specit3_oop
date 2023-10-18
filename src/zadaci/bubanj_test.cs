class Program
{
    static void Main(string[] args)
    {
        Bubanj b = new Bubanj(5);
        // izvlačenje prve kuglice
        Console.WriteLine("Izvucen je broj {0}", b.Izvuci());
        Console.WriteLine(b.Prazan());
        Console.WriteLine("Broj preostalih kuglica: {0}", b.BrojKuglica);

        var serija = b.Izvuci(3); // rezultat izvlačenja tri kuglice
        Console.Write("Izvuceni su brojevi");
        foreach (int br in serija) // ispisivanje izvučenih brojeva
            Console.Write(" {0}", br);
        Console.WriteLine();

        // izvlačenje poslednje kuglice
        Console.WriteLine("Izvucen je broj {0}", b.Izvuci());
        Console.WriteLine(b.Prazan());
        Console.WriteLine("Broj preostalih kuglica: {0}", b.BrojKuglica);
    }
}
