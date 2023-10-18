namespace DekorisaniOblici
{
    // klasa koja se prema svojim korisnicima i sama ponaša kao sprajt, i to 
    // sprajt koji predstavlja malo izmenjenu verziju sprajta sadržanog u njoj 
    // SpriteDecorator je bazna klasa za svaki stvaran dekorator internog sprajta
    abstract class SpriteDecorator : Sprite
    {
        protected Sprite sprite;
        public SpriteDecorator(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public virtual float X { get { return sprite.X; } set { sprite.X = value;  } }
        public virtual float Y { get { return sprite.Y; } set { sprite.Y = value; } }

        public virtual void Draw(Graphics g) {
            sprite.Draw(g);
        }

        public virtual void Update() {
            sprite.Update();
        }
    }
    
    // jedna vrsta dekoracije sprajta je njegovo treptanje 
    class Blinking : SpriteDecorator
    {
        int ticks; // broj tikova (frejmova) od početka rada
        int period; // ukupan broj tikova koji čine jedan treptaj

        public Blinking(Sprite s, int period)
            : base(s)
        {
            this.ticks = 0;
            this.period = period;
        }

        public override void Draw(Graphics g)
        {
            // ako smo u prvoj polovini perioda, sprajt se crta
            if ((ticks / period) % 2 == 0)
                sprite.Draw(g);
        }

        public override void Update()
        {
            ticks++;
            sprite.Update();
        }
    }

    // jedna vrsta dekoracije sprajta je njegovo pomeranje
    class Moving : SpriteDecorator
    {
        private float dx, dy;
        public Moving(Sprite s, float dx, float dy)
            : base(s)
        {
            this.dx = dx;
            this.dy = dy;
        }

        public override void Update()
        {
            sprite.X += dx;
            sprite.Y += dy;
            sprite.Update();
        }
    }
}
