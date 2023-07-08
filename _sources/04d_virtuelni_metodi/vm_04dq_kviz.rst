Виртуелни методи -- квиз
========================

.. quizq::

    .. mchoice:: virtuelan_metod
        :answer_a: Дефинисан у базној класи, а може да се редефинише.
        :answer_b: Дефинисан у базној класи и не може да се редефинише.
        :answer_c: Дефинисан у базној класи и мора да се редефинише.
        :answer_d: У базној класи је само декларисан, дефинише се у изведеној.
        :correct: a

        Која од ових реченица је тачна за виртуелан метод?


.. quizq::

    .. mchoice:: vecinski_metod
        :answer_a: У базној класи B као обичан метод, а у изведеним по потреби.
        :answer_b: У базној класи B као апстрактан метод, а у изведеним по потреби.
        :answer_c: У базној класи B као виртуелан метод, а у изведеним по потреби.
        :answer_d: Само у класи D4.
        :correct: c

        Класе D1, D2, D3 и D4 су изведене из класе B. Класе D1, D2, D3 треба све да врше поступак F на 
        исти начин, а класа D4 да га врши другачије. Где је најбоље да се напише метод F?


.. quizq::

    .. mchoice:: virtuelna_klasa
        :answer_a: Виртуелна класа има бар један виртуелан метод.
        :answer_b: Виртуелна класа има бар један апстрактан метод.
        :answer_c: Виртуелна класа не постоји.
        :answer_d: Виртуелна класа нема инстанце.
        :correct: c

        Која од ових реченица је тачна за виртуелне класе?


.. quizq::

    .. mchoice:: redefinisanje_virtuelnog_metoda
        :answer_a: Помоћу речи virtual
        :answer_b: Помоћу речи override
        :answer_c: Помоћу речи new
        :answer_d: Не означава се (довољна је ознака у базној)
        :correct: b

        Како се (у језику C#) означава редефинисање у изведеној класи метода који је у базној виртуелни?


.. quizq::

    .. mchoice:: apstraktna_klasa_moze
        :multiple_answers:
        :answer_a: Своју базну класу из које је изведена
        :answer_b: Апстрактне методе
        :answer_c: Статичке методе
        :answer_d: Инстанце
        :correct: a, b, c, d

        Нека класа ``C`` има виртуелне методе. Шта још може да има та класа? 
        
        Означи све тачне одговоре. На сваку опцију се одговара независно од осталих.


.. quizq::

    Дат је програм 

    .. code-block:: csharp

        using System;
        class B
        {
            public virtual void F1() { System.Console.Write("Bazna-F1 "); }
        }

        class I : B
        {
            public new void F1() { System.Console.Write("Izvedena-F1 "); }
        }

        class Program
        {
            static void Main(string[] args)
            {
                B y1 = new I(); y1.F1();
                I y2 = new I(); y2.F1();
            }
        }

    .. mchoice:: virt1
        :answer_a: Bazna-F1 Bazna-F1
        :answer_b: Bazna-F1 Izvedena-F1
        :answer_c: Izvedena-F1 Bazna-F1
        :answer_d: Izvedena-F1 Izvedena-F1
        :correct: b

        Шта исписује програм?



.. quizq::

    Дат је програм 

    .. code-block:: csharp

        using System;
        class B
        {
            public virtual void F1() { System.Console.Write("Bazna-F1 "); }
        }

        class I : B
        {
            public override void F1() { System.Console.Write("Izvedena-F1 "); }
        }

        class Program
        {
            static void Main(string[] args)
            {
                B y1 = new I(); y1.F1();
                I y2 = new I(); y2.F1();
            }
        }
    
    .. mchoice:: virt2
        :answer_a: Bazna-F2 Bazna-F2
        :answer_b: Bazna-F2 Izvedena-F2
        :answer_c: Izvedena-F2 Bazna-F2
        :answer_d: Izvedena-F2 Izvedena-F2
        :correct: d

        Шта исписује програм?



.. quizq::

    Дат је програм 

    .. code-block:: csharp

        using System;
        class B
        {
            public void F1() { F2(); }
            public virtual void F2() { System.Console.Write("Bazna-F2 "); }
        }

        class I : B
        {
            public override void F2() { System.Console.Write("Izvedena-F2 "); }
        }

        class Program
        {
            static void Main(string[] args)
            {
                B y1 = new I(); y1.F1();
                I y2 = new I(); y2.F1();
            }
        }
    
    .. mchoice:: virt3
        :answer_a: Bazna-F2 Bazna-F2
        :answer_b: Bazna-F2 Izvedena-F2
        :answer_c: Izvedena-F2 Bazna-F2
        :answer_d: Izvedena-F2 Izvedena-F2
        :correct: d

        Шта исписује програм?


.. quizq::

    Дат је програм 

    .. code-block:: csharp

        using System;
        class B
        {
            public void F1() { F2(); }
            public virtual void F2() { System.Console.Write("Bazna-F2 "); }
        }

        class I : B
        {
            public new void F2() { System.Console.Write("Izvedena-F2 "); }
        }

        class Program
        {
            static void Main(string[] args)
            {
                B y1 = new I(); y1.F1();
                I y2 = new I(); y2.F1();
            }
        }
    
    .. mchoice:: virt4
        :answer_a: Bazna-F2 Bazna-F2
        :answer_b: Bazna-F2 Izvedena-F2
        :answer_c: Izvedena-F2 Bazna-F2
        :answer_d: Izvedena-F2 Izvedena-F2
        :correct: a

        Шта исписује програм?


.. comment

        using System;
        class B
        {
            public virtual void F1() { F2(); }
            public virtual void F2() { System.Console.Write("Bazna-F2 "); }
        }

        class I : B
        {
            public new void F2() { System.Console.Write("Izvedena-F2 "); }
        }

        class Program
        {
            static void Main(string[] args)
            {
                B y1 = new I(); y1.F1();
                I y2 = new I(); y2.F1();
            }
        }
