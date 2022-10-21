using System;
using System.Collections.Generic;

public class StackWithMax<T> where T : IComparable
{
    Stack<T> stek = new Stack<T>();
    Stack<T> maksimumi = new Stack<T>();

    public void Clear() { stek.Clear(); maksimumi.Clear(); }
    public bool Empty() { return stek.Count == 0; }
    public void Push(T x)
    {
        stek.Push(x);
        if (maksimumi.Count == 0 || x.CompareTo(maksimumi.Peek()) >= 0)
            maksimumi.Push(x);
    }
    public T Pop()
    {
        T x = stek.Pop();
        if (x.CompareTo(maksimumi.Peek()) == 0)
            maksimumi.Pop();
        return x;
    }
    public T GetMax()
    {
        return maksimumi.Peek();
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            StackWithMax<string> s = new StackWithMax<string>();
            s.Push("Alfa"); s.Push("Gama"); s.Push("Beta");
            Console.WriteLine(s.GetMax());
            s.Pop();
            s.Push("Omega");
            Console.WriteLine(s.GetMax());
            s.Pop(); s.Pop();
            Console.WriteLine(s.GetMax());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
