using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzvidaciWebApp.Providers.Http.Dtos
{
    internal class Mjesto
    {
        public int PbrMjesta { get; set; }

        [Required(ErrorMessage = "Naziv mjesta ne smije biti null.")]
        [StringLength(50, ErrorMessage = "Naziv mjesta ne smije biti duzi od 50 znakova.")]
        public string NazivMjesta { get; set; } = string.Empty;
    }
}
