using System;
using System.Collections.Generic;
using System.Text;

public class Prilog
{
    public enum Sastojak
    {
        PAVLAKA,
        SENF,
        MAJONEZ,
        KUPUS,
        PARADAJZ,
        KRASTAVAC,
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
public class Pljeskavica
{
    public enum Velicina { MALA, SREDNJA, VELIKA }
    private Velicina velicina;
    private List<Prilog> pril;
    public Pljeskavica(Velicina v)
    {
        this.velicina = v;
        pril = new List<Prilog>();
    }
    public void Dodaj(Prilog p)
    {
        foreach (Prilog pr in pril)
        {
            if (pr.Equals(p)) throw new GreskaSastojak();
        }
        pril.Add(p);
    }
    public int Cena()
    {
        int c = 0;
        if (velicina == Velicina.MALA) c += 180;
        else if (velicina == Velicina.SREDNJA) c += 220;
        else c += 270;
        foreach (Prilog p in pril)
        {
            c += p.Cena();
        }
        return c;
    }
    public override String ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(velicina);
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
    private List<Pljeskavica> pal;
    public Porudzbina() { pal = new List<Pljeskavica>(); }
    public void Dodaj(Pljeskavica p) { pal.Add(p); }
    public int Broj() { return pal.Count; }
    public int Cena()
    {
        int c = 0;
        foreach (Pljeskavica p in pal) { c += p.Cena(); }
        return c;
    }
    public void IzbaciSve() { pal.Clear(); }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("porudzbina_" + id + ": ");
        foreach (Pljeskavica p in pal)
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
            Pljeskavica pl1 = new Pljeskavica(Pljeskavica.Velicina.MALA);
            Pljeskavica pl2 = new Pljeskavica(Pljeskavica.Velicina.SREDNJA);
            Prilog kupus = new Prilog(Prilog.Sastojak.KUPUS, 30);
            Prilog majonez = new Prilog(Prilog.Sastojak.MAJONEZ, 30);

            pl1.Dodaj(majonez);
            pl1.Dodaj(kupus);
            porudzbina.Dodaj(pl1);

            pl2.Dodaj(majonez);
            porudzbina.Dodaj(pl2);

            Console.WriteLine("Vasa porudzbina je: " + porudzbina);
            Console.WriteLine("Broj pljeskavica koje ste porucili je: " + porudzbina.Broj());
            Console.WriteLine("Cena Vase porudzbine je: " + porudzbina.Cena());
        }
        catch (GreskaSastojak e)
        {
            Console.WriteLine(e);
        }
    }
}

