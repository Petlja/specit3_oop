Како и зашто се програмирање мења
=================================

На самом почетку, покушаћемо да појаснимо како и зашто уопште настају промене у начину 
програмирања, како се оне преносе и усвајају. Сматрамо да је то значајан увид, који помаже 
да изградите конструктиван однос према препорукама и саветима стручњака о томе како треба 
писати програме, па и о градиву које се излаже у овом курсу. Зато ћемо да направимо кратак 
преглед развоја програмирања, са нагласком на проблеме на које се наилазило у писању програма, 
на стечено искуство и путеве превазилажења тих проблема.


Први масовни програмски језици
------------------------------

Програмски језици су ушли у масовнију употребу отприлике 1960-их година. Неки од најпознатијих 
језика тог времена су *FORTRAN* (од *FORmula TRANslation* -- превод формуле) намењен интензивним 
нумеричким израчунавањима, пре свега у науци, и *COBOL* (од *COmmon Business-Oriented Language* -- 
општи пословно оријентисан језик), намењен пословној примени. Да ли сте се некад запитали зашто 
се данас не програмира (бар не масовно) на тим језицима? Зашто су научници и инжењери убрзо 
почели да смишљају и остварују нове верзије истих програмских језика, па и потпуно нове језике?

Да би одговор био што јаснији, погледајмо део једног програма написаног на језику *FORTRAN IV* 
(Фортран 4, верзија која је била актуелна почетком 1960-их):

.. code-block:: fortran

      DO 100 I=1,N
      IF(Y(I).LE.0)) GOTO 50
      IF(X(I).LE.0)) GOTO 25
      SX1 = SX1 + X(I)
      SY1 = SY1 + Y(I)
      K1 = K1 + 1
      GOTO 100
   25 SX2 = SX2 + X(I)
      SY2 = SY2 + Y(I)
      K2 = K2 + 1
      GOTO 100
   50 IF(X(I).LE.0)) GOTO 75
      SX4 = SX4 + X(I)
      SY4 = SY4 + Y(I)
      K4 = K4 + 1
      GOTO 100
   75 SX3 = SX3 + X(I)
      SY3 = SY3 + Y(I)
      K3 = K3 + 1
  100 CONTINUE

Прилично нејасно, зар не? За оне који воле мозгалице и желе да разумеју овај кôд, дајемо мало 
појашњење: ``.LE.`` значи "мање или једнако", а наредба *GOTO* (или *GO TO*, енгл. "иди на") јесте 
такозвана наредба скока, или безусловног преласка. Запис "GOTO x" значи "пређи на наредбу 
испред које је број *x*".

Ради потпуног разумевања, дајемо исти алгоритам, записан на језику *C#*:

.. code-block:: csharp

    for (int i = 0; i < n; i++)
        if (y[i] > 0)
            if (x[i] > 0) {sx1 += x[i]; sy1 += y[i]; k1++;}
            else {sx2 += x[i]; sy2 += y[i]; k2++;}
        else
            if (x[i] > 0) {sx4 += x[i]; sy4 += y[i]; k4++;}
            else {sx3 += x[i]; sy3 += y[i]; k3++;}


То што други запис разумете боље него први није само зато што сте *C#* учили, а *FORTRAN* нисте. 
Програми на фортрану су објективно мање читљиви. Ево неких разлога за то:

- у Фортрану 4 свака наредба мора да се напише у посебном реду и да почне од седме колоне (не 
  рачунајући број испред наредбе). Нема индентације, тј. увлачења редова, која доприноси читљивости;
- у Фортрану 4 не постоји *else* грана у *if* наредби. Једини начин да програм нешто уради само 
  када услов није испуњен (а нешто друго само када јесте испуњен) је употреба наредбе *GOTO*;
- у Фортрану 4 у телу *if* наредбе (тј. у "да" грани) може да се нађе само једна наредба, јер не 
  постоји груписање наредби, нпр. помоћу знакова ``{`` и ``}``. Ако је потребно да се изврши више 
  наредби када је услов *if* наредбе испуњен, треба користити наредбу *GOTO*;
