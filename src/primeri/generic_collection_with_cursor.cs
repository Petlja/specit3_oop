using System;
using System.Collections.Generic;

public class CollectionWithCursor<T>
{
    private List<T> left = new List<T>(); // pre kursora
    private List<T> right = new List<T>(); // posle kursora

    private T PopLeft() // izvadi i vrati element levo od kursora
    {
        T x = left[left.Count - 1];
        left.RemoveAt(left.Count - 1);
        return x;
    }
    private T PopRight() // izvadi i vrati element desno od kursora
    {
        T x = right[right.Count - 1];
        right.RemoveAt(right.Count - 1);
        return x;
    }
    // ukupan broj elemenata u kolekciji
    public int Count { get { return left.Count + right.Count; } }

    // pozicija kursora je broj elemenata levo od njega
    public int CursorPosition { get { return left.Count; } }

    // indekser vraca element sa date pozicije, 
    // brojano onako kako kolekciju vidi korisnik 
    public T this[int i]
    {
        get
        {
            if (i < left.Count) return left[i];
            else return right[Count - 1 - i];
        }
    }
    
    // umetanje elementa na poziciji kursora 
    // (kursor ostaje desno od novog elementa)
    public void Insert(T x) { left.Add(x); }
    
    // brisanje elementa desno od kursora
    public void Delete() { PopRight(); }
    
    // brisanje elementa levo od kursora
    public void Backspace() { PopLeft(); }
    
    // pomeranje kursora za jedno mesto levo
    public void GoLeft() { right.Add(PopLeft()); }
    
    // pomeranje kursora za jedno mesto desno
    public void GoRight() { left.Add(PopRight()); }
}

class Program
{
    static void Display(CollectionWithCursor<char> a)
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
        CollectionWithCursor<char> a = new CollectionWithCursor<char>();
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
