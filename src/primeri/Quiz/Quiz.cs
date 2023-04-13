using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    class Quiz
    {
        List<Question> questions;

        public static Quiz Sample()
        {
            Quiz q = new Quiz();
            q.questions = new List<Question>();
            q.questions.Add(Question.Sample("ABC"));
            q.questions.Add(Question.Sample("MA"));
            q.questions.Add(Question.Sample("FITB"));
            q.questions.Add(Question.Sample("Parsons"));
            return q;
        }
        public static Quiz FromStream(StreamReader sr)
        {
            Quiz q = new Quiz();
            int n = int.Parse(sr.ReadLine());
            q.questions = new List<Question>();
            for (int i = 0; i < n; i++)
                q.questions.Add(Question.FromStream(sr));

            return q;
        }
        public string ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(questions.Count);
            sb.Append("\n");
            foreach (Question q in questions)
            {
                sb.Append(q.ToText());
            }
            return sb.ToString();
        }

        public void DoTheQuiz()
        {
            int iQuestion = 0;
            int numQuestions = questions.Count;
            bool questionChanged = true;
            while (true)
            {
                if (questionChanged)
                    Console.Clear();
                
                questionChanged = false;
                questions[iQuestion].Display();
                ConsoleKeyInfo ki = Console.ReadKey(true);
                switch (ki.Key)
                {
                    case ConsoleKey.PageUp:
                        iQuestion = (iQuestion + numQuestions - 1) % numQuestions;
                        questionChanged = true;
                        break;
                    case ConsoleKey.PageDown:
                        iQuestion = (iQuestion + 1) % numQuestions;
                        questionChanged = true;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        int pts = 0;
                        for (int i = 0; i < numQuestions; i++)
                            pts += questions[i].Evaluate();

                        Console.WriteLine("Ukupan broj poena: {0}", pts);
                        return;

                    default:
                        questions[iQuestion].HandleInput(ki);
                        break;
                }
            }
        }
    }
}
