using System;
namespace primer
{
    public class Vozilo
    {
        protected float potrosnja;           // u litrima na 100 km
        protected float kilometraza;         // u km
        protected float uRezervoaru;         // u litrima
        protected float kapacitetRezervoara; // u litrima
        public Vozilo(float potr, float v)
        {
            potrosnja = potr;
            kapacitetRezervoara = v;
            kilometraza = 0;
            uRezervoaru = 0;
        }
        public void Natoci(float gorivo)
        {
            if (uRezervoaru + gorivo >= kapacitetRezervoara)
                throw new Exception("Nema mesta u rezervoaru");

            uRezervoaru += gorivo;
        }
        public void Predji(float rastojanje)
        {
            float potrebnoLitara = potrosnja * rastojanje * 0.01f;
            if (uRezervoaru < potrebnoLitara)
                throw new Exception("Nema dovoljno goriva");

            kilometraza += rastojanje;
            uRezervoaru -= potrebnoLitara;
        }
        public float Domet { get { return 100 * uRezervoaru / potrosnja; } }
    }

    public class Autobus : Vozilo
    {
        private int brSedista;
        private int brPutnika;
        public Autobus(float potr, float kapac, int brMesta)
            : base(potr, kapac)
        {
            brSedista = brMesta;
            brPutnika = 0;
        }
        public void Ulaz(int x)
        {
            if (BrSlobodnihMesta < x)
                throw new Exception("Nema mesta za putnike");

            brPutnika += x;
        }
        public void Izlaz(int x)
        {
            if (brPutnika < x)
                throw new Exception("Nema toliko putnika");

            brPutnika -= x;
        }
        public int BrSlobodnihMesta { get { return brSedista - brPutnika; } }
        public int BrPutnika { get { return brPutnika; } }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vozilo v = new Vozilo(5, 40);
                v.Natoci(35);
                v.Predji(200);
                Console.WriteLine("Vozilo v moze da predje jos {0}Km.", v.Domet);

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
