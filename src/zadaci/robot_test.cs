class Program
{
    static void Main(string[] args)
    {
        // proba
        
        // novi robot na polju (2, 3), okrenut ka severu
        Robot r1 = new Robot(2, 3, 'N');

        r1.Napred();  // 1 polje napred
        r1.Nadesno(); // okret nadesno
        r1.Napred(5); // pet polja napred
        Console.WriteLine(r1); // ispis podataka o robotu
    }
}
