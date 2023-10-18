using System;

public class Robot
{
    private int x, y; // pozicija robota
    char smer; // smer u kome je robot okrenut
    
    // konstruktor - robot je zadat pozicijom i smerom
    public Robot(int x0, int y0, char orijentacija)
    {
        x = x0;
        y = y0;
        smer = orijentacija;
    }
    
    // metod za kretanje napred
    public void Napred(int n = 1)
    {
        switch (smer)
        {
            case 'N': y += n; break;
            case 'E': x += n; break;
            case 'S': y -= n; break;
            case 'W': x -= n; break;
        }
    }
    
    // metod za okretanje nalevo
    public void Nalevo()
    {
        switch (smer)
        {
            case 'N': smer = 'W'; break;
            case 'E': smer = 'N'; break;
            case 'S': smer = 'E'; break;
            case 'W': smer = 'S'; break;
        }
    }
    
    // metod za okretanje nadesno
    public void Nadesno()
    {
        switch (smer)
        {
            case 'N': smer = 'E'; break;
            case 'E': smer = 'S'; break;
            case 'S': smer = 'W'; break;
            case 'W': smer = 'N'; break;
        }
    }

    // prikaz podataka o robotu
    public override string ToString()
    {
        return string.Format("Robot({0}, {1}, {2})", x, y, smer);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // proba
        Robot r1 = new Robot(2, 3, 'N');
        r1.Napred();
        r1.Nadesno();
        r1.Napred(5);
        Console.WriteLine(r1);
    }
}
