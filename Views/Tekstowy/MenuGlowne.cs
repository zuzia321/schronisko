using Schronisko.Controllers;
using Spectre.Console;
using Schronisko.Models;
namespace Schronisko.Views.Tekstowy
{
    public class MenuGlowne
    {
        private Zwierzeta _zwierzeta = new Zwierzeta();
        static ZwierzetaFunkcje _zwierzetaFunkcje = new ZwierzetaFunkcje();
        public void MenuGlowneOpis(string choice, WidokTekstowy widokTekstowy, WebApplication app)
        {

            if (choice == "Formularz Wolontariusza")
            {
                AnsiConsole.Markup("[Deeppink1]FORMULARZ[/]\n");
                var wolontariuszController = new WolontariuszController(widokTekstowy);
                int pom = 0;
                wolontariuszController.StworzIWyswietlWolontariusza(pom);
                AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey(); 
            }
            else if (choice == "Zwierzęta do adopcji")
            {
                AnsiConsole.Markup("[Deeppink1]ZWIERZĘTA[/]\n");
                //_zwierzetaFunkcje.FiltrowanieSortowanieZwierza(_zwierzeta,null,null,null,null);
                _zwierzeta.WyswietlZwierzeta(widokTekstowy);
            }
            else if (choice == "Informacje o schronisku")
            {
                AnsiConsole.Markup("[Deeppink1]INFORMACJE O SCHRONISKU[/]\n");
                AnsiConsole.Markup("Nasze schronisko to miejsce, gdzie każde zwierzę otrzymuje miłość, opiekę i szansę na nowe życie. Jesteśmy organizacją, która od lat z pasją i zaangażowaniem wspiera porzucone oraz potrzebujące zwierzęta, zapewniając im bezpieczne schronienie, opiekę weterynaryjną i możliwość adopcji do kochających domów.\r\n\r\nW naszym schronisku staramy się stworzyć warunki, które jak najbardziej przypominają domowe otoczenie, dzięki czemu nasi podopieczni mogą czuć się komfortowo. Każde zwierzę jest dla nas wyjątkowe – znamy ich charaktery, potrzeby i historie. Pracujemy z wolontariuszami oraz specjalistami, aby zapewnić im najlepszą opiekę i rehabilitację, jeśli tego wymagają.\r\n\r\nOprócz opieki nad zwierzętami prowadzimy również programy edukacyjne, które mają na celu zwiększenie świadomości na temat odpowiedzialnej opieki nad zwierzętami, zapobieganie bezdomności i promowanie adopcji zamiast kupowania zwierząt.\r\n\r\nDzięki zaangażowaniu naszej społeczności oraz wsparciu darczyńców, jesteśmy w stanie nieść pomoc tym, którzy jej najbardziej potrzebują. Zapraszamy do adopcji, wsparcia oraz udziału w wolontariacie – razem możemy zmieniać los zwierząt na lepsze!");
                AnsiConsole.Markup("\n\n\n[#FC77BE]KONTAKT[/]\n\ttel: 123456789\n\temail: futrzanaferajna@gmail.com\n\tadres: ul. AnimalPlanet 27, Białystok, 15-967\n");
                
                AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
            else if (choice == "Darowizna")
            {
                AnsiConsole.Markup("[Deeppink1]DAROWIZNA[/]\n");
                AnsiConsole.Markup("[bold][#FC77BE]\nPomóż nam, pomagając zwierzętom!\n[/][/]");
                AnsiConsole.Markup("Schronisko dla zwierząt to miejsce, w którym każdy dzień jest wyzwaniem, ale także ogromną radością. Dzięki Twojemu wsparciu możemy dać schronienie, opiekę i miłość wielu zwierzakom, które znalazły się w trudnej sytuacji. Każda darowizna, niezależnie od jej wysokości, ma ogromne znaczenie i wpływa na życie naszych podopiecznych.");
                AnsiConsole.Markup("\n\n[#FC77BE]Jak możesz pomóc?[/]\n[#FABBDD]Przekaż darowiznę na nasze konto bankowe:[/]");
                AnsiConsole.Markup("\n[underline]\tNazwa schroniska:[/] Futrzasta Ferajna\n\t[underline]Numer konta:[/] 12 3456 7890 1234 5678 9000 0001\n\t[underline]Bank:[/] mBank\n\t[underline]Tytuł przelewu:[/] Darowizna na rzecz schroniska");
                AnsiConsole.Markup("\n\n[#FABBDD]Lista rzeczy, które są nam potrzebne:[/]\nAby nasze schronisko mogło sprawnie funkcjonować i zapewnić naszym zwierzętom odpowiednią opiekę, potrzebujemy:");
                AnsiConsole.Markup("\n\t- Karma dla psów i kotów (sucha i mokra)\n\t- Przysmaki dla zwierząt\n\t- Zabawki dla psów i kotów (piłki, gryzaki, myszki)\n\t- Kocyk i kołdry (do stworzenia ciepłych miejsc do spania)\n\t- Legowiska (dla psów i kotów)\n\t- Środki czystości (detergenty, worki na odpady)\n\t- Akcesoria do pielęgnacji (szczotki, szampony, nożyczki do pazurów)\n\t- Klatki transportowe (dla bezpiecznego przewozu zwierząt)\n\t- Świeże ręczniki (do wycierania i ogrzewania zwierząt po kąpieli)\n\t- Pojemniki na wodę i karmę\n");
                
                AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
            else if(choice=="Powrót")
            {
                return;
            }
            else if (choice == "Zakończ przeglądanie")
            {
                AnsiConsole.Markup("[#FF0000]KONIEC...[/]\n");
                Environment.Exit(0);
            }  
        }
    }
}
