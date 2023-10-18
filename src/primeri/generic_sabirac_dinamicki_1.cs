using System;

public class DinamickiSabirac<T>
{
    private int n; // dužina niza
    private T[] a; // elementi niza

    // konstruktor - niz je opisan svojom dužinom
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

    // metod koji vraća zbir traženih elemenata 
    // od - pozicija od koje se sabira
    // koliko - broj elemenata koji se sabiraju
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
        // kreiranje objekta koji računa celobrojne zbirove
        int n = 5;
        DinamickiSabirac<int> dsi = new DinamickiSabirac<int>(n);

        // upis nekih vrednosti
        for (int k = 0; k < n; k++)
            dsi[k] = k + 1;
        
        // isprobavanje metoda Zbir za cele brojeve
        Console.WriteLine(dsi.Zbir(0, 5)); // 1+2+3+4+5 = 15
        Console.WriteLine(dsi.Zbir(1, 3)); // 2+3+4 = 9
        Console.WriteLine(dsi.Zbir(2, 2)); // 3+4 = 7

        
        // kreiranje objekta koji računa realne zbirove
        DinamickiSabirac<double> dsd = new DinamickiSabirac<double>(n);
        
        // upis nekih vrednosti
        for (int k = 0; k < n; k++)
            dsd[k] = k + 0.5;
        
        // isprobavanje metoda Zbir za realne brojeve
        Console.WriteLine(dsd.Zbir(0, 5)); // 1+2+3+4+5 = 15
        Console.WriteLine(dsd.Zbir(1, 3)); // 2+3+4 = 9
        Console.WriteLine(dsd.Zbir(2, 2)); // 3+4 = 7
    }
}
