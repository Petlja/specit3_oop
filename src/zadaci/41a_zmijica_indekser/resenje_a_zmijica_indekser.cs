using System;
using System.Collections.Generic;
using System.Text;

public class Zmijica
{
    private List<Tuple<int, int>> polja;
    private int preostaliRast;

    public int Count { get { return polja.Count; } }
    public Zmijica(int x, int y)
    {
        polja = new List<Tuple<int, int>>();
        polja.Add(new Tuple<int, int>(x, y));
        preostaliRast = 0;
    }

    private void Pomak(int dx, int dy)
    {
        int gx = polja[0].Item1 + dx;
        int gy = polja[0].Item2 + dy;

        polja.Insert(0, new Tuple<int, int>(gx, gy));
        if (preostaliRast > 0)
        {
            preostaliRast--;
        }
        else
        {
            polja.RemoveAt(polja.Count - 1);
        }
    }
    public void Gore() { Pomak(0, 1); }
    public void Dole() { Pomak(0, -1); }
    public void Levo() { Pomak(-1, 0); }
    public void Desno() { Pomak(1, 0); }

    public void Rasti(int n) { preostaliRast += n; }

    public Tuple<int, int> this[int i]
    {
        get { return polja[i]; }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        int n = polja.Count;
        for (int i = 0; i < n; i++)
        {
            string clanak = string.Format("({0}, {1})", polja[i].Item1, polja[i].Item2);
            sb.Append(clanak);
        }
        sb.Append("]");
        return sb.ToString();
    }
}
class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            Zmijica z = new Zmijica(3, 3);
            Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);

            z.Rasti(3);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Desno(); Console.WriteLine(z);

            Console.WriteLine("Upotreba indeksera");
            for (int i = 0; i < z.Count; i++)
            {
                Console.WriteLine("Clanak {0}: x={1}, y={2}",
                    i, z[i].Item1, z[i].Item2);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
