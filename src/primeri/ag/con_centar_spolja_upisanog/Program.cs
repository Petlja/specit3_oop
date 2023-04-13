using System;
using AG;

namespace con_centar_spolja_upisanog
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odrediti centar kruga spolja upisanog uz stranicu AB trougla ABC,
            // cija su temena A(6, 5), B(3, 2), C(10, 1) 
            Tacka A = new Tacka(6, 5);
            Tacka B = new Tacka(3, 2);
            Tacka C = new Tacka(10, 1);

            Vektor AS1 = new Vektor(A, B).Ort() - new Vektor(A, C).Ort();
            Vektor BS1 = new Vektor(B, A).Ort() - new Vektor(B, C).Ort();
            Prava sa1 = new Prava(A, AS1);
            Prava sb1 = new Prava(B, BS1);
            Tacka S = sa1.Presek(sb1);
            Console.WriteLine(
                "Centar kruga spolja upisanog uz stranicu AB trougla ABC je {0}.", S);
            //Centar kruga spolja upisanog trouglu ABC je(2.000, 5.000).
        }
    }
}
