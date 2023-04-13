Различита кретања различитих тела
=================================

У овом примеру приказаћемо анимацију, током које се различите врсте тела крећу свака на свој начин. 
У примеру постоје три врсте тела: точкови који се котрљају, лоптице које скакућу и авиончићи који 
лете. Свим телима се управља на исти начин, не обраћајући пажњу на то које тело је које врсте. 

Да бисмо омогућили овакав сценарио, дефинисали смо апстрактну базну класу ``Telo``, са апстрактним 
методима ``PomeriSe``, ``RestartujSe`` и ``NacrtajSe``. Након тога, анимација се једноставно 
остварује тако што се из метода форме ``timer1_Tick`` за свако тело позове метод ``PomeriSe``, 
а из метода ``Form1_Paint`` се позове метод ``NacrtajSe``. Тело које после неког времена заврши 
са својим кретањем, може да позове свој метод ``RestartujSe`` и на тај начин почне да се понаша 
као ново тело.

|

Ево како изгледа изворни кôд класе ``Telo``:

.. activecode:: kretanje_tela_telo
    :passivecode: true
    :includesrc: src/primeri/KretanjeRazlicitihTela/Telo.cs


Класе ``Tocak``, ``Loptica`` и ``Avioncic``, изведене из класе ``Telo``, могу да буду написане овако.

.. reveal:: dugme_kretanje_tela_Tocak
    :showtitle: Садржај фајла Tocak.cs
    :hidetitle: Сакриј садржај фајла Tocak.cs

    .. activecode:: kretanje_tela_Tocak
        :passivecode: true
        :includesrc: src/primeri/KretanjeRazlicitihTela/Tocak.cs


.. reveal:: dugme_kretanje_tela_Loptica
    :showtitle: Садржај фајла Loptica.cs
    :hidetitle: Сакриј садржај фајла Loptica.cs

    .. activecode:: kretanje_tela_Loptica
        :passivecode: true
        :includesrc: src/primeri/KretanjeRazlicitihTela/Loptica.cs


.. reveal:: dugme_kretanje_tela_Avioncic
    :showtitle: Садржај фајла Avioncic.cs
    :hidetitle: Сакриј садржај фајла Avioncic.cs

    .. activecode:: kretanje_tela_Avioncic
        :passivecode: true
        :includesrc: src/primeri/KretanjeRazlicitihTela/Avioncic.cs

Класа ``Form1`` је необично мала, јер се састоји од само три врло кратка метода. Класа садржи само 
једну листу типа ``List<Telo>``. У форми се не помињу изведене класе, нити форма има потребу да зна 
било шта о тим изведеним класама. Листа тела се формира (попуњава) у методу ``Form1_Load``, при 
покретању програма. 

Као што смо поменули на почетку, на сваки откуцај тајмера тела се померају тако што се за свако од њих 
позове метод ``PomeriSe``, а у методу ``Form1_Paint`` се исцртавају позивањем метода ``NacrtajSe`` за 
свако тело.

Ако желите да испробате апликацију, потребно је да урадите следеће:

- Креирајте нови пројекат типа ``Windows Forms App`` и назовите га ``Kretanje``
- Додајте у пројекат нови фајл (десни клик на пројекат у прозору ``Solution Explorer``, ставка 
  ``Add``, а затим  ``New Item`` у искачућем менију), назовите га ``Telo.cs`` и копирајте у њега 
  кôд класе ``Telo``.
- На исти начин додајте у пројекат још три нова фајла, назовите их ``Tocak.cs``, ``Loptica.cs`` и 
  ``Avioncic.cs``, а затим копирајте у њих дате садржаје.
- Прегазите садржај фајла ``Form1.cs`` садржајем датим у наставку
- Прегазите садржај фајла ``Form1.Designer.cs`` садржајем који следи после фајла ``Form1.cs``

|

Садржај фајла ``Form1.cs``:

.. activecode:: kretanje_tela_Form1
    :passivecode: true
    :includesrc: src/primeri/KretanjeRazlicitihTela/Form1.cs
    



.. reveal:: dugme_kretanje_tela_Form1.Designer.cs
    :showtitle: Садржај фајла Form1.Designer.cs
    :hidetitle: Сакриј садржај фајла Form1.Designer.cs

    .. activecode:: kretanje_tela_Form1.Designer.cs
        :passivecode: true
        :includesrc: src/primeri/KretanjeRazlicitihTela/Form1.Designer.cs
