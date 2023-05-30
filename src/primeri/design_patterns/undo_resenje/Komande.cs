using System.Windows.Forms;

namespace UndoLista
{
    interface Komanda
    {
        public void Izvrsi();
        public void Ponisti();
    }

    class KomandaDodaj : Komanda
    {
        private ListBox.ObjectCollection kolekcija;
        private string s;
        public KomandaDodaj(ListBox.ObjectCollection kolekcija, string s)
        {
            this.kolekcija = kolekcija;
            this.s = s;
        }

        public void Izvrsi()
        {
            this.kolekcija.Add(this.s);
        }

        public void Ponisti()
        {
            this.kolekcija.RemoveAt(this.kolekcija.Count - 1);
        }
    }

    class KomandaPromeni : Komanda
    {
        private ListBox.ObjectCollection kolekcija;
        private string stari, novi;
        private int pozicija;

        public KomandaPromeni(ListBox.ObjectCollection kolekcija, int pozicija, string novi)
        {
            this.kolekcija = kolekcija;
            this.pozicija = pozicija;
            this.novi = novi;
            this.stari = "";
        }

        public void Izvrsi()
        {
            this.stari = (string)this.kolekcija[this.pozicija];
            this.kolekcija[this.pozicija] = this.novi;
        }

        public void Ponisti()
        {
            this.kolekcija[this.pozicija] = this.stari;
        }
    }

    public class KomandaObrisi : Komanda
    {
        private ListBox.ObjectCollection kolekcija;
        private string stari;
        private int pozicija;

        public KomandaObrisi(ListBox.ObjectCollection kolekcija, int pozicija)
        {
            this.kolekcija = kolekcija;
            this.pozicija = pozicija;
            stari = "";
        }

        public void Izvrsi()
        {
            stari = (string)kolekcija[pozicija];
            kolekcija.RemoveAt(pozicija);
        }

        public void Ponisti()
        {
            kolekcija.Insert(pozicija, stari);
        }
    }
}
