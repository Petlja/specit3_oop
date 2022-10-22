using System;

internal class Program
{
    static void Swap<T>(ref T a, ref T b)
    {
        T t = a;
        a = b; 
        b = t;
    }
    static void Main(string[] args)
    {
        int n1 = 1, n2 = 2;
        Swap(ref n1, ref n2);
        Console.WriteLine("{0}, {1}", n1, n2);

        string s1 = "jedan", s2 = "dva";
        Swap(ref s1, ref s2);
        Console.WriteLine("{0}, {1}", s1, s2);
    }
}
