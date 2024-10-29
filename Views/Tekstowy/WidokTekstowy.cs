using Schronisko.Models;
using Spectre.Console;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Schronisko.Views.Tekstowy
{
    public class WidokTekstowy
    {
        private readonly poprawnoscAdopcji poprawnosc = new poprawnoscAdopcji();
        private readonly poprawnoscZwierze poprawnoscZwierze = new poprawnoscZwierze();
        private readonly poprawnoscWolontariusza poprawnoscW=new poprawnoscWolontariusza();
        private readonly string plikW = "volounteers.txt";

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
       
        public Wolontariusz stworzWolontariusza(ref int pom)
        {
            Wolontariusz wolontariusz =new Wolontariusz();

            Console.WriteLine("WOLONTARIUSZ");

            wolontariusz.Imie = textInfo.ToTitleCase(Validation.PobierzPoprawneDane("Podaj imię:", wolontariusz, nameof(wolontariusz.Imie)).ToLower());
            wolontariusz.Nazwisko = textInfo.ToTitleCase(Validation.PobierzPoprawneDane("Podaj nazwisko:", wolontariusz, nameof(wolontariusz.Nazwisko)).ToLower());

            DateOnly dataUrodzenia;
            bool isValidDate = false;

            while (!isValidDate)
            {
                Console.Write("Podaj datę urodzenia (rrrr-mm-dd) lub (dd-mm-rrrr): ");
                string input = Console.ReadLine();
                if (DateOnly.TryParse(input, out dataUrodzenia))
                {
                    wolontariusz.DataUrodzenia = dataUrodzenia;
                    isValidDate = true;
                }
                else
                    Console.WriteLine("Niepoprawny format daty. Proszę podać datę w formacie rrrr-mm-dd.");
            }
            //wolontariusz.DataUrodzenia = Validation.PobierzPoprawneDane("Podaj datę urodzenia (format: YYYY-MM-DD):", wolontariusz, nameof(wolontariusz.DataUrodzenia));
            /* Console.Write("Podaj datę urodzenia (rrrr-mm-dd): ");
             wolontariusz.DataUrodzenia = DateOnly.Parse(Console.ReadLine());
             if(!poprawnoscW.wiek(wolontariusz.DataUrodzenia))
             {
                 Console.WriteLine("Nie możesz być wolontariuszem. Nie masz 18 lat");
                 Console.WriteLine("Czy chcesz wrócić do menu? (tak/nie): ");
                 string wybor = Console.ReadLine()?.ToLower();
                 if (wybor == "tak")
                 {
                     pom = 1;
                     return null;
                 }
                 else 
                     Environment.Exit(0);
             }*/

            wolontariusz.Telefon = Validation.PobierzPoprawneDane("Podaj numer telefonu:", wolontariusz, nameof(wolontariusz.Telefon));
            wolontariusz.Email = Validation.PobierzPoprawneDane("Podaj adres e-mail:", wolontariusz, nameof(wolontariusz.Email));

            Console.Write("Czy mieszkasz w Białymstoku (tak/nie): ");
            wolontariusz.Miasto = Console.ReadLine();
            while (!poprawnosc.poprawnoscDoswiadczenie(wolontariusz.Miasto))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : tak lub nie");
                Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
                wolontariusz.Miasto = Console.ReadLine();
            }

            wolontariusz.Opis = Validation.PobierzPoprawneDane("Podaj swoje zainteresowania:", wolontariusz, nameof(wolontariusz.Opis));

            Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
            wolontariusz.Doswiadczenie = Console.ReadLine();
            while (!poprawnosc.poprawnoscDoswiadczenie(wolontariusz.Doswiadczenie))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : tak lub nie");
                Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
                wolontariusz.Doswiadczenie = Console.ReadLine();
            }

            wolontariusz.Haslo = Validation.PobierzPoprawneDane("Podaj hasło: ", wolontariusz, nameof(wolontariusz.Haslo));
            var wszystkieLinie = File.ReadAllLines(plikW).ToList();
            int dlugosc = wszystkieLinie.Count;
            var linia = wszystkieLinie[dlugosc - 1].Split(';');

            int index = (int.Parse(linia[10])) + 1;
            wolontariusz.Id=index;
            //string daneWolontariusz = $"\n{wolontariusz.Imie};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email}";
            string daneWolontariusz = $"{wolontariusz.Imie};{wolontariusz.Nazwisko};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email};{wolontariusz.Miasto};{wolontariusz.Opis};{wolontariusz.Doswiadczenie};{wolontariusz.Stan};{wolontariusz.Haslo};{wolontariusz.Id}\n";
            File.AppendAllText(plikW, daneWolontariusz);

            return wolontariusz;
        }
        public void WyswietlWolontariusza(Wolontariusz wolontariusz)
        {
            Console.Clear();
            //int index = File.ReadAllLines(plikW).Length;
            /*
            var wszystkieLinie = File.ReadAllLines(plikW).ToList();
            int dlugosc = wszystkieLinie.Count;
            var linia=wszystkieLinie[dlugosc - 1].Split(';');

            int index = (int.Parse(linia[10]))+1;
            */
            AnsiConsole.Markup($"[Deeppink1]Twoj wygenerowany indeks do logowania:[bold] {wolontariusz.Id}[/][/] ");
            Console.WriteLine("\nDane wolontariusza:");
            Console.WriteLine($" Imię: {wolontariusz.Imie}");
            Console.WriteLine($" Nazwisko: {wolontariusz.Nazwisko}");
            Console.WriteLine($" Data urodzenia: {wolontariusz.DataUrodzenia.ToShortDateString()}");
            Console.WriteLine($" Telefon: {wolontariusz.Telefon}");
            Console.WriteLine($" Email: {wolontariusz.Email}");
            Console.WriteLine($" Mieszka w mieście: {wolontariusz.Miasto}");
            Console.WriteLine($" Zainteresowania: {wolontariusz.Opis}");
            Console.WriteLine($" Doświadczenie: {wolontariusz.Doswiadczenie}");
            AnsiConsole.Markup($" [#FF0000]{wolontariusz.Stan}[/]");
        }

        private readonly string plikZ = "Animals.txt";
        public Zwierze stworzZwierze()
        {
            Zwierze zwierze = new Zwierze();

            Console.WriteLine("ZWIERZĘ");

            zwierze.Imie = textInfo.ToTitleCase(Validation.PobierzPoprawneDane("Podaj imię:", zwierze, nameof(zwierze.Imie)).ToLower());
            zwierze.Wiek = Validation.PobierzPoprawneDane("Podaj wiek (jeżeli mniej niż rok podaj ułamek x/12): ", zwierze, nameof(zwierze.Wiek));

            Console.Write("Podaj datę trafienia do schroniska (rrrr-mm-dd): ");
            zwierze.OdKiedyWSchronisku = DateOnly.Parse(Console.ReadLine());

            Console.Write("Podaj jakie to zwierze (pies, kot ...): ");
            zwierze.RodzajZwierzecia = Console.ReadLine();
            while (!poprawnoscZwierze.poprawnoscRodzaj(zwierze.RodzajZwierzecia))
            {
                AnsiConsole.Markup("[#FF0000]Rodzaj zwierzęcia obejmuje taką listę[/]: pies, kot, jaszczurka, ptak, szczur, mysz, chomik, królik, świnka morska, szynszyla, żółw, krowa, świnia, kura, owca, koza, kaczka, gęś");
                Console.Write("Podaj jakie to zwierze: ");
                zwierze.RodzajZwierzecia = Console.ReadLine();
            }

            zwierze.Gatunek = Validation.PobierzPoprawneDane("Podaj gatunek: ", zwierze, nameof(zwierze.Gatunek));

            Console.Write("Czy jest już dostępny do adopcji? (tak/nie): ");
            zwierze.Stan = Console.ReadLine();
            while (!poprawnoscZwierze.poprawnoscStan(zwierze.Stan))
            {
                AnsiConsole.Markup("[#FF0000]Poprawna forma odpowiedzi : tak lub nie[/]");
                Console.Write("Czy jest już dostępny do adopcji? (tak/nie): ");
                zwierze.Stan = Console.ReadLine();
            }

            zwierze.Opis=Validation.PobierzPoprawneDane("Podaj opis: ",zwierze,nameof(zwierze.Opis));

            string daneZwierze = $"{zwierze.Imie};{zwierze.Wiek};{zwierze.OdKiedyWSchronisku};{zwierze.RodzajZwierzecia};{zwierze.Gatunek};{zwierze.Stan};{zwierze.Opis}\n";
            File.AppendAllText(plikZ, daneZwierze);

            return zwierze;

        }

        public void WyswietlZwierze(Zwierze zwierze)
        {
            Console.Clear();
            AnsiConsole.Markup("[bold][on Pink1][Deeppink3]FUTRZANA FERAJNA: ADMIN[/][/][/]\n\n");
            Console.WriteLine("\nDane zwierzęcia:");
            Console.WriteLine($"Imię: {zwierze.Imie}");
            Console.WriteLine($"Wiek: {zwierze.Wiek}");
            Console.WriteLine($"Data od kiedy w schronisku: {zwierze.OdKiedyWSchronisku.ToShortDateString()}");
            Console.WriteLine($"Rodzaj zwierzęcia: {zwierze.RodzajZwierzecia}");
            Console.WriteLine($"Gatunek: {zwierze.Gatunek}");
            Console.WriteLine($"Dostępny do adopcji? : {zwierze.Stan }");
            Console.WriteLine($"Opis: {zwierze.Opis}");
        }
        
        public Adopcja stworzAdopcje(ref int pom)
        {
            Adopcja adopcja = new Adopcja();
            adopcja.Imie =Validation.PobierzPoprawneDane("Podaj imię:", adopcja, nameof(adopcja.Imie));
            adopcja.Nazwisko = Validation.PobierzPoprawneDane("Podaj nazwisko:", adopcja, nameof(adopcja.Nazwisko));

            Console.Write("Podaj swój wiek: ");
            adopcja.Wiek = Console.ReadLine();
            if (!poprawnosc.poprawnoscWiek(adopcja.Wiek))
            {
                Console.WriteLine("Nie możesz adoptować zwierzęcia. Nie masz 18 lat. Porozmawiaj o tym z rodzicem. Jak się zgodzi prosimy rodzica o uzupelnienie formularza");
                Console.WriteLine("Czy chcesz wrócić do menu? (tak/nie): ");
                string wybor = Console.ReadLine()?.ToLower();
                if (wybor == "tak")
                {
                    pom = 1;
                    return null;
                }
                else
                    Environment.Exit(0);
            }

            adopcja.Telefon = Validation.PobierzPoprawneDane("Podaj numer telefonu:", adopcja, nameof(adopcja.Telefon));
            adopcja.Email = Validation.PobierzPoprawneDane("Podaj adres e-mail:", adopcja, nameof(adopcja.Email));
            while(!poprawnosc.poprawnoscEmail(adopcja.Email))
            {
                Console.WriteLine("Podaj poprawny email. Nie istnieje taka domena maila");
                Console.Write("Podaj adres e-mail: ");
                adopcja.Email = Console.ReadLine();
            }
          
            Console.Write("Podaj gdzie mieszkasz (dom/mieszkanie) : ");
            adopcja.TypMieszkania = Console.ReadLine();
            while (!poprawnosc.poprawnoscTypMieszkania(adopcja.TypMieszkania))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : dom lub mieszkanie");
                Console.Write("Podaj gdzie mieszkasz (dom/mieszkanie) : ");
                adopcja.TypMieszkania = Console.ReadLine();
            }

            Console.Write("Czy posiadasz ogród (tak/nie): ");
            adopcja.Ogrod = Console.ReadLine();
            while (!poprawnosc.poprawnoscOgrod(adopcja.Ogrod))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : tak lub nie");
                Console.Write("Czy posiadasz ogród (tak/nie): ");
                adopcja.Ogrod = Console.ReadLine();
            }

            adopcja.IloscOsob = Validation.PobierzPoprawneDane("Podaj ilość osób mieszkających z tobą:", adopcja, nameof(adopcja.IloscOsob));
            
            Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
            adopcja.Doswiadczenie = Console.ReadLine();
            while (!poprawnosc.poprawnoscDoswiadczenie(adopcja.Doswiadczenie))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : tak lub nie");
                Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
                adopcja.Doswiadczenie = Console.ReadLine();
            }

            adopcja.AktualneZwierzeta = Validation.PobierzPoprawneDane("Podaj aktualnie posiadane zwierzęta:", adopcja, nameof(adopcja.AktualneZwierzeta));

            return adopcja;
        }
    }
}

