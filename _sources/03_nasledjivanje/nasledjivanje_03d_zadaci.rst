Наслеђивање - задаци за вежбу
=============================


.. comment

    ``НАБАЦАНО СА`` https://medium.com/javarevisited/25-software-design-interview-questions-to-crack-any-programming-and-technical-interviews-4b8237942db0, треба пробрати.

    1) Програм по коме ради "Vending Machine"

    - Има листу производа које продаје
    - листа новчића и новчаница које прихвата. 
    - јунит тест да се потврди да раде уобичајени случајеви
    - основна функционалност: 

        - прихвати убачени новац, 
        - прихвати избор, дај то што је тражено, врати кусур
        - одбиј захтев ако тренутно нема тог производа 
        - одбиј захтев ако није убачено довољно новца
        - допуњавање машине залихама


    2) URL Shortening service (као goo.gl или bit.ly). За дати *URL* треба да врати краћи, јединствен алијас. Шема базе података, колико дуго се чувају линкови ...

    3) Систем семафора на једној раскрсници (или у целом граду, тако да важније улице имају зелени талас).

    .. code::

        abstract class semafor
            - ?

    4) Лицитирање на берзи (limit order book). Систем упарује компатибилне понуде за куповину и продају, и не ти допушта да нудиш на продају јефтиније од најниже понуде за куповину, нити да нудиш да купиш скупље од највише понуде за продају. 

    5) Веб сајт попут Pastebin. Pastebin ти омогућава да залепиш текст или код и поделиш линк са другима. Не нуди онлајн едитовање текста, али може да чува било какав текст онлајн.

    6) your own Instagram. Instagram је веб апликација за дељење фотографија која има и неке филтре за поправљање квалитета твојих фотографија. Твој програм треба да омогући поствљање (upload) фотографија, таговање слика ради претраге, и неке основне филтре. Додатно, било би лепо да се омогући дељење преко друштвених мрежа.

    7) Глобално дељење фајлова, као *Google Drive* или *Dropbox*. Теба да омогући корисницима upload / view / search / share / download / remove фајлова. Апликација треба да прати дозволе за приступ фајловимма и да омогући истовремено едитовање докумената.

    8) a chat application (као WhatsApp или Facebook Messenger). Chat апликација омогућава да пошаљете поруке пријатељима. То је веза од тачке до тачке. Апликација за сваког корисника одржава листу пријатеља, приказује њихов статус и омогућава слање и примање порука са њима. WhatsApp омогућава и групни разговор (не мора да се направи). 

    9. Сервис за слање порука (као Twitter). Објавиш поруку, а твоји протиоци то виде, могу да лајкују или ре-твитују. Треба направити функције попут запрати, hashtag, tweet, delete, ... 

    10. Видео стриминг сервис као YouTube или NetFlix. 

    11. Банкомат (ATM machine) омогућава корисницима да депонују и подижу новац. Корисник може да види стање.

    12. API Rate Limiter. Неки веб сервис има интерфејс, преко ког корисници траже услуге. Ограничавач стопе захтева регулише број захтева који се прихватају на обраду, да се сервер не би загушио. Захтеви: да подржи велики број сервера, да не одбацује клијенте ако не мора, да нема велико кашњење

    13. Twitter Search?

    14. Web Crawler (као Google) посећује сајт и прати све линкове и индексира их (да би касније могли да се појаве као одговор на претрагу). 

    15. Facebook’s Newsfeed? 

    The newsfeed is an important part of Facebook which allows a user to see what’s happening around his world which includes friends and families, the pages he has liked, the group he has followed, and of-course the Facebook Ads.
    The job of the Newsfeed algorithm is to show messages which are most important for the user and which can generate high engagement. Obviously, messages from friends and family should take priority.
    If you feel not going anywhere and stuck, you can follow the solution on System Design Interviews: Grokking the System Design Interview.

    16. Yelp or Nearby Friends?

    17. Глобални сервис за превоз на позив (Uber, Grab, Ola backend). Повезује возаче и путнике.  passengers together. Како омогућити да путник види возаче који су у близини и да их резервише / закаже / букира?

    18. BookMyShow? Веб сајт који омогућава резервацију карата за биоскоп и друге догађаје.

    19. Друштвена мрежа + огласна табла за постављање питања и одговарање на њих, попут Quora, Reddit, или HackerNews?  

    20. Airbnb? Корисници оглашавају собе или апартмане за издавање, а други корисници из изнајмљују. Посебне функционалности за администраторе, објављиваче, претплатнике (subscribers).

    21. Систем лифтова. У великим пословним зградама има по 3 до 4 лифта. Логика за систем од два лифта у згради са 10 спратова.

    - стања: иде горе, иде доле, стоји
    - параметри: број кабина, борј спратова, носивост, макс. брзина
    - циљ: мимизирати укупно време чекања путника, минимизирати потрошњу струје, максимизирати throughput

    .. code::

        https://www.youtube.com/watch?v=siqiJAJWUVg&t=457s
        
        abstract class Button
            PressDown()
            IsPressed()
            
        class ElevatorButton : Button
        class HaltButton : Button
        
        class Door
            Open()
            Close()
            IsOpen()

        abstract class Scheduler // strategy
            AcceptRequest(int floor, enum direction)
            
        class ScanScheduler : Scheduler // jedna od strategija (treca)
            - HashMap, key is floor, value is reuired direction
        ...

    22. Масован e-commerce веб сајт као Amazon или Flipcart.

    23. e-commerce веб сајт помоћу микро сервиса и трансакција.

    24. Јавна гаража. Треба покрити ове случајеве употребе: издавање карте кориснику на улазу, израчунавање цене при излазу.

    .. code::

        https://www.youtube.com/watch?v=tVRyb4HaHgw
        
        class ParkingSpot 
            - id
            - reserve
            - spotType
        
        class ParkingTicket
            - ticketID
            - ParkingSpotID
            - ParkingSpotType 
            - IssueTime / VremeIzdavanja
            
        class EntryTerminal
            - ID
            - GetTicket(spotType)

        class ExitTerminal
            - ID
            - AcceptTicket()
        
        Abstract class ParkingSpotStrategy
            - GetParkingSpot()
            - ReleaseParkingSpot()
            
        class ParkingSpotNearStrategy : ParkingSpotStrategy
            - Recnik nearest: kljuc je ID terminala, vrednost je odgovarajuci minheap (svaki terminal ima svoj minheap)
            - Skup availableSpots
            - Skup reservedSpots
            
        abstract class PaymentProcessor
        class CachePaymentProcessor : PaymentProcessor
        class CreditCardPaymentProcessor : PaymentProcessor
        
        class TariffCalculator
            CalulateTariff(time, spotType)
            
        class Logger
            LogMessage

    24. Autocomplete feature like word suggestions on search engines? Scale it to millions of users?

    25. Feed posting on a social network like Facebook, Instagram, Twitter, LinkedIn, etc?
