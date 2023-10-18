using System;
using System.Collections.Generic;

namespace Polygons
{
    // klasa koja predstavlja mnogougao
    public class Polygon
    {
        private List<Point> pts = new List<Point>(); // lista temena
        private double perimeter; // obim mnogougla
        private double area; // površina mnogougla
        public int PointCount { get { return pts.Count; } }
        public string Text // tekstualni opis mnogougla
        {
            get { return string.Format("Обим={0:0.00}, Површина={1:0.00}", perimeter, area); }
        }

        public void AddPoint(float x, float y)
        {
            pts.Add(new Point(x, y));
            UpdateAreaAndPerimeter();
        }

        public void RemoveLastPoint()
        {
            if (pts.Count > 0)
            {
                pts.RemoveAt(pts.Count - 1);
                UpdateAreaAndPerimeter();
            }
        }

        public int NumPoints { get { return pts.Count; } }
        public float X(int i) { return pts[i].X; }
        public float Y(int i) { return pts[i].Y; }
        
        private void UpdateAreaAndPerimeter()
        {
            perimeter = Perimeter();
            area = Area();
        }

        public double Perimeter()
        {
            int n = pts.Count;
            if (n == 0)
                return 0.0;
            double s = Point.Dist(pts[n - 1], pts[0]);
            for (int i = 1; i < n; i++) 
                s+= Point.Dist(pts[i - 1], pts[i]);
            return s;
        }

        public double Area()
        {
            int n = pts.Count;
            if (n < 3)
                return 0.0;

            // formula pertle
            double a = 
                pts[0].X * (pts[1].Y - pts[n - 1].Y) +
                pts[n-1].X * (pts[0].Y - pts[n - 2].Y);
            for (int i = 1; i < n-1; i++)
                a += pts[i].X * (pts[i+1].Y - pts[i-1].Y);

            return 0.5 * Math.Abs(a);
        }
    }

    // klasa vidljiva samo unutar biblioteke
    internal class Point 
    {
        private float x;
        private float y;
        public float X { get { return x; } }
        public float Y { get { return y; } }
        public Point(float x0, float y0) { x = x0; y = y0; }
        public static double Dist(Point A, Point B)
        {
            return Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y));
        }
    }
}
