using Spectre.Console;
using System.Reflection.Emit;

namespace Schronisko.Views.Tekstowy
{
    public class Logowanie
    {
        private readonly WidokTekstowy widokTekstowy;
        private readonly Menu menu;
        public Logowanie( Menu _menu, WidokTekstowy _widokTekstowy)
        {
            menu = _menu;
            widokTekstowy = _widokTekstowy; 
        }
       /* public void MenuLogowania(WebApplication app)
        {
            var opcje = new List<string>
            {
                //weryfikacja
                "Gość",
                "Wolontariusz",
                "Administrator",
                "Zakończ przeglądanie"
            };
            while (true)
            {
                AnsiConsole.Clear();

                var font = FigletFont.Load("Small.flf");

                AnsiConsole.Write(
                    new FigletText(font, "FUTRZANA FERAJNA\n\n")
                        .Centered()
                        .Color(Color.DeepPink3));

                //AnsiConsole.Markup("[bold][on Pink1][Deeppink4]FUTRZANA FERAJNA[/][/][/]\n\n");

                var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                .Title("Wybierz opcję:")
                            .AddChoices(opcje)
                            .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));
                Login(choice, widokTekstowy, app);
            }
        }*/
        public void Login(string choice,WidokTekstowy widokTekstowy,WebApplication app)
        {
            string haslo;
            if (choice == "Gość")
            {
                menu.OpcjeGlowne(app);
               // AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
                //Console.ReadKey();
            }
            else if (choice == "Wolontariusz")
            {
                AnsiConsole.Markup("Podaj hasło: ");
                int i;
                for (i = 3; i > 0; i--)
                {
                    haslo = Console.ReadLine();
                    if (haslo == "student")
                    {
                        menu.OpcjeWolontariusz(widokTekstowy, app);
                        AnsiConsole.Markup("wolontariusz");
                        break;
                    }
                    else if (i != 1)
                    {
                        AnsiConsole.Markup($"Bledne haslo.Zostały {i - 1} proby.\n Podaj poprawne haslo:");
                    }
                }
                Console.Clear();
                if (i == 0)
                {
                    Thread.Sleep(2000);
                    for (int j = 3; j > 0; j--)
                    {
                        Console.WriteLine(j);
                        Thread.Sleep(1000);
                    }
                    var image = new CanvasImage("bomba.png");
                    image.MaxWidth(48);
                    AnsiConsole.Write(image);
                    AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
            else if (choice == "Administrator")
            {
                AnsiConsole.Markup("Podaj hasło: ");
                haslo = Console.ReadLine();
                if (haslo == "netlab")
                {
                    AnsiConsole.Markup("admin");
                    menu.OpcjeAdmin(widokTekstowy, app);
                }
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
        }
    }
}
