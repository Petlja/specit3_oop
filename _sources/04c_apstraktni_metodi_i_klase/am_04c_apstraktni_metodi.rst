Апстрактни методи
=================

У овој лекцији:

- Шта је апстрактан метод, а шта апстрактна класа
- Пример употребе апстрактне класе
- Апстрактне класе као облик динамичког полиморфизма

У многим ситуацијама базну класу планирамо да наследимо са више изведених класа, од којих одређени 
метод свака реализује на свој начин. Када није природно да се претпостави неко подразумевано 
понашање објеката ове хијерархије, које би одговарало већини изведених класа, најбоље би било да 
у базној класи уопште не морамо да дефинишемо метод са датим именом, већ да га само декларишемо 
(најавимо). 

На пример, у програму који омогућава играње шаха, може да нам буде потребно да за дату фигуру 
добијемо све потезе  које она може да одигра. Овде је природно да планирамо базну класу ``Figura`` 
и изведене класе за сваку врсту фигуре. Тако, када нам буде потребно да добијемо све могуће потезе 
једног играча, можемо да за сваку његову фигуру тражимо све потезе те фигуре.

Пошто се свака фигура креће другачије од осталих, не постоји неко "најчешће" кретање, које би 
одговарало већини фигура. Другим речима, не постој добар начин да у базној класи ``Figura`` 
имплементирамо метод који враћа све потезе (неке опште) фигуре.

Могућност да не дефинишемо метод у базној класи постоји. Такав метод, који нема своју дефиницију, 
већ само декларацију (ниво приступа, име, тип и листу параметара) називамо **апстрактан метод**.
Испред апстрактног метода се пише реч ``abstract``. Она компајлеру (и другим програмерима) говори да 
метод није дефинисан и да се очекује да буде дефинисан у класама наследницама.

Када нека класа има бар један апстрактан, недефинисан метод, она није довршена и објекти те класе не 
могу да буду креирани (класа не може да се инстанцира). Таква класа се назива **апстрактна класа** и 
такође се означава речју ``abstract`` на свом почетку. Да би класа која непосредно или посредно (кроз 
хијерархију) наслеђује апстрактну класу могла да има инстанце, неопходно је да у њој (или у класама 
које јој претходе у хијерархији) буду дефинисани сви апстрактни методи базне класе. 

.. infonote::

    Уочимо разлику између апстрактних и виртуелних метода: ако је метод означен као апстрактан, 
    редефинисање (негде касније у хијерархији наслеђивања) је обавезно, а ако је означен као виртуелан, 
    редефинисање је могуће али није обавезно. 


Свако накнадно дефинисање (као и редефинисање) у некој од класа наследница означава се речју ``override``.
Реч ``override`` значи да се метод редефинише у односу на базну класу, а може (и не мора) да се даље 
редефинише у класама наследницама. 

Према томе, да би испред метода могла да се пише реч ``override``, потребно је да на месту где је метод 
уведен у некој од претходних класа у хијерархији наслеђивања буде означен као ``abstract`` или као 
``virtual``, а у класама између може да се не појављује, или (ако се појављује) да буде означен речју 
``override``.


.. infonote::

    Семантика позивања апстрактних и виртуелних метода:
    
    Приликом позива неког апстрактног или виртуелног метода, можемо да замислимо да се полази од класе 
    којој припда објекат чији метод позивамо, и да се кроз хијерархију класа навише (кроз базне класе) 
    тражи најближа постојећа дефиниција позваног метода. Стварни механизам одређивања метода који треба 
    да се изврши није тако спор (мада је спорији од позивања обичних метода), али ми можемо да пронађемо 
    тај метод по описаном правилу.

    
Пример - обими и површине кругова и многоуглова
-----------------------------------------------

У овом примеру написаћемо класе за израчунавање обима и површине круга и правилног многоугла. Круг 
и правилан мноогоугао могу да се изведу из заједничке базне апстрактне класе ``Figura2D`` 
(дводимензиона фигура, тј. фигура у равни), која ће имати апстрактне методе ``Obim`` и ``Povrsina``. 
Изведене класе ће да имплементирају ове методе на одговарајући начин.


