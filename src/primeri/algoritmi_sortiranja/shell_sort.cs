using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    public class ShellSort : Algorithm
    {
        public ShellSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Shell Sort") { }
        private int iBubble, iBubbleStart;

        override public void Display(Graphics g)
        {
            Display(g, new List<Tuple<int, int>>
            {
                new Tuple<int, int>(iBubble, iBubble + 1), // sorted part 
                new Tuple<int, int>(iBubbleStart, iBubbleStart + 1) // current comparing value
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            int n = a.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
                for (iBubbleStart = gap; iBubbleStart < n; iBubbleStart++)
                    for (iBubble = iBubbleStart; 
                            iBubble >= gap && a[iBubble - gap] > a[iBubble]; 
                            iBubble -= gap)
                        yield return Swap(iBubble, iBubble - gap);

            finished = true;
        }
    }
}
