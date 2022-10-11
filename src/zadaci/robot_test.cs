class Program
{
    static void Main(string[] args)
    {
        // proba
        Robot r1 = new Robot(2, 3, 'N');
        r1.Napred();
        r1.Nadesno();
        r1.Napred(5);
        Console.WriteLine(r1);
    }
}
