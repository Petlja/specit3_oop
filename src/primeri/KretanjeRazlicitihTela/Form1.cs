using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Kretanje
{
    public partial class Form1 : Form
    {
        private List<Telo> tela = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            tela = new List<Telo>();
            for (int i = 0; i < 30; i++)
                tela.Add(Telo.Kreiraj(w, h));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            foreach (Telo t in tela)
                t.PomeriSe(w, h);

            Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            foreach (Telo t in tela)
                t.NacrtajSe(e.Graphics, w, h);
        }
    }
}
