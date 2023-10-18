using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Ilustracija iteriranja kroz različite tipove kolekcija 
// pomoću naredbe yield
class Program
{
    // klasa koja nabraja Fibonačijeve brojeve
    class Fib : IEnumerable<int>
    {
        private int first, second; // prva dva elementa Fibonačivejog niza
        private int current, next; // tekuća dva elementa niza
        private int max; // granica do koje se nabraja

        // konstruktor - kolekcija je zadata sa prva dva člana i granicom
        public Fib(int f1, int f2, int n)
        {
            first = current = f1;
            second = next = f2;
            max = n;
        }
        
        // opšti enumerator objekata nam nije potreban u ovom primeru
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // enumerator celih brojeva
        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            // pripremi tekući i sledeći broj
            current = first;
            next = second;
            while (current <= max)
            {
                yield return current;
                // pripremi novi tekući i sledeći broj
                int next2 = current + next;
                current = next;
                next = next2;
            }
        }
    }
    public static void Main(String[] args)
    {
        // nekoliko različitih vrsta kolekcija
        Stack<int> s = new Stack<int>(); s.Push(1); s.Push(2); s.Push(3);
        Queue<int> q = new Queue<int>(); q.Enqueue(1); q.Enqueue(2); q.Enqueue(3);
        SortedSet<int> s2 = new SortedSet<int>() { 1, 2, 3 };
        List<int> l = new List<int>() { 1, 2, 3 };
        int[] a = new int[] { 1, 2, 3 };
        Fib f = new Fib(1, 2, 1000);

        // uniformna upotreba kolekcija
        var kolekcije = new List<IEnumerable<int>> { s, q, s2, l, a, f };
        foreach (IEnumerable<int> e in kolekcije)
        {
            Console.WriteLine("Tip kolekcije: {0}", e.GetType());
            
            // Iteriranje kroz kolekciju pomoću enumeratora
            Console.Write("Iteriranje kroz kolekciju pomocu enumeratora:");
            using (var enumerator = e.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    Console.Write(" {0}", enumerator.Current);
            }
            Console.WriteLine();

            // Iteriranje kroz kolekciju pomoću naredbe foreach
            Console.Write("Iteriranje kroz kolekciju pomocu naredbe foreach:");
            foreach (int x in e)
                Console.Write(" {0}", x);
            Console.WriteLine();

            // implicitno iteriranje pomoću podržanih agregatnih metoda
            Console.WriteLine("S={0}, N={1}, avg={2}, prvi={3}, poslednji={4}, min={5}, max={6}", 
                e.Sum(), e.Count(), e.Average(), e.First(), e.Last(), e.Min(), e.Max());

            // filtriranje nabrojive kolekcije pomoću metoda where
            var neparni = e.Where(x => x % 2 != 0);
            Console.Write("Neparni elementi:");
            foreach (int x in neparni)
                Console.Write(" {0}", x);
            Console.WriteLine();

            Console.WriteLine("------------------");
        }
    }
}
