using System.ComponentModel.DataAnnotations;

namespace Schronisko.Models
{
    public class Zwierze
    {
        public string Id { get; set; }

        [Display(Name ="Podaj imię zwierzaka")]
        [Required(ErrorMessage = "Imie jest wymagane.")]
        [MinLength(3, ErrorMessage = "imie musi miec min 3 znaki")]
        public string Imie { get; set; }

        [Display(Name ="Podaj wiek zwierzęcia")]
        [Required(ErrorMessage = "Wiek jest wymagany")]
        public string Wiek {  get; set; }

        [Display(Name = "Kiedy trafił do schroniska?")]
        [Required(ErrorMessage = "Data trafienia do schroniska jest wymagana.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        [DataType(DataType.Date, ErrorMessage = "Podaj poprawną datę.")]
        public DateOnly OdKiedyWSchronisku { get; set; }

        [Display(Name = "Jaki to rodzaj zwierzęcia?")]
        [Required(ErrorMessage = "Rodzaj zwierzęcia jest wymagany")]
        public string RodzajZwierzecia {get; set;}

        [Display(Name = "Podaj gatunek")]
        [Required(ErrorMessage = "Gatunek jest wymagany")]
        public string Gatunek { get; set;}

        [Display(Name = "Podaj płeć")]
        public string Płeć {  get; set;}

        [Display(Name = "Czy jest już dostępny do adopcji")]
        public string Stan { get; set; }

        [Display(Name = "Podaj więcej informacji o zwierzęciu. Jaki jest? Jakie były okoliczności trafienia do schroniska? Gdzie go znaleziono?")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Opis { get; set; }

        public List<Adopcja> Adopcje { get; set; } = new();
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
        public bool poprawnoscPłeć(string plec)
        {
            if (plec == "samiec" || plec == "samica")
                return true;
            return false;
        }
    }
}
