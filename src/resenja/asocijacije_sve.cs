using System;

public class Asocijacije
{
    private int brKolona;
    private int brPojmova;
    private string[,] pojam;
    private string[] resenjeKolone;
    private string konacnoResenje;
    private bool[,] otvoreno;
    private int[] brNeotvorenih;
    private bool[] resenaKolona;
    private bool reseno;
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
    //public bool Otvoreno(int kol, int p) { return otvoreno[kol, p]; }
    //public bool ResenaKol(int kol) { return resenaKolona[kol]; }
    //public bool Reseno { get { return reseno; } }
    //public string Konacno { get { return reseno ? konacnoResenje : "Konacno"; } }
    public int Pokusaj(int kol, string odgovor)
    {
        // poeni za kolonu = 3 + za svako neotvoreno polje po 1
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
    public int Pokusaj(string odgovor)
    {
        // poeni za konacno = 8 + odgovarajuci poeni za svaku kolonu
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

class Program
{
    static void Prikazi(Asocijacije igra, int ukPoena, string konacno = "")
    {
        for (int p = 0; p < igra.BrojPojmova; p++)
        {
            for (int kol = 0; kol < igra.BrojKolona; kol++)
            {
                Console.Write("{0,15}", igra[kol, p]);
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', 15 * igra.BrojKolona));
        for (int kol = 0; kol < igra.BrojKolona; kol++)
            Console.Write("{0,15}", igra[kol]);
        Console.WriteLine();
        if (konacno != "")
            Console.WriteLine("Konacno: {0}", konacno);
        Console.WriteLine("Ukupan broj poena do sada: {0}", ukPoena);
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        string c = "tocak";
        string[] b = { "vodenica", "kamion", "krug", "guma" };
        string[,] a = {
            { "brasno", "potok", "kamen" },
            { "vozac", "teret", "prikolica" },
            { "centar", "geometrija", "kolo" },
            { "kaucuk", "zvaka", "izolator" },
        };
        Asocijacije igra = new Asocijacije(a, b, c);

        bool kraj = false;
        int ukPoena = 0;
        while (!kraj)
        {
            string[] ulaz;
            Console.Clear();
            Prikazi(igra, ukPoena);
            if (!igra.SveOtvoreno)
            {
                bool uspesnoOtvaranje = false;
                while (!uspesnoOtvaranje)
                {
                    Console.WriteLine("(Unesi veliko slovo i broj, razdvojeno)");
                    Console.Write("Otvori polje ");
                    ulaz = Console.ReadLine().Split();
                    int kol = ulaz[0][0] - 'A';
                    int p = int.Parse(ulaz[1]) - 1;
                    uspesnoOtvaranje = igra.Otvori(kol, p);
                    Console.Clear();
                    Prikazi(igra, ukPoena);
                }
            }
            bool pravoNaPogadljanje = true;
            while (pravoNaPogadljanje)
            {
                Console.WriteLine("(Unesi veliko slovo za kolonu i pojam, ili za konacno samo pojam)");
                Console.Write("Pogadjaj: ");
                ulaz = Console.ReadLine().Split();
                if (ulaz.Length == 1)
                {
                    // pokusaj konacnog resenja
                    int poeni = igra.Pokusaj(ulaz[0]);
                    if (poeni > 0)
                    {
                        Console.WriteLine("Bravo, dobijas {0} poena.", poeni);
                        ukPoena += poeni;
                        for (int i = 0; i < 3; i++)
                            Console.WriteLine();
                        Prikazi(igra, ukPoena);
                        kraj = true;
                    }
                    pravoNaPogadljanje = false;
                }
                else
                {
                    // pokusaj resenja kolone
                    int kol = ulaz[0][0] - 'A';
                    int poeni = igra.Pokusaj(kol, ulaz[1]);
                    if (poeni > 0)
                    {
                        Console.WriteLine("Bravo, dobijas {0} poena.", poeni);
                        ukPoena += poeni;
                        for (int i = 0; i < 3; i++)
                            Console.WriteLine();
                        Prikazi(igra, ukPoena);
                    }
                    else
                        pravoNaPogadljanje = false;
                }
            }
        }
    }
}