- у Фортрану 4 у *if* наредбу не може да се смести петља или друга *if* наредба. Погађате, уместо 
  тога коришћена је наредба *GOTO*.

.. comment

    Куриозитет: скуп дозвољених знакова фортрана 4 чине само ови знакови: ``ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789=+-*/(),.$``
    Дакле, нема неких, данас потпуно уобичајених знакова, као што су ``[] {} < > % # \``. 
    Данас практично нема програма у коме се не појављује бар неки од ових знакова.

На краћим програмима или сегментима као што је овај, то све није толико страшно. Али сада замислите 
више стотина, или пар хиљада линија таквог кода. Додајте томе још (за данашња схватања) уврнутих 
особина тог језика:

- у фортрану 4 имена променљивих могу да имају највише 6 слова, тако да су у дужим програмима 
  често недовољно јасна;
- у фортрану 4 размаци се игноришу, па запис ``DO 100 I=1.5`` није синтаксна грешка (због тачке 
  уместо зареза), него значи исто што и ``DO100I = 1.5``,  а то је обична наредба додељивања вредности.

Како је изгледало тражење такве грешке у већем програму, боље да ни не размишљамо (узгред, 
интерактивно извршавање програма дуго није било могуће, програми су се извршавали у такозваној 
пакетној обради - пошаљете програм на извршење, сачекате пар минута и одете до штампача да 
прочитате резултат).


Разуме се да је један од највећих проблема у развоју софтвера тог времена била, у то време 
неизбежна, наредба *GOTO*. Праћење тока извршавања таквог програма је као читање оних књига са 
различитим наставцима: ако одлучите да кренете кроз пустињу, пређите на страну 57, а ако одлучите 
да сачекате, пређите на страну 43. Читајући такве књиге, обично немате оријентацију где сте у њој, 
ни колико сте удаљени од расплета. Различити токови наредби напред-назад по програму су некога 
подсећали на шпагете у тањиру, па је из тог времена остао израз "шпагети-кôд" за посебно замршене 
програме. 

.. infonote::

    У време интензивног коришћења *GOTO* наредби, **програмерима је било тешко да испрате начин 
    размишљања и основне идеје својих колега** само на основу програмског кода. На пример, када 
    се наиђе на *GOTO* наредбу, тешко је испратити да ли се помоћу ње покушава симулирање *ELSE* 
    гране или уметање друге *IF* наредбе, јер све GOTO наредбе изгледају исто. Зато је било 
    уобичајено да аутор програма уз кôд приложи и тзв. алгоритамску шему, која помаже да програм 
    разумеју и други програмери.

У таквој ситуацији проблем се обично најпре ублажава препорукама како треба, а како не треба да 
се користи *GOTO* наредба, препорукама о писању коментара који доприносе разумевању кода и слично. 
Међутим, када не постоји механизам који програмере тера да се придржавају таквих савета, увек се 
нађе неко коме се жури или ко има неко друго оправдање за непоштовање правила. Уосталом, чак и 
када би програмер био врло педантан и писао добро организован фортран програм уважавајући све 
препоруке, *GOTO* наредбе су и даље отежавале тимски рад.

Сазревале су идеје о бољем програмском језику, који би спречио лошу праксу скакања по програму и 
омогућио програмерима да пишу програме који су њиховим колегама лакши за разумевање. Наравно, било 
је потребно извесно време да се те идеје уобличе, конкретизују и постану опште прихваћене, а онда 
и да се рачунар оспособи да такав бољи, али за машину сложенији језик "разуме", тј. да може да га 
преведе у извршиви програм.

Структурно програмирање
-----------------------

