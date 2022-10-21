using System;
using System.Collections.Generic;

public class DictionaryPlus<TK, TV>
{
    private TV offset;
    private Dictionary<TK, TV> dict;
    public DictionaryPlus()
    {
        offset = (dynamic)0.0;
        dict = new Dictionary<TK, TV>();
    }

    public TV this[TK key]
    {
        get
        {
            TV val = (dynamic)0.0;
            if (dict.TryGetValue(key, out val))
                return (dynamic)val + (dynamic)offset;
            else
                return offset;
        }
        set
        {
            dict[key] = (dynamic)value - (dynamic)offset;
        }
    }
    public bool Erase(TK key)
    {
        return dict.Remove(key);
    }
    public void Increment(TK key, TV d)
    {
        TV val = (dynamic)0;
        if (dict.TryGetValue(key, out val))
            dict[key] = (dynamic)val + (dynamic)d;
        else
            dict[key] = d;
    }
    public void IncrementAll(TV d)
    {
        offset += (dynamic)d;
    }
}

internal class Program
{
    static void display(DictionaryPlus<char, double> m)
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
            DictionaryPlus<char, double> m = new DictionaryPlus<char, double>();
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
