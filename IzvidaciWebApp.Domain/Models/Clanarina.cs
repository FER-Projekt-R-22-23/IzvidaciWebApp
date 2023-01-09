﻿namespace IzvidaciWebApp.Domain.Models;
public class Clanarina
{
    private readonly int _id;
    private readonly bool _placenost;
    private readonly decimal _iznos;
    private readonly int _godina;
    private readonly int _clanId;
    private readonly DateTime? _datum;


    public int Id => _id;
    public bool Placenost => _placenost;
    public decimal Iznos => _iznos;
    public int Godina => _godina;
    public int ClanId => _clanId;
    public DateTime? Datum => _datum;

    public Clanarina(int id, bool placenost, decimal iznos, int godina, int clanId ,DateTime? datum)
    {
        _id = id;
        _placenost = placenost;
        _iznos = iznos;
        _godina = godina;
        _datum = datum;
        _clanId = clanId;
    }
}
