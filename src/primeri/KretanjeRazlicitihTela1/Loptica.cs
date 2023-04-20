using System;
using System.Drawing;

namespace Kretanje
{
    public class Loptica
    {
        private static Random rnd = new Random();
        private float x, y, vx, vy;
        private Color boja;
        private float r;
        public Loptica(int w, int h)
        {
            RestartujSe(w, h);
        }
        public void RestartujSe(int w, int h)
        {
            x = rnd.Next(w);
            y = rnd.Next(h);
            vx = rnd.Next(150, w) * 0.01f;
            vy = rnd.Next(h) * 0.01f;
            r = rnd.Next(20, 50);

            int gc = rnd.Next(192, 256);
            int rc = rnd.Next(gc/4) + (rnd.Next(2) == 1 ? gc/2 : 0);
            int bc = gc - rc;
            boja = Color.FromArgb(rc, gc, bc);
        }
        public bool PomeriSe(int w, int h)
        {
            const float a = 10;
            vy = vy + a;
            x = x + vx;
            y = y + vy;
            if (y > h - r)
            {
                y = h - r - (y + r - h);
                vy = -vy;
            }
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
            return true;
        }
        public void NacrtajSe(Graphics g, int w, int h)
        {
            Brush cetka = new SolidBrush(boja);
            g.FillEllipse(cetka, x - r, y - r, 2 * r, 2 * r);
        }
    }
}
