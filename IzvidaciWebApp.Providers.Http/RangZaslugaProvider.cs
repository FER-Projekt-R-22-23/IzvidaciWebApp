using System.Net.Http.Json;
using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Dtos;
using IzvidaciWebApp.Providers.Http.Options;

namespace IzvidaciWebApp.Providers.Http;

public class RangZaslugaProvider
{
    private readonly RangZaslugaProviderOptions _options;
    private readonly HttpClient _httpClient;
    
    public RangZaslugaProvider(RangZaslugaProviderOptions options)
    {
        _options = options;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(options.BaseUrl);
    }
    public Result<RangZasluga> Get(int id)
    {
        var result = _httpClient.GetFromJsonAsync<IEnumerable<RangZaslugaDto>>($"{id}");
        if (result.Result!.Any())
        {
            var rangovi = result.Result!.Select(r => DtoMapping.ToDomain(r)).ToList()[0];
        }
        return Results.OnFailure<RangZasluga>("Rang ne postoji");
    }

    public Result<IEnumerable<RangZasluga>> GetAll()
    {
        var result = _httpClient.GetFromJsonAsync<IEnumerable<RangZaslugaDto>>($"");
        if (result.Result!.Any())
        {
            var rangovi = result.Result!.Select(r => DtoMapping.ToDomain(r));
        }
        return Results.OnFailure<IEnumerable<RangZasluga>>("Rangovi ne postoji");
    }
}