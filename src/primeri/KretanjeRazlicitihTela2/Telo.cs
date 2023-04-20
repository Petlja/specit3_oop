using System;
using System.Drawing;

namespace Kretanje
{
    abstract public class Telo
    {
        protected static Random rnd = new Random();

        // Trenutni polozaj i brzina
        protected float x, y, vx, vy;
        protected Color boja;

        private enum VrstaTela { Loptica, Tocak, Avioncic, BrojVrsta };
        static public Telo Kreiraj(int w, int h, int vrsta)
        {
        VrstaTela t = (VrstaTela)(vrsta % (int)VrstaTela.BrojVrsta);
            switch (t)
            {
                case VrstaTela.Loptica:
                    return new Loptica(w, h);
                case VrstaTela.Tocak:
                    return new Tocak(w, h);
                case VrstaTela.Avioncic:
                    return new Avioncic(w, h);
                default:
                    return null;
            }
        }

        abstract public bool PomeriSe(int w, int h);
        abstract public void RestartujSe(int w, int h);
        abstract public void NacrtajSe(Graphics g, int w, int h);

    }

}
