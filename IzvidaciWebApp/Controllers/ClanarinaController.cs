using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers;

public class ClanarinaController : Controller
{
    private readonly IClanarineProvider _clanarineProvider;
    public ClanarinaController(IClanarineProvider clanarineProvider)
    {
        _clanarineProvider = clanarineProvider;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _clanarineProvider.GetAll();
        ClanarineViewModel cvm = new ClanarineViewModel();
        cvm.clanarine = result.Data.Select(r => new ClanarinaViewModel()
        {
            Id = r.Id,
            Placenost = r.Placenost,
            Iznos = r.Iznos,
            Godina = r.Godina,
            //ClanIme = r.Clan.Ime,
            //ClanPrezime = r.Clan.Prezime,
            ClanId = r.ClanId,
            Datum = r.Datum
        });
        return View(cvm);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clanarineProvider.Delete(id);
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
        var clanarina = await _clanarineProvider.Get(id);
        var c = new ClanarinaViewModel()
        {
            Id = clanarina.Data.Id,
            Placenost = clanarina.Data.Placenost,
            Iznos = clanarina.Data.Iznos,
            Godina = clanarina.Data.Godina,
            //ClanIme = clanarina.Data.Clan.Ime,
            //ClanPrezime = clanarina.Data.Clan.Prezime,
            ClanId = clanarina.Data.ClanId,
            Datum = clanarina.Data.Datum
            
        };
        return View(c);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(ClanarinaViewModel clanarina)
    {
        Clanarina c = new Clanarina(
                                clanarina.Id,
                                clanarina.Placenost,
                                clanarina.Iznos,
                                clanarina.Godina,
                                clanarina.ClanId,
                                clanarina.Datum
                            );
        var result = await _clanarineProvider.Edit(clanarina.Id, c);
        if (!result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClanarinaViewModel clanarina)
    {
        if (clanarina is not null)
        {
            Clanarina c = new Clanarina(
                                clanarina.Id,
                                clanarina.Placenost,
                                clanarina.Iznos,
                                clanarina.Godina,
                                clanarina.ClanId,
                                clanarina.Datum
                            );

            var result = await _clanarineProvider.Create(c);
            if (!result.IsSuccess)
            {
                Console.Out.WriteLine("Neuspjesno!");
                return RedirectToAction(nameof(Index));
            }
        }

        return RedirectToAction(nameof(Index));
    }
}