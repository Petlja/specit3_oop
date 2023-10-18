using System;
using AG;

namespace con_centar_spolja_upisanog
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odrediti centar kruga spolja upisanog uz stranicu AB trougla ABC,
            // čija su temena A(6, 5), B(3, 2), C(10, 1) 
            Tacka A = new Tacka(6, 5);
            Tacka B = new Tacka(3, 2);
            Tacka C = new Tacka(10, 1);

            // vektor simetrale spoljašnjeg ugla kod temena A
            Vektor AS1 = new Vektor(A, B).Ort() - new Vektor(A, C).Ort();

            // vektor simetrale spoljašnjeg ugla kod temena B
            Vektor BS1 = new Vektor(B, A).Ort() - new Vektor(B, C).Ort();

            Prava sa1 = new Prava(A, AS1); // simetrala spoljašnjeg ugla kod A
            Prava sb1 = new Prava(B, BS1); // simetrala spoljašnjeg ugla kod B
            Tacka S = sa1.Presek(sb1); // presek pravih sa1 i sb1
            
            Console.WriteLine(
                "Centar kruga spolja upisanog uz stranicu AB trougla ABC je {0}.", S);
            //Centar kruga spolja upisanog trouglu ABC je(2.000, 5.000).
        }
    }
}
