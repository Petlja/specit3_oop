using System;

public class Robot
{
    private int x, y, smer;
    private const string ImeSmera = "NESW";
    private static int[] dx = { 0, 1, 0, -1 };
    private static int[] dy = { 1, 0, -1, 0 };
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
    public void Napred(int n = 1)
    {
        x += n * dx[smer];
        y += n * dy[smer];
    }
    public void Nalevo()
    {
        smer = (smer + 3) % 4;
    }
    public void Nadesno()
    {
        smer = (smer + 1) % 4;
    }

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

