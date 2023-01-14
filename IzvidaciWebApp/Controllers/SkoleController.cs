using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IzvidaciWebApp.Controllers
{
    public class SkoleController : Controller
    {
        private readonly ISkolaProvider _skolaProvider;

        public SkoleController(ISkolaProvider skoleProvider)
        {
            _skolaProvider = skoleProvider;
        }
        public async Task<IActionResult> Index()
        {
            var result = _skolaProvider.GetAll();
            SkoleViewModel skoleViewModel = new SkoleViewModel();
            skoleViewModel.skole = result.Result.Data.Select(r => new SkolaViewModel
            {
                Id = r.IdSkole,
                NazivSkole = r.NazivSkole,
                MjestoPbr = r.MjestoPbr,
                Organizator = r.Organizator,
                KontaktOsoba = r.KontaktOsoba,
            });
            return View(skoleViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

      

    }
}