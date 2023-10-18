using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    // metod koji filtrira zadatu kolekciju 
    // (ovakav filtar može da se kreira koristeći metod 'where')
    static IEnumerable<int> Pozitivni(IEnumerable<int> a)
    {
        foreach (var x in a)
            if (x > 0)
                yield return x;
    }
    static void Main()
    {
        List<int> celi = new List<int> { -1, -4, 3, 5, 6, 2, -1, 7, -8 };

        Console.Write("Pozitivni elementi kolekcije su");

        foreach (var br in Pozitivni(celi))
            Console.Write(" " + br);

        Console.Write(". Njihov zbir je {0},", Pozitivni(celi).Sum());

        Console.WriteLine(" a njihov min je {0}.", Pozitivni(celi).Min());
    }
}