using System;
using System.Drawing;
using System.Windows.Forms;

using Polygons;

namespace PolygonsGraphics
{
    public partial class Form1 : Form
    {
        static float X0, Y0, XUnit, YUnit;
        float LastDraggingPosX = -1;
        float LastDraggingPosY = -1;
        bool IsDragging = false;

        string CurrentMousePos = "";
        float CurrentMousePosX = -1;
        float CurrentMousePosY = -1;
        float FontSize = 16;

        Polygon poly = new Polygon(); // mnogougao (u koordinatama sveta)

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseWheel += new MouseEventHandler(Form1_MouseMove);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(Form1_Paint);
            this.ClientSize = new System.Drawing.Size(800, 600);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            XUnit = 50.0f;
            YUnit = 50.0f;
            ResizeRedraw = true;
            // Biramo pocetni polozaj sistema sveta tako da je 
            // tacka (-1, -1) tacno u donjem levom uglu prozora
            X0 = XUnit;
            Y0 = ClientSize.Height - YUnit;
        }
        static float XWorldToScreen(float x) { return X0 + XUnit * x; }
        static float YWorldToScreen(float y) { return Y0 - YUnit * y; }
        static float XScreenToWorld(float x) { return (x - X0) / XUnit; }
        static float YScreenToWorld(float y) { return (Y0 - y) / YUnit; }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // zapocni vucenje
                IsDragging = true;
                LastDraggingPosX = e.X;
                LastDraggingPosY = e.Y;
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Dodaj novo teme, preracunato u koordinate sveta
                float x = XScreenToWorld(e.X); 
                float y = YScreenToWorld(e.Y); 
                poly.AddPoint(x, y);
                UpdateAreaAndPerimeter();
                Invalidate();
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                X0 += e.X - LastDraggingPosX;
                Y0 += e.Y - LastDraggingPosY;
                LastDraggingPosX = e.X;
                LastDraggingPosY = e.Y;
                Invalidate();
            }
            if (e.Delta != 0)
            {
                // tocak misa je okrenut, zumiraj (ka ili od)
                int WheelDelta = SystemInformation.MouseWheelScrollDelta;
                float f = (float)Math.Pow(1.1, -e.Delta / WheelDelta);
                XUnit *= f;
                YUnit *= f;
                FontSize *= f;
                Invalidate();
            }
            // azuriraj koordinate misa za prikaz na ekranu
            CurrentMousePosX = e.X;
            CurrentMousePosY = e.Y;
            CurrentMousePos = string.Format("({0:0.00}, {1:0.00})",
                XScreenToWorld(e.X), YScreenToWorld(e.Y));
            Invalidate();
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                IsDragging = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                poly.RemoveLastPoint();
                UpdateAreaAndPerimeter();
                Invalidate();
            }
        }

        private void DrawGrid(Graphics g)
        {
            Font fnt = new Font("Arial", FontSize);

            // uglovi prozora u koordinatama ekrana
            float xs0 = 0, xs1 = ClientSize.Width;
            float ys0 = 0, ys1 = ClientSize.Height;

            // uglovi prozora u koordinatama sveta
            float xw0 = XScreenToWorld(xs0);
            float yw0 = YScreenToWorld(ys0);
            float xw1 = XScreenToWorld(xs1);
            float yw1 = YScreenToWorld(ys1);

            // koordinate prve i poslednje linija resetke
            // za svaku osu, u koordinatama sveta
            float gxw0 = (float)Math.Ceiling(xw0);
            float gxw1 = (float)Math.Floor(xw1);
            float gyw0 = (float)Math.Floor(yw0);
            float gyw1 = (float)Math.Ceiling(yw1);

            // koordinate prve i poslednje linija resetke
            // za svaku osu, u koordinatama ekrana
            float gxs0 = XWorldToScreen(gxw0);
            float gys0 = YWorldToScreen(gyw0);
            float gxs1 = XWorldToScreen(gxw1);
            float gys1 = YWorldToScreen(gyw1);

            // koordinate koordinatnih osa sistema sveta
            // u koordinatama ekrana
            float xsYAxis = XWorldToScreen(0);
            float ysXAxis = YWorldToScreen(0);

            Pen p1 = new Pen(Color.Black, 1);
            Brush b = new SolidBrush(Color.Black);

            // uspravne linije
            float xw = gxw0;
            for (float xs = gxs0; xs <= gxs1; xs += XUnit)
            {
                g.DrawLine(p1, xs, ys0, xs, ys1);
                string txt = xw.ToString();
                SizeF txtSize = g.MeasureString(txt, fnt);
                g.DrawString(txt, fnt, b, xs, ys1 - txtSize.Height);
                xw += 1;
            }

            // vodoravne linije
            float yw = gyw0;
            for (float ys = gys0; ys <= gys1; ys += YUnit)
            {
                g.DrawLine(p1, xs0, ys, xs1, ys);
                g.DrawString(yw.ToString(), fnt, b, xs0, ys);
                yw -= 1;
            }

            // ose
            Pen p3 = new Pen(Color.Black, 3);
            g.DrawLine(p3, xs0, ysXAxis, xs1, ysXAxis); // x-osa
            g.DrawLine(p3, xsYAxis, ys0, xsYAxis, ys1); // y-osa

            // prikazi koordinate kursora
            if (CurrentMousePos != "")
            {
                g.DrawString(CurrentMousePos, new Font("Arial", 16), b,
                    CurrentMousePosX, CurrentMousePosY);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGrid(g);
            Pen p = new Pen(Color.Blue, 3);

            // Nacrtaj mnogougao, koristeci koordinate ekrana
            int n = poly.NumPoints;
            if (n > 0)
            {
                float r = 3;
                PointF pt0 = new PointF(
                    XWorldToScreen((float)poly.X(0)),
                    YWorldToScreen((float)poly.Y(0)));
                PointF ptPrev = pt0;
                for (int i = 1; i < n; i++)
                {
                    PointF pt = new PointF(
                        XWorldToScreen((float)poly.X(i)),
                        YWorldToScreen((float)poly.Y(i)));
                    g.DrawEllipse(p, pt.X - r, pt.Y - r, 2 * r, 2 * r);
                    g.DrawLine(p, ptPrev, pt);
                    ptPrev = pt;
                }
                g.DrawLine(p, ptPrev, pt0);
                g.DrawEllipse(p, pt0.X-r, pt0.Y-r, 2*r, 2*r);
            }
        }
        private void UpdateAreaAndPerimeter()
        {
            Text = string.Format("Обим={0:0.00}, Површина={1:0.00}",
                poly.Perimeter(), poly.Area());
        }
    }
}
