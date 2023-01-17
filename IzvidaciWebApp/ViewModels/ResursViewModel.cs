namespace IzvidaciWebApp.ViewModels;

public class ResursViewModel
{
    private DateOnly _datumNabave;
    
    public int IdResurs { get; set; }
    public int IdUdruga { get; set; } 
    public int IdProstor { get; set; }
    public string Naziv { get; set; }
    public DateTime DatumNabaveEdit { get; set; }
    public DateOnly DatumNabave { get => _datumNabave;
        set => _datumNabave = value;
    }
    public string? Napomena { get; set; }
}