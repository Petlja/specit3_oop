using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Fib : IEnumerable<int>
    {
        private int first, second, max;

        public Fib(int f1, int f2, int n)
        {
            first = f1;
            second = f2;
            max = n;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new FibEnumerator(this);
        }

        // klasa FibEnumerator je privatna unutar klase Fib
        // pa ima pravo da pristupa privatnim poljima klase Fib
        private class FibEnumerator : IEnumerator<int>
        {
            private Fib instance;
            private int current, next;
            public FibEnumerator(Fib inst)
            {
                this.instance = inst;
                current = instance.first;
                next = instance.second;
            }
            int IEnumerator<int>.Current
            {
                get { return current; }
            }

            public object Current { get { throw new NotImplementedException(); } }

            public bool MoveNext()
            {
                int next2 = current + next;
                current = next;
                next = next2;
                return (current <= instance.max);
            }

            public void Reset()
            {
                current = instance.first;
                next = instance.second;
            }

            public void Dispose()
            {
                return;
            }
        }
    }

    public static void Main(String[] args)
    {
        // nekoliko razlicitih vrsta kolekcija
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
            foreach (int x in neparni)
                Console.Write(" {0}", x);
            Console.WriteLine();

            Console.WriteLine("------------------");
        }
    }
}
