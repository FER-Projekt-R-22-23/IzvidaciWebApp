/*using System.Collections;
using System.Net.Http.Json;
using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using IzvidaciWebApp.Providers.Http.Dtos;
using IzvidaciWebApp.Providers.Http.Options;

namespace IzvidaciWebApp.Providers.Http;

public class ProstoriProvider : IProstoriProvider
{
    private readonly ProstoriProviderOptions _options;
    private readonly HttpClient _httpClient;
    
    public ProstoriProvider(ProstoriProviderOptions options,IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient("ProstoriOptions");
    }

    public async Task<Result<Prostori>> Get(int id)
    {
        var prostorDto = (await _httpClient.GetFromJsonAsync<IEnumerable<ProstoriDto>>($"/api/Prostori/{id}"))?.FirstOrDefault();
        if (prostorDto is not null)
        {
            var prostor = DtoMapping.ToDomain(prostorDto);
            return Results.OnSuccess<Prostori>(prostor);
        }
        return Results.OnFailure<Prostori>("Prostor ne postoji");
    }

    public async Task<Result<IEnumerable<Prostori>>> GetAll()
    {  
        var prostorDto = (await _httpClient.GetFromJsonAsync<IEnumerable<ProstoriDto>>($"/api/Prostori"));

        if (prostorDto is not null)
        {
            var prostor = prostorDto.Select(r => DtoMapping.ToDomain(r));
            return Results.OnSuccess<IEnumerable<Prostori>>(prostor);
        }
        return Results.OnFailure<IEnumerable<Prostori>>("prostor ne postoji");
    }

    public async Task<Result> Delete(int id)
    {
        String str = "api/Prostori/"+id + "";
        var response = _httpClient.DeleteAsync(str).IsCompletedSuccessfully;
        if (!response)
        {
            Console.WriteLine("L");
            return Results.OnFailure("Neuspjesno");
        }

        return Results.OnSuccess("Uspjesno obrisano");
    }

    public async Task<Result> Create(Prostori Prostori)
    {
        String str = "api/Prostori/";
        var json = JsonConvert.SerializeObject(Prostori);
        var data = new StringContent(json,Encoding.UTF8,"application/json");
        var response = await _httpClient.PostAsync(str, data);
        if (!(response.StatusCode == HttpStatusCode.Accepted))
        {
            return Results.OnFailure("Neuspjesno");
        }

        return Results.OnSuccess("Uspjesno obrisano");
    }

    public async Task<Result> Edit(int id, Prostori Prostori)
    {
        String str = "api/Prostori/"+id;
        var json = JsonConvert.SerializeObject(Prostori);
        var data = new StringContent(json,Encoding.UTF8,"application/json");
        var response = _httpClient.PutAsync(str,data);
        if (!response.IsCompleted)
        {
            return Results.OnFailure("Neuspjesno");
        }
        return Results.OnSuccess("Uspjesno");
    }
}*/

