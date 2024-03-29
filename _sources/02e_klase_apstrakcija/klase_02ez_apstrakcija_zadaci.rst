Апстракција, индексери -- задаци
================================

.. questionnote::

    **1. Велики редак низ**

    Написати класу ``SparseArray``, која представља велики редак низ.

    Објекти ове класе треба да се понашају као низови реалних бројева, у којима је дозвољен 
    сваки индекс из опсега типа ``ulong``. Мада ово допушта милијарде милијарди елемената, у 
    стварној употреби користио би се само понеки од ових индекса -- можда њих укупно неколико 
    хиљада. Као и код обичних низова, подразумева се да је непосредно након инстанцирања 
    објекта сваки елемент једнак нули. 
    
    Није неопходно да се омогући коришћење објеката класе ``SparseArray`` у наредбама 
    ``foreach``. Довољно је написати класу тако да може да се изврши следећи кôд: 

    .. activecode:: klasa_redakniz_test
        :passivecode: true

        class Program
        {
            static void Main(string[] args)
            {
                SparseArray x = new SparseArray();
                ulong n = 4000000000000;
                x[n]++;
                x[n+1] = 3;
                Console.WriteLine(x[n]);
                Console.WriteLine(x[n+1]);
            }
        }
        
    Извршавањем кода треба да се добије следећи резултат:
    
    .. code::

        1
        3


.. questionnote::

    **2. Сабирање ретких низова**

    Проширити претходно написану класу ``SparseArray`` могућношћу да се објекти ове класе 
    сабирају међусобно и са реалним бројевима. 
    
    Збир ретких низова је нови редак низ, чији сваки елемент је једнак збиру одговарајућих елемената 
    низова сабирака. Под одговарајућим елементом се мисли на елемент са истим индексом.

    Збир ретког низа и реалног броја је нови редак низ, чији сваки елемент је збир одговарајућег 
    елемента полазног низа и датог реалног броја.
    
    Класу треба написати тако да може да се изврши следећи кôд: 

    .. activecode:: klasa_sparse_array_sabiranje_demo
        :passivecode: true
        :includesrc: src/zadaci/sparse_array_sabiranje_demo.cs
        
    Извршавањем кода треба да се добије следећи резултат:
    
    .. code::

        5
        9
        12
        14

.. comment

    Могућа решења

    .. reveal:: redak_niz|_sab_predlog_resenja
        :showtitle: Могуће решење за класу
        :hidetitle: Сакриј решење

        .. activecode:: klasa_sabiranje_resenje
            :passivecode: true
            :includesrc: src/resenja/sparse_array_sabiranje_kompletan.cs

.. questionnote::

    **3. Змијица са индексером**

    Написати класу ``Zmijica`` поштујући следећу спецификацију: 
    
    - Објекат класе ``Zmijica`` представља змијицу из познате игре на првим мобилним телефонима. 
    - Змијица се састоји од једног или више чланака, а креће се по целобројној решетки у равни. 
    - Змијица је описана листом поља која покрива, односно координатама својих чланака (сваки чланак 
      се налази на једном пољу, узастопни чланци се налазе на суседним пољима, поље је описано паром 
      целобројних координата). Један од чланака је глава змијице. 
    
    - Конструктор има два целобројна параметра, координате полазног положаја. Подразумева се да је 
      дужина змијице на почетку једнака 1.

    - Класа има следеће јавне методе и својства:

      - Својство ``Duzina`` (само за читање), које даје дужину змијице,
      - Метод ``Gore``, који имплементира померање главе навише, док остали чланци прате главу,
      - Метод ``Dole``, који имплементира померање главе наниже, док остали чланци прате главу,
      - Метод ``Levo``, који имплементира померање главе налево, док остали чланци прате главу,
      - Метод ``Desno``, који имплементира померање главе надесно, док остали чланци прате главу,
      - Метод ``Rasti``, који имплементира продужавање змијице. Метод има један целобројни 
        параметар, који може да се изостави (подразумевана вредност је један). Змијица не расте 
        приликом позива овог метода, већ за по један чланак у следећих ``n`` померања, где је ``n`` 
        параметар метода ``Rasti``. Због тога ће, након позива овог метода, реп змијице остати 
        непомичан приликом наредних ``n`` померања. У случају да се метод ``Rasti`` поново позове 
        пре него што се обави ``n`` померања, објекат памти укупан број померања током којих још 
        треба да расте, тј. "не заборавља" раст који је био "дужан" од раније.
      - Метод ``ToString``, који враћа текстуалну репрезентацију змијице. На пример, змијица са 
        главом на пољу ``(1, 5)`` и осталим чланцима на пољима ``(1, 4)``, ``(2, 4)`` редом, 
        приказује се као ``[(1, 5)(1, 4)(2, 4)]``.

    - Класа има и индексер (само за читање), који враћа резултат типа ``Tuple<int, int>``. У овој 
      торки, први елемент је :math:`x`, а други :math:`y` координата чланка који одговара наведеном 
      индексу (0 одговара глави, 1 првом следећем чланку итд.).
    
    Класу треба написати тако да може да се изврши следећи кôд.

    .. activecode:: klasa_zmijica_test
        :passivecode: true
        :includesrc: src/zadaci/zmijica_indekser_test.cs
        
    Извршавањем кода треба да се добије следећи резултат:
    
    .. code::
    
        [(3, 3)]
        [(3, 4)]
        [(2, 4)]
        [(1, 4)(2, 4)]
        [(1, 5)(1, 4)(2, 4)]
        [(0, 5)(1, 5)(1, 4)(2, 4)]
        [(0, 6)(0, 5)(1, 5)(1, 4)]
        [(1, 6)(0, 6)(0, 5)(1, 5)]
        Upotreba indeksera
        Clanak 0: x=1, y=6
        Clanak 1: x=0, y=6
        Clanak 2: x=0, y=5
        Clanak 3: x=1, y=5

