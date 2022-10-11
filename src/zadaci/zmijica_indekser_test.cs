class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            Zmijica z = new Zmijica(3, 3);
            Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);

            z.Rasti(3);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Desno(); Console.WriteLine(z);

            Console.WriteLine("Upotreba indeksera");
            for (int i = 0; i < z.Count; i++)
            {
                Console.WriteLine("Clanak {0}: x={1}, y={2}",
                    i, z[i].Item1, z[i].Item2);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
