using System;

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            // formiramo niz ode nekih pet karata
            int brojKarata = 5;
            Karta[] baceneKarte = new Karta[brojKarata];
            baceneKarte[0] = new Karta("DT");
            baceneKarte[1] = new Karta("AT");
            baceneKarte[2] = new Karta("AP");
            baceneKarte[3] = new Karta("4K");
            baceneKarte[4] = new Karta("3K");

            // zadajemo vrednosti statičkih polja (koja su ista za sve karte)
            Karta.AdutskaBoja = Karta.Boja.Karo;
            Karta.BojaPrveKarte = baceneKarte[0].BojaKarte;

            // određujemo indeks i vrednost najjače karte
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

            // prikazujemo najjaču kartu i njen indeks u nizu
            Console.WriteLine("Nosi igrac sa indeksom {0}", iMax);
            Console.WriteLine("to je karta {0}", baceneKarte[iMax]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
