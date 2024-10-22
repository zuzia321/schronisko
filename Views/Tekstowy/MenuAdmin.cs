using Schronisko.Controllers;
using Schronisko.Models;
using Spectre.Console;

namespace Schronisko.Views.Tekstowy
{
    public class MenuAdmin
    {
        private readonly string plikW = "volounteers.txt";
        private readonly string plikWo = "volounteersWait.txt";
        public void DodajWolontariusza(Wolontariusz wolontariusz)
        {
            //plik z oczekujacymi i plik z zaakceptowanymi gdzie sa rowniez id
            //wyswietlic oczekujacych wolontariuszy
            //wybrac wolontariusza
            //zmienic stan 
            //dopisac id wedlug miejsca w pliku
            /*string wolontariuszDoAkceptu = Console.ReadLine();
            var wszystkieLinie = File.ReadAllLines(plikWo).ToList();
            wszystkieLinie.RemoveAll(l => l.Contains()); // Usunięcie linii zawierających określone imię
            File.WriteAllLines(plik, wszystkieLinie);
            string daneWolontariusza = $"\n{wolontariusz.Imie};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email};{wolontariusz.Miasto};{wolontariusz.Opis};{wolontariusz.Doswiadczenie};{wolontariusz.Dyspozycyjnosc}";
            wolontariusz.Stan = "zaakceptowany";
            File.AppendAllText(plikW, daneWolontariusza);*/

        }
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
                string plik = "Animals.txt";
                string imieDoUsuniecia = Console.ReadLine();
                var wszystkieLinie = File.ReadAllLines(plik).ToList(); // Odczytanie wszystkich linii z pliku
                wszystkieLinie.RemoveAll(l => l.Contains(imieDoUsuniecia)); // Usunięcie linii zawierających określone imię
                File.WriteAllLines(plik, wszystkieLinie);// Zapisanie zaktualizowanej zawartości z powrotem do pliku

            }
            else if (choice == "Dodaj Wolontariusza")
            {
                string plik = "volounteers.txt";
                var wszystkieLinie = File.ReadAllLines(plik).ToList();
                for(int i=0; i<wszystkieLinie.Count;i++)
                {
                   /* if ([10]=="oczekujący")
                    {
                        = "akceptacja";
                        WolontariuszController.Id = i;
                    }*/
                }
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