Увођење наредби *if-then-else*, *switch*, *while* и сличних, могућност уметања таквих наредби 
једних у друге, па и само груписање наредби у блокове, били су крупан корак у рачунарским наукама 
и програмерској пракси. Језик *ALGOL* (од *ALGOrithmic Language* -- алгоритамски језик), заснован на 
оваквим наредбама и другим напредним идејама, настао је свега пар година после Фортрана, али никада 
није ушао у масовну употребу (о разлозима ће још бити речи). Тек крајем 1960-их, група језика 
настала из Алгола (*PL/I*, *PASCAL* и други) успева да значајније промовише овај приступ програмирању. 
Један овакав, заокружен скуп идеја и концепата који мења начин размишљања и функционисања називамо 
**парадигма**. Дакле, започета је нова парадигма, која је постала позната под називом структурно 
програмирање. Реч "структурно" овде пре свега значи да сами програми имају структуру (мада је 
структура уведена и у податке), то јест да се програмске целине састоје од мањих целина -- наредбе се 
умећу у друге наредбе. Са ширењем структурних језика, претходна генерација језика је добила назив 
**неструктурни језици**, који им заиста и одговара. Да бисмо се у то уверили довољно је погледати 
наведени део програма у Фортрану. У програмима писаним на таквим језицима не издвајају се целине 
које би чиниле структуру програма, тј. ти програми нису хијерархијски организовани.

Језик C
'''''''

Почетком 1970-их у истраживачком центру Белових лабораторија (AT&T Bell Laboratories) настао је 
програмски језик *C*, који је брзо постао и дуго остао екстремно популаран, а и данас се користи 
за одређене намене. *C* је настао по угледу на 
структурне језике и усвојио је њихове концепте, али се није одрекао наредбе *goto* (која се на овом 
језику пише малим словима). Према томе, *C* није чист структурни језик, или бар то није био у време 
свог успона и револуције коју је донео, али га ипак помињемо у оквиру ове парадигме.

.. comment

    Језик *C* створио је Денис Ричи (Dennis Ritchie) 1972. године

Ако желимо да уопштено разумемо догађаје и трендове у развоју програмирања, ово је добро место да 
застанемо и запитамо се: због чега су творци језика *C* донели одлуку да задрже наредбу *goto*, када 
се добро знало какве проблеме она доноси? Такође се знало и да су научници који су осмислили 
структурно програмирање математички доказали да је наредба *goto* непотребна, тј. да сваки проблем 
може да се реши и без ње. Ипак, популарност језика *C* у односу на чисте структурне језике сугерише 
да одлука аутора није била погрешна. Када размислите, кликните на дугме испод да проверите своје 
размишљање.

.. reveal:: popularnost_c
    :showtitle: Језик C и наредба GOTO
    :hidetitle: Сакриј објашњење

    **Језик C и наредба GOTO**
    
    У то време већ је био написан велики број програма на Фортрану и другим неструктурним 
    језицима, које је било тешко одржавати и развијати даље. Задржавање наредбе *goto* у језику 
    *C* омогућило је далеко лакше (чак и аутоматско) превођење, тј. миграцију програма са старијих 
    језика на *C*. То је овом језику дало огромну почетну базу програмског кода, што га је учинило 
    недостижним по распрострањености и тражености у односу на чисте структурне језике, који су 
    почињали практично од нуле. При томе програмери углавном нису користили наредбу *goto* у новим 
    *C* програмима, али је из наведеног разлога било важно да она постоји. Са навикавањем 
    програмера на нове концепте, наредба *goto* је убрзо природно нестала из употребе.
    
    Наравно да наредба *goto* и преузимање програма са старијих језика није једини, па ни 
    најважнији разлог велике популарности језика *C*, али је на описани начин допринела да он 
    брзо постане популаран. Не треба сметнути с ума оригиналне доприносе овог језика. Поред 
    осталог, *C* је циљано писан тако да може што једноставније и што потпуније да искористи 
    постојећи хардвер (нпр. адресирање на нивоу бајта је била значајна могућност у време малих 
    и скупих меморија).

Већ овде можемо да приметимо (а то се потврђује и у даљем праћењу развоја програмских језика) 
извесне разлике између програмских језика насталих у академским установама и оних развијених у 
истраживачким центрима великих корпорација, као што је амерички AT&T. Наиме, из академских центара 
обично стижу језици у потпуности засновани на новим и бољим концептима. Такви језици стриктније 
поштују прокламоване принципе, па су у том смислу чистији и лакши за учење и коришћење. Међутим, 
због наслеђеног кода који има велику вредност, технолошке компаније не могу лако да пређу на такве, 
концептуално чисте језике. Прелазак на нове концепте уз забрану проблематичног, раније коришћеног 
начина писања кода би подразумевао писање постојећег кода испочетка. Често програмери то и желе да 
ураде, али за руководиоце пројеката и менаџере то је скоро увек неприхватљиво, што због иницијалне 
цене поновног писања, што због времена потребног за ишчишћавање нове верзије кода од багова. 

