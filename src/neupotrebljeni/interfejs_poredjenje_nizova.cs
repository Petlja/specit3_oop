using System;
using System.Collections.Generic;

namespace Primer
{
    public class Niz : IComparable
    {
        int[] a;
        public Niz(int[] _a) { a = (int[])_a.Clone(); }
        public int Count { get { return a.Length; } }
        public int this[int i] { get { return a[i]; } }
        public int CompareTo(object obj)
        {
            Niz b = (Niz)obj;
            if (a == null && b.a == null)
                return 0;

            if (a == null)
                return -1;

            if (b.a == null)
                return 1;

            for (int i = 0; i < a.Length && i < b.a.Length; i++)
                if (a[i] != b.a[i])
                    return a[i] - b.a[i];

            return a.Length - b.a.Length;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Niz> aa = new List<Niz>();
            aa.Add(new Niz(new int[] { 31, 32, 33 }));
            aa.Add(new Niz(new int[] { 21, 22, 23, 24 }));
            aa.Add(new Niz(new int[] { 21, 22 }));
            aa.Add(new Niz(new int[] { 11, 12, 13, 14, 15 }));

            aa.Sort();
            foreach (var a in aa)
            {
                for (int i = 0; i < a.Count; i++)
                    Console.Write("{0} ", a[i]);

                Console.WriteLine();
            }
        }
    }
}
