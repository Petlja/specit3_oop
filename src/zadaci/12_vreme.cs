using System;
class Vreme
{
    private int sec; // broj sekundi proteklih od (neke) ponoci

    private const int SecsPerDay = 24 * 60 * 60; // broj sekundi u jednom danu
    public int Total { get { return sec; } } // ukupan broj sekundi
    public int D { get { return sec / SecsPerDay; } } // broj dana
    public int H { get { return (sec % SecsPerDay) / 3600; } } // broj sati
    public int M { get { return (sec % 3600) / 60; } } // broj minuta
    public int S { get { return (sec % 60); } } // broj sekundi

    // konstruktorom se zadaje broj sati, minuta i sekundi (minuti i sekunde 
    // mogu da se izostave, u tom slučaju se podrazumeva 0 minuta i 0 sekundi)
    public Vreme(int h, int m = 0, int s = 0)
    {
        sec = (h * 60 + m) * 60 + s;
    }

    // Redefinišemo metod za prikazivanje objekta
    public override string ToString()
    {
        if (D == 0)
            return string.Format("{0:00}:{1:00}:{2:00}", H, M, S);
        else
            return string.Format("{0:00}:{1:00}:{2:00}+{3}", H, M, S, D);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        Vreme t1 = new Vreme(14, 23, 51); // 14 sati, 23 minuta, 51 sekunda
        Vreme t2 = new Vreme(15, 23, 50); // 15 sati, 23 minuta, 50 sekundi
        Console.WriteLine("t1 = {0}", t1); // implicitno se poziva metod ToString
        Console.WriteLine("t2 = {0}", t2);
        Console.Write("Vremenska razlika je ");
        Console.WriteLine(new Vreme(0, 0, t2.Total - t1.Total));
        Console.WriteLine();
        Vreme t3 = new Vreme(49);
        Console.WriteLine("t3 = {0}", t3);
    }
}
