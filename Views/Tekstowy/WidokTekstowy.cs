using Schronisko.Models;
using Spectre.Console;
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
        public Wolontariusz stworzWolontariusza()
        {
            Wolontariusz wolontariusz =new Wolontariusz();

            Console.WriteLine("WOLONTARIUSZ");

            wolontariusz.Imie = Validation.PobierzPoprawneDane("Podaj imię:", wolontariusz, nameof(wolontariusz.Imie));
            wolontariusz.Nazwisko = Validation.PobierzPoprawneDane("Podaj nazwisko:", wolontariusz, nameof(wolontariusz.Nazwisko));

            //wolontariusz.DataUrodzenia = Validation.PobierzPoprawneDane("Podaj datę urodzenia (format: YYYY-MM-DD):", wolontariusz, nameof(wolontariusz.DataUrodzenia));
            Console.Write("Podaj datę urodzenia (rrrr-mm-dd): ");
            wolontariusz.DataUrodzenia = DateTime.Parse(Console.ReadLine());
            if(!poprawnoscW.wiek(wolontariusz.DataUrodzenia))
            {
                Console.WriteLine("Nie możesz byc wolontariuszem. Nie masz 18 lat");
                Console.WriteLine("Czy chcesz wrócić do menu? (tak/nie): ");
                string wybor = Console.ReadLine()?.ToLower();
                if (wybor == "tak")
                {
                    // Przerywamy rejestrację i wracamy do poprzedniego menu
                    return null;
                }
                else 
                    Environment.Exit(0);
            }

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

            wolontariusz.Opis = Validation.PobierzPoprawneDane("Podaj opis:", wolontariusz, nameof(wolontariusz.Opis));

            Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
            wolontariusz.Doswiadczenie = Console.ReadLine();
            while (!poprawnosc.poprawnoscDoswiadczenie(wolontariusz.Doswiadczenie))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : tak lub nie");
                Console.Write("Czy posiadasz doświadczenie ze zwierzętami (tak/nie): ");
                wolontariusz.Doswiadczenie = Console.ReadLine();
            }

            wolontariusz.Dyspozycyjnosc = Validation.PobierzPoprawneDane("Podaj dyspozycyjność:", wolontariusz, nameof(wolontariusz.Dyspozycyjnosc));

            //string daneWolontariusz = $"\n{wolontariusz.Imie};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email}";
            string daneWolontariusz = $"\n{wolontariusz.Imie};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email};{wolontariusz.Miasto};{wolontariusz.Opis};{wolontariusz.Dyspozycyjnosc};{wolontariusz.Doswiadczenie};{wolontariusz.Stan}";
            File.AppendAllText(plikW, daneWolontariusz);
           
            return wolontariusz;

        }

        public void WyswietlWolontariusza(Wolontariusz wolontariusz)
        {
            Console.WriteLine("\nDane wolontariusza:");
            Console.WriteLine($"Imię: {wolontariusz.Imie}");
            Console.WriteLine($"Nazwisko: {wolontariusz.Nazwisko}");
            Console.WriteLine($"Data urodzenia: {wolontariusz.DataUrodzenia.ToShortDateString()}");
            Console.WriteLine($"Telefon: {wolontariusz.Telefon}");
            Console.WriteLine($"Email: {wolontariusz.Email}");
            Console.WriteLine($"Mieszka w mieście: {wolontariusz.Miasto}");
            Console.WriteLine($"Opis: {wolontariusz.Opis}");
            Console.WriteLine($"Doświadczenie: {wolontariusz.Doswiadczenie}");
            Console.WriteLine($"Dyspozycyjność: {wolontariusz.Dyspozycyjnosc}");
            AnsiConsole.Markup($"[red]{wolontariusz.Stan}[/]");
        }


        private readonly string plikZ = "Animals.txt";
        public Zwierze stworzZwierze()
        {
            Zwierze zwierze = new Zwierze();

            Console.WriteLine("ZWIERZĘ");

            zwierze.Imie = Validation.PobierzPoprawneDane("Podaj imie: ", zwierze, nameof(zwierze.Imie));
            zwierze.Wiek = Validation.PobierzPoprawneDane("Podaj wiek (jeżeli mniej niż rok podaj ułamek x/12): ", zwierze, nameof(zwierze.Wiek));

            Console.Write("Podaj datę trafirnia do schroniska (rrrr-mm-dd): ");
            zwierze.OdKiedyWSchronisku = DateOnly.Parse(Console.ReadLine());

            Console.Write("Podaj jakie to zwierze (pies, kot ...): ");
            zwierze.RodzajZwierzecia = Console.ReadLine();
            while (!poprawnoscZwierze.poprawnoscStan(zwierze.RodzajZwierzecia))
            {
                Console.WriteLine("Rodzaj zwierzęcia obejmuje taką listę: pies, kot, jaszczurka, ptak, szczur, mysz, chomik, królik, świnka morska, szynszyla, żółw, krowa, świnia, kura, owca, koza, kaczka, gęś");
                Console.Write("Podaj jakie to zwierze: ");
                zwierze.RodzajZwierzecia = Console.ReadLine();
            }
            Console.Write(" : ");
            zwierze.RodzajZwierzecia = Console.ReadLine();

            zwierze.Gatunek = Validation.PobierzPoprawneDane("Podaj gatunek: ", zwierze, nameof(zwierze.Gatunek));

            Console.Write("Czy jest już dostępny do adopcji? (tak/nie): ");
            zwierze.Stan = Console.ReadLine();
            while (!poprawnoscZwierze.poprawnoscStan(zwierze.Stan))
            {
                Console.WriteLine("Poprawna forma odpowiedzi : tak lub nie");
                Console.Write("Czy jest już dostępny do adopcji? (tak/nie): ");
                zwierze.Stan = Console.ReadLine();
            }

            zwierze.Opis=Validation.PobierzPoprawneDane("Podaj opis: ",zwierze,nameof(zwierze.Opis));

            string daneZwierze = $"\n{zwierze.Imie};{zwierze.Wiek};{zwierze.OdKiedyWSchronisku};{zwierze.RodzajZwierzecia};{zwierze.Gatunek};{zwierze.Stan};{zwierze.Opis}";
            File.AppendAllText(plikZ, daneZwierze);

            return zwierze;

        }

        public void WyswietlZwierze(Zwierze zwierze)
        {
            Console.WriteLine("\nDane zwierzęcia:");
            Console.WriteLine($"Imię: {zwierze.Imie}");
            Console.WriteLine($"Wiek: {zwierze.Wiek}");
            Console.WriteLine($"Data od kiedy w schronisku: {zwierze.OdKiedyWSchronisku.ToShortDateString()}");
            Console.WriteLine($"Rodzaj zwierzęcia: {zwierze.RodzajZwierzecia}");
            Console.WriteLine($"Gatunek: {zwierze.Gatunek}");
            Console.WriteLine($"Dostępny do adopcji? : {zwierze.Stan }");
            Console.WriteLine($"Opis: {zwierze.Opis}");
        }

        public Adopcja stworzAdopcje()
        {
            Adopcja adopcja = new Adopcja();

            Console.WriteLine("FORLMULARZ ADOPCYJNY");

            adopcja.Imie = Validation.PobierzPoprawneDane("Podaj imię:", adopcja, nameof(adopcja.Imie));
            adopcja.Nazwisko = Validation.PobierzPoprawneDane("Podaj nazwisko:", adopcja, nameof(adopcja.Nazwisko));
            adopcja.Telefon = Validation.PobierzPoprawneDane("Podaj numer telefonu:", adopcja, nameof(adopcja.Telefon));
            adopcja.Email = Validation.PobierzPoprawneDane("Podaj adres e-mail:", adopcja, nameof(adopcja.Email));
          
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

