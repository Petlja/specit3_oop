using System;
using System.Collections.Generic;
using System.Text;

public class Zmijica
{
    // lista polja koja zauzima zmija
    private List<Tuple<int, int>> polja;

    // broj polja za koji zmija još treba da poraste
    private int preostaliRast;

    // trenutna dužina zmije
    public int Duzina { get { return polja.Count; } }

    // konstruktor - zmija je opisana svojim početnim poljem
    public Zmijica(int x, int y)
    {
        polja = new List<Tuple<int, int>>();
        polja.Add(new Tuple<int, int>(x, y));
        preostaliRast = 0;
    }

    // pomeranje glave zmije u datom smeru (i tela za njom)
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
    
    // javni metodi za upravljanje zmijom
    public void Gore() { Pomak(0, 1); }
    public void Dole() { Pomak(0, -1); }
    public void Levo() { Pomak(-1, 0); }
    public void Desno() { Pomak(1, 0); }

    // zahtevanje budućeg rasta zmije za n polja
    public void Rasti(int n) { preostaliRast += n; }

    // položaj i-tog polja zmije
    public Tuple<int, int> this[int i]
    {
        get { return polja[i]; }
    }

    // tekstualni prikaz zmije
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
            // nova zmijica polazi sa polja (3, 3)
            Zmijica z = new Zmijica(3, 3);
            Console.WriteLine(z);
            
            // isprobavamo kretanje zmije
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);

            // isprobavamo produžavanje zmije
            // u kombinaciji sa kretanjem
            z.Rasti(3);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Desno(); Console.WriteLine(z);

            // isporbavamo ispis pojedinih članaka pomoću indeksera
            Console.WriteLine("Upotreba indeksera");
            for (int i = 0; i < z.Duzina; i++)
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
