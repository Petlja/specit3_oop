Употреба сопствених интерфејса
==============================

Писање интерфејса
-----------------

Писање интерфејса је једноставније од писања апстрактне класе. Као прво, није потребно писати реч 
``abstract`` ни испред имена интерфејса, ни испред имена метода, јер интерфејс је апстрактан по 
дефиницији. Такође, подразумева се да су методи јавни ако се не наведе другачије, па не мора да се 
пише ни реч ``public`` испред имена метода. У класи која имплементира интерфејс није потребно писати 
реч ``override`` испред имена метода, јер ако класа није апстрактна она свакако мора да имплементира 
све методе декларисане у интерфејсу. 

Погледајмо ово на следећем примеру.

Пример - настави низ
^^^^^^^^^^^^^^^^^^^^

.. questionnote::

    Написати програм који омогућава кориснику да игра игру "настави низ". Програм треба да изабере 
    неку правилност по којој се ређају елементи низа и да приказује елементе један по један, а корисник 
    програма може после сваког приказаног елемента да погађа следећи елемент, да тражи још један 
    елемент, да тражи нови низ, или да напусти програм.
    
    Правилности по којима се ређају елементи могу, на пример, да буду:
    
    - следећи број је за :math:`a` већи од претходног (аритметичка прогресија)
    - следећи број је :math:`b` пута већи од претходног (геометријска прогресија)
    - следећи број је збир претходна два (Фибоначијев низ)
    - низ се састоји од две аритметичке прогресије, чији елементи се наизменично појављују
    - следећи број је наизменично за :math:`a` већи и :math:`b` пута већи од претходног
    
    и слично.

За сваки од ових типова низа можемо да напишемо по једну малу класу. Објекту класе можемо у 
конструктору да проследимо параметре за генерисање елемената, а следећи елемент да добијамо позивом 
метода ``long Sledeci()``. Да бисмо након формирања објекта једне од ових класа што једноставније 
користили тај објекат, генерализоваћемо класе и направићемо једноставан интерфејс, који ће оне да 
имплементирају. Ево како би могао да изгледа тај интерфејс и саме класе.

.. activecode:: interfejs_nastavi_niz_klase
    :passivecode: true

    using System;

    interface INizClanPoClan
    {
        public long Sledeci();
    }

    class AritmetickiNiz : INizClanPoClan
    {
        private long a0, d, sled;
        public AritmetickiNiz(long a0, long d)
        {
            this.a0 = a0;
            this.d = d;
            this.sled = a0;
        }
        public long Sledeci() 
        {
            long rez = sled;
            sled += d;
            return rez;
        }
    }
    class GeometrijskiNiz : INizClanPoClan
    {
        private long a0, q, sled;
        public GeometrijskiNiz(long a0, long q)
        {
            this.a0 = a0;
            this.q = q;
            this.sled = a0;
        }
        public long Sledeci()
        {
            long rez = sled;
            sled *= q;
            return rez;
        }
    }
    class DveAritmProgresije : INizClanPoClan
    {
        private long sledA, sledB, da, db;
        private bool naReduJeA;
        public DveAritmProgresije(long a0, long da, long b0, long db)
        {
            this.sledA = a0; this.da = da;
            this.sledB = b0; this.db = db;
            naReduJeA = true;
        }
        public long Sledeci()
        {
            long rez;
            if (naReduJeA) { rez = sledA; sledA += da; }
            else { rez = sledB; sledB += db; }
            naReduJeA = !naReduJeA;

            return rez;
        }
    }
    class FibonacijevNiz : INizClanPoClan
    {
        private long sledeci1, sledeci2;
        public FibonacijevNiz(long a0, long a1)
        {
            this.sledeci1 = a0;
            this.sledeci2 = a1;
        }
        public long Sledeci()
        {
            long rez = sledeci1;
            sledeci1 = sledeci2;
            sledeci2 = sledeci1 + rez;
            return rez;
        }
    }

    class NaizmenicnoPlusPuta: INizClanPoClan
    {
        private long sled, d, q;
        private bool naReduJePlus;
        public NaizmenicnoPlusPuta(long a0, long d, long q)
        {
            this.sled = a0; 
            this.d = d;
            this.q = q;
            naReduJePlus = true;
        }
        public long Sledeci()
        {
            long rez = sled;
            sled = naReduJePlus ? sled + d : sled * q;
            naReduJePlus = !naReduJePlus;
            return rez;
        }
    }

