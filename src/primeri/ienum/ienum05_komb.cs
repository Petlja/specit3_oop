// primer ilustruje rekurzivan metod koji generiše kolekciju (tj. vraća kolekciju 
// pomoću naredbe yield return, ne zauzimajući prostor za celu kolekciju, već samo za 
// po jedan element
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static IEnumerable<int> Kombinacije(int prefiks, int josCifara, int maxCifra)
    {
        if (josCifara == 0)
            yield return prefiks;
        else
        {
            // sledeća cifra moda da bude veća od prethodne, 
            // a manja ili jednaka sa 'maxCifra'
            for (int i = prefiks % 10 + 1; i <= maxCifra; i++)
            {
                foreach (var y in Kombinacije(10 * prefiks + i, josCifara - 1, maxCifra))
                    yield return y;
            }
        }
    }
    static void Main()
    {
        foreach (int br in Kombinacije(0, 3, 5))
            Console.Write(br + " ");
        Console.WriteLine();
    }
}
