class Program
{
    static void Prikazi(Asocijacije igra, int ukPoena, string konacno = "")
    {
        for (int p = 0; p < igra.BrojPojmova; p++)
        {
            for (int kol = 0; kol < igra.BrojKolona; kol++)
            {
                Console.Write("{0,15}", igra[kol, p]);
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', 15 * igra.BrojKolona));
        for (int kol = 0; kol < igra.BrojKolona; kol++)
            Console.Write("{0,15}", igra[kol]);
        Console.WriteLine();
        if (konacno != "")
            Console.WriteLine("Konacno: {0}", konacno);
        Console.WriteLine("Ukupan broj poena do sada: {0}", ukPoena);
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        Console.Write("Unesi putanju do fajla: ");
        string putanja = Console.ReadLine();
        Asocijacije igra = Asocijacije.Kreiraj(putanja);
        int ukPoena = 0;
        igra.Otvori(1, 0);
        Prikazi(igra, ukPoena);
        int poeni = igra.Pokusaj(1, "kamion");
        if (poeni > 0)
        {
            ukPoena += poeni;
            Prikazi(igra, ukPoena);
        }
        poeni = igra.Pokusaj("tocak");
        if (poeni > 0)
        {
            ukPoena += poeni;
            Prikazi(igra, ukPoena, "tocak");
        }
    }
}
