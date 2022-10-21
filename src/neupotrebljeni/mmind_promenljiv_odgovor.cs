// moguca dorada: 
// da ruletom sa nejednakim slotovima dopusti i neki
// odgovor koji ne maksimizira broj preostalih mogucnosti
using System;
using System.Collections;
using System.Collections.Generic;
class MasterMind
{
    private Random rnd = new Random();
    private List<int[]> moguce = new List<int[]>();
    private int brMesta;
    private int brVrednosti;
    public MasterMind(int poz, int vr)
    {
        NewGame(poz, vr);
    }
    public void NewGame(int poz, int vr)
    {
        brVrednosti = vr;
        brMesta = poz;
        moguce.Clear();
        int[] a = new int[brMesta];
        Napuni(a, 0);
    }
    private void Napuni(int[] a, int d)
    {
        if (d == brMesta)
        {
            int[] b = (int[])a.Clone();
            moguce.Add(b);
        }
        else 
        {
            for (int i = 1; i <= brVrednosti; i++)
            {
                a[d] = i;
                Napuni(a, d + 1);
            }
        }
    }

    public int Odgovori(
        int[] pitanje,
        out int crvenih, out int zutih)
    {
        var brOdg = new int[brMesta + 1, brMesta + 1];
        int maxOdg = 0;
        var najOdg = new List<Tuple<int, int>>();
        foreach (var zadato in moguce)
        {
            var odg = Oceni(pitanje, zadato);
            brOdg[odg.Item1, odg.Item2]++;
            if (maxOdg < brOdg[odg.Item1, odg.Item2])
            {
                maxOdg = brOdg[odg.Item1, odg.Item2];
                najOdg.Clear();
            }
            if (maxOdg == brOdg[odg.Item1, odg.Item2] && odg.Item1 != brMesta)
                najOdg.Add(odg);
        }
        if (najOdg.Count == 0)
        {
            crvenih = brMesta;
            zutih = 0;
            return 1;
        }
        int k = rnd.Next(najOdg.Count);
        crvenih = najOdg[k].Item1;
        zutih = najOdg[k].Item2;
        int n = 0;
        for (int i = 0; i < moguce.Count; i++) 
        {
            var odg = Oceni(pitanje, moguce[i]);
            if (odg.Item1 == crvenih && odg.Item2 == zutih)
            {
                moguce[n] = moguce[i];
                n++;
            }
        }
        moguce.RemoveRange(n, moguce.Count-n);
        return n;
    }
    private Tuple<int, int> Oceni(
        int[] pitanje, int[] zadato)

    {
        bool[] uparenoZadato = new bool[brMesta];
        bool[] uparenoPitanje = new bool[brMesta];
        int crvenih = 0, zutih = 0;
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
        return new Tuple<int, int>(crvenih, zutih);
    }
}

class Program
{
    static void Main(string[] args)
    {
        int brMesta = 3;
        int brVrednosti = 4;
        MasterMind game = new MasterMind(brMesta, brVrednosti);
        bool kraj = false;
        int[] pitanje = new int[brMesta];
        int crvenih = 0, zutih = 0;
        while (!kraj)
        {
            string[] s = Console.ReadLine().Split();
            for (int i = 0; i < brMesta; i++)
            {
                pitanje[i] = int.Parse(s[i]);
            }
            int n = game.Odgovori(pitanje, out crvenih, out zutih);
            Console.WriteLine("        {0} {1} ({2})", crvenih, zutih, n);
            if (crvenih == brMesta)
                kraj = true;
        }
    }
}
/*
2 mesta 3 vrednosti se pogadja iz 3 (igra: 12->1:0(4), pa 13 razdvaja)      <== uvodna
3 mesta 2 vrednosti se pogadja iz 3 (igra: 112->2:0(3), pa 212 razdvaja)
3 mesta 4 vrednosti se pogadja iz 4 (igra: 112->1:0(17), 134->0:1(4) itd.)  <== lep izazov

3 mesta 5 vrednosti se pogadja iz 5 (proveriti da li moze iz 4)             <== tesko za punu analizu
2/4->4, 3/3->4,  su lakse 
*/
