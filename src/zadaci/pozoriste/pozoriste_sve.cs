using System;
using System.Collections.Generic;
using System.Text;


public class Pozoriste
{
    private string naziv;
    private List<Predstava> predstave = new List<Predstava>();
    private List<Glumac> glumci = new List<Glumac>();
    public Pozoriste(string n)
    {
        naziv = n;
    }
    public void DodajPredstavu(Predstava p)
    {
        predstave.Add(p);
    }
    public bool ZaposliGlumca(Glumac g)
    {
        if (glumci.Contains(g))
            return false;

        glumci.Add(g);
        return true;
    }
    public bool OtpustiGlumca(Glumac g)
    {
        return glumci.Remove(g);
    }

    public string Pregled()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("pozoriste {0}\n", naziv));
        sb.Append("aktivne predstave:\n");
        int brAktivnih = 0;
        foreach (Predstava p in predstave)
        {
            if (p.Aktivna)
            {
                brAktivnih++;
                sb.Append(p.Pregled(4));
            }
        }
        if (brAktivnih == 0)
            sb.Append("nema\n");

        return sb.ToString();
    }
}
public class Predstava
{
    private string naziv;
    private int potrebanBrGlumaca;
    private int brojIzvodljenja;
    private int ukupnoGledalaca;
    private List<Glumac> angazovaniGlumci = new List<Glumac>();
    private List<Izvodjenje> zakazanaIzvodljena = new List<Izvodjenje>();
    private List<Izvodjenje> odrzanaIzvodljena = new List<Izvodjenje>();
    private bool aktivna;
    public bool Aktivna { get { return aktivna; } }
    public Predstava(string n, int pbg) 
    {
        naziv = n;
        potrebanBrGlumaca = pbg;
        brojIzvodljenja = ukupnoGledalaca = 0;
        aktivna = false;
    }
    public bool Aktiviraj()
    {
        if (aktivna)
            return true;

        if (angazovaniGlumci.Count < potrebanBrGlumaca)
            return false;

        bool moze = true;
        foreach (Glumac g in angazovaniGlumci)
        {
            if (!g.Slobodan())
                moze = false;
        }

        if (moze)
        {
            foreach (Glumac g in angazovaniGlumci)
                g.Zauzmi();

            aktivna = true;
        }
        return moze;
    }
    public void Deaktiviraj()
    {
        if (!aktivna)
            return;
        
        foreach (Glumac g in angazovaniGlumci)
            g.Oslobodi();
        
        aktivna = false;
    }
    public void Zakazi(Izvodjenje izv)
    {
        if (zakazanaIzvodljena.Contains(izv))
            return;
        zakazanaIzvodljena.Add(izv);
    }

    public bool Izvedi(Izvodjenje izv)
    {
        if (!aktivna)
            return false;

        if (zakazanaIzvodljena.Remove(izv))
        {
            odrzanaIzvodljena.Add(izv);
            foreach (Glumac g in angazovaniGlumci)
                g.Izvodljenje();
            brojIzvodljenja++;
            ukupnoGledalaca += izv.BrojPosetilaca;
            return true;
        }
        return false;
    }

    public bool AngazujGlumca(Glumac g)
    {
        if (angazovaniGlumci.Contains(g))
            return false;

        angazovaniGlumci.Add(g);
        return true;
    }
    public bool OslobodiGlumca(Glumac g)
    {
        return angazovaniGlumci.Remove(g);
    }
    public string Pregled(int uvlacenje)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("{0}predstava: {1}\n", 
            new string(' ', uvlacenje), naziv));
        
        // angazovani glumci
        sb.Append(string.Format("{0}glumci:\n",
            new string(' ', uvlacenje)));
        foreach (Glumac g in angazovaniGlumci)
            sb.Append(string.Format("{0}{1}\n",
                new string(' ', uvlacenje + 4), g));

        // zakazana izvodjenja za koja ima karata
        sb.Append(string.Format("{0}Buduca izvodljenja\n",
            new string(' ', uvlacenje)));
        foreach (Izvodjenje izv in zakazanaIzvodljena)
        {
            if (izv.BrSlobodnihMesta > 0)
                sb.Append(string.Format("{0}datum i vreme: {1}, br. mesta:{2}\n",
                    new string(' ', uvlacenje + 4), izv.VremeIzvodljenja, izv.BrSlobodnihMesta));
        }

        sb.Append(string.Format("{0}br. odrzanih izvodljenja: {1}\n",
            new string(' ', uvlacenje), brojIzvodljenja));
        sb.Append(string.Format("{0}br. gledalaca: {1}\n",
            new string(' ', uvlacenje), ukupnoGledalaca));
        return sb.ToString();
    }
}
public class Izvodjenje 
{
    private DateTime vremeIzvodljenja;
    private int brSlobodnihMesta;
    private int ukMesta;
    public Izvodjenje(DateTime dt, int brUkM)
    {
        vremeIzvodljenja = dt;
        ukMesta = brUkM;
        brSlobodnihMesta = brUkM;
    }

