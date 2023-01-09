using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers;

public class RangZaslugaController : Controller
{
    public async Task<IActionResult> Index()
    {
        var provider = new RangZaslugaProvider(new RangZaslugaProviderOptions()
        {
            BaseUrl = "http://localhost:7273/api/RangZasluga"
        });
        var result = provider.GetAll();
        return View(result.Data);
    }
}