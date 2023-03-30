Апстрактни методи
=================

У овој лекцији:

- Шта је апстрактан метод, а шта апстрактна класа
- Пример употребе апстрактне класе
- Апстрактне класе као облик динамичког полиморфизма

У многим ситуацијама базну класу планирамо да наследимо са више изведених класа, од којих свака 
одређени метод реализује на свој начин. Када није природно да се претпостави неко подразумевано 
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

Могућност да метод у базној класи не дефинишемо постоји. Такав метод, који нема своју дефиницију, 
већ само декларацију (ниво приступа, име и листу параметара) називамо **апстрактан метод**.
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
``virtual``, а у класама између може да се не појављује, или да буде означен речју ``override``.

Пример - површине и запремине
-----------------------------

У примеру који следи постоје две хијерархије класа. Базна класа прве хијерархије је апстрактна класа 
``Figura2D``, из које су изведене класе ``Krug`` и ``Ntougao``. Базна класа друге хијерархије је 
апстрактна класа ``Figura3D``, из које су изведене класе ``SpicastaFigura`` и ``StubastaFigura``.


.. activecode:: klase_povrsine_zapremine
    :passivecode: true

    using System;
    internal class Program
    {
        public abstract class Figura2D
        {
            public abstract double Povrsina();
            public abstract double Obim();
            public abstract double ROpisanogKruga();
            public abstract double RUpisanogKruga();

        }

        public class Krug : Figura2D
        {
            private double r;
            public Krug(double r0) { r = r0; }
            public override double Povrsina() { return r * r * Math.PI; }
            public override double Obim() { return 2 * r * Math.PI; }
            public override double ROpisanogKruga() { return r; }
            public override double RUpisanogKruga() { return r; }
        }
        public class Ntougao : Figura2D
        {
            private double a;
            private int n;
            public Ntougao(double a0, int n0) { a = a0; n = n0; }
            public override double Povrsina() { return n * 0.5 * a * RUpisanogKruga(); }
            public override double Obim() { return n * a; }
            public override double ROpisanogKruga() { return 0.5 * a / Math.Sin(Math.PI / n); }
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
                double r = osnova.ROpisanogKruga();
                double b = osnova.Povrsina();
                double m = osnova.Obim() * visina; // omotac
                return b + b + m;
            }
        }
        static void Main(string[] args)
        {
            Figura2D kr = new Krug(5);
            Figura2D p = new Ntougao(6, 4); // kvadrat stranice 6

            Figura3D kupa1 = new SpicastaFigura(kr, 12);
            Figura3D pir1 = new SpicastaFigura(p, 10);
            Figura3D valjak1 = new StubastaFigura(kr, 12); // valjak
            Figura3D priz1 = new StubastaFigura(p, 10); // prizma
            Console.WriteLine("Kupa: P={0:0.00}, V={1:0.00}", kupa1.Povrsina(), kupa1.Zapremina());
            Console.WriteLine("Piramida: P={0:0.00}, V={1:0.00}", pir1.Povrsina(), pir1.Zapremina());
            Console.WriteLine("Valjak: P={0:0.00}, V={1:0.00}", valjak1.Povrsina(), valjak1.Zapremina());
            Console.WriteLine("Prizma: P={0:0.00}, V={1:0.00}", priz1.Povrsina(), priz1.Zapremina());
        }
    }

Прогрм исписује

.. code::

    Kupa: P=282.74, V=314.16
    Piramida: P=161.28, V=120.00
    Valjak: P=534.07, V=942.48
    Prizma: P=312.00, V=360.00
