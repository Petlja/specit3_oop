namespace StanjeOblika
{
    class Rectangle : Shape
    {
        private float w, h;

        public Rectangle(float x, float y, float w, float h, Color color) : base(x, y, color)
        {
            this.w = w;
            this.h = h;
        }
        override public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(color);
            g.FillRectangle(brush, x, y, w, h);
        }
        override protected bool ContainsPoint(int x, int y)
        {
            return (this.x <= x && x < this.x + this.w &&
                this.y <= y && y < this.y + this.h);
        }
    }
}
