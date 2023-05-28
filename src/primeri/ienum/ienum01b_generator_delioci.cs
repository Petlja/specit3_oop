using System;
using System.Collections;

class Program
{
    static IEnumerable ProstiDelioci(int n, int d)
    {
        for (int i = d; i * i <= n; i++)
        {
            if (n % i == 0) 
            {
                yield return i;
                foreach (var x in ProstiDelioci(n / i, i))
                    yield return x;
                yield break;
            }
        }
        yield return n;
    }
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        foreach (var d in ProstiDelioci(n, 2)) 
            Console.WriteLine(d);
    }
}