Пре него што пређемо на програмирање игре, можемо помоћу следеће класе ``Program`` да тестирамо 
написане класе за генерисање елемената низова.


.. activecode:: interfejs_nastavi_niz_test
    :passivecode: true

    class Program
    {
        static void Main(string[] args)
        {
            INizClanPoClan[] nizovi = new INizClanPoClan[] 
            {
                new AritmetickiNiz(10, 20),
                new GeometrijskiNiz(3, 2),
                new FibonacijevNiz(1, 1),
                new DveAritmProgresije(1, 3, 100, -2),
                new NaizmenicnoPlusPuta(2, 3, 2)
            };

            foreach (var niz in nizovi)
            {
                for (int i = 0; i < 10; i++)
                    Console.Write("{0,7}", niz.Sledeci());

                Console.WriteLine();
            }
        }
    }

Овај програм исписује

.. code::

     10     30     50     70     90    110    130    150    170    190
      3      6     12     24     48     96    192    384    768   1536
      1      1      2      3      5      8     13     21     34     55
      1    100      4     98      7     96     10     94     13     92
      2      5     10     13     26     29     58     61    122    125
     
Овим се уверавамо да да свака класа враћа баш оне елементе које очекујемо. Ово би било теже 
проверити главним програмом, јер се у њему користи генератор случајних бројева, па не бисмо знали 
када је која класа инстанцирана. Чак и када бисмо препознавали тип низа током тестирања, било би 
потребно да сачекамо да се свака класа инстанцира бар по једном, чиме тестирање постаје спорије и 
неудобније. 

На крају, ево и класе ``Program`` која омогућава играње игре. 

.. activecode:: interfejs_nastavi_niz_igra
    :passivecode: true

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dobijaces redom clanove nekog pravilnog niza,");
            Console.WriteLine("pokusaj da pogodis sledeci element");
            Console.WriteLine("\tPritisni 'Enter' za novi element istog niza");
            Console.WriteLine("\tPritisni '-' i 'Enter' za novi niz");
            Console.WriteLine("\tPritisni '--' i 'Enter' za izlazak iz programa");
            bool kraj = false;
            string unos = "";
            Random rnd = new Random();
            while (!kraj)
            {
                if (unos == "--")
                    break;

                Console.WriteLine("Pocinje novi niz");
                bool pogodio = false;
                INizClanPoClan niz = null;
                int vrstaNiza = rnd.Next(5); // biramo jedan od 5 tipova niza
                switch (vrstaNiza)
                {
                    case 0:
                        niz = new AritmetickiNiz(rnd.Next(1, 10), rnd.Next(3, 9));
                        break;
                    case 1:
                        niz = new GeometrijskiNiz(rnd.Next(1, 5), rnd.Next(2, 5));
                        break;
                    case 2:
                        long a1 = rnd.Next(1, 4);
                        long a2 = rnd.Next((int)a1, 6);
                        niz = new FibonacijevNiz(a1, a2);
                        break;
                    case 3:
                        long db = rnd.Next(-3, 3);
                        if (db == 0) db++;
                        niz = new DveAritmProgresije(rnd.Next(3, 7),
                            rnd.Next(2, 5), rnd.Next(45, 51), db);
                        break;
                    case 4:
                        niz = new NaizmenicnoPlusPuta(rnd.Next(1, 10),
                            rnd.Next(3, 7), rnd.Next(2, 5));
                        break;
                }
                long novi = niz.Sledeci();
                while (!pogodio)
                {
                    Console.Write("Novi element je {0}, pogadjaj sledeci ", novi);
                    novi = niz.Sledeci();

                    unos = Console.ReadLine();
                    if (unos == "") // Enter
                        continue;

                    if (unos == "-" || unos == "--")
                        break;

                    pogodio = (long.Parse(unos) == novi);
                }
                if (pogodio)
                    Console.WriteLine("Bravo!");
                else if (unos == "-")
                {
                    Console.Write("Steta, evo ti jos nekoliko elemenata: {0} ", novi);
                    for (int i = 0; i < 5; i++)
                        Console.Write("{0,7}", niz.Sledeci());

                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }

Приметимо да објекат ``niz`` након формирања користимо сасвим једноставно, не знајући (у том делу 
програма) који тип низа је у питању. Све што нам је потребно да знамо је да је метод ``Sledeci()`` 
из интерфејса имплементиран и да можемо да га позовемо и добијемо следећи број.

.. comment

    .. activecode:: interfejs_nastavi_niz
        :passivecode: true
        :includesrc: src/primeri/interfejs_nastavi_niz.cs
        
