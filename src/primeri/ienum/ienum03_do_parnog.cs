using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> DoParnog(IEnumerable<int> a)
    {
        foreach (var x in a)
        {
            if (x % 2 == 0)
                yield break;
            else
                yield return x;
        }
    }
    static void Main()
    {
        List<int> parNep = new List<int> { -1, -3, 5, 6, 2, -1, 7, -8 };
        Console.Write("Elementi do prvog parnog u kolekciji su ");
        foreach (var br in DoParnog(parNep))
            Console.Write(" " + br);
        Console.WriteLine(". Njihov zbir je {0}.", DoParnog(parNep).Sum());
    }
}