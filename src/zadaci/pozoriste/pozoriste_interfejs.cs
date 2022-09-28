public class Pozoriste
{
    public Pozoriste(string n) { /*...*/ }
    public void DodajPredstavu(Predstava p) { /*...*/ }
    public bool ZaposliGlumca(Glumac g) { /*...*/ return true;  }
    public bool OtpustiGlumca(Glumac g) { /*...*/ return true; }
    public string Pregled() { /*...*/ return ""; }
}
public class Predstava
{
    public bool Aktivna { get { /*...*/ return true; } }
    public Predstava(string n, int pbg) { /*...*/}
    public bool Aktiviraj() { /*...*/ return true; }
    public void Deaktiviraj() { /*...*/}
    public void Zakazi(Izvodjenje izv) { /*...*/ }
    public bool Izvedi(Izvodjenje izv) { /*...*/ return true; }
    public bool AngazujGlumca(Glumac g) { /*...*/ return true; }
    public bool OslobodiGlumca(Glumac g) { /*...*/ return true; }
    public string Pregled(int uvlacenje) { /*...*/ return ""; }
}
public class Izvodjenje 
{
    public Izvodjenje(DateTime dt, int brUkM) { }
    public bool ProdajKarte(int n) { /*...*/ return true; }
    public DateTime VremeIzvodljenja { get {return DateTime.Now; } }
    public int BrSlobodnihMesta { get { return 0; } }
    public int BrojPosetilaca { get { return 0; } }
}
public class Glumac 
{
    public Glumac(string i, int maxBAP) { }
    public bool Slobodan() { /*...*/ return true; }
    public bool Zauzmi() { /*...*/ return true; }
    public void Oslobodi() { /*...*/ }
    public void Izvodljenje() { /*...*/ }
    public override string ToString() { /*...*/ return ""; }
}
