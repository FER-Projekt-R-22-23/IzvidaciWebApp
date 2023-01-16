using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
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
                return RedirectToAction(nameof(EdukacijaPrijavljeni), new { idEdukacije = idEdukacije });
            }
            return RedirectToAction(nameof(EdukacijaPrijavljeni), new { idEdukacije = idEdukacije });
        }

        [HttpGet]
        public async Task<IActionResult> EditSkola(int id)
        {
            var skola  = await _skolaProvider.GetSkola(id);
            var skolaViewModel = new SkolaViewModel
            {
                Id = skola.Data.IdSkole,
                NazivSkole = skola.Data.NazivSkole,
                MjestoPbr = skola.Data.MjestoPbr,
                Organizator = skola.Data.Organizator,
                KontaktOsoba = skola.Data.KontaktOsoba,
            };
            return View(skolaViewModel);
        }

        [HttpGet]
        public IActionResult CreateSkola()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSkola(SkolaViewModel skola)
        {
            if (skola is not null)
            {
                Skola skolaDomain = new Skola(skola.Id, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba);

                var result = await _skolaProvider.CreateSkola(skolaDomain);
                if (!result.IsSuccess)
                {
                    Console.Out.WriteLine("Neuspjesno!");
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> EditSkola(SkolaViewModel skola)
        {
            Skola skolaDomain = new Skola(skola.Id, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba);
            var result = await _skolaProvider.EditSkola(skolaDomain);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> DeleteSkola(int id)
        {
            var result = await _skolaProvider.DeleteSkola(id);
            if (!result.IsSuccess)
            {
                Console.WriteLine("Not Succesful!");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateEdukacija(int idSkole)
        {
            ViewBag.idSkole = idSkole;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdukacija(int skolaId, EdukacijaViewModel edukacija)
        {
            if (edukacija is not null)
            {
                Edukacija edukacijaDomain = new Edukacija(edukacija.Id, edukacija.NazivEdukacije,edukacija.MjestoPbr, edukacija.Opis, skolaId);

                var result = await _skolaProvider.CreateEdukacija(skolaId, edukacijaDomain);
                if (!result.IsSuccess)
                {
                    Console.Out.WriteLine(edukacija.NazivEdukacije);
                    return RedirectToAction(nameof(SkolaEdukacije), new { idSkole = skolaId });
                }
            }

            return RedirectToAction(nameof(SkolaEdukacije), new { idSkole = skolaId });
        }

        public async Task<IActionResult> PrijaviPolaznika(int idEdukacije, int idClan)
        {
            var polaznikDomain = new PrijavljeniClanNaEdukaciji(idClan, DateTime.Now);
            var result = await _skolaProvider.PrijaviPolaznika(idEdukacije, polaznikDomain);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(EdukacijaPrijavljeni), new { idEdukacije = idEdukacije });
            }
            return RedirectToAction(nameof(EdukacijaPrijavljeni), new { idEdukacije = idEdukacije });
        }

        public async Task<IActionResult> PrijaviPredavaca(int idEdukacije, int idClan)
        {
            var predavacDomain = new PredavacNaEdukaciji(0, idClan);
            var result = await _skolaProvider.PrijaviPredavaca(idEdukacije, predavacDomain);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(EdukacijaPredavaci), new { idEdukacije = idEdukacije });
            }
            return RedirectToAction(nameof(EdukacijaPredavaci), new { idEdukacije = idEdukacije });
        }

        [HttpPost]
        public async Task<IActionResult> EditEdukacija(EdukacijaViewModel edukacija)
        {
            Edukacija edukacijaDomain = new Edukacija(edukacija.Id, edukacija.NazivEdukacije, edukacija.MjestoPbr, edukacija.Opis, edukacija.IdSkole);
            var result = await _skolaProvider.EditEdukacija(edukacijaDomain);
            if (!result.IsSuccess)
            {
                return View();
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditEdukacija(int id)
        {

            var edukacija = await _skolaProvider.GetEdukacijaBasic(id);
            ViewBag.skolaId = edukacija.Data.SkolaId;

            var edukacijaViewModel = new EdukacijaViewModel
            {
                Id = edukacija.Data.Id,
                NazivEdukacije = edukacija.Data.NazivEdukacije,
                MjestoPbr = edukacija.Data.MjestoPbr,
                Opis = edukacija.Data.OpisEdukacije,
                IdSkole = edukacija.Data.SkolaId
            };
            return View(edukacijaViewModel);
        }

        public async Task<IActionResult> OdjaviPolaznika(int idEdukacije, int idClan)
        {
            var result = await _skolaProvider.OdjaviClana(idEdukacije, idClan);
            if (!result.IsSuccess)
            {
                return RedirectToAction(nameof(EdukacijaPrijavljeni), new { idEdukacije = idEdukacije });
            }
            return RedirectToAction(nameof(EdukacijaPrijavljeni), new { idEdukacije = idEdukacije });
        }

        public async Task<IActionResult> DeleteEdukacija(int id, int idSkole)
        {
            var result = await _skolaProvider.DeleteEdukacija(id);
            if (!result.IsSuccess)
            {
                Console.WriteLine("Not Succesful!");
                return RedirectToAction(nameof(SkolaEdukacije), new { idSkole = idSkole });
            }
            return RedirectToAction(nameof(SkolaEdukacije), new { idSkole = idSkole });
        }
    }
}