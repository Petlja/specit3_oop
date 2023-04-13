using System;
using System.Drawing;

namespace coordinate_converter
{
    public class CoordinateConverter
    {
        private float FontSize = 16.0f;
        private float ScalingX = 50.0f;
        private float ScalingY = 50.0f;
        private float TranslationX;
        private float TranslationY;
        private float xePivot;
        private float yePivot;
        private string PivotPos = "";
        public CoordinateConverter(float xeSize, float yeSize)
        {
            // Biramo pocetni polozaj sistema sveta tako da je 
            // tacka (-1, -1) u donjem levom uglu prozora
            TranslationX = ScalingX;
            TranslationY = yeSize - ScalingY;
        }
        public void SetPivot(float x, float y)
        {
            xePivot = x;
            yePivot = y;
            PivotPos = string.Format("({0:0.00}, {1:0.00})",
                XScreenToWorld(xePivot), YScreenToWorld(yePivot));
        }
        public void Translate(float xeNewPivot, float yeNewPivot)
        {
            TranslationX += xeNewPivot - xePivot;
            TranslationY += yeNewPivot - yePivot;
            xePivot = xeNewPivot;
            yePivot = yeNewPivot;
        }
        public void Zoom(float zoomFactor)
        {
            TranslationX = xePivot + (TranslationX - xePivot) * zoomFactor;
            TranslationY = yePivot + (TranslationY - yePivot) * zoomFactor;
            FontSize *= zoomFactor;
            ScalingX *= zoomFactor;
            ScalingY *= zoomFactor;
        }
        public void DrawGrid(Graphics g, float xeSize, float yeSize, Color clr)
        {
            Font fnt = new Font("Arial", FontSize);

            //ClientRectangle u koordinatama ekrana
            float xs0 = 0, xs1 = xeSize;
            float ys0 = 0, ys1 = yeSize;

            //ClientRectangle u koordinatama sveta
            float xw0 = XScreenToWorld(xs0);
            float yw0 = YScreenToWorld(ys0);
            float xw1 = XScreenToWorld(xs1);
            float yw1 = YScreenToWorld(ys1);

            // koordinate linija resetke u sistemu sveta
            float gxw0 = MathF.Ceiling(xw0);
            float gxw1 = MathF.Floor(xw1);
            float gyw0 = MathF.Floor(yw0);
            float gyw1 = MathF.Ceiling(yw1);

            // koordinate linija resetke u sistemu ekrana
            float gxs0 = XWorldToScreen(gxw0);
            float gys0 = YWorldToScreen(gyw0);
            float gxs1 = XWorldToScreen(gxw1);
            float gys1 = YWorldToScreen(gyw1);
            float xsYAxis = XWorldToScreen(0); // ekranska koordinata y ose
            float ysXAxis = YWorldToScreen(0); // ekranska koordinata x ose

            Pen p1 = new Pen(clr, 1);
            Brush b = new SolidBrush(clr); // za crtanje teksta (koordinata)

            // uspravne linije resetke, ispisivanje x koordinata
            float xw = gxw0;
            for (float xs = gxs0; xs <= gxs1; xs += ScalingX)
            {
                g.DrawLine(p1, xs, ys0, xs, ys1);
                string txt = xw.ToString();
                SizeF txtSize = g.MeasureString(txt, fnt);
                g.DrawString(txt, fnt, b, xs, ys1 - txtSize.Height);
                xw += 1;
            }

            // vodoravne linije resetke, ispisivanje y koordinata
            float yw = gyw0;
            for (float ys = gys0; ys <= gys1; ys += ScalingY)
            {
                g.DrawLine(p1, xs0, ys, xs1, ys);
                g.DrawString(yw.ToString(), fnt, b, xs0, ys);
                yw -= 1;
            }

            Pen p3 = new Pen(clr, 3); // koordinatne ose crtamo podebljano 
            g.DrawLine(p3, xs0, ysXAxis, xs1, ysXAxis); // x-osa
            g.DrawLine(p3, xsYAxis, ys0, xsYAxis, ys1); // y-osa
            g.DrawString(PivotPos, new Font("Arial", 16), b, xePivot, yePivot); // pozicija pivota
        }
        public float XWorldToScreen(float xw) { return xw * ScalingX + TranslationX; }
        public float YWorldToScreen(float yw) { return -yw * ScalingY + TranslationY; }
        public float XScreenToWorld(float xs) { return (xs - TranslationX) / ScalingX; }
        public float YScreenToWorld(float ys) { return (ys - TranslationY) / -ScalingY; }
        public PointF WorldToScreen(PointF w)
        {
            return new PointF(XWorldToScreen(w.X), YWorldToScreen(w.Y));
        }
        public PointF ScreenToWorld(PointF s)
        {
            return new PointF(XScreenToWorld(s.X), YScreenToWorld(s.Y));
        }
    }
}
