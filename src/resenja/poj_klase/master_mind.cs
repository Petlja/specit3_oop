using System;
using System.Collections.Generic;

class MasterMind
{
    private Random rnd;
    private int brMesta;
    private int brCifara;
    private int[] zadato;

    public MasterMind(int brM, int brC)
    {
        rnd = new Random();
        brMesta = brM;
        brCifara = brC;
        zadato = new int[brMesta];
        for (int i = 0; i < brMesta; i++)
            zadato[i] = 1 + rnd.Next(brCifara);
    }
    public void Pokusaj(int[] pitanje, out int crvenih, out int zutih)
    {
        bool[] uparenoZadato = new bool[brMesta];
        bool[] uparenoPitanje = new bool[brMesta];
        crvenih = 0;
        zutih = 0;
        for (int i = 0; i < brMesta; i++)
        {
            if (zadato[i] == pitanje[i])
            {
                crvenih++;
                uparenoZadato[i] = true;
                uparenoPitanje[i] = true;
            }
        }
        for (int i = 0; i < brMesta; i++)
        {
            if (!uparenoZadato[i])
            {
                for (int j = 0; j < brMesta; j++)
                {
                    if (zadato[i] == pitanje[j] && !uparenoPitanje[j])
                    {
                        zutih++;
                        uparenoZadato[i] = true;
                        uparenoPitanje[j] = true;
                        break;
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int brojMesta = 4;
        int brojCifara = 6;
        int crvenih, zutih;
        MasterMind igra = new MasterMind(brojMesta, brojCifara);
        List<int[]> pitanja = new List<int[]>();
        List<int> odgovoriCrvene = new List<int>();
        List<int> odgovoriZute = new List<int>();
        bool kraj = false;
        while (!kraj)
        {
            int[] pitanje = new int[brojMesta];
            Console.Write("pogadjaj: ");
            string ulaz = Console.ReadLine();
            for (int i = 0; i < brojMesta; i++)
                pitanje[i] = int.Parse(ulaz[i].ToString());
            
            igra.Pokusaj(pitanje, out crvenih, out zutih);
            pitanja.Add(pitanje);
            odgovoriCrvene.Add(crvenih);
            odgovoriZute.Add(zutih);
            if (crvenih == brojMesta)
                kraj = true;

            Console.Clear();
            for (int i = 0; i < pitanja.Count; i++)
            {
                Console.Write("{0}: ", i + 1);
                for (int m = 0; m < brojMesta; m++)
                    Console.Write(pitanja[i][m]);
                Console.WriteLine(" -> {0} {1}", odgovoriCrvene[i], odgovoriZute[i]);
            }
        }
    }
}
