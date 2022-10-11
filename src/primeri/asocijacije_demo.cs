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
        string c = "tocak";
        string[] b = { "vodenica", "kamion", "krug", "guma" };
        string[,] a = {
            { "brasno", "potok", "kamen" },
            { "vozac", "teret", "prikolica" },
            { "centar", "geometrija", "kolo" },
            { "kaucuk", "zvaka", "izolator" },
        };
        Asocijacije igra = new Asocijacije(a, b, c);
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
