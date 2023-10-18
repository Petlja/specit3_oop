// primer pokazuje da u opštem slučaju nije moguće da se u vreme 
// kompajliranja programa odredi stvarna klasa objekta na koji 
// ukazuje referenca na baznu klasu

using System;

namespace ns
{
    // bazna klasa
    abstract class A { public abstract void f(); }
    
    // dve izvedene klase
    class B : A { public override void f() { Console.WriteLine("B.f"); } }    
    class C : A { public override void f() { Console.WriteLine("C.f"); } }
    
    class Program
    {
        static void Main(string[] args)
        {
            A a;
            string s = Console.ReadLine();
            if (s == "b")
                a = new B();
            else
                a = new C();
            
            // kompajler ne može da zna stvarnu klasu objekta 'a'
            // jer ona zavisi od unosa korisnika u vreme izvršavanja
            a.f();
        }
    }
}
