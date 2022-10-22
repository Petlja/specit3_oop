using System;
using System.Collections.Generic;

public class CustomStack<T>
{
    private List<T> a;
    public CustomStack() { a = new List<T>(); }
    public void Push(T x) { a.Add(x); }
    public bool Empty() { return a.Count == 0; }
    public T Peek() { return a[a.Count - 1]; }
    public T Pop()
    {
        T x = a[a.Count - 1];
        a.RemoveAt(a.Count - 1);
        return x;
    }
}

class Program
{
    static void Main(string[] args)
    {
        CustomStack<char> s = new CustomStack<char>();
        foreach (char c in "abcd")
            s.Push(c);

        while (!s.Empty())
            Console.WriteLine(s.Pop());
    }
}
