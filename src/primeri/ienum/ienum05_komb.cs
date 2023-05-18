using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> Kombinacije(int x, int d, int max)
    {
        if (d == 0)
            yield return x;
        else
        {
            for (int i = x % 10 + 1; i <= max; i++)
            {
                foreach (var y in Kombinacije(10 * x + i, d - 1, max))
                    yield return y;
            }
        }
    }
    static void Main()
    {
        foreach (int br in Kombinacije(0, 3, 5))
            Console.Write(br + " ");
        Console.WriteLine();
    }
}