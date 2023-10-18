class Program
{
    // prikaz trenutnog stanja na tabli 
    static void Prikazi(Asocijacije igra, int ukPoena, string konacno = "")
    {
        // nazivi polja ili otvoreni pojmovi
        for (int p = 0; p < igra.BrojPojmova; p++)
        {
            for (int kol = 0; kol < igra.BrojKolona; kol++)
            {
                Console.Write("{0,15}", igra[kol, p]);
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', 15 * igra.BrojKolona));

        // nazivi kolona ili rešenja kolona
        for (int kol = 0; kol < igra.BrojKolona; kol++)
            Console.Write("{0,15}", igra[kol]);
        Console.WriteLine();

        // konačno rešenje, ako je pogođeno
        if (konacno != "")
            Console.WriteLine("Konacno: {0}", konacno);

        Console.WriteLine("Ukupan broj poena do sada: {0}", ukPoena);
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        // kreiranje igre sa pojmovima koji se učitavaju iz fajla
        Console.Write("Unesi putanju do fajla: ");
        string putanja = Console.ReadLine();
        Asocijacije igra = Asocijacije.Kreiraj(putanja);

        // isprobavanje otvaranja polja
        int ukPoena = 0;
        igra.Otvori(1, 0);
        Prikazi(igra, ukPoena);
        
        // isprobavanje pogađanja kolone
        int poeni = igra.Pokusaj(1, "kamion");
        if (poeni > 0)
        {
            ukPoena += poeni;
            Prikazi(igra, ukPoena);
        }
        
        // isprobavanje pogađanja konačnog rešenja
        poeni = igra.Pokusaj("tocak");
        if (poeni > 0)
        {
            ukPoena += poeni;
            Prikazi(igra, ukPoena, "tocak");
        }
    }
}
