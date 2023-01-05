using BaseLibrary;
using IzvidaciWebApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzvidaciWebApp.Providers;
public interface IAkcijaProvider
{
    Result<Akcija> Get(int id);
    Result<IEnumerable<Akcija>> GetAll();
}
