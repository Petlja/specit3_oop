using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Kretanje
{
    public partial class Form1 : Form
    {
        private List<Avioncic> avioni = null;
        private List<Loptica> loptice = null;
        private List<Tocak> tockovi = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;

            avioni = new List<Avioncic>();
            for (int i = 0; i < 10; i++)
                avioni.Add(new Avioncic(w, h));

            loptice = new List<Loptica>();
            for (int i = 0; i < 10; i++)
                loptice.Add(new Loptica(w, h));

            tockovi = new List<Tocak>();
            for (int i = 0; i < 10; i++)
                tockovi.Add(new Tocak(w, h));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;

            foreach (Avioncic a in avioni)
                a.PomeriSe(w, h);

            foreach (Loptica l in loptice)
                l.PomeriSe(w, h);

            foreach (Tocak t in tockovi)
                t.PomeriSe(w, h);

            Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;

            foreach (Avioncic a in avioni)
                a.NacrtajSe(e.Graphics, w, h);

            foreach (Loptica l in loptice)
                l.NacrtajSe(e.Graphics, w, h);

            foreach (Tocak t in tockovi)
                t.NacrtajSe(e.Graphics, w, h);
        }
    }
}
