using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    // pitanje sa ponuđenim odgovorima, gde je samo jedan tačan
    public class ABCQuestion : Question
    {
        string[] answers; // ponuđeni odgovori
        int selectedAnswer; // indeks trenutno izabranog odgovora
        int cursorPosition; // indeks odgovora na kome se nalazi kursor
        int correctAnswer; // indeks tačnog odgovora

        private ABCQuestion()
        {
            selectedAnswer = -1;
            cursorPosition = 0;
        }

        // pomoćni metod za testiranje i kreiranje fajla sa pitanjima,
        // tako da taj fajl kasnije služi kao uzor za formiranje drugih fajlova
        public static ABCQuestion Sample()
        {
            ABCQuestion q = new ABCQuestion()
            {
                questionText = "Koliko je 2+2 ?",
                points = 1,
                answers = new string[] { "3", "4", "5", "6" },
                selectedAnswer = -1,
                cursorPosition = 0,
                correctAnswer = 1
            };
            return q;
        }

        // učitavanje jednog pitanja iz tekstualnog ulaznog toka
        public static new Question FromStream(StreamReader sr)
        {
            ABCQuestion q = new ABCQuestion();
            q.questionText = sr.ReadLine();
            int n = int.Parse(sr.ReadLine());
            q.answers = new string[n];
            for (int i = 0; i < n; i++)
                q.answers[i] = sr.ReadLine();

            q.correctAnswer = int.Parse(sr.ReadLine());
            q.points = int.Parse(sr.ReadLine());

            return q;
        }

        // ispisivanje pitanja u fajl
        override public string ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ABC"); sb.Append("\n");
            sb.Append(questionText); sb.Append("\n");
            sb.Append(answers.Length); sb.Append("\n");
            foreach (string ans in answers)
            {
                sb.Append(ans); sb.Append("\n");
            }
            sb.Append(correctAnswer); sb.Append("\n");
            sb.Append(points); sb.Append("\n");
            return sb.ToString();
        }
        
        // prikazivanje pitanja na ekranu
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
                string selectionMark = (i == selectedAnswer) ? selection : noSelection;
                Console.WriteLine("{0} {1} {2}: {3}", cursorMark, selectionMark, option, answers[i]);
                option++;
            }
        }
        
        // reakcija na pritisnut taster
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
                case ConsoleKey.Spacebar: // izbor odgovora
                    selectedAnswer = cursorPosition;
                    break;
            }
        }

        // vraća vrednost izabranog odgovora u poenima
        public override int Evaluate()
        {
            return (selectedAnswer == correctAnswer) ? points : 0;
        }
    }
}
