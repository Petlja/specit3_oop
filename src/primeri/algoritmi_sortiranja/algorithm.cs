using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SortingAlgorithms
{
    // apstraktna klasa Algorithm je bazna za sve 
    // implementirane algoritme sortiranja
    public abstract class Algorithm
    {
        // metod prikazivanja koji će svaki algoritam da definiše
        // kao poziv drugog istoimenog metoda sa specifičnim parametrima
        abstract public void Display(Graphics g);

        // metod koji nabraja razmene elemenata
        abstract public IEnumerable<int> DoSortingStep();

        // metodom SetWindow se zadaje pravougaoni deo klijentskog 
        // prozora forme, koji je na raspolaganju ovom algoritmu
        public void SetWindow(float l, float t, float w, float h)
        {
            wndTop = t;
            wndLeft = l;
            wndWidth = w;
            wndHeight = h;
        }
        
        // da li je algoritam završio sa radom
        public bool Finished { get { return finished; } }

        protected int[] a; // niz koji se sortira
        protected string name; // naziv algoritma (za prikaz)
        protected int numSwaps; // broj izvršenih razmena (za prikaz)
        protected bool finished; // da li je algoritam završio sa radom

        // najveći element niza koji se sortira (potreban radi određivanja
        // stvarne visine stubića koji predstavljaju elemente niza)
        private int aMax;

        // pravougaoni deo forme u kome ovaj algoritam crta
        private float wndLeft, wndTop, wndWidth, wndHeight;

        private Color color; // boja stubića za ovaj algoritam
        private Color finishColor; // boja stubića po završetku algoritma

        // konstruktor - algoritam je zadat nizom, bojama koje se koriste i nazivom
        protected Algorithm(int[] _a, Color c, Color fc, string n) 
        {
            a = (int[])_a.Clone();
            color = c;
            finishColor = fc;
            name = n;

            aMax = a.Max(); // maksimum potreban zbog prikaza
            numSwaps = 0;
        }
        
        protected void Display(Graphics g, List<Tuple<int, int>> ranges)
        {
            float h = wndHeight - 10; // 10 piksela za dodatno crtanje,
            // koje je specifično i različito za svaki algoritam

            float barWidth = wndWidth / (float)a.Length; // širina stubića
            
            // koeficijent kojim se množi vrednost elementa 
            // da se dobije visina njegovog stubića
            float k = h / (float)aMax;
            
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(finished ? finishColor : color);

            // stubići
            for (int i = 0; i < a.Length; i++)
            {
                float barHeight = a[i] * k;
                float x = wndLeft + i * barWidth;
                float y = wndTop + h - barHeight;
                g.FillRectangle(brush, x, y, barWidth, barHeight);
                g.DrawRectangle(pen, x, y, barWidth, barHeight);
            }

            // naziv algoritma i broj koraka
            brush = new SolidBrush(Color.Black);
            string s = string.Format("{0}:\n{1} korak(a)", name, numSwaps);
            g.DrawString(s, new Font("Arial", 16), brush, wndLeft + 10, wndTop + 10);
            g.DrawRectangle(pen, wndLeft, wndTop, wndWidth, wndHeight);

            // specifični opsezi koji se prikazuju ispod stubića
            if (!finished)
            {
                float y = wndTop + h;
                foreach (var r in ranges)
                {
                    float x = wndLeft + r.Item1 * barWidth;
                    float w = wndLeft + r.Item2 * barWidth - x;
                    g.FillRectangle(brush, x, y, w, 10);
                }
            }
        }
        
        // razmena mesta dva elementa niza
        protected int Swap(int p1, int p2)
        {
            int tmp = a[p1];
            a[p1] = a[p2];
            a[p2] = tmp;
            numSwaps++;
            return 0;
        }
    }
}
