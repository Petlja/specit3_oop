using System;
using System.Collections.Generic;

public class Karta
{
    public enum Boja { Pik, Karo, Herc, Tref };
    public static Boja BojaPrveKarte;
    public static Boja AdutskaBoja;

    public Boja BojaKarte { get { return Boja.Herc; } }
    public int Broj { get { return 0; } }

    public Karta(string oznaka) { }
    public int Vrednost { get { return 0; } }
    public override string ToString() { return ""; }
}
