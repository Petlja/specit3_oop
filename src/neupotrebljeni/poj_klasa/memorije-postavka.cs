/*
Napisati klasu Memorije, tako da program moze da se izvrsi.
Tokom igre, komanda se zadaje kucanjem malog ili velikog slova ispod crtice.
*/

using System;

class Memorije
{
}
class Program
{
    static bool resio = false;
    static void Prikazi(Memorije igra)
    {
        Console.Clear();
        if (!resio)
            Console.WriteLine("Pokusaj br. {0}", igra.BrojPokusaja + 1);

        for (int i = 0; i < igra.BrojPolja; i++)
            Console.Write("{0,3}", igra[i]);
        Console.WriteLine();
        for (int i = 0; i < igra.BrojPolja; i++)
            Console.Write("{0,3}", (char)('a' + i));
        Console.WriteLine();

        if (resio)
            Console.WriteLine("Reseno iz {0} pokusaja.", igra.BrojPokusaja);
    }
    static void Main(string[] args)
    {
        Console.Write("Sa koliko polja zelis da igras (unesi paran broj)? ");
        int brPolja = int.Parse(Console.ReadLine());
        Memorije igra = new Memorije(brPolja);
        while (!resio)
        {
            Prikazi(igra);
            Console.Write("Prvo polje: ");
            int i1 = Console.ReadLine().ToLower()[0] - 'a';
            igra.PrvoPolje(i1);
            Prikazi(igra);
            Console.Write("Drugo polje: ");
            int i2 = Console.ReadLine().ToLower()[0] - 'a';
            resio = igra.DrugoPolje(i2);
        }
        Prikazi(igra);
        Console.WriteLine("Bravo!");
    }
}
