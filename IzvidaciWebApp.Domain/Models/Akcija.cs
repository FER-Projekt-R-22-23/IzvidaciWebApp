namespace IzvidaciWebApp.Domain.Models;
public class Akcija
{
    private readonly int _id;
    private readonly string _name;

    public Akcija(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public int Id => _id;

    public string Name => _name;
}
