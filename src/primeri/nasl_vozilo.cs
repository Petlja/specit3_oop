using System;
namespace primer
{
    public class Vozilo
    {
        protected float potrosnja; // u litrima na 100 Km
        protected float kilometraza; // u Km
        protected float uRezervoaru; // u litrima
        // ...
        public Vozilo(float potr)
        {
            potrosnja = potr;
            kilometraza = 0;
            uRezervoaru = 0;
        }
        public void Natoci(float gorivo) { uRezervoaru += gorivo; }
        public void Predji(float rastojanje)
        {
            kilometraza += rastojanje;
            uRezervoaru -= potrosnja * rastojanje * 0.01f;
        }
        public float Domet { get { return 100 * uRezervoaru / potrosnja; } }
    }

    public class Autobus : Vozilo
    {
        private int brSedista;
        private int brPutnika;
        public Autobus(float potr, int n)
            : base(potr)
        {
            brSedista = n;
            brPutnika = 0;
        }

        public void Ulaz(int x) { brPutnika += x; }
        public void Izlaz(int x) { brPutnika -= x; }
        public int BrPutnika { get { return brPutnika; } }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Vozilo v = new Vozilo(5);
            v.Natoci(30);
            v.Predji(200);
            Console.WriteLine(v.Domet);

            Autobus a = new Autobus(10, 55);
            a.Natoci(30);
            a.Ulaz(20);
            a.Predji(200);
            a.Izlaz(5);
            Console.WriteLine(a.Domet);
            Console.WriteLine(a.BrPutnika);
        }
    }
}