.. reveal:: zmijica_savet
    :showtitle: Упутство
    :hidetitle: Сакриј упутство

    **Упутство:** 
    
    Померање змијице може једноставније да се реализује ако се уведе приватни метод ``Pomak``, јер се 
    онда јавни методи ``Gore``, ``Dole``, ``Levo``, ``Desno`` своде на један позив метода ``Pomak``:

    .. code::

        private void Pomak(int dx, int dy) { ... }
        public void Gore() { Pomak(0, 1); }
        public void Dole() { Pomak(0, -1); }
        public void Levo() { Pomak(-1, 0); }
        public void Desno() { Pomak(1, 0); }

    Сам метод ``Pomak`` може да се имплементира на различите начине, а ми ћемо овде описати два. У оба 
    случаја, чланци змијице се памте у листи парова целих бројева, где сваки елемент листе садржи 
    :math:`x` и :math:`y` координату једног чланка.

    .. code::

        private List<Tuple<int, int>> polja;

    - Једна очигледна идеја је да чланци змије у сваком тренутку одговарају редом елементима 
      листе. То значи да ``polja[0]`` представља главу, ``polja[1]`` следећи чланак итд. Овај 
      приступ подразумева да се при померању змије убаци нови елемент на почетак листе, а да се 
      избаци елемент са краја листе (уколико змијица не расте при том померању).
      
    - Ефикасније решење је да се листа која представља змијицу само допуњава, тако да је глава увек 
      на крају. ово решење је ефикасније јер не захтева премештање свих елемената листе при сваком 
      померању змијице. У овом случају, у индексеру треба на основу дужине листе и индекса траженог 
      чланка израчунати стваран положај тог чланка у листи. 
      
      Оваквим приступом ће на почетку листе остати елементи који су некад припадали змијици, али јој 
      више не припадају. Зато треба повести рачуна да ти елементи са почетка листе не буду враћени 
      као стварни чланци змијице, ако грешком буду тражени. 
      
      У овом решењу може (а не мора) да се дода и провера да ли је тај почетни, непотребан део листе 
      постао већи од неке изабране константе (нпр. 1000), па ако јесте -- избацити га из листе. 
      Овакво, повремено избацивање је много боље (ефикасније) него избацивање након сваког померања 
      главе змијице.

.. comment

      
    Могућа решења

    .. reveal:: zmijica_predlog_resenja1
        :showtitle: Могуће (једноставније) решење за класу
        :hidetitle: Сакриј решење

        **Прво (једноставније) решење за класу**
        
        .. activecode:: klasa_zmijica1
            :passivecode: true
            :includesrc: src/resenja/resenje_a_zmijica_indekser.cs

    .. reveal:: zmijica_predlog_resenja2
        :showtitle: Могуће (ефикасније) решење за класу
        :hidetitle: Сакриј решење

        **Друго (ефикасније) решење за класу**
        
        - решење са кружним бафером овде није нарочито добро због могућег раста змије.

        .. activecode:: klasa_zmijica2
            :passivecode: true
            :includesrc: src/resenja/resenje_b_zmijica_indekser.cs
