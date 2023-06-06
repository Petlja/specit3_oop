using System;
using System.Collections.Generic;

namespace Primer
{
    public class VelikiBroj : IComparable
    {
        private string cifre;
        public VelikiBroj(string s) { cifre  = s; }
        public override string ToString() { return cifre; }
        public int CompareTo(object obj)
        {
            VelikiBroj drugi = (VelikiBroj)obj;
            if (cifre[0] == '-' && drugi.cifre[0] == '-')
                return new VelikiBroj(drugi.cifre.Substring(1)).CompareTo(
                    new VelikiBroj(cifre.Substring(1)));

            if (cifre[0] == '-')
                return -1;

            if (drugi.cifre[0] == '-')
                return 1;

            if (cifre.Length != drugi.cifre.Length)
                return cifre.Length - drugi.cifre.Length;

            return cifre.CompareTo(drugi.cifre);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            List<VelikiBroj> brojevi = new List<VelikiBroj>();
            brojevi.Add(new VelikiBroj("-123"));
            brojevi.Add(new VelikiBroj("-12"));
            brojevi.Add(new VelikiBroj("-125"));
            brojevi.Add(new VelikiBroj("0"));
            brojevi.Add(new VelikiBroj("2500"));
            brojevi.Add(new VelikiBroj("251"));
            brojevi.Add(new VelikiBroj("263"));

            brojevi.Sort();
            foreach (var broj in brojevi)
                Console.WriteLine(broj);
        }
    }
}
