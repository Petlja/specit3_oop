namespace DekorisaniOblici
{
    public partial class Form1 : Form
    {
        SpriteManager spriteManager = new SpriteManager();

        public Form1()
        {
            InitializeComponent();
            spriteManager.AddSprite(new Rectangle(200, 300, 20, 50, Color.Blue));
            spriteManager.AddSprite(new Circle(400, 100, 30, Color.Red));
            spriteManager.AddSprite(new Blinking(new Circle(300, 150, 20, Color.Green), 50));
            spriteManager.AddSprite(new Moving(new Rectangle(30, 20, 10, 10, Color.Purple), 1, 2));
            spriteManager.AddSprite(new Blinking(new Moving(new Rectangle(500, 400, 10, 20, Color.Orange), -1, -2), 20));
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            spriteManager.Update();
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            spriteManager.Draw(e.Graphics);
        }
    }
}