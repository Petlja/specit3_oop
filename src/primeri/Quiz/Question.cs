using System;
using System.IO;

namespace Quiz
{
    public abstract class Question
    {
        protected string questionText;
        protected int points;
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
        abstract public string ToText();
        abstract public void Display();
        abstract public void HandleInput(ConsoleKeyInfo ki);
        abstract public int Evaluate();
    }
}
