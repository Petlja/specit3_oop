using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main(String[] args)
    {
        List<int> l = new List<int>() { 1, 2, 3 };
        
        // Iteriranje kroz kolekciju pomoću enumeratora
        Console.Write("Iteriranje kroz kolekciju pomocu enumeratora:");
        using (var enumerator = l.GetEnumerator())
        {
            while (enumerator.MoveNext())
                Console.Write(" {0}", enumerator.Current);
        }
        Console.WriteLine();

        // Iteriranje kroz kolekciju pomoću naredbe foreach
        Console.Write("Iteriranje kroz kolekciju pomocu naredbe foreach:");
        foreach (int x in l)
            Console.Write(" {0}", x);
        Console.WriteLine();

        // implicitno iteriranje pomoću podržanih agregatnih metoda
        Console.WriteLine("S={0}, N={1}, avg={2}, prvi={3}, poslednji={4}, min={5}, max={6}", 
            l.Sum(), l.Count(), l.Average(), l.First(), l.Last(), l.Min(), l.Max());

        // filtriranje nabrojive kolekcije pomoću metoda where
        var neparni = l.Where(x => x % 2 != 0);
        Console.Write("Neparni elementi:");
        foreach (int x in neparni)
            Console.Write(" {0}", x);
        Console.WriteLine();
    }
}
