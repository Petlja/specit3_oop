using System;

namespace ns
{
    abstract class A { public abstract void f(); }
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
            a.f();
        }
    }
}
