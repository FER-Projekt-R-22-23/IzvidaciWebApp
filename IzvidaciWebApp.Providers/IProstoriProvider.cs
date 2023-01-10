using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IzvidaciWebApp.Providers;

public interface IProstoriProvider
{
    Task<Result<Prostori>> Get(int id);
    Task<Result<IEnumerable<Prostori>>>GetAll();
    Task<Result> Delete(int id);
    Task<Result> Create(Prostori prostori);
    Task<Result> Update(Prostori prostori);
}
