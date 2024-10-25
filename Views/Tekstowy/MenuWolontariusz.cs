using Schronisko.Models;
using Spectre.Console;

namespace Schronisko.Views.Tekstowy
{
    public class MenuWolontariusz
    {
        public void MenuWolontariuszOpis(string choice, WidokTekstowy widokTekstowy, WebApplication app,MenuKalendarz menuKalendarz,string nazwa)
        {
            if(choice=="Wyświetl Kalendarz")
            {
                menuKalendarz.WyswietlKalendarz(nazwa);
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
