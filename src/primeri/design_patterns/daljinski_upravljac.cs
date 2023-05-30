using System;

namespace Primer
{
    public interface IDaljinskiUpravljac
    {
        void Ukljuci();
        void Iskljuci();
    }

    public class DaljinskiUkljucenogTV : IDaljinskiUpravljac
    {
        public void Ukljuci() { Console.WriteLine("TV je vec ukljucen."); }
        public void Iskljuci() { Console.WriteLine("TV je uspesno iskljucen."); }
    }

    public class DaljinskiIskljucenogTV : IDaljinskiUpravljac
    {
        public void Ukljuci() { Console.WriteLine("TV je uspesno ukljucen."); }
        public void Iskljuci() { Console.WriteLine("TV je vec iskljucen."); }
    }

    public class PraviUpravljac : IDaljinskiUpravljac
    {
        private IDaljinskiUpravljac ukljucen = new DaljinskiUkljucenogTV();
        private IDaljinskiUpravljac iskljucen = new DaljinskiIskljucenogTV();
        private IDaljinskiUpravljac aktivanDaljinski;

        public PraviUpravljac() { aktivanDaljinski = iskljucen; }
        public void Ukljuci() { aktivanDaljinski.Ukljuci(); aktivanDaljinski = ukljucen; }
        public void Iskljuci() { aktivanDaljinski.Iskljuci(); aktivanDaljinski = iskljucen; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IDaljinskiUpravljac upravljac = new PraviUpravljac();
            upravljac.Ukljuci();  // TV je uspesno ukljucen.
            upravljac.Ukljuci();  // TV je vec ukljucen.
            upravljac.Iskljuci(); // TV je uspesno iskljucen.
            upravljac.Iskljuci(); // TV je vec iskljucen.
        }
    }
}
