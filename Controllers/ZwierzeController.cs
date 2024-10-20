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

        [HttpPost]
        public ActionResult CreateZwierze(Zwierze zwierze)
        {
            ViewBag.Message = zwierze.Imie + " , " + zwierze.RodzajZwierzecia + ", " + zwierze.Wiek;
            Console.WriteLine($"{zwierze.Imie}");

            return View();
        }

        private WidokTekstowy view;
        public ZwierzeController(WidokTekstowy view)
        {
            this.view = view;
        }
        public void StworzIWyswietlZwierze()
        {
            Zwierze noweZwierze = view.stworzZwierze();
            view.WyswietlZwierze(noweZwierze);
        }
    }
}
