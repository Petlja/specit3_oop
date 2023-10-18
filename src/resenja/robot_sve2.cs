using System;

public class Robot
{
    private int x, y; // pozicija robota
    private int smer; // smer u kome je robot okrenut
    
    // pomaci duž x i y ose za svaki smer
    private static int[] dx = { 0, 1, 0, -1 }; 
    private static int[] dy = { 1, 0, -1, 0 };
    
    private const string ImeSmera = "NESW"; // oznake smerova

    // konstruktor - robot je zadat pozicijom i smerom
    public Robot(int x0, int y0, char orijentacija)
    {
        x = x0;
        y = y0;
        switch (char.ToUpper(orijentacija))
        {
            case 'N': smer = 0; break;
            case 'E': smer = 1; break;
            case 'S': smer = 2; break;
            case 'W': smer = 3; break;
            default: throw new Exception("Nepostojeca orijentacija");
        }
    }
    
    // metod za kretanje napred
    public void Napred(int n = 1)
    {
        x += n * dx[smer];
        y += n * dy[smer];
    }
    
    // metod za okretanje nalevo
    public void Nalevo()
    {
        smer = (smer + 3) % 4;
    }
    
    // metod za okretanje nadesno
    public void Nadesno()
    {
        smer = (smer + 1) % 4;
    }

    // prikaz podataka o robotu
    public override string ToString()
    {
        return string.Format("Robot({0}, {1}, {2})", x, y, ImeSmera[smer]);
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

