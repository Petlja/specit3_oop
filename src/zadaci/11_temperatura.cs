using System;
class Temperatura
{
    private double t; // temperatura se interno pamti u stepenima Celzijusa

    public double C // Svojstvo C predstavlja temperaturu u stepenima Celzijusa
    {
        get { return t; }
        set { t = value; }
    }

    public double F // Svojstvo F predstavlja temperaturu u stepenima Farenhajta
    {
        get { return t * 1.8 + 32.0; }
        set { t = (value - 32.0) / 1.8; }
    }

    public double K // // Svojstvo K predstavlja temperaturu u stepenima Kelvina
    {
        get { return t + 273.15; }
        set { t = value - 273.15; }
    }

    // Konstruktor, objekat je zadatat temperaturom u stepenima Celzijusa
    public Temperatura(double temp) 
    {
        t = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        Temperatura t1 = new Temperatura(23); // početna temperatura je 23°C
        Console.WriteLine("{0:0.00}C = {1:0.00}F", t1.C, t1.F);
        t1.F = 80; // postavljamo temperaturu na 80°F
        Console.WriteLine("{0:0.00}F = {1:0.00}C", t1.F, t1.C);
    }
}
