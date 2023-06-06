using System;

interface IGlasan { public string Zvuk(); }
interface IPokretan { public double Brzina(); }

public class Pas : IGlasan, IPokretan
{
    public string Zvuk() { return "av av"; }
    public double Brzina() { return 36; }
    public override string ToString() { return "pas"; }
}

public class MotornaTestera : IGlasan
{
    public string Zvuk() { return "vruum"; }
    public override string ToString() { return "motorna testera"; }
}

public class Bicikl : IPokretan
{
    public double Brzina() { return 50; }
    public override string ToString() { return "bicikl"; }
}

class Program
{
    static void Main(string[] args)
    {
        Pas p = new Pas();
        MotornaTestera mt = new MotornaTestera();
        Bicikl b = new Bicikl();

        IGlasan[] glasni = { p, mt };
        IPokretan[] pokretni = { p, b };

        Console.WriteLine("Glasovi glasnih:");
        foreach (var glasan in glasni)
            Console.WriteLine("\t{0}: {1}", glasan, glasan.Zvuk());

        Console.WriteLine("Brzine pokretnih:");
        foreach (var pokretan in pokretni)
            Console.WriteLine("\t{0}: {1}Km/h", pokretan, pokretan.Brzina());
    }
}
