using System;

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
