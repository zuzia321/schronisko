using Schronisko.Models;
using Spectre.Console;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Schronisko.Views.Tekstowy
{
    public class WidokTekstowy
    {
        private readonly string plikW = "volounteersWait.txt";
        public Wolontariusz stworzWolontariusza()
        {
            Wolontariusz wolontariusz =new Wolontariusz();

            Console.WriteLine("WOLONTARIUSZ");


            wolontariusz.Imie = Validation.PobierzPoprawneDane("Podaj imię:", wolontariusz, nameof(wolontariusz.Imie));
            wolontariusz.Nazwisko = Validation.PobierzPoprawneDane("Podaj nazwisko:", wolontariusz, nameof(wolontariusz.Nazwisko));

            //wolontariusz.DataUrodzenia = Validation.PobierzPoprawneDane("Podaj datę urodzenia (format: YYYY-MM-DD):", wolontariusz, nameof(wolontariusz.DataUrodzenia));
            Console.Write("Podaj datę urodzenia (rrrr-mm-dd): ");
            wolontariusz.DataUrodzenia = DateTime.Parse(Console.ReadLine());

            wolontariusz.Telefon = Validation.PobierzPoprawneDane("Podaj numer telefonu:", wolontariusz, nameof(wolontariusz.Telefon));
            wolontariusz.Email = Validation.PobierzPoprawneDane("Podaj adres e-mail:", wolontariusz, nameof(wolontariusz.Email));

            //wolontariusz.Miasto = Validation.PobierzPoprawneDane("Podaj miasto:", wolontariusz, nameof(wolontariusz.Miasto));
            Console.Write("Czy mieszka w mieście? (tak/nie): ");
            string miasto = Console.ReadLine().ToLower();
            wolontariusz.Miasto = miasto == "tak";

            wolontariusz.Opis = Validation.PobierzPoprawneDane("Podaj opis:", wolontariusz, nameof(wolontariusz.Opis));

            // wolontariusz.Doswiadczenie = Validation.PobierzPoprawneDane("Czy masz doświadczenie? (true/false):", wolontariusz, nameof(wolontariusz.Doswiadczenie));
            Console.Write("Podaj doświadczenie: ");
            string doswiadczenie = Console.ReadLine().ToLower();
            wolontariusz.Doswiadczenie = doswiadczenie == "tak";

            wolontariusz.Dyspozycyjnosc = Validation.PobierzPoprawneDane("Podaj dyspozycyjność:", wolontariusz, nameof(wolontariusz.Dyspozycyjnosc));

            /*Console.WriteLine("Podaj imie");
            wolontariusz.Imie = Console.ReadLine();

            Console.Write("Podaj nazwisko: ");
            wolontariusz.Nazwisko = Console.ReadLine();

            Console.Write("Podaj datę urodzenia (rrrr-mm-dd): ");
            wolontariusz.DataUrodzenia = DateTime.Parse(Console.ReadLine());

            Console.Write("Podaj numer telefonu: ");
            wolontariusz.Telefon = Console.ReadLine();

            Console.Write("Podaj email: ");
            wolontariusz.Email = Console.ReadLine();

            Console.Write("Czy mieszka w mieście? (tak/nie): ");
            string miasto = Console.ReadLine().ToLower();
            wolontariusz.Miasto = miasto == "tak";

            Console.Write("Podaj krótki opis: ");
            wolontariusz.Opis = Console.ReadLine();

            Console.Write("Podaj doświadczenie: ");
            string doswiadczenie = Console.ReadLine().ToLower();
            wolontariusz.Doswiadczenie = doswiadczenie == "tak"; 

            Console.Write("Podaj dyspozycyjność: ");
            wolontariusz.Dyspozycyjnosc = Console.ReadLine();*/

            string daneWolontariusz = $"\n{wolontariusz.Imie};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email}";
            wolontariusz.Stan = "zaakceptowany";
            File.AppendAllText(plikW, daneWolontariusz);
            //menuAdmin.DodajWolontariusza(wolontariusz);

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
            Console.WriteLine($"Mieszka w mieście: {(wolontariusz.Miasto ? "Tak" : "Nie")}");
            Console.WriteLine($"Opis: {wolontariusz.Opis}");
            Console.WriteLine($"Doświadczenie: {(wolontariusz.Doswiadczenie ? "Tak" : "Nie")}");
            Console.WriteLine($"Dyspozycyjność: {wolontariusz.Dyspozycyjnosc}");
            AnsiConsole.Markup($"[red]{wolontariusz.Stan}[/]");
        }


        private readonly string plikZ = "Animals.txt";
        public Zwierze stworzZwierze()
        {
            Zwierze zwierze = new Zwierze();

            Console.WriteLine("ZWIERZĘ");

            Console.WriteLine("\nPodaj imie");
            zwierze.Imie = Console.ReadLine();

            Console.Write("Podaj wiek: ");
            zwierze.Wiek = Console.ReadLine();

            Console.Write("Podaj datę trafirnia do schroniska (rrrr-mm-dd): ");
            //zwierze.OdKiedyWSchronisku = DateTime.Parse(Console.ReadLine());
            zwierze.OdKiedyWSchronisku = DateOnly.Parse(Console.ReadLine());

            Console.Write("Podaj jakie to zwierze (pies, kot ...) : ");
            zwierze.RodzajZwierzecia = Console.ReadLine();

            Console.Write("Podaj gatunek: ");
            zwierze.Gatunek = Console.ReadLine();

            Console.Write("Czy dostępny do adopcji? (tak/nie): ");
            string stan = Console.ReadLine().ToLower();
            zwierze.Stan = stan ;

            Console.Write("Podaj krótki opis: ");
            zwierze.Opis = Console.ReadLine();


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
    }
}

