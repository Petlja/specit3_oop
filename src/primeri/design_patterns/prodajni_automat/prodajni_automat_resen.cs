using System;
namespace Primer
{
    abstract public class AutomatUNekomStanju
    {
        protected ProdajniAutomat pa;
        protected double ubacenNovac;
        protected AutomatUNekomStanju(ProdajniAutomat prodajni, double novac) { pa = prodajni; ubacenNovac = novac; }
        abstract public void Ubacivanje(double iznos);
        abstract public void IzborProizvoda(double cena);
        abstract public void Odustajanje();
        abstract public void Cekanje();
    }
    public class SlobodanAutomat : AutomatUNekomStanju
    {
        public SlobodanAutomat(ProdajniAutomat prodajni, double novac) : base(prodajni, novac) { }
        public override void Ubacivanje(double iznos)
        {
            ubacenNovac = iznos;
            pa.a = new AutomatSaNovcem(pa, ubacenNovac);
            Console.WriteLine("Ukupno ubaceno {0}, izaberi proizvod", ubacenNovac);
        }
        public override void IzborProizvoda(double cena)
        {
            Console.WriteLine("Prvo ubaci novac");
        }
        public override void Odustajanje()
        {
            Console.WriteLine("Odustajes ni od cega");
        }
        public override void Cekanje()
        {
            Console.WriteLine("Nema sta da cekas, ubaci novac");
        }
    }
    public class AutomatSaNovcem : AutomatUNekomStanju
    {
        public AutomatSaNovcem(ProdajniAutomat prodajni, double novac) : base(prodajni, novac) { }
        public override void Ubacivanje(double iznos)
        {
            ubacenNovac += iznos;
            Console.WriteLine("Ukupno ubaceno {0}, izaberi proizvod", ubacenNovac);
        }
        public override void IzborProizvoda(double cena)
        {
            if (ubacenNovac >= cena)
            {
                ubacenNovac -= cena;
                Console.WriteLine("Sacekaj isporuku");
                pa.a = new AutomatKojiIsporucuje(pa, ubacenNovac);
            }
            else
                Console.WriteLine("Proizvod kosta {0} dinara, ubaci jos novca", cena);
        }
        public override void Odustajanje()
        {
            Console.WriteLine("Sacekaj da ti vratim novac");
            pa.a = new AutomatKojiVracaNovac(pa, ubacenNovac);
        }
        public override void Cekanje()
        {
            Console.WriteLine("Nema sta da cekas, izaberi nesto");
        }
    }
    public class AutomatKojiIsporucuje : AutomatUNekomStanju
    {
        public AutomatKojiIsporucuje(ProdajniAutomat prodajni, double novac) : base(prodajni, novac) { }
        public override void Ubacivanje(double iznos)
        {
            Console.WriteLine("Propadose ti pare, trebalo je da sacekas isporuku");
        }
        public override void IzborProizvoda(double cena)
        {
            Console.WriteLine("Sacekaj da se zavrsi isporuka");
        }
        public override void Odustajanje()
        {
            Console.WriteLine("Kasno, isporuka je u toku, sacekaj da se zavrsi");
        }
        public override void Cekanje()
        {
            if (ubacenNovac > 0)
            {
                Console.WriteLine("Izvoli svoj proizvod, sacekaj kusur");
                pa.a = new AutomatKojiVracaNovac(pa, ubacenNovac);
            }
            else
            {
                Console.WriteLine("Izvoli svoj proizvod");
                pa.a = new SlobodanAutomat(pa, ubacenNovac);
            }
        }
    }
    public class AutomatKojiVracaNovac : AutomatUNekomStanju
    {
        public AutomatKojiVracaNovac(ProdajniAutomat prodajni, double novac) : base(prodajni, novac) { }
        public override void Ubacivanje(double iznos)
        {
            Console.WriteLine("Propadose ti pare, trebalo je da sacekas vracanje novca prethodnom kupcu");
        }
        public override void IzborProizvoda(double cena)
        {
            Console.WriteLine("Sacekaj vracanje novca prethodnom kupcu");
        }
        public override void Odustajanje()
        {
            Console.WriteLine("Kasno, u toku je vracanje novca prethodnom kupcu, sacekaj da se zavrsi");
        }
        public override void Cekanje()
        {
            Console.WriteLine("Izvoli svojih {0}", ubacenNovac);
            ubacenNovac = 0;
            pa.a = new SlobodanAutomat(pa, ubacenNovac);
        }
    }

    public class ProdajniAutomat
    {
        public AutomatUNekomStanju a;
        public ProdajniAutomat() { a = new SlobodanAutomat(this, 0); }
        public void Ubacivanje(double iznos) { a.Ubacivanje(iznos); }
        public void IzborProizvoda(double cena) { a.IzborProizvoda(cena); }
        public void Odustajanje() { a.Odustajanje(); }
        public void Cekanje() { a.Cekanje(); }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ProdajniAutomat pa = new ProdajniAutomat();
            bool kraj = false;
            Console.WriteLine("'Ux' za ubacivanje novca (neki broj umesto x, npr. U200)");
            Console.WriteLine("'Ix' za izbor proizvoda koji kosta x (neki broj umesto x, npr. I200)");
            Console.WriteLine("'O' za odustajanje");
            Console.WriteLine("'C' za cekanje");
            Console.WriteLine("'K' za kraj");
            while (!kraj)
            {
                Console.Write("Sta cemo: ");
                try
                {
                    string izbor = Console.ReadLine().ToUpper();
                    switch (izbor[0])
                    {
                        case 'U': pa.Ubacivanje(double.Parse(izbor.Substring(1))); break;
                        case 'I': pa.IzborProizvoda(double.Parse(izbor.Substring(1))); break;
                        case 'O': pa.Odustajanje(); break;
                        case 'C': pa.Cekanje(); break;
                        case 'K': kraj = true; break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Neispravna komanda");
                }
            }
            Console.WriteLine("Dovidjenja");
        }
    }
}
