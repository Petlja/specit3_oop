using System;
using System.IO;

namespace Quiz
{
    // apstraktna bazna klasa za sve vrste pitanja
    public abstract class Question
    {
        protected string questionText; // postavka pitanja
        protected int points; // broj poena koje donosi tačan odgovor

        // pomoćni metod za testiranje i kreiranje fajla sa pitanjima,
        // tako da taj fajl kasnije služi kao uzor za formiranje drugih fajlova
        public static Question Sample(string type)
        {
            Question q = null;
            switch (type)
            {
                case "ABC": q = ABCQuestion.Sample(); break;
                case "MA": q = MultipleAnswersQuestion.Sample(); break;
                case "FITB": q = FillInTheBlanksQuestion.Sample(); break;
                case "Parsons": q = ParsonsQuestion.Sample(); break;
            }
            return q;
        }

        // učitavanje jednog pitanja iz tekstualnog ulaznog toka
        public static Question FromStream(StreamReader sr)
        {
            Question q = null;
            string t = sr.ReadLine();
            switch (t)
            {
                case "ABC": q = ABCQuestion.FromStream(sr); break;
                case "MA": q = MultipleAnswersQuestion.FromStream(sr); break;
                case "FITB": q = FillInTheBlanksQuestion.FromStream(sr); break;
                case "Parsons": q = ParsonsQuestion.FromStream(sr); break;
            }
            return q;
        }

        // metodi specifični za pojedine vrste pitanja
        abstract public string ToText();
        abstract public void Display();
        abstract public void HandleInput(ConsoleKeyInfo ki);
        abstract public int Evaluate();
    }
}
