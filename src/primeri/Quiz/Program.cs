using System;
using System.IO;

namespace Quiz
{

    class Program 
    {
        static void TestWriteSampleQuiz(string path)
        {
            Quiz quiz = Quiz.Sample();
            string s = quiz.ToText();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(s);
            }
        }
        static void TestReadQuiz(string path)
        {
            Quiz quiz = null;
            using (StreamReader sr = new StreamReader(path))
                quiz = Quiz.FromStream(sr);

            string s = quiz.ToText();
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            TestWriteSampleQuiz(@"../../a.txt");
            string path = @"../../a.txt";
            //TestReadQuiz(@"../../a.txt");

            //Console.Write("Unesite putanju do fajla sa pitanjima: ");
            //string path = Console.ReadLine();
            Quiz quiz = null;
            using (StreamReader sr = new StreamReader(path))
                quiz = Quiz.FromStream(sr);

            quiz.DoTheQuiz();
        }
    }
}

