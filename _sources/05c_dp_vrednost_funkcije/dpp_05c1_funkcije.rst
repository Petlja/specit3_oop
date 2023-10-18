Вредност функције
=================

Овај пример је знатно већи од претходна два и обухвата неколико пројеката. Најважнији пројекат 
у примеру је библиотека ``Funkcije``,  која омогућава задавање функције у текстуалном облику и 
брзо израчунавање вредности те функције за разне вредности променљиве :math:`x`. Ова библиотека 
се затим користи у различитим апликацијама. 

Прва употреба библиотеке ``Funkcije`` је једноставна конзолна апликација, која табеларно 
приказује вредности задате функције у задатом интервалу. Она уједно служи и за тестирање библиотеке. 

Друга употреба је апликација за цртање графика функције коју унесе корисник. У овој апликацији је 
употребљена и библиотека ``CoordinateConverter``, која омогућава флексибилан приказ координатног 
система и прерачунавање координата из координатног система света у координатни систем екрана и обрнуто.

Трећа употреба је апликација која израчунава приближну вредност променљиве :math:`x` за коју функција 
има вредност нула.

Дакле, у примеру се појављују следећи пројекти: 

- библиотека ``Funkcije``,
- конзолна апликација за тестирање и табелирање вредности функције,
- библиотека ``CoordinateConverter``, која је коришћена и раније,
- графичка апликација за цртање графика произвољне функције,
- конзолна апликација за налажење нула функције.

Класа Function и изведене класе
-------------------------------

Суштина сваке математичке функције је да дефинише правило по коме се дати реалан број :math:`x` 
пресликава у реалан број :math:`f(x)`. Према томе, класа која представља математичку функцију треба 
да има метод попут:

.. code-block:: csharp

    double Value(double x)

...помоћу кога се добија вредност функције за дато :math:`x`. Пошто свака функција ту вредност одређује 
на свој начин, можемо да формирамо апстрактну класу ``Function`` са јавним апстрактним методом 
``Value``, који свака конкретна функција треба да имплементира на свој начин. 

.. code-block:: csharp

    abstract public class Function
    {
        abstract public double Value(double x);
    }

Функције се често компонују у сложене функције, на пример :math:`e^{\sin(x)}`, :math:`\sqrt{x^2+1}` и 
слично. Да бисмо могли да градимо овакве, сложене функције, згодно је да објекат који представља 
функцију садржи референце на друге функције, које се појављују као њени параметри. На пример, ако 
желимо да формирамо објекат који представља функцију :math:`e^{f(x)}` где је :math:`f` нека већ 
креирана функција, згодно је да нова, експоненцијална функција садржи референцу на функцију :math:`f` 
и да је користи за израчунавање своје вредности.

Тако долазимо до прве, поједностављене верзије хијерархије класа за представљање математичких функција:

.. activecode:: ParsiranjeFunkcija_Functions0.cs
    :passivecode: true

    using System;

    namespace FunctionValue
    {
        // bazna klasa za sve podržane funkcije
        public abstract class Function
        {
            abstract public double Value(double x);
        }

        // funkcija oblika sin(f(x))
        internal class Sine : Function
        {
            private Function a;
            public Sine(Function f) { a = f; } // argument funkcije sin
            override public string ToString() {
                return string.Format("sin({0})", a);
            }
            override public double Value(double x) {
                return Math.Sin(a.Value(x));
            }
        }

        // funkcija oblika exp(f(x))
        internal class Exponential : Function
        {
            private Function a;
            public Exponential(Function f) { a = f; }
            override public string ToString() {
                return string.Format("exp({0})", a); 
            }
            override public double Value(double x) {
                return Math.Exp(a.Value(x));
            }
        }
        
        // funkcija oblika f(x) * g(x)
        internal class Product : Function
        {
            private Function a, b;
            public Product(Function f, Function g) 
            { 
                a = f; b = g; // argumenti funkcije f(x) * g(x)
            }
            override public string ToString() {
                return string.Format("({0} * {1})", a, b);
            }
            override public double Value(double x) {
                return a.Value(x) * b.Value(x);
            }
        }

        // funkcija oblika "x"
        internal class Variable : Function
        {
            private string name; // ime promenljive
            public Variable(string s="x") { name = s; }
            override public string ToString() { return name; }
            override public double Value(double x) { return x; }
        }

        // funkcija koja ne zavisi od x (konstantna funkcija)
        internal class Constant : Function
        {
            private double a;

            public Constant(double c) { a = c; }
            override public string ToString() { return a.ToString(); }
            override public double Value(double x) { return a; }
        }
    }

Након оваквог креирања класа ``Sine``, ``Exponential``, ``Product``, ``Variable`` и ``Constant``, 
можемо да пишемо наредбе попут:


.. code-block:: csharp

    Function x = new Variable();
    Function sin = new Sine(x);
    Function expsin = new Exponential(sin);
    Console.WriteLine("exp(sin(0)) = {0}", expsin.Value(0));

    Function tri = new Constant(3);
    Function triPutaX = new Product(tri, x);
    Console.WriteLine("3 x 5 = {0}", triPutaX.Value(5));

Додатну удобност добијамо ако напишемо и оператор за имплицитну конверзију реалног броја у 
константну функцију. 

.. code-block:: csharp

    public static implicit operator Function(double c) { 
        return new Constant(c); 
    }

Слично томе, можемо да додамо и оператор за имплицитну конверзију стринга у функцију типа 
``Variable``.

.. code-block:: csharp

    public static implicit operator Function(string s) { 
        return new Variable(s); 
    }

Сада уместо: 

.. code-block:: csharp

    Function x = new Variable();
    Function tri = new Constant(3);
    Function triPutaX = new Product(tri, x);

...можемо краће да пишемо 

.. code-block:: csharp

    Function triPutaX = new Product(3, "x"); // implicitna konverzija

Креирањем врло сличних класа за остале уобичајене функције долазимо до коначног облика 
хијерархије класа за представљање математичких функција. На крају фајла је мала класа 
``FunctionTester`` којом испробавамо написани кôд.

.. reveal:: dugme_ParsiranjeFunkcija_Functions.cs
    :showtitle: Садржај фајла Functions.cs
    :hidetitle: Сакриј садржај фајла Functions.cs

    .. activecode:: ParsiranjeFunkcija_Functions.cs
        :passivecode: true
        :includesrc: src/primeri/ParsiranjeFunkcija/Funkcije/Functions.cs
