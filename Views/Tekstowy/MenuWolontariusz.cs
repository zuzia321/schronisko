using Schronisko.Models;
using Spectre.Console;

namespace Schronisko.Views.Tekstowy
{
    public class MenuWolontariusz
    {
        public void MenuWolontariuszOpis(string choice, WidokTekstowy widokTekstowy, WebApplication app,MenuKalendarz menuKalendarz)
        {
            if (choice == "Dodaj Dzień Pracy")
            {
               /// menuKalendarz.WyswietlKalendarz();
                Console.WriteLine("Podaj datę (format: yyyy-mm-dd):");
                var dataInput = Console.ReadLine();
                DateTime data;
                if (DateTime.TryParse(dataInput, out data))
                {
                    Console.WriteLine("Podaj opis dnia pracy:");
                    string opis = Console.ReadLine();
                   // menuKalendarz.DodajDzieńPracy(data, opis); // przekazujemy wartości
                }
                else
                {
                    Console.WriteLine("Niepoprawny format daty!");
                }
            }
            if(choice=="Wyświetl Kalendarz")
            {
                //int rok=DateTime.Now.Year;
                //int miesiac=DateTime.Now.Month;
                //int rok = AnsiConsole.Ask<int>("Podaj rok:");
                //int miesiac= AnsiConsole.Ask<int>("Podaj miesiąc:");
                menuKalendarz.WyswietlKalendarz();// rok,miesiac);
            }
            else if (choice == "Wyloguj")
            {
                return;
            }
            else if (choice == "Zakończ przeglądanie")
            {
                AnsiConsole.Markup("[bold red]KONIEC...[/]\n");
                Environment.Exit(0);
            }
            AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }
}
