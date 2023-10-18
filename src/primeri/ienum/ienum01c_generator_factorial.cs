using System;
using System.Collections.Generic;
class Program
{
    // metod koji generiše kolekiju, što znači da on 
    // izračunva i vraća elemente kolekcije "u letu" 
    // tj. ne zauzima prostor za celu kolekciju
    static IEnumerable<long> FaktorijeliDo(long max)
    {
        // pripremi prvi broj        
        long n = 1, f = 1;
        while (f <= max)
        {
            yield return f;
            // pripremi sledeći broj
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