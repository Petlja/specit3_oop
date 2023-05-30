using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UndoLista
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Stack<Komanda> komande = new Stack<Komanda>();
        private Stack<Komanda> redoKomande = new Stack<Komanda>();

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            Komanda komanda = new KomandaDodaj(listBox1.Items, textBox1.Text);
            komanda.Izvrsi();
            komande.Push(komanda);
            redoKomande.Clear();
        }

        private void buttonPromeni_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Komanda komanda = new KomandaPromeni(listBox1.Items, listBox1.SelectedIndex, textBox1.Text);
                komanda.Izvrsi();
                komande.Push(komanda);
                redoKomande.Clear();
            }
        }

        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Komanda komanda = new KomandaObrisi(listBox1.Items, listBox1.SelectedIndex);
                komanda.Izvrsi();
                komande.Push(komanda);
                redoKomande.Clear();
            }
        }

        private void buttonPonisti_Click(object sender, EventArgs e)
        {
            if (komande.Count > 0)
            {
                Komanda komanda = komande.Pop();
                komanda.Ponisti();
                redoKomande.Push(komanda);
            }
        }

        private void buttonVrati_Click(object sender, EventArgs e)
        {
            if (redoKomande.Count > 0)
            {
                Komanda komanda = redoKomande.Pop();
                komanda.Izvrsi();
                komande.Push(komanda);
            }
        }
    }
}