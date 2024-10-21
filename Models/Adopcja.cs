namespace Schronisko.Models
{
    public class Adopcja
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string TypMieszkania { get; set; } //mieszkanie dom
        public bool Ogrod {  get; set; }
        public int IloscOsob {  get; set; }
        public bool Doswiadczenie { get; set; }
        public string AktualneZwierzeta { get; set; }
        public string Opiekun {  get; set; }
        public string Spanie {  get; set; }
        //wywolywane po wybraniu formularz w zwierzeciu
    }
}
