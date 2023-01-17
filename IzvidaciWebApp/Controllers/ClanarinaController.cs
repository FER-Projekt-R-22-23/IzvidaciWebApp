using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IzvidaciWebApp.Controllers;

public class ClanarinaController : Controller
{
    private readonly IClanarineProvider _clanarineProvider;
    private readonly IClanProvider _clanProvider;
    public ClanarinaController(IClanarineProvider clanarineProvider, IClanProvider clanProvider)
    {
        _clanarineProvider = clanarineProvider;
        _clanProvider = clanProvider;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _clanarineProvider.GetAll();
        var clanovi = await _clanProvider.GetAll();
        var idClana = result.Data.Select(c => c.ClanId);

        ClanarineViewModel cvm = new ClanarineViewModel();
        cvm.clanarine = result.Data.Select(r => new ClanarinaViewModel()
        {
            Id = r.Id,
            Placenost = r.Placenost,
            Iznos = r.Iznos,
            Godina = r.Godina,
            ClanIme = clanovi.Data.Where(c => c.Id == r.ClanId).Select(c => c.Ime).First(),
            ClanPrezime = clanovi.Data.Where(c => c.Id == r.ClanId).Select(c => c.Prezime).First(),
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

    private async Task PrepareDropDownList()
    {
        var clanovi = await _clanProvider.GetAll();
        var imena = clanovi.Data.Select(c => new { c.Id, c.Prezime }).ToList();

        ViewBag.Clanovi = new SelectList(imena, nameof(Clan.Id), nameof(Clan.Prezime));
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
           // Clan = clanarina.Data.Clan.Prezime,
            ClanId = clanarina.Data.ClanId,
            Datum = clanarina.Data.Datum
            
        };
        await PrepareDropDownList();
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
        await PrepareDropDownList();
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