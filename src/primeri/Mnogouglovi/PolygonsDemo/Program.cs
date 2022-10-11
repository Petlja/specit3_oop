using System;
using Polygons;

namespace PolygonsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Polygon p = new Polygon();
            p.AddPoint(1, 1);
            p.AddPoint(1, 3);
            p.AddPoint(4, 3);
            p.AddPoint(4, 1);
            p.AddPoint(5, 2);
            p.RemoveLastPoint();

            Console.Write("Tacke su:");
            for (int i = 0; i < p.NumPoints; i++)
            {
                Console.Write(" ({0}, {1})", p.X(i), p.Y(i));
            }
            Console.WriteLine();

            Console.WriteLine("Obim je {0}", p.Perimeter());
            Console.WriteLine("Povrsina je {0}", p.Area());
        }
    }
}
