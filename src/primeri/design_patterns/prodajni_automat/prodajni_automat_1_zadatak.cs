using System;
namespace Primer
{
    abstract public class AutomatUNekomStanju
    {
        protected ProdajniAutomat pa;
        protected double ubacenNovac;
        protected AutomatUNekomStanju(ProdajniAutomat prodajni, double novac) { pa = prodajni; ubacenNovac = novac; }

        //
        // dodati apstraktne metode
        //
    }

    //
    // dodati klase izvedene iz klase AutomatUNekomStanju
    //

    // klasa koju korisnik vidi kao automat, a koja 
    // interno koristi prethodne klase kao pomoćne
    public class ProdajniAutomat
    {
        public AutomatUNekomStanju a;
        public ProdajniAutomat()
        {
            //sa = new SlobodanAutomat(this, 0); 
        }
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
            
            // prikaži korisniku raspoložive komande
            Console.WriteLine("'Ux' za ubacivanje novca (neki broj umesto x, npr. U200)");
            Console.WriteLine("'Ix' za izbor proizvoda koji kosta x (neki broj umesto x, npr. I200)");
            Console.WriteLine("'O' za odustajanje");
            Console.WriteLine("'C' za cekanje");
            Console.WriteLine("'K' za kraj");
            while (!kraj) // dok korisnik ne izabere kraj
            {
                // ponudi korisnika da zada akciju
                // i pozovi odgovarajući metod za obradu akcije
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
