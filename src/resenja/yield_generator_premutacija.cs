using System;
using System.Collections.Generic;

namespace Primer
{
    public class GeneratorPermutacija
    {
        char[] a;
        public IEnumerable<string> Permutacije(string polazni)
        {
            a = polazni.ToCharArray();
            foreach (int x in Permutacije(0))
                yield return new string(a);
        }

        public IEnumerable<int> Permutacije(int d)
        {
            if (d == a.Length)
                yield return 0;
            else
            {
                for (int i = d; i < a.Length; i++)
                {
                    char c = a[i]; a[i] = a[d]; a[d] = c;
                    foreach (int x in Permutacije(d + 1))
                        yield return x;

                    c = a[i]; a[i] = a[d]; a[d] = c;
                }
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            GeneratorPermutacija gp = new GeneratorPermutacija();
            foreach (string s in gp.Permutacije("sve"))
                Console.WriteLine(s);
        }
    }
}