.. activecode:: klase_obimi_povrsine
    :passivecode: true

    using System;
    using System.Collections.Generic;

    internal class Program
    {
        public abstract class Figura2D
        {
            public abstract double Povrsina();
            public abstract double Obim();
        }

        public class Krug : Figura2D
        {
            private double r;
            public Krug(double r0) { r = r0; }
            public override double Povrsina() { return r * r * Math.PI; }
            public override double Obim() { return 2 * r * Math.PI; }
        }
        public class Ntougao : Figura2D
        {
            private double a;
            private int n;
            public Ntougao(double a0, int n0) { a = a0; n = n0; }
            public override double Povrsina() 
            {
                double rUpisanogKruga = 0.5 * a / Math.Tan(Math.PI / n);
                return n * 0.5 * a * rUpisanogKruga; 
            }
            public override double Obim() { return n * a; }
        }

        static void Main(string[] args)
        {
            List<Figura2D> figure = new List<Figura2D>();
            figure.Add(new Krug(5));        // krug poluprecnika 5
            figure.Add(new Ntougao(6, 4));  // kvadrat stranice 6

            double p = 0;
            foreach (Figura2D f in figure)
            {
                Console.WriteLine("Figura: Obim={0:0.00}, Povrsina={1:0.00}", f.Obim(), f.Povrsina());
                p += f.Povrsina();
            }

            Console.WriteLine("Ukupna povrsina je {0:0.00}.", p); 
        }
    }

Програм исписује

.. code::

    Figura: Obim=31.42, Povrsina=78.54
    Figura: Obim=24.00, Povrsina=36.00
    Ukupna povrsina je 114.54.

.. suggestionnote::

    У овом примеру поново смо се срели са динамичким полиморфизмом, овај пут оствареним помоћу 
    апстрактне базне класе. Видимо да се инстанциране фигуре користе на исти, униформан начин. 
    Приликом употребе (завршна ``foreach`` петља) не морамо да водимо рачуна о томе која фигура 
    је круг, а која многоугао, али се ипак обим и површина сваке од њих израчунавају правилно, 
    тј. у складу са стварном природом фигуре. 


Пример - површине и запремине тела
----------------------------------

Пример који следи, надовезује се на претходни пример. Сада ћемо да додамо класе за израчунавање 
површине и запремине ваљка, призме, купе и пирамиде (мисли се на прави ваљак, праву купу, праву 
правилну призму и праву правилну пирамиду). Подсетимо се познатих формула за површину и запремину 
набројаних тела.

.. math::

    \begin{align} \\
    &V_{valjka} = BH,               &   P_{valjka} = 2B + M\\
    &V_{prizme} = BH,               &   P_{prizme} = 2B + M\\
    &V_{kupe} = \frac{1}{3}BH,      &   P_{kupe} = B + M\\
    &V_{piramide} = \frac{1}{3}BH,  &   P_{piramide} = B + M\\
    \end{align}

У овим формулама, :math:`B` је ознака за површину основе, а :math:`M` за површину омотача. Овако 
написане, формуле за ваљак и призму су међусобно потпуно исте, а такође и формуле за купу и 
пирамиду. Када још узмено у обзир да се површина омотача и код ваљка и код призме израчунава као 
производ висине и обима основе, долазимо до закључка да ваљак и призму за потребе овог примера 
можемо да представимо истом класом. Та класа треба да садржи само висину као реалан број, и основу 
којој ће да приступа преко референце на класу ``Figura2D``.

|

Површина омотача купе се рачуна као :math:`M = \frac{1}{2} O \cdot s`, где је :math:`O` обим (кружне) 
основе, а :math:`s` дужина изводнице. У случају пирамиде, формула за површину омотача је слична: 
:math:`M = \frac{1}{2} O \cdot h`, где је :math:`O` поново обим (овај пут многоугаоне) основе, а 
:math:`h` је висина бочне стране (апотема). Да ли ова разлика захтева посебне класе за купу и 
пирамиду? Одговор зависи од тога шта од података желимо да памтимо за ова тела. Ако, по угледу на 
ваљак и призму, памтимо само основу и висину, онда изводницу, односно висину бочне стране треба да 
израчунамо. Изводница купе се рачуна по формули :math:`s = \sqrt{r^2 + H^2}`. Висина :math:`h` бочне 
стране пирамиде се рачуна по истој формули, :math:`h = \sqrt{r^2 + H^2}`, с тим да :math:`r` код 
пирамиде означава полупречник круга уписаног у многоугаону основу, а то је величина коју већ 
израчунавамо у класи ``Ntougao`` (погледајте претходни пример, метод ``Povrsina``). Зато је довољно 
да у апстрактну класу ``Figura2D``, додамо и апстрактан метод ``RUpisanogKruga``. Формулу за 
полупречник уписаног круга многоугла већ имамо, а за круг је то сам полупречник круга. 

|

Из ове анализе произилази да су нам довољне следеће две мале хијерархије класа. 

