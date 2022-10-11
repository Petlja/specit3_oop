public class DinamickiSabirac
{
    private int n;
    private int offset;
    private int[] a;

    public DinamickiSabirac(int duzina)
    {
        n = duzina;
        offset = 1;
        while (duzina > 0) { duzina >>= 1; offset <<= 1; }
        a = new int[2 * offset];
    }

    public int this[int i]
    {
        get
        {
            if (0 <= i && i < n)
                return a[offset + i];
            else return a[-1]; // throw
        }
        set
        {
            int k = offset + i;
            a[k] = value;
            while (k > 1)
            {
                int k1 = k ^ 1;
                int kPola = k >> 1;
                a[kPola] = a[k] + a[k1];
                k = kPola;
            }
        }
    }

    public int Zbir(int od, int koliko)
    {
        int poc = od + offset;
        int zbir = 0;
        while (koliko > 0)
        {
            if ((poc & 1) > 0) { zbir += a[poc]; poc++; koliko--; }
            if ((koliko & 1) > 0) { zbir += a[poc + koliko - 1]; koliko--; }
            poc >>= 1;
            koliko >>= 1;
        }
        return zbir;
    }
}
