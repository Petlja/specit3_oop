Апстрактни методи и класе -- задаци
===================================

1. Игра погађања замишљеног броја
---------------------------------

.. questionnote::

    Написати програм који омогућава играчу да зада ниво тежине 
    игре и број :Math:`n` као дужину интервала, а затим да погађа замишљени број између :Math:`1` и 
    :Math:`n`. Играч после сваког покушаја треба да добије информацију да ли је замишљени број мањи, 
    већи или једнак његовом покушају. Када играч погоди замишљени број, програм му саопштава број 
    покушаја који су му били потребни, а затим понуди нову игру. 
    
    Програм написати тако да подржава два нивоа тежине. На првом нивоу програм заиста бира случајан 
    број од :Math:`1` до :Math:`n` и после сваког покушаја играча одговара као што је описано. На 
    другом нивоу програм не бира замишљени број него само ствара утисак да је замислио неки број. 
    На покушаје погађања, програм одговара у складу са претходним одговорима, али тако да играчу 
    сваки пут даје најнеповољнији одговор, тј. одговор који оставља највећу недоумицу. На пример, 
    ако играч погађа број од 1 до 100, за наведене покушаје погађања одговори треба да изгледају 
    овако: 
    
    - играч: 40? програм: замишљени број је већи
    - играч: 90? програм: замишљени број је мањи
    - играч: 38? програм: замишљени број је већи
    - играч: 60? програм: замишљени број је већи
    - играч: 80? програм: замишљени број је мањи
    - играч: 71? програм: замишљени број је мањи
    - играч: 67? програм: замишљени број је мањи
    - играч: 63? програм: замишљени број је већи
    - играч: 65? програм: замишљени број је мањи (овде је одговор могао да буде и да је замишљени број већи)
    - играч: 62? програм: замишљени број је већи
    - играч: 68? програм: замишљени број је мањи
    - играч: 64? програм: честитам, погодили сте из 12 покушаја.
    
.. collapse:: Упутство

    Може да се напише апстрактна класа ``Igra`` и две из ње изведене класе, за сваки ниво тежине по 
    једна. Довољна су два апстрактна метода: ``void Pocni(int n)`` и ``string Odgovori(int pokusaj)``. 

    Једноставан начин да се имплементира други ниво је да програм одржава интервал коме "замишљени" 
    број мора да припада. На пример, ако играч погађа број од 1 до 100, тај интервал је на самом 
    почетку :math:`[1 .. 100]`, након покушаја :math:`40` и одговора да је замишљени број већи, 
    интервал је :math:`[41 .. 100]` итд. Једина ситуација у којој програм на другом нивоу одговара 
    да је број погођен је да се овај интервал претходно свео на један број и да играч као свој 
    покушај наведе баш тај број. 

2. Роботи 
---------

.. questionnote::

    Четири типа робота могу да се крећу по бинарној матрици, у којој нуле представљају 
    пролаз, а јединице препреке. Роботи разумеју команде "десно", "лево", "напред" и "назад", али 
    их извршавају на различите начине:

    - тип 1 – неусмерен спор робот:

      - На команду **десно** иде једно поље десно, тј. у следећу колону ако је могуће (поље постоји и слободно је)
      - На команду **лево** иде једно поље лево, тј. у претходну колону ако је могуће (поље постоји и слободно је)
      - На команду **напред** иде једно поље горе, тј. у претходну врсту ако је могуће (поље постоји и слободно је)
      - На команду **назад** иде једно поље доле, тј. у следећу врсту  ако је могуће (поље постоји и слободно је)

    - тип 2 – неусмерен брз робот:

      - На команду **десно** иде десно док постоје слободна поља десно
      - На команду **лево** иде лево док постоје слободна поља лево
      - На команду **напред** иде горе док постоје слободна поља горе
      - На команду **назад** иде доле док постоје слободна поља доле

    - тип 3 – усмерен спор робот (на почетку окренут на горе):

      - На команду **десно** окреће се 90 степени удесно
      - На команду **лево** окреће се 90 степени улево
      - На команду **напред** иде једно поље у смеру у ком је окренут, ако у том смеру поље постоји и слободно је
      - На команду **назад** иде једно поље уназад, ако поље иза робота (тј. у смеру супротном од смера у ком је окренут) постоји и слободно је

    - тип 4 – усмерен брз робот (на почетку окренут на горе):

      - На команду **десно** окреће се 90 степени удесно
      - На команду **лево** окреће се 90 степени улево
      - На команду **напред** иде у смеру у ком је окренут док постоје слободна поља у том смеру
      - На команду **назад** иде уназад (без окретања) док постоје слободна поља у том смеру, а то је смер супротан од смера у ком је окренут

    Дат је програм који, не користећи никакве класе, само демонстрира кретање робота. Овај програм 
    нуди корисника да одабере тип робота, а затим на унапред припремљеној матрици извршава низ 
    унапред припремљених команди за одабраног робота. На крају, програм исписује координате поља 
    на коме се налази робот након извршења команди. 

    .. reveal:: kretanje_razlicitih_robota_0_dugme
        :showtitle: Програм без класа
        :hidetitle: Сакриј програм без класа

        **Кретање различитих робота -- програм без класа**
        
        .. activecode:: kretanje_razlicitih_robota_0_bez_klasa
            :passivecode: true
            :includesrc: src/zadaci/kretanje_razlicitih_robota_0_bez_klasa.cs
            
    Програм би био комплетнији када би омогућио задавање матрице, као и низа команди, а могао би и да 
    приказује кретање одабраног робота по матрици. У овом задатку се не тражи писање таквог програма. 

    Потребно је на основу датог програма допунити следећи започет програм одговарајућим класама 
    робота, тако да овај програм при извршавању даје исти резултат као претходни програм који не 
    користи класе. 

    **Програм који треба допунити писањем класа**
        
    .. activecode:: kretanje_razlicitih_robota_1_zadatak
        :passivecode: true
        :includesrc: src/zadaci/kretanje_razlicitih_robota_1_zadatak.cs

