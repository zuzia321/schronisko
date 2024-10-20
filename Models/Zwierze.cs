namespace Schronisko.Models
{
    public class Zwierze
    {
        public string Id { get; set; }
        public string Imie { get; set; }
        public string Wiek {  get; set; }
        public DateTime OdKiedyWSchronisku {  get; set; }
        public string RodzajZwierzecia {get; set;}//może rozwijalna lista, jezeli jakiegos nie ma to inne i pojawia sie pole do wpisania
        public string Gatunek { get; set;}
        public string Stan { get; set; } //czy adoptowany, do adopcji itp.
        public string Opis { get; set; }
        //zdjęcie
    }
}
