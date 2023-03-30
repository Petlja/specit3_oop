using System;

namespace Program
{
    public class Proizvod
    {
        protected string naziv;
        protected int cena;
        public Proizvod(string naziv, int cena)
        {
            this.naziv = naziv;
            this.cena = cena;
        }
        public override string ToString()
        {
            return string.Format("{0}, {1}din.", naziv, cena);
        }
    }
    public class Namirnica : Proizvod
    {
        public Namirnica(string naziv, int masa, int cena, DateTime rok) : base(naziv, cena)
        {
            this.masa = masa;
            this.rok = rok;
        }
        protected int masa;
        protected DateTime rok;
        public override string ToString()
        {
            return string.Format("{0}, masa: {1}, upotrebljivo do {2}, {3}din.", 
                naziv, masa, rok.ToString("dd.MM.yyyy"), cena);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Proizvod sijalica = new Proizvod("Sijalica", 250);
            Proizvod mleko = new Namirnica("Mleko", 120, 1000, DateTime.Now.AddDays(2));
            Namirnica sir = new Namirnica("Sir", 400, 200, DateTime.Now.AddDays(30));

            Console.WriteLine(sijalica);
            Console.WriteLine(sir);
            Console.WriteLine(mleko);
        }
    }
}
