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

        // obezbeđujemo uslov da je imenilac pozitivan
        if (q < 0)
        {
            p = -p;
            q = -q;
        }

        // obezbeđujemo uslov da je razlomak neskrativ
        a = p;
        b = q;
        Skrati(ref a, ref b);
    }

    public Razlomak(int n)
    {
        a = n; b = 1;
    }

    // Metod NZD izračunava najveći zajednički delilac dva neoznačena broja,
    // koristeći Euklidov algoritam 
    private static int NZD(int a, int b)
    {
        // nametnut preduslov (pretpostavka): a >= 0 i b >= 0
        while (b > 0) { int r = a % b; a = b; b = r; }
        return a;
    }

    // Metod Skrati deli oba svoja argumenta njihovim najvećim zajedničkim deliocem
    private static void Skrati(ref int x, ref int y)
    {
        // nametnut preduslov (pretpostavka): x i y nisu oba nule
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
}

class Program
{
    static void Main(string[] args)
    {
        // Isprobavanje metoda Equals i CompareTo
        Razlomak r1 = new Razlomak(2, 3);
        Razlomak r2 = new Razlomak(4, 6);
        Razlomak r3 = new Razlomak(4, 3);
        Razlomak r4 = new Razlomak(1);

        if (r1.Equals(r2))
            Console.WriteLine("Razlomci r1 i r2 su jednaki.");
        else
            Console.WriteLine("Razlomci r1 i r2 nisu jednaki.");

        if (r2.CompareTo(r3) < 0)
            Console.WriteLine("Razlomak r2 je manji od r3.");
        else
            Console.WriteLine("Razlomak r2 nije manji od r3.");

        if (r3.CompareTo(r4) < 0)
            Console.WriteLine("Razlomak r3 je manji od r4.");
        else
            Console.WriteLine("Razlomak r3 nije manji od r4.");
    }
}