Уместо преласка на концептуално чисте језике, компаније обично проширују постојеће језике, или 
стварају нове верзије тих језика, уводећи нове концепте уз задржавање постојећих, проблематичних 
особина претходних језика (или верзија језика). Инжењери су принуђени да нова решења усвајају само 
у мери у којој могу да их економично уклопе у затечено стање. Тиме што омогућава комбиновање кода 
писаног старим и новим стилом, индустрија софтвера постиже компромис између два циља: са једне 
стране, допушта да се програми писани на превазиђен начин (или њихови директни преводи) још дуго 
користе, а са друге, омогућава програмерима да почну да користе нове концепте и свој даљи рад 
учине продуктивнијим. При томе сваки тим програмера може да пређе на нови стил програмирања онда 
када је за то спреман и када му одговара. 

Период раста и нови проблеми
''''''''''''''''''''''''''''

Структурно програмирање је било актуелан концепт током 1970-их. Оно је значајно олакшало развој 
софтвера. Читави оперативни системи су пребацивани на *C*, да би се на њему лакше даље развијали. 
Слично се догодило са многим научноистраживачким програмима писаним на Фортрану и другим 
пројектима. Почеле су да се развијају и рачунарске игре. Број софтверских пројеката је нагло 
растао, а и сами пројекти су постали већи и сложенији. Разумљиво, растућа сложеност тада актуелних 
пројеката довела је до нових проблема. 

Један од тих нових проблема представљале су **глобалне променљиве**. 

Уопштено говорећи, променљиве које користимо у програму служе да моделирају објекте и процесе из 
проблема који решавамо. Да би променљиве заједно представљале неко смислено стање, тј. да би наш 
модел био конзистентан, вредности променљивих треба да задовољавају неке услове. На пример, 
количина горива у резервоару треба да буде између нуле и капацитета резервоара. У случају 
сложенијег модела и већег броја променљивих и ти услови могу да буду сложенији. 

Замислите сада огроман програм са мноштвом функција распоређених у велики број фајлова. Многе од 
тих функција користе неке глобалне променљиве, могу да их читају и да им мењају вредности. У неком 
тренутку приметимо да променљиве више не задовољавају потребне услове, тј. да наш модел више није 
конзистентан. Природно, желимо да откријемо како је дошло до нарушавања услова, да бисмо отклонили 
неисправност у коду. Међутим, трагање за узроком грешке у великом програму са глобалним 
променљивима може да буде веома тешко.

Други проблем се тиче променљивости **интерфејса функција**. 

Вероватно вам се догодило да неку функцију коју сте написали почнете да користите, а касније 
схватите да бисте могли да је дорадите. На пример, желите да уопштите функцију и омогућите јој 
да поред онога што већ ради, покрије још неке сличне случајеве. Ради тога функцији најчешће 
треба додати један или више параметара или направити неку другу измену у начину позивања. Уз 
то је потребно и да се на већ постојећим местима позива функције дода нови параметар, чија 
вредност одговара старој функционалности. У малом програму то је лако, али проблем настаје ако 
се функција позива на много места. 
Још је већи проблем ако ту функцију већ користе и други програмери. Ово може да се догоди при 
било каквој промени начина позивања неке већ написане функције.

Искуснији програмери организују рад на пројекту тако да ове проблеме што више ублаже. На пример, 
одређене променљиве дисциплиновано користе само у једном делу кода и тиме смањују могућност 
грешке (мањи број људи користи променљиву па је мање неразумевања око њене употребе), а ако до 
грешке и дође, лакше је пронаћи је. Такође, најважније функције за које се зна да ће их користити 
и други тимови програмера, пажљиво смишљају и дизајнирају најискуснији чланови задуженог тима, 
да би се смањиле шансе да буде потребна нека измена начина позивања тих функција. 

Као што већ знамо, ако нема формалног механизма који обавезује на поштовање уведених правила, увек 
се нађе неко ко прекрши та правила, често верујући да неће доћи до проблема. На пример, некоме 
може да изгледа згодно да употреби функцију једног модула, која није намењена за употребу ван 
модула. Начин позивања те функције може да измени програмер који ради на поменутом модулу, не 
знајући да се она користи и ван модула. Слично, неко може да употреби глобалну променљиву на 
месту где се то не очекује, а при томе може и да јој промени вредност и тиме наруши начин 
употребе те променљиве који је договорен између оних који су дату променљиву увели.

Сазревало је време за нове концепте, који би искуснијим програмерима омогућили да пројекат поставе 
тако, да остали програмери касније врло тешко могу да покваре ред који је успостављен на почетку.

Објектно оријентисано програмирање
----------------------------------

Првим објектно оријентисаним језиком се сматра језик Симула (*Simula*), који је настао у Норвешком 
рачунарском центру у Ослу (Norwegian Computing Centre, NCC) још почетком 1960-тих. Међутим, ни овај 
језик, као ни Алгол, није масовније коришћен и није (директно) утицао на главни ток развоја 
програмирања. Као и при продору структурног програмирања, било је потребно да прође доста времена и 
да се масовно раширена пракса судари са проблемима које не може да реши и који јој наносе велику 
штету, да би нови концепти почели масовније да се усвајају. Тако је парадигма објектно оријентисаног 
програмирања наступила тек двадесетак година касније.

Почетком 1980-их година, Бјарне Стравструп (Bjarne Stroustrup), инспирисан Симулом и другим 
језицима из академских кругова (нпр. *Smalltalk*), надграђује "индустријски" језик *C* и ствара 
језик *C++*, задржавајући при томе и старе, необјектне карактеристике из претходног језика. 
Вероватно можете већ на основу овог кратког описа да претпоставите да ни језик *C++* (као ни *C*) 
није настао у академским круговима, већ индустријским. Заиста, Стравструп је у време стварања 
језика *C++* радио у истим оним Беловим лабораторијама (у којима је настао и језик *C*), мада је 
касније велики део каријере провео и као универзитетски професор. 

Програмирање је наставило да се развија на сличан начин и касније. Језик *C++* сада је стар већ 
неколико деценија и током свог постојања је много пута дорађиван. У тим дорадама (ревизијама) језика, 
он је углавном прошириван, било новим могућностима његове стандардне библиотеке *STL*, било новим 
синтаксним конструкцијама, што је побољшавало језик на разне начине. Међутим, врло ретко су неке 
постојеће могућности укидане и избациване из језика, јер би то значило престанак рада огромног 
броја постојећих програма у новим верзијама језика. У ревизије језика често су укључивани и 
концепти развијени у чисто научним круговима, мада обично са задршком од бар неколико година између 
првих објављивања нових концепата и њихове масовне примене, до које је дошло када се за тим 
концептима јавила озбиљна потреба.

О новостима које је самом својом појавом, а и каснијим развојем донело објектно оријентисано 
програмирање (скраћено: ООП) биће много више речи у наставку. Овде ћемо се тих новитета само 
дотаћи, да бисмо додатно илустровали изнесени поглед на развој програмирања.

Најважнији појам парадигме ООП је класа. Распоређивање кода у класе нам омогућава да неке 
променљиве и неке функције учинимо недоступним ван класе (променљиве и функције унутар класе се 
зову поља, односно методи класе). На тај начин, класе доносе једно могуће решење проблема описаних 
у вези са структурним програмирањем (глобалне променљиве и глобалне функције). Када о класи 
размишљамо као неко ко ту класу прави, проглашавање неких података и функција за приватне делове 
класе називамо **енкапсулација** (затварање у капсулу). Када о класи размишљамо као неко ко ту 
класу користи, то што су неки делови класе невидљиви споља називамо **апстракција**. Корисник 
класе не мора да зна ништа о томе како је класа имплементирана -- њега једино интересује интерфејс 
(начин употребе) те класе. У ствари је и боље да корисник класе не зна ништа о имплементацији 
класе да не би почео да тражи начине да се веже за неке детаље имплементације, који би могли 
касније да се промене. Везивањем за детаље имплементације отежавамо даљи развој и побољшавање 
кода класе. У том смислу, апстракција као концепт за корисника класе значи бављење само оним што 
је битно, а то је експонирана (откривена, изложена) функционалност и начин употребе те 
функционалности, док све остало може да се занемари.

Поред концепата енкапсулације и апстракције, ООП је донело и друге важне концепте, који су 
одговорили на још неке честе потребе програмера. Концепт **наслеђивања** је омогућио да постојећу 
функционалност једноставно надограђујемо на различите начине на различитим местима, а да при томе 
постојећи кôд не мора ни да се мења ни да се копира. Концепт **полиморфизма** је омогућио да 
објекте различитог типа и различитог понашања користимо као да су истог типа, што значајно 
поједностављује њихову употребу. Као што смо рекли, свим овим и другим концептима бавићемо се 
кроз већи део овог курса. 

Други правци развоја програмирања
---------------------------------

Један, али не и једини ток развоја програмских језика и парадигми програмирања чине неструктурни, 
структурни и објектно оријентисани језици, о којима је до сада било речи. Све ове језике једним 
именом називамо **императивним** језицима. Поред императивних, упоредо су се развијали и други 
језици и програмске парадигме, као што су разни облици декларативног програмирања (функционално 
програмирање, логичко програмирање, реактивно програмирање и други). Овде се нећемо упуштати у 
детаљније упознавање ових или других парадигми, већ ћемо само поменути пар концепата који су из 
њих стигли и до императивних језика.

Поменули смо потребу да у оквиру једног модула поједине функције сакријемо и сачувамо само за 
интерну употребу. Један начин да се тај циљ оствари су приватни методи класа, који ће ускоро бити 
детаљније објашњени. Другачије решење 
нам стиже из функционалног програмирања кроз концепте угнежђених (локалних) функција, анонимних 
функција и слично. Ови концепти нам омогућавају да напишемо функцију која може да се користи на 
само једном месту у коду (анонимна функција), или да ограничимо употребу функције на мали део кода 
у њеној близини (локална функција), не допуштајући да се она користи за нешто за шта није намењена.

Још један важан концепт који се промовише функционалним програмирањем су непроменљиви (имутабилни) 
подаци. Идеја је да се у програму користе имена којима се вредност придружује само једном. Такве 
величине се не могу касније променити, већ само могу да се користе за израчунавање нових величина. 
Доследно и потпуно спровођење овог концепта у императивним језицима не би било практично, али и 
делимичним спровођењем може да се поправи квалитет програма, односно да се смањи могућност грешке. 
Ево неких принципа који проистичу из концепта непроменљивих података.

- Функције треба да за исте аргументе увек дају исти резултат (не треба да имају и чувају неко 
  своје "интерно стање", од кога би резултат могао да зависи).
- Функције треба да примају податке само преко аргумената (не треба да имају приступ додатним 
  подацима, као што би биле нпр. глобалне променљиве).
- Функције треба да дају резултате само као враћене вредности (не треба да мењају вредности својих 
  аргумената или било које друге вредности).

Поштовањем ових принципа нестаје могућност да функција произведе такозвани *споредан ефекат* (*side 
effect*, бочни ефекат), односно скривено дејство. Када функције имају споредан ефекат, промена 
редоследа рачунања или поновно позивање неке функције могу да доведу до другачијег резултата и 
грешака, што се и дешава када програмер који мења код није свестан споредних ефеката. Управо зато 
је пожељно да функције немају споредних ефеката, а ако их имају да они буду што очигледнији, нпр. 
на основу имена функције.

Принцип избегавања споредног ефекта и употребе "чистих" функција је донекле у супротности са 
концептом енкапсулације, који управо нуди могућност памћења унутрашњег стања. Ово значи да 
различите концепте треба примењивати са јасним циљем и разумевањем. Концепт којег ћете се држати 
у пројекту зависи од тога које проблеме желите да избегнете. Ове концепте не морате да примењујете 
истовремено, а ако их користите у истом пројекту, не морате да их користите на истом нивоу 
сложености (нивоу апстракције). Ово ће вероватно бити јасније у конкретним ситуацијама, на какве 
ћемо наилазити и током овог курса. 

Декларативни програмски језици као језици и као целовита парадигма су углавном остали везани за 
академске (образовне и научноистраживачке) кругове и до сада се нису у значајној мери пренели на 
индустрију софтвера. Део разлога је свакако и у огромном наслеђеном коду, који се не може ни 
одбацити (у питању је велика вредност) ни мигрирати тако да буде у пуном складу са предложеним 
концептима (преправке су велике и не могу да се аутоматизују). Још један разлог је ефикасност, 
јер имплементација декларативних језика често заостаје по брзини извршавања за императивним 
језицима. Ипак, као што видимо, неки концепти декларативног програмирања проналазе своје путеве 
и полако улазе у императивне језике као један могући стил писања кода. 

~~~~

Из свега реченог можемо да уочимо неке правилности у развоју програмирања. Напредак се обично 
испољава кроз смењивање и спирално понављање одређених фаза. 

- Током времена се примете извесни проблеми и тешкоће у раду.
- Међу научницима и истраживачима се појављују нови концепти у чистом, на неки начин идеалном 
  облику. Међутим, као што је већ истицано, у узнапредовалим пројектима је због наслеђеног кода 
  тешко применити те нове концепте у чистом, изворном облику. 
- У индустрији почињу да се примењују решења заснована на "саветима за избегавање проблема". 
  Успех је делимичан, проблеми су ублажени, али и даље постоје и проузрокују штету. Напредак 
  пројеката постаје толико спор и скуп да се све интензивније ради на омогућавању нових концепата 
  на постојећим, поодмаклим пројектима, уз даље коришћење наслеђеног кода без измена у њему. 
- Нови концепти улазе у ширу праксу, било кроз проширења постојећих језика, било кроз нове језике, 
  врло сличне постојећим. У случају увођења нових, савременијих језика, по правилу се у њима 
  задржавају и оне застареле могућности које су доприносиле настанку проблема, да би се постојећи 
  пројекти што једноставније наставили. Настаје сложенији језик, који подржава различите стилове 
  програмирања.
- Следи период привикавања ширих кругова програмера на нови стил програмирања, који доводи до 
  раста и напретка у достигнућима. Током ове фазе стиже се до нових изазова на вишем нивоу.

Ово изнуђено усложњавање језика има многе негативне последице: сам језик постаје све тежи за учење, 
договарање о томе који део пројекта ће бити писан којим стилом (стандардом) постаје сложеније, 
а компликује се и одржавање различитих делова кода писаних различитим стиловима. Нажалост, то је 
цена напретка у постојећим условима, до доласка неке корените промене која би могла да успостави 
потпуно ново стање. 

Савети о учењу
--------------

Усавршавање -- проширивање знања
''''''''''''''''''''''''''''''''

Да би избегао организационе компликације које настају при преласку на нови стил програмирања (а можда 
и да би уштедео време потребно за усвајање новог стила), одређени тим програмера може да одлучи да на 
нови стил пређе касније. Краткорочно, таква одлука може да буде оправдана ако тим тренутно нема времена 
(нпр. треба за релативно кратко време да заврши одређену фазу пројекта). Усвајање нових концепата 
углавном није хитно, а многе нове идеје и не доживе нарочит успех у пракси, па није рационално учити 
све новитете чим се појаве. Међутим, дугорочно избегавање преласка на нови, шире прихваћен стил 
програмирања води у проблеме. Једна врста проблема тиче се пропуштања користи које нови стил доноси, 
што значи отежан и успорен напредак у пројектима, о чему је већ било речи. Друга врста проблема се 
јавља у случају веома дугог игнорисања бољег и шире прихваћеног стила у програмирању, а то је ризик 
да се остане ван главног тока у програмерској заједници или некој њеној грани. 

Посматрано дугорочно, **знање програмирања није статично знање** које се стиче једном, већ је потребно 
бар информативно пратити промене, а повремено и улагати напор да се благовремено науче, разумеју и 
постепено усвоје неки нови концепти који су се већ довољно афирмисали и који могу да олакшају даљи 
рад. Овакав поглед на програмирање је веома важан за све програмере и оне који ће то постати. 


Прелазак на веће пројекте -- продубљивање знања
'''''''''''''''''''''''''''''''''''''''''''''''

