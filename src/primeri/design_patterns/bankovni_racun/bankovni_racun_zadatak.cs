using System;
namespace Primer
{
    //
    // ovde dodati sve potrebne klase 
    //

    public class Program
    {
        public static void Main(string[] args)
        {
            Racun perinRacun = new Racun("Petar Zivkovic");
            Racun lazinRacun = new Racun("Marko Lazovic");

            perinRacun.Uplata(135000.0);
            perinRacun.ObracunOdrzavanja();
            perinRacun.PripisKamate();

            perinRacun.Uplata(80000.0);
            perinRacun.ObracunOdrzavanja();
            perinRacun.PripisKamate();

            lazinRacun.Uplata(15500.0);

            perinRacun.Isplata(80000.0);
            perinRacun.ObracunOdrzavanja();
            perinRacun.PripisKamate();
            perinRacun.Isplata(70000.00);
            perinRacun.Isplata(100000.00);
            perinRacun.Isplata(10000.00);
            perinRacun.ObracunOdrzavanja();
        }
    }
}
