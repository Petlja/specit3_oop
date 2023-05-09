using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main(String[] args)
    {
        // nekoliko razlicitih vrsta kolekcija
        Stack<int> s = new Stack<int>(); s.Push(1); s.Push(2); s.Push(3);
        Queue<int> q = new Queue<int>(); q.Enqueue(1); q.Enqueue(2); q.Enqueue(3);
        SortedSet<int> s2 = new SortedSet<int>() { 1, 2, 3 };
        List<int> l = new List<int>() { 1, 2, 3 };
        int[] a = new int[] { 1, 2, 3 };

        // uniformna upotreba kolekcija, putem interfejsa IEnumerable
        var kolekcije = new List<IEnumerable<int>> { s, q, s2, l, a };
        foreach (IEnumerable<int> e in kolekcije)
        {
            Console.WriteLine("Tip kolekcije: {0}", e.GetType());
            Console.Write("Iteriranje kroz kolekciju pomocu enumeratora:");
            using (var enumerator = e.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    Console.Write(" {0}", enumerator.Current);
            }
            Console.WriteLine();

            Console.Write("Iteriranje kroz kolekciju pomocu naredbe foreach:");
            foreach (int x in e)
                Console.Write(" {0}", x);
            Console.WriteLine();

            Console.WriteLine("S={0}, N={1}, avg={2}, prvi={3}, poslednji={4}, min={5}, max={6}", 
                e.Sum(), e.Count(), e.Average(), e.First(), e.Last(), e.Min(), e.Max());

            var neparni = e.Where(x => x % 2 != 0);
            Console.Write("Neparni elementi:");
            foreach (long x in neparni)
                Console.Write(" {0}", x);
            Console.WriteLine();

            Console.WriteLine("------------------");
        }
    }
}
