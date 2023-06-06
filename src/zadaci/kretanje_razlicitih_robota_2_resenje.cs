using System;
public abstract class Robot
{
    protected int[,] tabla;
    protected int nV, nK;
    protected int x, y;
    public int X { get {return x;} }
    public int Y { get { return y; } }

    protected Robot(int x0, int y0, int[,] a)
    {
        x = x0; 
        y = y0;
        tabla = a;
        nV = a.GetLength(0);
        nK = a.GetLength(1);
    }
    public static Robot Napravi(int tip, int x0, int y0, int[,] a)
    {
        switch (tip)
        {
            case 1: return new NeusmereniSporiRobot(x0, y0, a);
            case 2: return new NeusmereniBrziRobot(x0, y0, a);
            case 3: return new UsmereniSporiRobot(x0, y0, 'N', a);
            case 4: return new UsmereniBrziRobot(x0, y0, 'N', a);
            default: return null;
        }
    }
    public abstract void Desno();
    public abstract void Levo();
    public abstract void Napred();
    public abstract void Nazad();
}

class NeusmereniSporiRobot : Robot
{
    public NeusmereniSporiRobot(int x0, int y0, int[,] a) : base(x0, y0, a)
    {
    }
    public override void Desno() 
    {
        if (x + 1 < nK && tabla[y, x + 1] == 0) x++;
    }
    public override void Levo()
    {
        if (x > 0 && tabla[y, x - 1] == 0) x--;
    }

    public override void Napred() 
    {
        if (y > 0 && tabla[y - 1, x] == 0) y--;
    }
    public override void Nazad()
    {
        if (y + 1 < nV && tabla[y + 1, x] == 0) y++;
    }
}

class NeusmereniBrziRobot : Robot
{
    public NeusmereniBrziRobot(int x0, int y0, int[,] a) : base(x0, y0, a)
    {
    }
    public override void Desno()
    {
        while (x + 1 < nK && tabla[y, x + 1] == 0) x++;
    }
    public override void Levo()
    {
        while (x > 0 && tabla[y, x - 1] == 0) x--;
    }

    public override void Napred()
    {
        while (y > 0 && tabla[y - 1, x] == 0) y--;
    }
    public override void Nazad()
    {
        while (y + 1 < nV && tabla[y + 1, x] == 0) y++;
    }
}

class UsmereniSporiRobot : Robot
{
    static readonly int[] dx = new int[] { 0, 1, 0, -1 };
    static readonly int[] dy = new int[] { -1, 0, 1, 0 };
    
    protected int smer;
    public UsmereniSporiRobot(int x0, int y0, char sm, int[,] a) : base(x0, y0, a)
    {
        switch (sm)
        {
            case 'N': smer = 0; break;
            case 'E': smer = 1; break;
            case 'S': smer = 2; break;
            case 'W': smer = 3; break;
            default: smer = 0; break;
        }
    }
    public override void Desno()
    {
        smer = (smer + 1) % 4;
    }
    public override void Levo()
    {
        smer = (smer + 3) % 4;
    }

    public override void Napred()
    {
        int x1 = x + dx[smer];
        int y1 = y + dy[smer];
        if (x1 >= 0 && y1 >= 0 && x1 <nK && y1 < nV && tabla[y1, x1] == 0) 
        {
            y = y1; 
            x = x1;
        }
    }
    public override void Nazad()
    {
        int x1 = x - dx[smer];
        int y1 = y - dy[smer];
        if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
        {
            y = y1;
            x = x1;
        }
    }
}

class UsmereniBrziRobot : Robot
{
    static readonly int[] dx = new int[] { 0, 1, 0, -1 };
    static readonly int[] dy = new int[] { -1, 0, 1, 0 };

    protected int smer;
    public UsmereniBrziRobot(int x0, int y0, char sm, int[,] a) : base(x0, y0, a)
    {
        switch (sm)
        {
            case 'N': smer = 0; break;
            case 'E': smer = 1; break;
            case 'S': smer = 2; break;
            case 'W': smer = 3; break;
            default: smer = 0; break;
        }
    }
    public override void Desno()
    {
        smer = (smer + 1) % 4;
    }
    public override void Levo()
    {
        smer = (smer + 3) % 4;
    }

    public override void Napred()
    {
        while (true)
        {
            int x1 = x + dx[smer];
            int y1 = y + dy[smer];
            if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
            {
                y = y1;
                x = x1;
            }
            else break;
        }
    }
    public override void Nazad()
    {
        while (true)
        {
            int x1 = x - dx[smer];
            int y1 = y - dy[smer];
            if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
            {
                y = y1;
                x = x1;
            }
            else break;
        }
    }
}
using System;
class Program
{
    static void Main()
    {
        int[,] tabla = new int[,] {
            { 0, 0, 0, 1, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 1, 0, 0, 1, 0, 0, 0, 0, 1 },
        };
        Console.WriteLine("1: neusmeren spor robot");
        Console.WriteLine("2: neusmeren brz robot");
        Console.WriteLine("3: usmeren spor robot");
        Console.WriteLine("4: usmeren brz robot");
        Console.Write("Izaberi robota (1/2/3/4) ");
        int tipRobota = int.Parse(Console.ReadLine());
        Robot robot = Robot.Napravi(tipRobota, 0, 0, tabla);
        string komande = "RRFLFRFLBF";
        foreach (char komanda in komande)
        {
            switch (komanda)
            {
                case 'R': robot.Desno(); break;
                case 'L': robot.Levo(); break;
                case 'F': robot.Napred(); break;
                case 'B': robot.Nazad(); break;
                default: break;
            }
        }
        Console.WriteLine("Robot je na polju ({0}, {1})",
            robot.X, robot.Y);
    }
}
/*
za tip 1 ispisuje:  Robot je na polju (1, 0) 
za tip 2 ispisuje:  Robot je na polju (0, 0) 
za tip 3 ispisuje:  Robot je na polju (1, 2) 
za tip 4 ispisuje:  Robot je na polju (7, 3) 
*/