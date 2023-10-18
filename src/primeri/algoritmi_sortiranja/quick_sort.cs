using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    // klasa koja predstavlja algoritam Quick Sort
    public class QuickSort : Algorithm
    {
        public QuickSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Quick Sort")
        {
            currentRange.Push(new Tuple<int, int>(0, a.Length));
        }

        private Random rnd = new Random();
        private Stack<Tuple<int, int>> currentRange = new Stack<Tuple<int, int>>();
        private int i, j;
        override public void Display(Graphics g)
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
            Display(g, new List<Tuple<int, int>>
            {
                currentRange.Peek() // podniz koji se trenutno sortira
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
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

            currentRange.Push(new Tuple<int, int>(lo, hi - 1));
            int pivot = a[rnd.Next(lo, hi)];
            i = lo;
            j = hi;
            while (i < j)
            {
                if (a[i] < pivot) i++;
                else if (a[j - 1] > pivot) j--;
                else
                {
                    yield return Swap(i, j - 1);
                    i++; j--;
                }
            }

            foreach (var _ in Sort(lo, i))
                yield return _;

            foreach (var _ in Sort(i, hi))
                yield return _;

            currentRange.Pop();
        }
    }
}
