using System;

public class Razlomak
{
    // razlomak je oblika a / b 
    private int a, b; // nametnut uslov: a i b  su uzajamno prosti, b > 0

    public Razlomak(int p, int q)
    {
        if (q == 0)
        {
            throw new Exception("Imenilac razlomka je 0");
        }

        if (q < 0)
        {
            p = -p;
            q = -q;
        }

        a = p;
        b = q;
        Skrati(ref a, ref b);
    }

    public Razlomak(int n)
    {
        a = n; b = 1;
    }

    private static int NZD(int a, int b)
    {
        // nametnut preduslov: a >= 0 i b >= 0
        while (b > 0) { int r = a % b; a = b; b = r; }
        return a;
    }

    private static void Skrati(ref int x, ref int y)
    {
        // preduslov: x i y nisu oba nule
        int d = NZD(Math.Abs(x), Math.Abs(y));
        x /= d;
        y /= d;
    }

    public bool Equals(Razlomak r)
    {
        return a == r.a && b == r.b;
    }

    public int CompareTo(Razlomak r)
    {
        return a * r.b - r.a * b;
    }
    public static Razlomak Parse(string s)
    {
        if (String.IsNullOrEmpty(s))
        {
            throw new Exception("Nije dobar zapis razlomka");
        }
        int k = s.IndexOf('/');
        if (k == -1) { return new Razlomak(int.Parse(s)); }
        return new Razlomak(
            int.Parse(s.Substring(0, k)),
            int.Parse(s.Substring(k + 1)));
    }

    public static Razlomak operator +(Razlomak r, Razlomak s)
    {
        int d = NZD(r.b, s.b);
        int nzs = (r.b / d) * s.b;
        return new Razlomak(r.a * (s.b / d) + s.a * (r.b / d), nzs);
    }

    public static Razlomak operator -(Razlomak r)
    {
        return new Razlomak(-r.a, r.b);
    }

    public static Razlomak operator -(Razlomak r, Razlomak s)
    {
        int d = NZD(r.b, s.b);
        int nzs = (r.b / d) * s.b;
        return new Razlomak(r.a * (s.b / d) - s.a * (r.b / d), nzs);
    }

    public static Razlomak operator *(Razlomak r, Razlomak s)
    {
        int ra = r.a, rb = r.b, sa = s.a, sb = s.b;
        Skrati(ref ra, ref sb);
        Skrati(ref sa, ref rb);
        return new Razlomak(ra * sa, rb * sb);
    }

    public static Razlomak operator /(Razlomak r, Razlomak s)
    {
        int ra = r.a, rb = r.b, sa = s.a, sb = s.b;
        Skrati(ref ra, ref sa);
        Skrati(ref rb, ref sb);
        return new Razlomak(ra * sb, rb * sa);
    }

    public static implicit operator Razlomak(int n)
    {
        return new Razlomak(n);
    }
    public override string ToString()
    {
        if (a == 0) { return "0"; }
        if (b == 1) { return a.ToString(); }
        return a.ToString() + "/" + b.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Program resava jednacinu oblika ax+b=cx+d");
        Console.Write("Unesite razlomak a: ");
        Razlomak a = Razlomak.Parse(Console.ReadLine());
        Console.Write("Unesite razlomak b: ");
        Razlomak b = Razlomak.Parse(Console.ReadLine());
        Console.Write("Unesite razlomak c: ");
        Razlomak c = Razlomak.Parse(Console.ReadLine());
        Console.Write("Unesite razlomak d: ");
        Razlomak d = Razlomak.Parse(Console.ReadLine());
        // ax+b=cx+d --> x=(d-b)/(a-c)
        Console.WriteLine("Resenje je {0}", (d - b) / (a - c));
    }
}
