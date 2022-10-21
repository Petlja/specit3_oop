// ./_zadaci/02 Zbirka_2/100 strukture_podataka/04 apstraktne_strukture/01 mapa_sa_uvecanjem
using System;
using System.Collections.Generic;

public class IntToIntMap
{
    private int offset;
    private Dictionary<int, int> dict;
    public IntToIntMap()
    {
        offset = 0;
        dict = new Dictionary<int, int>();
    }

    public int this[int key]
    {
        get
        {
            int val = 0;
            if (dict.TryGetValue(key, out val))
                return val + offset;
            else
                return offset;
        }
        set
        {
            dict[key] = value - offset;
        }
    }
    public bool Erase(int key)
    {
        // ?
        return dict.Remove(key);
    }
    public void Increment(int key, int d)
    {
        int val = 0;
        if (dict.TryGetValue(key, out val))
            dict[key] = val + d;
        else
            dict[key] = d;
    }
    public void IncrementAll(int d)
    {
        offset += d;
    }
}

internal class Program
{
    static void display(IntToIntMap m)
    {
        for (int i = 1; i <= 4; i++)
            Console.Write("m[{0}] = {1,2}    ", i, m[i]);
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        // proba
        try
        {
            IntToIntMap m = new IntToIntMap();
            Console.Write("prazno: "); display(m);
            Console.Write("Svi +2: "); m.IncrementAll(2); display(m);
            Console.Write("[3] +4: "); m.Increment(3, 4); display(m);
            Console.Write("Svi +7: "); m.IncrementAll(7); display(m);
            Console.Write("[2] +1: "); m.Increment(2, 1); display(m);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
