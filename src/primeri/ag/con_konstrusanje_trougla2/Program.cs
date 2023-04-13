using System;
using AG;

namespace con_konstrusanje_trougla2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Konstruisati trougao ABC, ako je dato 
            // B(21, 9), C(15, 15) , beta = 45 stepeni, b+c = 24
            Tacka B = new Tacka(21, 9);
            Tacka C = new Tacka(15, 15);
            double beta = Math.PI / 4;
            double bd = 24;

            // D je tacka na pravoj AB, takva da |DB| = b+c
            Tacka D = B + bd * new Vektor(B, C).Rotacija(beta).Ort();
            Prava ab = new Prava(B, D);
            Prava s = Prava.Simetrala(C, D);
            Tacka A = ab.Presek(s);
            Console.WriteLine("Tacka A{0}", A);
            //Tacka A(7.000, 9.000)
        }
    }
}
