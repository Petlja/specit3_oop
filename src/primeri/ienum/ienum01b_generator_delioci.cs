using System;
using System.Collections;

class Program
{
    // primer rekurzivnog metoda koji generiše kolekciju
    static IEnumerable ProstiDelioci(int n, int d)
    {
        for (int i = d; i * i <= n; i++)
        {
            if (n % i == 0) 
            {
                // vrati i
                yield return i;

                // vrati sve delioce od n/i, počev od i
                foreach (var x in ProstiDelioci(n / i, i))
                    yield return x;

                // proglasi kraj kolekcije
                yield break;
            }
        }
        yield return n;
    }
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        foreach (var d in ProstiDelioci(n, 2)) 
            Console.WriteLine(d);
    }
}
