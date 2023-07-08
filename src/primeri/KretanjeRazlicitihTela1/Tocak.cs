using System;
using System.Drawing;

namespace Kretanje
{
    public class Tocak
    {
        private static Random rnd = new Random();
        private float x, y, vx, vy; // Trenutni položaj i brzina
        private Color boja;
        private float r;
        private float rMalo;
        private float ugao;
        private int brPrecnika;
        public Tocak(int w, int h)
        {
            RestartujSe(w, h);
        }
        public void RestartujSe(int w, int h)
        {
            x = rnd.Next(w);
            y = rnd.Next(h);
            r = rnd.Next(20, 100);
            rMalo = r / rnd.Next(3, 6);
            y = h - r;
            vx = rnd.Next(150, w) * 0.01f;
            vy = 0;
            ugao = 0;

            brPrecnika = rnd.Next(3, 7);

            int rc = rnd.Next(128, 256);
            int gc = rnd.Next(rc / 3, 2 * rc / 3);
            int bc = rnd.Next(rc / 5);
            boja = Color.FromArgb(rc, gc, bc);
        }

        public bool PomeriSe(int w, int h)
        {
            x = x + vx;
            if (x < r)
            {
                x = r + (r - x);
                vx = -vx;
            }
            else if (x > w - r)
            {
                x = w - r - (x + r - w);
                vx = -vx;
            }
            ugao = ugao + vx / r;
            return true;
        }
        public void NacrtajSe(Graphics g, int w, int h)
        {
            Pen olovka = new Pen(boja, r / 8);
            g.DrawEllipse(olovka, x - r, y - r, 2 * r, 2 * r);
            float dUgao = (float)(Math.PI / brPrecnika);
            for (int i = 0; i < brPrecnika; i++)
            {
                float x0 = r * (float)Math.Cos(ugao + i * dUgao);
                float y0 = r * (float)Math.Sin(ugao + i * dUgao);
                g.DrawLine(olovka, x - x0, y - y0, x + x0, y + y0);
            }

            Brush cetka = new SolidBrush(boja);
            g.FillEllipse(cetka, x - rMalo, y - rMalo, 2 * rMalo, 2 * rMalo);
        }
    }
}
