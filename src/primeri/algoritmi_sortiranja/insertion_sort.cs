using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    // klasa koja predstavlja algoritam Insertion Sort
    public class InsertionSort : Algorithm
    {
        public InsertionSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Insertion Sort") { }
        private int nSorted, iPos;
        override public void Display(Graphics g)
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
            Display(g, new List<Tuple<int, int>>
            {
                // ova dva opsega zajedno predstavljaju sortirani deo niza
                new Tuple<int, int>(0, iPos), // deo niza u kome se još traži mesto
                new Tuple<int, int>(iPos + 1, nSorted) // pretraženi deo sortiranog dela niza
            });
        }
        public override IEnumerable<int> DoSortingStep()
        {
            // razmene koje po ovom algoritmu treba izvršiti radi sortiranja
            for (nSorted = 1; nSorted < a.Length; nSorted++)
                for (iPos = nSorted - 1; iPos >= 0 && a[iPos] > a[iPos + 1]; iPos--)
                    yield return Swap(iPos, iPos + 1);

            finished = true;
        }
    }
}
