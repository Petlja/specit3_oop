using System;
using AG;

namespace geom_zadaci_con
{
    class Program
    {
        public static void Zad_01()
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
                "Zad. 1: centar kruga opisanog oko trougla ABC je {0}.", O);
            //Zad. 1: centar kruga opisanog oko trougla ABC je(7.000, 6.000).
        }

        public static void Zad_02()
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
                "Zad. 2: centar kruga upisanog u trougao ABC je {0}.", S);
            //Zad. 2: centar kruga upisanog u trougao ABC je(6.000, 3.000).
        }

        public static void Zad_03()
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
                "Zad. 3: centar kruga spolja upisanog uz stranicu AB trougla ABC je {0}.", S);
            //Zad. 3: centar kruga spolja upisanog trouglu ABC je(2.000, 5.000).
        }

        public static void Zad_04()
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
            Console.WriteLine("Zad. 4: tacka C{0}", C);
            //Zad. 4: tacka C(7.500, 4.598)
        }

        public static void Zad_05()
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
            Console.WriteLine("Zad. 5: tacka A{0}", A);
            //Zad. 5: tacka A(7.000, 9.000)
        }

        public static void Main(string[] args)
        {
            Zad_01();
            Zad_02();
            Zad_03();
            Zad_04();
            Zad_05();
        }
    }
}
