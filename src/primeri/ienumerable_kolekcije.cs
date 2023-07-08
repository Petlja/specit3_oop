using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static IEnumerable<int> F1() { return new Stack<int>(new[] { 4, 3, 5, 1, 2 }); }
    static IEnumerable<int> F2() { return new Queue<int>(new[] { 4, 3, 5, 1, 2 }); }
    static IEnumerable<int> F3() { return new SortedSet<int>() { 4, 3, 5, 1, 2 }; }
    static IEnumerable<int> F4() { return new List<int>() { 4, 3, 5, 1, 2 }; }
    static IEnumerable<int> F5() { return new int[] { 4, 3, 5, 1, 2 }; }

    static void Ispis(IEnumerable<int> e)
    {
        foreach (int x in e)
            Console.Write(" {0}", x);
        Console.WriteLine();
    }
    static void RazneOperacije(IEnumerable<int> e)
    {
        Console.Write("Elementi kolekcije redom:    "); Ispis(e);
        Console.Write("Elementi od kraja ka početku:"); Ispis(e.Reverse());
        Console.Write("Samo neparni elementi:       "); Ispis(e.Where(x => x % 2 != 0));
        Console.WriteLine("S={0}, N={1}, avg={2}, prvi={3}, poslednji={4}, min={5}, max={6}",
            e.Sum(), e.Count(), e.Average(), e.First(), e.Last(), e.Min(), e.Max());
        Console.WriteLine("------------------");
    }
    public static void Main(String[] args)
    {
        // uniformna upotreba različitih kolekcija pomoću interfejsa IEnumerable
        var kolekcije = new List<IEnumerable<int>> { F1(), F2(), F3(), F4(), F5() };
        foreach (IEnumerable<int> e in kolekcije)
            RazneOperacije(e);
    }
}
