
using Microsoft.AspNetCore.Mvc;
using Schronisko.Models;
using Schronisko.Views.Tekstowy;

namespace Schronisko.Controllers
{
    public class AdopcjaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Adopt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adopt(Adopcja adopcja)
        {
            ViewBag.Message = adopcja.Imie + " , " + adopcja.Nazwisko + ", " + adopcja.Telefon;

            return View();
        }

        private WidokTekstowy widokTekstowy;
        public AdopcjaController(WidokTekstowy _widokTekstowy)
        {
            widokTekstowy = _widokTekstowy;
        }
    }
}
