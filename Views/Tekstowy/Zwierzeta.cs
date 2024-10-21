using Schronisko.Models;
using Spectre.Console;
namespace Schronisko.Views.Tekstowy
{
    public class Zwierzeta
    {
        public void WyswietlZwierzeta(WidokTekstowy widokTekstowy)
        {
            string plik = "Animals.txt";
            if(File.Exists(plik))
            {
                var linie=File.ReadAllLines(plik);
                for(int i=0; i<linie.Length; i++)
                {
                    var linia = linie[i].Split(',');
                    if(linia.Length==7)
                    {
                        var zwierze = new Zwierze
                        {
                            Imie = linia[0].Trim(),
                            Wiek = linia[1].Trim(),
                            OdKiedyWSchronisku = DateTime.Parse(linia[2].Trim()),
                            RodzajZwierzecia = linia[3].Trim(),
                            Gatunek = linia[4].Trim(),
                            Stan = linia[5].Trim(),
                            Opis = linia[6].Trim()
                        };
                        widokTekstowy.WyswietlZwierze(zwierze);
                    }
                }
            }
            else
            {
                Console.WriteLine("Plik nie istnieje\n");
            }
        }
    }
}
