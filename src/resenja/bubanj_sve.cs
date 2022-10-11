using System;
using System.Collections;
using System.Collections.Generic;

public class Bubanj
{
    private int brKuglica;
    private int[] kuglice;
    private Random r;
    public Bubanj(int n) 
    {
        r = new Random();
        brKuglica = n;
        kuglice = new int[n];
        for (int i = 0; i < n; i++)
            kuglice[i] = i + 1;

        for (int i = n; i > 1; i--)
        {
            int k = r.Next(i);
            int t = kuglice[k];
            kuglice[k] = kuglice[i-1];
            kuglice[i - 1] = t;
        }
    }
    public bool Prazan() { return brKuglica == 0; }
    public int BrojKuglica { get { return brKuglica; } }
    public int Izvuci() 
    {
        int m = r.Next(brKuglica);
        int rez = kuglice[m];
        kuglice[m] = kuglice[brKuglica - 1];
        brKuglica--;
        return rez;
    }
    public int[] Izvuci(int k)
    {
        int[] rez = new int[k];
        for (int i = 0; i < k; i++)
        {
            int m = r.Next(brKuglica);
            rez[i] = kuglice[m];
            kuglice[m] = kuglice[brKuglica - 1];
            brKuglica--;
        }
        return rez;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Bubanj b = new Bubanj(5);
        Console.WriteLine("Izvucen je broj {0}", b.Izvuci());
        Console.WriteLine(b.Prazan());
        Console.WriteLine("Broj preostalih kuglica: {0}", b.BrojKuglica);
        var serija = b.Izvuci(3);
        Console.Write("Izvuceni su brojevi");
        foreach (int br in serija)
            Console.Write(" {0}", br);
        Console.WriteLine();
        Console.WriteLine("Izvucen je broj {0}", b.Izvuci());
        Console.WriteLine(b.Prazan());
        Console.WriteLine("Broj preostalih kuglica: {0}", b.BrojKuglica);
    }
}
