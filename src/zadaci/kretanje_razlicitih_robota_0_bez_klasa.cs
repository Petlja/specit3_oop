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
        const int NEUSM_SPOR = 1, NEUSM_BRZ = 2, USM_SPOR = 3, USM_BRZ = 4;
        int[] dx = new int[] { 0, 1, 0, -1 };
        int[] dy = new int[] { -1, 0, 1, 0 };
        int nV = tabla.GetLength(0);
        int nK = tabla.GetLength(1);
        int x = 0, y = 0, smer = 0, x1, y1; 
        string komande = "RRFLFRFLBF";
        foreach (char komanda in komande)
        {
            switch (komanda)
            {
                case 'R': //Desno
                    switch (tipRobota)
                    {
                        case NEUSM_SPOR: 
                            if (x + 1 < nK && tabla[y, x + 1] == 0) 
                                x++;
                            break;
                        case NEUSM_BRZ:
                            while (x + 1 < nK && tabla[y, x + 1] == 0) 
                                x++;
                            break;
                        case USM_SPOR:
                            smer = (smer + 1) % 4; 
                            break;
                        case USM_BRZ:
                            smer = (smer + 1) % 4;
                            break;

                        default:
                            break;
                    }
                    break;
                case 'L': // Levo
                    switch (tipRobota)
                    {
                        case NEUSM_SPOR:
                            if (x > 0 && tabla[y, x - 1] == 0) 
                                x--;
                            break;
                        case NEUSM_BRZ:
                            while (x > 0 && tabla[y, x - 1] == 0) 
                                x--; 
                            break;
                        case USM_SPOR:
                            smer = (smer + 3) % 4;
                            break;
                        case USM_BRZ:
                            smer = (smer + 3) % 4; 
                            break;
                        default:
                            break;
                    }
                    break;
                case 'F': // Napred
                    switch (tipRobota)
                    {
                        case NEUSM_SPOR:
                            if (y > 0 && tabla[y - 1, x] == 0) 
                                y--;
                            break;
                        case NEUSM_BRZ:
                            while (y > 0 && tabla[y - 1, x] == 0) 
                                y--;
                            break;
                        case USM_SPOR:
                            x1 = x + dx[smer];
                            y1 = y + dy[smer];
                            if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
                            {
                                y = y1;
                                x = x1;
                            }
                            break;
                        case USM_BRZ:
                            while (true)
                            {
                                x1 = x + dx[smer];
                                y1 = y + dy[smer];
                                if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
                                {
                                    y = y1;
                                    x = x1;
                                }
                                else break;
                            }
                            break;

                        default:
                            break;
                    }
                    break;
                case 'B': //Nazad
                    switch (tipRobota)
                    {
                        case NEUSM_SPOR:
                            if (y + 1 < nV && tabla[y + 1, x] == 0) 
                                y++; 
                            break;
                        case NEUSM_BRZ:
                            while (y + 1 < nV && tabla[y + 1, x] == 0) 
                                y++;
                            break;
                        case USM_SPOR:
                            x1 = x - dx[smer];
                            y1 = y - dy[smer];
                            if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
                            {
                                y = y1;
                                x = x1;
                            }
                            break;
                        case USM_BRZ:
                            while (true)
                            {
                                x1 = x - dx[smer];
                                y1 = y - dy[smer];
                                if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
                                {
                                    y = y1;
                                    x = x1;
                                }
                                else break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default: break;
            }
        }
        Console.WriteLine("Robot je na polju ({0}, {1})", x, y);
    }
}
/*
za tip 1 ispisuje:  Robot je na polju (1, 0) 
za tip 2 ispisuje:  Robot je na polju (0, 0) 
za tip 3 ispisuje:  Robot je na polju (1, 2) 
za tip 4 ispisuje:  Robot je na polju (7, 3) 
*/