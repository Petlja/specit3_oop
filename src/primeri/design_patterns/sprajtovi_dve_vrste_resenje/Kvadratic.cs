namespace SprajtoviDveVrste
{
    class Kvadratic : Sprajt
    {
        private int a;
        public Kvadratic(int x, int y, int a, int vx, int vy, Color boja)
            : base(x, y, vx, vy, boja)
        {
            this.a = a;
        }

        public int A { get { return a; } }

        public override bool SudaraSe(Sprajt druga)
        {
            return druga.SudaraSeSaKvadraticem(this);
        }

        public override bool SudaraSeSaLopticom(Loptica druga)
        {
            return druga.SudaraSeSaKvadraticem(this);
        }

        static bool PostojiPresekIntervala(int a1, int b1, int a2, int b2)
        {
            return Math.Max(a1, a2) < Math.Min(b1, b2);
        }

        public override bool SudaraSeSaKvadraticem(Kvadratic drugi)
        {
            return PostojiPresekIntervala(this.x - this.a / 2, this.x + this.a / 2,
                drugi.x - drugi.a / 2, drugi.x + drugi.a / 2) &&
                PostojiPresekIntervala(this.y - this.a / 2, this.y + this.a / 2,
                drugi.y - drugi.a / 2, drugi.y + drugi.a / 2);
        }

        public override bool uProzoruX(int sirinaProzora)
        {
            return this.x - this.a / 2 >= 0 && this.x + this.a / 2 < sirinaProzora;
        }

        public override bool uProzoruY(int visinaProzora)
        {
            return this.y - this.a / 2 >= 0 && this.y + this.a / 2 < visinaProzora;
        }

        public override void Nacrtaj(Graphics g)
        {
            g.FillRectangle(cetka, this.x - this.a / 2, this.y - this.a / 2, a, a);
        }
    }
}
