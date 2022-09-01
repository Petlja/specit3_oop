public class DinamickiSabirac
{
    private int n;
    private int[] a;

    public DinamickiSabirac(int duzina)
    {
        n = duzina;
        a = new int[n];
    }

    public int this[int i]
    {
        get { return a[i]; }
        set { a[i] = value; }
    }

    public int Zbir(int od, int koliko)
    {
        int zbir = 0;
        for (int i = od; i < od + koliko; i++)
            zbir += a[i];
        return zbir;
    }
}
