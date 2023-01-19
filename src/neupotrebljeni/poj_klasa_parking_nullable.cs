using System;
using System.Collections.Generic;

class Parking
{
    private int brMesta;
    private double cenaPoSatu;
    DateTime?[] dosao;
    Queue<int> slobodan;
    public Parking(int n, double c)
    {
        brMesta = n;
        cenaPoSatu = c;
        dosao = new DateTime?[n];
        slobodan = new Queue<int>();
        for (int i = 0; i < n; i++)
            slobodan.Enqueue(i);
    }
    public int BrSlobodnihMesta
    {
        get { return slobodan.Count; }
    }
    public int Dolazak(DateTime t)
    {
        if (slobodan.Count == 0)
            return -1;

        int i = slobodan.Dequeue();
        dosao[i] = t;
        return i;
    }
    public double Odlazak(int i, DateTime t)
    {
        if (!dosao[i].HasValue)
            return 0;

        int sati = (int)Math.Ceiling(t.Subtract(dosao[i].Value).TotalHours);
        dosao[i] = null;
        slobodan.Enqueue(i);
        return sati * cenaPoSatu;
    }
}
class Program
{
    static void Main(string[] args)
    {
        DateTime t0 = DateTime.Today;
        DateTime t1 = t0.AddMinutes(62);

        Parking p = new Parking(5, 100);
        int m = p.Dolazak(t0);
        Console.WriteLine("Ima {0} mesta.", p.BrSlobodnihMesta);
        double platiti = p.Odlazak(m, t1);
        Console.WriteLine("Platiti {0} dinara.", platiti);
    }
}
