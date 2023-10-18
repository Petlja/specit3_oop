using System;

namespace Program
{
    // bazna klasa
    public class A { public void F() { Console.WriteLine("A.F"); } }
    
    // izvedena klasa, u kojoj metod F sakriva istoimeni bazni metod
    public class B : A { public new void F() { Console.WriteLine("B.F"); } }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A(); a.F();   // A.F (jer a nema veze sa klasom B)
            A b1 = new B(); b1.F(); // A.F (jer je b1 referenca na A)
            B b2 = new B(); b2.F(); // B.F (jer je b1 referenca na B)
        }
    }
}
