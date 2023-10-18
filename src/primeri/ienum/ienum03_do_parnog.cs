// primer ilustruje dejstvo naredbe 'yield return'
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> DoParnog(IEnumerable<int> a, IEnumerable<int> b)
    {
        foreach (var x in a)
        {
            if (x % 2 == 0)
                yield break;

            yield return x;
        }
        
        // ako je u kolekciji 'a' bilo parnih brojeva, 
        // sledeće naredbe se neće izvršiti
        foreach (var x in b)
        {
            if (x % 2 == 0)
                yield break;

            yield return x;
        }
    }
    static void Main()
    {
        List<int> parNep = new List<int> { -1, -3, 5, 6, 2, -1, 7, -8 };
        List<int> sviNep = new List<int> { 9, -3, 17, 1 };

        Console.Write("Elementi do prvog parnog su ");
        foreach (var br in DoParnog(parNep, sviNep))
            Console.Write(" " + br);

        Console.WriteLine(". Njihov zbir je {0}.",
            DoParnog(parNep, sviNep).Sum());
    }
}
