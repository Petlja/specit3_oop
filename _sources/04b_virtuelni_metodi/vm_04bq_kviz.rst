Виртуелни методи - квиз
=======================

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
