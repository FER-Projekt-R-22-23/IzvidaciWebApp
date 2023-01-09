using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers;

public class RangZaslugaController : Controller
{
    private readonly IRangZaslugaProvider _rangZaslugaProvider;
    public RangZaslugaController(IRangZaslugaProvider rangZaslugaProvider)
    {
        _rangZaslugaProvider = rangZaslugaProvider;
    }
    public async Task<IActionResult> Index()
    {
        var result = _rangZaslugaProvider.GetAll();
        RangZaslugeViewModel rzv = new RangZaslugeViewModel();
       rzv.rangovi = result.Result.Data.Select(r => new RangZaslugaViewModel
        {
            id = r.Id,
            naziv = r.Naziv
        });
        return View(rzv);
    }
}