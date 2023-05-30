namespace StanjeOblika
{
    public partial class Form1 : Form
    {
        List<Shape> shapes = new List<Shape>();
        public Form1()
        {
            InitializeComponent();

            float x1 = pictureBox1.Width / 4.0f;
            float x2 = 2 * x1;
            float x3 = 3 * x1;
            float y = pictureBox1.Height / 2.0f;

            shapes.AddRange(new Shape[] {
                new Ball(x1, y, 20, Color.Red),
                new Line(x2 - 30, y - 50, x2 + 30, y + 50, Color.Green),
                new Rectangle(x3, y, 30, 20, Color.Blue)
            });
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape sprite in shapes)
                sprite.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Shape sprite in shapes)
                sprite.Update();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int button = 0;
            if (e.Button == MouseButtons.Left) button = 1;
            if (e.Button == MouseButtons.Right) button = 2;

            foreach (Shape sprite in shapes)
                sprite.OnClick(button, e.X, e.Y);
        }
    }
}