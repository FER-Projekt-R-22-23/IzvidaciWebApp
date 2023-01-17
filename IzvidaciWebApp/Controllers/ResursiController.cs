using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Controllers;

public class ResursiController : Controller
{
    private readonly IResursiProvider _resursiProvider;
    public ResursiController(IResursiProvider resursiProvider)
    {
        _resursiProvider = resursiProvider;
    }
    public async Task<IActionResult> Index()
    {
        
        var result = await _resursiProvider.GetAll();
        ResursiViewModel res = new ResursiViewModel();
        res.Resursi = result.Data.Select(r => { 
            return new ResursViewModel()
            {
                IdResurs = r.Id,
                DatumNabave = DateOnly.Parse(r.DatumNabave.ToString().Split(" ")[0]),
                IdProstor = r.IdProstor,
                IdUdruga = r.IdUdruge,
                Napomena = r.Napomena,
                Naziv = r.Naziv
            };
        });
        return View(res);
    }
    
    public async Task<IActionResult> IndexPotrosni()
    {
        
        var result = await _resursiProvider.GetAllPotrosni();
        PotrosniResursiViewModel res = new PotrosniResursiViewModel();
        res.Resursi = result.Data.Select(r => { 
            return new PotrosniResursViewModel()
            {
                IdResurs = r.Id,
                DatumNabave = DateOnly.Parse(r.DatumNabave.ToString().Split(" ")[0]),
                IdProstor = r.IdProstor,
                IdUdruga = r.IdUdruge,
                Napomena = r.Napomena,
                Naziv = r.Naziv,
                RokTrajanja = DateOnly.Parse(r.RokTrajanja.ToString().Split(" ")[0])
            };
        });
        return View(res);
    }
    
    public async Task<IActionResult> IndexTrajni()
    {
        var result = await _resursiProvider.GetAllTrajni();
        TrajniResursiViewModel res = new TrajniResursiViewModel();
        res.Resursi = result.Data.Select(r => { 
            return new TrajniResursViewModel()
            {
                IdResurs = r.Id,
                DatumNabave = DateOnly.Parse(r.DatumNabave.ToString().Split(" ")[0]),
                IdProstor = r.IdProstor,
                IdUdruga = r.IdUdruge,
                Napomena = r.Napomena,
                Naziv = r.Naziv,
                Dostupno = r.JeDostupno ? "DA" : "NE",
                InventarniBroj = r.InventarniBroj
            };
        });
        return View(res);
    }
    
    [HttpGet]
    public IActionResult CreatePotrosni()
    {
        return View();
    }
    
    
    [HttpGet]
    public IActionResult CreateTrajni()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePotrosni(PotrosniResursViewModel? resurs)
    {
        if (resurs is not null)
        {
            PotrosniResurs res = new PotrosniResurs(resurs.IdResurs, resurs.Naziv, resurs.Napomena, DateTime.Parse(resurs.DatumNabave.ToString()), resurs.IdUdruga, resurs.IdProstor, DateTime.Parse(resurs.RokTrajanja.ToString()));
    
            var result = await _resursiProvider.CreatePotrosni(res);
            if (!result.IsSuccess)
            {
                Console.Out.WriteLine("Neuspjesno!");
                return RedirectToAction(nameof(IndexPotrosni));
            }
        }
    
        return RedirectToAction(nameof(IndexPotrosni));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTrajni(TrajniResursViewModel? resurs)
    {
        if (resurs is not null)
        {
            TrajniResurs res = new TrajniResurs(resurs.IdResurs, resurs.Naziv, resurs.Napomena, DateTime.Parse(resurs.DatumNabave.ToString()), resurs.IdUdruga, resurs.IdProstor, resurs.InventarniBroj, resurs.Dostupno.Equals("DA"));
    
            var result = await _resursiProvider.CreateTrajni(res);
            if (!result.IsSuccess)
            {
                Console.Out.WriteLine("Neuspjesno!");
                return RedirectToAction(nameof(IndexTrajni));
            }
        }
    
        return RedirectToAction(nameof(IndexTrajni));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _resursiProvider.Delete(id);
        if (!result.IsSuccess)
        {
            Console.WriteLine("Not Succesful!");
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> EditPotrosni(int id)
    {
        
        var resurs = (await _resursiProvider.GetPotrosni(id)).Data;
        var res = new PotrosniResursViewModel()
        {
            IdResurs = resurs.Id,
            DatumNabaveEdit = resurs.DatumNabave,
            IdProstor = resurs.IdProstor,
            IdUdruga = resurs.IdUdruge,
            Napomena = resurs.Napomena,
            Naziv = resurs.Naziv,
            RokTrajanjaEdit = resurs.RokTrajanja
        };
        return View(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditPotrosni(PotrosniResursViewModel resurs)
    {
        Console.WriteLine(resurs.IdResurs);
        PotrosniResurs res = new PotrosniResurs(resurs.IdResurs, resurs.Naziv, resurs.Napomena, resurs.DatumNabaveEdit, resurs.IdUdruga, resurs.IdProstor, resurs.RokTrajanjaEdit);
        var result = await _resursiProvider.EditPotrosni(res.Id, res);
        if (!result.IsSuccess)
        {
            return RedirectToAction(nameof(IndexPotrosni));
        }
        return RedirectToAction(nameof(IndexPotrosni));
    }
    
    [HttpGet]
    public async Task<IActionResult> EditTrajni(int id)
    {
        
        var resurs = (await _resursiProvider.GetTrajni(id)).Data;
        var res = new TrajniResursViewModel()
        {
            IdResurs = resurs.Id,
            DatumNabaveEdit = resurs.DatumNabave,
            IdProstor = resurs.IdProstor,
            IdUdruga = resurs.IdUdruge,
            Napomena = resurs.Napomena,
            Naziv = resurs.Naziv,
            Dostupno = resurs.JeDostupno ? "DA" : "NE",
            InventarniBroj = resurs.InventarniBroj
        };
        return View(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditTrajni(TrajniResursViewModel resurs)
    {
        Console.WriteLine(resurs.IdResurs);
        TrajniResurs res = new TrajniResurs(resurs.IdResurs, resurs.Naziv, resurs.Napomena, resurs.DatumNabaveEdit, resurs.IdUdruga, resurs.IdProstor, resurs.InventarniBroj, resurs.Dostupno.Equals("DA"));
        var result = await _resursiProvider.EditTrajni(res.Id, res);
        if (!result.IsSuccess)
        {
            return RedirectToAction(nameof(IndexTrajni));
        }
        return RedirectToAction(nameof(IndexTrajni));
    }
    
}
    