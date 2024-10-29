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
                var wszystkieLinie = File.ReadAllLines(plikA).ToList();
                var validator = new poprawnoscZwierze();
                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneZwierzaka = wszystkieLinie[j].Split(';');
                    Console.WriteLine($"Imię: {daneZwierzaka[0]}, Wiek: {daneZwierzaka[1]}, Gatunek: {daneZwierzaka[3]}\n");
                }
                bool znaleziono = false;
                while (true)
                {
                    AnsiConsole.Markup("[Deeppink1]Podaj imię zwierzaka, którego chcesz edytować: [/]");
                    string imieDoEdycji = textInfo.ToTitleCase((Console.ReadLine()).ToLower());
                    for (int i = 0; i < wszystkieLinie.Count; i++)
                    {
                        if (wszystkieLinie[i].StartsWith(imieDoEdycji + ";"))
                        {
                            znaleziono = true;
                            string[] daneZwierzaka = wszystkieLinie[i].Split(';');
                            while (true)
                            {
                                Console.Clear();
                                AnsiConsole.Markup("[Deeppink1]Bieżące dane zwierzęcia:[/]\n");
                                Console.WriteLine($"1. Imię: {daneZwierzaka[0]}, \n2. Wiek: {daneZwierzaka[1]},\n3. Data przyjęcia: {daneZwierzaka[2]},\n4. Gatunek: {daneZwierzaka[3]},\n5. Rasa: {daneZwierzaka[4]},\n6. Czy dostępny do adopcji? : {daneZwierzaka[5]},\n7. Opis: {daneZwierzaka[6]},\n8. Anuluj edytowanie");
                                Console.WriteLine("\nKtórą informację chcesz edytować? (1-7): ");
                                int wybor = int.Parse(Console.ReadLine());
                                if (wybor == 8)
                                    break;
                                string nowaWartosc = "";
                                Zwierze zwierze = new Zwierze();
                                bool isValid = true;
                                switch (wybor)
                                {
                                    case 1:
                                        nowaWartosc = textInfo.ToTitleCase(Validation.PobierzPoprawneDane("Podaj nową wartość:", zwierze, nameof(zwierze.Imie)).ToLower());
                                        break;
                                    case 2:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość: ", zwierze, nameof(zwierze.Wiek));
                                        break;
                                    case 3:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość: ", zwierze, nameof(zwierze.OdKiedyWSchronisku));
                                        break;
                                    case 4:
                                        Console.WriteLine("Podaj nową wartość: ");
                                        nowaWartosc = Console.ReadLine();
                                        if (!validator.poprawnoscRodzaj(nowaWartosc))
                                        {
                                            Console.WriteLine();
                                            isValid = false;
                                        }
                                        break;
                                    case 5:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość: ", zwierze, nameof(zwierze.Gatunek));
                                        break;
                                    case 6:
                                        Console.WriteLine("Podaj nową wartość: ");
                                        nowaWartosc = Console.ReadLine();
                                        if (!validator.poprawnoscStan(nowaWartosc))
                                        {
                                            Console.WriteLine();
                                            isValid = false;
                                        }
                                        break;
                                    case 7:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość: ", zwierze, nameof(zwierze.Opis));
                                        break;
                                    default:
                                        break;
                                }
                                if (isValid)
                                {
                                    daneZwierzaka[wybor - 1] = nowaWartosc;
                                    wszystkieLinie[i] = string.Join(";", daneZwierzaka);
                                    Console.WriteLine("Dane zwierzaka zostały zaktualizowane.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wprowadzone dane są niepoprawne. Spróbuj ponownie.");
                                }
                            }
                            break;
                        }
                    }
                    if (znaleziono)
                        break;
                    AnsiConsole.Markup("[#FF0000] Nie ma takiego zwierzęcia w bazie[/]\n");
                }
                File.WriteAllLines(plikA, wszystkieLinie);
            }
            else if (choice == "Usuń Zwierzaka")
            {
                var wszystkieLinie = File.ReadAllLines(plikA).ToList();
                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneZwierzaka = wszystkieLinie[j].Split(';');
                    Console.WriteLine($"Imię: {daneZwierzaka[0]}, Wiek: {daneZwierzaka[1]}, Gatunek: {daneZwierzaka[3]}\n");
                }
                Console.WriteLine("\nWpisz anuluj jeżeli rezygnujesz z usuwania zwierzat\n");
                AnsiConsole.Markup("[Deeppink1]Podaj imię zwierzęcia do usunięcia: [/]");
                string imieDoUsuniecia = Console.ReadLine();
                if (imieDoUsuniecia == "anuluj")
                    return;
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

                for(int j=0; j<linie.Count;j++)
                {
                    var linia = linie[j].Split(';');
                    if (linia.Length == 11 && linia[8] == "oczekujący")
                    {
                        Console.WriteLine($"Id: {linia[10]}\n Imię: {linia[0]}\n Nazwisko: {linia[1]}\n Data urodzenia: {linia[2]}\n telefon: {linia[3]}\n email: {linia[4]}\n Czy mieszka w Białymstoku: {linia[5]}\n Zainteresowania: {linia[6]}\n Doświadczenie: {linia[7]}\n");
                        var rule = new Rule();
                        AnsiConsole.Write(rule);
                    }
                }
                Console.WriteLine("Podaj indeks osoby którą chcesz dodać do grona wolontariuszy: ");
                string index=Console.ReadLine() ;
                int i;
                string imie = "";
                string[] osoba = [];
                for(i=0;i<linie.Count; i++)
                {
                    osoba = linie[i].Split(';');
                    if (osoba[10]==index)
                    {
                        if (osoba[8] == "oczekujący")
                        {
                            imie = osoba[0];
                            osoba[8] = "akceptacja";
                            break;
                        }
                    }
                }
                linie[i] = string.Join(';', osoba);
                File.WriteAllLines(plikW, linie);

                AnsiConsole.Markup($"Grono wolontariuszy się poszerzyło o [Deeppink1]{imie}[/]");
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
                                Console.WriteLine($"1. Imię: {daneWolontariusza[0]}, \n2. Nazwisko: {daneWolontariusza[1]},\n3. Data urodzenia: {daneWolontariusza[2]},\n4. telefon: {daneWolontariusza[3]},\n5. email: {daneWolontariusza[4]},\n6. Czy mieszka w Białymstoku: {daneWolontariusza[5]},\n7. Zainteresowania: {daneWolontariusza[6]}\n8. Doświadczenie: {daneWolontariusza[7]},\n9. Anuluj edycje wolontariusza");
                                Console.WriteLine("\nKtórą informację chcesz edytować? (1-8): ");
                                int wybor = int.Parse(Console.ReadLine());
                                if (wybor == 9)
                                    break;
                                string nowaWartosc="";
                                Wolontariusz wolontariusz = new Wolontariusz();
                                bool isValid = true;
                                switch (wybor)
                                {
                                    case 1:     
                                        nowaWartosc = textInfo.ToTitleCase(Validation.PobierzPoprawneDane("Podaj nową wartość:", wolontariusz, nameof(wolontariusz.Imie)).ToLower());
                                        break;
                                    case 2:
                                        nowaWartosc = textInfo.ToTitleCase(Validation.PobierzPoprawneDane("Podaj nową wartość:", wolontariusz, nameof(wolontariusz.Nazwisko)).ToLower());
                                        break;
                                    case 4:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość:", wolontariusz, nameof(wolontariusz.Telefon));
                                        break;
                                    case 5:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość:", wolontariusz, nameof(wolontariusz.Email));
                                        break;
                                    case 7:
                                        nowaWartosc = Validation.PobierzPoprawneDane("Podaj nową wartość:", wolontariusz, nameof(wolontariusz.Opis)) ;
                                        break;
                                    case 3:
                                        Console.WriteLine("Podaj nową wartość: ");
                                        nowaWartosc = Console.ReadLine();
                                        DateOnly dataUrodzenia;

                                        if (DateOnly.TryParseExact(nowaWartosc, "dd.MM.yyyy", null, DateTimeStyles.None, out dataUrodzenia))
                                        {
                                           
                                            // Sprawdź wiek
                                            if (!validator.wiek(dataUrodzenia))
                                            {
                                                Console.WriteLine("Wolontariusz musi mieć co najmniej 18 lat.");
                                                isValid = false;
                                            }
                                        }
                                        break;
                                    case 6:
                                        Console.WriteLine("Podaj nową wartość: ");
                                        nowaWartosc = Console.ReadLine();
                                        if (!validator.poprawnoscMiasto(nowaWartosc))
                                        {
                                            Console.WriteLine("Odpowiedź na pytanie o miejsce zamieszkania powinna być 'tak' lub 'nie'.");
                                            isValid = false;
                                        }
                                        break;

                                    case 8:
                                        Console.WriteLine("Podaj nową wartość: ");
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
                var wszystkieLinie = File.ReadAllLines(plikW).ToList();

                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneWolontariusza = wszystkieLinie[j].Split(';');
                    Console.WriteLine($"ID: {daneWolontariusza[10]}, Imię: {daneWolontariusza[0]}, Nazwisko: {daneWolontariusza[1]}\n");
                }
                string[] pom = wszystkieLinie[wszystkieLinie.Count - 1].Split(';');
                int zmienna = int.Parse(pom[10]) + 1;
                Console.WriteLine($"Wciśnij {zmienna} aby wyjsc z usuwania");
                AnsiConsole.Markup("[Deeppink1]Podaj indeks wolontariusza do usunięcia: [/]");
                string indeks = Console.ReadLine();
                if (int.Parse(indeks) == int.Parse(pom[10])+1)
                    return;
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
