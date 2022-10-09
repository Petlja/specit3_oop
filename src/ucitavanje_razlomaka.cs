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
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Unesite prvi razlomak: ");
        Razlomak r1 = Razlomak.Parse(Console.ReadLine());
        Console.Write("Unesite drugi razlomak: ");
        Razlomak r2 = Razlomak.Parse(Console.ReadLine());

        if (r1.Equals(r2))
            Console.WriteLine("Prvi razlomak je jednak drugom.");
        else
            Console.WriteLine("Prvi razlomak nije jednak drugom.");

        int cmp = r1.CompareTo(r2);
        if (cmp < 0)
            Console.WriteLine("Prvi razlomak je manji od drugog.");
        else if (cmp == 0)
            Console.WriteLine("Razlomci su jednaki.");
        else
            Console.WriteLine("Prvi razlomak je veci od drugog.");
    }
}
