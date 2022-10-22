using System;
using System.Collections.Generic;

public class Vesala
{
    //...
}

class Program
{
    static void Prikazi(Vesala igra)
    {
        Console.Clear();
        Console.WriteLine(igra.Stanje);
        Console.WriteLine("Pogresna slova: {0}", igra.PokusanaSlova);
        Console.WriteLine("Preostalo zivota: {0}", igra.PreostaloZivota);
    }
    static void Main(string[] args)
    {
        Console.Write("Zadavac neka unese tekst ");
        string pojam = Console.ReadLine().ToUpper();
        Vesala igra = new Vesala(pojam, 6);
        bool kraj = false;
        Prikazi(igra);
        while (!kraj)
        {
            Console.Write("Pogadjaj slovo ");
            char c = Console.ReadLine().ToUpper()[0];
            kraj = igra.Pokusaj(c);
            Prikazi(igra);
        }
        if (igra.PreostaloZivota > 0)
            Console.WriteLine("Bravo");
        else
            Console.WriteLine("Resenje: {0}", pojam);
    }
}
