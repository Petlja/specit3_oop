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

        bool kraj = false;
        int ukPoena = 0;
        while (!kraj)
        {
            string[] ulaz;
            Console.Clear();
            Prikazi(igra, ukPoena);
            if (!igra.SveOtvoreno)
            {
                bool uspesnoOtvaranje = false;
                while (!uspesnoOtvaranje)
                {
                    Console.WriteLine("(Unesi veliko slovo i broj, razdvojeno)");
                    Console.Write("Otvori polje ");
                    ulaz = Console.ReadLine().Split();
                    int kol = ulaz[0][0] - 'A';
                    int p = int.Parse(ulaz[1]) - 1;
                    uspesnoOtvaranje = igra.Otvori(kol, p);
                    Console.Clear();
                    Prikazi(igra, ukPoena);
                }
            }
            bool pravoNaPogadljanje = true;
            while (pravoNaPogadljanje)
            {
                Console.WriteLine("(Unesi veliko slovo za kolonu i pojam, ili za konacno samo pojam)");
                Console.Write("Pogadjaj: ");
                ulaz = Console.ReadLine().Split();
                if (ulaz.Length == 1)
                {
                    // pokusaj konacnog resenja
                    int poeni = igra.Pokusaj(ulaz[0]);
                    if (poeni > 0)
                    {
                        Console.WriteLine("Bravo, dobijas {0} poena.", poeni);
                        ukPoena += poeni;
                        for (int i = 0; i < 3; i++)
                            Console.WriteLine();
                        Prikazi(igra, ukPoena);
                        kraj = true;
                    }
                    pravoNaPogadljanje = false;
                }
                else
                {
                    // pokusaj resenja kolone
                    int kol = ulaz[0][0] - 'A';
                    int poeni = igra.Pokusaj(kol, ulaz[1]);
                    if (poeni > 0)
                    {
                        Console.WriteLine("Bravo, dobijas {0} poena.", poeni);
                        ukPoena += poeni;
                        for (int i = 0; i < 3; i++)
                            Console.WriteLine();
                        Prikazi(igra, ukPoena);
                    }
                    else
                        pravoNaPogadljanje = false;
                }
            }
        }
    }
}
