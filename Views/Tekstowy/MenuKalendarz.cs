using Microsoft.Extensions.Logging;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Schronisko.Views.Tekstowy
{
    public class MenuKalendarz
    {
        private List<(DateTime, string)> dniPracy = new List<(DateTime, string)>();
        private const int MaxOsob = 4;
        private const string plik = "Calendar.txt";

        public MenuKalendarz()
        {
            Wczytaj(); // Wczytaj wydarzenia z pliku podczas inicjalizacji
        }

        // Metoda do zapisywania wydarzeń do pliku
        public void Zapisz()
        {
            using (var writer = new StreamWriter(plik))
            {
                foreach (var (data,nazwa) in dniPracy)
                {
                    writer.WriteLine($"{data:yyyy-MM-dd};{nazwa}");
                }
            }
        }

        // Metoda do wczytywania wydarzeń z pliku
        public void Wczytaj()
        {
            if (File.Exists(plik))
            {
                using (var reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length == 2 &&
                            DateTime.TryParse(parts[0], out DateTime data))
                        {
                            dniPracy.Add((data, parts[1]));
                        }
                    }
                }
            }
        }


        public void DodajDzien(DateTime data,string nazwa)
        {
            bool wpisany = false;
            foreach (var dzien in dniPracy)
            {
                if (dzien.Item1 == data) 
                {
                    if (dzien.Item2 == nazwa) 
                    {
                        wpisany = true;
                        break;
                    }
                }
            }
            if (wpisany)
                AnsiConsole.Markup($"\n[#FF0000]{nazwa} już pracujesz w ten dzień !!![/]\n".ToUpper());
            else
            {
                dniPracy.Add((data, nazwa));
                Console.WriteLine($"\n{data:yyyy-MM-dd}, {nazwa} - Dzień pracy został dodany.");
                Zapisz();
            }
        }

        public void WyswietlKalendarz(string nazwa)
        {
            while (true)
            {
                int rok = AnsiConsole.Ask<int>("Podaj rok:");
                int miesiac = AnsiConsole.Ask<int>("Podaj miesiąc:");
                if (rok > DateTime.Now.Year || (rok == DateTime.Now.Year && miesiac >= DateTime.Now.Month))
                {
                    WybierzDate(rok, miesiac, nazwa);
                    break;
                }
                else
                    AnsiConsole.Markup("[#FF0000]Nie cofamy sie do przeszłości kolego[/]\n");
            }
        }

        public void WybierzDate(int rok, int miesiac, string nazwa,int wybranyDzien = 1)
        {
            int dniWMiesiacu = DateTime.DaysInMonth(rok, miesiac);

            while (true)
            {
                Console.Clear();
                var kalendarz = new Spectre.Console.Calendar(rok, miesiac);
                kalendarz.Culture("pl-PL");
                // Dodajemy wydarzenia z listy dni pracy, które pasują do wybranego miesiąca
                foreach (var (data, opis) in dniPracy)
                {
                    if (data.Year == rok && data.Month == miesiac)
                    {
                        kalendarz.AddCalendarEvent(data.Year, data.Month, data.Day);
                    }
                }

                // Podświetlamy wybrany dzień
                kalendarz.HighlightStyle(Style.Parse("magenta bold")).AddCalendarEvent(rok, miesiac, wybranyDzien);

                // Wyświetlamy kalendarz w konsoli
                AnsiConsole.Write(kalendarz);

                // Wyświetlamy wydarzenia dla wybranego dnia
                var wydarzenia = dniPracy.FindAll(wydarzenie => wydarzenie.Item1.Year == rok && wydarzenie.Item1.Month == miesiac && wydarzenie.Item1.Day == wybranyDzien);

                string kolorWydarzenia = wydarzenia.Count >= MaxOsob ? "magenta" : "white";

                if (wydarzenia.Count > 0)
                {
                    AnsiConsole.MarkupLine($"[bold]Grafik na {wybranyDzien}.{miesiac}:[/]");
                    foreach (var w in wydarzenia)
                    {
                        AnsiConsole.MarkupLine($"- [{kolorWydarzenia}]{w.Item2}[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine($"Grafik na {wybranyDzien}.{miesiac} jest wolny");
                }

                // Oczekiwanie na wejście od użytkownika
                ConsoleKeyInfo key = Console.ReadKey();

                // Obsługa klawiszy strzałek
                if (key.Key == ConsoleKey.RightArrow && wybranyDzien < dniWMiesiacu)
                {
                    wybranyDzien++;
                }
                else if (key.Key == ConsoleKey.LeftArrow && wybranyDzien > 1)
                {
                    wybranyDzien--;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    wybranyDzien -= 7;  // Przesunięcie o tydzień w górę
                    if (wybranyDzien < 1) wybranyDzien = 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    wybranyDzien += 7;  // Przesunięcie o tydzień w dół
                    if (wybranyDzien > dniWMiesiacu) wybranyDzien = dniWMiesiacu;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;  // Zakończenie pętli
                }
                else if (key.Key == ConsoleKey.Enter) // Dodawanie dnia pracy po naciśnięciu Enter
                {
                    DodajDzienPracy(wybranyDzien, rok, miesiac,nazwa);
                }
            }
        }

        private void DodajDzienPracy(int wybranyDzien, int rok, int miesiac,string nazwa)
        {
            var data = new DateTime(rok, miesiac, wybranyDzien);

            // Sprawdzenie, czy można dodać nowe wydarzenie
            var wydarzenia = dniPracy.FindAll(wydarzenie => wydarzenie.Item1.Year == rok && wydarzenie.Item1.Month == miesiac && wydarzenie.Item1.Day == wybranyDzien);
            
            if (wydarzenia.Count >= MaxOsob)
            {
                AnsiConsole.MarkupLine("[#FF0000]Limit wydarzeń na ten dzień został osiągnięty![/]");
                AnsiConsole.MarkupLine("\nNaciśnij dowolny klawisz, aby wrócić do kalendarza...");
                Console.ReadKey();
                return;
            }

            // Prośba o opis wydarzenia
            Console.Clear();
            AnsiConsole.MarkupLine($"Dodaj dzień pracy dla {data:yyyy-MM-dd}:");
            DodajDzien(data, nazwa);

            AnsiConsole.MarkupLine("\nNaciśnij dowolny klawisz, aby wrócić do kalendarza...");
            Console.ReadKey();
        }
    }
}
