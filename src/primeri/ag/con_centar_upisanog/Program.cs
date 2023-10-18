using System;
using AG;

namespace con_centar_upisanog
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odrediti centar kruga upisanog u trougao ABC, čija su temena
            // A(6, 5), B(3, 2), C(10, 1) 
            Tacka A = new Tacka(6, 5);
            Tacka B = new Tacka(3, 2);
            Tacka C = new Tacka(10, 1);

            // vektor simetrale ugla BAC
            Vektor AS = new Vektor(A, B).Ort() + new Vektor(A, C).Ort();
            
            // vektor simetrale ugla ABC
            Vektor BS = new Vektor(B, A).Ort() + new Vektor(B, C).Ort();
            
            Prava sa = new Prava(A, AS); // simetrala ugla BAC
            Prava sb = new Prava(B, BS); // simetrala ugla ABC
            Tacka S = sa.Presek(sb); // presek pravih sa i sb
            
            Console.WriteLine(
                "Centar kruga upisanog u trougao ABC je {0}.", S);
            //Centar kruga upisanog u trougao ABC je(6.000, 3.000).
        }
    }
}
