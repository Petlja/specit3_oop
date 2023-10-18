using System;
using FunctionValue;

namespace NalazenjeNuleFunkcije
{
    class Program
    {
        // konzolni program koji ilustruje primenu biblioteke Funkcije
        static void Main(string[] args)
        {
            // ponudi korisnika da unese izraz
            Console.Write("Unesi izraz ");
            string expression = Console.ReadLine();
            Function F = null;
            string errMessage = "";
            if (Parser.Evaluate(expression, out F, out errMessage))
            {
                // ako je uneti izraz uspešno parsiran, 
                // ponudi unos granica intervala
                Console.Write("Unesi pocetak intervala ");
                double a = double.Parse(Console.ReadLine());
                Console.Write("Unesi kraj intervala ");
                double b = double.Parse(Console.ReadLine());

                // metodom polovljenja intervala odredi tačku 
                // u kojoj je vrednost funkcije jednaka nuli
                // (ovo je slično metodu binarne pretrage)
                double fa = F.Value(a);
                double fb = F.Value(b);
                double m = 0.5 * (a + b);
                double fm = F.Value(m);
                while (Math.Abs(b - a) > 1e-10)
                {
                    if (fa * fm > 0)
                    {
                        a = m;
                        fa = fm;
                    } 
                    else
                    {
                        b = m;
                        fb = fm;
                    }
                    m = 0.5 * (a + b);
                    fm = F.Value(m);
                }

                // prikaži rezultat
                Console.WriteLine("Funkcija je priblizno jednaka nuli za x = {0}", m.ToString("0.000000000"));
                Console.WriteLine("{0}", F.Value(a).ToString("0.000000000"));
            }
        }
    }
}
