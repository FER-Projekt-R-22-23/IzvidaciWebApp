using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers
{
    public class UdrugeController : Controller
    {
        private readonly IUdrugeProvider mjestoProvider;
        public UdrugeController(IUdrugeProvider mjestoProvider)
        {
            this.mjestoProvider = mjestoProvider;
        }
        public async Task<IActionResult> Index()
        {
            var result = mjestoProvider.GetAll();
            UdrugeViewModel akt = new UdrugeViewModel();
            akt.udruge = result.Result.Data.Select(r => {
                return new UdrugaViewModel
                {
                    IdUdruge = r.IdUdruge,
                    OIB = r.OIB,
                    Naziv = r.Naziv,
                    Sjediste = r.Sjediste,
                    BrMob = r.BrMob,
                    Mail = r.Mail
                };
            });
            return View(akt);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UdrugaViewModel mjesto)
        {
            if (mjesto is not null)
            {
                Udruge mj = new Udruge(mjesto.IdUdruge, mjesto.OIB, mjesto.Naziv, mjesto.Sjediste, mjesto.BrMob, mjesto.Mail);

                var result = await mjestoProvider.Create(mj);
                if (!result.IsSuccess)
                {
                    Console.Out.WriteLine("Neuspjesno!");
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await mjestoProvider.Delete(id);
            if (!result.IsSuccess)
            {
                Console.WriteLine("Not Succesful!");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Console.Write("dsadskda\n");
            var mjesto = await mjestoProvider.Get(id);
            var akt = new UdrugaViewModel()
            {
                IdUdruge = id,
                OIB = mjesto.Data.OIB,
                Naziv = mjesto.Data.Naziv,
                Sjediste = mjesto.Data.Sjediste,
                BrMob = mjesto.Data.BrMob,
                Mail = mjesto.Data.Mail

            };
            return View(akt);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UdrugaViewModel mjesto)
        {
            Udruge akt = new Udruge(mjesto.IdUdruge, mjesto.OIB, mjesto.Naziv, mjesto.Sjediste, mjesto.BrMob, mjesto.Mail);
            var result = await mjestoProvider.Edit(mjesto.IdUdruge, akt);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