Нови концепти се обично илуструју на малим примерима, да би пажња читалаца била усредсређена на 
оно о чему је реч. Нажалост, на тако малим примерима корист од новог концепта често не може да 
дође до (пуног) изражаја, па тај концепт може да делује као бескористан, тј. као неко "паметовање 
у празно". Треба имати на уму да су концепти о којима ће бити речи уведени да би решавали проблеме 
који настају у великим програмима, па њихово дејство треба и замишљати на великим програмима.

Када довољно увежбате смишљање алгоритама и писање мањих програма, дешаваће вам се разне ситуације. 
Претпоставимо да умете да решите проблем којим се тренутно бавите. Ако при томе неки концепт о коме 
сте учили не разумете или не осећате његов смисао, боље је да на свој начин завршите то на чему 
тренутно радите. Праћење упутстава без разумевања тешко може да буде корисно, а може да буде и 
погрешно. Ако концепт оквирно разумете али сматрате да вам није неопходан, можете да пробате да га 
следите за вежбу, са идејом да ће се такав приступ у неком тренутку исплатити. 

У обе ове ситуације вероватно је ипак најкорисније да пређете **на сложеније задатке, најпре 
самостално, а затим и у тиму** (рад у тиму је посебно вредно искуство). Да би вам савети који 
нуде решење неког проблема били корисни, потребно је прво да имате проблем. Када се сударите са 
задатком који вам је довољно изазован (тежак), лакше ћете препознати које препоруке за вас имају 
смисла и могу да вам помогну. Усвајање препорука које бар делимично разумете обично значи мање 
великих преправки у коду пројекта и дугорочно гледано већу ефикасност (продуктивност). Некада је 
потребно осетити и последице лоших одлука, да би се боље схватило шта се добија придржавањем 
одређених савета. Поуке извучене у таквим ситуацијама граде искуство. Постепено, током рада на 
већим пројектима стиче се искуство и долази до дубљег разумевања препоручених концепата и стилова 
програмирања. 


