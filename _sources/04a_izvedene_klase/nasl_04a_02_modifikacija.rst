Модификација базне класе
========================

.. comment

    Сакривање чланова базне класе

У примеру производа и намирнице илустровано је како изведена класа може **да прошири** базну 
увођењем нових поља и својстава, а могли смо на исти начин да додамо и нове методе. Видели смо да 
објекат изведене класе у том случају може да се користи на исти начин као и објекат базне класе, 
а може и на неке нове начине. 

Осим што можемо да је проширимо, кроз механизам наслеђивања можемо и да **модификујемо** базну 
класу, тј. да променимо њено понашање. То значи да неке методе и својства који су дефинисани 
у базној класи, можемо да дефнишемо другачије у изведеној класи. Следећи пример показује како 
то можемо да урадимо.


Пример - правоугаоник
---------------------

Претпоставимо да је некоме била потребна класа ``Pravougaonik``, која моделира правоугаоник чије 
су странице паралелне са координатним осама. Такав правоугаоник је потпуно одређен координатама 
једног (нпр. доњег левог) темена и дужинама двеју суседних страница. Према томе, ако темена 
означимо са ``A``, ``B``, ``C``, ``D``, довољно је да правоугаоник од података садржи координате 
``ax, ay`` темена ``A`` и дужину ``w`` и висину ``h`` правоугаоника. 

Интерфејс, тј. јавни део класе могу да чине нпр. методи ``Obim`` и ``Povrisna``, који враћају 
обим и површину правоугаоника, својства ``AX``, ``AY``, ``BX``, ``BY``, ``CX``, ``CY``, 
``DX``, ``DY`` за дохватање координата темена правоугаоника, и на кају својства ``W`` i ``H``, 
за добијање дужине и висине правоугаоника. Та класа је могла да буде написана овако:

.. activecode:: klasa_pravougaonik
    :passivecode: true

    public class Pravougaonik
    {
        protected double w, h;
        protected double ax, ay;
        public Pravougaonik(double w, double h, double ax, double ay)
        {
            this.w = w;
            this.h = h;
            this.ax = ax;
            this.ay = ay;
        }

        public double Obim() { return 2 * w + 2 * h; }
        public double Povrisna() { return w * h; }

        public double W { get { return w; } }
        public double H { get { return h; } }

        public double AX { get { return ax; } }
        public double AY { get { return ay; } }
        public double BX { get { return ax + w; } }
        public double BY { get { return ay; } }
        public double CX { get { return ax + w; } }
        public double CY { get { return ay + h; } }
        public double DX { get { return ax; } }
        public double DY { get { return ay + h; } }
    }

Једино што је у овој класи другачије од класа које смо до сада писали је реч ``protected`` 
испред назива поља која су до сада по правилу била приватна. 

Претпоставимо даље да нам се указала потреба за сличном класом, која допушта да правоугаоник буде 
под углом у односу на координатне осе. За такав правоугаоник, потребно је поред коордианта једног 
темена и дужина страница памтити и нпр. угао :math:`\varphi` између позитивног смера `x` осе и 
странице `AB`. 

.. figure:: ../../_images/rotirani_pravougaonik.png
    :align: center   
    
    Ротирани правоугаоник, задат теменом :math:`A`, дужинама страница :math:`w, h` и углом 
    :math:`\varphi`.

Методи ``Obim`` и ``Povrisna`` нам одговарају у постојећем облику, а исто важи и за својства 
``AX``, ``AY``, ``W`` i ``H``. Део који треба променити су дефиниције својстава ``BX``, ``BY``, 
``CX``, ``CY``, ``DX``, ``DY``.

Ако претпоставимо да су дате координате темена :math:`A` и угао :math:`\varphi`, Формуле за 
израчунавање координата осталих темена можемо да изведемо користећи основне тригонометријске 
једнакости и адиционе формуле:

