using Schronisko.Controllers;
using Schronisko.Models;
using Spectre.Console;
using System.Globalization;

namespace Schronisko.Views.Tekstowy
{
    public class MenuAdmin
    {
        private readonly string plikW = "volounteers.txt";
        private readonly string plikA = "Animals.txt";
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        public void MenuAdminOpis(string choice, WidokTekstowy widokTekstowy, WebApplication app)
        {
            if (choice == "Dodaj Zwierzaka")
            {
                var zwierzeController = new ZwierzeController(widokTekstowy);
                zwierzeController.StworzIWyswietlZwierze();
            }
            else if (choice == "Edytuj zwierzaka")
            {
                string plik = "Animals.txt";
                var wszystkieLinie = File.ReadAllLines(plik).ToList();
                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneZwierzaka = wszystkieLinie[j].Split(';');
                    Console.WriteLine($"Imię: {daneZwierzaka[0]}, Wiek: {daneZwierzaka[1]}, Gatunek: {daneZwierzaka[3]}\n");
                }
                int i;
                bool znaleziono = false;
                while (true)
                {
                    AnsiConsole.Markup("[Deeppink1]Podaj imię zwierzaka, którego chcesz edytować: [/]");
                    string imieDoEdycji = textInfo.ToTitleCase((Console.ReadLine()).ToLower());
                    for (i = 0; i < wszystkieLinie.Count; i++)
                    {
                        if (wszystkieLinie[i].StartsWith(imieDoEdycji + ";"))
                        {
                            znaleziono = true;
                            string[] daneZwierzaka = wszystkieLinie[i].Split(';');
                            Console.Clear();
                            AnsiConsole.Markup("[Deeppink1]Bieżące dane zwierzęcia:[/]\n");
                            Console.WriteLine($"1. Imię: {daneZwierzaka[0]}, \n2. Wiek: {daneZwierzaka[1]},\n3. Data przyjęcia: {daneZwierzaka[2]},\n4. Gatunek: {daneZwierzaka[3]},\n5. Rasa: {daneZwierzaka[4]},\n6. Czy dostępny do adopcji? : {daneZwierzaka[5]},\n7. Opis: {daneZwierzaka[6]},\n8. Anuluj edytokwanie");
                            Console.WriteLine("\nKtórą informację chcesz edytować? (1-7): ");
                            int wybor = int.Parse(Console.ReadLine());
                            if (wybor == 8)
                                break;
                            Console.WriteLine("\nPodaj na co chcesz zmienić: ");
                            string nowaWartosc = Console.ReadLine();
                            daneZwierzaka[wybor - 1] = nowaWartosc;
                            wszystkieLinie[i] = $"{daneZwierzaka[0]};{daneZwierzaka[1]};{daneZwierzaka[2]};{daneZwierzaka[3]};{daneZwierzaka[4]};{daneZwierzaka[5]};{daneZwierzaka[6]}";
                            Console.WriteLine("\nDane zwierzęcia zostały zaktualizowane.");
                            break;
                        }
                    }
                    if (znaleziono)
                        break;
                    AnsiConsole.Markup("[#FF0000] Nie ma takiego zwierzęcia w bazie[/]\n");
                }
                File.WriteAllLines(plik, wszystkieLinie);
            }
            else if (choice == "Usuń Zwierzaka")
            {
                var wszystkieLinie = File.ReadAllLines(plikA).ToList();
                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneZwierzaka = wszystkieLinie[j].Split(';');
                    Console.WriteLine($"Imię: {daneZwierzaka[0]}, Wiek: {daneZwierzaka[1]}, Gatunek: {daneZwierzaka[3]}\n");
                }
                AnsiConsole.Markup("[Deeppink1]Podaj imię zwierzęcia do usunięcia: [/]");
                string imieDoUsuniecia = Console.ReadLine();