.. comment

    Развој софтвера (а и развој уопште) прате и мале и велике промене. Мање, еволутивне промене су 
    постепена побољшавања језика о којима смо говорили и оне се дешавају релативно често. Са друге 
    стране, радикалне одлуке попут престанка подршке за одређену верзију језика или одређену верзију 
    неке библиотеке су веома ретке. До таквих одлука долази тек када преостали број корисника старе 
    верзије постане врло мали, а одржавање врло скупо због нагомиланих разлика у односу на новије 
    верзије.

    

    Дакле, кренимо храбро напред.

    На следећој слици је приказана идеја о перципираној (доживљеној) тежини проблема, у зависности 
    од усвојених концепата и стварне, објективне комплексности. Зелена боја означава оно што 
    доживљавамо као лако, а црвена тешко.

    ГРАФИК!

    овде слика - график (x: време, y: комплексност)
        перципирана тежина проблема представљена бојом (доле десно зелено, горе лево црвено, са 
        постепеним прелазом)

    ~~~~

    Међу функцијама природно настаје извесна хијерархија.

    **слика дрвета позива**

    Функције које су ближе корену дрвета су обично улазне тачке у поједине модуле, док функције 
    које су ближе листовима обично имплементирају поједине функционалности унутар модула. За 
    функционисање целог пројекта је важно да се 

    Мањи интерфејс према кориснику
    
    ~~~~
    
    Било је људи који су говорили: "Шта ће ми *while* када имам *goto*", или "Шта ће ми приватни 
    чланови у класи", или нешто друго. 
