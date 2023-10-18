using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        HashSet<string> razliciteReci = new HashSet<string>();
        string[] sveReci = Console.ReadLine().Split();
        foreach (string rec in sveReci)
            razliciteReci.Add(rec);

        // ako je broj unetih reci jednak broju elemenata skupa,znači da 
        // nije bilo ponavljanja elemenata (jer tada bi skup bio manji),
        // pa su sve unete reči različite.
        if (sveReci.Length == razliciteReci.Count)
            Console.WriteLine("Sve reci su razlicite.");
        else
            Console.WriteLine("Neke reci su jednake.");
    }
}
