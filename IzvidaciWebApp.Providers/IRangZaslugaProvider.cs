using BaseLibrary;
using IzvidaciWebApp.Domain.Models;

namespace IzvidaciWebApp.Providers;

public interface IRangZaslugaProvider
{
    Result<RangZasluga> Get(int id);
    Result<IEnumerable<RangZasluga>> GetAll();
}
