using System;
using System.Collections.Generic;

public class FreqStack<T>
{
    private Dictionary<T, int> frekv = new Dictionary<T, int>();
    private List<Stack<T>> stekovi = new List<Stack<T>>();
    private int maksFrekv = 0;

    public bool Empty() 
    { 
        return maksFrekv == 0; 
    }
    public void Clear() 
    {
        stekovi.Clear(); 
        frekv.Clear(); 
        maksFrekv = 0; 
    }
    public void Push(T x)
    {
        int f = 0;
        frekv.TryGetValue(x, out f);
        frekv[x] = f + 1;
        if (f == stekovi.Count)
            stekovi.Add(new Stack<T>());

        stekovi[f].Push(x);
        maksFrekv = Math.Max(maksFrekv, f + 1);
    }
    public T Pop()
    {
        T x = stekovi[maksFrekv - 1].Pop();
        frekv[x]--;
        if (stekovi[maksFrekv - 1].Count == 0)
            maksFrekv--;
        return x;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            string[] imena = new string[] { "Pera", "Mika", "Mika", "Pera", "Laza", "Mika" };
            FreqStack<string> s = new FreqStack<string>();
            foreach (string ime in imena)
                s.Push(ime);

            while (!s.Empty())
                Console.WriteLine(s.Pop());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
