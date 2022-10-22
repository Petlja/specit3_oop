using System;
using System.Collections.Generic;

public class QueueWNoDup<T>
{
    Queue<T> q = new Queue<T>();
    HashSet<T> s = new HashSet<T>();

    public bool Empty() { return q.Count == 0; }
    public bool Push(T x)
    {
        if (s.Contains(x))
            return false;
        else
        {
            q.Enqueue(x);
            s.Add(x);
            return true;
        }
    }
    public T Pop()
    {
        T x = q.Dequeue();
        s.Remove(x);
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
            QueueWNoDup<char> s = new QueueWNoDup<char>();
            s.Push('A'); s.Push('B'); s.Push('A'); s.Push('C');
            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());
            s.Push('D');
            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
