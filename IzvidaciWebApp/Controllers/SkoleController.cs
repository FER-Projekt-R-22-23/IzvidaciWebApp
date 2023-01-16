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

        public async Task<IActionResult> SkolaEdukacije(int idSkole)
        {
            var result = _skolaProvider.GetSkolaEdukacije(idSkole);
            EdukacijeViewModel edukacijeViewModel = new EdukacijeViewModel();
            edukacijeViewModel.IdSkole = idSkole;
            edukacijeViewModel.NazivSkole = result.Result.Data.NazivSkole;
            edukacijeViewModel.edukacije = result.Result.Data.EdukacijeUSkoli.Select(r => new EdukacijaViewModel
            {
                Id = r.Id,
                NazivEdukacije = r.NazivEdukacije,
                MjestoPbr = r.MjestoPbr,
                Opis = r.OpisEdukacije,
            });
            return View(edukacijeViewModel);
        }


        public async Task<IActionResult> EdukacijaPredavaci(int idEdukacije)
        {
            var result = _skolaProvider.GetEdukacija(idEdukacije);
            PredavaciViewModel predavaciViewModel = new PredavaciViewModel();
            predavaciViewModel.IdEdukacije = idEdukacije;
            predavaciViewModel.NazivEdukacije = result.Result.Data.NazivEdukacije;
            predavaciViewModel.predavaci = result.Result.Data.PredavaciNaEdukaciji.Select(r => new PredavacViewModel
            {
                Id = r.idPredavac,
                IdClan = r.idClan
            });
            return View(predavaciViewModel);
        }

        public async Task<IActionResult> EdukacijaPrijavljeni(int idEdukacije)
        {
            var result = _skolaProvider.GetEdukacija(idEdukacije);
            PrijavljeniViewModel prijavljeniViewModel = new PrijavljeniViewModel();
            prijavljeniViewModel.IdEdukacije = idEdukacije;
            prijavljeniViewModel.NazivEdukacije = result.Result.Data.NazivEdukacije;
            prijavljeniViewModel.prijavljeni = result.Result.Data.PrijavljeniNaEdukaciji.Select(r => new PrijavljenViewModel
            {
                IdClan = r.idPolaznik,
                Datum = r.datumPrijave
            });
            return View(prijavljeniViewModel);
        }

        public async Task<IActionResult> EdukacijaPolaznici(int idEdukacije)
        {
            var result = _skolaProvider.GetEdukacija(idEdukacije);
            PolazniciViewModel polazniciViewModel = new PolazniciViewModel();
            polazniciViewModel.IdEdukacije = idEdukacije;
            polazniciViewModel.NazivEdukacije = result.Result.Data.NazivEdukacije;
            polazniciViewModel.polaznici = result.Result.Data.PolazniciEdukacije.Select(r => new PolaznikViewModel
            {
                IdClan = r.idPolaznik,
            });
            return View(polazniciViewModel);
        }

        public async Task<IActionResult> DolaziNaEdukaciju(int idEdukacije, int idClan)
        {
            var polaznikDomain = new PolaznikNaEdukaciji(idClan);
            var result = await _skolaProvider.DolaziNaEdukaciju(idEdukacije, polaznikDomain);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditSkola(SkolaViewModel skola)
        {
            var skolaDomain = new Skola(skola.Id, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba);
            var result = await _skolaProvider.EditSkola(skolaDomain);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        /*public async Task<IActionResult> EditSkola(int id)
        {
            var skola = await _aktivnostProvider.Get(id);
            var akt = new AktivnostViewModel()
            {
                IdAktivnost = id,
                MjestoPbr = aktivnost.Data.MjestoPbr,
                KontaktOsoba = aktivnost.Data.KontaktOsoba,
                Opis = aktivnost.Data.Opis,
                AkcijaId = aktivnost.Data.AkcijaId

            };
            return View(akt);
        }*/

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

      

    }
}