using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    public class SelectionSort : Algorithm
    {
        public SelectionSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Selection Sort") { }

        private int nSorted, iMin, iCmp;

        override public void Display(Graphics g)
        {
            Display(g, new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, nSorted), // sorted part 
                new Tuple<int, int>(iMin, iMin + 1), // current min
                new Tuple<int, int>(iCmp, iCmp + 1) // current comparing value
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
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
