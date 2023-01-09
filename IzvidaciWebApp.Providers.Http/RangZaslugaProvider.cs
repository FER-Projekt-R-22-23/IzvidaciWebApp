using System.Collections;
using System.Net.Http.Json;
using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Dtos;
using IzvidaciWebApp.Providers.Http.Options;

namespace IzvidaciWebApp.Providers.Http;

public class RangZaslugaProvider : IRangZaslugaProvider
{
    private readonly RangZaslugaProviderOptions _options;
    private readonly HttpClient _httpClient;
    
    public RangZaslugaProvider(RangZaslugaProviderOptions options,IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient("RangZaslugaOptions");
    }

    public async Task<Result<RangZasluga>> Get(int id)
    {
        var rangDto = (await _httpClient.GetFromJsonAsync<IEnumerable<RangZaslugaDto>>($"/api/RangZasluga/{id}"))?.FirstOrDefault();
        if (rangDto is not null)
        {
            var rang = DtoMapping.ToDomain(rangDto);
            return Results.OnSuccess<RangZasluga>(rang);
        }
        return Results.OnFailure<RangZasluga>("Rang ne postoji");
    }

    public async Task<Result<IEnumerable<RangZasluga>>> GetAll()
    {  
        var rangDto = (await _httpClient.GetFromJsonAsync<IEnumerable<RangZaslugaDto>>($"/api/RangZasluga"));

        if (rangDto is not null)
        {
            var rang = rangDto.Select(r => DtoMapping.ToDomain(r));
            return Results.OnSuccess<IEnumerable<RangZasluga>>(rang);
        }
        return Results.OnFailure<IEnumerable<RangZasluga>>("Rangovi ne postoji");
    }

    public async Task<Result> Delete(int id)
    {
        String str = "api/RangZasluga/"+id + "";
        var response = _httpClient.DeleteAsync(str).IsCompletedSuccessfully;
        if (!response)
        {
            Console.WriteLine("L");
            return Results.OnFailure("Neuspjesno");
        }

        return Results.OnSuccess("Uspjesno obrisano");
    }

    public async Task<Result> Create(RangZasluga rangZasluga)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Update(RangZasluga rangZasluga)
    {
        throw new NotImplementedException();
    }
}