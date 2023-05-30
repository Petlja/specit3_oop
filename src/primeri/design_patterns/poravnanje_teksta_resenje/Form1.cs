using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PoravnanjeTeksta
{
    public partial class Form1 : Form
    {
        List<string> paragraphs = new List<string>();
        int alignmentType = 0;
        Font font = new System.Drawing.Font("Arial", 16);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                paragraphs.Clear();
                foreach(string line in File.ReadLines(openFileDialog1.FileName))
                    paragraphs.Add(line);

                Invalidate();
            }
        }
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alignmentType = 0;
            Invalidate();
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alignmentType = 1;
            Invalidate();
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alignmentType = 2;
            Invalidate();
        }

        private void justifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alignmentType = 3;
            Invalidate();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog1.Font;
                Invalidate();
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (paragraphs == null)
                return;

            Graphics g = e.Graphics;
            TextAligner ta = TextAligner.Create(font, alignmentType);
            ta.DisplayText(g, paragraphs, menuStrip1.Height + 10, ClientSize);
        }
    }
}
