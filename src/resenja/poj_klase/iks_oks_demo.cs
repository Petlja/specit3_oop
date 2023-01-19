using System;
using System.Collections.Generic;

public class IksOks
{
    //...
}

class Program
{
    static void Prikazi(IksOks igra)
    {
        Console.Clear();
        Console.WriteLine("1 {0}|{1}|{2}", igra[0,0], igra[0, 1], igra[0, 2]);
        Console.WriteLine("  -+-+-");
        Console.WriteLine("2 {0}|{1}|{2}", igra[1, 0], igra[1, 1], igra[1, 2]);
        Console.WriteLine("  -+-+-");
        Console.WriteLine("3 {0}|{1}|{2}", igra[2, 0], igra[2, 1], igra[2, 2]);
        Console.WriteLine();
        Console.WriteLine("  A B C");
    }
    static void Main(string[] args)
    {
        IksOks igra = new IksOks();
        Prikazi(igra);
        string naPotezu = "O";
        while (!igra.Kraj)
        {
            if (naPotezu == "X")
                naPotezu = "O";
            else
                naPotezu = "X";

            bool potezOdigran = false;
            while (!potezOdigran)
            {
                Console.Write("Igrac {0} - polje? ", naPotezu);
                string s = Console.ReadLine().ToUpper();
                int i = s[1] - '1';
                int j = s[0] - 'A';
                potezOdigran = igra.Potez(i, j, naPotezu == "X");
                Prikazi(igra);
            }
        }
        if(igra.Pobeda)
            Console.WriteLine("Igrac {0} je podedio.", naPotezu);
        else
            Console.WriteLine("Nereseno.");
    }
}
