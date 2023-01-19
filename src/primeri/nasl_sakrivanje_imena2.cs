using System;

namespace Program
{
    public class A { public void F() { Console.WriteLine("A.F"); } }
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
            a.F();

            B b = new B(); b.F();
            C c = new C(); c.F();
        }
    }
}
