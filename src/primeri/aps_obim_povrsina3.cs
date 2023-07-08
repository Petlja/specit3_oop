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
    }
    public class Krug : Figura2D
    {
        private double r;
        public Krug(Color b, double r0) : base(b) { r = r0; }
        public override double Povrsina() { return r * r * Math.PI; }
        public override double Obim() { return 2 * r * Math.PI; }
    }
    public class Ntougao : Figura2D
    {
        private double a;
        private int n;
        public Ntougao(Color b, double a0, int n0) : base(b) { a = a0; n = n0; }
        public override double Povrsina()
        {
            double rUpisanogKruga = 0.5 * a / Math.Tan(Math.PI / n);
            return n * 0.5 * a * rUpisanogKruga;
        }
        public override double Obim() { return n * a; }
    }

    static void Main(string[] args)
    {
        List<Figura2D> figure = new List<Figura2D>();
        figure.Add(new Krug(Color.Red, 5));        // crveni krug poluprecnika 5
        figure.Add(new Krug(Color.Green, 7));      // zeleni krug poluprecnika 7
        figure.Add(new Ntougao(Color.Red, 6, 4));  // crveni kvadrat stranice 6
        figure.Add(new Ntougao(Color.Blue, 2, 6)); // plavi šestougao stranice 2

        Dictionary<Color, double> povBoje = new Dictionary<Color, double>();
        foreach (Figura2D f in figure)
        {
            Console.WriteLine("Figura: Boja={0}, Obim={1:0.00}, Povrsina={2:0.00}",
                f.Boja, f.Obim(), f.Povrsina());

            double prethPov = 0;
            povBoje.TryGetValue(f.Boja, out prethPov);
            povBoje[f.Boja] = prethPov + f.Povrsina();
        }

        foreach (KeyValuePair<Color, double> kv in povBoje)
        {
            Console.WriteLine("Ukupna povrsina boje {0} je {1:0.00}", kv.Key, kv.Value);
        }
    }
}
