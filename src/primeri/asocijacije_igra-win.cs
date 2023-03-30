using System.Windows.Forms;
using AsocijacijeLib;

namespace AsocijacijeWin
{
    public partial class Form1 : Form
    {
        Asocijacije igra;
        int ukupnoPoena = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            Osvezi();
        }
        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                igra = Asocijacije.Kreiraj(openFileDialog1.FileName);
                Osvezi();
                lblPoruka.Text = "Отворите поље";
            }
        }
        private void btn_Click(object sender, System.EventArgs e)
        {
            if (igra == null)
            {
                MessageBox.Show("Прво треба да учитате игру");
                return;
            }

            Button b = sender as Button;
            string tag = (string)b.Tag;
            int kol = tag[0] - 'A';
            int polje = tag[1] - '1';
            if (igra.Otvori(kol, polje))
            {
                b.Text = igra[kol, polje];
                DozvoliUnos(true);
            }
        }

        private void tbABCD_Leave(object sender, System.EventArgs e)
        {
            if (igra == null)
            {
                MessageBox.Show("Прво треба да учитате игру");
                return;
            }

            TextBox tb = sender as TextBox;
            string tag = (string)tb.Tag;
            if (tag.Length == 1)
            {
                int kol = tag[0] - 'A';
                tb.Text = igra[kol];
            }
            else if (!igra.Reseno)
                tb.Text = tag;
        }

        private void tbABCD_Enter(object sender, System.EventArgs e)
        {
            if (igra == null)
            {
                MessageBox.Show("Прво треба да учитате игру");
                return;
            }

            TextBox tb = sender as TextBox;
            tb.Text = "";
        }

        private void tbABCD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (igra == null)
            {
                MessageBox.Show("Прво треба да учитате игру");
                return;
            }

            TextBox tb = sender as TextBox;
            string tag = (string)tb.Tag;
            int kol = tag[0] - 'A';
            if (e.KeyCode == Keys.Enter)
            {
                int poeni = (kol < igra.BrojKolona) ?
                    igra.Pokusaj(kol, tb.Text) :
                    igra.Pokusaj(tb.Text);

                if (poeni > 0)
                {
                    ukupnoPoena += poeni;
                    lblPoeni.Text = string.Format("Поени: {0}", ukupnoPoena);
                    Osvezi();
                }
                else
                {
                    tb.Text = tag;
                    DozvoliUnos(false); // posle promasaja mora da se otvori novo polje
                }

                this.ActiveControl = null;
            }
        }
        private void DozvoliUnos(bool dozvoli)
        {
            TextBox[] tb = { tbA, tbB, tbC, tbD };

            if (igra == null)
            {
                for (int kol = 0; kol < 4; kol++)
                    tb[kol].Enabled = dozvoli;

                tbKonacno.Enabled = dozvoli;
                return;
            }

            for (int kol = 0; kol < igra.BrojKolona; kol++)
                tb[kol].Enabled = dozvoli && !igra.ResenaKol(kol);

            tbKonacno.Enabled = dozvoli && !igra.Reseno;

            if (igra.Reseno)
                lblPoruka.Text = "Браво!";
            else if (dozvoli)
                lblPoruka.Text = "Погађајте";
            else
                lblPoruka.Text = "Отворите поље";
        }
        private void Osvezi()
        {
            Button[,] b = {
                { btnA1, btnA2, btnA3, btnA4 },
                { btnB1, btnB2, btnB3, btnB4 },
                { btnC1, btnC2, btnC3, btnC4 },
                { btnD1, btnD2, btnD3, btnD4 },
            };

            TextBox[] tb = { tbA, tbB, tbC, tbD };

            if (igra == null)
            {
                for (int kol = 0; kol < 4; kol++)
                {
                    for (int pojam = 0; pojam < 4; pojam++)
                    {
                        b[kol, pojam].Text = "";
                        b[kol, pojam].Enabled = false;
                    }
                }

                lblPoruka.Text = "Учитајте игру";
                DozvoliUnos(false);
                return;
            }

            for (int kol = 0; kol < igra.BrojKolona; kol++)
                for (int pojam = 0; pojam < igra.BrojPojmova; pojam++)
                {
                    b[kol, pojam].Enabled = true;
                    b[kol, pojam].Text = igra[kol, pojam];
                }

            for (int kol = 0; kol < igra.BrojKolona; kol++)
            {
                tb[kol].Text = igra[kol];
                if (igra.ResenaKol(kol))
                    tb[kol].Enabled = false;
            }

            if (igra.Reseno)
                DozvoliUnos(false);
        }
    }
}