    public bool ProdajKarte(int n)
    {
        if (n > brSlobodnihMesta)
            return false;
        
        brSlobodnihMesta -= n;
        return true;
    }
    public DateTime VremeIzvodljenja { get {return vremeIzvodljenja; } }
    public int BrSlobodnihMesta { get { return brSlobodnihMesta; } }
    public int BrojPosetilaca 
    { 
        get { return ukMesta - brSlobodnihMesta; } 
    }
}
public class Glumac 
{
    private string ime;
    private int brAktivnihPredstava;
    private int maxBrAktivnihPredstava;
    private int brOdigranihPredstava;
    //public int BrOdigranihPredstava { get { return brOdigranihPredstava; } }
    public Glumac(string i, int maxBAP)
    {
        ime = i;
        maxBrAktivnihPredstava = maxBAP;
        brAktivnihPredstava = 0;
        brOdigranihPredstava = 0;
    }
    public bool Slobodan() 
    {
        return brAktivnihPredstava < maxBrAktivnihPredstava;
    }
    public bool Zauzmi()
    {
        if (brAktivnihPredstava == maxBrAktivnihPredstava)
            return false;
        brAktivnihPredstava++;
        return true;
    }
    public void Oslobodi()
    {
        if (brAktivnihPredstava == 0)
            throw new Exception("Greska: negativna aktivnost glumca");
        brAktivnihPredstava--;
    }
    public void Izvodljenje()
    {
        brOdigranihPredstava++;
    }
    public override string ToString()
    {
        return string.Format("ime: {0}, br. predstava: {1}", ime, brOdigranihPredstava);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Glumac g1 = new Glumac("Pera", 4);
        Glumac g2 = new Glumac("Mika", 3);
        Glumac g3 = new Glumac("Zika", 2);

        Izvodjenje i1 = new Izvodjenje(new DateTime(2022, 12, 23, 19, 30, 0), 200);
        i1.ProdajKarte(180);
        Izvodjenje i2 = new Izvodjenje(new DateTime(2022, 12, 24, 19, 30, 0), 200);
        i2.ProdajKarte(50);
        i2.ProdajKarte(10);

        Predstava p1 = new Predstava("Pr1", 3);
        p1.AngazujGlumca(g1);
        bool aktivirana = p1.Aktiviraj();
        Console.WriteLine("Predstava " + (aktivirana ? "je aktivirana" : "nije aktivirana"));
        p1.AngazujGlumca(g2);
        p1.AngazujGlumca(g3);
        aktivirana = p1.Aktiviraj();
        Console.WriteLine("Predstava " + (aktivirana ? "je aktivirana" : "nije aktivirana"));
        p1.Zakazi(i1);
        bool izvedena = p1.Izvedi(i1);
        Console.WriteLine("Predstava " + (izvedena ? "je izvedena" : "nije izvedena"));
        izvedena = p1.Izvedi(i1);
        Console.WriteLine("Predstava " + (izvedena ? "je izvedena" : "nije izvedena"));
        Console.WriteLine("--------------");

        Pozoriste jdp = new Pozoriste("JDP");
        jdp.DodajPredstavu(p1);
        Console.WriteLine(jdp.Pregled());
        Console.WriteLine("--------------");

        p1.Deaktiviraj();
        Console.WriteLine(jdp.Pregled());
        Console.WriteLine("--------------");

        p1.Aktiviraj();
        p1.Zakazi(i2);
        //p1.Izvedi(i2);
        Console.WriteLine(jdp.Pregled());
    }
}
