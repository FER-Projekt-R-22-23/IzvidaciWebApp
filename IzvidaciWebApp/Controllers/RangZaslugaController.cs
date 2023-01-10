using IzvidaciWebApp.Domain.Models;
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
        var result = await _rangZaslugaProvider.GetAll();
        RangZaslugeViewModel rzv = new RangZaslugeViewModel();
       rzv.rangovi = result.Data.Select(r => new RangZaslugaViewModel
        {
            id = r.Id,
            naziv = r.Naziv
        });
        return View(rzv);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _rangZaslugaProvider.Delete(id);
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
        var rang = await _rangZaslugaProvider.Get(id);
        var r = new RangZaslugaViewModel()
        {
            id = rang.Data.Id,
            naziv = rang.Data.Naziv
        };
        return View(r);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(RangZaslugaViewModel rangZasluga)
    {
        RangZasluga rang = new RangZasluga(rangZasluga.id, rangZasluga.naziv);
        var result = await _rangZaslugaProvider.Edit(rangZasluga.id,rang);
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
    public async Task<IActionResult> Create(RangZaslugaViewModel rangZasluga)
    {
        if (rangZasluga is not null)
        {
            RangZasluga rang = new RangZasluga(rangZasluga.id, rangZasluga.naziv);

            var result = await _rangZaslugaProvider.Create(rang);
            if (!result.IsSuccess)
            {
                Console.Out.WriteLine("Neuspjesno!");
                return RedirectToAction(nameof(Index));
            }
        }

        return RedirectToAction(nameof(Index));
    }
}