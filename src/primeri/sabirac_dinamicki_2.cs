// Klasa dinamički sabirač je klasa koja se ponaša kao 
// niz, ali ima i dodatni metod sa zbir segmenta.
// Implementacija ove klase je složenija nego u prethodnom
// primeru, ali je računanje zbira znatno efikasnije.
// Detalji implementacije namerno nisu objašnjeni, poenta je da se klasa
// koristi na isti način pre i posle ovog unapređenja, pa je dovoljno
// da se razume šta klasa radi (čak i ako se ne razume kako radi).
public class DinamickiSabirac
{
    private int n; // dužina niza
    private int pomakPocetka;
    private int[] a;

    // konstruktor - niz je opisan svojom dužinom
    public DinamickiSabirac(int duzina)
    {
        n = duzina;
        pomakPocetka = 1;
        while (duzina > 0) { duzina >>= 1; pomakPocetka <<= 1; }
        a = new int[2 * pomakPocetka];
    }

    // indekser i dalje služi za pristup elementima niza
    // semantika je ista kao u prethodnoj implementaciji
    public int this[int i]
    {
        get
        {
            if (0 <= i && i < n)
                return a[pomakPocetka + i];
            else 
                return a[-1]; // baca izuzetak (izaziva pucanje programa)
        }
        set
        {
            int k = pomakPocetka + i;
            a[k] = value;
            while (k > 1)
            {
                int k1 = k ^ 1;
                int kPola = k >> 1;
                a[kPola] = a[k] + a[k1];
                k = kPola;
            }
        }
    }

    // metod i dalje vraća zbir traženih elemenata (način
    // upotrebe i efekti su isti kao u prethodnoj implementaciji)
    // od - pozicija od koje se sabira
    // koliko - broj elemenata koji se sabiraju
    public int Zbir(int od, int koliko)
    {
        int poc = od + pomakPocetka;
        int zbir = 0;
        while (koliko > 0)
        {
            if ((poc & 1) > 0) { zbir += a[poc]; poc++; koliko--; }
            if ((koliko & 1) > 0) { zbir += a[poc + koliko - 1]; koliko--; }
            poc >>= 1;
            koliko >>= 1;
        }
        return zbir;
    }
}
