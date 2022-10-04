using System;
using System.Collections;
using System.Collections.Generic;

public class SparseArray : IEnumerable<double>
{
    private Dictionary<ulong, double> a = new Dictionary<ulong, double>();
    public double this[ulong i]
    {
        get
        {
            if (a.ContainsKey(i))
                return a[i];
            else
                return 0;
        }
        set { a[i] = value; }
    }

    public IEnumerator GetEnumerator()
    {
        return new SparseArrayEnum(this);
    }

    IEnumerator<double> IEnumerable<double>.GetEnumerator()
    {
        return new SparseArrayEnum(this);
        //throw new NotImplementedException();
    }

}
public class SparseArrayEnum : IEnumerator<double>
{
    public SparseArray _sa;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    uint position = 0;

    public SparseArrayEnum(SparseArray sa)
    {
        _sa = sa;
    }

    public bool MoveNext()
    {
        position++;
        return (position < 1000);
    }

    public void Reset()
    {
        position = 0;
    }

    public void Dispose()
    {
        return;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public double Current
    {
        get
        {
            try
            {
                return _sa[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

}
class Program
{
    static void Main(string[] args)
    {
        SparseArray x = new SparseArray();
        ulong n = 4000000000000;
        x[n]++;
        x[n + 1] = 3;
        Console.WriteLine(x[n]);
        Console.WriteLine(x[n + 1]);
        foreach (var v in x)
            Console.WriteLine(v);
    }
}
