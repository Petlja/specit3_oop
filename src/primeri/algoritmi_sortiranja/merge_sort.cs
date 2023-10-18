using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    // klasa koja predstavlja algoritam Merge Sort
    public class MergeSort : Algorithm
    {
        public MergeSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Merge Sort")
        {
            b = new int[a.Length];
            currentRange.Push(new Tuple<int, int>(0, a.Length));
        }

        private int[] b;
        private Stack<Tuple<int, int>> currentRange = new Stack<Tuple<int, int>>();

        override public void Display(Graphics g)
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
            Display(g, new List<Tuple<int, int>>
            {
                // podniz koji se trenutno spaja algoritmom Merge
                currentRange.Peek() 
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            // razmene koje po ovom algoritmu treba izvršiti radi sortiranja
            foreach (var _ in Sort(0, a.Length))
                yield return _;

            finished = true;
        }

        public IEnumerable<int> Sort(int lo, int hi)
        {
            // razmene koje po ovom algoritmu treba izvršiti radi sortiranja
            // (zadate rekurzivno)
            if (lo + 1 >= hi)
                yield break;

            currentRange.Push(new Tuple<int, int>(lo, hi-1));
            int mid = lo + (hi - lo) / 2;
            foreach (var _ in Sort(lo, mid))
                yield return _;

            foreach (var _ in Sort(mid, hi))
                yield return _;

            for (int p = lo; p < hi; p++) { b[p] = a[p]; a[p] = 0; }

            int i = lo, j = mid, k = lo;
            while (i < mid || j < hi)
            {
                if (j == hi || (i < mid && b[i] < b[j])) 
                    a[k++] = b[i++]; 
                else 
                    a[k++] = b[j++];

                numSwaps++;
                yield return 0;
            }
            currentRange.Pop();
        }
    }
}
