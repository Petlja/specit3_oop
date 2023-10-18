// uvodni primer za ilustrovanje naredbe yield return
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    // metod f vraća brojeve 5, 4, 5 jedan po jedan
    static IEnumerable<int> f()
    {
        yield return 5;
        yield return 4;
        yield return 5;
    }

    static void Main()
    {
        // primena agregatnih metoda na vraćenu kolekciju
        Console.WriteLine("suma: {0}", f().Sum());
        Console.WriteLine("prosek: {0}", f().Average());
        Console.WriteLine("min: {0}", f().Min());

        // ispisivanje kolekcije član po član
        Console.Write("Elementi:");
        foreach (int x in f())
            Console.Write(" {0}", x);
        Console.WriteLine();
    }
}
