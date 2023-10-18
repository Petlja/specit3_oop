using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quiz
{
    // pitanje sa upisivanjem odgovora
    public class FillInTheBlanksQuestion : Question
    {
        string userAnswer; // odgovor korisnika
        string correctAnswer; // tačan odgovor
        int cursorPosition = 0; // pozicija kursora u otkucanom tekstu

        private FillInTheBlanksQuestion()
        {
            userAnswer = "";
            cursorPosition = 0;
        }

        // pomoćni metod za testiranje i kreiranje fajla sa pitanjima,
        // tako da taj fajl kasnije služi kao uzor za formiranje drugih fajlova
        public static FillInTheBlanksQuestion Sample()
        {
            FillInTheBlanksQuestion q = new FillInTheBlanksQuestion()
            {
                questionText = "Upisi rec koja nedostaje: ko rako rani, dve srece ",
                userAnswer = "",
                correctAnswer = "^[Gg]rabi|GRABI$",
                cursorPosition = 0,
                points = 1
            };
            return q;
        }

        // učitavanje jednog pitanja iz tekstualnog ulaznog toka
        public static new Question FromStream(StreamReader sr)
        {
            FillInTheBlanksQuestion q = new FillInTheBlanksQuestion();
            q.questionText = sr.ReadLine();
            q.correctAnswer = sr.ReadLine();
            q.points = int.Parse(sr.ReadLine());
            return q;
        }

        // ispisivanje pitanja u fajl
        override public string ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FITB"); sb.Append("\n");
            sb.Append(questionText); sb.Append("\n");
            sb.Append(correctAnswer); sb.Append("\n");
            sb.Append(points); sb.Append("\n");
            return sb.ToString();
        }

        // prikazivanje pitanja na ekranu
        override public void Display()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(questionText);
            Console.WriteLine();
            Console.Write("Odgovor: {0} ", userAnswer);
            int x, y;
            (x, y) = Console.GetCursorPosition();
            Console.SetCursorPosition(x + cursorPosition - userAnswer.Length - 1, y);
        }

        // reakcija na pritisnut taster
        public override void HandleInput(ConsoleKeyInfo ki)
        {
            int n = userAnswer.Length;
            switch (ki.Key)
            {
                case ConsoleKey.Backspace:
                    // brisanje karaktera levo od kursora
                    if (cursorPosition > 0)
                    {
                        userAnswer = userAnswer.Remove(cursorPosition - 1, 1);
                        cursorPosition--;
                    }
                    break;
                case ConsoleKey.Delete:
                    // brisanje karaktera desno od kursora
                    if (cursorPosition < n)
                    {
                        userAnswer = userAnswer.Remove(cursorPosition, 1);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    // pomeranje kursora levo
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    // pomeranje kursora desno
                    if (cursorPosition < n)
                    {
                        cursorPosition++;
                    }
                    break;
                default:
                    if (char.IsLetterOrDigit(ki.KeyChar))
                    {
                        // upisivanje otkucanog slova
                        userAnswer = userAnswer.Insert(cursorPosition, ki.KeyChar.ToString());
                        cursorPosition++;
                    }
                    break;
            }
        }

        // vraća vrednost izabranog odgovora u poenima
        public override int Evaluate()
        {
            Regex rCorrectAnswer = new Regex(correctAnswer);
            return (rCorrectAnswer.IsMatch(userAnswer)) ? points : 0;
            
            // alternativa bez upotrebe regularnih izraza
            //return (userAnswer == correctAnswer) ? points : 0;
        }
    }
}
