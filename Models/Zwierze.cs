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
        [DataType(DataType.Date, ErrorMessage = "Podaj poprawną datę.")]
        public DateOnly OdKiedyWSchronisku { get; set; }

        [Required(ErrorMessage = "Rodzaj zwierzęcia jest wymagany")]
        public string RodzajZwierzecia {get; set;}//może rozwijalna lista, jezeli jakiegos nie ma to inne i pojawia sie pole do wpisania

        [Required(ErrorMessage = "Gatunek jest wymagany")]
        public string Gatunek { get; set;}

        public string Stan { get; set; } //czy adoptowany, do adopcji itp.
       
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Opis { get; set; }
        //zdjęcie
    }
}
