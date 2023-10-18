using System;
using System.Collections.Generic;
using System.Drawing;

namespace SortingAlgorithms
{
    // klasa koja predstavlja algoritam Shell Sort
    public class ShellSort : Algorithm
    {
        public ShellSort(int[] _a, Color c, Color fc) : base(_a, c, fc, "Shell Sort") { }
        private int iBubble, iBubbleStart;

        override public void Display(Graphics g)
        {
            // karakteristični opsezi koje treba prikazati ispod stubića
            Display(g, new List<Tuple<int, int>>
            {
                // sortirani deo niza
                new Tuple<int, int>(iBubble, iBubble + 1), 
                
                // vrednost koja se trenutno poredi
                new Tuple<int, int>(iBubbleStart, iBubbleStart + 1) 
            });
        }

        public override IEnumerable<int> DoSortingStep()
        {
            // razmene koje po ovom algoritmu treba izvršiti radi sortiranja
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
