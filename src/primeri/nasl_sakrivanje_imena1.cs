using System;

namespace Program
{
    public class A { public void F() { Console.WriteLine("A.F"); } }
    public class B : A { public new void F() { Console.WriteLine("B.F"); } }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A(); a.F(); 
            A b1 = new B(); b1.F();
            B b2 = new B(); b2.F();
        }
    }
}
