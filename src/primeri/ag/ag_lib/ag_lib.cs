using System;

namespace AG
{
    public class Tacka
    {
        private double x;
        private double y;
        public double X { get { return x; } }
        public double Y { get { return y; } }

        public Tacka(double _x, double _y) { x = _x; y = _y; }

        // Tačka dobijena translacijom date tačke za dati vektor
        public static Tacka operator +(Tacka A, Vektor V)
        {
            return new Tacka(A.X + V.X, A.Y + V.Y);
        }
        
        // Središte duži AB
        public static Tacka Srediste(Tacka A, Tacka B)
        {
            return new Tacka(0.5 * (A.x + B.x), 0.5 * (A.y + B.y));
        }

        public override string ToString()
        {
            return string.Format("({0,0:F3}, {1,0:F3})", X, Y);
        }
    }

    public class Vektor
    {
        private double x;
        private double y;
        public double X { get { return x; } }
        public double Y { get { return y; } }

        // kopija vektora v
        public Vektor(Vektor v) { x = v.x; y = v.y; }
        
        // vektor zadat koordinatama
        public Vektor(double _x, double _y) { x = _x; y = _y; }

        // vektor zadat dvema tačkama
        public Vektor(Tacka P, Tacka Q)
        {
            x = Q.X - P.X;
            y = Q.Y - P.Y;
        }

        // Dužina vektora
        public double Duzina()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        
        // Jedinični vektor istog smera
        public Vektor Ort() 
        {
            double d = Duzina();
            if (d == 0)
                throw new Exception("Ort nula-vektora");

            return new Vektor(X / d, Y / d);
        }

        public static Vektor operator +(Vektor a, Vektor b)
        {
            return new Vektor(a.X + b.X, a.Y + b.Y);
        }

        // binarni minus (operator vraća razliku vektora)
        public static Vektor operator -(Vektor a, Vektor b)
        {
            return new Vektor(a.X - b.X, a.Y - b.Y);
        }
        
        // unarni minus (operator vraća suprotan vektor)
        public static Vektor operator -(Vektor a)
        {
            return new Vektor(-a.X, -a.Y);
        }

        public static Vektor operator *(Vektor a, double n)
        {
            return new Vektor(n * a.X, n * a.Y);
        }

        public static Vektor operator *(double n, Vektor a)
        {
            return new Vektor(n * a.X, n * a.Y);
        }

        // Rotacija za ugao dat u radijanima, u pozitivnom (ccw) smeru 
        public Vektor Rotacija(double ugao)
        {
            double c = Math.Cos(ugao);
            double s = Math.Sin(ugao);
            return new Vektor(c * X - s * Y, s * X + c * Y);
        }
    }

    public class Prava
    {
        private Tacka tacka;
        private Vektor pravac;

        // prava zadata dvema tačkama
        public Prava(Tacka P, Tacka Q)
        {
            tacka = new Tacka(P.X, P.Y);
            pravac = new Vektor(P, Q);
        }

        // prava zadata tačkom i vektorom pravca
        public Prava(Tacka A, Vektor AB)
        {
            tacka = new Tacka(A.X, A.Y);
            pravac = new Vektor(AB);
        }

        // vektor pravca date prave
        public Vektor Pravac()
        {
            return new Vektor(pravac);
        }

        // vektor normalan na datoj pravoj
        public Vektor Normala()
        {
            return new Vektor(pravac.Y, -pravac.X);
        }

        // statički metod - simetrala date duži
        public static Prava Simetrala(Tacka A, Tacka B)
        {
            Tacka S = Tacka.Srediste(A, B);
            Vektor n = new Prava(A, B).Normala();
            return new Prava(S, n);
        }

        // presek ove prave sa datom pravom
        public Tacka Presek(Prava p)
        {
            // Za presečnu tačku vazi:
            // A.x + k AB.x == C.x + m CD.x
            // A.y + k AB.y == C.y + m CD.y

            // A.x * CD.y + k * AB.x * CD.y == C.x * CD.y + m * CD.x * CD.y
            // A.y * CD.x + k * AB.y * CD.x == C.y * CD.x + m * CD.y * CD.x
            // k * (AB.x * CD.y - AB.y * CD.x) == - A.x * CD.y + A.y * CD.x + C.x * CD.y - C.y * CD.x
            // k = (- A.x * CD.y + A.y * CD.x + C.x * CD.y - C.y * CD.x) / (AB.x * CD.y - AB.y * CD.x)

            double im = pravac.X * p.pravac.Y - pravac.Y * p.pravac.X;
            if (im == 0)
                throw new Exception("Presek paralelnih pravih");

            double k = (-tacka.X * p.pravac.Y + tacka.Y * p.pravac.X
                + p.tacka.X * p.pravac.Y - p.tacka.Y * p.pravac.X) / im;
            return tacka + k * pravac;
        }
    }
}