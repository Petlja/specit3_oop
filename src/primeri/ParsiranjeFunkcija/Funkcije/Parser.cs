using System;

namespace FunctionValue
{
    // svi metodi ove klase su statički, što znači da se ova klasa koristi bez instanciranja
    public class Parser
    {
        static string s;
        static int i;

        // jedini javan metod ove klase, koji služi da na osnovu tekstualnog zapisa neke 
        // funkcije kreira objkat f koji predstavlja tu funkciju
        public static bool Evaluate(string userInput, out Function f, out string errMessage)
        {
            s = userInput;
            i = 0;
            f = null;
            try
            {
                SkipWhitespace();
                f = Expression();
                SkipWhitespace();
                if (i < s.Length)
                    throw new Exception(string.Format("Neocekivan znak na poziciji {0}", i));
            }
            catch (Exception e)
            {
                errMessage = string.Format("Neispravan izraz, {0}", e.Message);
                return false;
            }

            errMessage = "ok";
            return true;
        }

        // preskoči beline u zapisu
        private static void SkipWhitespace()
        {
            while (i < s.Length && (s[i] == ' ' || s[i] == '\t'))
                i++;
        }

        // vraća nisku slova koja počinje na poziciji i
        private static string Name()
        {
            int i0 = i;
            while (i < s.Length && char.IsLetterOrDigit(s[i]))
                i++;

            return s.Substring(i0, i - i0);
        }
        
        // vraća funkciju predstavljenu izrazom koji počinje na poziciji i
        private static Function Expression()
        {
            Function f = Term();
            SkipWhitespace();
            while (i < s.Length && (s[i] == '+' || s[i] == '-'))
            {
                if (s[i] == '+')
                {
                    i++;
                    SkipWhitespace();
                    f = new Sum(f, Term());
                }
                else if (s[i] == '-')
                {
                    i++;
                    SkipWhitespace();
                    f = new Diff(f, Term());
                }
            }
            return f;
        }
        
        // vraća funkciju predstavljenu termom koji počinje na poziciji i
        private static Function Term()
        {
            Function f = Factor();
            SkipWhitespace();
            while (i < s.Length && (s[i] == '*' || s[i] == '/'))
            {
                if (s[i] == '*')
                {
                    i++;
                    SkipWhitespace();
                    f = new Product(f, Factor());
                }
                else if (s[i] == '/')
                {
                    i++;
                    SkipWhitespace();
                    f = new Quotient(f, Factor());
                }
            }
            return f;
        }
        
        // vraća funkciju predstavljenu faktorom koji počinje na poziciji i
        private static Function Factor()
        {
            if (s[i] >= '0' && s[i] <= '9')
                return new Constant(Number());
            else if (char.IsLetter(s[i]))
            {
                string token = Name();
                SkipWhitespace();
                if (token.ToLower() == "x")
                    return new Variable();

                if (i == s.Length || s[i] != '(')
                    throw new Exception(string.Format("Nedostaje '(' na poziciji {0}", i));
                i++;
                SkipWhitespace(); 
                Function a = Expression();
                SkipWhitespace();
                if (i == s.Length || s[i] != ')')
                    throw new Exception(string.Format("Nedostaje ')' na poziciji {0}", i));
                i++;
                switch (token.ToLower())
                {
                    case "sin":
                        return new Sine(a);
                    case "cos":
                        return new Cosine(a);
                    case "tg":
                        return new Tangent(a);
                    case "ctg":
                        return new Cotangent(a);
                    case "exp":
                        return new Exponential(a);
                    case "ln":
                        return new NaturalLog(a);
                    case "log":
                        return new Log10(a);
                    case "sqr":
                        return new Sqr(a);
                    case "sqrt":
                        return new Sqrt(a);
                }
            }
            else if (s[i] == '(')
            {
                i++; // '('
                SkipWhitespace();
                Function f = Expression();
                SkipWhitespace();
                if (i == s.Length || s[i] != ')')
                    throw new Exception(string.Format("Nedostaje ')' na poziciji {0}", i));
                i++;
                return f;
            }
            throw new Exception(string.Format("Neocekivan znak na poziciji {0}", i));
        }
        
        // vraća broj koji počinje na poziciji i
        private static double Number()
        {
            double x = 0;
            while (i < s.Length && char.IsDigit(s[i]))
            {
                x = x * 10 + s[i] - '0';
                i++;
            }
            if (i < s.Length && s[i] == '.')
            {
                i++;
                double w = 1.0;
                while (i < s.Length && s[i] >= '0' && s[i] <= '9')
                {
                    w *= 0.1;
                    x += w * (s[i] - '0');
                    i++;
                }
            }
            return x;
        }
    }
}
