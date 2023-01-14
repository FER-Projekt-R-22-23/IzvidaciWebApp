using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Dtos;
using IzvidaciWebApp.Providers.Http.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IzvidaciWebApp.Providers.Http;
public class SkolaProvider : ISkolaProvider
{
    private readonly AkcijaProviderOptions _options;
    private readonly HttpClient _httpClient;

    public SkolaProvider(AkcijaProviderOptions options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient("AkcijeOptions");
    }

    public Task<Result<Skola>> Get(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<Skola>>> GetAll()
    {
        var result = (await _httpClient.GetFromJsonAsync<IEnumerable<SkolaDto>>($"/api/Skole"));

        if (result is not null)
        {
            var skole = result.Select(r => DtoMapping.ToDomain(r));
            return Results.OnSuccess<IEnumerable<Skola>>(skole);
        }
        return Results.OnFailure<IEnumerable<Skola>>("Skole ne postoji");
    }
}
