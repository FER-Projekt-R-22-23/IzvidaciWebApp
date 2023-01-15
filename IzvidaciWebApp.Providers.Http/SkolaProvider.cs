﻿using BaseLibrary;
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

    public async Task<Result<Skola>> GetSkolaEdukacije(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<SkolaEdukacijeDto>($"/api/Skole/SkoleAggregate/{id}");

        if (result is not null)
        {
            var skolaEdukacije = DtoMapping.ToDomainAggregate(result);
            return Results.OnSuccess<Skola>(skolaEdukacije);
        }
        return Results.OnFailure<Skola>("Skola ne postoji");
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

    public async Task<Result<Edukacija>> GetEdukacija(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<EdukacijaAggregateDto>($"/api/Edukacija/EdukacijaAggregate/{id}");

        if (result is not null)
        {
            var edukacija = DtoMapping.toDomainAggregate(result);
            return Results.OnSuccess<Edukacija>(edukacija);
        }
        return Results.OnFailure<Edukacija>("Edukacija ne postoji");
    }

    public async Task<Result> DolaziNaEdukaciju(int id, PolaznikNaEdukaciji polaznik)
    {
        String str = $"/api/Edukacija/DolaziNaEdukaciju/{id}";
        var json = JsonConvert.SerializeObject(polaznik.ToDto());
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PostAsync(str, data);
        if (!response.IsCompleted)
        {
            return Results.OnFailure("Neuspjesno");
        }
        return Results.OnSuccess("Uspjesno");
    }

    public async Task<Result> EditSkola(Skola skola)
    {
        String str = $"api/Skole/{skola.IdSkole}";
        var json = JsonConvert.SerializeObject(skola);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PutAsync(str, data);
        if (!response.IsCompleted)
        {
            Console.WriteLine("Greska");
            return Results.OnFailure("Neuspjesno");
        }
        return Results.OnSuccess("Uspjesno");
    }
}
