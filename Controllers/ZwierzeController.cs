using Microsoft.AspNetCore.Mvc;
using Schronisko.Models;
using Schronisko.Views.Tekstowy;

namespace Schronisko.Controllers
{
    public class ZwierzeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult CreateZwierze()
        {
            return View();
        }


        string plik = "Animals.txt";
        [HttpPost]
        public ActionResult CreateZwierze(Zwierze zwierze)
        {
            ViewBag.Message = zwierze.Imie + " , " + zwierze.RodzajZwierzecia + ", " + zwierze.Wiek;
            Console.WriteLine($"{zwierze.Imie}");
            
            string daneZwierze = $"{zwierze.Imie};{zwierze.Wiek};{zwierze.OdKiedyWSchronisku};{zwierze.RodzajZwierzecia};{zwierze.Gatunek};{zwierze.Stan};{zwierze.Opis}";
            System.IO.File.AppendAllText(plik, daneZwierze);
            
            return View();
        }

        private WidokTekstowy widokTekstowy;
        public ZwierzeController(WidokTekstowy _widokTekstowy)
        {
            widokTekstowy = _widokTekstowy;
        }
        public void StworzIWyswietlZwierze()
        {
            Zwierze noweZwierze = widokTekstowy.stworzZwierze();
            widokTekstowy.WyswietlZwierze(noweZwierze);
        }
    }
}
