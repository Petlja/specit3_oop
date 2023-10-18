class Program
{
    static void Main(string[] args)
    {
        // proba
        try
        {
            // nova zmijica polazi sa polja (3, 3)
            Zmijica z = new Zmijica(3, 3);
            Console.WriteLine(z);
            
            // isprobavamo kretanje zmije
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);

            // isprobavamo produžavanje zmije
            // u kombinaciji sa kretanjem
            z.Rasti(3);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Levo(); Console.WriteLine(z);
            z.Gore(); Console.WriteLine(z);
            z.Desno(); Console.WriteLine(z);

            // isporbavamo ispis pojedinih članaka pomoću indeksera
            Console.WriteLine("Upotreba indeksera");
            for (int i = 0; i < z.Duzina; i++)
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
