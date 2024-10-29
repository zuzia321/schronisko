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
            Wczytaj();
        }
        public void Zapisz()
        {
            using (var z = new StreamWriter(plik))
            {
                foreach (var (data,nazwa) in dniPracy)
                {
                    z.WriteLine($"{data:yyyy-MM-dd};{nazwa}");
                }
            }
        }
        public void Wczytaj()
        {
            if (File.Exists(plik))
            {
                using (var czytaj = new StreamReader(plik))
                {
                    string line;
                    while ((line = czytaj.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length == 2 &&DateTime.TryParse(parts[0], out DateTime data))
                            dniPracy.Add((data, parts[1]));
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
            {
                AnsiConsole.Markup($"\n\n[#FF0000]{nazwa} już pracujesz w ten dzień !!![/]\n".ToUpper());
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"{data:yyyy-MM-dd}:");
                var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nCo chcesz zrobić?")
                    .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1))
                    .AddChoices("Wpisz się na dany dzień", "Wróć do kalendarza"));

                if (wybor == "Wpisz się na dany dzień")
                {
                    dniPracy.Add((data, nazwa));
                    Console.WriteLine($"\n{data:yyyy-MM-dd}, {nazwa} - Zostałeś dodany do grafiku.");
                    Zapisz();
                }
                else if (wybor == "Wróć do kalendarz")
                {
                    return;
                }
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
               
                AnsiConsole.Markup("[Magenta]LEGENDA[/]\n");
                AnsiConsole.Markup(" - Po kalendarzu można poruszac się strzałkami\n - ENTER spowoduje wybór dnia i umożliwi wpisanie się na dany dzień\n - Escape spowoduje wyjście z kalendarza i powrot do menu wolontariusza\n");
                var rule = new Rule();
                AnsiConsole.Write(rule);
                var kalendarz = new Spectre.Console.Calendar(rok, miesiac);
                kalendarz.Culture("pl-PL");
                kalendarz.BorderColor(Color.Pink1);
                kalendarz.HeaderStyle(Color.Pink1);

                foreach (var (data, opis) in dniPracy)
                {
                    if (data.Year == rok && data.Month == miesiac)
                        kalendarz.AddCalendarEvent(data.Year, data.Month, data.Day);
                }

                kalendarz.HighlightStyle(Style.Parse("magenta")).AddCalendarEvent(rok, miesiac, wybranyDzien);
                AnsiConsole.Write(kalendarz);
                var wydarzenia = dniPracy.FindAll(wydarzenie => wydarzenie.Item1.Year == rok && wydarzenie.Item1.Month == miesiac && wydarzenie.Item1.Day == wybranyDzien);

                if (wydarzenia.Count > 0)
                {
                    AnsiConsole.MarkupLine($"Grafik na [bold Magenta] [on Pink1]{wybranyDzien}.{miesiac}[/][/]:");
                    foreach (var w in wydarzenia)
                    {
                        string kolorWydarzenia = wydarzenia.Count >= MaxOsob ? "magenta" : "white";

                        AnsiConsole.MarkupLine($"- [{kolorWydarzenia}]{w.Item2}[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine($"Grafik na [bold Magenta] [on Pink1]{wybranyDzien}.{miesiac}[/][/] jest wolny");
                }

                ConsoleKeyInfo key = Console.ReadKey();
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
                    wybranyDzien -= 7;
                    if (wybranyDzien < 1) wybranyDzien = 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    wybranyDzien += 7;
                    if (wybranyDzien > dniWMiesiacu) wybranyDzien = dniWMiesiacu;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    DodajDzienPracy(wybranyDzien, rok, miesiac,nazwa);
                }
            }
        }
        private void DodajDzienPracy(int wybranyDzien, int rok, int miesiac,string nazwa)
        {
            var data = new DateTime(rok, miesiac, wybranyDzien);

            var wydarzenia = dniPracy.FindAll(wydarzenie => wydarzenie.Item1.Year == rok && wydarzenie.Item1.Month == miesiac && wydarzenie.Item1.Day == wybranyDzien);
            
            if (wydarzenia.Count >= MaxOsob)
            {
                var rule = new Rule();
                AnsiConsole.Write(rule);
                AnsiConsole.MarkupLine("\n[#FF0000]Limit wydarzeń na ten dzień został osiągnięty![/]");
                AnsiConsole.MarkupLine("\nNaciśnij dowolny klawisz, aby wrócić do kalendarza...");
                Console.ReadKey();
                return;
            }
            DodajDzien(data, nazwa);

        }
    }
}
