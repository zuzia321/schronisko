using Microsoft.Extensions.Logging;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Schronisko.Views.Tekstowy
{
    public class MenuKalendarz
    {
        private List<(DateTime, string)> dniPracy = new List<(DateTime, string)>();
        private const int MaxWydarzeniaNaDzien = 4; // Maksymalna liczba wydarzeń na dzień

        public void DodajDzieńPracy(DateTime data, string opis)
        {
            opis= char.ToUpper(opis[0]) + opis.Substring(1).ToLower();
            dniPracy.Add((data, opis));
            Console.WriteLine($"\n{data:yyyy-MM-dd}, {opis} - Dzień pracy został dodany.");
        }

        public void WyswietlKalendarz()
        {
            int rok = AnsiConsole.Ask<int>("Podaj rok:");
            int miesiac = AnsiConsole.Ask<int>("Podaj miesiąc:");

            WybierzDate(rok, miesiac);
        }

        public void WybierzDate(int rok, int miesiac, int wybranyDzien = 1)
        {
            int dniWMiesiacu = DateTime.DaysInMonth(rok, miesiac);

            while (true)
            {
                Console.Clear();
                var kalendarz = new Calendar(rok, miesiac);

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

                // Zmieniamy kolor na czerwony, jeśli liczba wydarzeń wynosi 4
                string kolorWydarzenia = wydarzenia.Count >= MaxWydarzeniaNaDzien ? "magenta" : "white";
                

                if (wydarzenia.Count > 0)
                {
                    AnsiConsole.MarkupLine($"[bold]Grafik na {wybranyDzien}.{miesiac}:[/]");
                    foreach (var w in wydarzenia)
                    {
                        AnsiConsole.MarkupLine($"- [{kolorWydarzenia}]{w.Item2}[/]");
                    }
                   /* AnsiConsole.MarkupLine($"[bold]Grafik na {wybranyDzien}.{miesiac}:[/]");

                    foreach (var w in wydarzenia)
                    {
                        var kolorTla = wydarzenia.Count >= MaxWydarzeniaNaDzien ? "magenta" : "white"; // Tło magenta, jeśli max osiągnięty
                        var kolorTekstu = wydarzenia.Count >= MaxWydarzeniaNaDzien ? "white" : "black"; // Biały tekst na magencie

                        // Użycie stylu do zmiany tła i koloru tekstu
                        AnsiConsole.MarkupLine($"[{kolorTekstu} on {kolorTla}] - {w.Item2}[/]");
                    }*/
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
                    DodajDzienPracy(wybranyDzien, rok, miesiac);
                }
            }
        }

        private void DodajDzienPracy(int wybranyDzien, int rok, int miesiac)
        {
            var data = new DateTime(rok, miesiac, wybranyDzien);

            // Sprawdzenie, czy można dodać nowe wydarzenie
            var wydarzenia = dniPracy.FindAll(wydarzenie => wydarzenie.Item1.Year == rok && wydarzenie.Item1.Month == miesiac && wydarzenie.Item1.Day == wybranyDzien);
            if (wydarzenia.Count >= MaxWydarzeniaNaDzien)
            {
                AnsiConsole.MarkupLine("[red]Limit wydarzeń na ten dzień został osiągnięty![/]");
                AnsiConsole.MarkupLine("[blue]Naciśnij dowolny klawisz, aby wrócić do kalendarza...[/]");
                Console.ReadKey();
                return;
            }

            // Prośba o opis wydarzenia
            Console.Clear();
            AnsiConsole.MarkupLine($"Dodaj dzień pracy dla {data:yyyy-MM-dd}:");
            string opis = AnsiConsole.Ask<string>("Podaj opis dnia pracy:");
            DodajDzieńPracy(data, opis);

            AnsiConsole.MarkupLine("\nNaciśnij dowolny klawisz, aby wrócić do kalendarza...");
            Console.ReadKey();
        }

        // Metoda wyświetlająca dni pracy dla konkretnej daty
      /*  private void PokazDniPracy(DateTime data)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[bold green]Dni pracy dla daty {data:yyyy-MM-dd}:[/]");
            bool found = false;

            foreach (var (dzien, opis) in dniPracy)
            {
                if (dzien.Date == data.Date)
                {
                    AnsiConsole.MarkupLine($"- [yellow]{dzien:yyyy-MM-dd}[/]: {opis}");
                    found = true;
                }
            }

            if (!found)
            {
                AnsiConsole.MarkupLine("[red]Brak dni pracy dla tej daty.[/]");
            }

            AnsiConsole.MarkupLine("[bold blue]Naciśnij dowolny klawisz, aby wrócić...[/]");
            Console.ReadKey();
        }*/
    }
}
