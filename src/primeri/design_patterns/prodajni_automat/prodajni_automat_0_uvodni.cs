using System;
namespace Primer
{
    public class ProdajniAutomat
    {
        private enum Status { Slobodan, PrimioNovac, Isporucuje, VracaKusur };
        private Status status;
        private double ubacenNovac;

        public ProdajniAutomat()
        {
            // čeka kupca (napunjen)
            status = Status.Slobodan;
        }
        
        // automat različito reaguje na ubacivanje novca, zavisno od prethodnog statusa
        public void Ubacivanje(double iznos)
        {
            switch (status)
            {
                case Status.Slobodan:
                    status = Status.PrimioNovac;
                    ubacenNovac = iznos;
                    Console.WriteLine("Ukupno ubaceno {0}, izaberi proizvod", ubacenNovac);
                    break;
                case Status.PrimioNovac:
                    ubacenNovac += iznos;
                    Console.WriteLine("Ukupno ubaceno {0}, izaberi proizvod", ubacenNovac);
                    break;
                case Status.Isporucuje:
                    Console.WriteLine("Propadose ti pare, trebalo je da sacekas isporuku");
                    break;
                case Status.VracaKusur:
                    Console.WriteLine("Propadose ti pare, trebalo je da sacekas vracanje novca");
                    break;
            }
        }
        
        // automat različito reaguje na izbor proizvoda, zavisno od prethodnog statusa
        public void IzborProizvoda(double iznos)
        {
            switch (status)
            {
                case Status.Slobodan:
                    Console.WriteLine("Prvo ubaci novac");
                    break;
                case Status.PrimioNovac:
                    if (ubacenNovac >= iznos)
                    {
                        ubacenNovac -= iznos;
                        Console.WriteLine("Sacekaj isporuku");
                        status = Status.Isporucuje;
                    }
                    else
                        Console.WriteLine("Proizvod kosta {0} dinara, ubaci jos novca", iznos);
                    break;
                case Status.Isporucuje:
                    Console.WriteLine("Sacekaj da se zavrsi isporuka");
                    break;
                case Status.VracaKusur:
                    Console.WriteLine("Sacekaj vracanje novca za prethodnu kupovinu");
                    break;
            }
        }
        
        // automat različito reaguje na odustajanje od kupovine, zavisno od prethodnog statusa
        public void Odustajanje()
        {
            switch (status)
            {
                case Status.Slobodan:
                    Console.WriteLine("Odustajes ni od cega");
                    break;
                case Status.PrimioNovac:
                    Console.WriteLine("Sacekaj da ti vratim novac");
                    status = Status.VracaKusur;
                    break;
                case Status.Isporucuje:
                    Console.WriteLine("Kasno, isporuka je u toku, sacekaj da se zavrsi");
                    break;
                case Status.VracaKusur:
                    Console.WriteLine("U toku je vracanje novca, sacekaj da se zavrsi");
                    break;
            }

        }
        
        // automat različito reaguje na čekanje, zavisno od prethodnog statusa
        public void Cekanje()
        {
            switch (status)
            {
                case Status.Slobodan:
                    Console.WriteLine("Nema sta da cekas, ubaci novac");
                    break;
                case Status.PrimioNovac:
                    Console.WriteLine("Nema sta da cekas, izaberi nesto");
                    break;
                case Status.Isporucuje:
                    if (ubacenNovac > 0)
                    {
                        Console.WriteLine("Izvoli svoj proizvod, sacekaj kusur");
                        status = Status.VracaKusur;
                    }
                    else
                    {
                        Console.WriteLine("Izvoli svoj proizvod");
                        status = Status.Slobodan;
                    }
                    break;
                case Status.VracaKusur:
                    Console.WriteLine("Izvoli svojih {0}", ubacenNovac);
                    status = Status.Slobodan;
                    ubacenNovac = 0;
                    break;
            }
        }
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
