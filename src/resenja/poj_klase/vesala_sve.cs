using System;
using System.Collections.Generic;

public class Vesala
{
    private string zadato;
    private string pokusanaSlova;
    private char[] polupogodjeno;
    private int preostaloZivota;
    private int n;

    public Vesala(string pojam, int brZivota)
    {
        zadato = pojam.ToUpper();
        n = pojam.Length;
        pokusanaSlova = "";
        polupogodjeno = new char[n];
        for (int i = 0; i < n; i++)
            polupogodjeno[i] = '_';

        preostaloZivota = brZivota;
    }
    public string Stanje { get { return new string(polupogodjeno); } }
    public int PreostaloZivota { get { return preostaloZivota; } }
    public string PokusanaSlova { get { return pokusanaSlova; } }
    public bool Pokusaj(char c)
    {
        if (pokusanaSlova.Contains(c))
            return false;

        c = char.ToUpper(c);
        bool imaToSlovo = false;
        bool reseno = true;
        for (int i = 0; i < n; i++)
        {
            if (zadato[i] == c)
            {
                imaToSlovo = true;
                polupogodjeno[i] = c;
            }
            if (polupogodjeno[i] == '_')
                reseno = false;
        }
        if (!imaToSlovo)
        {
            pokusanaSlova += c;
            preostaloZivota--;
        }

        // da li je kraj
        return reseno || (preostaloZivota == 0);
    }
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
