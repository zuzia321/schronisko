using Spectre.Console;

namespace Schronisko.Views.Tekstowy
{
    public class MenuWolontariusz
    {
        public void MenuWolontariuszOpis(string choice, WidokTekstowy widokTekstowy, WebApplication app)
        {
            if (choice == "Dodaj Dzień Pracy")
            {

            }
            else if (choice == "Wyloguj")
            {
                return;
            }
            else if (choice == "Strona internetowa")
            {
                app.Run();
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
