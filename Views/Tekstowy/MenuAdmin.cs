using Schronisko.Controllers;
using Spectre.Console;

namespace Schronisko.Views.Tekstowy
{
    public class MenuAdmin
    {
        public void MenuAdminOpis(string choice, WidokTekstowy widokTekstowy, WebApplication app)
        {
            if (choice == "Dodaj Zwierzaka")
            {
                //formularz zwierząt
                var zwierzeController = new ZwierzeController(widokTekstowy);
                zwierzeController.StworzIWyswietlZwierze();
            }
            else if (choice == "Edytuj zwierzaka")
            {

            }
            else if (choice == "Usuń Zwierzaka")
            {

            }
            else if (choice == "Dodaj Wolontariusza")
            {

            }
            else if (choice == "Edytuj Wolontariusza")
            {

            }
            else if (choice == "Usuń Wolontariusza")
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
