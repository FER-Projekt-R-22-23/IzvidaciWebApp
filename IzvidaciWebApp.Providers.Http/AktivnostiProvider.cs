using System.Collections;
using System.Net.Http.Json;
using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Dtos;
using IzvidaciWebApp.Providers.Http.Options;

namespace IzvidaciWebApp.Providers.Http;

public class AktivnostiProvider : IAktivnostProvider
{
    private readonly AkcijaProviderOptions _options;
    private readonly HttpClient _httpClient;

    public AktivnostiProvider(AkcijaProviderOptions options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient("AkcijeOptions");
    }

    public async Task<Result<IzvidaciWebApp.Domain.Models.Aktivnost>> Get(int id)
    {
        var aktivnostDto = (await _httpClient.GetFromJsonAsync<IEnumerable<IzvidaciWebApp.Providers.Http.Dtos.AktivnostDto>>($"/api/Aktivnosti/{id}"))?.FirstOrDefault();
        if (aktivnostDto is not null)
        {
            var aktivnost = DtoMapping.ToDomain(aktivnostDto);
            return Results.OnSuccess<IzvidaciWebApp.Domain.Models.Aktivnost>(aktivnost);
        }
        return Results.OnFailure<IzvidaciWebApp.Domain.Models.Aktivnost>("Rang ne postoji");
    }

    public async Task<Result<IEnumerable<Aktivnost>>> GetAll()
    {
        var aktivnostDto = (await _httpClient.GetFromJsonAsync<IEnumerable<AktivnostDto>>($"/api/Aktivnosti"));

        if (aktivnostDto is not null)
        {
            var aktivnost = aktivnostDto.Select(r => DtoMapping.ToDomain(r));
            return Results.OnSuccess<IEnumerable<IzvidaciWebApp.Domain.Models.Aktivnost>>(aktivnost);
        }
        return Results.OnFailure<IEnumerable<IzvidaciWebApp.Domain.Models.Aktivnost>>("Rangovi ne postoji");
    }

    public async Task<Result> Delete(int id)
    {
        String str = "api/Aktivnosti/" + id + "";
        var response = _httpClient.DeleteAsync(str).IsCompletedSuccessfully;
        if (!response)
        {
            Console.WriteLine("L");
            return Results.OnFailure("Neuspjesno");
        }

        return Results.OnSuccess("Uspjesno obrisano");
    }

    public async Task<Result> Create(Aktivnost aktivnost)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Update(Aktivnost aktivnost)
    {
        throw new NotImplementedException();
    }
}