class Program
{
    static void Main(string[] args)
    {
        // proba
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
