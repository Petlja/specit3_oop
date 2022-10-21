using System;
using System.Collections.Generic;

public class DictionaryPlus<TK>
{
    private double offset;
    private Dictionary<TK, double> dict;
    public DictionaryPlus()
    {
        offset = 0.0;
        dict = new Dictionary<TK, double>();
    }

    public double this[TK key]
    {
        get
        {
            double val = 0.0;
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
    public bool Erase(TK key)
    {
        // ?
        return dict.Remove(key);
    }
    public void Increment(TK key, int d)
    {
        double val = 0;
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
    static void display(DictionaryPlus<char> m)
    {
        for (char c = 'A'; c <= 'D'; c++)
            Console.Write("m[{0}] = {1,2}    ", c, m[c]);
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        // proba
        try
        {
            DictionaryPlus<char> m = new DictionaryPlus<char>();
            Console.Write("prazno: "); display(m);
            Console.Write("Svi +2: "); m.IncrementAll(2); display(m);
            Console.Write("[3] +4: "); m.Increment('C', 4); display(m);
            Console.Write("Svi +7: "); m.IncrementAll(7); display(m);
            Console.Write("[2] +1: "); m.Increment('B', 1); display(m);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
