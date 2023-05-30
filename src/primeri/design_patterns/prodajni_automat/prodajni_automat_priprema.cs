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
            // pun stvari
            status = Status.Slobodan;
        }
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
                    Console.WriteLine("Propadose ti pare, trebalo je da sacekas vracanje novca prethodnom kupcu");
                    break;
            }
        }
        public void IzborProizvoda()
        {
            switch (status)
            {
                case Status.Slobodan:
                    Console.WriteLine("Prvo ubaci novac");
                    break;
                case Status.PrimioNovac:
                    if (ubacenNovac >= 100)
                    {
                        ubacenNovac -= 100;
                        Console.WriteLine("Sacekaj isporuku");
                        status = Status.Isporucuje;
                    }
                    else
                        Console.WriteLine("Proizvod kosta 100 dinara, ubaci jos novca");
                    break;
                case Status.Isporucuje:
                    Console.WriteLine("Sacekaj da se zavrsi isporuka");
                    break;
                case Status.VracaKusur:
                    Console.WriteLine("Sacekaj vracanje novca prethodnom kupcu");
                    break;
            }
        }
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
                    Console.WriteLine("Kasno, u toku je vracanje novca prethodnom kupcu, sacekaj da se zavrsi");
                    break;
            }

        }
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
