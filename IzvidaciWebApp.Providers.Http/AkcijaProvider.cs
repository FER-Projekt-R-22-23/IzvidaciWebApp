using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzvidaciWebApp.Providers.Http;
public class AkcijaProvider : IAkcijaProvider
{
    private readonly AkcijaProviderOptions _options;

    public AkcijaProvider(AkcijaProviderOptions options)
    {
        _options = options;
        
    }

    public Result<Akcija> Get(int id)
    {
        // use the HttpClient here
        throw new NotImplementedException();
    }

    public Result<IEnumerable<Akcija>> GetAll()
    {
        throw new NotImplementedException();
    }
}
