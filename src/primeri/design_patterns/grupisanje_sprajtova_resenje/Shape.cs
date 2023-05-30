namespace grupisanje
{
    public abstract class Shape : Sprite
    {
        protected float x, y;
        protected Color color;
        public Shape(float x, float y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
        public override IEnumerable<Sprite> SubSprites()
        {
            yield break;
        }
        public override float X { get { return x; } }
        public override float Y { get { return y; } }
        public override bool Contains(float x, float y)
        {
            return (this.x < x) && (this.y < y) && (x < this.x + Width) && (y < this.y + Height);
        }
        public override void Move(float dx, float dy)
        {
            x += dx;
            y += dy;
        }
    }
    class CircleShape : Shape
    {
        private float r;
        private Brush brush;
        public CircleShape(float xc, float yc, float r, Color color)
            : base(xc - r, yc - r, color)
        {
            this.r = r;
            this.brush = new SolidBrush(Color.FromArgb(192, color));
        }
        override public float Width { get { return 2 * r; } }
        override public float Height { get { return 2 * r; } }
        override public void Draw(Graphics g)
        {
            g.FillEllipse(brush, X, Y, Width, Height);
        }
    }

    class RectangleShape : Shape
    {
        private float width, height;
        private Brush brush;

        public RectangleShape(float x, float y, float width, float height, Color color)
            : base(x, y, color)
        {
            this.width = width;
            this.height = height;
            this.brush = new SolidBrush(Color.FromArgb(192, color));
        }
        public override float Width { get { return width; } }
        public override float Height { get { return height; } }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(brush, x, y, width, height);
        }
    }
    class LineShape : Shape
    {
        private float dx, dy;
        private Pen pen;

        public LineShape(float x1, float y1, float x2, float y2, Color color, int w)
            : base(Math.Min(x1, x2), Math.Min(y1, y2), color)
        {
            this.dx = Math.Abs(x1 - x2);
            this.dy = Math.Abs(y1 - y2);
            this.pen = new Pen(color, w);
        }
        public override float Width { get { return dx; } }
        public override float Height { get { return dy; } }
        public override void Draw(Graphics g)
        {
            g.DrawLine(pen, x, y, x + dx, y + dy);
        }
    }
}
