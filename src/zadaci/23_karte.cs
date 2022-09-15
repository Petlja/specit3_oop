using System;
public class Karta
{
    public enum Boja { Pik, Karo, Herc, Tref };
    public static Boja BojaPrveKarte;
    public static Boja AdutskaBoja;

    private Boja boja;
    private int broj;

    public Boja BojaKarte { get { return boja; } }
    public int Broj { get { return Broj; } }

    public Karta(string oznaka)
    {
        if (oznaka.Length != 2)
            throw new Exception("Pogresna oznaka karte");

        switch (oznaka[0])
        {
            case 'A': broj = 15; break;
            case 'K': broj = 14; break;
            case 'Q': broj = 13; break;
            case 'J': broj = 12; break;
            case 'D': broj = 10; break;
            case '9': broj = 9; break;
            case '8': broj = 8; break;
            case '7': broj = 7; break;
            case '6': broj = 6; break;
            case '5': broj = 5; break;
            case '4': broj = 4; break;
            case '3': broj = 3; break;
            case '2': broj = 2; break;
            default: throw new Exception("Pogresna oznaka karte");
        }
        switch (oznaka[1])
        {
            case 'P': boja = Boja.Pik; break;
            case 'K': boja = Boja.Karo; break;
            case 'H': boja = Boja.Herc; break;
            case 'T': boja = Boja.Tref; break;
            default: throw new Exception("Pogresna oznaka karte");
        }
    }

    public int Vrednost
    {
        get
        {
            if (boja == AdutskaBoja) return 40 + broj;
            if (boja == BojaPrveKarte) return 20 + broj;
            return broj;
        }
    }

    public override string ToString()
    {
        string znak = "";
        switch (boja)
        {
            case Boja.Pik: znak = "Pik"; break;
            case Boja.Karo: znak = "Karo"; break;
            case Boja.Herc: znak = "Herc"; break;
            case Boja.Tref: znak = "Tref"; break;
        }
        string ime = "";
        switch (broj)
        {
            case 15: ime = "As"; break;
            case 14: ime = "Kralj"; break;
            case 13: ime = "Dama"; break;
            case 12: ime = "Zandar"; break;
            default: ime = broj.ToString(); break;
        }
        return string.Format("{0} {1}", ime, znak);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            int brojKarata = 5;
            Karta[] baceneKarte = new Karta[brojKarata];
            baceneKarte[0] = new Karta("DT");
            baceneKarte[1] = new Karta("AT");
            baceneKarte[2] = new Karta("AP");
            baceneKarte[3] = new Karta("4K");
            baceneKarte[4] = new Karta("3K");

            Karta.AdutskaBoja = Karta.Boja.Karo;
            Karta.BojaPrveKarte = baceneKarte[0].BojaKarte;

            int maxVr = baceneKarte[0].Vrednost;
            int iMax = 0;
            for (int i = 1; i < brojKarata; i++)
            {
                if (baceneKarte[i].Vrednost > maxVr)
                {
                    maxVr = baceneKarte[i].Vrednost;
                    iMax = i;
                }
            }
            Console.WriteLine("Nosi igrac sa indeksom {0}", iMax);
            Console.WriteLine("to je karta {0}", baceneKarte[iMax]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
