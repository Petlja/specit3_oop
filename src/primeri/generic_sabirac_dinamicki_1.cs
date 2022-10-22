using System;

public class DinamickiSabirac<T>
{
    private int n;
    private T[] a;

    public DinamickiSabirac(int duzina)
    {
        n = duzina;
        a = new T[n];
    }

    public T this[int i]
    {
        get { return a[i]; }
        set { a[i] = value; }
    }

    public T Zbir(int od, int koliko)
    {
        T zbir = (dynamic)0;
        for (int i = od; i < od + koliko; i++)
            zbir += (dynamic)a[i];
        return zbir;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int n = 5;
        DinamickiSabirac<int> dsi = new DinamickiSabirac<int>(n);
        for (int k = 0; k < n; k++)
            dsi[k] = k + 1;
        Console.WriteLine(dsi.Zbir(0, 5)); // 1+2+3+4+5 = 15
        Console.WriteLine(dsi.Zbir(1, 3)); // 2+3+4 = 9
        Console.WriteLine(dsi.Zbir(2, 2)); // 3+4 = 7    }

        DinamickiSabirac<double> dsd = new DinamickiSabirac<double>(n);
        for (int k = 0; k < n; k++)
            dsd[k] = k + 0.5;
        Console.WriteLine(dsd.Zbir(0, 5)); // 1+2+3+4+5 = 15
        Console.WriteLine(dsd.Zbir(1, 3)); // 2+3+4 = 9
        Console.WriteLine(dsd.Zbir(2, 2)); // 3+4 = 7    }
    }
}
