using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SortingAlgorithms
{
    public partial class Form1 : Form
    {
        List<Algorithm> algorithms;
        List<IEnumerator<int>> enumerators;
        public Form1()
        {
            InitializeComponent();
        }

        private void Reset() 
        {
            // re-generate array
            int arrayLength = 100;
            Random rnd = new Random();
            int[] a = new int[arrayLength];
            for (int i = 0; i < a.Length; i++)
                a[i] = rnd.Next(5, 300);

            algorithms = new List<Algorithm>
            {
                new SelectionSort(a, Color.Red, Color.DarkRed),
                new BubbleSort(a, Color.Orange, Color.DarkOrange),
                new InsertionSort(a, Color.LightYellow, Color.Yellow),
                new MergeSort(a, Color.Cyan, Color.DarkCyan),
                new ShellSort(a, Color.LightGreen, Color.Green),
                new QuickSort(a, Color.Blue, Color.DarkBlue)
            };

            enumerators = new List<IEnumerator<int>>();
            foreach (Algorithm sa in algorithms)
                enumerators.Add(sa.DoSortingStep().GetEnumerator());

            ArrangeSubwindows();
            timer1.Enabled = true;
        }

        private void ArrangeSubwindows()
        {
            float w = ((float)ClientSize.Width - 10.0f) / 3.0f;
            float h = ((float)ClientSize.Height - 10.0f) / 2.0f;

            if (algorithms != null)
                for (int row = 0; row < 2; row++)
                    for (int col = 0; col < 3; col++)
                        algorithms[3 * row + col].SetWindow(10 + col * w, 10 + row * h, w - 10, h - 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Algoritmi sortiranja (pritisni P za pauzu, R za reset)";
            ResizeRedraw = true;
            Reset();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ArrangeSubwindows();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var en in enumerators)
                en.MoveNext();

            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
                timer1.Enabled = !timer1.Enabled;
            if (e.KeyCode == Keys.R)
                Reset();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Algorithm sa in algorithms)
                sa.Display(e.Graphics);
        }
    }
}
