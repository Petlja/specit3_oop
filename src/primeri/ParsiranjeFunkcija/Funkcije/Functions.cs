using System;

namespace FunctionValue
{
    abstract public class Function
    {
        abstract public double Value(double x);
        public static implicit operator Function(double c) { return new Constant(c); }
        public static implicit operator Function(string s) { return new Variable(s); }
    }
    internal class Sine : Function
    {
        private Function a;
        public Sine(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("sin({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Sin(a.Value(x));
        }
    }
    internal class Cosine : Function
    {
        private Function a;
        public Cosine(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("cos({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Cos(a.Value(x));
        }
    }
    internal class Tangent : Function
    {
        private Function a;
        public Tangent(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("tg({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Tan(a.Value(x));
        }
    }
    internal class Cotangent : Function
    {
        private Function a;
        public Cotangent(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("ctg({0})", a);
        }
        override public double Value(double x)
        {
            return 1.0 / Math.Tan(a.Value(x));
        }
    }
    internal class Exponential : Function
    {
        private Function a;
        public Exponential(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("exp({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Exp(a.Value(x));
        }
    }
    internal class NaturalLog : Function
    {
        private Function a;
        public NaturalLog(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("ln({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Log(a.Value(x));
        }
    }
    internal class Log10 : Function
    {
        private Function a;
        public Log10(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("log({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Log10(a.Value(x));
        }
    }
    internal class Sqr : Function
    {
        private Function a;
        public Sqr(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("sqr({0})", a);
        }
        override public double Value(double x)
        {
            return a.Value(x) * a.Value(x);
        }
    }
    internal class Sqrt : Function
    {
        private Function a;
        public Sqrt(Function f)
        {
            a = f;
        }
        override public string ToString()
        {
            return string.Format("sqrt({0})", a);
        }
        override public double Value(double x)
        {
            return Math.Sqrt(a.Value(x));
        }
    }
    internal class Sum : Function
    {
        private Function a, b;
        public Sum(Function f, Function g)
        {
            a = f; b = g;
        }
        override public string ToString()
        {
            return string.Format("({0} + {1})", a, b);
        }
        override public double Value(double x)
        {
            return a.Value(x) + b.Value(x);
        }
    }
    internal class Diff : Function
    {
        private Function a, b;
        public Diff(Function f, Function g)
        {
            a = f; b = g;
        }
        override public string ToString()
        {
            return string.Format("({0} - {1})", a, b);
        }
        override public double Value(double x)
        {
            return a.Value(x) - b.Value(x);
        }
    }

    internal class Product : Function
    {
        private Function a, b;
        public Product(Function f, Function g)
        {
            a = f; b = g;
        }
        override public string ToString()
        {
            return string.Format("({0} * {1})", a, b);
        }
        override public double Value(double x)
        {
            return a.Value(x) * b.Value(x);
        }
    }
    internal class Quotient : Function
    {
        private Function a, b;
        public Quotient(Function f, Function g)
        {
            a = f; b = g;
        }
        override public string ToString()
        {
            return string.Format("({0} / {1})", a, b);
        }
        override public double Value(double x)
        {
            return a.Value(x) / b.Value(x);
        }
    }

    internal class Variable : Function
    {
        private string name;
        public Variable(string s="x") { name = s; }
        override public string ToString() { return name; }
        override public double Value(double x)
        {
            return x;
        }
    }
    internal class Constant : Function
    {
        private double a;

        public Constant(double c) { a = c; }
        override public string ToString() { return a.ToString(); }
        override public double Value(double x)
        {
            return a;
        }
    }
    public class FunctionTester
    {
        public static void DoTest()
        {
            Function x = new Variable();

            Function sin = new Sine(x);
            Function cos = new Cosine(x);
            Function exp = new Exponential(x);
            Function expsin = new Exponential(sin);
            Console.WriteLine("sin(pi/2) = {0}", sin.Value(Math.PI / 2));
            Console.WriteLine("sin(pi) = {0}", sin.Value(Math.PI));
            Console.WriteLine("cos(pi/2) = {0}", cos.Value(Math.PI / 2));
            Console.WriteLine("cos(pi) = {0}", cos.Value(Math.PI));
            Console.WriteLine("exp(1) = {0}", exp.Value(1));
            Console.WriteLine("exp(sin(0)) = {0}", expsin.Value(0));

            Function tri = new Constant(3);
            //Function putaTri = new Product(3, "x"); // implicitna konverzija
            Function putaTri = new Product(tri, x);
            Console.WriteLine("3 x 5 = {0}", putaTri.Value(5));

            Function xInv = new Quotient(1, x);
            Console.WriteLine("1 / 5 = {0}", xInv.Value(5));
        }
    }
}