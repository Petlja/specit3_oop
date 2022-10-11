using System;
using System.Collections.Generic;

namespace Polygons
{
    public class Polygon
    {
        private List<Point> P = new List<Point>();
        public void AddPoint(double x, double y)
        {
            P.Add(new Point(x, y));
        }
        public void RemoveLastPoint()
        {
            if (P.Count > 0)
                P.RemoveAt(P.Count - 1);
        }
        public int NumPoints { get { return P.Count; } }
        public double X(int i) { return P[i].X; }
        public double Y(int i) { return P[i].Y; }
        public double Perimeter()
        {
            int n = P.Count;
            if (n == 0)
                return 0.0;
            double s = Point.Dist(P[n - 1], P[0]);
            for (int i = 1; i < n; i++) 
                s+= Point.Dist(P[i - 1], P[i]);
            return s;
        }
        public double Area()
        {
            int n = P.Count;
            if (n < 3)
                return 0.0;

            double a = 
                P[0].X * (P[1].Y - P[n - 1].Y) +
                P[n-1].X * (P[0].Y - P[n - 2].Y);
            for (int i = 1; i < n-1; i++)
                a += P[i].X * (P[i+1].Y - P[i-1].Y);

            return 0.5 * Math.Abs(a);
        }
    }

    public class Point 
    {
        private double x;
        private double y;
        public double X { get { return x; } }
        public double Y { get { return y; } }
        public Point(double x0, double y0) { x = x0; y = y0; }
        public static double Dist(Point A, Point B)
        {
            return Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y));
        }
    }
}
