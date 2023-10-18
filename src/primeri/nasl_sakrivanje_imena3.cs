using System;

namespace Program
{
    // bazna klasa
    public class A
    {
        public int n = 5;
        public void F() 
        { 
            Console.WriteLine("A.F: n = {0}", n); // 5
        }
        
    }
    
    // izvedena klasa
    public class B : A
    {
        // ime 'n' sakriva istoimeno polje bazne klase
        public int n = 10; 

        public void F() 
        { 
            // ime 'n' se ovde odnosi na polje izvedene klase
            Console.WriteLine("B.F: n = {0}", n);  // 10
        }
        public void G() 
        { 
            // polje bazne klase je dostupno kao 'base.n'
            Console.WriteLine("B.G: n = {0}", base.n); // 5
        } 

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