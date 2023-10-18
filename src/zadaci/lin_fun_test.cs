using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            LinFun f1 = new LinFun(2, 3); // f1(x) = 2x+3
            LinFun f2 = new LinFun(3, 5); // f2(x) = 3x+5

            LinFun f3 = f1 + f2; // sabiranje funkcija
            LinFun f4 = LinFun.Compose(f1, f2); // slaganje funkcija
            
            // ispisivanje vrednosti funkcija f3 i f4 za razne x
            foreach (double x in new double[] { 0, 1, 2, 3 })
            {
                Console.WriteLine("f3({0}) = {1}, f4({0}) = {2}", 
                    x, f3.Value(x), f4.Value(x));
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
