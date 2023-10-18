using System;
using System.Collections.Generic;

public class CustomStack<T>
{
    // Stek interno predstavljamo pomoću liste 'a', 
    // tj. u listi čuvamo elemente koje korisnik stavi u stek
    private List<T> a;

    // podržane operacije sa stekom:
    public CustomStack() { a = new List<T>(); } // kreiranje praznog steka

    public void Push(T x) { a.Add(x); } // dodavanje elementa na vrh steka

    public bool Empty() { return a.Count == 0; } // provera da li je stek prazan

    public T Peek() { return a[a.Count - 1]; } // čitanje vrednosti sa vrha steka (bez uzimanja)

    public T Pop() // uzimanje elementa sa vrha steka
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
        // stavljanje nekih elemenata na stek
        CustomStack<char> s = new CustomStack<char>();
        foreach (char c in "abcd")
            s.Push(c);

        // uzimanje sa steka
        while (!s.Empty())
            Console.WriteLine(s.Pop());
    }
}
