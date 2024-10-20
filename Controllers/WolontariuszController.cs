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

        private WidokTekstowy view;
        public WolontariuszController(WidokTekstowy view)
        {
            this.view = view;
        }
        public void StworzIWyswietlWolontariusza()
        {
            Wolontariusz nowyWolontariusz = view.stworzWolontariusza();
            view.WyswietlWolontariusza(nowyWolontariusz);
        }
    }
}
