using System;
using FunctionValue;

namespace NalazenjeNuleFunkcije
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Unesi izraz ");
            string expression = Console.ReadLine();
            Function F = null;
            string errMessage = "";
            if (Parser.Evaluate(expression, out F, out errMessage))
            {
                Console.Write("Unesi pocetak intervala ");
                double a = double.Parse(Console.ReadLine());
                Console.Write("Unesi kraj intervala ");
                double b = double.Parse(Console.ReadLine());

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

                Console.WriteLine("Funkcija je priblizno jednaka nuli za x = {0}", m.ToString("0.000000000"));
                Console.WriteLine("{0}", F.Value(a).ToString("0.000000000"));
            }
        }
    }
}
