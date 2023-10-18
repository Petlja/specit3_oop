using System;
using System.Collections.Generic;

class Program
{
    // metod F1 vraća celu listu
    static IEnumerable<int> F1() { return new List<int> { 1, 2, 3 }; }
    
    // metod F2 vraća ceo niz
    static IEnumerable<int> F2() { return new int[] { 4, 5, 6 }; }
    
    // metod F3 vraća kolekciju član po član
    static IEnumerable<int> F3() 
    {
        yield return 7;
        yield return 8;
        yield return 9;
    }

    static void Main()
    {
        // svaki od ova 3 metoda može da se koristi na isti način
        foreach (int x in F1())
            Console.Write(x + " ");

        foreach (int x in F2())
            Console.Write(x + " ");

        foreach (int x in F3())
            Console.Write(x + " ");
    }
}