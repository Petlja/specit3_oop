using System;
using System.Collections.Generic;
class Program
{
    static IEnumerable<long> FaktorijeliDo(long max)
    {
        long n = 1, f = 1;
        while (f <= max)
        {
            yield return f;
            n++;
            f *= n;
        }
    }

    static void Main()
    {
        foreach (long x in FaktorijeliDo(10000))
            Console.Write(x + " ");

        Console.WriteLine();
    }
}