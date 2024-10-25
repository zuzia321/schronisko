using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Net;

namespace Schronisko.Models
{
    public class Adopcja
    {
        [Required(ErrorMessage = "Imie jest wymagane.")]
        [MinLength(3, ErrorMessage = "imie musi miec min 3 znaki")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MinLength(2, ErrorMessage = "Nazwisko musi mieć co najmniej 2 znaki.")]
        [MaxLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków.")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage ="Wiek jest wymagany. ")]
        public string Wiek {  get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Numer telefonu musi mieć dokładnie 9 cyfr.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numer telefonu może zawierać tylko cyfry.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email (musi zawierać @).")]
        public string Email { get; set; }
       
        public string TypMieszkania { get; set; }

        public string Ogrod {  get; set; }

        [Required(ErrorMessage ="Ilosc osob wymagana")]
        [RegularExpression(@"^(30|[1-2]?[0-9])$", ErrorMessage ="Ilość osób musi być liczbą od 1-30")]
        public string IloscOsob {  get; set; }

        public string Doswiadczenie { get; set; }

        public string? AktualneZwierzeta { get; set; }

    }
    public class poprawnoscAdopcji()
    {
         public bool poprawnoscOgrod(string ogrod)
         {
             if (ogrod == "tak" || ogrod == "nie")
                 return true;
             return false;
         }
        public bool poprawnoscDoswiadczenie(string doswiadczenie)
        {
            if(doswiadczenie=="tak" || doswiadczenie=="nie")
                return true;
            return false;
        }
        public bool poprawnoscTypMieszkania(string typMieszkania)
        {
            if (typMieszkania == "dom" || typMieszkania == "mieszkanie")
                return true;
            return false;
        }
        public bool poprawnoscWiek(string wiek)
        {
            if (int.Parse(wiek) >= 18)
                return true;
            return false;
        }
        public bool poprawnoscEmail(string email)
        {
            try
            {
                var domain = email.Substring(email.IndexOf('@') + 1);
                var hostEntry = Dns.GetHostEntry(domain);
                return hostEntry.AddressList.Length > 0;
            }
            catch (SocketException)
            {
                return false; // Domeny nie ma w DNS
            }
        }
    }
}
