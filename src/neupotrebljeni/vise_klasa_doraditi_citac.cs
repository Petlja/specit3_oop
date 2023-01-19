using System;
public class Dokument
{
    private Strana prvaStrana;
    public Dokument(Strana s) { prvaStrana = s; }
    public Strana PrvaStrana() { return prvaStrana;  }
}
public class Strana
{
    private Red prviRed;
    private Strana sledecaStrana;
    public Strana(Red r, Strana s) { prviRed = r; sledecaStrana = s; }
    public Red PrviRed() { return prviRed; }
    public Strana SledecaStrana() { return sledecaStrana; }
}

public class Red
{
    private string tekst;
    private Red sledeciRed;
    public Red(string str, Red r) { tekst = str; sledeciRed = r; }
    public string Tekst() { return tekst; }
    public Red SledeciRed() { return sledeciRed; }
}

public class Citac
{
    private Dokument tekuciDokument;
    private Strana tekucaStrana;
    private Red tekuciRed;
    public Citac(Dokument d) 
    {
        tekuciDokument = d;
    }
    public Red PrviRed()
    {
        tekucaStrana = tekuciDokument.PrvaStrana();
        tekuciRed = tekucaStrana.PrviRed();
        return tekuciRed;
    }
    public Red SledeciRed()
    {
        Red sledeciRed = tekuciRed.SledeciRed();
        if (sledeciRed != null) 
        {
            tekuciRed = sledeciRed;
            return tekuciRed;
        }
        Strana sledecaStrana = tekucaStrana.SledecaStrana();
        if (sledecaStrana != null)
        {
            tekucaStrana = sledecaStrana;
            tekuciRed = tekucaStrana.PrviRed();
            return tekuciRed;
        }
        return null; // nema sledeceg reda, vraca null
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            Red[] redovi = new Red[6];
            redovi[5] = new Red("Dovidjenja.", null);
            redovi[4] = new Red("Prva vest je da nema vesti, a druga isto.", null);
            redovi[3] = new Red("Imam dve vesti, dobru i losu.", redovi[4]);
            redovi[2] = new Red("Bas mi je drago.", null);
            redovi[1] = new Red("Kako ste?", redovi[2]);
            redovi[0] = new Red("Dobar dan,", redovi[1]);

            Strana[] strane = new Strana[3];
            strane[2] = new Strana(redovi[5], null);
            strane[1] = new Strana(redovi[3], strane[2]);
            strane[0] = new Strana(redovi[0], strane[1]);

            Dokument d = new Dokument(strane[0]);

            Citac c = new Citac(d);

            for (Red r = c.PrviRed(); r != null; r = c.SledeciRed())
            {
                Console.WriteLine(r.Tekst());
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine(e); 
        }
    }
}
