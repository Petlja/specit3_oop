using System;
using FunctionValue;

namespace TabelaVrednostiFunkcije
{
    class Program
    {
        static void Main(string[] args)
        {
            // ponudi korisnika da unese izraz, tj. funkciju
            Console.Write("Unesi izraz ");
            string expression = Console.ReadLine();
            Function F = null;
            string errMessage = "";
            if (Parser.Evaluate(expression, out F, out errMessage))
            {
                // ako je uneta ispravna funkcija, 
                // ponudi unos intervala u kome se računaju vrednosti funkcije
                // i broj tačaka u kojima treba računati vrednosti funkcije
                Console.Write("Unesi pocetak intervala ");
                double a = double.Parse(Console.ReadLine());
                Console.Write("Unesi kraj intervala ");
                double b = double.Parse(Console.ReadLine());
                Console.Write("Unesi broj deonih tacaka ");
                int n = int.Parse(Console.ReadLine());
                double step = (b - a) / (n - 1);
                // izračunaj vrednosti funkcije u traženim tačkama
                for (int i = 0; i < n; i++)
                {
                    double x = a + i * step;
                    string s = F.ToString().Replace("x", x.ToString("0.000"));
                    Console.WriteLine("{0} = {1}", s, F.Value(x).ToString("0.000"));
                }
            }
        }
    }
}
