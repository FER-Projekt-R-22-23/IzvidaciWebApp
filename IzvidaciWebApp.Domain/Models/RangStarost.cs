namespace IzvidaciWebApp.Domain.Models;
public class RangStarost
{
    private readonly int _id;
    private readonly string _naziv;

    public RangStarost(int id, string naziv)
    {
        _id = id;
        _naziv = naziv;
    }
    public int Id => _id;
    public string Naziv => _naziv;
}
