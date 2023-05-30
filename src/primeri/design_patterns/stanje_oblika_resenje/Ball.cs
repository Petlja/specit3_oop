namespace StanjeOblika
{
    class Ball : Shape
    {
        private float r;

        public Ball(float x, float y, float r, Color color) : base(x, y, color)
        {
            this.r = r;
        }

        override public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, x - r, y - r, 2 * r, 2 * r);
        }

        override protected bool ContainsPoint(int x, int y)
        {
            float dx = x - this.x;
            float dy = y - this.y;
            return (dx * dx + dy * dy <= this.r * this.r);
        }
    }
}
