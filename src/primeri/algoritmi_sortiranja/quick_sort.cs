using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
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
            Display(g, new List<Tuple<int, int>>
            {
                currentRange.Peek() // sub-array currently being merged
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            foreach (var _ in Sort(0, a.Length))
                yield return _;

            finished = true;
        }

        public IEnumerable<int> Sort(int lo, int hi)
        {
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
