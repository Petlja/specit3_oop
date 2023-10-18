using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class TaskSetter
{
    protected Random rnd = new Random();
    
    // inicijalizacija igre na intervalu od 1 do n
    abstract public void Init(int n);
    
    // vrednuj upit 'da li je to broj x'
    // ako je x premali, vrati negativnu vrednost
    // ako je x pogodak, vrati nulu
    // ako je x preveliki, vrati pozitivnu vrednost
    abstract public int Evaluate(int x);
}

public class FriendlyTaskSetter : TaskSetter
{
    private int low, high; // granice intervala

    // inicijalizacija igre na intervalu od 1 do n
    public override void Init(int n)
    {
        low = 1;
        high = n;
    }

    // prihvata se svaki upit koji ima smisla
    public override int Evaluate(int x)
    {
        if (x < low) return -1;
        if (x > high) return 1;
        return 0; 
    }
}

// klasa koja omogućava uobičajeno igranje igre
public class RandomTaskSetter : TaskSetter
{
    private int max, target;

    // inicijalizacija igre na intervalu od 1 do n
    public override void Init(int n)
    {
        max = n;
        target = rnd.Next(n) + 1;
    }

    public override int Evaluate(int x)
    {
        if (x < target) return -1;
        if (x > target) return 1;
        return 0;
    }
}

// klasa protiv koje je najteže pogoditi 'zamišljeni' broj
public class HarshTaskSetter : TaskSetter
{
    // granice intervala koje su u skladu sa prethodnim odgovorima
    private int low, high; 

    // inicijalizacija igre na intervalu od 1 do n
    public override void Init(int n)
    {
        low = 1;
        high = n;
    }

    // evaluacija koja na svaki upit daje najnepovoljniji odgovor
    // koji je u skladu sa prethodnim odgovorima
    public override int Evaluate(int x)
    {
        if (x < low) return -1;
        if (x > high) return 1;
        if (low == high) return 0;
        if (x - low < high - x) { low = x+1; return -1; }
        if (x - low > high - x) { high = x-1; return 1; }
        if (rnd.Next(2) == 0) { low = x+1; return -1; }
        else { high = x-1; return 1; }
    }
}

class Program
{
    public static void Main(String[] args)
    {
        // ispisujemo meni, tj. opcije između kojih se bira
        Console.Write("1 za prijateljski, 2 za slucajni, 3 za strogi (1/2/3)? ");
        int taskSetterKind = int.Parse(Console.ReadLine());
        
        // kreiramo odgovarajući objekat za postavljanje zadataka
        TaskSetter ts = null;
        if (taskSetterKind == 1) ts = new FriendlyTaskSetter();
        else if (taskSetterKind == 2) ts = new RandomTaskSetter();
        else ts = new HarshTaskSetter();

        // biramo slučajnu gornju granicu
        Random rnd = new Random();
        int[] maxima = new int[] { 3, 7, 15, 31, 63, 127 };
        int max = maxima[rnd.Next(maxima.Length)];

        // inicijalizujemo igru i nudimo igrača da pogađa dok ne pogodi
        bool guessed = false;
        int guessCnt = 0;
        ts.Init(max);
        Console.WriteLine("Pogadjas broj od 1 do {0}", max);
        while (!guessed)
        {
            guessCnt++;
            Console.Write("Pokusaj br. {0}, pogadjaj ", guessCnt);
            int x = int.Parse(Console.ReadLine());
            int ans = ts.Evaluate(x);
            if (ans < 0) Console.WriteLine("moj broj je veci");
            else if (ans > 0) Console.WriteLine("moj broj je manji");
            else { Console.WriteLine("bravo, pogodak"); guessed = true; }
        }
    }
}
