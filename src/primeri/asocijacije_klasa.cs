public class Asocijacije
{
    private int brKolona;    // broj kolona u igri
    private int brPojmova;   // broj pojmova u svakoj koloni
    private string[,] pojam; // pojmovi koji asociraju na rešenja
    private string[] resenjeKolone;
    private string konacnoResenje;
    private bool[,] otvoreno;
    private int[] brNeotvorenih; // broj neotvorenih polja po koloni
    private bool[] resenaKolona;
    private bool reseno;

    // a je matrica pojmova
    // b su rešenja kolona
    // c je konačno rešenje
    public Asocijacije(string[,] a, string[] b, string c)
    {
        brKolona = a.GetLength(0);
        brPojmova = a.GetLength(1);
        pojam = (string[,])a.Clone();
        resenjeKolone = (string[])b.Clone();
        konacnoResenje = c;
        otvoreno = new bool[brKolona, brPojmova];
        resenaKolona = new bool[brKolona];
        brNeotvorenih = new int[brKolona];
        for (int kol = 0; kol < brKolona; kol++)
            brNeotvorenih[kol] = brPojmova;
        reseno = false;
    }

    // privatan konstruktor, koristi se samo u metodu Kreiraj
    private Asocijacije() 
    {
    } 

    public static Asocijacije Kreiraj(string putanja)
    {
        if (!File.Exists(putanja))
            return null;

        Asocijacije igra = new Asocijacije();
        // pokušaj da učitaš pojmove i rešenja iz fajla
        try
        {
            using (StreamReader sr = new StreamReader(putanja))
            {
                string[] linija = sr.ReadLine().Split();
                igra.brKolona = int.Parse(linija[0]);
                igra.brPojmova = int.Parse(linija[1]);

                igra.pojam = new string[igra.brKolona, igra.brPojmova];
                igra.resenjeKolone = new string[igra.brKolona];
                for (int k = 0; k < igra.brKolona; k++)
                {
                    for (int p = 0; p < igra.brPojmova; p++)
                        igra.pojam[k, p] = sr.ReadLine();

                    igra.resenjeKolone[k] = sr.ReadLine();
                }
                igra.konacnoResenje = sr.ReadLine();
            }
            igra.otvoreno = new bool[igra.brKolona, igra.brPojmova];
            igra.resenaKolona = new bool[igra.brKolona];
            igra.brNeotvorenih = new int[igra.brKolona];
            for (int kol = 0; kol < igra.brKolona; kol++)
                igra.brNeotvorenih[kol] = igra.brPojmova;
            igra.reseno = false;
        }
        catch (System.Exception)
        {
            return null;
        }
        // ako je sve uspešno pročitano, vrati inicijalizovan objekat
        return igra;
    }
    
    // svojstvo odgovara na pitanje da li su svi pojmovi odtvoreni
    public bool SveOtvoreno
    {
        get
        {
            for (int kol = 0; kol < brKolona; kol++)
                if (brNeotvorenih[kol] > 0)
                    return false;
            return true;
        }
    }

    public int BrojKolona { get { return brKolona; } }

    public int BrojPojmova { get { return brPojmova; } }

    // pokušava da otvori polje
    public bool Otvori(int kol, int p)
    {
        if (!otvoreno[kol, p])
        {
            brNeotvorenih[kol]--;
            otvoreno[kol, p] = true;
            return true;
        }
        return false;
    }

    // za otvoreno polje vraća pojam, a za zatvoreno naziv polja
    public string this[int kol, int p]
    {
        get
        {
            if (otvoreno[kol, p])
                return pojam[kol, p];
            else
                return string.Format("{0}{1}", (char)('A' + kol), p + 1);
        }
    }
    
    // za rešenu kolonu vraća rešenje, a za zatvorenu naziv kolone
    public string this[int kol]
    {
        get
        {
            if (resenaKolona[kol])
                return resenjeKolone[kol];
            else
                return string.Format("Kolona {0}", (char)('A' + kol));
        }
    }

    public bool Otvoreno(int kol, int p) { return otvoreno[kol, p]; }

    public bool ResenaKol(int kol) { return resenaKolona[kol]; }

    public bool Reseno { get { return reseno; } }

    public string Konacno { get { return reseno ? konacnoResenje : "Konacno"; } }

    // pokušaj pogađanja kolone
    public int Pokusaj(int kol, string odgovor)
    {
        if (resenaKolona[kol])
            return 0;
        if (resenjeKolone[kol] == odgovor)
        {
            resenaKolona[kol] = true;
            int poeni = 3 + brNeotvorenih[kol];
            for (int p = 0; p < brPojmova; p++)
                otvoreno[kol, p] = true;
            return poeni;
        }
        else
            return 0;
    }
    
    // pokušaj pogađanja konačnog rešenja
    public int Pokusaj(string odgovor)
    {
        if (reseno)
            return 0;
        if (konacnoResenje == odgovor)
        {
            reseno = true;
            int poeni = 8;
            for (int kol = 0; kol < brKolona; kol++)
            {
                poeni += 3 + brNeotvorenih[kol];
                for (int p = 0; p < brPojmova; p++)
                    otvoreno[kol, p] = true;
                resenaKolona[kol] = true;
            }
            return poeni;
        }
        else
            return 0;
    }
}