.. math::

    \begin{align} \\
    B_x &= A_x + w \cdot \cos \varphi \\
    B_y &= A_y + w \cdot \sin \varphi \\
    C_x &= B_x + h \cdot \cos \left( {\varphi + \frac{\pi}{2}} \right) 
         = B_x + h \cdot \left( \cos \varphi \cos \frac{\pi}{2} - \sin \varphi \sin \frac{\pi}{2} \right)
         = B_x - h \cdot \sin \varphi \\
    C_y &= B_y + h \cdot \sin \left( {\varphi + \frac{\pi}{2}} \right) 
         = B_x + h \cdot \left( \sin \varphi \cos \frac{\pi}{2} + \cos \varphi \sin \frac{\pi}{2} \right)
         = B_x + h \cdot \cos \varphi \\
    D_x &= AX + w \cdot \cos \left( {\varphi + \frac{\pi}{2}} \right)
         = A_x + h \cdot \left( \cos \varphi \cos \frac{\pi}{2} - \sin \varphi \sin \frac{\pi}{2} \right)
         = A_x - h \cdot \sin \varphi \\
    D_y &= A_y + w \cdot \sin \left( {\varphi + \frac{\pi}{2}} \right)
         = A_x + h \cdot \left( \sin \varphi \cos \frac{\pi}{2} + \cos \varphi \sin \frac{\pi}{2} \right)
         = A_x + h \cdot \cos \varphi \\
    \end{align}    

Пошто координате темена могу да буду потребне више пута, боље је да уместо угла памтимо његов синус и 
косинус, које израчунавамо само једном, у конструктору класе ``RotiraniPravougaonik``. 

Сада класу ``RotiraniPravougaonik`` можемо да напишемо овако:

.. activecode:: klasa_rotirani_pravougaonik
    :passivecode: true

    public class RotiraniPravougaonik : Pravougaonik
    {
        private double sinUgla;
        private double cosUgla;
        public RotiraniPravougaonik(double a, double b, 
            double ax, double ay, double ugao)
            : base(a, b, ax, ay)
        {
            this.sinUgla = Math.Sin(ugao);
            this.cosUgla = Math.Cos(ugao);
        }
        public new double BX { get { return ax + w * cosUgla; } }
        public new double BY { get { return ay + w * sinUgla; } }
        public new double CX { get { return BX - h * sinUgla; } }
        public new double CY { get { return BY + h * cosUgla; } }
        public new double DX { get { return ax - h * sinUgla; } }
        public new double DY { get { return ay + h * cosUgla; } }
    }

**Сакривање члана базне класе**

Приметимо да смо у "преправљеним" верзијама својстава додали кључну реч ``new`` пре типа 
својства. Тиме истичемо да не желимо да користимо стара својства са истим именима, дефинисана 
у базној класи.

Уколико бисмо изоставили кључну реч ``new`` у овим дефиницијама, компајлер би нам упозорењем 
скренуо пажњу на то да овим дефиницијама онемогућавамо (директну) употребу претходних истоимених 
дефиниција у објектима изведене класе, тј. сакривамо претходне, наслеђене дефиниције. На пример, 
ако бисмо уместо ``public new double BX`` писали само ``public double BX``, добили бисмо овакво 
упозорење:

.. code::

    Warning CS0108 'RotiraniPravougaonik.BX' hides inherited member 
    'Pravougaonik.BX'. Use the new keyword if hiding was intended.

.. infonote::

    Дефинисањем члана у изведеној класи, који се зове исто као неки члан базне класе, онемогућили 
    смо (директну) употребу тог члана базне класе. Каже се и да смо сакрили одговарајућег истоименог 
    члана базне класе. Због тога из класе ``RotiraniPravougaonik`` не можемо да користимо својства 
    ``BX``, ``BY``, ``CX``, ``CY``, ``DX``, ``DY`` базне класе наводећи само њихова имена, али та 
    својства нам нису ни потребна у изведеној класи (она би за ротирани правоугаоник давала 
    неисправне вредности координата). 

