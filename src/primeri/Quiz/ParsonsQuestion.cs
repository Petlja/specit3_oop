using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    public class ParsonsQuestion : Question
    {
        string[] items;
        bool[] selected;
        string userAnswer = "";
        string correctOrder;
        int cursorPosition;
        public static ParsonsQuestion Sample()
        {
            ParsonsQuestion q = new ParsonsQuestion()
            {
                questionText = "Poredjaj od najmladjeg do najstarijeg",
                items = new string[] { "deda", "dete", "covek" },
                selected = new bool[3],
                userAnswer = "",
                correctOrder = "BCA",
                cursorPosition = 0,
                points = 1
            };
            return q;
        }
        public static new Question FromStream(StreamReader sr)
        {
            ParsonsQuestion q = new ParsonsQuestion();
            q.questionText = sr.ReadLine();
            int n = int.Parse(sr.ReadLine());
            q.items = new string[n];
            q.selected = new bool[n];
            for (int i = 0; i < n; i++)
                q.items[i] = sr.ReadLine();

            q.correctOrder = sr.ReadLine();
            q.points = int.Parse(sr.ReadLine());

            return q;
        }
        override public string ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Parsons"); sb.Append("\n");
            sb.Append(questionText); sb.Append("\n");
            sb.Append(items.Length); sb.Append("\n");
            foreach (string item in items)
            {
                sb.Append(item); sb.Append("\n");
            }
            sb.Append(correctOrder); sb.Append("\n");
            sb.Append(points); sb.Append("\n");
            return sb.ToString();
        }
        override public void Display()
        {
            Console.SetCursorPosition(0, 0);
            string noCursor = " ";
            string selection = "[X]";
            string noSelection = "[ ]";
            Console.WriteLine(questionText);
            char option = 'A';
            string options = "";
            for (int i = 0; i < items.Length; i++)
            {
                string selectionMark = selected[i] ? selection : noSelection;
                Console.WriteLine("{0} {1} {2}: {3}", noCursor, selectionMark, option, items[i]);
                options += option + ", ";
                option++;
            }
            Console.WriteLine("Unesi slova {0}u potrebnom redosledu.", options);
            Console.Write("Odgovor: {0} ", userAnswer);
            int x, y;
            (x, y) = Console.GetCursorPosition();
            Console.SetCursorPosition(x + cursorPosition - userAnswer.Length - 1, y);
        }
        public override void HandleInput(ConsoleKeyInfo ki)
        {
            int n = userAnswer.Length;
            int i;
            switch (ki.Key)
            {
                case ConsoleKey.Backspace:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                        i = userAnswer[cursorPosition] - 'A';
                        selected[i] = false;
                        userAnswer = userAnswer.Remove(cursorPosition, 1);
                    }
                    break;
                case ConsoleKey.Delete:
                    if (cursorPosition < n)
                    {
                        i = userAnswer[cursorPosition] - 'A';
                        selected[i] = false;
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
                    if (char.IsLetter(ki.KeyChar))
                    {
                        char ch = char.ToUpper(ki.KeyChar);
                        i = ch - 'A';
                        if (0 <= i && i < selected.Length && !selected[i])
                        {
                            selected[i] = true;
                            userAnswer = userAnswer.Insert(cursorPosition, ch.ToString());
                            cursorPosition++;
                        }
                    }
                    break;
            }
        }

        public override int Evaluate()
        {
            return (userAnswer == correctOrder) ? points : 0;
        }
    }
}
