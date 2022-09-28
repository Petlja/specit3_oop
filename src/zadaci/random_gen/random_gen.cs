using System;
using System.Text;

public class Rand
{
    // void Seed(double seed)
    // double GetState()
    // void SetState(double state)
    private static Random rnd = new Random();

    /// Slucajno izabran broj iz opsega[0, n - 1]
    public static int Choice(int n)
    {
        return rnd.Next(n);
    }
    /// Slucajno izabran broj iz opsega[0, n - 1]
    /// gde je counts[i] broj ponavljanja broja i, 0 <= i < n
    public static int Choice(int n, int[] counts)
    {
        int[] a = new int[n];
        int s = 0;
        for (int i = 0; i < n; i++)
        {
            s += counts[i];
            a[i] = s;
        }
        int c = rnd.Next(s);
        for (int i = 0; i < n; i++)
            if (c < a[i])
                return i;

        return n;
    }
    /// slucajan izbor k elemenata iz opsega [0, n-1] (sa vracanjem)
    public static int[] Choices(int n, int k)
    {
        int[] a = new int[k];
        for (int i = 0; i < k; i++)
        {
            a[i] = rnd.Next(n);
        }
        return a;
    }
    /// slucajan izbor k elemenata iz opsega [0, n-1] (sa vracanjem)
    /// gde je counts[i] broj ponavljanja broja i, 0 <= i < n
    public static int[] Choices(int n, int[] counts, int k)
    {
        int[] a = new int[n];
        int s = 0;
        for (int i = 0; i < n; i++)
        {
            s += counts[i];
            a[i] = s;
        }
        int[] res = new int[k];
        for (int i = 0; i < k; i++)
        {
            int c = rnd.Next(s);
            for (int j = 0; j < n; j++)
                if (c < a[j])
                {
                    res[i] = j;
                    break;
                }
        }
        return res;
    }
    /// slucajan uzorak (bez vracanja) od k elemenata iz opsega[0, n - 1]
    public static int[] Sample(int n, int k)
    {
        // O(k^2) slozenost (nije optimalna)
        int[] a = new int[k];
        for (int i = 0; i < k; i++)
        {
            int r = rnd.Next(n - i);
            a[i] = r;
            for (int j = 0; j < i; j++)
                if (a[j] <= r)
                    a[i]++;
        }
        return a;
    }
    /// slucajan uzorak (bez vracanja) od k elemenata iz opsega[0, n - 1]
    /// gde je counts[i] broj ponavljanja broja i, 0 <= i < n
    public static int[] Sample(int n, int[] counts, int k)
    {
        // O(k*n) slozenost (nije optimalna)
        int[] a = new int[n];
        int s = 0;
        for (int i = 0; i < n; i++)
        {
            s += counts[i];
            a[i] = s;
        }
        int[] res = new int[k];
        for (int i = 0; i < k; i++)
        {
            int r = rnd.Next(s);
            for (int j = 0; j < n; j++)
            {
                if (r < a[j])
                {
                    res[i] = j;
                    break;
                }
            }
            for (int j = res[i]; j < n; j++)
            {
                a[j]--;
            }
            s--;
        }
        return res;
    }

    public static double Uniform(double a, double b)
    {
        return a + rnd.NextDouble() * (b - a);
    }
    public static double Triangular(double a, double b, double mode)
    {
        double h = 2.0 / (b - a);
        double pLeft = 0.5 * (mode - a) * h;
        double px = rnd.NextDouble();
        if (px < pLeft)
            return a + (mode - a) * Math.Sqrt(px / pLeft);
        else
            return b - (b - mode) * Math.Sqrt((1 - px) / (1 - pLeft));
    }

    // https://www.alanzucconi.com/2015/09/16/how-to-sample-from-a-gaussian-distribution/
    public static double Gauss(double m, double sigma)
    {
        //double u1 = 1.0 - rnd.NextDouble(); //u1 je iz (0,1]
        //double u2 = 1.0 - rnd.NextDouble();
        //double randStdNormal = 
        //    Math.Sqrt(-2.0 * Math.Log(u1)) *
        //    Math.Sin(2.0 * Math.PI * u2);
        //return m + sigma * randStdNormal;
        double v1, v2, s;
        do
        {
            v1 = 2.0 * rnd.NextDouble() - 1.0; // [-1, 1]
            v2 = 2.0 * rnd.NextDouble() - 1.0; // [-1, 1]
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0 || s == 0);

        s = Math.Sqrt((-2.0f * Math.Log(s)) / s);
        return m + v1 * s * sigma;
    }
}

class Program
{
    static string BojeSlotovaRuleta(int n)
    {
        StringBuilder sb = new StringBuilder();
        string colors = "RBG"; // crvena, crna, zelena
        int[] colorCounts = { 18, 18, 2 };
        var chosen = Rand.Choices(colors.Length, colorCounts, n);
        foreach (int i in chosen)
            sb.Append(colors[i] + " ");

        return sb.ToString();
    }
    static double OcekivaniBrKarataSaSlikom(int brKarata, int brPon)
    {
        int[] cardCounts = { 16, 36 }; // 16 slika, 36 praznih
        double s = 0;
        for (int i = 0; i < brPon; i++)
        {
            int[] sample = Rand.Sample(cardCounts.Length, cardCounts, brKarata);
            foreach (int x in sample)
                if (x == 0)
                    s++;
        }
        return s / brPon;
    }

    static double VerovatnocaZbiraKockiceDo(int zbir, int brBacanja, int brPonavljanja)
    {
        int brUspeha = 0;
        for (int pon = 0; pon < brPonavljanja; pon++)
        {
            int[] a = Rand.Choices(6, brBacanja);
            int s = 0;
            foreach (int i in a)
                s += i + 1;
            if (s <= zbir)
                brUspeha++;
        }
        return (double)brUspeha / brPonavljanja;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Boje slotova ruleta" +
            " za 10 bacanja kuglice: {0}",
            BojeSlotovaRuleta(10));

        Console.WriteLine("Ocekivani br. karata sa slikom" +
            " medju 13 karata: {0:0.000}",
            OcekivaniBrKarataSaSlikom(13, 30));

        Console.WriteLine("Priblizna verovatnoca da iz tri" +
            " bacanja kockice zbir bude najvise 11: {0:0.000}",
            VerovatnocaZbiraKockiceDo(11, 3, 20));

        Console.Write("Uniform (3, 5): ");
        for (int i = 0; i < 10; i++)
            Console.Write("{0:0.000} ", Rand.Uniform(3, 5));
        Console.WriteLine();

        Console.Write("Triangular(3, 6, 5): ");
        for (int i = 0; i < 10; i++)
            Console.Write("{0:0.000} ", Rand.Triangular(3, 6, 5));
        Console.WriteLine();

        Console.Write("Gauss(7, 2): ");
        for (int i = 0; i < 10; i++)
            Console.Write("{0:0.000} ", Rand.Gauss(7, 2));
        Console.WriteLine();
    }
}
