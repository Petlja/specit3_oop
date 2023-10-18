using System;
using System.Collections.Generic;

public class Karta
{
    public enum Boja { Pik, Karo, Herc, Tref };
    
    // statička polja, ista za sve objekte (pamte se na nivou klase)
    public static Boja BojaPrveKarte;
    public static Boja AdutskaBoja;

    // svojstva konkretne karte
    public Boja BojaKarte { get { return Boja.Herc; } }
    public int Broj { get { return 0; } }

    // konstruktor koji treba implementirati
    public Karta(string oznaka) { }

    // svojstvo koje treba implementirati
    public int Vrednost { get { return 0; } }
    
    // metod koji treba redefinisati
    public override string ToString() { return ""; }
}
