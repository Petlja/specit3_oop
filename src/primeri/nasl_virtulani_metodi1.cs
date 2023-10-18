using System;

// bazna klasa
class B
{
    public void F1() { Console.Write("b.f1 "); }
    virtual public void F2() { Console.Write("b.f2 "); }
    public void F3() { Console.Write("b.f3 "); }
    virtual public void F4() { Console.Write("b.f4 "); }

}

// izvedena klasa
class D : B
{
    // metod F1 je namenjen da se prosto nasledi
    // metod F2 moze da bude redefinisan, ali u klasi D nije, pa se koristi iz klase B

    // metod F3 je nov (new) metod sa istim imenom, on sakriva F3 iz bazne klase
    // Pošto F3 nije virtuelan, razrešava se statički (po deklarisanom tipu)
    public new void F3() { Console.Write("d.f3 "); }

    // F4 je redefinisan virtuelan metod, razresava se dinamički (po aktuelnom tipu objekta)
    override public void F4() { Console.Write("d.f4 "); }
}
internal class Program
{
    static void Main(string[] args)
    {
        B o1 = new B(); o1.F1(); o1.F2(); o1.F3(); o1.F4(); Console.WriteLine();
        B o2 = new D(); o2.F1(); o2.F2(); o2.F3(); o2.F4(); Console.WriteLine();
        D o3 = new D(); o3.F1(); o3.F2(); o3.F3(); o3.F4(); Console.WriteLine();
    }
}
