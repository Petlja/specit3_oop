using System;
using System.Collections.Generic;
using System.Text;

public class Zmijica
{
    private List<Tuple<int, int>> polja;
    private int indeksGlave;
    private int preostaliRast;

    public int Count { get { return polja.Count; } }
    public Zmijica(int x, int y)
    {
        polja = new List<Tuple<int, int>>();
        polja.Add(new Tuple<int, int>(x, y));
        indeksGlave = 0;
        preostaliRast = 0;
    }

    private void Pomak(int dx, int dy)
    {
        int gx = polja[indeksGlave].Item1 + dx;
        int gy = polja[indeksGlave].Item2 + dy;

        if (preostaliRast > 0)
        {
            polja.Insert(indeksGlave, new Tuple<int, int>(gx, gy));
            preostaliRast--;
        }
        else
        {
            int n = polja.Count;
            int indeksRepa = (indeksGlave + n - 1) % n;

            polja[indeksRepa] = new Tuple<int, int>(gx, gy);
            indeksGlave = indeksRepa;
        }
    }
    public void Gore() { Pomak(0, 1); }
    public void Dole() { Pomak(0, -1); }
    public void Levo() { Pomak(-1, 0); }
    public void Desno() { Pomak(1, 0); }

    public void Rasti(int n) { preostaliRast += n; }

    public Tuple<int, int> this[int i]
    {
        get { return polja[(i + indeksGlave) % Count]; }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        int n = polja.Count;
        int g = indeksGlave;
        for (int i = 0; i < n; i++)
        {
            string clanak = string.Format("({0}, {1})", polja[g].Item1, polja[g].Item2);
            sb.Append(clanak);
            g = (g + 1) % n;
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
