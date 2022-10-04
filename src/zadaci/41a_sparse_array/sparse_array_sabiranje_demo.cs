class Program
{
    static void Main(string[] args)
    {
        SparseArray x = new SparseArray(); x[2] = 3; x[3] = 7;
        SparseArray y = new SparseArray(); y[2] = 1; y[4] = 9;
        SparseArray z = 2 + x + y + 3;
        Console.WriteLine(z[1]);
        Console.WriteLine(z[2]);
        Console.WriteLine(z[3]);
        Console.WriteLine(z[4]);
    }
}
