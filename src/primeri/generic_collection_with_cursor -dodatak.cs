//...
using System.Text;

public class CollectionWihCursor<T>
{
    //...
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        
        // prikaži sve levo od kursora
        foreach (T x in left)
        {
            sb.Append(x);
            sb.Append(' ');
        }

        // prikaži sve desno od kursora
        for (int i = right.Count - 1; i >= 0; i--)
        {
            sb.Append(right[i]);
            sb.Append(' ');
        }

        return sb.ToString();
    }
}