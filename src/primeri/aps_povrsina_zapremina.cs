using System;
using System.Collections.Generic;
using System.Drawing;

internal class Program
{
    public abstract class Figura2D
    {
        protected Color boja;
        public Figura2D(Color b) { boja = b; }
        public Color Boja { get { return boja; } }
        public abstract double Povrsina();
        public abstract double Obim();
        public abstract double RUpisanogKruga();
    }
    public class Krug : Figura2D
    {
        private double r;
        public Krug(Color b, double r0) : base(b) { r = r0; }
        public override double Povrsina() { return r * r * Math.PI; }
        public override double Obim() { return 2 * r * Math.PI; }
        public override double RUpisanogKruga() { return r; }
    }
    public class Ntougao : Figura2D
    {
        private double a;
        private int n;
        public Ntougao(Color b, double a0, int n0) : base(b) { a = a0; n = n0; }
        public override double Povrsina() { return n * 0.5 * a * RUpisanogKruga(); }
        public override double Obim() { return n * a; }
        public override double RUpisanogKruga() { return 0.5 * a / Math.Tan(Math.PI / n); }
    }

    public abstract class Figura3D
    {
        protected Figura2D osnova;
        protected double visina;
        public Figura3D(Figura2D b, double h)
        {
            osnova = b;
            visina = h;
        }
        public Color Boja { get { return osnova.Boja; } }
        public abstract double Zapremina();
        public abstract double Povrsina();
    }
    public class SpicastaFigura : Figura3D // kupa, piramida
    {
        public SpicastaFigura(Figura2D b, double h) : base(b, h) { }
        public override double Zapremina() { return osnova.Povrsina() * visina / 3; }
        public override double Povrsina()
        {
            double r = osnova.RUpisanogKruga();
            double s = Math.Sqrt(r * r + visina * visina); // izvodnica, ili visina bočne strane
            double b = osnova.Povrsina();
            double m = 0.5 * osnova.Obim() * s; // omotač
            return b + m;
        }
    }
    public class StubastaFigura : Figura3D // valjak, prizma
    {
        public StubastaFigura(Figura2D b, double h) : base(b, h) { }
        public override double Zapremina() { return osnova.Povrsina() * visina; }
        public override double Povrsina()
        {
            double b = osnova.Povrsina();
            double m = osnova.Obim() * visina; // omotac
            return b + b + m;
        }
    }
    static void Main(string[] args)
    {
        List<Figura2D> figure2D = new List<Figura2D>();
        figure2D.Add(new Krug(Color.Red, 5));        // crveni krug poluprecnika 5
        figure2D.Add(new Krug(Color.Green, 7));      // zeleni krug poluprecnika 7
        figure2D.Add(new Ntougao(Color.Red, 6, 4));  // crveni kvadrat stranice 6
        figure2D.Add(new Ntougao(Color.Blue, 2, 6)); // plavi šestougao stranice 2

        List<Figura3D> figure3D = new List<Figura3D>();
        figure3D.Add(new SpicastaFigura(figure2D[0], 12)); // crvena kupa
        figure3D.Add(new StubastaFigura(figure2D[1], 12)); // zeleni valjak
        figure3D.Add(new SpicastaFigura(figure2D[2], 10)); // crvena četvorostrana piramida
        figure3D.Add(new StubastaFigura(figure2D[3], 10)); // plava šestostrana prizma

        var povZap = new Dictionary<Color, Tuple<double, double>>();
        foreach (Figura3D f in figure3D)
        {
            double p = f.Povrsina(), v = f.Zapremina();
            Console.WriteLine("Figura: P={0:0.00}, V={1:0.00}", p, v);
            if (povZap.ContainsKey(f.Boja))
                povZap[f.Boja] = new Tuple<double, double>(
                    povZap[f.Boja].Item1 + p, povZap[f.Boja].Item2 + v);
            else
                povZap[f.Boja] = new Tuple<double, double>(p, v);
        }

        foreach (var kv in povZap)
        {
            Console.WriteLine("Za boju {0} ukupna povrsina je {1:0.00}, a zapremina {2:0.00}",
                kv.Key, kv.Value.Item1, kv.Value.Item2);
        }
    }
}