- Базна класа прве хијерархије је апстрактна класа ``Figura2D`` (дводимензиона фигура), из које су изведене 
  класе ``Krug`` и ``Ntougao``. Ове класе су незнатно измењене у однсу на претходни пример.
- Базна класа друге хијерархије је апстрактна класа ``Figura3D``  (тродимензиона фигура), из које су изведене 
  класе ``SpicastaFigura`` (која може да представља купу или пирамиду) и ``StubastaFigura`` (која може да 
  представља ваљак или призму). Тродимензиона фигура садржи дводимензиону фигуру као своју основу.

.. figure:: ../../_images/apstraktne_figure.png
    :align: center   

    Однос класа које се појављују у примеру, са означеним специјализацијама и агрегацијама.



.. activecode:: klase_povrsine_zapremine
    :passivecode: true

    using System;
    using System.Collections.Generic;

    internal class Program
    {
        public abstract class Figura2D
        {
            public abstract double Povrsina();
            public abstract double Obim();
            public abstract double RUpisanogKruga();

        }

        public class Krug : Figura2D
        {
            private double r;
            public Krug(double r0) { r = r0; }
            public override double Povrsina() { return r * r * Math.PI; }
            public override double Obim() { return 2 * r * Math.PI; }
            public override double RUpisanogKruga() { return r; }
        }
        public class Ntougao : Figura2D
        {
            private double a;
            private int n;
            public Ntougao(double a0, int n0) { a = a0; n = n0; }
            public override double Povrsina() { return n * 0.5 * a * RUpisanogKruga(); }
            public override double Obim() { return n * a; }
            public override double RUpisanogKruga() { return 0.5 * a / Math.Tan(Math.PI / n); }
        }

        public abstract class Figura3D
        {
            protected Figura2D osnova;
            protected double visina;
            public Figura3D(Figura2D b, double h)
            {
                osnova = b;
                visina = h;
            }
            public abstract double Zapremina();
            public abstract double Povrsina();
        }
        public class SpicastaFigura : Figura3D // kupa, piramida
        {
            public SpicastaFigura(Figura2D b, double h) : base(b, h) { }
            public override double Zapremina() { return osnova.Povrsina() * visina / 3; }
            public override double Povrsina()
            {
                double r = osnova.RUpisanogKruga();
                double s = Math.Sqrt(r * r + visina * visina); // izvodnica, ili visina bocne strane
                double b = osnova.Povrsina();
                double m = 0.5 * osnova.Obim() * s; // omotac
                return b + m;
            }
        }
        public class StubastaFigura : Figura3D // valjak, prizma
        {
            public StubastaFigura(Figura2D b, double h) : base(b, h) { }
            public override double Zapremina() { return osnova.Povrsina() * visina; }
            public override double Povrsina()
            {
                double b = osnova.Povrsina();
                double m = osnova.Obim() * visina; // omotac
                return b + b + m;
            }
        }
        static void Main(string[] args)
        {
            Figura2D krug = new Krug(5);
            Figura2D mnogougao = new Ntougao(6, 4); // kvadrat stranice 6

            List<Figura3D> figure = new List<Figura3D>();
            figure.Add(new SpicastaFigura(krug, 12)); // kupa
            figure.Add(new SpicastaFigura(mnogougao, 10));  // priamida
            figure.Add(new StubastaFigura(krug, 12)); // valjak
            figure.Add(new StubastaFigura(mnogougao, 10));  // prizma

            double p = 0, v = 0;
            foreach (Figura3D f in figure)
            {
                Console.WriteLine("Figura: P={0:0.00}, V={1:0.00}", f.Povrsina(), f.Zapremina());
                p += f.Povrsina();
                v += f.Zapremina();
            }

            Console.WriteLine("Ukupna povrsina je {0:0.00}, a zapremina {1:0.00}.", p, v); 
        }
    }

Програм исписује

.. code::

    Figura: P=282.74, V=314.16
    Figura: P=161.28, V=120.00
    Figura: P=534.07, V=942.48
    Figura: P=312.00, V=360.00
    Ukupna povrsina je 1290.10, a zapremina 1736.64.

.. suggestionnote::

    Ево још једном динамичког полиморфизма на делу. Поново нема потребе да на месту где се објекти 
    користе водимо рачуна о томе који објекат је које врсте и користимо наредбе гранања. Конкретније, 
    овај пут приликом рачунања површина и запремина фигура не морамо да водимо рачуна о томе која је 
    фигура ког облика. 
    
    
Класе смо написали тако да свака фигура "зна" како се израчунава њена површина, односно запремина. 
Пошто су методи за површину и запремину апстрактни, приликом позивања тих метода узима се у обзир стварна 
природа објеката.

