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