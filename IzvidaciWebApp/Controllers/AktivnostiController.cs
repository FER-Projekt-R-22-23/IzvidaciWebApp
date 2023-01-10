using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers
{
    public class AktivnostiController : Controller
    {
        private readonly IAktivnostProvider _aktivnostProvider;
        public AktivnostiController(IAktivnostProvider aktivnostProvider)
        {
            _aktivnostProvider = aktivnostProvider;
        }
        public async Task<IActionResult> Index()
        {
            var result = _aktivnostProvider.GetAll();
            AktivnostiViewModel akt = new AktivnostiViewModel();
            akt.aktivnosti = result.Result.Data.Select(r => new AktivnostViewModel
            {
                IdAktivnost = r.Id,
                MjestoPbr = r.MjestoPbr,
                KontaktOsoba = r.KontaktOsoba,
                Opis = r.Opis,
                AkcijaId = r.AkcijaId
            });
            return View(akt);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = _aktivnostProvider.Delete(id).Result;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
