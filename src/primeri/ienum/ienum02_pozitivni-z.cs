using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> Pozitivni(IEnumerable<int> a)
    {
        // ovde dodati potrebne naredbe
    }
    static void Main()
    {
        List<int> pozNeg = new List<int> { -1, -4, 3, 5, 6, 2, -1, 7, -8 };
        Console.Write("Pozitivni elementi kolekcije su");
        foreach (var br in Pozitivni(pozNeg))
            Console.Write(" " + br);
        Console.Write(". Njihov zbir je {0},", Pozitivni(pozNeg).Sum());
        Console.WriteLine(" a njihov min je {0}.", Pozitivni(pozNeg).Min());
    }
}