// primer pokazuje da se poziv metoda koji nijje virtuelan (niti apstraktan)
// razrešava na osnovu tipa reference, a ne na osnovu stvarnog tipa objekta.

using System;

namespace Program
{
    // bazna klasa
    public class A { public void F() { Console.WriteLine("A.F"); } }

    // dve izvedene klase
    public class B : A { public new void F() { Console.WriteLine("B.F"); } }
    public class C : A { public new void F() { Console.WriteLine("C.F"); } }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A(); a.F();

            string s = Console.ReadLine();
            if (s == "b")
                a = new B();
            else
                a = new C();

            a.F(); // i dalje "A.F"

            B b = new B(); b.F();
            C c = new C(); c.F();
        }
    }
}
