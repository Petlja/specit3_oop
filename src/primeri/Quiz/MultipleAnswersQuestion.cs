using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    public class MultipleAnswersQuestion : Question
    {
        string[] answers;
        bool[] selectedAnswers;
        bool[] correctAnswers;
        int cursorPosition;
        private MultipleAnswersQuestion ()
        {
            cursorPosition = 0;
        }
        public static MultipleAnswersQuestion Sample()
        {
            MultipleAnswersQuestion q = new MultipleAnswersQuestion();
            q.questionText = "Koji izrazi su jednaki 4? (zaokruzi sve tacne odgovore)";
            q.points = 1;
            q.answers = new string[] { "3+3", "3+2", "3+1", "2+2" };
            q.selectedAnswers = new bool[4];
            q.correctAnswers = new bool[4] { false, false, true, true };
            q.cursorPosition = 0;
            return q;
        }
        public static new Question FromStream(StreamReader sr)
        {
            MultipleAnswersQuestion q = new MultipleAnswersQuestion();
            q.questionText = sr.ReadLine();
            int n = int.Parse(sr.ReadLine());
            q.answers = new string[n];
            q.selectedAnswers = new bool[n];
            q.correctAnswers = new bool[n];
            for (int i = 0; i < n; i++)
                q.answers[i] = sr.ReadLine();
            for (int i = 0; i < n; i++)
            {
                string s = sr.ReadLine();
                q.correctAnswers[i] = (s == "1");
            }
            q.points = int.Parse(sr.ReadLine());
            q.cursorPosition = 0;
            return q;
        }
        override public string ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("MA"); sb.Append("\n");
            sb.Append(questionText); sb.Append("\n");
            sb.Append(answers.Length); sb.Append("\n");

            foreach (string ans in answers)
            {
                sb.Append(ans); sb.Append("\n");
            }
            foreach (bool correctAns in correctAnswers)
            {
                sb.Append(correctAns ? "1" : "0"); sb.Append("\n");
            }
            sb.Append(points); sb.Append("\n");
            return sb.ToString();
        }

        override public void Display()
        {
            Console.SetCursorPosition(0, 0);
            string cursor = "»";
            string noCursor = " ";
            string selection = "[✓]";  // карактер "✓" je \u2713;
            string noSelection = "[ ]";
            Console.WriteLine(questionText);
            char option = 'A';
            for (int i = 0; i < answers.Length; i++)
            {
                string cursorMark = (i == cursorPosition) ? cursor : noCursor;
                string selectionMark = (selectedAnswers[i]) ? selection : noSelection;
                Console.WriteLine("{0} {1} {2}: {3}", cursorMark, selectionMark, option, answers[i]);
                option++;
            }
        }
        public override void HandleInput(ConsoleKeyInfo ki)
        {
            int n = answers.Length;
            switch (ki.Key)
            {
                case ConsoleKey.LeftArrow:
                    cursorPosition = (cursorPosition + n - 1) % n;
                    break;
                case ConsoleKey.UpArrow:
                    cursorPosition = (cursorPosition + n - 1) % n;
                    break;
                case ConsoleKey.RightArrow:
                    cursorPosition = (cursorPosition + 1) % n;
                    break;
                case ConsoleKey.DownArrow:
                    cursorPosition = (cursorPosition + 1) % n;
                    break;
                case ConsoleKey.Spacebar:
                    selectedAnswers[cursorPosition] = !selectedAnswers[cursorPosition];
                    break;
            }
        }

        public override int Evaluate()
        {
            bool allCorrect = true;
            int n = answers.Length;
            for (int i = 0; i < n; i++)
                if (selectedAnswers[i] != correctAnswers[i])
                    allCorrect = false;

            return allCorrect ? points : 0;
        }
    }
}
