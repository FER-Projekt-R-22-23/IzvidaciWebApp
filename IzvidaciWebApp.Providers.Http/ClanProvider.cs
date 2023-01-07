using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Options;

namespace IzvidaciWebApp.Providers.Http;

public class ClanProvider : IClanProvider
{
    private readonly ClanProviderOptions _options;

    public ClanProvider(ClanProviderOptions options)
    {
        _options = options;
        
    }
    public Result<Clan> Get(int id)
    {
        // use the HttpClient here
        throw new NotImplementedException();
    }

    public Result<IEnumerable<Clan>> GetAll()
    {
        throw new NotImplementedException();
    }
}