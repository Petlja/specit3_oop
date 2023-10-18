using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    // pitanje u kome treba poređati ponuđene stavke po nekom redu
    public class ParsonsQuestion : Question
    {
        string[] items; // stavke
        bool[] selected; // izabrane stavke
        string userAnswer = ""; // odgovor korisnika
        string correctOrder; // ispravan redosled stavki (tačan odgovor)
        int cursorPosition; // indeks stavke na kojoj se nalazi kursor

        // pomoćni metod za testiranje i kreiranje fajla sa pitanjima,
        // tako da taj fajl kasnije služi kao uzor za formiranje drugih fajlova
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

        // učitavanje jednog pitanja iz tekstualnog ulaznog toka
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

        // ispisivanje pitanja u fajl
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

        // prikazivanje pitanja na ekranu
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

        // reakcija na pritisnut taster
        public override void HandleInput(ConsoleKeyInfo ki)
        {
            int n = userAnswer.Length;
            int i;
            switch (ki.Key)
            {
                case ConsoleKey.Backspace:
                // poništavanje stavke levo od kursora
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                        i = userAnswer[cursorPosition] - 'A';
                        selected[i] = false;
                        userAnswer = userAnswer.Remove(cursorPosition, 1);
                    }
                    break;
                case ConsoleKey.Delete:
                // poništavanje stavke desno od kursora
                    if (cursorPosition < n)
                    {
                        i = userAnswer[cursorPosition] - 'A';
                        selected[i] = false;
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
                    // ubacivanje stavke čije slovo je pritisnuto
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

        // vraća vrednost izabranog odgovora u poenima
        public override int Evaluate()
        {
            return (userAnswer == correctOrder) ? points : 0;
        }
    }
}
