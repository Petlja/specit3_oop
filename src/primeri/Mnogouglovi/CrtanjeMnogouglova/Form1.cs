using System;
using System.Drawing;
using System.Windows.Forms;
using coordinate_converter;
using Polygons;

namespace CrtanjeMnogouglova
{
    public partial class Form1 : Form
    {
        CoordinateConverter cc;
        Polygon poly;
        bool IsDragging;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            MouseDown += new MouseEventHandler(Form1_MouseDown);
            MouseWheel += new MouseEventHandler(Form1_MouseMove);
            MouseMove += new MouseEventHandler(Form1_MouseMove);
            MouseUp += new MouseEventHandler(Form1_MouseUp);
            Paint += new System.Windows.Forms.PaintEventHandler(Form1_Paint);
            ClientSize = new System.Drawing.Size(800, 600);
            ResizeRedraw = true;

            cc = new CoordinateConverter(ClientSize.Width, ClientSize.Height);
            poly = new Polygon();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                IsDragging = true; // zapocni vucenje
                cc.SetPivot(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                poly.AddPoint(cc.XScreenToWorld(e.X), cc.YScreenToWorld(e.Y));
                Invalidate();
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
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                poly.RemoveLastPoint();
                Invalidate();
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Text = poly.Text;
            Graphics g = e.Graphics;
            cc.DrawGrid(g, ClientSize.Width, ClientSize.Height, Color.Black);

            // Izracunaj temena mnogougla u koordinatama ekrana i nacrtaj ga
            int n = poly.PointCount;
            if (n > 0)
            {
                Pen pen = new Pen(Color.Blue, 3);
                g.DrawLine(pen, 
                    cc.XWorldToScreen(poly.X(n - 1)), cc.YWorldToScreen(poly.Y(n - 1)),
                    cc.XWorldToScreen(poly.X(0)), cc.YWorldToScreen(poly.Y(0)));
                for (int i = 1; i < n; i++)
                    g.DrawLine(pen,
                    cc.XWorldToScreen(poly.X(i - 1)), cc.YWorldToScreen(poly.Y(i - 1)),
                    cc.XWorldToScreen(poly.X(i)), cc.YWorldToScreen(poly.Y(i)));
            }
        }
    }
}
