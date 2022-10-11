using System;

class Sabirac
{
    private int[] zbirPre;

    public Sabirac(int[] a)
    {
        if (a == null)
            throw new Exception("Ne postoji niz nad kojim se formira sabirac");

        int n = a.Length;
        zbirPre = new int[n + 1];
        zbirPre[0] = 0;
        for (int i = 0; i < n; i++)
            zbirPre[i + 1] = zbirPre[i] + a[i];
    }

    public int Saberi(int start, int br)
    {
        return zbirPre[start + br] - zbirPre[start];
    }

    public int Saberi(int start)
    {
        return zbirPre[zbirPre.Length - 1] - zbirPre[start];
    }

    public int Saberi()
    {
        return zbirPre[zbirPre.Length - 1];
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[] a = { 1, 3, -1, 4, 2 };
        Sabirac sa = new Sabirac(a);
        Console.WriteLine(sa.Saberi());
        Console.WriteLine(sa.Saberi(2));
        Console.WriteLine(sa.Saberi(1, 2));
    }
}