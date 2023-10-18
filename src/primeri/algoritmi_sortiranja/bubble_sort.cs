using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    // klasa koja predstavlja algoritam Bubble Sort
    public class BubbleSort : Algorithm
    {
        private int iSortedFrom, iBubble;

        public BubbleSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Bubble Sort") { }

        override public void Display(Graphics g)
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
            Display(g, new List<Tuple<int, int>>
            {
                new Tuple<int, int>(iBubble, iBubble + 1), // šetajući "balončić"
                new Tuple<int, int>(iSortedFrom, a.Length) // sortirani deo niza
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            // razmene koje po ovom algoritmu treba izvršiti radi sortiranja
            for (iSortedFrom = a.Length - 1; iSortedFrom > 0; iSortedFrom--)
                for (iBubble = 0; iBubble < iSortedFrom; iBubble++)
                    if (a[iBubble] > a[iBubble + 1])
                        yield return Swap(iBubble, iBubble + 1);

            finished = true;
        }
    }
}
