using System;

class CoveceNeLjutiSe
{
    public enum StatusPoteza { NijeOdigran, Odigran, Pobednicki };
    public enum Figura { R = 0, G, Y, B, Prazno };
    public enum Polje { CrvenaKucica = 0, ZelenaKucica, ZutaKucica, PlavaKucica, Obicno };
    private string[] ime = { "R", "G", "Y", "B" };
    private int[] tabla; // -1 prazno, igraci su 0, 1, 2, 3
    private int[] brFiguraUIgri; // ... za svakog igraca
    private Random rnd;
    private const int maxBrFigura = 4; // maks. broj figura po igracu
    private int brIgraca; // ukupan broj igraca
    private int naRedu; // indeks igraca koji je na redu
    private int kockica; // broj koji je pao na kockici
    private bool bacioKockicu; // da pazimo da se baca i igra *naizmenicno*
    private bool igraJeZavrsena;
    public CoveceNeLjutiSe(int brIgr)
    {
        // najmanje 2, a najvise 4 igraca
        brIgraca = Math.Max(2, Math.Min(4, brIgr));

        rnd = new Random();
        tabla = new int[brIgraca * 10];
        for (int i = 0; i < tabla.Length; i++)
            tabla[i] = -1;

        brFiguraUIgri = new int[brIgraca];
        naRedu = rnd.Next(brIgraca);
    }
    public Figura this[int i]
    {
        get
        {
            if (tabla[i] >= 0)
                return (Figura)tabla[i];
            return Figura.Prazno;
        }
    }
    public Polje VrstaPolja(int i)
    {
        if (i % 10 == 0 && i < 10 * brIgraca)
            return (Polje)(i / 10);
        else
            return Polje.Obicno;
    }
    public string ImeIgracaNaRedu { get { return ime[naRedu]; } }
    public string ImePobednika
    {
        get { return igraJeZavrsena ? ime[naRedu] : ""; }
    }
    public int BrojPolja { get { return tabla.Length; } }
    public bool MozeNovaFigura
    {
        get
        {
            int kucica = 10 * naRedu;
            return bacioKockicu &&
                kockica == 6 &&
                brFiguraUIgri[naRedu] < maxBrFigura &&
                tabla[kucica] != naRedu;
        }
    }
    public bool MozeDaOdigra
    {
        get
        {
            return bacioKockicu && (brFiguraUIgri[naRedu] > 0 || MozeNovaFigura);
        }
    }
    public int BaciKocku()
    {
        if (bacioKockicu)
            return 0; // ignorisano, mora prvo tekuci igrac da odigra

        kockica = rnd.Next(6) + 1;
        bacioKockicu = true;
        if (!MozeDaOdigra)
        {
            // nema figuru u igri i nije 6, gubi potez
            naRedu = (naRedu + 1) % brIgraca; // na redu je sledeci
            bacioKockicu = false; // sledeci treba da baca
        }
        return kockica;
    }
    public StatusPoteza IgrajSaPolja(int i)
    {
        if (i < 0 || i >= BrojPolja)
            return StatusPoteza.NijeOdigran; // ... jer to nije polje
        if (!bacioKockicu)
            return StatusPoteza.NijeOdigran; // .. jer treba prvo da se baci kockica
        if (tabla[i] != naRedu)
            return StatusPoteza.NijeOdigran; // ... jer ta tom polju nije njegova figura

        int i1 = (i + kockica) % BrojPolja; // i1 je dolazno polje
        if (tabla[i1] == naRedu)
            return StatusPoteza.NijeOdigran; // ne moze da jede sebe

        if (tabla[i1] >= 0)
            brFiguraUIgri[tabla[i1]]--; // pojeo protivnicku figuru
        bacioKockicu = false; // treba da baca sledeci (ili isti, ako je 6 na kockici)
        tabla[i] = -1; // isprazni staro polje
        tabla[i1] = naRedu; // popuni novo polje
        int kucica = 10 * naRedu;

        if (i1 >= kucica && i1 - kockica < kucica) // stigao ili prosao kucicu
        {
            igraJeZavrsena = true;
            return StatusPoteza.Pobednicki;
        }
        else
        {
            if (kockica < 6) // ako nije 6
                naRedu = (naRedu + 1) % brIgraca; // na redu je sledeci
            return StatusPoteza.Odigran;
        }
    }
    public void NovaFigura()
    {
        if (MozeNovaFigura)
        {
            int kucica = 10 * naRedu;
            if (tabla[kucica] >= 0)
                brFiguraUIgri[tabla[kucica]]--; // pojeo protivnicku figuru
            tabla[kucica] = naRedu;
            brFiguraUIgri[naRedu]++;
            bacioKockicu = false; // treba ponovo da baca
        }
    }
}

