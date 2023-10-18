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
            Osvezi(); // ažuriraj tekstove na dugmadi i poljima za pogađanje
        }
        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // ako je izabran fajl, kreiraj igru
                igra = Asocijacije.Kreiraj(openFileDialog1.FileName);
                Osvezi(); // ažuriraj tekstove 
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

            // nađi koordinate dugmeta na koje je kliknuto
            Button b = sender as Button;
            string tag = (string)b.Tag;
            int kol = tag[0] - 'A';
            int polje = tag[1] - '1';
            
            if (igra.Otvori(kol, polje))
            {
                // ako je polje otvoreno, ažuriraj tekst na dugmetu
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
            // polje za pogađanje je napušteno bez pritiska na Enter,
            // pa treba da se vrati naziv polja
            TextBox tb = sender as TextBox;
            string tag = (string)tb.Tag;
            if (tag.Length == 1)
            {
                // ako je tag samo jedno slovo, radi se o koloni
                int kol = tag[0] - 'A';
                tb.Text = igra[kol];
            }
            else if (!igra.Reseno) // u protivnom, to je polje za konačno rešenje
                tb.Text = tag;
        }

        private void tbABCD_Enter(object sender, System.EventArgs e)
        {
            if (igra == null)
            {
                MessageBox.Show("Прво треба да учитате игру");
                return;
            }

            // pri ulasku u polje brišemo tekst, da bi korisnik mogao da pogađa
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
                // pokušaj da pogodiš kolonu ili konačno rešenje
                int poeni = (kol < igra.BrojKolona) ?
                    igra.Pokusaj(kol, tb.Text) : // kolona
                    igra.Pokusaj(tb.Text); // konačno rešenje

                if (poeni > 0)
                {
                    // pogođena kolona ili konačno rešenje
                    ukupnoPoena += poeni;
                    lblPoeni.Text = string.Format("Поени: {0}", ukupnoPoena);
                    Osvezi(); // ažuriraj tekstove 
                }
                else
                {
                    // neuspeo pokušaj, zabrajuje se pogađanje
                    // (mora da se otvori novo polje)
                    DozvoliUnos(false);
                }
                // posle pritiska na Enter polje prestaje da bude u fokusu
                this.ActiveControl = null;
            }
        }

        // omogućava ili onemogućava upis u polja za pogađanje
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

        // ažurira tekstove na dugmadi i poljima za pogađanje
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
            
            // dugmad
            for (int kol = 0; kol < igra.BrojKolona; kol++)
                for (int pojam = 0; pojam < igra.BrojPojmova; pojam++)
                {
                    b[kol, pojam].Enabled = true;
                    b[kol, pojam].Text = igra[kol, pojam];
                }

            // polja za pogađanje
            for (int kol = 0; kol < igra.BrojKolona; kol++)
            {
                tb[kol].Text = igra[kol];
                if (igra.ResenaKol(kol))
                    tb[kol].Enabled = false;
            }

            // konačno rešenje
            if (igra.Reseno)
                DozvoliUnos(false);
        }
    }
}
