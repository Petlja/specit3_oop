using System;
class B
{
    public void F1() { Console.Write("b.f1 "); }
    virtual public void F2() { Console.Write("b.f2 "); }
    public void F3() { Console.Write("b.f3 "); }
    virtual public void F4() { Console.Write("b.f4 "); }

}
class D : B
{
    // F1 je namenjena da se prosto nasledi
    // F2 moze da bude redefinisana, ali u klasi D nije, pa se koristi iz klase B

    // F3 je nova (new) funkcija sa istim imenom, ona sakriva F3 iz bazne klase
    // Posto F3 nije virtuelna, razresava se staticki (po deklarisanom tipu)
    public new void F3() { Console.Write("d.f3 "); }


    // F4 je redefinisana virtuelna funkcija, razresava se dinamicki (po aktuelnom tipu objekta)
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
