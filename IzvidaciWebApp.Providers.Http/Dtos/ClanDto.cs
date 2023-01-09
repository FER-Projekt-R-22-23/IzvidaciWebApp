namespace IzvidaciWebApp.Providers.Http.Dtos;

public class ClanDto
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public DateTime DatumRodenja { get; set; }
    public byte[]? Slika { get; set; }
    public string Adresa { get; set; }
    public bool ImaMaramu { get; set; }
    public DateTime? DatumMarama { get; set; }
    public string? MjestoMarama { get; set; }
    public int udrugaId { get; set; }
}