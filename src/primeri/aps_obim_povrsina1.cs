using System;
using System.Collections.Generic;
using System.Drawing;

internal class Program
{
    public class Krug
    {
        private Color boja; // boja kruga
        private double r; // poluprečnik kruga

        public Krug(Color b, double r0) { boja = b; r = r0; }
        public double Povrsina() { return r * r * Math.PI; }
        public double Obim() { return 2 * r * Math.PI; }
        public Color Boja { get { return boja; } }
    }
    public class Ntougao
    {
        private Color boja;
        private double a; // dužina stranice mnogougla
        private int n; // broj stranica mnogougla

        public Ntougao(Color b, double a0, int n0) { boja = b; a = a0; n = n0; }

        public double Povrsina()
        {
            double rUpisanogKruga = 0.5 * a / Math.Tan(Math.PI / n);
            return n * 0.5 * a * rUpisanogKruga;
        }

        public double Obim() { return n * a; }

        public Color Boja { get { return boja; } }
    }

    static void Main(string[] args)
    {
        List<Krug> krugovi = new List<Krug>();
        krugovi.Add(new Krug(Color.Red, 5));        // crveni krug poluprečnika 5
        krugovi.Add(new Krug(Color.Green, 7));      // zeleni krug poluprečnika 7
        List<Ntougao> mnogouglovi = new List<Ntougao>();
        mnogouglovi.Add(new Ntougao(Color.Red, 6, 4));  // crveni kvadrat stranice 6
        mnogouglovi.Add(new Ntougao(Color.Blue, 2, 6)); // plavi šestougao stranice 2

        // ukupna površina svake boje koja se pojavi
        Dictionary<Color, double> povBoje = new Dictionary<Color, double>();

        // za svaki krug
        foreach (Krug k in krugovi)
        {
            Console.WriteLine("Figura: Boja={0}, Obim={1:0.00}, Povrsina={2:0.00}",
                k.Boja, k.Obim(), k.Povrsina());

            double prethPov = 0;
            povBoje.TryGetValue(k.Boja, out prethPov);
            povBoje[k.Boja] = prethPov + k.Povrsina();
        }
        
        // za svaki mnogougao
        foreach (Ntougao p in mnogouglovi)
        {
            Console.WriteLine("Figura: Boja={0}, Obim={1:0.00}, Povrsina={2:0.00}",
                p.Boja, p.Obim(), p.Povrsina());

            double prethPov = 0;
            povBoje.TryGetValue(p.Boja, out prethPov);
            povBoje[p.Boja] = prethPov + p.Povrsina();
        }

        foreach (KeyValuePair<Color, double> kv in povBoje)
        {
            Console.WriteLine("Ukupna povrsina boje {0} je {1:0.00}", kv.Key, kv.Value);
        }
    }
}
