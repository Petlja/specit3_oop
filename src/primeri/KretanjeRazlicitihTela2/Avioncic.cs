using System;
using System.Drawing;

namespace Kretanje
{
    public class Avioncic : Telo
    {
        PointF[] temena = new PointF[3];
        float duzina, visina;
        int pauza;
        public Avioncic(int w, int h)
        {
            RestartujSe(w, h);
        }

        public override void RestartujSe(int w, int h)
        {
            x = rnd.Next(w);
            y = rnd.Next(h);
            vx = rnd.Next(150, w) * 0.1f;
            vy = rnd.Next(h) * 0.01f;

            int d = rnd.Next(80, 200);
            duzina = d;
            visina = rnd.Next(d / 10, d / 5);

            int bc = rnd.Next(100, 256);
            int gc = bc;
            int rc = bc * 9 / 10;
            boja = Color.FromArgb(rc, gc, bc);

            pauza = 0;
        }
        public override bool PomeriSe(int w, int h)
        {
            if (pauza > 0)
            {
                pauza--;
                if (pauza > 0)
                    return false;
                else
                {
                    RestartujSe(w, h);
                    return true;
                }
            }
            else 
            {
                if (x < w)
                {
                    pauza = 0;
                    x = x + vx;
                    vy += rnd.Next(-h, h) * 0.01f;
                    vy = Math.Max(vy, -0.1f * h);
                    vy = Math.Min(vy, 0.1f * h);
                    y += vy;
                    y = Math.Max(y, 0.01f * h);
                    y = Math.Min(y, 0.99f * h);
                    return true;
                }
                else
                {
                    pauza = 10;
                    return false;
                }
            }
        }
        public override void NacrtajSe(Graphics g, int w, int h)
        {
            temena[0].X = x; 
            temena[0].Y = y;
            
            temena[1].X = x - duzina; 
            temena[1].Y = y - visina / 2;
            
            temena[2].X = x - duzina; 
            temena[2].Y = y + visina / 2;

            Brush cetka = new SolidBrush(boja);
            g.FillPolygon(cetka, temena);
        }
    }
}
