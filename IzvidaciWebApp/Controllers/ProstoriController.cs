using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers;

public class ProstoriController : Controller
{
    private readonly IProstoriProvider _prostoriProvider;
    public ProstoriController(IProstoriProvider ProstoriProvider)
    {
        _ProstoriProvider = ProstoriProvider;
    }
    public async Task<IActionResult> Index()
    {
        var result = _ProstoriProvider.GetAll();
       ProstoriViewModel rzv = new ProstoriViewModel();
       rzv.prostori = result.Result.Data.Select(r => new ProstoriViewModel
        {
            id = r.Id,
            idUdruge = r.IdUdruge,
            adresa = r.Adresa,
            namjena = r.Namjena,
            dodijelio = r.Dodijelio,
            dodjeljenoDo = r.DodjeljenoDo,
            geoDuzina = r.GeoDuzina,
            geoSirina = r.GeoSirina
        });
        return View(rzv);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = _ProstoriProvider.Delete(id).Result;
        return RedirectToAction(nameof(Index));
    }
}