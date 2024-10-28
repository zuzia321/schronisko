using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Schronisko.Controllers;
using Spectre.Console;
using System.Threading.Tasks;

namespace Schronisko.Views.Tekstowy
{
    public class Menu
    {
        private readonly WidokTekstowy widokTekstowy;
        private readonly MenuGlowne menuGlowne;
        private readonly MenuAdmin menuAdmin;
        private readonly MenuWolontariusz menuWolontariusz;
        private readonly MenuKalendarz menuKalendarz;

        public Menu(WidokTekstowy _widokTekstowy, MenuGlowne _menuGlowne, MenuAdmin _menuAdmin, MenuWolontariusz _menuWolontariusz, MenuKalendarz _menuKalendarz)
        {
            widokTekstowy = _widokTekstowy;
            menuGlowne = _menuGlowne;
            menuAdmin = _menuAdmin;
            menuWolontariusz = _menuWolontariusz;
            menuKalendarz = _menuKalendarz;
            menuKalendarz = _menuKalendarz;
        }

        public void OpcjeGlowne(WebApplication app)
        {
            var options = new List<string>
                {
                    "Formularz Wolontariusza",
                    "Zwierzęta do adopcji",
                    "Informacje o schronisku",
                    "Darowizna",
                    "Powrót",
                    "Zakończ przeglądanie"
                };
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Markup("[bold][on Pink1][Deeppink3]FUTRZANA FERAJNA[/][/][/]\n\n");

                var wybor = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Wybierz opcję:")
                            .AddChoices(options)
                            .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));

                menuGlowne.MenuGlowneOpis(wybor, widokTekstowy, app);
                if (wybor == "Powrót")
                {
                    break; 
                }
            }
        }
        public void OpcjeWolontariusz(WidokTekstowy widokTekstowy, WebApplication app,string nazwa)
        {
            var options = new List<string>
            {
                "Wyświetl Kalendarz",
                "Wyloguj",
                "Zakończ przeglądanie"
            };
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Markup("[bold][on Pink1][Deeppink3]FUTRZANA FERAJNA: WOLONTARIUSZ[/][/][/]\n\n");

                var wybor = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Wybierz opcję:")
                            .AddChoices(options)
                            .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));

                menuWolontariusz.MenuWolontariuszOpis(wybor, widokTekstowy, app,menuKalendarz,nazwa);
                if (wybor == "Wyloguj")
                {
                    break;
                }
            }
        }
        public void OpcjeAdmin(WidokTekstowy widokTekstowy, WebApplication app)
        {
            var options = new List<string>
            {
                
                "Dodaj Zwierzaka",
                "Edytuj zwierzaka",
                "Usuń Zwierzaka",
                "Dodaj Wolontariusza",
                "Edytuj Wolontariusza",
                "Usuń Wolontariusza",
                "Wyloguj",
                "Zakończ przeglądanie"
            };
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Markup("[bold][on Pink1][Deeppink3]FUTRZANA FERAJNA: ADMIN[/][/][/]\n\n");

                var wybor = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Wybierz opcję:")
                            .AddChoices(options)
                            .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));

                menuAdmin.MenuAdminOpis(wybor, widokTekstowy, app);
                if (wybor == "Wyloguj")
                {
                    break;
                }
            }
        }
    }
}
