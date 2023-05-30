namespace SprajtoviDveVrste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Sprajt> sprajtovi = new List<Sprajt>();
        private Random rnd = new Random();
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                sprajtovi.Add(new Loptica(e.X, e.Y, 15, rnd.Next(-3, 3), rnd.Next(-3, 3), Color.Red));
            else
                sprajtovi.Add(new Kvadratic(e.X, e.Y, 30, rnd.Next(-3, 3), rnd.Next(-3, 3), Color.Blue));
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Sprajt s in sprajtovi)
            {
                s.Nacrtaj(e.Graphics);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Sprajt s in sprajtovi)
            {
                s.Pomeri(pictureBox1.Width, pictureBox1.Height);
            }

            // uklanjamo sve sprajtove koji se sudaraju sa drugima
            sprajtovi = sprajtovi.Where(sprajt => 
                !sprajtovi.Any(drugi => drugi != sprajt && sprajt.SudaraSe(drugi))).ToList();

            Refresh();
        }
    }
}