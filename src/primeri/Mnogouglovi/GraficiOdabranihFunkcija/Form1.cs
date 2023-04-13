using System;
using System.Drawing;
using System.Windows.Forms;
using coordinate_converter;

namespace GraficiOdabranihFunkcija
{
    public partial class Form1 : Form
    {
        private enum Function { Sin, Cos, Tg, Ctg, Sqr, Sqrt, Exp, Log, None };
        private Function SelectedFunction = Function.None;
        private CoordinateConverter cc;
        bool IsDragging;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            MouseDown += new MouseEventHandler(Form1_MouseDown);
            MouseWheel += new MouseEventHandler(Form1_MouseMove);
            MouseMove += new MouseEventHandler(Form1_MouseMove);
            MouseUp += new MouseEventHandler(Form1_MouseUp);
            Paint += new System.Windows.Forms.PaintEventHandler(Form1_Paint);
            ClientSize = new System.Drawing.Size(800, 600);
            ResizeRedraw = true;
            Text = "Цртање графика";
            cc = new CoordinateConverter(ClientSize.Width, ClientSize.Height);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                IsDragging = true; // zapocni vucenje
                cc.SetPivot(e.X, e.Y);
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
                cc.Translate(e.X, e.Y);

            if (e.Delta != 0) // ako je tocak misa okrenut, zumiraj (ka ili od)
            {
                float WheelDelta = SystemInformation.MouseWheelScrollDelta;
                float f = MathF.Pow(1.1f, -e.Delta / WheelDelta);
                cc.Zoom(f);
            }
            cc.SetPivot(e.X, e.Y); // azurira koordinate misa za prikaz koordinata na ekranu
            Invalidate();
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                IsDragging = false;
        }

        private void sineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Sin;
            Text = "Цртање графика - синус";
        }

        private void cosineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Cos;
            Text = "Цртање графика - косинус";
        }

        private void tangentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Tg;
            Text = "Цртање графика - тангенс";
        }

        private void cotangentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Ctg;
            Text = "Цртање графика - котангенс";
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Sqr;
            Text = "Цртање графика - квадрат";
        }

        private void squareRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Sqrt;
            Text = "Цртање графика - квадратни корен";
        }

        private void exponentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Exp;
            Text = "Цртање графика - експоненцијална";
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFunction = Function.Log;
            Text = "Цртање графика - природни логаритам";
        }
        private float F(float x)
        {
            switch (SelectedFunction)
            {
                case Function.Sin: return MathF.Sin(x);
                case Function.Cos: return MathF.Cos(x);
                case Function.Tg: return MathF.Tan(x);
                case Function.Ctg: return 1.0f / MathF.Tan(x);
                case Function.Sqr: return x * x;
                case Function.Sqrt: return MathF.Sqrt(x);
                case Function.Exp: return MathF.Exp(x);
                case Function.Log: return MathF.Log(x);
            }
            throw new Exception("Nepoznata funcija");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            cc.DrawGrid(g, ClientSize.Width, ClientSize.Height, Color.Black);

            // grafik
            if (SelectedFunction == Function.None)
                return;

            Pen p3 = new Pen(Color.Blue, 3);
            float xsPrev = 0.0f;
            float xwPrev = cc.XScreenToWorld(xsPrev);
            float ywPrev = F(xwPrev);
            float ysPrev = cc.YWorldToScreen(ywPrev);
            for (float xs = 0; xs < ClientSize.Width; xs++)
            {
                float xw = cc.XScreenToWorld(xs);
                float yw = F(xw);
                float ys = cc.YWorldToScreen(yw);
                // ako je F definisana u prethodnoj i tekucoj tacki
                // i te tacke nisu u razlicitim granama grafika
                // spoj te tacke linijom
                if (float.IsFinite(ys) && float.IsFinite(ysPrev) && 
                    MathF.Abs(ysPrev - ys) < ClientSize.Height / 2)
                    g.DrawLine(p3, xsPrev, ysPrev, xs, ys);
                xsPrev = xs;
                ysPrev = ys;
            }
        }
    }
}
