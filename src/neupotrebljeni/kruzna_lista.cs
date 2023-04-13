using System;
using System.Collections.Generic;

internal class Program
{
    public class KruznaLista<T>
    {
        private T[] a;
        private int i;
        public KruznaLista(T[] objekti)
        {
            int n = objekti.Length;
            i = 0;
            a = new T[n];
            for (int i = 0; i < n; i++)
                a[i] = objekti[i];
        }

        public T Tekuci() { return a[i]; }
        public void Pomak(int d)
        {
            int n = a.Length;
            d = d % n;
            if (d < 0) d += n;
            i = (i + d) % n;
        }
    }

    static void Main(string[] args)
    {
        int[] x = { 2, 5, 6, 4 };
        KruznaLista<int> kl = new KruznaLista<int>(x);
        Console.WriteLine(kl.Tekuci());
        kl.Pomak(-10);
        Console.WriteLine(kl.Tekuci());
    }
}
