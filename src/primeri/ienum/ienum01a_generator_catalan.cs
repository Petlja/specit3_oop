using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    // metod izračunava i vraća Katalanove brojeve
    static IEnumerable<long> Catalan(long max)
    {
        // c(n) = (2n)! / (n! * n! * (n+1))
        // c(n+1) = c(n) * (2n+1) * (2n+2) / ((n+1)*(n+2))

        // pripremi prvi broj
        long n = 1, c = 1; 
        while (c <= max)
        {
            yield return c;
            // pripremi sledeći broj
            c *= (2*n + 1) * (2 * n + 2);
            c /= (n + 1) * (n + 2);
            n++;
        }
    }
    
    static void Main()
    {
        foreach (long c in Catalan(10000))
            Console.Write(c + " ");

        Console.WriteLine();
    }
}