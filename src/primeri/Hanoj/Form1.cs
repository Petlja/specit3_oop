using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Hanoj
{
    public partial class Form1 : Form
    {
        // lista od tri stapa koja opisuje trenutno stanje 
        List<Stack<int>> stap; 

        // nabrajac kolekcije koraka koja predstavlja resenje
        IEnumerator<Tuple<int, int>> koraciResenja;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
            Reset();
        }

        void Reset()
        {
            // postavljamo stapove u pocetno stanje

            Stack<int> A = new Stack<int>();
            Stack<int> B = new Stack<int>();
            Stack<int> C = new Stack<int>();
            // na prvi stap stavljamo sve diskove od najveceg do najmanjeg
            int n = (int)numericUpDown1.Value;
            for (int i = n; i > 0; i--)
                A.Push(i);

            // formiramo listu stapova
            stap = new List<Stack<int>>() { A, B, C };
            Invalidate();
        }

        private void btnResi_Click(object sender, EventArgs e)
        {
            // inicijalizujemo resavanje, tako sto kreiramo nabrajac i ukljucimo tajmer
            numericUpDown1.Enabled = false;
            btnResi.Enabled = false;
            Reset();
            int n = (int)numericUpDown1.Value;
            koraciResenja = HanojskeKule(n, 0, 1, 2).GetEnumerator();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ako nisu izvrseni svi koraci
            if (koraciResenja.MoveNext())
            {
                // izvrsavamo jedan korak resenja
                var t = koraciResenja.Current;
                int disk = stap[t.Item1].Pop();
                stap[t.Item2].Push(disk);
                Invalidate();
            }
            else
            {
                // kraj postupka, azuriramo korisnicki interfejs
                numericUpDown1.Enabled = true;
                btnResi.Enabled = true;
                timer1.Enabled = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (stap == null)
                return;

            Graphics g = e.Graphics;
            Brush b = new SolidBrush(Color.Yellow);
            Pen p = new Pen(Color.Black, 2);

            int w = ClientSize.Width, h = ClientSize.Height;
            int n = (int)numericUpDown1.Value;
            int r0 = w / (n * 7), hDiska = h / (n * 2);

            int x0 = w / 6;
            for (int i = 0; i < 3; i++) // za svaki stap
            {
                int y0 = h - hDiska * stap[i].Count;
                foreach (int d in stap[i]) // za svaki disk na stapu
                {
                    g.FillRectangle(b, x0 - r0 * d, y0, 2 * r0 * d, hDiska);
                    g.DrawRectangle(p, x0 - r0 * d, y0, 2 * r0 * d, hDiska);
                    y0 += hDiska;
                }
                x0 += w / 3;
            }
        }

        // Rekurzivno resenje problema, koje vraca kolekciju poteza (potez je par brojeva)
        static IEnumerable<Tuple<int, int>> HanojskeKule(
            int n, int poc, int pom, int kraj)
        {
            if (n > 0)
            {
                foreach (var x in HanojskeKule(n - 1, poc, kraj, pom))
                    yield return x;

                yield return new Tuple<int, int>(poc, kraj);

                foreach (var x in HanojskeKule(n - 1, pom, poc, kraj))
                    yield return x;
            }
        }

    }
}
