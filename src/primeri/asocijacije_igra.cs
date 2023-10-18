using System;
using AsocijacijeLib;

namespace AsocijacijeCon
{
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
            
            bool kraj = false;
            int ukPoena = 0;
            
            // dok igrač ne pogodi konačno rešenje ili ne pritisne 'K'
            while (!kraj)
            {
                string[] ulaz;
                Console.Clear();
                Prikazi(igra, ukPoena);
                if (!igra.SveOtvoreno)
                {
                    bool uspesnoOtvaranje = false;
                    // ako nisu sva polja otvorena, nudi igrača da otvori polje
                    // dok ne otvori neko polje ili dok ne odustane od igre
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
                            
                            // ako je pritisnuto 'K', to je kraj igre
                            if (ulaz.Length == 1 && ulaz[0].ToUpper() == "K")
                                kraj = true;
                            else
                            {
                                // pokušaj da otvoriš zadato polje
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
                // nudi igrača da pogađa dok ne pogreši, ili ne pogodi konačno rešenje
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
                            // pokušaj konačnog rešenja
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
                            // pokušaj rešenja kolone
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
