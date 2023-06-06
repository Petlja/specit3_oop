using System;
using System.Collections.Generic;

class Program
{
    static IEnumerable<int> F1() { return new List<int> { 1, 2, 3 }; }
    static IEnumerable<int> F2() { return new int[] { 4, 5, 6 }; }
    static IEnumerable<int> F3() 
    {
        yield return 7;
        yield return 8;
        yield return 9;
    }

    static void Main()
    {
        foreach (int x in F1())
            Console.Write(x + " ");

        foreach (int x in F2())
            Console.Write(x + " ");

        foreach (int x in F3())
            Console.Write(x + " ");
    }
}