.. collapse:: Упутство

    Треба прво написати апстрактну базну класу робот. Ова класа свакако треба да има апстрактне 
    јавне методе ``Desno()``, ``Levo()``, ``Napred()`` и ``Nazad()``.  У базној класи треба 
    написати и статички метод ``Napravi``, који враћа референцу на класу ``Robot``, позивајући 
    конструктор одговарајуће изведене класе (зависно од целобројног типа робота, који се задаје 
    као први параметар метода ``Napravi``).     
    
    Осим тога, базна класа може да има два целобројна заштићена поља, помоћу којих памти локацију 
    робота у матрици. За базну класу може да се напише и заштићени конструктор, помоћу којег се 
    задаје почетна локација робота. 
   
    Даље, за сваки од четири описана типа робота треба написати по једну класу, изведену из класе 
    ``Robot``. Усмерени роботи треба да имају и поље ``smer``, које може да има једну од четири 
    вредности. На пример, ако је поље типа ``char``, вредности могу да буду ``'N'``, ``'E'``, 
    ``'S'`` и ``'W'``, а ако је поље целобројно, вредности могу да буду бројеви 0, 1, 2 и 3.

3. Банковни рачун
-----------------

.. questionnote::

    У зависности од количине расположивог новца на рачуну, банка може 
    различито да наплаћује одржавање рачуна, да исплаћује или не исплаћује камате итд. На пример:
    
    - ако је рачун у минусу, одржавање се наплаћује 500 динара месечно (подразумева се да нема камате)
    - ако је рачун у умереном плусу, одржавање се наплаћује 200 динара месечно и нема камате
    - ако је рачун у великом плусу, одржавање се наплаћује 250 динара месечно, а камата је 5%
    
    За рачун који је у минусу кажемо да је презадужен, за рачун у умереном плусу да је стандардан, 
    а рачун у великом плусу је повлашћен. После сваке уплате или исплате рачун може да промени статус 
    у складу са тренутном количином новца на њему.
    
    Дат је програм који моделира један такав рачун. Детаљи модела могу да се виде у самом програму.
    
    .. reveal:: bankovni_racun_priprema_dugme
        :showtitle: Банковни рачун - уводни програм
        :hidetitle: Сакриј уводни програм

        **Банковни рачун -- уводни програм**
        
        .. activecode:: bankovni_racun_priprema
            :passivecode: true
            :includesrc: src/primeri/design_patterns/bankovni_racun/bankovni_racun_priprema.cs

    У овом задатку потребно је да се допуни доле започети програм писањем апстрактне класе ``RacunSaStatusom`` 
    и три из ње изведене класе (по једна класа за сваки статус рачуна), тако да након допуне програм 
    има исту функционалност као и уводни програм.

    .. activecode:: bankovni_racun_zadatak
        :passivecode: true
        :includesrc: src/primeri/design_patterns/bankovni_racun/bankovni_racun_zadatak.cs

.. collapse:: Упутство

    Ако до сада нисте, пажљиво проучите пример "Продајни аутомат" из ове лекције. Решење овог задатка 
    може да се напише по угледу на друго решење поменутог примера.


4. Телефонски претплатници
--------------------------

.. questionnote::

    Написати класу ``TelefonskiPretplatnik``, која садржи податке о особи, број телефона, евиденцију 
    о послатим SMS порукама, обављеним разговорима и протоку података на интернету. Поред свих ових 
    података, класа ``TelefonskiPretplatnik`` садржи и референцу на класу ``TarifniPaket``, која на 
    основу евиденције укупног комуницирања израчунава износ рачуна. Класа ``TarifniPaket`` треба да 
    има више изведених класа, од којих свака може на различит начин да тарифира разговоре и поруке у 
    истој и различитој мрежи, домаћи и инострани саобраћај, потрошене гигабајте на интернету и слично. 
    
    Напомена: спецификација у овом задатку није сасвим прецизна и детаљна, што оставља слободу да 
    се она допуни на различите начине. Самим тим, могућа су решења која се за исте улазне податке 
    различито понашају. 
    
.. comment

    Пример игрице у којој учествују различити карактери

    .. code::

        abstract class Karakter
            PrikaziSe();
            
    Разни карактери се приказују на различите начине. 

    - Непокретан карактер може само да нацрта своју битмапу на својој локацији
    - Покретан карактер може да користи једну од неколико битмапа, зависно од тога да ли стоји или се креће у неком смеру
    - Неки карактери могу да се приказују помоћу две или више битмапа (нпр. према томе како држе оружје)
    - Неки карактери могу да преко своје битмапе нацртају одређене ефекте у складу са акцијом коју предузимају
