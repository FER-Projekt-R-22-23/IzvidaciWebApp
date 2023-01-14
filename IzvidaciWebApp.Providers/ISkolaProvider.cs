using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzvidaciWebApp.Providers;
public interface ISkolaProvider
{
    Task<Result<Skola>> GetSkolaEdukacije(int id);
    Task<Result<IEnumerable<Skola>>> GetAll();
    Task<Result<Edukacija>> GetEdukacija(int id);
}
