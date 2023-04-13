using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using coordinate_converter;
using FunctionValue;

namespace Grafik
{
    public partial class Form1 : Form
    {
        CoordinateConverter cc;
        Function F = null;
        string expression = "";
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

            cc = new CoordinateConverter(ClientSize.Width, ClientSize.Height);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            expression = Prompt.ShowDialog("Унесите функцију од X:", "Задавање функције", expression);
            string errMessage = "";
            if (Parser.Evaluate(expression, out F, out errMessage))
            {
                Text = string.Format("Функција {0}", F.ToString());
                Invalidate();
            }
            else
            {
                // ako ne uspe da parsira, F ostaje null 
                MessageBox.Show(errMessage);
            }
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
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                Invalidate();
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            cc.DrawGrid(g, ClientSize.Width, ClientSize.Height, Color.Black);

            if (F != null)
            {
                int penWidth = 3;
                Pen p3 = new Pen(Color.Blue, penWidth);
                float xsPrev = 0.0f;
                float xwPrev = cc.XScreenToWorld(xsPrev);
                float ywPrev = (float)F.Value(xwPrev);
                float ysPrev = cc.YWorldToScreen(ywPrev);
                for (float xs = 0; xs < ClientSize.Width; xs++)
                {
                    float xw = cc.XScreenToWorld(xs);
                    float yw = (float)F.Value(xw);
                    float ys = cc.YWorldToScreen(yw);
                    if (float.IsFinite(ys) && float.IsFinite(ysPrev))
                    {
                        ysPrev = MathF.Min(MathF.Max(-penWidth, ysPrev), ClientSize.Height + penWidth);
                        ys = MathF.Min(MathF.Max(-penWidth, ys), ClientSize.Height + penWidth);
                        g.DrawLine(p3, xsPrev, ysPrev, xs, ys);
                    }
                    xsPrev = xs;
                    ysPrev = ys;
                }
            }
        }
    }
}
