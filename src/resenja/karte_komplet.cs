using System;
using System.Collections.Generic;

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
            List<Karta> spil = new List<Karta>();
            foreach (char broj in "23456789DJQKA")
                foreach (char boja in "PKHT")
                    spil.Add(new Karta(broj.ToString() + boja.ToString()));

            // mesanje karata
            Random rnd = new Random();
            for (int i = 51; i >= 0; i--)
            {
                int ndx = rnd.Next(i + 1);
                Karta k = spil[ndx];
                spil[ndx] = spil[i];
                spil[i] = k;
            }
            int brIgraca = 4;
            Karta.AdutskaBoja = spil[51].BojaKarte;
            int[] brStihova = new int[brIgraca];
            int nosioPrethodnu = 0;
            for (int iRuka = 0; iRuka < 52; iRuka += brIgraca)
            {
                // radi jednostavnosti, pretpostavimo da 
                // igraci bacaju karte redom kako im dolaze
                Karta[] ruka = new Karta[brIgraca];
                for (int i = 0; i < brIgraca; i++)
                    ruka[i] = spil[iRuka + i];

                Karta.BojaPrveKarte = ruka[nosioPrethodnu].BojaKarte;
                int maxVr = ruka[nosioPrethodnu].Vrednost;
                int kMax = nosioPrethodnu;
                for (int k = 0; k < brIgraca; k++)
                {
                    if (ruka[k].Vrednost > maxVr)
                    {
                        maxVr = ruka[k].Vrednost;
                        kMax = k;
                    }
                }
                brStihova[kMax]++;
                nosioPrethodnu = kMax;
            }
            for (int i = 0; i < brIgraca; i++)
                Console.WriteLine("Igrac br {0} odneo je {1}", i, brStihova[i]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
