namespace StanjeOblika
{
    class Line : Shape
    {
        private float w, h;

        public Line(float x1, float y1, float x2, float y2, Color color) : base(x1, y1, color)
        {
            w = x2 - x1;
            h = y2 - y1;
        }
        override public void Draw(Graphics g)
        {
            Pen pen = new Pen(color, 3);
            g.DrawLine(pen, x, y, x + w, y + h);
        }
        override protected bool ContainsPoint(int x, int y)
        {
            float x1 = this.x, y1 = this.y, x2 = x1 + w, y2 = y1 + h;

            // a je dvostruka povrsina trougla 
            // b je kvadrat duzine osnovice
            // rastojanje d = a/sqrt(b)
            // uslov a * a < b * 9 vazi za d < 5

            float a = MathF.Abs((x2 - x1) * (y1 - y) - (x1 - x) * (y2 - y1));
            float b = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            return (a * a < b * 25);
        }
    }
}
