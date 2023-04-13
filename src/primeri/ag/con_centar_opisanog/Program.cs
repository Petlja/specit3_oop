using System;
using AG;

namespace con_centar_opisanog
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odrediti centar kruga opisanog oko trougla ABC, cija su temena
            // A(3, 3), B(10, 10), C(12, 6)
            Tacka A = new Tacka(3, 3);
            Tacka B = new Tacka(10, 10);
            Tacka C = new Tacka(12, 6);

            Prava sa = Prava.Simetrala(B, C);
            Prava sb = Prava.Simetrala(A, C);
            Tacka O = sa.Presek(sb);
            Console.WriteLine(
                "Centar kruga opisanog oko trougla ABC je {0}.", O);
            //Centar kruga opisanog oko trougla ABC je(7.000, 6.000).
        }
    }
}
