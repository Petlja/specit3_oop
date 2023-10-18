using System;
using System.Collections;
using System.Collections.Generic;

// klasa koja predstavlja redak niz
public class SparseArray
{
    // vrednosti elemenata interno pamtimo u rečniku
    private Dictionary<ulong, double> a = new Dictionary<ulong, double>();
    
    // vrednost i-tog elementa se simulira pomoću indeksera
    public double this[ulong i]
    {
        get
        {
            if (a.ContainsKey(i))
                return a[i];
            else
                return 0;
        }
        set { a[i] = value; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        SparseArray x = new SparseArray();
        ulong n = 4000000000000;
        x[n]++; // poziva oba pristupnika (i get i set)
        x[n + 1] = 3; // pristupnik set
        Console.WriteLine(x[n]); // pristupnik get
        Console.WriteLine(x[n + 1]); // pristupnik get
    }
}
