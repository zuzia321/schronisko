using System.ComponentModel.DataAnnotations;

namespace Schronisko.Models
{
    public class Zwierze
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Imie jest wymagane.")]
        [MinLength(3, ErrorMessage = "imie musi miec min 3 znaki")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Wiek jest wymagany")]
        public string Wiek {  get; set; }

        [Required(ErrorMessage = "Data trafienia do schroniska jest wymagana.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        [DataType(DataType.Date, ErrorMessage = "Podaj poprawną datę.")]
        public DateOnly OdKiedyWSchronisku { get; set; }

        [Required(ErrorMessage = "Rodzaj zwierzęcia jest wymagany")]
        public string RodzajZwierzecia {get; set;}//może rozwijalna lista, jezeli jakiegos nie ma to inne i pojawia sie pole do wpisania

        [Required(ErrorMessage = "Gatunek jest wymagany")]
        public string Gatunek { get; set;}

        public string Stan { get; set; }
       
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Opis { get; set; }
        //zdjęcie
    }
    public class poprawnoscZwierze()
    {
        public bool poprawnoscStan(string stan)
        {
            if (stan == "tak" || stan == "nie")
                return true;
            return false;
        }
        public bool poprawnoscRodzaj(string rodzaj)
        {
            List<string> rodzaje = new List<string>()
            {
                "pies", "kot",
                "jaszczurka","ptak",
                "szczur", "mysz",
                "chomik", "królik",
                "świnka morska",
                "szynszyla","żółw",
                "krowa","świnia",
                "kura", "owca",
                "koza","kaczka",
                "gęś"
            };
            if (rodzaje.Contains(rodzaj))
                return true;
            return false;
        }
    }
}
