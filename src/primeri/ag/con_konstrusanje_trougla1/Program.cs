using System;
using AG;

namespace con_konstrusanje_trougla1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Konstruisati trougao ABC, ako je dato 
            // A(3, 2), B(9, 2) , alfa = 30 stepeni, beta = 60 stepeni
            Tacka A = new Tacka(3, 2);
            Tacka B = new Tacka(9, 2);
            double alfa = Math.PI / 6;
            double beta = Math.PI / 3;

            Prava ac = new Prava(A, new Vektor(A, B).Rotacija(alfa));
            Prava bc = new Prava(B, new Vektor(B, A).Rotacija(-beta));
            Tacka C = ac.Presek(bc);
            Console.WriteLine("Tacka C{0}", C);
            //Tacka C(7.500, 4.598)
        }
    }
}
