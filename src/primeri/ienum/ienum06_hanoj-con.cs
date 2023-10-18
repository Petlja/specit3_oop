using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    // rekurzivan metod koji nabraja korake rešenja za problem hanojskih kula
    // metod zapravo generiše kolekciju koraka rešenja
    static IEnumerable<string> Hanoj(int n, string poc, string pom, string kraj)
    {
        if (n > 0)
        {
            foreach (var x in Hanoj(n - 1, poc, kraj, pom))
                yield return x;

            yield return $"Kotur {n} sa {poc} na {kraj}";

            foreach (var x in Hanoj(n - 1, pom, poc, kraj)) 
                yield return x;
        }
    }

    static void Main()
    {
        foreach (string s in Hanoj(4, "A", "B", "C"))
            Console.WriteLine(s);
    }
}