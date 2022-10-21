using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Monomial
{
    private static Dictionary<char, int> ndx = new Dictionary<char, int>();
    private static List<char> vars = new List<char>();
    private List<uint> exp;
    private double coef;
    private Monomial() { }
    public Monomial(string s, double c)
    {
        coef = c;
        exp = new List<uint>();
        foreach (char x in s)
        {
            int n = 0;
            if (!ndx.TryGetValue(x, out n))
            {
                n = ndx.Count;
                ndx[x] = n;
                vars.Add(x);
            }
            while (exp.Count <= n)
            {
                exp.Add(0);
            }
            exp[n]++;
        }
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(coef.ToString("0.0000"));
        sb.Append("*");
        for (int i = 0; i < exp.Count; i++)
        {
            sb.Append(string.Format("{0}^{1}", vars[i], exp[i]));
        }
        return sb.ToString();
    }
    public static Monomial operator +(Monomial a, Monomial b)
    {
        if (a.exp.SequenceEqual(b.exp))
        {
            Monomial c = new Monomial();
            c.exp = new List<uint>(a.exp);
            c.coef = a.coef + b.coef;
            return c;
        }
        return null;
    }
    public static Monomial operator -(Monomial a, Monomial b)
    {
        if (a.exp.Equals(b.exp))
        {
            Monomial c = new Monomial();
            c.exp = new List<uint>(a.exp);
            c.coef = a.coef - b.coef;
            return c;
        }
        return null;
    }
    public static Monomial operator *(Monomial a, Monomial b)
    {
        Monomial c = new Monomial();
        c.exp = new List<uint>();
        if (a.exp.Count > b.exp.Count)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
        for (int i = 0; i < a.exp.Count; i++)
            c.exp.Add(a.exp[i] + b.exp[i]);

        for (int i = a.exp.Count; i < b.exp.Count; i++)
            c.exp.Add(b.exp[i]);

        c.coef = a.coef * b.coef;
        return c;
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            Monomial m1 = new Monomial("xxy", 4);
            Console.WriteLine(m1);
            Monomial m2 = new Monomial("xxy", 3);
            Monomial m3 = m1 + m2;
            Console.WriteLine(m3);
            Monomial m4 = new Monomial("xy", 5);
            Monomial m5 = m1 + m4;
            Console.WriteLine(m5);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
