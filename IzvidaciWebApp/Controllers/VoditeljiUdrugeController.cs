using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers
{
    public class VoditeljiUdrugeController : Controller
    {
        private readonly IVoditeljiUdrugeProvider mjestoProvider;
        public VoditeljiUdrugeController(IVoditeljiUdrugeProvider mjestoProvider)
        {
            this.mjestoProvider = mjestoProvider;
        }
        public async Task<IActionResult> Index()
        {

            var result = await mjestoProvider.GetAll();
            VoditeljiUdrugeViewModel akt = new VoditeljiUdrugeViewModel();
            akt.voditeljiUdruge = result.Data.Select(r => {
                return new VoditeljUdrugeViewModel
                {
                    IdUdruge = r.IdUdruge,
                    IdClan = r.IdClan,
                    Pozicija = r.Pozicija,
                    NaPozicijiDo = r.NaPozicijiDo
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
        public async Task<IActionResult> Create(VoditeljUdrugeViewModel mjesto)
        {
            if (mjesto is not null)
            {
                VoditeljiUdruge mj = new VoditeljiUdruge(mjesto.IdUdruge, mjesto.IdClan, mjesto.Pozicija, mjesto.NaPozicijiDo);

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

            var mjesto = await mjestoProvider.Get(id);
            var akt = new VoditeljUdrugeViewModel()
            {
                IdUdruge = id,
                IdClan = mjesto.Data.IdClan,
                Pozicija = mjesto.Data.Pozicija,
                NaPozicijiDo = mjesto.Data.NaPozicijiDo
            };
            return View(akt);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(VoditeljUdrugeViewModel mjesto)
        {
            VoditeljiUdruge akt = new VoditeljiUdruge(mjesto.IdUdruge, mjesto.IdClan, mjesto.Pozicija, mjesto.NaPozicijiDo);
            var result = await mjestoProvider.Edit(mjesto.IdClan, akt);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
