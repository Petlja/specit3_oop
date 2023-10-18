using System;
using System.Collections.Generic;
using System.Text;

public class Zmijica
{
    // lista polja koja zauzima zmija
    private List<Tuple<int, int>> polja;

    // interni položaj glave unutar liste polja zmije
    private int indeksGlave;

    // broj polja za koji zmija još treba da poraste
    private int preostaliRast;

    // trenutna dužina zmije
    public int Duzina { get { return polja.Count; } }
    
    // konstruktor - zmija je opisana svojim početnim poljem
    public Zmijica(int x, int y)
    {
        polja = new List<Tuple<int, int>>();
        polja.Add(new Tuple<int, int>(x, y));
        indeksGlave = 0; // glava je trenutno nulti element liste
        preostaliRast = 0;
    }

    // pomeranje glave zmije u datom smeru (i tela za njom)
    private void Pomak(int dx, int dy)
    {
        // sledeći položaj glave
        int gx = polja[indeksGlave].Item1 + dx;
        int gy = polja[indeksGlave].Item2 + dy;

        if (preostaliRast > 0)
        {
            polja.Insert(indeksGlave, new Tuple<int, int>(gx, gy));
            preostaliRast--;
        }
        else
        {
            // ako se zmija ne produžava, glavu upisujemo na mesto repa
            int n = polja.Count;
            int indeksRepa = (indeksGlave + n - 1) % n;

            polja[indeksRepa] = new Tuple<int, int>(gx, gy);
            indeksGlave = indeksRepa;
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
        get { return polja[(i + indeksGlave) % Duzina]; }
    }

    // tekstualni prikaz zmije
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
