using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers;

public class RangStarostController : Controller
{
    private readonly IRangStarostProvider _rangStarostProvider;
    public RangStarostController(IRangStarostProvider rangStarostProvider)
    {
        _rangStarostProvider = rangStarostProvider;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _rangStarostProvider.GetAll();
        RangStarostiViewModel rzv = new RangStarostiViewModel();
       rzv.rangovi = result.Data.Select(r => new RangStarostViewModel()
        {
            id = r.Id,
            naziv = r.Naziv
        });
        return View(rzv);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _rangStarostProvider.Delete(id);
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
        var rang = await _rangStarostProvider.Get(id);
        var r = new RangStarostViewModel()
        {
            id = rang.Data.Id,
            naziv = rang.Data.Naziv
        };
        return View(r);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(RangStarostViewModel rangStarost)
    {
        RangStarost rang = new RangStarost(rangStarost.id, rangStarost.naziv);
        var result = await _rangStarostProvider.Edit(rangStarost.id,rang);
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
    public async Task<IActionResult> Create(RangStarostViewModel rangStarost)
    {
        if (rangStarost is not null)
        {
            RangStarost rang = new RangStarost(rangStarost.id, rangStarost.naziv);

            var result = await _rangStarostProvider.Create(rang);
            if (!result.IsSuccess)
            {
                Console.Out.WriteLine("Neuspjesno!");
                return RedirectToAction(nameof(Index));
            }
        }

        return RedirectToAction(nameof(Index));
    }
}