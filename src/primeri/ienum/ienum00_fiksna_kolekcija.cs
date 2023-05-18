using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> f()
    {
        yield return 4;
        yield return 5;
        yield return 5;
        yield return 3;
    }

    static void Main()
    {
        Console.WriteLine("suma: {0}", f().Sum());
        Console.WriteLine("prosek: {0}", f().Average());
        Console.WriteLine("min: {0}", f().Min());

        Console.Write("Elementi:");
        foreach (int x in f())
            Console.Write(" {0}", x);
        Console.WriteLine();
    }
}