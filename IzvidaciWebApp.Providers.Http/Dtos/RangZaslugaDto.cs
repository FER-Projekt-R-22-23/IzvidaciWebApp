using IzvidaciWebApp.Domain.Models;

namespace IzvidaciWebApp.Providers.Http.Dtos;

public class RangZaslugaDto
{
    public int id { get; set; }
    public string naziv { get; set; }
    
}

public static partial class DtoMapping
{
    public static RangZaslugaDto ToDto(this Domain.Models.RangZasluga clan)
        => new RangZaslugaDto()
        {
            id = clan.Id,
            naziv = clan.Naziv
        };

    public static Domain.Models.RangZasluga ToDomain(this RangZaslugaDto clan
    )
        => new Domain.Models.RangZasluga(clan.id,clan.naziv);
}