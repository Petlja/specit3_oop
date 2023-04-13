using System;
using AG;

namespace con_centar_upisanog
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odrediti centar kruga upisanog u trougao ABC, cija su temena
            // A(6, 5), B(3, 2), C(10, 1) 
            Tacka A = new Tacka(6, 5);
            Tacka B = new Tacka(3, 2);
            Tacka C = new Tacka(10, 1);

            Vektor AS = new Vektor(A, B).Ort() + new Vektor(A, C).Ort();
            Vektor BS = new Vektor(B, A).Ort() + new Vektor(B, C).Ort();
            Prava sa = new Prava(A, AS);
            Prava sb = new Prava(B, BS);
            Tacka S = sa.Presek(sb);
            Console.WriteLine(
                "Centar kruga upisanog u trougao ABC je {0}.", S);
            //Centar kruga upisanog u trougao ABC je(6.000, 3.000).
        }
    }
}
