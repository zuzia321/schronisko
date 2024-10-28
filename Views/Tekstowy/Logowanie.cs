using Schronisko.Models;
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
        public void Login(string choice,WidokTekstowy widokTekstowy,WebApplication app)
        {
            if (choice == "Gość")
            {
                menu.OpcjeGlowne(app);
            }
            else if (choice == "Wolontariusz")
            {
                HasloW(app);
            }
            else if(choice == "Administrator")
            {
                HasloA(app);
            }
            else if (choice == "Strona internetowa")
            {
                app.Run();
            }
            else if (choice == "Zakończ przeglądanie")
            {
                AnsiConsole.Markup("[#FF0000]KONIEC...[/]\n");
                Environment.Exit(0);
            }
        }
        public void HasloW(WebApplication app)
        {
            string h, id;
            AnsiConsole.Markup("Podaj indeks: ");
            int i;
            for (i = 3; i > 0; i--)
            {
                id=Console.ReadLine();
                AnsiConsole.Markup("Podaj hasło: ");
                h = Console.ReadLine();
                string plik = "volounteers.txt";
                if (File.Exists(plik))
                {
                    var linie = File.ReadAllLines(plik);
                    for (int j = 0; j < linie.Length; j++)
                    {
                        var linia = linie[j].Split(';');
                        if (linia[8] != "akceptacja")
                        {
                            Console.WriteLine("Nie zostałej jeszcze przyjęty do grona wolontariuszy, nie możesz sie zalogować");
                        }
                        else if (linia.Length == 11)
                        {
                            if (linia[9] == h && linia[10] == id)
                            {
                                string nazwa = linia[0] +' '+ linia[1];
                                menu.OpcjeWolontariusz(widokTekstowy, app, nazwa);
                                return;
                            }
                        }
                    }
                }
                if (i != 1)
                   AnsiConsole.Markup($"Podano błędne hasło lub indeks. Zostały {i - 1} próby logowania.\n Podaj indeks:"); 
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
        public void HasloA(WebApplication app )
        {
           // string haslo = "";
            AnsiConsole.Markup("Podaj hasło: ");
            int i;
            for (i = 3; i > 0; i--)
            {
                string haslo = UkryjWejscie();
                //haslo = Console.ReadLine();
                if (haslo == "netlab")
                {
                    menu.OpcjeAdmin(widokTekstowy, app);
                    AnsiConsole.Markup("Administrator");
                    break;
                }

                else if (i != 1)
                {
                    AnsiConsole.Markup($"\nBłędne hasło.Zostały {i - 1} próby.\n Podaj poprawne haslo:");
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
        static string UkryjWejscie()
        {
            string input = string.Empty;

            // Odczytywanie znaków aż do naciśnięcia Enter
            while (true)
            {
                var key = Console.ReadKey(intercept: true); // Nie pokazuj znaku
                if (key.Key == ConsoleKey.Enter) break; // Wyjdź po Enter

                input += key.KeyChar; // Dodaj znak do wejścia
                Console.Write('*'); // Wyświetl zastępczy znak
            }

            return input; // Zwróć wprowadzone dane
        }
    }
}