                //int usunieteLinie = wszystkieLinie.RemoveAll(l => l.Contains(imieDoUsuniecia));
                int usunieteLinie = wszystkieLinie.RemoveAll(l => l.StartsWith(imieDoUsuniecia + ";"));
                if (usunieteLinie == 0)
                    Console.WriteLine("Nie znaleziono zwierzęcia o takim imieniu.");
                else
                    Console.WriteLine("Udało się usunąć zwierzaka");
                File.WriteAllLines(plikA, wszystkieLinie);

            }
            else if (choice == "Dodaj Wolontariusza")
            {
                var linie = File.ReadAllLines(plikW).ToList();

                for(int i=0; i<linie.Count;i++)
                {
                    var linia = linie[i].Split(';');
                    if (linia.Length == 10 && linia[8] == "oczekujący")
                    {
                        Console.WriteLine($"Id: {i+1}\n Imię: {linia[0]}\n Nazwisko: {linia[1]}\n Data urodzenia: {linia[2]}\n telefon: {linia[3]}\n email: {linia[4]}\n Czy mieszka w Białymstoku: {linia[5]}\n Zainteresowania: {linia[6]}\n Doświadczenie: {linia[7]}\n");
                        var rule = new Rule();
                        AnsiConsole.Write(rule);
                    }
                }
                Console.WriteLine("Podaj indeks osoby którą chcesz dodać do grona wolontariuszy: ");
                int id=int.Parse(Console.ReadLine()) ;
                var wolontariusz=linie[id - 1].Split(';');
                wolontariusz[8] = "akceptacja";
                linie[id-1]= string.Join(';', wolontariusz) + $";{id}";
                File.WriteAllLines(plikW, linie);

                AnsiConsole.Markup($"Grono wolontariuszy się poszerzyło o [Deeppink1]{wolontariusz[0]}[/]");
            }
            else if (choice == "Edytuj Wolontariusza")
            {
                var wszystkieLinie = File.ReadAllLines(plikW).ToList();
                var validator = new poprawnoscWolontariusza();

                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneWolontariusza = wszystkieLinie[j].Split(';');
                    if (daneWolontariusza[8]=="akceptacja")
                        Console.WriteLine($"ID: {daneWolontariusza[10]}, Imię: {daneWolontariusza[0]}, Nazwisko: {daneWolontariusza[1]}\n");
                }
                bool znaleziono = false;
                while (true)
                {
                    AnsiConsole.Markup("[Deeppink1]Podaj indeks wolontariusza, którego chcesz edytować: [/]");
                    string indeks = Console.ReadLine();
                    int i;
                    for (i = 0; i < wszystkieLinie.Count; i++)
                    {
                        if (wszystkieLinie[i].EndsWith(indeks))
                        {
                            znaleziono = true;
                            string[] daneWolontariusza = wszystkieLinie[i].Split(';');
                            while (true)
                            {
                                AnsiConsole.Markup("[Deeppink1]Bieżące dane wolontariusza:[/]\n");
                                Console.WriteLine($"1. Imię: {daneWolontariusza[0]}, \n2. Nazwisko: {daneWolontariusza[1]},\n3. Data urodzenia: {daneWolontariusza[2]},\n4. telefon: {daneWolontariusza[3]},\n5. email: {daneWolontariusza[4]},\n6. Czy mieszka w Białymstoku: {daneWolontariusza[5]},\n7. Zainteresowania: {daneWolontariusza[6]}\n8. Doświadczenie: {daneWolontariusza[7]}\n,9. Anuluj edycje wolontariusza");
                                Console.WriteLine("\nKtórą informację chcesz edytować? (1-8): ");
                                int wybor = int.Parse(Console.ReadLine());
                                if (wybor == 9)
                                    break;
                               // Console.WriteLine("\nPodaj na co chcesz zmienić: ");
                                string nowaWartosc="";//= Console.ReadLine();

                                bool isValid = true;
                                switch (wybor)
                                {
                                   /* case 1:
                                    case 2:
                                    case 4:
                                    case 6:
                                    case 7:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość:", daneWolontariusza, $"Element {wybor - 1}") ;
                                        break;
                                    case 3: // Data urodzenia - walidacja wieku
                                        nowaWartosc = Console.ReadLine();
                                        if (DateTime.TryParse(nowaWartosc, out DateTime dataUrodzenia) && !validator.wiek(dataUrodzenia))
                                        {
                                            Console.WriteLine("Wolontariusz musi mieć co najmniej 18 lat.");
                                            isValid = false;
                                        }
                                        break;
                                   */
                                    case 5: // Miasto - walidacja czy wolontariusz mieszka w Białymstok
                                        nowaWartosc = Console.ReadLine();
                                        if (!validator.poprawnoscMiasto(nowaWartosc))
                                        {
                                            Console.WriteLine("Odpowiedź na pytanie o miejsce zamieszkania powinna być 'tak' lub 'nie'.");
                                            isValid = false;
                                        }
                                        break;

                                    case 8: // Doświadczenie - walidacja na odpowiedź tak/nie
                                        nowaWartosc = Console.ReadLine();
                                        if (!validator.poprawnoscDoswiadczenie(nowaWartosc))
                                        {
                                            Console.WriteLine("Odpowiedź na pytanie o doświadczenie powinna być 'tak' lub 'nie'.");
                                            isValid = false;
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                if (isValid)
                                {
                                    daneWolontariusza[wybor - 1] = nowaWartosc;
                                    wszystkieLinie[i] = string.Join(";", daneWolontariusza);
                                    Console.WriteLine("Dane wolontariusza zostały zaktualizowane.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wprowadzone dane są niepoprawne. Spróbuj ponownie.");
                                }
                            }
                            break;
                            /*daneWolontariusza[wybor - 1] = nowaWartosc;
                            wszystkieLinie[i] = $"{daneWolontariusza[0]};{daneWolontariusza[1]};{daneWolontariusza[2]};{daneWolontariusza[3]};{daneWolontariusza[4]};{daneWolontariusza[5]};{daneWolontariusza[6]};{daneWolontariusza[7]};{daneWolontariusza[8]};{daneWolontariusza[9]};{daneWolontariusza[10]}";
                            Console.WriteLine("\nDane wolontariusza zostały zaktualizowane.");
                            */
                           
                        }
                    }
                    if (znaleziono)
                        break;
                    AnsiConsole.Markup("[#FF0000] Nie ma takiej osoby w bazie[/]\n");
                }
                File.WriteAllLines(plikW, wszystkieLinie);
            }
            else if (choice == "Usuń Wolontariusza")
            { 
                AnsiConsole.Markup("[Deeppink1]Podaj indeks wolontariusza do usunięcia: [/]");
                string indeks = Console.ReadLine();
                var wszystkieLinie = File.ReadAllLines(plikW).ToList();
                int usunieteLinie = wszystkieLinie.RemoveAll(l => l.EndsWith(";" + indeks));
                if (usunieteLinie == 0)
                    Console.WriteLine("Nie znaleziono osoby o takim indeksie.Może być jeszcze w trybie oczekiwania");
                else
                {
                    File.WriteAllLines(plikW, wszystkieLinie);
                    AnsiConsole.Markup($"Udało się usunąć wolontariusza");
                }
            }
            else if (choice == "Wyloguj")
            {
                return;
            }
            else if (choice == "Zakończ przeglądanie")
            {
                AnsiConsole.Markup("[#FF0000]KONIEC...[/]\n");
                Environment.Exit(0);
            }
            AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }
}
