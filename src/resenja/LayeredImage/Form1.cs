using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayeredImage
{
    public partial class Form1 : Form
    {
        Image im = new Image();
        public Form1()
        {
            InitializeComponent();
        }

        public void Test()
        {
            im = new Image();
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            int pauseDuration = 21000;
            Layer l1 = new Layer(w, h, true, Color.Green);
            Layer l2 = new Layer(w, h, false, Color.Purple);
            im.AddLayer(l1);
            im.AddLayer(l2);

            Rectangle r11 = new Rectangle(10, 10, 160, 120, Color.Red, true);
            Rectangle r12 = new Rectangle(20, 20, 160, 120, Color.Blue, true);
            Rectangle r13 = new Rectangle(30, 30, 160, 120, Color.White, false);
            l1.Add(r11); l1.Add(r12); l1.Add(r13);

            Rectangle r21 = new Rectangle(60, 60, 40, 30, Color.Cyan, true);
            Rectangle r22 = new Rectangle(70, 70, 40, 30, Color.Yellow, true);
            l2.Add(r21); l2.Add(r22);
            Refresh();
            Thread.Sleep(pauseDuration);

            l1.Select(r12);
            l1.BringToFront();
            Refresh();
            Thread.Sleep(pauseDuration);

            l1.SetToBack();
            Refresh();
            Thread.Sleep(pauseDuration);

            im.SelectLayer(0);
            im.BringToFront();
            Refresh();
            Thread.Sleep(pauseDuration);

            im.SelectLayer(1);
            im.SetToBack();
            Refresh();
            Thread.Sleep(pauseDuration);

            l2.SetBackground(true);
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            im.Draw(e.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
    public class Rectangle
    {
        private float x, y, w, h;
        private Color color;
        private bool filled;
        public Rectangle(float x0, float y0, float w0, float h0, Color c, bool f)
        {
            x = x0; y = y0; w = w0; h = h0;
            color = c;
            filled = f;
        }
        //public float X { get { return x; } }
        //public float Y { get { return y; } }
        //public float W { get { return w; } }
        //public float H { get { return h; } }
        //public Color GetColor { get { return color; } }
        public void Draw(Graphics g)
        {
            if (filled)
            {
                Brush b = new SolidBrush(color);
                g.FillRectangle(b, x, y, w, h);
            }
            else
            {
                Pen p = new Pen(color, 3);
                g.DrawRectangle(p, x, y, w, h);
            }
        }
    }

    public class Layer
    {
        private int w;
        private int h;
        private Color bgColor;
        private bool bgIsTransparent;
        private List<Rectangle> rectangles = new List<Rectangle>();
        private HashSet<int> selectedIndices = new HashSet<int>();
        public Layer(int w0, int h0, bool opaque, Color c)
        {
            w = w0; h = h0; bgIsTransparent = !opaque; bgColor = c;
        }

        public void Add(Rectangle r)
        {
            rectangles.Add(r);
        }
        public void SetBackground(bool opaque)
        {
            bgIsTransparent = !opaque;
        }
        public void SetBackground(bool opaque, Color c)
        {
            bgIsTransparent = !opaque; 
            bgColor = c;
        }
        public bool Select(Rectangle r)
        {
            int index = rectangles.IndexOf(r);
            if (index >= 0)
                selectedIndices.Add(index);
            
            return index >= 0;
        }
        public bool BringToFront()
        {
            if (selectedIndices.Count != 1)
                return false;

            int index = selectedIndices.Single();
            Rectangle r = rectangles[index];
            rectangles.RemoveAt(index);
            rectangles.Add(r);
            selectedIndices.Clear();
            selectedIndices.Add(rectangles.Count - 1);
            return true;
        }
        public bool SetToBack()
        {
            if (selectedIndices.Count != 1)
                return false;

            int index = selectedIndices.Single();
            Rectangle r = rectangles[index];
            rectangles.RemoveAt(index);
            rectangles.Insert(0, r);
            selectedIndices.Clear();
            selectedIndices.Add(0);
            return true;
        }
        public void Draw(Graphics g)
        {
            if (!bgIsTransparent)
            {
                Brush b = new SolidBrush(bgColor);
                g.FillRectangle(b, 0, 0, w, h);
            }
            foreach (Rectangle r in rectangles)
                r.Draw(g);
        }
        public override string ToString()
        {
            return string.Format("{0}({1})", bgColor, rectangles.Count);
        }
    }
    public class Image
    {
        private List<Layer> layers = new List<Layer>();
        private int selectedLayer = -1;
        public void SelectLayer(int i)
        {
            if (0 <= i && i < layers.Count)
                selectedLayer = i;
        }
        public void AddLayer(Layer l)
        {
            selectedLayer = layers.Count;
            layers.Add(l);
        }
        public void BringToFront()
        {
            Layer l = layers[selectedLayer];
            layers.RemoveAt(selectedLayer);
            layers.Add(l);
        }
        public void SetToBack()
        {
            Layer l = layers[selectedLayer];
            layers.RemoveAt(selectedLayer);
            layers.Insert(0, l);
        }
        public void Draw(Graphics g)
        {
            foreach (Layer l in layers)
                l.Draw(g);
        }
    }
}
