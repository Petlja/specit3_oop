class Program
{
    static void Main(string[] args)
    {
        Bubanj b = new Bubanj(5);
        Console.WriteLine("Izvucen je broj {0}", b.Izvuci());
        Console.WriteLine(b.Prazan());
        Console.WriteLine("Broj preostalih kuglica: {0}", b.BrojKuglica);
        var serija = b.Izvuci(3);
        Console.Write("Izvuceni su brojevi");
        foreach (int br in serija)
            Console.Write(" {0}", br);
        Console.WriteLine();
        Console.WriteLine("Izvucen je broj {0}", b.Izvuci());
        Console.WriteLine(b.Prazan());
        Console.WriteLine("Broj preostalih kuglica: {0}", b.BrojKuglica);
    }
}
