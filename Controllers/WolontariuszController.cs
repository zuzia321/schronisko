using Microsoft.AspNetCore.Mvc;
using Schronisko.Models;
using Schronisko.Views.Tekstowy;
using System;
using System.Drawing.Drawing2D;

namespace Schronisko.Controllers
{
    public class WolontariuszController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        string plik = "volounteers.txt";
        [HttpPost]
        public ActionResult Create(Wolontariusz wolontariusz)
        {
           // string plik = ("volounteers.txt");
            int index = plik.Count() + 1;
            Console.Write($"Twoj indeks do logowania: {index} ");
            ViewBag.Message = wolontariusz.Imie + " " + wolontariusz.Nazwisko+" "+index;
           // Console.WriteLine($"{wolontariusz.Imie}");

            string daneWolontariusz = $"\n{wolontariusz.Imie};{wolontariusz.Nazwisko};{wolontariusz.DataUrodzenia};{wolontariusz.Telefon};{wolontariusz.Email};{wolontariusz.Miasto};{wolontariusz.Opis};{wolontariusz.Doswiadczenie};{wolontariusz.Stan}";
            System.IO.File.AppendAllText(plik, daneWolontariusz);

            return View();
        }


        //konsola
        private WidokTekstowy widokTekstowy;
        public WolontariuszController(WidokTekstowy _widokTekstowy)
        {
            widokTekstowy = _widokTekstowy;
        }
        public void StworzIWyswietlWolontariusza(int pom)
        {
            Wolontariusz nowyWolontariusz = widokTekstowy.stworzWolontariusza(ref pom);
            if(pom== 0) 
                widokTekstowy.WyswietlWolontariusza(nowyWolontariusz);
        }
    }
}
