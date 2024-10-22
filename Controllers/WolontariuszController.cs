using Microsoft.AspNetCore.Mvc;
using Schronisko.Models;
using Schronisko.Views.Tekstowy;
using System;

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

        [HttpPost]
        public ActionResult Create(Wolontariusz wolontariusz)
        {
            ViewBag.Message = wolontariusz.Id + " , " + wolontariusz.Imie + ", " + wolontariusz.Nazwisko;
            Console.WriteLine($"{wolontariusz.Imie}");

            return View();
        }


        //konsola
        private WidokTekstowy widokTekstowy;
        public WolontariuszController(WidokTekstowy _widokTekstowy)
        {
            widokTekstowy = _widokTekstowy;
        }
        public void StworzIWyswietlWolontariusza()
        {
            Wolontariusz nowyWolontariusz = widokTekstowy.stworzWolontariusza();
            widokTekstowy.WyswietlWolontariusza(nowyWolontariusz);
        }
    }
}
