using System;
class Program
{
    static void Main()
    {
        // tabla po kojoj se kreću roboti
        int[,] tabla = new int[,] {
            { 0, 0, 0, 1, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 1, 0, 0, 1, 0, 0, 0, 0, 1 },
        };

        // prikazujemo meni korisniku
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

        // niska komandi za upravljanje robotom (svako slovo je jedna komanda)
        string komande = "RRFLFRFLBF";
        foreach (char komanda in komande)
        {
            switch (komanda)
            {
                case 'R': //Desno
                    switch (tipRobota)
                    {
                        case NEUSM_SPOR: 
                            // pomeranje za jedno polje nadesno (ako je moguće)
                            if (x + 1 < nK && tabla[y, x + 1] == 0) 
                                x++;
                            break;
                        case NEUSM_BRZ:
                            // pomeranje za više polja nadesno
                            while (x + 1 < nK && tabla[y, x + 1] == 0) 
                                x++;
                            break;
                        case USM_SPOR:
                            // okret nadesno
                            smer = (smer + 1) % 4; 
                            break;
                        case USM_BRZ:
                            // okret nadesno
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
                            // pomeranje za jedno polje nalevo (ako je moguće)
                            if (x > 0 && tabla[y, x - 1] == 0) 
                                x--;
                            break;
                        case NEUSM_BRZ:
                            // pomeranje za više polja nalevo 
                            while (x > 0 && tabla[y, x - 1] == 0) 
                                x--; 
                            break;
                        case USM_SPOR:
                            // okret nalevo
                            smer = (smer + 3) % 4;
                            break;
                        case USM_BRZ:
                            // okret nalevo
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
                            // pomeranje za jedno polje ka vrhu table (ako je moguće)
                            if (y > 0 && tabla[y - 1, x] == 0) 
                                y--;
                            break;
                        case NEUSM_BRZ:
                            // pomeranje za više polja ka vrhu table
                            while (y > 0 && tabla[y - 1, x] == 0) 
                                y--;
                            break;
                        case USM_SPOR:
                            // pomeranje za jedno polje u tekućem smeru (ako je moguće)
                            x1 = x + dx[smer];
                            y1 = y + dy[smer];
                            if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
                            {
                                y = y1;
                                x = x1;
                            }
                            break;
                        case USM_BRZ:
                            // pomeranje za više polja u tekućem smeru
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
                        // pomeranje za jedno polje ka dnu table (ako je moguće)
                        case NEUSM_SPOR:
                            if (y + 1 < nV && tabla[y + 1, x] == 0) 
                                y++; 
                            break;
                        // pomeranje za više polja ka dnu table
                        case NEUSM_BRZ:
                            while (y + 1 < nV && tabla[y + 1, x] == 0) 
                                y++;
                            break;
                        // pomeranje za jedno polje suprotno od tekućeg smera (ako je moguće)
                        case USM_SPOR:
                            x1 = x - dx[smer];
                            y1 = y - dy[smer];
                            if (x1 >= 0 && y1 >= 0 && x1 < nK && y1 < nV && tabla[y1, x1] == 0)
                            {
                                y = y1;
                                x = x1;
                            }
                            break;
                        // pomeranje za više polja suprotno od tekućeg smera (ako je moguće)
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