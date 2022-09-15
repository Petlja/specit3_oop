// https://rti.etf.bg.ac.rs/rti/ir2oo2/rokovi/OO2KP190630.pdf
using System;
using System.Collections.Generic;
using System.Text;

public class Prilog
{
    public enum Sastojak
    {
        PLAZMA, 
        KIKIRIKI,
        KOKOS, 
        ORASI
    }
    private Sastojak sastojak; 
    private int cena;
    public Prilog(Sastojak s, int c) { sastojak = s; cena = c; }
    public Sastojak DohvatiSastojak() { return sastojak; }
    public int Cena() { return cena; }
    public override bool Equals(Object obj)
    {
        Prilog p = obj as Prilog;
        return (p != null) && sastojak == p.sastojak;
    }
}
public class Palacinka
{
    public enum Premaz { EUROKREM, DZEM }
    private Premaz premaz;
    private List<Prilog> pril;
    public Palacinka(Premaz p)
    {
        this.premaz = p;
        pril = new List<Prilog>();
    }
    public void Dodaj(Prilog p)
    {
        foreach(Prilog pr in pril) {
            if(pr.Equals(p)) throw new GreskaSastojak();
        }
        pril.Add(p);
    }
    public int Cena()
    {
        int c = 0;
        foreach (Prilog p in pril) { c += p.Cena(); }
        c += (premaz == Premaz.DZEM ? 70 : 100);
        return c;
    }
    public override String ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(premaz);
        foreach (Prilog p in pril) 
        {
            sb.Append(", ");
            sb.Append(p.DohvatiSastojak());
        }
        return sb.ToString();
    }
}

public class GreskaSastojak : Exception
{
    public override string ToString() 
    { 
        return "Greska u sastojcima"; 
    }
}

public class Porudzbina
{
    private static int posId = 0;
    private int id = ++posId;
    private List<Palacinka> pal;
    public Porudzbina() { pal = new List<Palacinka>(); }
    public void Dodaj(Palacinka p) { pal.Add(p); }
    public int Broj() { return pal.Count; }
    public int Cena()
    {
        int c = 0;
        foreach (Palacinka p in pal) { c += p.Cena(); }
        return c;
    }
    public void IzbaciSve() { pal.Clear(); }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("porudzbina_" + id + ": ");
        foreach (Palacinka p in pal)
        {
            sb.Append("[").Append(p).Append("] ");
        }
        return sb.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            Porudzbina porudzbina = new Porudzbina();
            Palacinka pal1 = new Palacinka(Palacinka.Premaz.EUROKREM);
            Palacinka pal2 = new Palacinka(Palacinka.Premaz.EUROKREM);
            Prilog plazma = new Prilog(Prilog.Sastojak.PLAZMA, 30);
            Prilog kikiriki = new Prilog(Prilog.Sastojak.KIKIRIKI, 30);

            pal1.Dodaj(plazma);
            pal1.Dodaj(kikiriki);
            porudzbina.Dodaj(pal1);

            pal2.Dodaj(plazma);
            porudzbina.Dodaj(pal2);
            
            Console.WriteLine("Vasa porudzbina je: " + porudzbina);
            Console.WriteLine("Broj palacinki koje ste porucili je: " + porudzbina.Broj());
            Console.WriteLine("Cena Vase porudzbine je: " + porudzbina.Cena());
        }
        catch (GreskaSastojak e) 
        {
            Console.WriteLine(e); 
        }
    }
}

