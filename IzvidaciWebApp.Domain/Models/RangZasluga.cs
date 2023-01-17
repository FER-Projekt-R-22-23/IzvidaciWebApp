using System.ComponentModel.DataAnnotations;

namespace IzvidaciWebApp.Domain.Models;
public class RangZasluga 
{
    private readonly int _id;
    private readonly string _naziv;

    /*public RangZasluga(int id, string naziv)
    {
        _id = id;
        _naziv = naziv;
    }*/
    public RangZasluga()
    {
        
    }
    public int Id { get; set; }
    public string Naziv { get; set; }
}