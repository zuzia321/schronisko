using Schronisko.Controllers;
using Schronisko.Models;
using Spectre.Console;

namespace Schronisko.Views.Tekstowy
{
    public class MenuAdmin
    {
        private readonly string plikW = "volounteers.txt";
        private readonly string plikA = "Animals.txt";

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
                AnsiConsole.Markup("[Deeppink1]Podaj imię zwierzaka, którego chcesz edytować: [/]");
                string imieDoEdycji = Console.ReadLine();
               
                int i;
                for (i = 0; i < wszystkieLinie.Count; i++)
                {
                    if (wszystkieLinie[i].StartsWith(imieDoEdycji + ";"))
                    {
                        string[] daneZwierzaka = wszystkieLinie[i].Split(';');
                        Console.WriteLine("Bieżące dane zwierzęcia:\n");
                        Console.WriteLine($"1. Imię: {daneZwierzaka[0]}, \n2. Wiek: {daneZwierzaka[1]},\n3. Data przyjęcia: {daneZwierzaka[2]},\n4. Gatunek: {daneZwierzaka[3]},\n5. Rasa: {daneZwierzaka[4]},\n6. Status adopcji: {daneZwierzaka[5]},\n7. Opis: {daneZwierzaka[6]}\n");
                        Console.WriteLine("Którą informację chcesz edytować? (1-7): ");
                        int wybor = int.Parse(Console.ReadLine());
                        Console.WriteLine("\nPodaj na co chcesz zmienić: ");
                        string nowaWartosc = Console.ReadLine();
                        daneZwierzaka[wybor - 1] = nowaWartosc;
                        wszystkieLinie[i] = $"{daneZwierzaka[0]};{daneZwierzaka[1]};{daneZwierzaka[2]};{daneZwierzaka[3]};{daneZwierzaka[4]};{daneZwierzaka[5]};{daneZwierzaka[6]}";
                        Console.WriteLine("\nDane zwierzęcia zostały zaktualizowane.");
                        break;
                    }
                }
                if (i == wszystkieLinie.Count)
                    AnsiConsole.Markup("[#FF0000] Nie ma takiego zwierzęcia w bazie[/]");
                // Zapisanie zaktualizowanej zawartości do pliku
                File.WriteAllLines(plik, wszystkieLinie);
            }
            else if (choice == "Usuń Zwierzaka")
            {
                string plik = "Animals.txt";
                AnsiConsole.Markup("Deeppink1]Podaj imię zwierzęcia do usunięcia: [/]");
                string imieDoUsuniecia = Console.ReadLine();
                var wszystkieLinie = File.ReadAllLines(plik).ToList();
                int usunieteLinie = wszystkieLinie.RemoveAll(l => l.Contains(imieDoUsuniecia));
                if (usunieteLinie == 0)
                    Console.WriteLine("Nie znaleziono zwierzęcia o takim imieniu.");
                File.WriteAllLines(plik, wszystkieLinie);

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
                string plik = "volounteers.txt";
                var wszystkieLinie = File.ReadAllLines(plik).ToList();
                for (int j = 0; j < wszystkieLinie.Count; j++)
                {
                    string[] daneWolontariusza = wszystkieLinie[j].Split(';');
                    if (daneWolontariusza[8]=="akceptacja")
                        Console.WriteLine($"ID: {daneWolontariusza[10]}, Imię: {daneWolontariusza[0]}, Nazwisko: {daneWolontariusza[1]}\n");
                    //sprawdzić czy jest akceptacja

                }
                AnsiConsole.Markup("[Deeppink1]Podaj imię wolontariusza, którego chcesz edytować: [/]");
                string indeks = Console.ReadLine();
                int i;
                for (i = 0; i < wszystkieLinie.Count; i++)
                {

                    if (wszystkieLinie[i].EndsWith(indeks ))
                    {
                        string[] daneWolontariusza = wszystkieLinie[i].Split(';');
                        Console.WriteLine("Bieżące dane wolontariusza:\n");
                        Console.WriteLine($"1. Imię: {daneWolontariusza[0]}, \n2. Nazwisko: {daneWolontariusza[1]},\n3. Data urodzenia: {daneWolontariusza[2]},\n4. telefon: {daneWolontariusza[3]},\n5. email: {daneWolontariusza[4]},\n6. Czy mieszka w Białymstoku: {daneWolontariusza[5]},\n7. Zainteresowania: {daneWolontariusza[6]}\n8. Doświadczenie: {daneWolontariusza[7]}\n");
                        Console.WriteLine("Którą informację chcesz edytować? (1-8): ");
                        int wybor = int.Parse(Console.ReadLine());
                        Console.WriteLine("\nPodaj na co chcesz zmienić: ");
                        string nowaWartosc = Console.ReadLine();
                        daneWolontariusza[wybor - 1] = nowaWartosc;
                        wszystkieLinie[i] = $"{daneWolontariusza[0]};{daneWolontariusza[1]};{daneWolontariusza[2]};{daneWolontariusza[3]};{daneWolontariusza[4]};{daneWolontariusza[5]};{daneWolontariusza[6]};{daneWolontariusza[7]};{daneWolontariusza[8]};{daneWolontariusza[9]};{daneWolontariusza[10]}";
                        Console.WriteLine("\nDane wolontariusza zostały zaktualizowane.");
                        break;
                    }
                }
                if (i == wszystkieLinie.Count)
                    AnsiConsole.Markup("[#FF0000] Nie ma takiej osoby w bazie[/]");
                // Zapisanie zaktualizowanej zawartości do pliku
                File.WriteAllLines(plik, wszystkieLinie);
            }
            else if (choice == "Usuń Wolontariusza")
            {
                string plik = "volounteers.txt";
                AnsiConsole.Markup("Deeppink1]Podaj indeks wolontariusza do usunięcia: [/]");
                string indeks = Console.ReadLine();
                var wszystkieLinie = File.ReadAllLines(plik).ToList(); 
                int usunieteLinie = wszystkieLinie.RemoveAll(l => l.Contains(indeks));
                if (usunieteLinie ==0)
                    Console.WriteLine("Nie znaleziono osoby o takim imieniu.");
                File.WriteAllLines(plik, wszystkieLinie);
            }
            else if (choice == "Wyloguj")
            {
                return;
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
