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
        public string Naziv { get { return naziv; } }
        public int Cena { get { return cena; } }
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
        public int Masa { get { return masa; } }
        public DateTime RokTrajanja { get { return rok; } }
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

            Console.WriteLine("Cena sijalice je {0}", sijalica.Cena);
            Console.WriteLine("Cena mleka je {0}", mleko.Cena);
            Console.WriteLine("Cena sira je {0}", sir.Cena);

            //Console.WriteLine("Masa sijalice je {0}", sijalica.Masa);
            //Console.WriteLine("Masa mleka je {0}", mleko.Masa);
            Console.WriteLine("Masa sira je {0}", sir.Masa);
        }
    }
}
