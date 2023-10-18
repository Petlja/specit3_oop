using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    // klasa koja predstavlja algoritam Selection Sort
    public class SelectionSort : Algorithm
    {
        public SelectionSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Selection Sort") { }

        private int nSorted, iMin, iCmp;

        override public void Display(Graphics g)
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
            Display(g, new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, nSorted), // sortirani deo 
                new Tuple<int, int>(iMin, iMin + 1), // tekući minimum
                new Tuple<int, int>(iCmp, iCmp + 1) // tekući kadidat za minimum
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            // razmene koje po ovom algoritmu treba izvršiti radi sortiranja
            for (nSorted = 0; nSorted < a.Length - 1; nSorted++)
            {
                iMin = nSorted;
                for (iCmp = nSorted + 1; iCmp < a.Length; iCmp++)
                    if (a[iCmp] < a[iMin])
                        yield return Swap(iCmp, iMin);
            }
            finished = true;
        }
    }
}