.. comment

    Пример употребе сакривеног члана ``n`` из базне класе навођењем "пуног имена" члана
    
    .. activecode:: sakrivanje_imena3
        :passivecode: true
        :includesrc: src/primeri/nasl_sakrivanje_imena3.cs

    .. code::

        A.F: n = 5
        B.F: n = 10
        B.G: n = 5


Да бисмо се уверили да су класе ``RotiraniPravougaonik`` и ``Pravougaonik`` исправно написане, 
увек је добро да се оне испробају. Брзу проверу исправности написаних класа можемо да изведемо 
нпр. помоћу следећег кода:

.. activecode:: testiranje_rotiranih_pravougaonika
    :passivecode: true

    internal class Program
    {
        static void Main(string[] args)
        {
            Pravougaonik p = new Pravougaonik(5, 3, 1, 1);
            Console.WriteLine("Duzina(sirina) je {0}, a visina {1}", 
                p.W, p.H);
            Console.WriteLine("Obim je {0}, a povrsina {1}", 
                p.Obim(), p.Povrisna());
            Console.Write("A({0:0.00}, {1:0.00}), ", p.AX, p.AY);
            Console.Write("B({0:0.00}, {1:0.00}), ", p.BX, p.BY);
            Console.Write("C({0:0.00}, {1:0.00}), ", p.CX, p.CY);
            Console.WriteLine("D({0:0.00}, {1:0.00})", p.DX, p.DY);
            Console.WriteLine();

            RotiraniPravougaonik rp = 
                new RotiraniPravougaonik(4, 2, 3, 3, -Math.PI / 6);
            Console.WriteLine("Duzina(sirina) je {0}, a visina {1}", 
                rp.W, rp.H); // preuzeto iz bazne klase
            Console.WriteLine("Obim je {0}, a povrsina {1}", 
                rp.Obim(), rp.Povrisna()); // preuzeto iz bazne klase
            Console.Write("A({0:0.00}, {1:0.00}), ", 
                rp.AX, rp.AY); // preuzeto iz bazne klase
            Console.Write("B({0:0.00}, {1:0.00}), ", rp.BX, rp.BY); // novo
            Console.Write("C({0:0.00}, {1:0.00}), ", rp.CX, rp.CY); // novo
            Console.WriteLine("D({0:0.00}, {1:0.00})", rp.DX, rp.DY); // novo
        }
    }

Програм исписује 

.. code::
       
    Duzina(sirina) je 5, a visina 3
    Obim je 16, a povrsina 15
    A(1.00, 1.00), B(6.00, 1.00), C(6.00, 4.00), D(1.00, 4.00)

    Duzina(sirina) je 4, a visina 2
    Obim je 12, a povrsina 8
    A(3.00, 3.00), B(6.46, 1.00), C(7.46, 2.73), D(4.00, 4.73)

Лако се проверава да су добијене вредности својстава управо оне које је и требало да добијемо за 
дате аргументе конструктора једног и другог правоугаоника.

.. suggestionnote::

    **Напомена:**
    
    Када на овакав начин модификујемо базну класу, важно је да објектима изведене класе 
    **приступамо искључиво преко референци на ту изведену класу**. У противном, тј. у случају да 
    објекту изведене класе приступимо преко референце на базну класу, можемо да добијемо неисправан 
    резултат! 
    
На пример, ако програму за проверу исправности додамо следећа два реда на сам крај

.. code-block:: csharp

        p = rp;
        Console.WriteLine("B({0:0.00}, {1:0.00})", p.BX, p.BY);

програм би на крају још исписао

.. code::

    B(7.00, 3.00)

што не одговара тачки ``B`` ниједног од два правоугаоника. Заиста, као што смо већ видели, тачка 
``B`` првог правоугаоника има коордианте ``B(6.00, 1.00)`` а другог ``B(6.46, 1.00)``. Уз мало 
додатне анализе, можемо да приметимо да се за податке другог, ротираног правоугаоника извршио 
приступник ``get`` својства ``B`` базне класе, тј. да смо добили коордианте тачке ``B`` као да 
правоугаоник није ротиран. 

У следећој лекцији ћемо видети како да превазиђемо овај проблем.
