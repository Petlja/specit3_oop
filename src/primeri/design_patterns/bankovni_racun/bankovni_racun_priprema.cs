using System;
namespace Primer
{
    public class Racun
    {
        // Zadužen status:    račun je u minusu, ne dobija kamatu, održavanje mu je najskuplje 
        // Standardan status: račun je u malom plusu, ne dobija kamatu, održavanje mu je najjeftinije
        // Povlašćen status:  račun je u velikom plusu, dobija kamatu, srednji troškovi održavanja
        // Svaki novi račun se otvara kao standardan

        #region PODACI
        private enum StatusRacuna { Zaduzen, Standardan, Povlascen };
        private string vlasnik;
        private StatusRacuna status;
        private double stanje, donjaGranica, gornjaGranica, kamatnaStopa, odrzavanjeRacuna;
        #endregion

        #region INTERFEJS (public metodi i svojstva)
        public Racun(string vlasnik)
        {
            this.vlasnik = vlasnik;
            PostaviStatus(StatusRacuna.Standardan);
        }
        public string Vlasnik { get { return vlasnik; } }
        public double Stanje { get { return stanje; } }
        public string Status { get { return status.ToString(); } }
        public void Uplata(double iznos)
        {
            stanje += iznos;
            AzurirajStatus();
            Izvestaj("uplata", iznos);
        }
        public void Isplata(double iznos)
        {
            if (stanje > 0)
            {
                stanje -= iznos;
                AzurirajStatus();
            }
            else
                Console.WriteLine("Nema novca za podizanje!");

            Izvestaj("isplata", iznos);
        }
        public void ObracunOdrzavanja()
        {
            stanje -= odrzavanjeRacuna;
            AzurirajStatus();
            Izvestaj("odrzavanje", odrzavanjeRacuna);
        }
        public void PripisKamate()
        {
            double kamata = (status == StatusRacuna.Povlascen) ? kamatnaStopa * stanje : 0;
            stanje += kamata;
            Izvestaj("pripis kamate", kamata);
        }
        #endregion

        #region PRIVATNI METODI
        private void PostaviStatus(StatusRacuna noviStatus)
        {
            status = noviStatus;
            switch (status)
            {
                case StatusRacuna.Zaduzen:
                    kamatnaStopa = 0.0;          // nebitna, ne dobija kamatu
                    donjaGranica = -1.0;         // nebitna, ne postoji niži status
                    gornjaGranica = 0.0;         // za prelazak u standardan
                    odrzavanjeRacuna = 500.0;
                    break;
                case StatusRacuna.Standardan:
                    kamatnaStopa = 0.0;          // nebitna, ne dobija kamatu
                    donjaGranica = 0.0;          // za prelazak u zadužen
                    gornjaGranica = 200000.0;    // za prelazak u povlašćen
                    odrzavanjeRacuna = 200.0;
                    break;
                case StatusRacuna.Povlascen:
                    kamatnaStopa = 0.05;
                    donjaGranica = 100000.0;     // za prelazak u standardan (za zadužen je 0)
                    gornjaGranica = 100000000.0; // nebitna, ne postoji viši status
                    odrzavanjeRacuna = 250.0;
                    break;
            }
        }
        private void AzurirajStatus()
        {
            // račun se različito ažurira u zavisnosti od statusa
            switch (status)
            {
                case StatusRacuna.Zaduzen:
                    if (stanje > gornjaGranica)
                        PostaviStatus(StatusRacuna.Standardan);
                    break;
                case StatusRacuna.Standardan:
                    if (stanje < donjaGranica)
                        PostaviStatus(StatusRacuna.Zaduzen);
                    else if (stanje > gornjaGranica)
                        PostaviStatus(StatusRacuna.Povlascen);
                    break;
                case StatusRacuna.Povlascen:
                    if (stanje < 0.0)
                        PostaviStatus(StatusRacuna.Zaduzen);
                    else if (stanje < donjaGranica)
                        PostaviStatus(StatusRacuna.Standardan);
                    break;
            }
        }
        
        private void Izvestaj(string usluga, double iznos)
        {
            Console.WriteLine(
                "Racun {0,-20} {1,-14} {2}: novo stanje = {3:10}, status = {4,7}.",
                vlasnik,
                usluga,
                string.Format("{0,10:#0.00}", iznos),
                string.Format("{0,10:#0.00}", stanje),
                status);
        }
        #endregion
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Racun perinRacun = new Racun("Petar Zivkovic");
            Racun lazinRacun = new Racun("Marko Lazovic");

            perinRacun.Uplata(135000.0);
            perinRacun.ObracunOdrzavanja();
            perinRacun.PripisKamate();

            perinRacun.Uplata(80000.0);
            perinRacun.ObracunOdrzavanja();
            perinRacun.PripisKamate();

            lazinRacun.Uplata(15500.0);

            perinRacun.Isplata(80000.0);
            perinRacun.ObracunOdrzavanja();
            perinRacun.PripisKamate();
            perinRacun.Isplata(70000.00);
            perinRacun.Isplata(100000.00);
            perinRacun.Isplata(10000.00);
            perinRacun.ObracunOdrzavanja();
        }
    }
}
/*
Racun Petar Zivkovic       uplata          135000.00: novo stanje =  135000.00, status = Standardan.
Racun Petar Zivkovic       odrzavanje         200.00: novo stanje =  134800.00, status = Standardan.
Racun Petar Zivkovic       pripis kamate        0.00: novo stanje =  134800.00, status = Standardan.
Racun Petar Zivkovic       uplata           80000.00: novo stanje =  214800.00, status = Povlascen.
Racun Petar Zivkovic       odrzavanje         250.00: novo stanje =  214550.00, status = Povlascen.
Racun Petar Zivkovic       pripis kamate    10727.50: novo stanje =  225277.50, status = Povlascen.
Racun Marko Lazovic        uplata           15500.00: novo stanje =   15500.00, status = Standardan.
Racun Petar Zivkovic       isplata          80000.00: novo stanje =  145277.50, status = Povlascen.
Racun Petar Zivkovic       odrzavanje         250.00: novo stanje =  145027.50, status = Povlascen.
Racun Petar Zivkovic       pripis kamate     7251.38: novo stanje =  152278.88, status = Povlascen.
Racun Petar Zivkovic       isplata          70000.00: novo stanje =   82278.88, status = Standardan.
Racun Petar Zivkovic       isplata         100000.00: novo stanje =  -17721.13, status = Zaduzen.
Nema novca za podizanje!
Racun Petar Zivkovic       isplata          10000.00: novo stanje =  -17721.13, status = Zaduzen.
Racun Petar Zivkovic       odrzavanje         500.00: novo stanje =  -18221.13, status = Zaduzen.
*/
