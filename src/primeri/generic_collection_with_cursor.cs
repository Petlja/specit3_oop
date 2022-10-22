using System;
using System.Collections.Generic;

public class CollectionWihCursor<T>
{
    private List<T> left = new List<T>();
    private List<T> right = new List<T>();

    private T PopLeft()
    {
        T x = left[left.Count - 1];
        left.RemoveAt(left.Count - 1);
        return x;
    }
    private T PopRight()
    {
        T x = right[right.Count - 1];
        right.RemoveAt(right.Count - 1);
        return x;
    }
    public int Count { get { return left.Count + right.Count; } }
    public int CursorPosition { get { return left.Count; } }
    public T this[int i]
    {
        get
        {
            if (i < left.Count) return left[i];
            else return right[Count - 1 - i];
        }
    }
    public void Insert(T x) { left.Add(x); }
    public void Delete() { PopRight(); }
    public void Backspace() { PopLeft(); }
    public void GoLeft() { right.Add(PopLeft()); }
    public void GoRight() { left.Add(PopRight()); }
}

class Program
{
    static void Display(CollectionWihCursor<char> a)
    {
        Console.Write(
            "br. elemenata: {0}, pozicija kursora: {1}, elementi:",
            a.Count, a.CursorPosition);
        for (int i = 0; i < a.Count; i++)
            Console.Write(" " + a[i]);
        Console.WriteLine(".");
    }
    static void Main(string[] args)
    {
        CollectionWihCursor<char> a = new CollectionWihCursor<char>();
        foreach (char c in "abcdef")
            a.Insert(c);

        a.GoLeft(); a.GoLeft();
        Display(a);

        a.Backspace(); a.Delete(); a.Insert('i');
        Display(a);

        a.Insert('k');
        Display(a);
    }
}
