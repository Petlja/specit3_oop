Чланови објекта и чланови класе - квиз
======================================

.. mchoice:: staticki_metodi_q1
    :answer_a: Нема право да користи објекте a, b и pom класе K.
    :answer_b: Може да користи a и b, али не може да креира објекат pom.
    :answer_c: Може да користи објекте a, b и pom, али не може да приступа њиховом приватном пољу n.
    :answer_d: Може да користи објекте a, b и pom и да приступа њиховом приватном пољу n.
    :correct: d
    :feedback_a: Не.
    :feedback_b: Не.
    :feedback_c: Не.
    :feedback_d: Тачно!
    
    Нека је класа ``K`` дефинисана овако (неки делови су изостављени):
    
    .. code-block:: csharp

        class K
        {
            private int n;

            public static void M(K a, K b) 
            {
                K pom = new K();
                pom.n = Math.Max(a.n, b.n);
                ...
            }
            ...
        }

    Која реченица је тачна за метод ``M``, као статички метод ове класе?



.. mchoice:: staticki_metodi_q2
    :answer_a: Програм не може да се покрене јер статички метод покушава да приступи пољу "a".
    :answer_b: Програм ће да испише "a = 1"
    :answer_c: Програм не може да се покрене јер метод "Ispis" није позван на исправан начин.
    :answer_d: Програм пукне у току извршавања.
    :correct: a
    :feedback_a: Тачно!
    :feedback_b: Не.
    :feedback_c: Не.
    :feedback_d: Не.
    
    Шта ће се досгодити ако покушамо да покренемо следећи програм?

    .. code-block:: csharp

        using System;

        class Program
        {
            int a = 1;
            static void Ispis()
            {
                Console.WriteLine("a = {0}", a);
            }
            static void Main(string[] args)
            {
                Ispis();
            }
        }



.. mchoice:: staticki_metodi_q3
    :multiple_answers:
    :answer_a: На месту означеном у коментару, метод F може да се позове наредбом "a.F();", али не може да користи поља објекта "a".
    :answer_b: На месту означеном у коментару, метод F може да се позове наредбом "K.F();".
    :answer_c: За метод F не постоји "свој" објекат.
    :answer_d: Mетод F не може да инстанцира објекте класе K.
    :correct: b,c
    
    Нека је дат следећи кôд (неки делови су изостављени):
    
    .. code-block:: csharp

        class K
        {
            public static void F() {...}
            ...
        }
        
        public static void Main(string[] args)
        {
            K a = new K();
            // poziv metoda F
        }

    Означи све тачне реченице о методу ``F`` класе ``K``.