class Program
{
    static void Prikazi(CoveceNeLjutiSe igra)
    {
        Console.Clear();
        int N = igra.BrojPolja;
        double R = N / Math.PI;
        double fi0 = 0.75 * Math.PI;
        double dFi = -2.0 * Math.PI / N;
        for (int i = 0; i < N; i++)
        {
            double x = Math.Round(3 * R + 1 + 3 * R * Math.Cos(fi0 + i * dFi));
            double y = Math.Round(R + 1 + R * Math.Sin(fi0 + i * dFi));
            Console.SetCursorPosition((int)x, (int)y);
            CoveceNeLjutiSe.Figura figura = igra[i];
            CoveceNeLjutiSe.Polje polje = igra.VrstaPolja(i);
            string tekst = i.ToString();
            switch (figura)
            {
                case CoveceNeLjutiSe.Figura.R:
                    Console.ForegroundColor = ConsoleColor.Red; tekst = "R"; break;
                case CoveceNeLjutiSe.Figura.G:
                    Console.ForegroundColor = ConsoleColor.Green; tekst = "G"; break;
                case CoveceNeLjutiSe.Figura.Y:
                    Console.ForegroundColor = ConsoleColor.Yellow; tekst = "Y"; break;
                case CoveceNeLjutiSe.Figura.B:
                    Console.ForegroundColor = ConsoleColor.Blue; tekst = "B"; break;
                default: Console.ForegroundColor = ConsoleColor.DarkGray; break;
            };
            switch (polje)
            {
                case CoveceNeLjutiSe.Polje.CrvenaKucica:
                    Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case CoveceNeLjutiSe.Polje.ZelenaKucica:
                    Console.BackgroundColor = ConsoleColor.DarkGreen; break;
                case CoveceNeLjutiSe.Polje.ZutaKucica:
                    Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                case CoveceNeLjutiSe.Polje.PlavaKucica:
                    Console.BackgroundColor = ConsoleColor.DarkBlue; break;
                default: Console.BackgroundColor = ConsoleColor.Black; break;
            }
            Console.Write(tekst);
        }
        Console.SetCursorPosition(0, (int)(2 * R + 3));
        Console.ResetColor();
    }
    static void Main(string[] args)
    {
        Console.Write("Unesi broj igraca: ");
        int brIgraca = int.Parse(Console.ReadLine());
        CoveceNeLjutiSe igra = new CoveceNeLjutiSe(brIgraca);
        CoveceNeLjutiSe.StatusPoteza statusPoteza = CoveceNeLjutiSe.StatusPoteza.NijeOdigran;
        while (statusPoteza != CoveceNeLjutiSe.StatusPoteza.Pobednicki)
        {
            Prikazi(igra);
            Console.WriteLine("Na redu je {0}", igra.ImeIgracaNaRedu);
            Console.Write("Enter da bacis kocku: ");
            Console.ReadLine();
            int kockica = igra.BaciKocku();
            Console.WriteLine("Palo je {0}", kockica);
            if (igra.MozeDaOdigra)
                statusPoteza = CoveceNeLjutiSe.StatusPoteza.NijeOdigran;
            else
            {
                Console.WriteLine("Enter za nastavak");
                Console.ReadLine();
                statusPoteza = CoveceNeLjutiSe.StatusPoteza.Odigran;
            }
            while (statusPoteza == CoveceNeLjutiSe.StatusPoteza.NijeOdigran)
            {
                Console.Write("Sa kog polja igras? ");
                if (igra.MozeNovaFigura) Console.Write("(Enter za novog) ");
                string odg = Console.ReadLine();
                if (odg == "")
                {
                    if (igra.MozeNovaFigura)
                    {
                        igra.NovaFigura();
                        statusPoteza = CoveceNeLjutiSe.StatusPoteza.Odigran;
                    }
                }
                else
                    statusPoteza = igra.IgrajSaPolja(int.Parse(odg));
            }
        }
        Prikazi(igra);
        Console.WriteLine("Pobedio je {0}", igra.ImePobednika);
    }
}
