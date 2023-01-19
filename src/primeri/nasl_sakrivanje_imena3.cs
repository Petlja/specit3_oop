using System;

namespace Program
{
    public class A
    {
        public int n = 5;
        public void F() { Console.WriteLine("A.F: n = {0}", n); } // 5
    }

    public class B : A
    {
        public int n = 10;
        public void F() { Console.WriteLine("B.F: n = {0}", n); } // 10
        public void G() { Console.WriteLine("B.G: n = {0}", base.n); } // 5

    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            a.F();

            B b = new B();
            b.F(); b.G();
        }
    }
}