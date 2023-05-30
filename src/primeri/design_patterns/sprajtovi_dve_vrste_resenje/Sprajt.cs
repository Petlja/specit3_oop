using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SprajtoviDveVrste
{
    internal abstract class Sprajt
    {
        protected int x, y;
        protected int vx, vy;        
        protected Color boja;
        protected Brush cetka;

        public Sprajt(int x, int y, int vx, int vy, Color boja)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.boja = boja;
            this.cetka = new SolidBrush(this.boja);
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public abstract void Nacrtaj(Graphics g);

        public void Pomeri(int sirinaProzora, int visinaProzora)
        {
            this.x += this.vx;
            this.y += this.vy;

            if (!uProzoruX(sirinaProzora))
                this.vx = -this.vx;
            if (!uProzoruY(visinaProzora))
                this.vy = -this.vy;
        }
        public static double Rastojanje(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public abstract bool uProzoruX(int sirinaProzora);
        public abstract bool uProzoruY(int visinaProzora);

        public abstract bool SudaraSe(Sprajt drugi);
        public abstract bool SudaraSeSaLopticom(Loptica drugi);
        public abstract bool SudaraSeSaKvadraticem(Kvadratic drugi);
    }
}
