using System;
using System.Collections.Generic;

public class IksOks
{
    private char[,] tabla;
    private bool pobeda;
    private bool kraj;
    private int brPraznihPolja;
    public IksOks()
    {
        tabla = new char[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                tabla[i, j] = ' ';
        kraj = false;
        brPraznihPolja = 9;
    }

    public char this[int i, int j]
    {
        get { return tabla[i, j]; }
    }
    public bool Potez(int i, int j, bool X)
    {
        if (kraj)
            return false;
        if (i < 0 || i > 2 || j < 0 || j > 2)
            return false;
        if (tabla[i, j] != ' ')
            return false;

        tabla[i, j] = X ? 'X' : 'O';
        brPraznihPolja--;
        ProveriKraj(i, j, tabla[i, j]);
        return true;
    }
    private void ProveriKraj(int i, int j, char c)
    {
        pobeda =
            (tabla[i, 0] == c && tabla[i, 1] == c && tabla[i, 2] == c) ||
            (tabla[0, j] == c && tabla[1, j] == c && tabla[2, j] == c) ||
            (tabla[0, 0] == c && tabla[1, 1] == c && tabla[2, 2] == c) ||
            (tabla[0, 2] == c && tabla[1, 1] == c && tabla[2, 0] == c);

        kraj = pobeda || (brPraznihPolja == 0);
    }
    public bool Kraj { get { return kraj; } }
    public bool Pobeda { get { return pobeda; } }
}

class Program
{
    static void Prikazi(IksOks igra)
    {
        Console.Clear();
        Console.WriteLine("1 {0}|{1}|{2}", igra[0,0], igra[0, 1], igra[0, 2]);
        Console.WriteLine("  -+-+-");
        Console.WriteLine("2 {0}|{1}|{2}", igra[1, 0], igra[1, 1], igra[1, 2]);
        Console.WriteLine("  -+-+-");
        Console.WriteLine("3 {0}|{1}|{2}", igra[2, 0], igra[2, 1], igra[2, 2]);
        Console.WriteLine();
        Console.WriteLine("  A B C");
    }
    static void Main(string[] args)
    {
        IksOks igra = new IksOks();
        Prikazi(igra);
        string naPotezu = "O";
        while (!igra.Kraj)
        {
            if (naPotezu == "X")
                naPotezu = "O";
            else
                naPotezu = "X";

            bool potezOdigran = false;
            while (!potezOdigran)
            {
                Console.Write("Igrac {0} - polje? ", naPotezu);
                string s = Console.ReadLine().ToUpper();
                int i = s[1] - '1';
                int j = s[0] - 'A';
                potezOdigran = igra.Potez(i, j, naPotezu == "X");
                Prikazi(igra);
            }
        }
        if(igra.Pobeda)
            Console.WriteLine("Igrac {0} je podedio.", naPotezu);
        else
            Console.WriteLine("Nereseno.");
    }
}
