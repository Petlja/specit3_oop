// primer pokazuje da u opštem slučaju nije moguće da se u vreme 
// kompajliranja programa odredi stvarna klasa objekta na koji 
// ukazuje referenca na baznu klasu

using System;

namespace ns
{
    // bazna klasa
    class A { public virtual void f() { Console.WriteLine("A.f"); } }
    
    // izvedena klasa
    class B : A { public override void f() { Console.WriteLine("B.f"); } }
    
    class Program
    {
        static void Main(string[] args)
        {
            A a;
            string s = Console.ReadLine();
            if (s == "a")
                a = new A();
            else
                a = new B();

            // kompajler ne može da zna stvarnu klasu objekta 'a'
            // jer ona zavisi od unosa korisnika u vreme izvršavanja
            a.f();
        }
    }
}
