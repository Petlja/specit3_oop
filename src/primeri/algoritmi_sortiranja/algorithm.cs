using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SortingAlgorithms
{
        public abstract class Algorithm
        {
            abstract public void Display(Graphics g);
            abstract public IEnumerable<int> DoSortingStep();

        public void SetWindow(float l, float t, float w, float h)
        {
            wndTop = t;
            wndLeft = l;
            wndWidth = w;
            wndHeight = h;
        }
        public bool Finished { get { return finished; } }

        protected int[] a;
        protected string name;
        protected int numSwaps;
        protected bool finished;

        private int aMax;
        private float wndLeft, wndTop, wndWidth, wndHeight;
        private Color color;
        private Color finishColor;
        protected Algorithm(int[] _a, Color c, Color fc, string n) 
        {
            a = (int[])_a.Clone();
            color = c;
            finishColor = fc;
            name = n;

            aMax = a.Max(); // max needed for displaying
            numSwaps = 0;
        }
        protected void Display(Graphics g, List<Tuple<int, int>> ranges)
        {
            float h = wndHeight - 10; // 10 pixels for additional drawing by each specific algorithm
            float barWidth = wndWidth / (float)a.Length;
            float k = h / (float)aMax;
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(finished ? finishColor : color);

            // bars
            for (int i = 0; i < a.Length; i++)
            {
                float barHeight = a[i] * k;
                float x = wndLeft + i * barWidth;
                float y = wndTop + h - barHeight;
                g.FillRectangle(brush, x, y, barWidth, barHeight);
                g.DrawRectangle(pen, x, y, barWidth, barHeight);
            }

            // algorithm name and step count
            brush = new SolidBrush(Color.Black);
            string s = string.Format("{0}:\n{1} korak(a)", name, numSwaps);
            g.DrawString(s, new Font("Arial", 16), brush, wndLeft + 10, wndTop + 10);
            g.DrawRectangle(pen, wndLeft, wndTop, wndWidth, wndHeight);

            // specific ranges below bars
            if (!finished)
            {
                float y = wndTop + h;
                foreach (var r in ranges)
                {
                    float x = wndLeft + r.Item1 * barWidth;
                    float w = wndLeft + r.Item2 * barWidth - x;
                    g.FillRectangle(brush, x, y, w, 10);
                }
            }
        }
        protected int Swap(int p1, int p2)
        {
            int tmp = a[p1];
            a[p1] = a[p2];
            a[p2] = tmp;
            numSwaps++;
            return 0;
        }
    }
}
