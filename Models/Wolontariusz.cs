using System.ComponentModel.DataAnnotations;

namespace Schronisko.Models
{
    public class Wolontariusz
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imie jest wymagane.")]
        [MinLength(3,ErrorMessage ="imie musi miec min 3 znaki")]
        public string Imie {  get; set; }
        
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MinLength(2, ErrorMessage = "Nazwisko musi mieć co najmniej 2 znaki.")]
        [MaxLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków.")]
        public string Nazwisko { get; set; }
        
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        [DataType(DataType.Date, ErrorMessage = "Podaj poprawną datę.")]
        public DateTime DataUrodzenia { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Numer telefonu musi mieć dokładnie 9 cyfr.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numer telefonu może zawierać tylko cyfry.")]
        public string Telefon {  get; set; }
        
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email (musi zawierać @).")]
        public string Email { get; set; }
        
        public bool Miasto { get; set; }
        public string ?Opis { get; set; }
        public bool Doswiadczenie { get; set; }
        public string ?Dyspozycyjnosc {  get; set; }
        public string Stan { get; set; } = "oczekujący";


        //public void index()
    }
}
