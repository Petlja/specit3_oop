using System;
namespace primer
{
    public class Vozilo
    {
        protected float potrosnja;           // u litrima na 100 km
        protected float kilometraza;         // u km
        protected float uRezervoaru;         // u litrima
        protected float kapacitetRezervoara; // u litrima

        // konstruktor - za potrebe ovog primera, vozilo je potpuno 
        // opisano potrošnjom goriva i kapacitetom rezervoara
        public Vozilo(float potr, float v) 
        {
            potrosnja = potr;
            kapacitetRezervoara = v;
            kilometraza = 0; // novo vozilo još nije prešlo ni jedan kilometar
            uRezervoaru = 0; // rezervoar je na početku prazan
        }

        // Sipanje u rezervoar date količine goriva u litrima 
        public void Natoci(float gorivo)
        {
            if (uRezervoaru + gorivo >= kapacitetRezervoara)
                throw new Exception("Nema mesta u rezervoaru");

            uRezervoaru += gorivo;
        }
        
        // Prelazak datog rastojanja u kilometrima
        public void Predji(float rastojanje)
        {
            float potrebnoLitara = rastojanje * (potrosnja * 0.01f);
            if (uRezervoaru < potrebnoLitara)
                throw new Exception("Nema dovoljno goriva");

            kilometraza += rastojanje;
            uRezervoaru -= potrebnoLitara;
        }

        // Svojstvo Domet govori koliko kilometara može da pređe vozilo 
        // sa količinom goriva trenutno zatečenom u rezervoaru
        public float Domet { get { return 100 * uRezervoaru / potrosnja; } }
    }

    public class Autobus : Vozilo
    {
        private int brSedista;
        private int brPutnika;

        // Za opis autobusa, pored potrošnje goriva i kapaciteta rezervoara, 
        // potrebno je navesti i broj mesta za putnike (tj. broj sedišta)
        public Autobus(float potr, float kapac, int brMesta)
            : base(potr, kapac)
        {
            brSedista = brMesta;
            brPutnika = 0;
        }

        // Ulazak zadatog broja putnika u autobus
        public void Ulaz(int x)
        {
            if (BrSlobodnihMesta < x)
                throw new Exception("Nema mesta za putnike");

            brPutnika += x;
        }
        
        // Izlazak zadatog broja putnika iz autobusa
        public void Izlaz(int x)
        {
            if (brPutnika < x)
                throw new Exception("Nema toliko putnika");

            brPutnika -= x;
        }
        
        // Vrednost svojstva 'BrSlobodnihMesta' ne mora da se čuva kao
        // polje, jer može da se izračuna na osnovu trenutnog broja putnika 
        // u autobusu, i broja sedišta koji se ne menja
        public int BrSlobodnihMesta { get { return brSedista - brPutnika; } }

        public int BrPutnika { get { return brPutnika; } }
    }
    
    // isprobavanje rada klasa 'Vozilo' i 'Autobus'
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // isprobavanje metoda klase Vozilo
                Vozilo v = new Vozilo(5, 40);
                v.Natoci(35);
                v.Predji(200);
                Console.WriteLine("Vozilo v moze da predje jos {0}Km.", v.Domet);

                // sa objektom klase Autobus može da se radi sve što i sa klasom Vozilo,
                // a podržani su i metodi Ulaz i Izlaz
                Autobus a = new Autobus(25, 300, 55);
                a.Natoci(250);
                a.Ulaz(20);
                a.Predji(200);
                a.Izlaz(5);
                Console.WriteLine("Vozilo a moze da predje jos {0}Km.", a.Domet);
                Console.WriteLine("U vozilu a ima jos {0} mesta za putnike.", a.BrSlobodnihMesta);
            }
            catch (Exception)
            {
                Console.WriteLine("Popravi brojeve u primeru i probaj ponovo");
            }
        }
    }
}
