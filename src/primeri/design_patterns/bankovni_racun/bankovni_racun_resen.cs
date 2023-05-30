using System;
namespace Primer
{
    public abstract class RacunSaStatusom
    {
        protected Racun racun;
        protected double stanje, donjaGranica, gornjaGranica, kamatnaStopa, odrzavanjeRacuna;
        public Racun Racun { get { return racun; } }
        public double Stanje { get { return stanje; } }
        public void Uplata(double iznos)
        {
            stanje += iznos;
            AzurirajStatus();
            racun.Izvestaj("uplata", iznos);
        }
        public void PripisKamate()
        {
            double kamata = kamatnaStopa * stanje;
            stanje += kamata;
            racun.Izvestaj("pripis kamate", kamata);
        }
        public void ObracunOdrzavanja()
        {
            stanje -= odrzavanjeRacuna;
            AzurirajStatus();
            racun.Izvestaj("odrzavanje", odrzavanjeRacuna);
        }

        public abstract void Isplata(double iznos);
        protected abstract void AzurirajStatus();
    }


    // ZaduzenRacun je u minusu, ne dobija kamatu, odrzavanje mu je najskuplje 
    public class ZaduzenRacun : RacunSaStatusom
    {
        public ZaduzenRacun(RacunSaStatusom racunSaStatuson)
        {
            stanje = racunSaStatuson.Stanje;
            racun = racunSaStatuson.Racun;
            kamatnaStopa = 0.0;
            donjaGranica = -1.0;         // nebitna, ne postoji nizi status
            gornjaGranica = 0.0;         // za prelazak u standardan
            odrzavanjeRacuna = 500.0;
        }
        public override void Isplata(double iznos)
        {
            Console.WriteLine("Nema novca za podizanje!");
            racun.Izvestaj("isplata", iznos);
        }
        protected override void AzurirajStatus()
        {
            if (Stanje > gornjaGranica)
                Racun.RacunStatus = new StandardanRacun(this);
        }
    }

    // StandardanRacun je u malom plusu, ne dobija kamatu, odrzavanje mu je najjeftinije
    public class StandardanRacun : RacunSaStatusom
    {
        public StandardanRacun(RacunSaStatusom racunSaStatuson)
        {
            stanje = racunSaStatuson.Stanje;
            racun = racunSaStatuson.Racun;
            kamatnaStopa = 0.0;
            donjaGranica = 0.0;          // za prelazak u zaduzen
            gornjaGranica = 200000.0;    // za prelazak u povlascen
            odrzavanjeRacuna = 200.0;
        }
        public StandardanRacun(double stanje, Racun racun)
        {
            this.stanje = stanje;
            this.racun = racun;
            kamatnaStopa = 0.0;
            donjaGranica = 0.0;          // za prelazak u zaduzen
            gornjaGranica = 200000.0;    // za prelazak u povlascen
            odrzavanjeRacuna = 200.0;
        }
        public override void Isplata(double iznos)
        {
            stanje -= iznos;
            AzurirajStatus();
            racun.Izvestaj("isplata", iznos);
        }
        protected override void AzurirajStatus()
        {
            if (stanje < donjaGranica)
                Racun.RacunStatus = new ZaduzenRacun(this);
            else if (stanje > gornjaGranica)
                Racun.RacunStatus = new PovlascenRacun(this);
        }
    }

    // Povlascen status:  racun je u velikom plusu, dobija kamatu, srednji torskovi odrzavanja
    public class PovlascenRacun : RacunSaStatusom
    {
        public PovlascenRacun(RacunSaStatusom racunSaStatuson)
        {
            stanje = racunSaStatuson.Stanje;
            racun = racunSaStatuson.Racun;
            kamatnaStopa = 0.05;
            donjaGranica = 100000.0;     // za prelazak u standardan (za zaduzen je 0)
            gornjaGranica = 100000000.0; // nebitna, ne postoji visi status
            odrzavanjeRacuna = 250.0;
        }
        public override void Isplata(double iznos)
        {
            stanje -= iznos;
            AzurirajStatus();
            racun.Izvestaj("isplata", iznos);
        }
        protected override void AzurirajStatus()
        {
            if (stanje < 0.0)
                Racun.RacunStatus = new ZaduzenRacun(this);
            else if (stanje < donjaGranica)
                Racun.RacunStatus = new StandardanRacun(this);
        }
    }

    public class Racun
    {
        private RacunSaStatusom racunStatus;
        private string vlasnik;
        public Racun(string vlasnik)
        {
            this.vlasnik = vlasnik;
            racunStatus = new StandardanRacun(0.0, this);
        }
        public double Stanje { get { return racunStatus.Stanje; } }
        public RacunSaStatusom RacunStatus
        {
            get { return racunStatus; }
            set { racunStatus = value; }
        }
        public void Uplata(double iznos)
        {
            racunStatus.Uplata(iznos);
        }
        public void Isplata(double iznos)
        {
            racunStatus.Isplata(iznos);
        }
        public void ObracunOdrzavanja()
        {
            racunStatus.ObracunOdrzavanja();
        }
        public void PripisKamate()
        {
            racunStatus.PripisKamate();
        }
        public void Izvestaj(string usluga, double iznos)
        {
            Console.WriteLine(
                "Racun {0,-20} {1,-14} {2}: novo stanje = {3:10}, status = {4,7}.",
                vlasnik,
                usluga,
                string.Format("{0,10:#0.00}", iznos),
                string.Format("{0,10:#0.00}", Stanje),
                racunStatus.GetType().Name);
        }
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
Racun Petar Zivkovic       uplata          135000.00: novo stanje =  135000.00, status = StandardanRacun.
Racun Petar Zivkovic       odrzavanje         200.00: novo stanje =  134800.00, status = StandardanRacun.
Racun Petar Zivkovic       pripis kamate        0.00: novo stanje =  134800.00, status = StandardanRacun.
Racun Petar Zivkovic       uplata           80000.00: novo stanje =  214800.00, status = PovlascenRacun.
Racun Petar Zivkovic       odrzavanje         250.00: novo stanje =  214550.00, status = PovlascenRacun.
Racun Petar Zivkovic       pripis kamate    10727.50: novo stanje =  225277.50, status = PovlascenRacun.
Racun Marko Lazovic        uplata           15500.00: novo stanje =   15500.00, status = StandardanRacun.
Racun Petar Zivkovic       isplata          80000.00: novo stanje =  145277.50, status = PovlascenRacun.
Racun Petar Zivkovic       odrzavanje         250.00: novo stanje =  145027.50, status = PovlascenRacun.
Racun Petar Zivkovic       pripis kamate     7251.38: novo stanje =  152278.88, status = PovlascenRacun.
Racun Petar Zivkovic       isplata          70000.00: novo stanje =   82278.88, status = StandardanRacun.
Racun Petar Zivkovic       isplata         100000.00: novo stanje =  -17721.13, status = ZaduzenRacun.
Nema novca za podizanje!
Racun Petar Zivkovic       isplata          10000.00: novo stanje =  -17721.13, status = ZaduzenRacun.
Racun Petar Zivkovic       odrzavanje         500.00: novo stanje =  -18221.13, status = ZaduzenRacun.
*/
