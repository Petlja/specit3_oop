namespace SprajtoviDveVrste
{
    internal class Loptica : Sprajt
    {
        private int r;

        public Loptica(int x, int y, int r, int vx, int vy, Color boja)
            : base(x, y, vx, vy, boja)
        {
            this.r = r;
        }

        public int R { get { return this.r; } }

        public override void Nacrtaj(Graphics g)
        {
            g.FillEllipse(cetka, x - r, y - r, 2 * r, 2 * r);
        }

        public override bool uProzoruX(int sirinaProzora)
        {
            return this.x + this.r < sirinaProzora && this.x - this.r >= 0;
        }

        public override bool uProzoruY(int visinaProzora)
        {
            return this.y + this.r < visinaProzora && this.y - this.r >= 0;
        }

        public override bool SudaraSe(Sprajt drugi)
        {
            return drugi.SudaraSeSaLopticom(this);
        }

        public override bool SudaraSeSaLopticom(Loptica drugi)
        {
            return Sprajt.Rastojanje(this.x, this.y, drugi.x, drugi.y) <= this.r + drugi.r;
        }

        public override bool SudaraSeSaKvadraticem(Kvadratic drugi)
        {
            int dx = x - Math.Max(drugi.X - drugi.A / 2, Math.Min(x, drugi.X + drugi.A / 2));
            int dy = y - Math.Max(drugi.Y - drugi.A / 2, Math.Min(y, drugi.Y + drugi.A / 2));
            return dx * dx + dy * dy < r * r;
        }
    }
}
