using System;
using System.Collections.Generic;

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
