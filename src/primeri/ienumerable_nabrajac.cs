using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main(String[] args)
    {
        IEnumerable<int> a = new List<int>() { 1, 2, 3, 4, 5 };
        int n = a.Count();
        
        // enumerator koji nabraja elemente od početka liste
        IEnumerator<int> p = a.GetEnumerator();
        
        // enumerator koji nabraja elemente od kraja liste
        IEnumerator<int> k = a.Reverse().GetEnumerator();
        
        // pazimo da bude n poziva metoda MoveNext za oba enumeratora ukupno
        for (int i = 0; i < n; i++)
        {
            Console.Write("Sa pocetka ili sa kraja (p/k)? ");
            string odgovor = Console.ReadLine();
            if (odgovor == "p") { p.MoveNext(); Console.WriteLine(p.Current); }
            else { k.MoveNext(); Console.WriteLine(k.Current); }
        }
    }
}
