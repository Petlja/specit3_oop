using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class TaskSetter
{
    protected Random rnd = new Random();
    abstract public void Init(int n);
    abstract public int Evaluate(int x);
}

public class FriendlyTaskSetter : TaskSetter
{
    private int low, high;
    public override void Init(int n)
    {
        low = 1;
        high = n;
    }
    public override int Evaluate(int x)
    {
        if (x < low) return -1;
        if (x > high) return 1;
        return 0;
    }
}
public class RandomTaskSetter : TaskSetter
{
    private int max, target;
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
public class HarshTaskSetter : TaskSetter
{
    private int low, high;
    public override void Init(int n)
    {
        low = 1;
        high = n;
    }
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
        Console.Write("1 za prijateljski, 2 za slucajni, 3 za strogi (1/2/3)? ");
        int taskSetterKind = int.Parse(Console.ReadLine());
        
        TaskSetter ts = null;
        if (taskSetterKind == 1) ts = new FriendlyTaskSetter();
        else if (taskSetterKind == 2) ts = new RandomTaskSetter();
        else ts = new HarshTaskSetter();

        Random rnd = new Random();
        int[] maxima = new int[] { 3, 7, 15, 31, 63, 127 };
        int max = maxima[rnd.Next(maxima.Length)];
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
