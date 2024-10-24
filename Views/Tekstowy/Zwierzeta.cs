using Schronisko.Models;
using Spectre.Console;
namespace Schronisko.Views.Tekstowy
{
    public class Zwierzeta
    {
        public List<Zwierze> ListaZwierzat { get;  set; }=new List<Zwierze>();

        public void WyswietlZwierzeta(WidokTekstowy widokTekstowy)
        {
            string plik = "Animals.txt";
            ListaZwierzat.Clear();
            if (File.Exists(plik))
            {
                var linie=File.ReadAllLines(plik);
                for(int i=0; i<linie.Length; i++)
                {
                    var linia = linie[i].Split(';');
                    if(linia.Length==7)
                    {
                        var zwierze = new Zwierze
                        {
                            Imie = linia[0].Trim(),
                            Wiek = linia[1].Trim(),
                           // OdKiedyWSchronisku = DateOnly.Parse(linia[2].Trim()),
                            RodzajZwierzecia = linia[3].Trim(),
                            Gatunek = linia[4].Trim(),
                            Stan = linia[5].Trim(),
                            Opis = linia[6].Trim()
                        };
                        ListaZwierzat.Add(zwierze);
                        //widokTekstowy.WyswietlZwierze(zwierze);
                    }
                }
                OpcjeZwierzeta(widokTekstowy);
            }
            else
            {
                Console.WriteLine("Plik nie istnieje\n");
            }
        }
        public void Wyswietl()
        {
            foreach (var zwierze in ListaZwierzat)
            {
                Console.WriteLine($"{zwierze.Imie}, {zwierze.Wiek}, {zwierze.Gatunek}");
            }
        }
        public void OpcjeZwierzeta(WidokTekstowy widokTekstowy)
        {
            var wybory = new List<string>();
            foreach (var zwierze in ListaZwierzat)
            {
                wybory.Add($"{zwierze.Imie}, {zwierze.Wiek}, {zwierze.Gatunek}");
            }
            Console.Clear();
            var wybraneZwierze = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[Deeppink1]ZWIERZĘTA[/]\n \nWybierz zwierzę:")
                    .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1))
                    //.PageSize(10) // Ustawienia dotyczące przewijania jezeli jest wiecej niz 10 elementow to lista bedzie sie przesuwac
                    .AddChoices(wybory));

            int id = wybory.IndexOf(wybraneZwierze);
            var zwierzeDoPokazania = ListaZwierzat[id];

            AnsiConsole.Markup($"[DeepPink3_1]{zwierzeDoPokazania.Imie.ToUpper()}[/]\n");
            AnsiConsole.Markup($"Wiek: {zwierzeDoPokazania.Wiek}\n");
            AnsiConsole.Markup($"W schronisku od: {zwierzeDoPokazania.OdKiedyWSchronisku}\n");
            AnsiConsole.Markup($"Rodzaj: {zwierzeDoPokazania.RodzajZwierzecia}\n");
            AnsiConsole.Markup($"Gatunek: {zwierzeDoPokazania.Gatunek}\n");
            AnsiConsole.Markup($"Opis: {zwierzeDoPokazania.Opis}\n");


            var opcje = new List<string> 
            { 
                "Formularz adopcyjny", 
                "Powrót" 
            };

            var wybranaOpcja = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz opcję:")
                     .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1))
                    .AddChoices(opcje));

            if (wybranaOpcja == "Formularz adopcyjny")
            {
                FormularzAdopcyjny(zwierzeDoPokazania,widokTekstowy);
                AnsiConsole.WriteLine("\n\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();  
            }
            else if (wybranaOpcja == "Powrót")
            {
                return;
            }
        }
        private void FormularzAdopcyjny(Zwierze zwierze,WidokTekstowy widokTekstowy)
        {
            Console.Clear();
            AnsiConsole.Markup($"[DeepPink3_1]Formularz adopcyjny dla: {zwierze.Imie}[/]\n");
            widokTekstowy.stworzAdopcje();
        }

    }
}

