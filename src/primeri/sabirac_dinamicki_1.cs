// klasa dinamički sabirač je klasa koja se ponaša kao 
// niz, ali ima i dodatni metod za zbir segmenta
public class DinamickiSabirac
{
    private int n; // dužina niza
    private int[] a; // elementi niza

    // konstruktor - niz je opisan svojom dužinom
    public DinamickiSabirac(int duzina)
    {
        n = duzina;
        a = new int[n];
    }

    // indekser omogućava pristup elementima niza
    public int this[int i]
    {
        get { return a[i]; }
        set { a[i] = value; }
    }

    // metod koji vraća zbir traženih elemenata 
    // od - pozicija od koje se sabira
    // koliko - broj elemenata koji se sabiraju
    public int Zbir(int od, int koliko)
    {
        int zbir = 0;
        for (int i = od; i < od + koliko; i++)
            zbir += a[i];
        return zbir;
    }
}
