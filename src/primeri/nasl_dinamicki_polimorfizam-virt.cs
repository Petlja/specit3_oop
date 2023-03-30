using System;

namespace ns
{
    class A { public virtual void f() { Console.WriteLine("A.f"); } }
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
            a.f();
        }
    }
}
