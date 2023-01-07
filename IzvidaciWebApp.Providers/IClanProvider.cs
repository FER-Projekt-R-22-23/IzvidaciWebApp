using BaseLibrary;
using IzvidaciWebApp.Domain.Models;

namespace IzvidaciWebApp.Providers;

public interface IClanProvider
{
    Result<Clan> Get(int id);
    Result<IEnumerable<Clan>> GetAll();
}
