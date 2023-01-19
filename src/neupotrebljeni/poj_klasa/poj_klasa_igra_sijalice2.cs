﻿using System;

class Sijalice
{
    private const int Blok = -2;
    private const int Sijalica = -1;
    private const int Prazno = 0;
    private int[,] tabla;
    private int brRedova;
    private int brKolona;

    public Sijalice(int[,] a)
    {
        brRedova = a.GetLength(0);
        brKolona = a.GetLength(1);
        tabla = new int[brRedova, brKolona];
        for (int r = 0; r < brRedova; r++)
            for (int k = 0; k < brKolona; k++)
                if (a[r, k] > 0)
                    tabla[r, k] = Blok;
                else
                    tabla[r, k] = Prazno;
    }
    public char this[int i, int j]
    {
        get
        {
            //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
            //    return ' ';
            if (tabla[i, j] == Blok)
                return '█';
            if (tabla[i, j] == Sijalica)
                return 'X';
            if (tabla[i, j] == Prazno)
            {
                for (int i1 = i + 1; i1 < brRedova && tabla[i1, j] != Blok; i1++)
                    if (tabla[i1, j] == Sijalica) return 'o';
                for (int i1 = i - 1; i1 >= 0 && tabla[i1, j] != Blok; i1--)
                    if (tabla[i1, j] == Sijalica) return 'o';
                for (int j1 = j + 1; j1 < brKolona && tabla[i, j1] != Blok; j1++)
                    if (tabla[i, j1] == Sijalica) return 'o';
                for (int j1 = j - 1; j1 >= 0 && tabla[i, j1] != Blok; j1--)
                    if (tabla[i, j1] == Sijalica) return 'o';
            }
            return '.';
        }
    }
    public int BrRedova { get { return brRedova; } }
    public int BrKolona { get { return brKolona; } }
    public void Stavi(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return;
        if (tabla[i, j] == Prazno)
            tabla[i, j] = Sijalica;
    }
    public void Skloni(int i, int j)
    {
        //if (i < 0 || i >= BrRedova || j < 0 || j >= brKolona)
        //    return;
        if (tabla[i, j] == Sijalica)
            tabla[i, j] = Prazno;
    }
    public bool Resio
    {
        get
        {
            for (int i = 0; i < brRedova; i++)
                for (int j = 0; j < brKolona; j++)
                {
                    if (tabla[i, j] == Blok)
                        continue;

                    bool osvetljeno = false;
                    for (int i1 = i + 1;
                        i1 < brRedova && tabla[i1, j] != Blok && !osvetljeno;
                        i1++)
                        if (tabla[i1, j] == Sijalica) osvetljeno = true;
                    for (int i1 = i - 1;
                        i1 >= 0 && tabla[i1, j] != Blok && !osvetljeno;
                        i1--)
                        if (tabla[i1, j] == Sijalica) osvetljeno = true;
                    for (int j1 = j + 1;
                        j1 < brKolona && tabla[i, j1] != Blok && !osvetljeno;
                        j1++)
                        if (tabla[i, j1] == Sijalica) osvetljeno = true;
                    for (int j1 = j - 1;
                        j1 >= 0 && tabla[i, j1] != Blok && !osvetljeno;
                        j1--)
                        if (tabla[i, j1] == Sijalica) osvetljeno = true;

                    if (tabla[i, j] == Sijalica && osvetljeno)
                        return false;
                    if (tabla[i, j] == Prazno && !osvetljeno)
                        return false;
                }
            return true;
        }
    }
}
class Program
{
    static void Prikazi(Sijalice igra)
    {
        for (int red = 0; red < igra.BrRedova; red++)
        {
            Console.Write("{0} ", red + 1);
            for (int kol = 0; kol < igra.BrKolona; kol++)
                Console.Write(igra[red, kol]);

            Console.WriteLine();
        }
        Console.Write("  ");
        for (int kol = 0; kol < igra.BrKolona; kol++)
            Console.Write((char)('a' + kol));
        Console.WriteLine();
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        int[,] a = {
            { 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };
        Sijalice igra = new Sijalice(a);

        while (!igra.Resio)
        {
            Console.Clear();
            Prikazi(igra);
            Console.Write("Komanda: ");
            string[] unos = Console.ReadLine().Split();
            string komanda = unos[0];
            int red = unos[1][1] - '1';
            int kol = unos[1][0] - 'a';
            if (komanda == "x")
            {
                igra.Stavi(red, kol);
                Prikazi(igra);
            }
            else if (komanda == ".")
            {
                igra.Skloni(red, kol);
                Prikazi(igra);
            }
        }
        Console.Clear();
        Prikazi(igra);
        Console.WriteLine("Bravo!");
    }
}
