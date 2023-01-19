/*
Napisati klasu PogadjanjeBroja, tako da program moze da se izvrsi.
Tokom igre, komanda se zadaje kucanjem malog ili velikog slova ispod crtice.
*/

using System;

class PogadjanjeBroja
{
}

class Program
{
    static void Prikazi(PogadjanjeBroja igra)
    {
        Console.Clear();
        for (int i = 0; i < igra.N; i++)
            Console.Write("{0,4}", igra[i]);
        Console.WriteLine();

        for (int i = 0; i < igra.N; i++)
            Console.Write("{0,4}", (char)('A' + i));
        Console.WriteLine();

        Console.WriteLine("Nadji broj {0}", igra.Zadatak);
    }
    static void Main(string[] args)
    {
        PogadjanjeBroja igra = new PogadjanjeBroja(25);

        bool resio = false;
        while (!resio)
        {
            Prikazi(igra);
            Console.Write("Otvori polje: ");
            char unos = Console.ReadLine().ToLower()[0];
            int i = unos - 'a';
            if (0 <= i && i < 25)
                resio = igra.Otvori(i);
        }
        Console.Clear();
        Prikazi(igra);
        Console.WriteLine("Bravo!");
    }
}
