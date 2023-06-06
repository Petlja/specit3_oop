using System;
namespace Primer
{
    public class ProdajniAutomat
    {
        abstract public class AutomatUNekomStanju
        {
            protected ProdajniAutomat glavni;
            protected double ubacenNovac;
            protected AutomatUNekomStanju(ProdajniAutomat pa, double novac)
            {
                glavni = pa;
                ubacenNovac = novac;
            }
            abstract public void Ubacivanje(double iznos);
            abstract public void IzborProizvoda(double cena);
            abstract public void Odustajanje();
            abstract public void Cekanje();
        }
        private class SlobodanAutomat : AutomatUNekomStanju
        {
            public SlobodanAutomat(ProdajniAutomat pa, double novac) : base(pa, novac) { }

            public override void Ubacivanje(double iznos)
            {
                ubacenNovac = iznos;
                glavni.aktivni = new AutomatSaNovcem(glavni, ubacenNovac);
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
        private class AutomatSaNovcem : AutomatUNekomStanju
        {
            public AutomatSaNovcem(ProdajniAutomat pa, double novac) : base(pa, novac) { }
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
                    glavni.aktivni = new AutomatKojiIsporucuje(glavni, ubacenNovac);
                }
                else
                    Console.WriteLine("Proizvod kosta {0} dinara, ubaci jos novca", cena);
            }
            public override void Odustajanje()
            {
                Console.WriteLine("Sacekaj da ti vratim novac");
                glavni.aktivni = new AutomatKojiVracaNovac(glavni, ubacenNovac);
            }
            public override void Cekanje()
            {
                Console.WriteLine("Nema sta da cekas, izaberi nesto");
            }
        }
        private class AutomatKojiIsporucuje : AutomatUNekomStanju
        {
            public AutomatKojiIsporucuje(ProdajniAutomat pa, double novac) : base(pa, novac) { }
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
                    glavni.aktivni = new AutomatKojiVracaNovac(glavni, ubacenNovac);
                }
                else
                {
                    Console.WriteLine("Izvoli svoj proizvod");
                    glavni.aktivni = new SlobodanAutomat(glavni, ubacenNovac);
                }
            }
        }
        private class AutomatKojiVracaNovac : AutomatUNekomStanju
        {
            public AutomatKojiVracaNovac(ProdajniAutomat pa, double novac) : base(pa, novac) { }
            public override void Ubacivanje(double iznos)
            {
                Console.WriteLine("Propadose ti pare, trebalo je da sacekas vracanje novca");
            }
            public override void IzborProizvoda(double cena)
            {
                Console.WriteLine("Sacekaj vracanje novca za prethodnu kupovinu");
            }
            public override void Odustajanje()
            {
                Console.WriteLine("Kasno, u toku je vracanje novca prethodnom kupcu, sacekaj da se zavrsi");
            }
            public override void Cekanje()
            {
                Console.WriteLine("Izvoli svojih {0}", ubacenNovac);
                ubacenNovac = 0;
                glavni.aktivni = new SlobodanAutomat(glavni, ubacenNovac);
            }
        }

        private AutomatUNekomStanju aktivni;
        public ProdajniAutomat() { aktivni = new SlobodanAutomat(this, 0); }
        public void Ubacivanje(double iznos) { aktivni.Ubacivanje(iznos); }
        public void IzborProizvoda(double cena) { aktivni.IzborProizvoda(cena); }
        public void Odustajanje() { aktivni.Odustajanje(); }
        public void Cekanje() { aktivni.Cekanje(); }
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
