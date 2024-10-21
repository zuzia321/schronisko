namespace Schronisko.Models
{
    public class Wolontariusz
    {
        public int Id { get; set; }
        public string Imie {  get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string Telefon {  get; set; }
        public string Email { get; set; }
        public bool Miasto { get; set; }
        public string Opis { get; set; }
        public bool Doswiadczenie { get; set; }
        public string Dyspozycyjnosc {  get; set; }

        //public void index()
    }
}
