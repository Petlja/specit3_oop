using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quiz
{
    public class FillInTheBlanksQuestion : Question
    {
        string userAnswer;
        string correctAnswer;
        int cursorPosition = 0;
        private FillInTheBlanksQuestion()
        {
            userAnswer = "";
            cursorPosition = 0;
        }
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
        public static new Question FromStream(StreamReader sr)
        {
            FillInTheBlanksQuestion q = new FillInTheBlanksQuestion();
            q.questionText = sr.ReadLine();
            q.correctAnswer = sr.ReadLine();
            q.points = int.Parse(sr.ReadLine());
            return q;
        }
        override public string ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FITB"); sb.Append("\n");
            sb.Append(questionText); sb.Append("\n");
            sb.Append(correctAnswer); sb.Append("\n");
            sb.Append(points); sb.Append("\n");
            return sb.ToString();
        }
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
        public override void HandleInput(ConsoleKeyInfo ki)
        {
            int n = userAnswer.Length;
            switch (ki.Key)
            {
                case ConsoleKey.Backspace:
                    if (cursorPosition > 0)
                    {
                        userAnswer = userAnswer.Remove(cursorPosition - 1, 1);
                        cursorPosition--;
                    }
                    break;
                case ConsoleKey.Delete:
                    if (cursorPosition < n)
                    {
                        userAnswer = userAnswer.Remove(cursorPosition, 1);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorPosition < n)
                    {
                        cursorPosition++;
                    }
                    break;
                default:
                    if (char.IsLetterOrDigit(ki.KeyChar))
                    {
                        userAnswer = userAnswer.Insert(cursorPosition, ki.KeyChar.ToString());
                        cursorPosition++;
                    }
                    break;
            }
        }

        public override int Evaluate()
        {
            Regex rCorrectAnswer = new Regex(correctAnswer);
            return (rCorrectAnswer.IsMatch(userAnswer)) ? points : 0;
            //return (userAnswer == correctAnswer) ? points : 0;
        }
    }
}
