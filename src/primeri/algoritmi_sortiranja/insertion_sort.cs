using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    public class InsertionSort : Algorithm
    {
        public InsertionSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Insertion Sort") { }
        private int nSorted, iPos;
        override public void Display(Graphics g)
        {
            Display(g, new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, iPos),
                new Tuple<int, int>(iPos + 1, nSorted)
            });
        }
        public override IEnumerable<int> DoSortingStep()
        {
            for (nSorted = 1; nSorted < a.Length; nSorted++)
                for (iPos = nSorted - 1; iPos >= 0 && a[iPos] > a[iPos + 1]; iPos--)
                    yield return Swap(iPos, iPos + 1);

            finished = true;
        }
    }
}
