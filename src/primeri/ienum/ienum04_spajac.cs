// primer ilustruje motod koji izvršava algoritam Merge nad dve date 
// kolekcije, a da pri tome ne zna njihovu stvarnu prirodu i ne zauzima
// prostor sa smeštanje svih elemenata (ako prostor nije već zauzet na 
// mestu poziva)

using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> Spajac(IEnumerable<int> a, IEnumerable<int> b)
    {
        IEnumerator<int> ea = a.GetEnumerator();
        IEnumerator<int> eb = b.GetEnumerator();
        bool imaA = ea.MoveNext(), imaB = eb.MoveNext();
        
        // dok u bar jednoj kolekcijji ima još elemenata
        while (imaA || imaB)
        {
            // vrati manji od dva tekuća elementa, 
            // odnosno element iz kolekcije u kojoj ih još ima
            if (!imaB || (imaA && ea.Current < eb.Current))
            {
                yield return ea.Current;
                imaA = ea.MoveNext();
            }
            else
            {
                yield return eb.Current;
                imaB = eb.MoveNext();
            }
        }
    }

    static void Main()
    {
        var e = Spajac(
                new List<int> { 1, 3, 7 },
                new List<int> { 2, 4, 5, 6 });

        foreach (int x in e)
            Console.Write(x + " ");
        Console.WriteLine();
    }
}