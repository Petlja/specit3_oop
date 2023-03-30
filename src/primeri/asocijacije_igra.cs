using System;
using AsocijacijeLib;

namespace AsocijacijeCon
{
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
                    while (!uspesnoOtvaranje && !kraj)
                    {
                        try
                        {
                            Console.WriteLine("Za kraj igre, unesi `K`");
                            Console.WriteLine("Da ovoris polje, unesi veliko slovo i broj, razdvojeno");
                            Console.Write("Tvoj izbor: ");
                            while (Console.KeyAvailable)
                                Console.ReadKey(false); // propusti prethodne pritiske na tastere
                            ulaz = Console.ReadLine().Split();
                            if (ulaz.Length == 1 && ulaz[0].ToUpper() == "K")
                                kraj = true;
                            else
                            {
                                int kol = ulaz[0].ToUpper()[0] - 'A';
                                int p = int.Parse(ulaz[1]) - 1;
                                uspesnoOtvaranje = igra.Otvori(kol, p);
                                Console.Clear();
                                Prikazi(igra, ukPoena);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Neispravna komanda");
                        }
                    }
                }
                bool pravoNaPogadljanje = !kraj;
                while (pravoNaPogadljanje)
                {
                    try
                    {
                        Console.WriteLine("(Unesi veliko slovo za kolonu i pojam, ili za konacno samo pojam)");
                        Console.Write("Pogadjaj: ");
                        while (Console.KeyAvailable)
                            Console.ReadKey(false); // propusti prethodne pritiske na tastere
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
                            int kol = ulaz[0].ToUpper()[0] - 'A';
                            int poeni = igra.Pokusaj(kol, ulaz[1]);
                            if (poeni > 0)
                            {
                                Console.WriteLine("Bravo, dobijas {0} poena.", poeni);
                                int x = Math.Abs(4);
                                ukPoena += poeni;
                                for (int i = 0; i < 3; i++)
                                    Console.WriteLine();
                                Prikazi(igra, ukPoena);
                            }
                            else
                                pravoNaPogadljanje = false;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Neispravna komanda");
                    }
                }
            }
        }
    }
}
